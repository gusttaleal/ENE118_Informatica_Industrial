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
        public void escreveDados(object obj) // 708 end do acoplamento
        {
            int partida_modo = (int)obj;

            
            try
            {
                if (modbus.Connected)
                {
                    int[] dadosHR = modbus.ReadHoldingRegisters(708, 1); //Nesse caso verifica se o motor está acoplado e qual motor (0 = Motor desacoplado, 1= motor 1, 2= motor 2)
                    if (dadosHR[0] == 0)
                    {
                        bt_lig.Enabled = false;
                    }
                    else
                    {
                        if (dadosHR[0] == 1 || dadosHR[0] == 2)
                        {

                            switch (partida_modo)
                            {
                                case 1:
                                    modbus.WriteSingleRegister(1324, 1); // acionamento do motor (end do motor = 1312) (1=liga, 0=desliga, 2=reseta)
                                    partida_real = 1;
                                    bt_lig.Enabled = true;
                                    break;
                                case 2:
                                    modbus.WriteSingleRegister(1324, 2);
                                    partida_real = 2;
                                    bt_lig.Enabled = true;
                                    break;
                                case 3:
                                    modbus.WriteSingleRegister(1324, 3);
                                    partida_real = 3;
                                    bt_lig.Enabled = true;
                                    break;
                                case 4:
                                    switch (partida_real)
                                    {
                                        case 1:
                                            modbus.WriteSingleRegister(1316, 1); // Ligar com soft
                                            break;
                                        case 2:
                                            modbus.WriteSingleRegister(1312, 1); //Ligar com inversor 
                                            modbus.WriteSingleRegister(1314, 10); //Ligar com inversor 
                                            modbus.WriteSingleRegister(1313, 600); //Ligar com inversor 
                                            break;
                                        case 3:
                                            modbus.WriteSingleRegister(1319, 1); //Ligar com partida direta
                                            break;
                                    }
                                    break;
                                case 5:

                                    switch (partida_real)
                                    {
                                        case 1:
                                            modbus.WriteSingleRegister(1316, 0); //Desliga o Motor com Soft
                                            break;
                                        case 2:
                                            modbus.WriteSingleRegister(1312, 0); //Desliga o Motor com Inversor 
                                            modbus.WriteSingleRegister(1315, 10); //Desliga o Motor com Inversor 
                                            break;
                                        case 3:
                                            modbus.WriteSingleRegister(1319, 0); // Desliga o Motor com partida Direta 
                                            break;
                                    }
                                    break;
                                case 6:
                                    switch (partida_real)
                                    {
                                        case 1:
                                            modbus.WriteSingleRegister(1316, 2); //Reset  Motor com Soft
                                            break;
                                        case 2:
                                            modbus.WriteSingleRegister(1312, 2); //Reset  o Motor com Inversor 
                                            break;
                                        case 3:
                                            modbus.WriteSingleRegister(1319, 2); // Reset o Motor com partida Direta 
                                            break;
                                    }

                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("O motor está desconectado");
            }
        }
    }
}