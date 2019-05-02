/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CLASSES;

namespace CLIENTE
{
    public partial class Client_Main : Form
    {
        NetworkStream Stream;

        // Declaração dos Delegates
        private delegate string Get_Interface_Delegate(object var);
        private delegate void Refresh_Interface_Delegate(object var);

        // Instância dos Delegates
        private Get_Interface_Delegate Get_Interface_Pointer;
        private Refresh_Interface_Delegate Refresh_Interface_Pointer;

        public Client_Main()
        {
            InitializeComponent();

            // Inicializa as ComboBoxes: Port e Ip
            Set_Config_Function();

            // Atribuição do Delegate
            Get_Interface_Pointer = new Get_Interface_Delegate(Get_Interface_Function);
            Refresh_Interface_Pointer = new Refresh_Interface_Delegate(Refresh_Interface_Function);

            // Inicia o software com as funções de envio desabilitadas
            Refresh_Interface_Function(5);
        }

        // Função para configuração inicial da interface
        private void Set_Config_Function()
        {
            // Configura as ComboBoxes
            cb_port.Items.Add("9900");
            cb_ip.Items.Add("127.0.0.1");

            cb_classes.Items.Add("Carro");
            cb_classes.Items.Add("Pessoa");
            cb_classes.Items.Add("Conta Bancária");
        }

        // Função para pegar IP e Porta configurados na Interface
        private string Get_Interface_Function(object var)
        {
            int flag = (int)var;
            string str = " ";

            switch (flag)
            {
                case 1:
                    str = cb_ip.Text;
                    break;

                case 2:
                    str = cb_port.Text;
                    break;
            }
            return str;
        }

        // Função para atualizar interface
        private void Refresh_Interface_Function(object var)
        {
            int flag = (int)var;

            switch (flag)
            {
                case 1:
                    // Conectado ao Servidor
                    b_connect.Text = "Desconectar";
                    bs_status.Text = "Conectado";
                    l_index.BackColor = Color.Lime;
                    break;

                case 2:
                    // Desconectado do Servidor
                    b_connect.Text = "Conectar";
                    bs_status.Text = "Desconectado";
                    l_index.BackColor = Color.Red;
                    break;

                case 3:
                    // Limpa todos os campos 
                    label1.Text = "-";
                    label2.Text = "-";
                    label3.Text = "-";

                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;

                    cb_classes.Text = null;
                    break;

                case 4:
                    // Habilata todos os itens
                    b_send.Enabled = true;

                    b_clear.Enabled = true;

                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;

                    cb_classes.Enabled = true;
                    break;

                case 5:
                    // Desabilita todos os itens
                    b_send.Enabled = false;

                    b_clear.Enabled = false;

                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;

                    cb_classes.Enabled = false;
                    break;
            }
        }

        // Função que estabelece o status da conexão
        private void Status_Function(object var)
        {
            string flag = (string)var;

            if (flag == "Connect")
            {
                try
                {
                    TcpClient Client = new TcpClient();
                    string flag1 = (string)Invoke(Get_Interface_Pointer, 1);
                    string flag2 = (string)Invoke(Get_Interface_Pointer, 2);

                    Client.Connect(IPAddress.Parse(flag1), Convert.ToInt16(flag2));
                    Stream = Client.GetStream();

                    // Conectado ao Servidor
                    Invoke(Refresh_Interface_Pointer, 1);
                    // Habilata todos os itens
                    Invoke(Refresh_Interface_Pointer, 4);
                }
                catch
                {
                    // Desconectado do Servidor
                    Invoke(Refresh_Interface_Pointer, 2);
                    // Limpa todos os campos 
                    Invoke(Refresh_Interface_Pointer, 3);
                    // Desabilita todos os itens
                    Invoke(Refresh_Interface_Pointer, 5);

                    MessageBox.Show("Falha ao Conectar");

                }
            }

            if (flag == "Disconnect")
            {
                // Desconectado do Servidor
                Invoke(Refresh_Interface_Pointer, 2);
                // Limpa todos os campos 
                Invoke(Refresh_Interface_Pointer, 3);
                // Desabilita todos os itens
                Invoke(Refresh_Interface_Pointer, 5);

                Stream.Close();
            }
        }

