using EGlobalT.Device.SmartCard;
using GS.Util.Hex;
using MC.BusinessObjects.Entities;
using MC.BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MC.CRT_570Device
{
    public class CRT570DeviceClass
    {
        UInt32 Hdle = 0;

        public EventHandler DeviceMessageDispenserState;
        StatesDispenser _StatesDispenser = new StatesDispenser();

        public ResultadoOperacion ConectarDispensador(string sPuerto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                Hdle = DllClass.CommOpen(sPuerto);
                if (Hdle != 0)
                {
                    _StatesDispenser = StatesDispenser.ConexionExitosa_Dispensador;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Conexion Exitosa Lector";
                }
                else
                {
                    _StatesDispenser = StatesDispenser.ErrorConexion_Dispensador;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error de conexion";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion DesconectarDispensador()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {
                    int i = DllClass.CommClose(Hdle);
                    Hdle = 0;
                    _StatesDispenser = StatesDispenser.Desconexion_Exitosa;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Desconexion Exitosa Lector";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion AlistarTarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {

                    int i = DllClass.CRT570_PreDispense(Hdle);
                    if (i == 0)
                    {
                        //MessageBox.Show("Run OK", "Run");
                        Thread.Sleep(500);
                        if (Hdle != 0)
                        {
                            Thread.Sleep(500);
                            int a = DllClass.RF_DetectCard(Hdle);
                            if (a == 0)
                            {
                                //MessageBox.Show("Test RF Card OK", "Test RF Card");

                                if (Hdle != 0)
                                {
                                    Thread.Sleep(500);
                                    byte[] _CardID = new byte[300];
                                    byte _CardIDLen = 0;
                                    int b = -1;
                                    b = DllClass.RF_GetCardID(Hdle, ref _CardIDLen, _CardID);

                                    if (b == 0)
                                    {
                                        int n;
                                        string StrBuf = "";

                                        for (n = 0; n < 4; n++)
                                        {
                                            StrBuf += _CardID[n].ToString("X2");
                                        }
                                        
                                        //RfSNlABEL.Text = StrBuf;

                                        //MessageBox.Show("Card S/N OK", "Card S/N");
                                        _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                        oResultadoOperacion.EntidadDatos = StrBuf;
                                        oResultadoOperacion.Mensaje = "Card S/N OK";
                                    }
                                    else if (b == 69)
                                    {
                                        //MessageBox.Show("No Card In", "Caution");
                                        _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                        oResultadoOperacion.Mensaje = "Comm. port is not Opened";
                                    }
                                    else if (b == 87)
                                    {
                                        //MessageBox.Show("The card is not on the card operation position", "Caution");
                                        _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                        oResultadoOperacion.Mensaje = "Comm. port is not Opened";
                                    }
                                    else if (b == 78)
                                    {
                                        //MessageBox.Show("Excute Command Error", "Caution");
                                        _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                        oResultadoOperacion.Mensaje = "Comm. port is not Opened";
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                                        _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                        oResultadoOperacion.Mensaje = "Comm. port is not Opened";
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Comm. port is not Opened", "Caution");
                                    _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                    oResultadoOperacion.Mensaje = "Comm. port is not Opened";
                                }
                            }
                            else if (a == 69)
                            {
                                //MessageBox.Show("No Card In", "Caution");
                                _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                oResultadoOperacion.Mensaje = "No Card In";
                            }
                            else if (a == 87)
                            {
                                //MessageBox.Show("The card is not on the card operation position", "Caution");
                                _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                oResultadoOperacion.Mensaje = "The card is not on the card operation position";
                            }
                            else if (a == 78)
                            {
                                //MessageBox.Show("Excute Command Error", "Caution");
                                _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                oResultadoOperacion.Mensaje = "Excute Command Error";
                            }
                            else
                            {
                                //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                                _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                oResultadoOperacion.Mensaje = "Error de conexion";
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Comm. port is not Opened", "Caution");
                            _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error de conexion";
                        }

                    }
                    else
                    {
                        _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = "Error de conexion";
                    }
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_AlistarTarjeta;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error de conexion";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion DispensaTarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {

                    int i = DllClass.CRT570_Dispense(Hdle);
                    if (i == 0)
                    {
                        //MessageBox.Show("Run OK", "Run");
                        _StatesDispenser = StatesDispenser.Tarjeta_MovidaReceptor_RF_TO_FRONT;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta_MovidaReceptor_RF_TO_FRONT";
                    }
                    else
                    {
                        _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_RF_TO_FRONT;
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_RF_TO_FRONT";
                    }
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_RF_TO_FRONT;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_RF_TO_FRONT";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion CapturarTarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {

                    int i = DllClass.CRT570_Capture(Hdle);
                    if (i == 0)
                    {
                        //MessageBox.Show("Run OK", "Run");
                        _StatesDispenser = StatesDispenser.Tarjeta_Movida_TOBINBOX;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta_Movida_TOBINBOX";
                    }
                    else
                    {
                        _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_TO_BINBOX;
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_TO_BINBOX";
                    }
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_TO_BINBOX;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_TO_BINBOX";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion EstadoStacker()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {

                Thread.Sleep(500);

                if (Hdle != 0)
                {
                    byte PSS1, PSS2, PSS3, PSS4;
                    PSS1 = 0;
                    PSS2 = 0;
                    PSS3 = 0;
                    PSS4 = 0;
                    string Sb1, Sb2, Sb3, Sb4;
                    Sb1 = "";
                    Sb2 = "";
                    Sb3 = "";
                    Sb4 = "";

                    int i = DllClass.CRT570_GetAP(Hdle, ref PSS1, ref PSS2, ref PSS3, ref PSS4);
                    if (i == 0)
                    {
                        switch (PSS1)
                        {
                            case 0x0:
                                Sb1 = "0x00";
                                _StatesDispenser = StatesDispenser.Estado_StackerGood;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Estado_Stacker ok";
                                break;
                            case 0x1:
                                Sb1 = "Error Card Bin Is Full:0x1";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x2:
                                Sb1 = "Dispenser Error Alarm:0x2";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x4:
                                Sb1 = "0x04";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x8:
                                Sb1 = "0x08";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                        }
                        switch (PSS2)
                        {
                            case 0x0:
                                Sb2 = " 0x0";
                                _StatesDispenser = StatesDispenser.Estado_StackerGood;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Estado_Stacker OK";
                                break;
                            case 0x1:
                                Sb2 = "Card Capture Error:0x1";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x2:
                                Sb2 = "Card Dispense Error:0x2";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x4:
                                Sb2 = "Card Is Capturing:0x4";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x8:
                                Sb2 = "Card Is Dispensing:0x8";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                        }
                        switch (PSS3)
                        {
                            case 0x0:
                                Sb3 = "0x0";
                                _StatesDispenser = StatesDispenser.Estado_StackerGood;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Estado_Stacker ok";
                                break;
                            case 0x1:
                                Sb3 = "Stacker Is Pre-empty:0x1";
                                _StatesDispenser = StatesDispenser.Estado_StackerNivelBajo;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerNivelBajo";
                                break;
                            case 0x2:
                                Sb3 = "Card Dispenser Clip Card:0x2";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x3:
                                Sb3 = "Card Dispenser Clip Card:0x3";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x4:
                                Sb3 = "Card Overlapped:0x4";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x8:
                                Sb3 = "0x8";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                        }

                        switch (PSS4)
                        {
                            case 0x0:
                                Sb4 = "0x0";
                                _StatesDispenser = StatesDispenser.Sin_Tarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x1:
                                Sb4 = "Card Stopped at the Door:0x01";
                                _StatesDispenser = StatesDispenser.Tarjeta_En_Boca;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta_En_Boca";
                                break;
                            case 0x2:
                                Sb4 = "0x2";
                                _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                                break;
                            case 0x3:
                                Sb4 = "Card Stopped at the Reading Position:0x3";
                                _StatesDispenser = StatesDispenser.Tarjeta_En_RF;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Tarjeta_En_RF";
                                break;
                            case 0x4:
                                Sb4 = "Waiting for Dispensing:0x4";
                                _StatesDispenser = StatesDispenser.Estado_StackerGood;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Estado_Stacker ok";
                                break;
                            case 0x6:
                                break;
                            case 0x8:
                                Sb4 = "Stacker Is Empty::0x8";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                            case 0x9:
                                Sb4 = "Stacker Is Empty::0x9";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                            case 0xa:
                                Sb4 = "Stacker Is Empty::0xa";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                            case 0xb:
                                Sb4 = "Stacker Is Empty:0xb";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                            case 0xc:
                                Sb4 = "Stacker Is Empty:0xc";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                            case 0xe:
                                Sb4 = "Stacker Is Empty::0xe";
                                _StatesDispenser = StatesDispenser.Estado_StackerSinTarjetas;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "StackerSinTarjetas";
                                break;
                        }

                    }
                    else
                    {
                        //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                        _StatesDispenser = StatesDispenser.Error_Estado_Stacker;
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = "Error_Estado_Stacker";
                    }
                }
                else
                {
                    //MessageBox.Show("Comm. port is not Opened", "Caution");
                    _StatesDispenser = StatesDispenser.Error_Estado_Stacker;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error_Estado_Stacker";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion Leertarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {

                    int i = DllClass.RF_DetectCard(Hdle);
                    if (i == 0)
                    {
                        //MessageBox.Show("Test RF Card OK", "Test RF Card");

                    }
                    else if (i == 69)
                    {
                        //MessageBox.Show("No Card In", "Caution");
                        _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                    }
                    else if (i == 87)
                    {
                        //MessageBox.Show("The card is not on the card operation position", "Caution");
                        _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                    }
                    else if (i == 78)
                    {
                        //MessageBox.Show("Excute Command Error", "Caution");
                        _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                    }
                    else
                    {
                        //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                        _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                    }
                }
                else
                {
                    //MessageBox.Show("Comm. port is not Opened", "Caution");
                    _StatesDispenser = StatesDispenser.Tarjeta_Alistada;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Tarjeta Alistada ok";
                }
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion Limpiartarjeta(Tarjeta oTarjeta, string sKey)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {
                    Thread.Sleep(500);
                    byte[] PassWordData = new byte[6];
                    string PasswordStr = sKey;

                    byte RfType = 0;
                    byte KEYLen = 6;
                    //for (int n = 0; n < 6; n++) PassWordData[n] = 0xff;
                    PassWordData[0] = (byte)Convert.ToInt32(PasswordStr.Substring(0, 2), 16);
                    PassWordData[1] = (byte)Convert.ToInt32(PasswordStr.Substring(3, 2), 16);
                    PassWordData[2] = (byte)Convert.ToInt32(PasswordStr.Substring(6, 2), 16);
                    PassWordData[3] = (byte)Convert.ToInt32(PasswordStr.Substring(9, 2), 16);
                    PassWordData[4] = (byte)Convert.ToInt32(PasswordStr.Substring(12, 2), 16);
                    PassWordData[5] = (byte)Convert.ToInt32(PasswordStr.Substring(15, 2), 16);
                    RfType = 0;

                    int i = DllClass.RF_LoadSecKey(Hdle, 1, RfType, KEYLen, PassWordData);

                    //if (i == 0)
                    //{
                        string write = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

                        byte[] BlockData = new byte[16];
                        string BlockDataStr = write;
                        byte BlockDataLen = 16;
                        byte SecNo = 1;
                        byte BlockNo = 0;

                        for (int n = 0; n < 16; n++)
                        {
                            BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                        }

                        int b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                        Thread.Sleep(500);

                        if (b == 0)
                        {

                            write = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

                            BlockData = new byte[16];
                            BlockDataStr = write;
                            BlockDataLen = 16;
                            SecNo = 1;
                            BlockNo = 1;

                            for (int n = 0; n < 16; n++)
                            {
                                BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                            }

                            b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                            Thread.Sleep(500);
                            if (b == 0)
                            {
                                write = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

                                BlockData = new byte[16];
                                BlockDataStr = write;
                                BlockDataLen = 16;
                                SecNo = 1;
                                BlockNo = 2;

                                for (int n = 0; n < 16; n++)
                                {
                                    BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                                }

                                b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                                Thread.Sleep(500);
                                if (b == 0)
                                {
                                    _StatesDispenser = StatesDispenser.Tarjeta_Borrada;
                                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                    oResultadoOperacion.Mensaje = "Write Data OK";
                                    //MessageBox.Show("Write Data OK", "Write Data");
                                }
                                else 
                                {
                                    _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                    oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                                }
                            }
                            else
                            {
                                _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                                oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            }
                        }
                        else if (b == 0x30)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("No RF Card In", "Write Data");
                        }
                        else if (b == 0x31)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Sector Error", "Write Data");
                        }
                        else if (b == 0x32)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("S/N Error", "Write Data");
                        }
                        else if (b == 0x33)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Password Error", "Write Data");
                        }
                        else if (b == 0x34)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Block Error", "Write Data");
                        }
                        else if (b == 0x35)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Value overflow", "Write Data");
                        }
                        else if (b == 69)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("No Card In", "Caution");
                        }
                        else if (b == 87)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("The card is not on the card operation position", "Caution");
                        }
                        else if (b == 78)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Excute Command Error", "Caution");
                        }
                        else if (b == -1)
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                        }
                        else
                        {
                            _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                            oResultadoOperacion.oEstado = TipoRespuesta.Error;
                            oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                            //MessageBox.Show("unknow Error", "Caution");
                        }
                    //}
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                }

            }
            catch (Exception ex)
            {
                _StatesDispenser = StatesDispenser.Error_Borrar_Tarjeta;
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error_Escribir_Tarjeta";
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion Escribirtarjeta(Tarjeta oTarjeta, string sKey)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {
                    Thread.Sleep(800);
                    byte[] PassWordData = new byte[6];
                    string PasswordStr = sKey;

                    byte RfType = 0;
                    byte KEYLen = 6;
                    //for (int n = 0; n < 6; n++) PassWordData[n] = 0xff;
                    PassWordData[0] = (byte)Convert.ToInt32(PasswordStr.Substring(0, 2), 16);
                    PassWordData[1] = (byte)Convert.ToInt32(PasswordStr.Substring(3, 2), 16);
                    PassWordData[2] = (byte)Convert.ToInt32(PasswordStr.Substring(6, 2), 16);
                    PassWordData[3] = (byte)Convert.ToInt32(PasswordStr.Substring(9, 2), 16);
                    PassWordData[4] = (byte)Convert.ToInt32(PasswordStr.Substring(12, 2), 16);
                    PassWordData[5] = (byte)Convert.ToInt32(PasswordStr.Substring(15, 2), 16);
                    RfType = 0;

                    int i = DllClass.RF_LoadSecKey(Hdle, 1, RfType, KEYLen, PassWordData);

                    //if (i == 0)
                    //{
                        string año = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("yy");
                        string mes = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("MM");
                        string dia = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("dd");
                        string hora = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("HH");
                        string minuto = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("mm");
                        string segundo = Convert.ToDateTime(oTarjeta.DateTimeEntrance).ToString("ss");

                        long decValue1 = Convert.ToInt64(año);
                        string hexValue1 = decValue1.ToString("X");
                        if (hexValue1.Length < 2)
                        {
                            hexValue1 = hexValue1.PadLeft(2, '0');
                        }
                        long decValue2 = Convert.ToInt64(mes);
                        string hexValue2 = decValue2.ToString("X");
                        if (hexValue2.Length < 2)
                        {
                            hexValue2 = hexValue2.PadLeft(2, '0');
                        }
                        long decValue3 = Convert.ToInt64(dia);
                        string hexValue3 = decValue3.ToString("X");
                        if (hexValue3.Length < 2)
                        {
                            hexValue3 = hexValue3.PadLeft(2, '0');
                        }
                        long decValue4 = Convert.ToInt64(hora);
                        string hexValue4 = decValue4.ToString("X");
                        if (hexValue4.Length < 2)
                        {
                            hexValue4 = hexValue4.PadLeft(2, '0');
                        }
                        long decValue5 = Convert.ToInt64(minuto);
                        string hexValue5 = decValue5.ToString("X");
                        if (hexValue5.Length < 2)
                        {
                            hexValue5 = hexValue5.PadLeft(2, '0');
                        }
                        long decValue6 = Convert.ToInt64(segundo);
                        string hexValue6 = decValue6.ToString("X");
                        if (hexValue6.Length < 2)
                        {
                            hexValue6 = hexValue6.PadLeft(2, '0');
                        }


                        byte[] ModuloEntrada = Encoding.ASCII.GetBytes(oTarjeta.EntranceModule);
                        string ModEnt = HexFormatting.ToHexString(ModuloEntrada, true);
                        string fechaEntrada = hexValue1 + " " + hexValue2 + " " + hexValue3 + " " + hexValue4 + " " + hexValue5 + " " + hexValue6;

                        string write = "01" + " 01 " + fechaEntrada + " " + ModEnt + "20 20";

                        byte[] BlockData = new byte[16];
                        string BlockDataStr = write;
                        byte BlockDataLen = 16;
                        byte SecNo = 1;
                        byte BlockNo = 0;

                        for (int n = 0; n < 16; n++)
                        {
                            BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                        }

                        int b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                        Thread.Sleep(500);

                        if (b == 0)
                        {
                            string tipo = string.Empty;

                            if (oTarjeta.TypeVehicle == EGlobalT.Device.SmartCard.TYPEVEHICLE_TARJETAPARKING_V1.AUTOMOBILE)
                            {
                                tipo = "01";
                            }
                            else
                            {
                                tipo = "02";
                            }

                            write = "00 00 00 00 00 00 00 00 " + tipo + " 00 00 00 00 00 00 00";

                            //byte[] BlockData = new byte[16];
                            BlockDataStr = write;
                            //byte BlockDataLen = 16;
                            SecNo = 1;
                            BlockNo = 1;

                            for (int n = 0; n < 16; n++)
                            {
                                BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                            }

                            b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                            Thread.Sleep(500);

                            if (b == 0)
                            {

                                write = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

                                //byte[] BlockData = new byte[16];
                                BlockDataStr = write;
                                //byte BlockDataLen = 16;
                                SecNo = 1;
                                BlockNo = 2;

                                for (int n = 0; n < 16; n++)
                                {
                                    BlockData[n] = (byte)Convert.ToInt32(BlockDataStr.Substring(n * 3, 2), 16);
                                }

                                
                                b = DllClass.RF_WriteBlock(Hdle, SecNo, BlockNo, BlockDataLen, BlockData);

                                if (b == 0)
                                {

                                    _StatesDispenser = StatesDispenser.Tarjeta_Escrita;
                                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                    oResultadoOperacion.Mensaje = "Write Data OK";
                                    //MessageBox.Show("Write Data OK", "Write Data");
                                }
                                else 
                                {
                                    _StatesDispenser = StatesDispenser.Error_Escribir_Tarjeta;
                                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                    oResultadoOperacion.Mensaje = "Communication Error";
                                }
                            }
                            else
                            {
                                _StatesDispenser = StatesDispenser.Error_Escribir_Tarjeta;
                                oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                                oResultadoOperacion.Mensaje = "Communication Error";
                            }

                        }
                        else if (b == 0x30)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "No RF Card In";
                            //MessageBox.Show("No RF Card In", "Write Data");
                        }
                        else if (b == 0x31)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Sector Error";
                            //MessageBox.Show("Sector Error", "Write Data");
                        }
                        else if (b == 0x32)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "S/N Error";
                            //MessageBox.Show("S/N Error", "Write Data");
                        }
                        else if (b == 0x33)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Password Error";
                            //MessageBox.Show("Password Error", "Write Data");
                        }
                        else if (b == 0x34)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Block Error";
                            //MessageBox.Show("Block Error", "Write Data");
                        }
                        else if (b == 0x35)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Value overflow";
                            //MessageBox.Show("Value overflow", "Write Data");
                        }
                        else if (b == 69)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "No Card In";
                            //MessageBox.Show("No Card In", "Caution");
                        }
                        else if (b == 87)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "The card is not on the card operation position";
                            //MessageBox.Show("The card is not on the card operation position", "Caution");
                        }
                        else if (b == 78)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Excute Command Error";
                            //MessageBox.Show("Excute Command Error", "Caution");
                            //CapturarTarjeta();
                        }
                        else if (b == -1)
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Communication Error";
                            //MessageBox.Show("Communication Error\r\nPossible causes:\r\n1>Communication setup error\r\n2>Wrong model selected\r\n3>No connected\r\n4>No power on unit", "Caution");
                        }
                        else
                        {
                            //_StatesLector = StatesLector.ErrorEscribir;
                            //oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                            //oResultadoOperacion.Mensaje = "Communication Error";
                            //MessageBox.Show("unknow Error", "Caution");
                        }
                    //}
                    //else
                    //{
                    //    _StatesDispenser = StatesDispenser.Error_Escribir_Tarjeta;
                    //    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    //    oResultadoOperacion.Mensaje = "Communication Error";
                    //    //MessageBox.Show("unknow Error", "Caution");
                    //    //MessageBox.Show("Comm. port is not Opened", "Caution");
                    //}
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_Escribir_Tarjeta;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Communication Error";
                    //MessageBox.Show("unknow Error", "Caution");
                    //MessageBox.Show("Comm. port is not Opened", "Caution");
                }

            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
        public ResultadoOperacion DevolverTarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                if (Hdle != 0)
                {

                    int i = DllClass.CRT570_PreDispense(Hdle);
                    if (i == 0)
                    {
                        //MessageBox.Show("Run OK", "Run");
                        _StatesDispenser = StatesDispenser.Tarjeta_Movida_FRONT_TO_RF;
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = "Tarjeta_Movida_FRONT_TO_RF";
                    }
                    else
                    {
                        _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_FRONT_TO_RF;
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_RF_TO_FRONT";
                    }
                }
                else
                {
                    _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_FRONT_TO_RF;
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error_MoverTarjetaReceptor_RF_TO_FRONT";
                }
            }
            catch (Exception ex)
            {
                _StatesDispenser = StatesDispenser.Error_MoverTarjetaReceptor_FRONT_TO_RF;
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            EventArgsDispensadorDevice e = new EventArgsDispensadorDevice(_StatesDispenser, oResultadoOperacion);
            DeviceMessageDispenserState(this, e);

            return oResultadoOperacion;
        }
    }

    public class EventArgsDispensadorDevice : EventArgs
    {
        private StatesDispenser _result;

        public StatesDispenser result
        {
            get { return _result; }
            set { _result = value; }
        }

        private ResultadoOperacion _resultString;

        public ResultadoOperacion resultString
        {
            get { return _resultString; }
            set { _resultString = value; }
        }

        public EventArgsDispensadorDevice(StatesDispenser oStatesDispenser, ResultadoOperacion oResultadoOperacion)
        {
            _result = oStatesDispenser;
            _resultString = oResultadoOperacion;
        }
    }
}
