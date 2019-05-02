/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CLASSES;

namespace SERVIDOR
{
    public partial class Server_Main : Form
    {
        // Instanciando um Objeto Server_Client_Global da Classe TcpListener
        TcpListener Server_Client_Global = new TcpListener(IPAddress.Any, 9900);

        // Instância dos Delegates
        private Refresh_Interface_Delegate Refresh_Interface_Pointer;

        // Declaração dos Delegates
        private delegate void Refresh_Interface_Delegate(int i, string s1, string s2, string s3);

        public Server_Main()
        {
            //Iniciando os Componentes
            InitializeComponent();

            // Nomeando a Thread Principal
            Thread.CurrentThread.Name = "Thread_Main";

            // Inicializa a interface com as text box desabilitadas
            Refresh_Interface_Function(4, " ", " ", " ");
            Thread.Sleep(100);
            Refresh_Interface_Function(8, " ", " ", " ");

            // Atribuição do Delegate
            Refresh_Interface_Pointer = new Refresh_Interface_Delegate(Refresh_Interface_Function);
        }

        private void b_connect_Click(object sender, EventArgs e)
        {
            // Sinalizador para interromper while da função Server
            bool flag = true;

            // Declaração da Thread do Servidor
            Thread Thread_Server = new Thread(new ParameterizedThreadStart(Server_Function));
            Thread_Server.Name = "Thread_Server";

            if (b_connect.Text == "Conectar")
            {
                Server_Client_Global.Start();
                Thread_Server.Start(new Thread_Parameter_Class(flag, Server_Client_Global));
            }
            else
            {
                flag = false;
                Server_Client_Global.Stop();
                Thread_Server.Start(new Thread_Parameter_Class(flag, Server_Client_Global));
                Thread.Sleep(100);
                Thread_Server.Abort();
            }
        }

        // Função do Servidor
        private void Server_Function(object var2)
        {
            // Tradução do Objeto recebido pela Thread principal via b_connect
            Thread_Parameter_Class Thread_Parameter = (Thread_Parameter_Class)var2;
            bool flag = Thread_Parameter.Parameter1;
            TcpListener Server_Client_Local = Thread_Parameter.Parameter2;

            // Instanciando um Objeto Client da Classe TcpClient - Que permitirá a conexão com um Cliente
            TcpClient Client_Server;

            // Declaração da Thread de Tratamento de Dados
            Thread Thread_Data_Treatment = new Thread(new ParameterizedThreadStart(Data_Treatment_Function));
            Thread_Data_Treatment.Name = "Thread_Data_Treatment";

            while (flag)
            {
                try
                {
                    // Aguardando Cliente
                    Invoke(Refresh_Interface_Pointer, 1, " ", " ", " ");
                    Client_Server = Server_Client_Local.AcceptTcpClient();

                    // Pausa na Thread por 1/2 segundo
                    Thread.Sleep(500);

                    // Cliente Conectado
                    Thread_Data_Treatment.Start(Client_Server);
                }
                catch
                {
                    // Cliente Desconectado
                    Invoke(Refresh_Interface_Pointer, 4, " ", " ", " ");
                    Thread.Sleep(100);
                    Invoke(Refresh_Interface_Pointer, 8, " ", " ", " ");
                    flag = false;
                }
            }

            // Cliente Desconectado
            Invoke(Refresh_Interface_Pointer, 4, " ", " ", " ");

            Thread.Sleep(100);
            // Atualiza Interface (Blank)
            Invoke(Refresh_Interface_Pointer, 8, " ", " ", " ");

            Thread_Data_Treatment.Abort();
        }

