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
        private void Refresh_Interface()
        {
            while (modbus.Connected)
            {
                try
                {
                    // INICIO - Verificação do acoplamento do motor:
                    {
                        int[] dadosHR_ENC = modbus.ReadHoldingRegisters(727, 2);
                        if (dadosHR_ENC[0] > 100)
                        {
                            // Motor Vermelho: Acoplado e Inativo
                            pb_motor_g.Invoke((MethodInvoker)(() => { pb_motor_g.Visible = true; }));
                            pb_motor_r.Invoke((MethodInvoker)(() => { pb_motor_r.Visible = false; }));
                            pb_motor_c.Invoke((MethodInvoker)(() => { pb_motor_c.Visible = false; }));
                        }
                        else
                        {
                            int[] dadosHR = modbus.ReadHoldingRegisters(708, 1);
                            if (dadosHR[0] == 0)
                            {
                                // Motor Cinza: Desacopaldo
                                pb_motor_c.Invoke((MethodInvoker)(() => { pb_motor_c.Visible = true; }));
                                pb_motor_r.Invoke((MethodInvoker)(() => { pb_motor_r.Visible = false; }));
                                pb_motor_g.Invoke((MethodInvoker)(() => { pb_motor_g.Visible = false; }));

                                // Motor Real Desacoplado
                                PB_motor_color_g.Invoke((MethodInvoker)(() => { PB_motor_color_g.Visible = false; }));
                                PB_motor_color_b.Invoke((MethodInvoker)(() => { PB_motor_color_b.Visible = false; }));
                            }
                            else if (dadosHR[0] == 1)
                            {
                                // Motor Vermelho: Acoplado e Inativo
                                pb_motor_r.Invoke((MethodInvoker)(() => { pb_motor_r.Visible = true; }));
                                pb_motor_g.Invoke((MethodInvoker)(() => { pb_motor_g.Visible = false; }));
                                pb_motor_c.Invoke((MethodInvoker)(() => { pb_motor_c.Visible = false; }));

                                // Motor Real Verde
                                PB_motor_color_g.Invoke((MethodInvoker)(() => { PB_motor_color_g.Visible = true; }));
                                PB_motor_color_b.Invoke((MethodInvoker)(() => { PB_motor_color_b.Visible = false; }));

                            }
                            else if (dadosHR[0] == 2)
                            {
                                // Motor Vermelho: Acoplado e Inativo
                                pb_motor_r.Invoke((MethodInvoker)(() => { pb_motor_r.Visible = true; }));
                                pb_motor_g.Invoke((MethodInvoker)(() => { pb_motor_g.Visible = false; }));
                                pb_motor_c.Invoke((MethodInvoker)(() => { pb_motor_c.Visible = false; }));

                                // Motor Real Azul
                                PB_motor_color_g.Invoke((MethodInvoker)(() => { PB_motor_color_b.Visible = true; }));
                                PB_motor_color_b.Invoke((MethodInvoker)(() => { PB_motor_color_g.Visible = false; }));
                            }
                        }
                    }
                    // FIM - Verificação do acoplamento do motor:

                    // INICIO - Leitura Niveis superiro e inferior dos tanques 
                    {
                        int[] dadosS1 = modbus.ReadHoldingRegisters(716, 1);
                        BitArray BA = new BitArray(dadosS1);

                        if (BA[0].ToString() == "True")
                        {
                            l_nv_sup_up.Invoke((MethodInvoker)(() => { l_nv_sup_up.BackColor = Color.Lime; }));
                        }
                        else
                        {
                            l_nv_sup_up.Invoke((MethodInvoker)(() => { l_nv_sup_up.BackColor = Color.Red; }));
                        }

                        if (BA[1].ToString() == "True")
                        {
                            l_nv_sup_dw.Invoke((MethodInvoker)(() => { l_nv_sup_dw.BackColor = Color.Lime; }));
                        }
                        else
                        {
                            l_nv_sup_dw.Invoke((MethodInvoker)(() => { l_nv_sup_dw.BackColor = Color.Red; }));
                        }

                        if (BA[2].ToString() == "True")
                        {
                            l_nv_inf_up.Invoke((MethodInvoker)(() => { l_nv_inf_up.BackColor = Color.Lime; }));
                        }
                        else
                        {
                            l_nv_inf_up.Invoke((MethodInvoker)(() => { l_nv_inf_up.BackColor = Color.Red; }));
                        }

                        if (BA[3].ToString() == "True")
                        {
                            l_nv_inf_dw.Invoke((MethodInvoker)(() => { l_nv_inf_dw.BackColor = Color.Lime; }));
                        }
                        else
                        {
                            l_nv_inf_dw.Invoke((MethodInvoker)(() => { l_nv_inf_dw.BackColor = Color.Red; }));
                        }
                    }
                    // FIM - Leitura Niveis superiro e inferior dos tanques  

                    // INICIO - Leitura da porcentagem dos tanques
                    {
                        PictureBox[] PB_UP = { pb_10_up, pb_20_up, pb_30_up, pb_40_up, pb_50_up, pb_60_up, pb_70_up, pb_80_up, pb_90_up, pb_100_up };
                        PictureBox[] PB_DW = { pb_100_down, pb_90_down, pb_80_down, pb_70_down, pb_60_down, pb_50_down, pb_40_down, pb_30_down, pb_20_down, pb_10_down };

                        int[] dados_nivel = modbus.ReadHoldingRegisters(714, 2);
                        float nivelTanqueA = ModbusClient.ConvertRegistersToFloat(dados_nivel, ModbusClient.RegisterOrder.LowHigh);
                        l_nv_tanque_up.Invoke((MethodInvoker)(() => { l_nv_tanque_up.Text = "Volume: " + Convert.ToString((Int16)nivelTanqueA) + "%"; }));
                        l_nv_tanque_dw.Invoke((MethodInvoker)(() => { l_nv_tanque_dw.Text = "Volume: " + Convert.ToString(100 - (Int16)nivelTanqueA) + "%"; }));

                        // Nivel tanque A: 100% / B:10%
                        if (nivelTanqueA > 90)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 9)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 80 && nivelTanqueA < 90)
                        {
                            // Nivel tanque A: 90% / B:20%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 8)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 70 && nivelTanqueA < 80)
                        {
                            // Nivel tanque A: 80% / B:30%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 7)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 60 && nivelTanqueA < 70)
                        {
                            // Nivel tanque A: 70% / B:40%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 6)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 50 && nivelTanqueA < 60)
                        {
                            // Nivel tanque A: 60% / B:50%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 5)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 40 && nivelTanqueA < 50)
                        {
                            // Nivel tanque A: 50% / B:60%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 4)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 30 && nivelTanqueA < 40)
                        {
                            // Nivel tanque A: 40% / B:70%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 3)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 20 && nivelTanqueA < 30)
                        {
                            // Nivel tanque A: 30% / B:80%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 2)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA > 10 && nivelTanqueA < 20)
                        {
                            // Nivel tanque A: 20% / B:90%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 1)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                        if (nivelTanqueA < 10)
                        {
                            // Nivel tanque A: 10% / B:100%
                            for (int i = 0; i < 10; i++)
                            {
                                if (i == 0)
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = true; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = true; }));
                                }
                                else
                                {
                                    PB_UP[i].Invoke((MethodInvoker)(() => { PB_UP[i].Visible = false; }));
                                    PB_DW[i].Invoke((MethodInvoker)(() => { PB_DW[i].Visible = false; }));
                                }
                            }
                        }
                    }
                    // FIM - Leitura da porcentagem dos tanques

                    // INICIO - Leitura temperatura das fases
                    {
                        // Fase R
                        int[] dados_tempR = modbus.ReadHoldingRegisters(700, 2);
                        float dadosTempR = ModbusClient.ConvertRegistersToFloat(dados_tempR, ModbusClient.RegisterOrder.LowHigh) / 10;
                        bt_lig.Invoke((MethodInvoker)(() => { lb_dadosTempR.Text = "Fase R: " + Convert.ToString((Int16)dadosTempR) + "°"; }));

                        // Fase S 
                        int[] dados_tempS = modbus.ReadHoldingRegisters(702, 2);
                        float dadosTempS = ModbusClient.ConvertRegistersToFloat(dados_tempS, ModbusClient.RegisterOrder.LowHigh) / 10;
                        bt_lig.Invoke((MethodInvoker)(() => { lb_dadosTempS.Text = "Fase S: " + Convert.ToString((Int16)dadosTempS) + "°"; }));

                        // Fase T 
                        int[] dados_tempT = modbus.ReadHoldingRegisters(704, 2);
                        float dadosTempT = ModbusClient.ConvertRegistersToFloat(dados_tempT, ModbusClient.RegisterOrder.LowHigh) / 10;
                        bt_lig.Invoke((MethodInvoker)(() => { lb_dadosTempT.Text = "Fase T: " + Convert.ToString((Int16)dadosTempT) + "°"; }));
                    }
                    // FIM - Leitura temperatura das fases

                    // INICIO - Grava Dados
                    {
                        int[] dados_nivel = modbus.ReadHoldingRegisters(714, 2);
                        float nivelTanqueA = ModbusClient.ConvertRegistersToFloat(dados_nivel, ModbusClient.RegisterOrder.LowHigh);

                        int[] nivelSA = modbus.ReadHoldingRegisters(716, 1);
                        BitArray BA = new BitArray(nivelSA);

                        int[] dados_tempR = modbus.ReadHoldingRegisters(700, 2);
                        float dadosTempR = ModbusClient.ConvertRegistersToFloat(dados_tempR, ModbusClient.RegisterOrder.LowHigh) / 10;
                        int[] dados_tempS = modbus.ReadHoldingRegisters(702, 2);
                        float dadosTempS = ModbusClient.ConvertRegistersToFloat(dados_tempS, ModbusClient.RegisterOrder.LowHigh) / 10;
                        int[] dados_tempT = modbus.ReadHoldingRegisters(704, 2);
                        float dadosTempT = ModbusClient.ConvertRegistersToFloat(dados_tempT, ModbusClient.RegisterOrder.LowHigh) / 10;

                        StreamWriter sw = new StreamWriter("DataOUT.txt", true); // Modo append: adiciona dados no txt sem apagar os anteriores
                        sw.WriteLine("True para atingido e False para não atingido \r\n Nível superior:  " + BA[0].ToString() + "\r\n Nível inferior:  " + BA[2].ToString()
                            + "\r\n Nível do tanque: " + nivelTanqueA.ToString() + "%"
                            + "\r\n Temperatura da fase R: " + dadosTempR.ToString()
                            + "\r\n Temperatura da fase S: " + dadosTempS.ToString()
                            + "\r\n Temperatura da fase T: " + dadosTempT.ToString());
                        sw.Close();
                    }
                    // FIM - Grava Dados
                }
                catch
                {
                    //MessageBox.Show("Sistema Desconectado");
                }

                Thread.Sleep(100);
            }
        }
    }
}