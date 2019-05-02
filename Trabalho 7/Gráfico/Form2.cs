/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace Grafico
{
    public partial class Form2 : Form
    {
        public Form2(Form1 F1, SerialPort S1)
        {
            InitializeComponent();
            this.Inicializacao();
            this.Sp1 = S1;
            this.Form1 = F1;
        }

        public SerialPort Sp1 { get; set; }
        public Form1 Form1;

        public void Inicializacao()
        {
            string[] baudrate = { "9600", "115200" };

            Parity[] paridades = { Parity.Even, Parity.Odd, Parity.None };

            int[] databits = { 7, 8 };

            StopBits[] sb = { StopBits.One, StopBits.OnePointFive, StopBits.Two };

            cb_port.Items.AddRange(SerialPort.GetPortNames());
            cb_brate.Items.AddRange(baudrate);

            foreach (int db in databits)
            {
                cb_databits.Items.Add(db.ToString());
            }

            foreach (StopBits S in sb)
            {
                cb_sbit.Items.Add(S.ToString());
            }

            foreach (Parity P in paridades)
            {
                cb_parity.Items.Add(P.ToString());
            }
        }

        public void UpdateSerial(SerialPort SP)
        {
            try
            {
                SP.PortName = cb_port.SelectedItem.ToString();
                SP.BaudRate = Convert.ToInt32(cb_brate.SelectedItem);
                SP.DataBits = Convert.ToInt32((cb_databits.SelectedItem.ToString()));
                SP.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cb_sbit.SelectedItem.ToString());
                SP.Parity = (Parity)Enum.Parse(typeof(Parity), cb_parity.SelectedItem.ToString());

                SP.DataReceived += new SerialDataReceivedEventHandler(Form1.Data_Treatment_Funtion);

                MessageBox.Show("Porta serial configurada com sucesso!");
            }
            catch
            {
                MessageBox.Show("Preencha todos os campos");
            }
        }

        private void b_config_Click(object sender, EventArgs e)
        {
            UpdateSerial(this.Sp1);
        }
    }
}
