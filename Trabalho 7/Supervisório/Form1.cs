/*
UNIVERSIDADE FEDERAL DE JUIZ DE FORA - FACULDADE DE ENGENHARIA
GUSTAVO LEAL SILVA E SOUZA - 201469055B
INFORMÁTICA INDUSTRIAL
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Supervisorio
{
    public partial class Form1 : Form
    {
        public Vazao vz;

        public Form1()
        {
            InitializeComponent();
            vz = new Vazao(0);
        }
        
        public void Enable()
        {
            try
            {
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("Erro ao iniciar a conexão");
                return;
            }
            this.sm_connect.Text = "Desconectar";
            this.l_index.BackColor = Color.Lime;
            this.ss_index.Text = "Conectado";
            this.timer1.Enabled = true;
        }

        public void Disable()
        {
            try
            {
                this.serialPort1.WriteLine("EOT");
                this.serialPort1.Close();
            }
            catch
            {
                MessageBox.Show("Falha ao desligar o sensor");
            }
            this.timer1.Enabled = false;
            this.sm_connect.Text = "Conectar";
            this.l_index.BackColor = Color.Red;
            this.ss_index.Text = "Desconectado";
            this.l_screen.Text = "00,00";
        }

        private void configuraçõesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 Config = new Form2(this.serialPort1);
            Config.Show();
        }

        private void sm_connect_Click(object sender, EventArgs e)
        {
            if (this.sm_connect.Text == "Conectar")
            {
                this.Enable();
            }
            else
            {
                this.Disable();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.vz.calculaVazao();
            this.vz.contador = (this.vz.contador + 1) % 10;
            this.l_screen.Text = this.vz.vazao.ToString();
            try
            {
                serialPort1.WriteLine(this.vz.vazao.ToString());
            }
            catch
            {
                MessageBox.Show("Falha ao enviar dados");
                this.Disable();
            }
        }
    }
}
