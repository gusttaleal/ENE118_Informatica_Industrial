/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;

namespace Grafico
{
    public partial class Form1 : Form
    {
         int count = 0;
        // Inicialização de Componentes
        public Form1()
        {
            InitializeComponent();
            DisableConnection = new Disable_Connection_Delegate(Disable_Funtion);
            RefreshChart = new Refresh_Chart_Delegate(Refresh_Chart_Funtion);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = -300;
            chart1.ChartAreas[0].AxisY.Maximum = 300;
        }

        // Declaração dos Delegates
        private delegate void Refresh_Chart_Delegate(double data);
        private delegate void Disable_Connection_Delegate();

        // Instância dos Delegates
        private Refresh_Chart_Delegate RefreshChart;
        private Disable_Connection_Delegate DisableConnection;

        // Função de Atualização do Gráfico
        public void Refresh_Chart_Funtion(double data)
        {
            if (count < 100)
            {
                chart1.Series[0].Points.Add(data);
                count++;
            }
            else
            {
                chart1.Series[0].Points.Clear();
                count = 0;
            }
        }

        // Função de Tratamento de Dados
        public void Data_Treatment_Funtion(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            if (sp.IsOpen)
            {
                try
                {
                    // Leitura dos Dados
                    string indata = sp.ReadLine();
                    // Limpeza do Buffer(UART) de Entrada
                    sp.DiscardInBuffer();
                    // Verificação de Conexão (End Of Transmission)
                    if (indata == "EOT")
                    {
                        MessageBox.Show("Sensor desconectado");
                        this.BeginInvoke(this.DisableConnection);
                    }
                    else
                    {
                        // Invoke, ao contrário do BeginInvoke, é um comando bloqueante
                        this.chart1.Invoke(RefreshChart, Convert.ToDouble(indata));
                    }
                }
                catch
                {
                    MessageBox.Show("Erro ao ler os dados");
                }
            }
        }

        // Função para Habilitar a Porta Serial
        public void Enable_Funtion()
        {
            try
            {
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("Erro ao iniciar conexão");
            }
            this.m_connect.Text = "Desconectar";
            this.s_status.Text = "Conectado";
            this.l_index.BackColor = Color.Lime;

        }

        // Função para Desabilitar a Porta Serial
        public void Disable_Funtion()
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                }
            }
            catch
            {
                MessageBox.Show("Falha ao desligar o sensor");
            }
            this.m_connect.Text = "Conectar";
            this.s_status.Text = "Desconectado";
            this.l_index.BackColor = Color.Red;
        }

        // Função vinculada ao evento Click
        private void m_connect_Click(object sender, EventArgs e)
        {
            if (m_connect.Text == "Conectar")
            {
                Enable_Funtion();
            }
            else
            {
                this.BeginInvoke(this.DisableConnection);
            }
        }

        // Função vinculada ao evento Click
        private void m_config_Click(object sender, EventArgs e)
        {
            Form2 Config = new Form2(this, this.serialPort1);
            Config.Show();
        }
    }
}