        // Função de Tratamento de Dados
        private void Data_Treatment_Function(object var)
        {
            bool flag = true;

            // Atribuição do Objeto "var" ao Objeto Client_Treatment
            TcpClient Client_Treatment = (TcpClient)var;

            // Instanciando um Objeto Stream da Classe NetworkStream - Abrindo o Fluxo de Dados
            NetworkStream Stream;

            

            Stream = Client_Treatment.GetStream();

            // Cliente Conectado
            Invoke(Refresh_Interface_Pointer, 2, " ", " ", " ");

            // Pausa na Thread por um segundo
            Thread.Sleep(500);

            // Tratando os dados
            Invoke(Refresh_Interface_Pointer, 3, " ", " ", " ");

            while (flag)
            {
                // Variáveis para Recepção/Envio dos Dados
                byte[] Message_in = new byte[512];

                IFormatter formatter = new BinaryFormatter();
                Stream.Read(Message_in, 0, Message_in.Length);
                MemoryStream stream_ms = new MemoryStream(Message_in);

                try
                {
                    stream_ms.Position = 0;
                    Carro Carro = (Carro)formatter.Deserialize(stream_ms);
                    stream_ms.Close();
                    Invoke(Refresh_Interface_Pointer, 5, Convert.ToString(Carro.Modelo), Convert.ToString(Carro.Ano), Convert.ToString(Carro.Valor));
                }
                catch
                {
                    try
                    {
                        stream_ms.Position = 0;
                        Pessoa Pessoa = (Pessoa)formatter.Deserialize(stream_ms);
                        stream_ms.Close();
                        Invoke(Refresh_Interface_Pointer, 6, Convert.ToString(Pessoa.Nome), Convert.ToString(Pessoa.Sexo), Convert.ToString(Pessoa.CPF));
                    }
                    catch
                    {
                        try
                        {
                            stream_ms.Position = 0;
                            Conta_Bancaria Conta_Bancaria = (Conta_Bancaria)formatter.Deserialize(stream_ms);
                            stream_ms.Close();
                            Invoke(Refresh_Interface_Pointer, 7, Convert.ToString(Conta_Bancaria.Titular), Convert.ToString(Conta_Bancaria.Agencia), Convert.ToString(Conta_Bancaria.Conta));
                        }
                        catch
                        {
                            //MessageBox.Show("Deserialização mal sucedida");
                            Invoke(Refresh_Interface_Pointer, 4, " ", " ", " ");
                            Thread.Sleep(100);
                            Invoke(Refresh_Interface_Pointer, 8, " ", " ", " ");
                            Client_Treatment.Close();
                            Server_Client_Global.Stop();
                            flag = false;
                        }
                    }
                }
            }
        }

        // Função ligada a classe Thread_Parameter_Class
        public static void Thread_Parameter_Function(object obj)
        {
            Thread_Parameter_Class Parameter = (Thread_Parameter_Class)obj;
            bool p1 = Parameter.Parameter1;
            TcpListener p2 = Parameter.Parameter2;
        }

        // Função para Atualizar a Interface Gráfica
        private void Refresh_Interface_Function(int var, string label1, string label2, string label3)
        {
            switch (var)
            {
                case 1:
                    // Aguardando Cliente
                    b_connect.Text = "Desconectar";
                    bs_status.Text = "Aguardando Cliente";
                    l_index.BackColor = Color.Lime;
                    break;

                case 2:
                    // Cliente Conectado
                    b_connect.Text = "Desconectar";
                    bs_status.Text = "Cliente Conectado";
                    l_index.BackColor = Color.Lime;
                    break;

                case 3:
                    // Tratando Dados
                    b_connect.Text = "Desconectar";
                    bs_status.Text = "Tratando Dados";
                    l_index.BackColor = Color.Gold;
                    break;

                case 4:
                    // Cliente Desconectado
                    b_connect.Text = "Conectar";
                    bs_status.Text = "Desconectado";
                    l_index.BackColor = Color.Red;
                    break;

                case 5:
                    l_class.Text = "Classe";
                    tb_class.Text = "Carro";

                    l_index1.Text = "Modelo";
                    tb_index1.Text = label1;

                    l_index2.Text = "Ano";
                    tb_index2.Text = label2;

                    l_index3.Text = "Valor";
                    tb_index3.Text = label3;
                    break;

                case 6:
                    l_class.Text = "Classe";
                    tb_class.Text = "Pessoa";

                    l_index1.Text = "Nome";
                    tb_index1.Text = label1;

                    l_index2.Text = "Sexo";
                    tb_index2.Text = label2;

                    l_index3.Text = "CPF";
                    tb_index3.Text = label3;
                    break;

                case 7:
                    l_class.Text = "Classe";
                    tb_class.Text = "Conta Bancária";

                    l_index1.Text = "Titular";
                    tb_index1.Text = label1;

                    l_index2.Text = "Agência";
                    tb_index2.Text = label2;

                    l_index3.Text = "Conta";
                    tb_index3.Text = label3;
                    break;

                case 8:
                    l_class.Text = "-";
                    tb_class.Text = null;
                    tb_class.Enabled = false;

                    l_index1.Text = "-";
                    tb_index1.Text = null;
                    tb_index1.Enabled = false;

                    l_index2.Text = "-";
                    tb_index2.Text = null;
                    tb_index2.Enabled = false;

                    l_index3.Text = "-";
                    tb_index3.Text = null;
                    tb_index3.Enabled = false;
                    break;
            }
        }
    }

    // Classe utilizada como artifício para que o argumento da Thread aceite 2 objetos de classes diferentes
    public class Thread_Parameter_Class
    {
        public bool Parameter1 { get; set; }
        public TcpListener Parameter2 { get; set; }


        public Thread_Parameter_Class(bool Parameter1, TcpListener Parameter2)
        {
            this.Parameter1 = Parameter1;
            this.Parameter2 = Parameter2;
        }
    }
}