        private void b_connect_Click(object sender, EventArgs e)
        {
            string flag;

            // Criação da Thread secundária para processar a função Status_Function
            Thread Thread_Status = new Thread(new ParameterizedThreadStart(Status_Function));
            Thread_Status.Name = "Thread_Status";

            if (b_connect.Text == "Conectar")
            {
                flag = "Connect";
                Thread_Status.Start(flag);
            }
            else
            {
                flag = "Disconnect";
                Thread_Status.Start(flag);
                Stream.Close();
            }
        }

        private void cb_classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_classes.Text)
            {
                case "Carro":
                    label1.Text = "Modelo";
                    label2.Text = "Ano";
                    label3.Text = "Valor";
                    break;
                case "Pessoa":
                    label1.Text = "Nome";
                    label2.Text = "Sexo";
                    label3.Text = "CPF";
                    break;
                case "Conta Bancária":
                    label1.Text = "Titular";
                    label2.Text = "Agência";
                    label3.Text = "Conta";
                    break;
            }
        }

        private void b_clear_Click(object sender, EventArgs e)
        {
            //Clear_itens();
            Invoke(Refresh_Interface_Pointer, 3);
        }

        private void b_send_Click(object sender, EventArgs e)
        {
            NetworkStream Stream_Local = (NetworkStream)Stream;
            try
            {
                if (cb_classes.Text != "Carro" && cb_classes.Text != "Pessoa" && cb_classes.Text != "Conta Bancária")
                {
                    MessageBox.Show("Classe Inválida");
                }
                else
                {
                    if (Stream.CanRead == true)
                    {
                        switch (cb_classes.Text)
                        {
                            case "Carro":
                                Carro Carro = new Carro(Convert.ToString(textBox1.Text), Convert.ToInt16(textBox2.Text), Convert.ToDouble(textBox3.Text));
                                IFormatter formatter_Carro = new BinaryFormatter();
                                MemoryStream stream_Carro = new MemoryStream();
                                formatter_Carro.Serialize(stream_Carro, Carro);
                                Stream.Write(stream_Carro.ToArray(), 0, Convert.ToInt32(stream_Carro.Length));
                                stream_Carro.Close();
                                break;

                            case "Pessoa":
                                Pessoa Pessoa = new Pessoa(Convert.ToString(textBox1.Text), Convert.ToString(textBox2.Text), Convert.ToInt16(textBox3.Text));
                                IFormatter formatter_Pessoa = new BinaryFormatter();
                                MemoryStream stream_Pessoa = new MemoryStream();
                                formatter_Pessoa.Serialize(stream_Pessoa, Pessoa);
                                Stream.Write(stream_Pessoa.ToArray(), 0, Convert.ToInt32(stream_Pessoa.Length));
                                stream_Pessoa.Close();
                                break;

                            case "Conta Bancária":
                                Conta_Bancaria Conta_Bancaria = new Conta_Bancaria(Convert.ToString(textBox1.Text), Convert.ToInt16(textBox2.Text), Convert.ToInt16(textBox3.Text));
                                IFormatter formatter_Conta_Bancaria = new BinaryFormatter();
                                MemoryStream stream_Conta_Bancaria = new MemoryStream();
                                formatter_Conta_Bancaria.Serialize(stream_Conta_Bancaria, Conta_Bancaria);
                                Stream.Write(stream_Conta_Bancaria.ToArray(), 0, Convert.ToInt32(stream_Conta_Bancaria.Length));
                                stream_Conta_Bancaria.Close();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Falha ao Enviar");
                        // Desconectado do Servidor
                        Invoke(Refresh_Interface_Pointer, 2);
                        // Limpa todos os campos 
                        Invoke(Refresh_Interface_Pointer, 3);
                        // Desabilita todos os itens
                        Invoke(Refresh_Interface_Pointer, 5);

                        Stream.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Não foi possível enviar os dados");
            }
        }
    }
}
