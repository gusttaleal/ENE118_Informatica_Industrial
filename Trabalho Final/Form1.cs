using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyModbus;
using System.Threading;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Runtime;

namespace ModbusTCPClient
{
    public partial class tb_dados : Form
    {
        ModbusClient modbus;
        int partida_real = 0;

        public tb_dados()
        {
            InitializeComponent();
            string[] ip = { "10.15.1.240", "10.0.0.13" };
            tb_ip.Items.AddRange(ip);
            string[] port = { "9003", "502" };
            tb_porta.Items.AddRange(port);
        }

        private void bt_connect_Click_1(object sender, EventArgs e)
        {
            if (ValidateIPv4(tb_ip.Text) && Int16.TryParse(tb_porta.Text, out short port))
            {
                Thread Refresh_Data = new Thread(Refresh_Interface);

                modbus = new ModbusClient(tb_ip.Text, port);
                modbus.ConnectionTimeout = 3000;

                if (bt_connect.Text == "Conectar")
                {
                    try
                    {
                        modbus.Connect();
                    }

                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                        return;
                    }

                    Refresh_Data.Start();

                    bt_dadosHist.Enabled = false;
                    bt_connect.Text = "Desconectar";
                }
                else
                {
                    modbus.Disconnect();
                    bt_dadosHist.Enabled = true;
                    bt_connect.Text = "Conectar";
                }
            }
            else
            {
                MessageBox.Show("IP ou porta inválidos");
            }
        }

        private void bt_soft_Click(object sender, EventArgs e)
        {
            escreveDados(1);
        }

        private void bt_inv_Click(object sender, EventArgs e)
        {
            escreveDados(2);
        }

        private void bt_direct_Click(object sender, EventArgs e)
        {
            escreveDados(3);
        }

        private void bt_lig_Click(object sender, EventArgs e)
        {
            escreveDados(4);
        }

        private void bt_deslig_Click(object sender, EventArgs e)
        {
            escreveDados(5);
        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            escreveDados(6);
        }

        private void bt_dadosHist_Click(object sender, EventArgs e)
        {

            StreamReader sr = new StreamReader("DataOUT.txt");
            string data;
            bool erro = false;
            try
            {
                //Lê apenas uma linha de arquivo txt 
                string[] dados = new string[1];
                while (!sr.EndOfStream)                              //armazenar a data e a hora em uma variavel e consultar no if , tem q transformar para a string 
                {
                    data = sr.ReadLine();
                    if (data == "\r\n")     //ideia colocar um if dentro de um if pois um vai olhar a hora e o outro vai ser por linha ai vai plotar tudo daquela data 
                    {
                        break;
                    }
                    if (data == "\n")
                    {
                        tb_acessoDados.Text += "\r\n";
                    }

                    for (int i = 0; i < dados.Length; i++)
                    {
                        dados = data.Select(x => x.ToString()).ToArray();

                        tb_acessoDados.Text += dados[i].ToString();

                    }
                }
            }

            catch (Exception err)
            {
                MessageBox.Show("Erro:" + err.Message);
                erro = true;
            }

            finally
            {
                if (!erro)
                {
                    MessageBox.Show("Dados Exibidos com Sucesso ");
                }
            }
            sr.Close();
        }

        public void timer1_Tick_1(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            lbldata.Text = localDate.ToString();
        }

        private void lbldata_Click(object sender, EventArgs e)
        {

        }
    }
}





