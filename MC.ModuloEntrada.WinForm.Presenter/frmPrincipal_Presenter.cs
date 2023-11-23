﻿using EGlobalT.Device.SmartCardReaders;
using EGlobalT.Device.SmartCardReaders.Entities;
using GS.Apdu;
using GS.Util.Hex;
using MC.BusinessObjects.DataTransferObject;
using MC.BusinessObjects.Entities;
using MC.BusinessObjects.Enums;
using MC.ControlDevice;
using MC.CRT_570Device;
using MC.CRT603Device;
using MC.KytsDevice;
using MC.ModuloEntrada.WinForm.View;
using MC.PLCDevice;
using MC.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace MC.ModuloEntrada.WinForm.Presenter
{
    public class frmPrincipal_Presenter : Presenter<IView_Principal>
    {
        public frmPrincipal_Presenter(IView_Principal view)
            : base(view)
        {
            oPLCDevice.DeviceMessage += new EventHandler(oPLCDevice_DeviceMessage);
            oKYTsDevice.DeviceMessageKytState += new EventHandler(oKYTsDevice_DeviceMessage);
            oCRTDevice.DeviceMessageCrtState += new EventHandler(oCRTDevice_DeviceMessage);
            oDispenserDevice.DeviceMessageDispenserState += new EventHandler(oDispenserDevice_DeviceMessage);
            oControlDevice.DeviceMessageControlState += new EventHandler(oControlDevice_DeviceMessage);
        }

        Lectora_ACR122 Lectora = new Lectora_ACR122();
        Rspsta_CodigoTarjeta_LECTOR RspId = new Rspsta_CodigoTarjeta_LECTOR();
        PLCDeviceClass oPLCDevice = new PLCDeviceClass();
        KYTsDeviceClass oKYTsDevice = new KYTsDeviceClass();
        CRTDeviceClass oCRTDevice = new CRTDeviceClass();
        CRT570DeviceClass oDispenserDevice = new CRT570DeviceClass();
        PCSCReader reader = new PCSCReader();
        ControlDeviceClass oControlDevice = new ControlDeviceClass();

        string Descripcion = string.Empty;
        int NivelError = 0;
        string Parte = string.Empty;
        string TipoError = string.Empty;
        bool LoadKey = false;
        bool CheckKey = false;

        #region Modulo
        public bool ObtenerInfoModulo()
        {
            Modulo oModulo = new Modulo();
            oModulo.IdModulo = Globales.sSerial;
            oModulo.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);

            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = Model.ObtenerInfoModulo(oModulo);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.DtoModulo = (DtoModulo)oResultadoOperacion.EntidadDatos;
                View.General_Events = "(Presenter ObtenerInfoModulo) OK";
                ok = true;
            }
            else if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.General_Events = "(Presenter ObtenerInfoModulo) Sistema Suspendido - Info Modulo Error";
            }

            return ok;
        }
        public bool ObtenerPartes()
        {
            Modulo oModulo = new Modulo();
            oModulo.IdModulo = Globales.sSerial;
            oModulo.IdEstacionamiento = View.DtoModulo.IdEstacionamiento;

            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            View.DtoModulo.Partes.Clear();
            oResultadoOperacion = Model.ObtenerPartesModulo(oModulo);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.DtoModulo.Partes = (List<DtoParteModulo>)oResultadoOperacion.ListaEntidadDatos;

                View.General_Events = "(Presenter ObtenerInfoPartes) OK";
                ok = true;
            }
            else if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.General_Events = "(Presenter ObtenerInfoPartes) Sistema Suspendido - Info Partes Error";
                ok = false;
            }

            return ok;
        }
        public bool ObtenerParametros()
        {
            long IdEstacionamiento = View.DtoModulo.IdEstacionamiento;

            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            View.DtoModulo.Parametros.Clear();
            oResultadoOperacion = Model.ObtenerParametrosModulo(IdEstacionamiento);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.DtoModulo.Parametros = (List<DtoParametros>)oResultadoOperacion.ListaEntidadDatos;

                View.General_Events = "(Presenter ObtenerInfoParametros) OK";
                ok = true;
            }
            else if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.General_Events = "(Presenter ObtenerInfoParametros) Sistema Suspendido - Info Parametros Error";
            }

            return ok;
        }
        public bool RegistrarEntrada(Transaccion oTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            bool ok = false;
            oResultadoOperacion = Model.RegistrarMovimiento(oTransaccion);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {            
                ok = true;
            }

            return true;
        }
        public bool RegistrarAlarma(string Descripcion, int NivelError, string Parte, string TipoError)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            Alarma oAlarma = new Alarma();
 
            oAlarma.Descripcion = Descripcion;
            oAlarma.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);
            oAlarma.IdModulo = Globales.sSerial;
            oAlarma.NivelError = NivelError;
            oAlarma.Parte = Parte;
            oAlarma.TipoError = TipoError;

            bool ok = false;
            oResultadoOperacion = Model.RegistrarAlarma(oAlarma);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                ok = true;
            }

            return true;
        }
        public void SolucionarTodasAlarmas()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            Modulo oModulo = new Modulo();
            oModulo.IdModulo = Globales.sSerial;
            oModulo.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);

            oResultadoOperacion = Model.SolucionarTodasAlarmas(oModulo);
        }
        public bool ObtenerAutorizado(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            bool ok = false;
            oResultadoOperacion = Model.ObtenerAutorizado(oAutorizado);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.lstDtoAutorizado = (List<DtoAutorizado>)oResultadoOperacion.ListaEntidadDatos;
                ok = true;
            }

            return ok;
        }
        public bool ObtenerAutorizadoPlaca(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            bool ok = false;
            oResultadoOperacion = Model.ObtenerAutorizadoPlaca(oAutorizado);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.lstDtoAutorizado = (List<DtoAutorizado>)oResultadoOperacion.ListaEntidadDatos;
                ok = true;
            }

            return ok;
        }
        public string ObtenerValorParametro(Parametros idParametro)
        {
            string ValorParametro = string.Empty;
            foreach (DtoParametros item in View.DtoModulo.Parametros)
            {
                if (item.Codigo == idParametro.ToString())
                {
                    ValorParametro = item.Valor;
                    break;
                }
            }
            return ValorParametro;
        }
        public bool ObtenerTarjetas()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            Modulo oModulo = new Modulo();
            oModulo.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);

            bool ok = false;
            oResultadoOperacion = Model.ObtenerTarjetas(oModulo);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.lstDtoTarjetas = (List<DtoTarjetas>)oResultadoOperacion.ListaEntidadDatos;
                ok = true;
            }

            return ok;
        }
        public bool ValidarPlacaEntrada()
        {
            bool ok = false;

            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string IdModulo = Globales.sSerial;

            oResultadoOperacion = Model.ValidarPlacaEntrada(IdModulo);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.PlacaRegistrada = oResultadoOperacion.EntidadDatos.ToString();
                ok = true;
            }


            return ok;
        }
        #endregion

        #region Device

        public bool TestConexionDispositivos()
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            
            oKYTsDevice.TestConexionDispenser();

            if (View.KytReady)
            {

                oResultadoOperacion = PLCDeviceClass.EstadoConexion(Globales.sPuertoPLC);

                if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
                {
                    if (View.Presentacion == Pantalla.SistemaSuspendido)
                    {
                        View.Presentacion = Pantalla.SalvaPantallas;
                    }
                    ok = true;
                }
                else
                {
                    View.Presentacion = Pantalla.SistemaSuspendido;
                    Descripcion = "PLC no conectado";
                    NivelError = 1;
                    Parte = "PLC";
                    TipoError = "Error_Conexion_PLC";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    View.General_Events = "Error Conexion PLC";
                }
            }
            else 
            {
                View.Presentacion = Pantalla.SistemaSuspendido;
            }

            return ok;
        }
        public bool TestConexionSuspendidoDispositivos()
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oKYTsDevice.ConectarDevice(Globales.sPuerto, ObtenerValorParametro(Parametros.claveTarjeta).ToString());

            if (View.KytReady)
            {

                oResultadoOperacion = PLCDeviceClass.EstadoConexion(Globales.sPuertoPLC);

                if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
                {
                    if (View.Presentacion == Pantalla.SistemaSuspendido)
                    {
                        View.Presentacion = Pantalla.SalvaPantallas;
                    }
                    ok = true;
                }
                else
                {
                    View.Presentacion = Pantalla.SistemaSuspendido;
                    Descripcion = "PLC no conectado";
                    NivelError = 1;
                    Parte = "PLC";
                    TipoError = "Error_Conexion_PLC";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    View.General_Events = "Error Conexion PLC";
                }
            }
            else
            {
                View.Presentacion = Pantalla.SistemaSuspendido;
            }

            return ok;
        }


        #region PLC
        void oPLCDevice_DeviceMessage(object sender, EventArgs e)
        {
            var i = (EventArgsPLCDevice)e;

            if (i.result.oEstado == TipoRespuesta.Exito)
            {
                if (Convert.ToBoolean(i.result.EntidadDatos) == false)
                {
                    //RegistrarAlarma(TipoAlarma.ErrorPLC, TipoParte.PLC.ToString(), "Error conexion PLC");
                }
                else
                {
                    //SolucionarAlarma(TipoAlarma.ErrorPLC, TipoParte.PLC, "Error conexion PLC");
                }
            }
        }
        public bool ConexionPLC()
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = PLCDeviceClass.Conectar(Globales.sPuertoPLC);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                ok = true;
                View.General_Events = "(PRESENTER) Conexion Exitosa PLC";
            }
            else
            {
                View.General_Events = oResultadoOperacion.Mensaje;
            }

            return ok;
        }
        public bool LimpiarValoresPLC()
        {
            bool ok = false;

            try
            {
                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.BorraEstadoLlegoCarroBarrera(Globales.sPuertoPLC);

                if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
                {
                    oResultadoOperacion = PLCDeviceClass.BorraEstadoSefueCarroBarrera(Globales.sPuertoPLC);
                    if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
                    {
                        ok = true;
                        View.General_Events = "(PRESENTER) Borrado Exitoso Variables PLC";
                    }
                    else
                    {
                        View.General_Events = "(PRESENTER) Error Borrado Varibales PLC";
                    }
                }
                else
                {
                    View.General_Events = "(PRESENTER) Error Borrado Varibales PLC";
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }
            return ok;
        }
        public bool EstadoConexionPLC()
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = PLCDeviceClass.EstadoConexion(Globales.sPuertoPLC);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                if (View.Presentacion == Pantalla.SistemaSuspendido)
                {
                    View.Presentacion = Pantalla.SalvaPantallas;
                }
                ok = true;
            }
            else 
            {
                View.Presentacion = Pantalla.SistemaSuspendido;
                Descripcion = "PLC no conectado";
                NivelError = 1;
                Parte = "PLC";
                TipoError = "Error_Conexion_PLC";

                RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                View.General_Events = "Error Conexion PLC";
            }
            
            return ok;
        }
        public bool VehiculoMueble()
        {
            bool ok = false;
            try
            {

                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.CarroMueble(Globales.sPuertoPLC);

                if (Convert.ToBoolean(oResultadoOperacion.ListaEntidadDatos[0]) == true)
                {

                    ok = true;
                    View.General_Events = "Vehiculo En Mueble";
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }

            return ok;
        }
        public bool ValidacionMotoMueble()
        {
            bool ok = false;
            try
            {

                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.ValidacionMotoMueble(Globales.sPuertoPLC);

                if (Convert.ToBoolean(oResultadoOperacion.ListaEntidadDatos[0]) == true)
                {
                    ok = true;
                    View.General_Events = "Vehiculo En Mueble";
                }
                else 
                {
                    ok = false;
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }

            return ok;
        }
        public bool VehiculoTalanquera()
        {
            bool ok = false;
            try
            {
                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.CarroenBarrera(Globales.sPuertoPLC);

                if (Convert.ToBoolean(oResultadoOperacion.ListaEntidadDatos[0]) == true)
                {
                    ok = true;
                    View.General_Events = "Vehiculo En Talanquera";
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }
         
            return ok;
        }
        public bool VehiculoSalioTalanquera()
        {
            bool ok = false;

            try
            {
                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.CarroSeFueBarrera(Globales.sPuertoPLC);

                if (Convert.ToBoolean(oResultadoOperacion.ListaEntidadDatos[0]) == true)
                {
                    ok = true;
                    View.General_Events = "Vehiculo Salio Talanquera";
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }
            return ok;
        }
        public bool AbrirTalanquera()
        {
            bool ok = false;
            try
            {
                ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

                oResultadoOperacion = PLCDeviceClass.AbrirTalanquera(Globales.sPuertoPLC);

                if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
                {
                    ok = true;
                    View.General_Events = "Talanquera Abierta";
                }
            }
            catch (Exception)
            {
                View.General_Events = "Error Conexion PLC";
                View.Presentacion = Pantalla.SistemaSuspendido;
            }
            return ok;
        }
        #endregion

        #region KytDispenser

        #region Funciones
        public void ConectarDispensador()
        {
            oKYTsDevice.ConectarDevice(Globales.sPuerto, ObtenerValorParametro(Parametros.claveTarjeta).ToString());
        }
        public void AlistarTarjeta()
        {
            oKYTsDevice.AlistarTarjeta();
        }
        public void LimpiarTarjeta()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            oResultadoOperacion = oKYTsDevice.BorrarTarjeta();

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.IdCard = oResultadoOperacion.EntidadDatos.ToString();
                View.CleanCardKyt = true;
            }

        }
        public void DispensarTarjeta()
        {
            oKYTsDevice.DispensarTarjeta();
        }
        public void DevolverTarjeta()
        {
            oKYTsDevice.DevolverTarjeta();
        }
        public void TestConexionDispensador()
        {
            oKYTsDevice.TestConexionDispenser();
        }
        public void PocisionCard()
        {
            oKYTsDevice.PosicionCard();
        }
        public void DesecharCard()
        {
            oKYTsDevice.DesecharTarjeta();
        }
        public void StateDispenser()
        {
            oKYTsDevice.EstadoStacker();
        }

        public void EscribirCard(Tarjeta oTarjeta,bool Moto)
        {
            oKYTsDevice.EscribirTarjeta(oTarjeta,Moto);
        }

        #endregion
       
        #region Eventos
        void oKYTsDevice_DeviceMessage(object sender, EventArgs e)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            var i = (EventArgsKytDevice)e;

            switch (i.result)
            {
                #region Error_Conexion
                case StatesKYT.Error_Conexion_Dispensador:
                    View.KytReady = false;
                    View.General_Events =  i.result.ToString();

                    Descripcion = "Dispensador no conectado";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_Conexion_Dispensador";
                    
                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Conexion_Exitosa
                case StatesKYT.Conexion_Exitosa_Dispensador:
                    View.KytReady = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion
                
                #region Alistar_Tarjeta
                case StatesKYT.Tarjeta_Alistada:
                    
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;

                    Modulo oModulo = new Modulo();
                    oModulo.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);

                    ResultadoOperacion oResultadoOperacionTarjeta = new ResultadoOperacion();
                    oResultadoOperacionTarjeta = Model.ObtenerTarjetas(oModulo);

                    if (oResultadoOperacionTarjeta.oEstado == TipoRespuesta.Exito)
                    {
                        List<DtoTarjetas> olstDtoTarjetas = new List<DtoTarjetas>();

                        olstDtoTarjetas = (List<DtoTarjetas>)oResultadoOperacionTarjeta.ListaEntidadDatos;

                        for (int t = 0; t < olstDtoTarjetas.Count; t++)
                        {
                            if (olstDtoTarjetas[t].IdTarjeta == oResultadoOperacion.EntidadDatos.ToString() && olstDtoTarjetas[t].Estado == true)
                            {
                                View.General_Events = "Tarjeta Enconrada: " + olstDtoTarjetas[t].IdTarjeta;
                                View.CardKytReady = true;
                                break;
                            }
                        }

                        if (!View.CardKytReady)
                        {
                            DesecharCard();
                        }

                    }
                    else if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
                    {
                        DesecharCard();
                    }                  

                    break;
                #endregion

                #region Error_AlistarTarjeta
                case StatesKYT.Error_AlistarTarjeta:
                    View.CardKytReady = false;
                    //View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Desechar_Tarjeta
                case StatesKYT.Tarjeta_Movida_TOBINBOX:
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;
                    AlistarTarjeta();
                    break;
                #endregion

                #region Error_DesecharTarjeta
                case StatesKYT.Error_MoverTarjetaTO_BINBOX:
                    View.CardKytReady = false;
                    View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();

                    Descripcion = "Error desechando tarjeta";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_DesecharTarjeta";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Tarjeta_Borrada
                case StatesKYT.Tarjeta_Borrada:
                    View.CleanCardKyt = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_Borrar_Tarjeta
                case StatesKYT.Error_Borrar_Tarjeta:
                    View.CleanCardKyt = false;
                    View.General_Events = i.result.ToString();

                    Descripcion = "Error Borrando tarjeta";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_Borrar_Tarjeta";
                    
                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Tarjeta_Movida_RF_TO_FRONT
                case StatesKYT.Tarjeta_Movida_RF_TO_FRONT:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_MoverTarjetaRF_TO_FRONT
                case StatesKYT.Error_MoverTarjetaRF_TO_FRONT:
                    View.General_Events = i.result.ToString();

                    Descripcion = "Error Moviendo tarjeta";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_MoverTarjetaRF_TO_FRONT";
                    
                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Tarjeta_Movida_FRONT_TO_RF
                case StatesKYT.Tarjeta_Movida_FRONT_TO_RF:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_MoverTarjeta_FRONT_TO_RF
                case StatesKYT.Error_MoverTarjeta_FRONT_TO_RF:
                    View.General_Events = i.result.ToString();

                    Descripcion = "Error Moviendo tarjeta";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_MoverTarjeta_FRONT_TO_RF";
                    
                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Tarjeta_Escrita
                case StatesKYT.Tarjeta_Escrita:
                    View.General_Events = i.result.ToString();
                    View.WriteCard = true;
                    break;
                #endregion

                #region Error_Escribir_Tarjeta
                case StatesKYT.Error_Escribir_Tarjeta:
                    View.General_Events = i.result.ToString();
                    View.WriteCard = false;
                    Descripcion = "Error Escribiendo tarjeta";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_Escribir_Tarjeta";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Dispensador_OK
                case StatesKYT.Estado_Dispensador_OK:
                    //View.General_Events = i.result.ToString();
                    View.KytReady = true;
                    if (View.Presentacion == Pantalla.SistemaSuspendido)
                    {
                        View.Presentacion = Pantalla.SalvaPantallas;
                    }

                    break;
                #endregion

                #region TestDispensador_ERROR
                case StatesKYT.TestDispensador_ERROR:
                    View.KytReady = false;
                    //View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();

                    Descripcion = "Error en el Test del Dispensador";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_Conexion_Dispensador";
                    
                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Estado_StackerGood
                case StatesKYT.Estado_StackerGood:

                    if (View.Presentacion == Pantalla.SinTarjetas)
                    {
                        View.Presentacion = Pantalla.SalvaPantallas;
                    }

                    break;
                #endregion

                #region Estado_StackerNivelBajo
                case StatesKYT.Estado_StackerNivelBajo:
                    View.General_Events = i.result.ToString();
                    
                    Descripcion = "Nivel Bajo de tarjetas";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Nivel_Bajo_Stacker";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    break;
                #endregion

                #region Estado_StackerSinTarjetas
                case StatesKYT.Estado_StackerSinTarjetas:
                    
                    View.Presentacion = Pantalla.SinTarjetas;
                    View.General_Events = i.result.ToString();

                    View.SinTarjetas = true;

                    Descripcion = "No hay tarjetas Stacker";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Sin_Tarjetas_Dispenser";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    break;
                #endregion

                #region Error_Estado_Stacker
                case StatesKYT.Error_Estado_Stacker:
                    View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();

                    //Descripcion = "Error en el Test del Dispensador";
                    //NivelError = 1;
                    //Parte = "kyt_Dispenser";
                    //TipoError = "Error_Conexion_Dispensador";

                    //RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Sin_Tarjeta
                case StatesKYT.Sin_Tarjeta:
                    View.RemoveCard = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Default
                default:
                    break;
                #endregion
            }

        }
        #endregion

        #endregion

        #region CRT
        
        #region Funciones
        public void ConectarCRT()
        {
            oCRTDevice.ConectarCRT();
        }
        public void GetIdCard()
        {
            oCRTDevice.GetIdCardCRT();
        }
        public void LoadKeyCard()
        {
            oCRTDevice.LoadKeyCRT("");
        }
        public void CheckKeyCard()
        {
            oCRTDevice.CheckKeyCRT();
        }
        public void WriteCard(Tarjeta oTarjeta)
        {
            oCRTDevice.LoadKeyCRT(ObtenerValorParametro(Parametros.ClaveTarjetasCRT).ToString());
            if (LoadKey)
            {
                oCRTDevice.CheckKeyCRT();

                if (CheckKey)
                {
                    oCRTDevice.WriteCardCRT(oTarjeta);
                }
            }
            
        }
        public bool ReadCard()
        {
            bool ok = false;

            try
            {
                

                RspId = Lectora.ObtenerIDTarjeta();

                if (RspId.CodigoTarjeta != string.Empty)
                {
                    View.IdCardAutorizado = RspId.CodigoTarjeta;
                }
            }
            catch (Exception ex)
            {
                reader.Disconnect();
            }

            return ok;
        }
        public bool conexionACR()
        {
            bool ok = false;

            try
            {
               Rspsta_Conexion_LECTOR OO = Lectora.Conectar(true);
               if (OO.Conectado)
               {
                   ok = true; 
               }
            }
            catch (Exception ex)
            {
            }

            return ok;
        }
        #endregion

        #region Eventos
        void oCRTDevice_DeviceMessage(object sender, EventArgs e)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            var i = (EventArgsCrtDevice)e;
            
            switch (i.result)
            {
                #region Error_Conexion
                case StatesCRT.Error_Conexion_CRT:
                    View.CRTReady = false;
                    oResultadoOperacion = i.resultString;
                    View.General_Events = oResultadoOperacion.Mensaje;

                    Descripcion = "Lector Autorizados no conectado";
                    NivelError = 1;
                    Parte = "CRT_603";
                    TipoError = "Error_Conexion_CRT";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Conexion_Exitosa
                case StatesCRT.Conexion_Exitosa_CRT:
                    View.CRTReady = true;
                    oResultadoOperacion = i.resultString;
                    //View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region Sin_Tarjeta
                case StatesCRT.Sin_Tarjeta:
                    //View.RemoveCard = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region IdCardCRT_OK
                case StatesCRT.IdCardCRT_OK:
                    oResultadoOperacion = i.resultString;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    View.IdCardAutorizado = oResultadoOperacion.EntidadDatos.ToString();
                    break;
                #endregion

                #region Error_GetId
                case StatesCRT.Error_GetId:
                    oResultadoOperacion = i.resultString;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region LoadKeyCRT_ok
                case StatesCRT.LoadKeyCRT_ok:
                    LoadKey = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_LoadKeyCRT
                case StatesCRT.Error_LoadKeyCRT:
                    LoadKey = false;
                    View.General_Events = i.result.ToString();
                    
                    //ACERQUE LÑA TARJETA DE AUTO OTRAVEZ 
                    //View.Presentacion = Pantalla

                    break;
                #endregion

                #region CheckKeyCRT_ok
                case StatesCRT.CheckKeyCRT_ok:
                    CheckKey = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_CheckKeyCRT
                case StatesCRT.Error_CheckKeyCRT:
                    CheckKey = false;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region WriteCard_ok
                case StatesCRT.WriteCard_ok:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_WriteCard
                case StatesCRT.Error_WriteCard:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region ReadCard_ok
                case StatesCRT.ReadCard_ok:
                    oResultadoOperacion = i.resultString;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    View.Tarjeta = (Tarjeta)oResultadoOperacion.EntidadDatos;
                    break;
                #endregion

                #region Error_ReadCard
                case StatesCRT.Error_ReadCard:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Default
                default:
                    break;
                #endregion
            }

        }
        #endregion


        #endregion

        #region Camara
        public void CapturaFoto(string Orden, string SecuenciaTransaccion)
        {

            for (int i = 0; i < View.DtoModulo.Partes.Count; i++)
            {

                string sFileName = string.Empty;

                sFileName = @"C:\FOTOS\";

                if (!Directory.Exists(sFileName))
                {
                    Directory.CreateDirectory(sFileName);
                }


                if (View.DtoModulo.Partes[i].TipoParte == "CAMARA" && View.DtoModulo.Partes[i].Estado)
                {
                    sFileName = sFileName + SecuenciaTransaccion + "_" + Globales.sSerial + "_" + View.DtoModulo.Partes[i].NombreParte + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + Orden + ".jpg";

                    //string sUrl = "http://" + ObtenerValorParametro(Parametros.IPCamara) + "/cgi-bin/media.cgi?action=getSnapshot";
                    string User = ObtenerValorParametro(Parametros.UsuarioCamaras).ToString();
                    string Pass = ObtenerValorParametro(Parametros.PasswordCamaras).ToString();

                    View.General_Events = "(Presenter CapturaFoto) " + User + " " + Pass;

                    string sUrl = "http://" + View.DtoModulo.Partes[i].IPDispositivo + "/Streaming/channels/101/picture";
                    
                    View.General_Events = "(Presenter CapturaFoto) COMANDO " + sUrl;
                    if (Generales.WebSiteIsAvailable(sUrl, User,Pass))
                    {
                        WebClient webClient = new WebClient();                        

                        webClient.Credentials = new NetworkCredential(User, Pass);
                        try
                        {
                            webClient.DownloadFile(sUrl, sFileName);
                        }
                        catch (Exception e)
                        {
                            View.General_Events = "(Presenter CapturaFoto) Exception: "+ View.DtoModulo.Partes[i].NombreParte + " " + e.InnerException + " " + e.Message + " " + e.Source;
                        }
                    }
                    else
                    {
                        View.General_Events = "(Presenter CapturaFoto) Error: ALARMA CAMARA IP" + View.DtoModulo.Partes[i].NombreParte;
                    } 
                }
            }            
        }
        #endregion

        #region Barrera
        public bool ObtenerEventoDispo()
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = Model.ObtenerEventoDispo();

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.Barrera = (string)oResultadoOperacion.EntidadDatos;
                View.General_Events = "(Presenter ObtenerEventoDispo) OK";
                ok = true;
            }

            return ok;
        }
        public bool ActualizarEventoDispo(long IdEvento)
        {
            bool ok = false;
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = Model.ActualizarEventoDispositivo(IdEvento);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.General_Events = "(Presenter ActualizarEventoDispositivo) OK";
                ok = true;
            }

            return ok;
        }
        #endregion

        #region Dispensador
        
        #region Eventos
        void oDispenserDevice_DeviceMessage(object sender, EventArgs e)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            var i = (EventArgsDispensadorDevice)e;

            switch (i.result)
            {
                #region Error_Conexion
                case StatesDispenser.ErrorConexion_Dispensador:
                    View.KytReady = false;
                    View.General_Events = i.result.ToString();

                    Descripcion = "Dispensador no conectado";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Error_Conexion_Dispensador";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Conexion_Exitosa
                case StatesDispenser.ConexionExitosa_Dispensador:
                    View.KytReady = true;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Alistar_Tarjeta
                case StatesDispenser.Tarjeta_Alistada:

                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;

                    Modulo oModulo = new Modulo();
                    oModulo.IdEstacionamiento = Convert.ToInt64(Globales.iCodigoEstacionamiento);

                    ResultadoOperacion oResultadoOperacionTarjeta = new ResultadoOperacion();
                    oResultadoOperacionTarjeta = Model.ObtenerTarjetas(oModulo);

                    if (oResultadoOperacionTarjeta.oEstado == TipoRespuesta.Exito)
                    {
                        List<DtoTarjetas> olstDtoTarjetas = new List<DtoTarjetas>();

                        olstDtoTarjetas = (List<DtoTarjetas>)oResultadoOperacionTarjeta.ListaEntidadDatos;
                        if (oResultadoOperacion.EntidadDatos != null)
                        {
                            View.IdCard = oResultadoOperacion.EntidadDatos.ToString();


                            for (int t = 0; t < olstDtoTarjetas.Count; t++)
                            {
                                if (olstDtoTarjetas[t].IdTarjeta == oResultadoOperacion.EntidadDatos.ToString() && olstDtoTarjetas[t].Estado == true)
                                {
                                    View.General_Events = "Tarjeta Encontrada: " + olstDtoTarjetas[t].IdTarjeta;
                                    View.CardKytReady = true;
                                    //LimpiarTarjetaNew();
                                    break;
                                }
                            }
                        }

                        if (!View.CardKytReady)
                        {
                            //DesecharCard();
                        }

                    }
                    else if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
                    {
                        DesecharCard();
                    }

                    break;
                #endregion

                #region Dispensar_Tarjeta
                case StatesDispenser.Tarjeta_MovidaReceptor_RF_TO_FRONT:
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;
                    View.RemoveCard = false;
                    if (View.IdCardAutorizado == "VENCIDO")
                    {
                        View.Presentacion = Pantalla.AutorizacionVencida;
                    }
                    else 
                    {
                        View.Presentacion = Pantalla.RetireTarjeta;
                    }
                    break;
                #endregion

                #region Tarjeta_En_Boca
                case StatesDispenser.Tarjeta_En_Boca:
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;
                    View.RemoveCard = false;
                    View.General_Events = "Tarjeta_En_Boca";
                    break;
                #endregion

                #region Sin_Tarjeta
                case StatesDispenser.Sin_Tarjeta:
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;
                    View.RemoveCard = true;
                    break;
                #endregion

                #region Tarjeta_Borrada
                case StatesDispenser.Tarjeta_Borrada:
                    View.General_Events = i.result.ToString();
                    View.RemoveCard = false;
                    break;
                #endregion

                #region Tarjeta_Movida_TOBINBOX
                case StatesDispenser.Tarjeta_Movida_TOBINBOX:
                    View.General_Events = i.result.ToString();
                    AlistarTarjetaNew();
                    break;
                #endregion

                #region Error_Borrar_Tarjeta
                case StatesDispenser.Error_Borrar_Tarjeta:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Devolver_Tarjeta
                case StatesDispenser.Tarjeta_MovidaReceptor_FRONT_TO_RF:
                    View.General_Events = i.result.ToString();
                    oResultadoOperacion = i.resultString;
                    View.RemoveCard = false;
                    break;
                #endregion

                #region Error_DevolverTarjeta
                case StatesDispenser.Error_MoverTarjetaReceptor_FRONT_TO_RF:
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_DispensarTarjeta
                case StatesDispenser.Error_MoverTarjetaReceptor_RF_TO_FRONT:
                    View.CardKytReady = false;
                    //View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_AlistarTarjeta
                case StatesDispenser.Error_AlistarTarjeta:
                    View.CardKytReady = false;
                    //View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = i.result.ToString();
                    break;
                #endregion

                #region Error_EscribirTarjeta
                case StatesDispenser.Error_Escribir_Tarjeta:
                    View.General_Events = i.resultString.ToString();
                    CapturarTarjetaNew();
                    break;
                #endregion

                #region Error_MoverTarjetaReceptor_TO_BINBOX
                case StatesDispenser.Error_MoverTarjetaReceptor_TO_BINBOX:
                    View.General_Events = i.resultString.ToString();
                    View.Presentacion = Pantalla.SistemaSuspendido;
                    View.General_Events = "Error_MoverTarjetaReceptor_TO_BINBOX Tarjeta Atascada";
                    break;
                #endregion

                #region Estado_StackerGood
                case StatesDispenser.Estado_StackerGood:

                    if (View.Presentacion == Pantalla.SinTarjetas)
                    {
                        View.Presentacion = Pantalla.SalvaPantallas;
                    }

                    break;
                #endregion

                #region Estado_StackerNivelBajo
                case StatesDispenser.Estado_StackerNivelBajo:
                    View.General_Events = i.result.ToString();

                    Descripcion = "Nivel Bajo de tarjetas";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Nivel_Bajo_Stacker";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    break;
                #endregion

                #region Estado_StackerSinTarjetas
                case StatesDispenser.Estado_StackerSinTarjetas:

                    View.Presentacion = Pantalla.SinTarjetas;
                    View.General_Events = i.result.ToString();

                    View.SinTarjetas = true;

                    Descripcion = "No hay tarjetas Stacker";
                    NivelError = 1;
                    Parte = "kyt_Dispenser";
                    TipoError = "Sin_Tarjetas_Dispenser";

                    RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);
                    break;
                #endregion

                #region Error_Estado_Stacker
                case StatesDispenser.Error_Estado_Stacker:
                    //View.Presentacion = Pantalla.SistemaSuspendido;
                    //View.General_Events = i.result.ToString();

                    //Descripcion = "Error en el Test del Dispensador";
                    //NivelError = 1;
                    //Parte = "kyt_Dispenser";
                    //TipoError = "Error_Conexion_Dispensador";

                    //RegistrarAlarma(Descripcion, NivelError, Parte, TipoError);

                    break;
                #endregion

                #region Tarjeta_Escrita
                case StatesDispenser.Tarjeta_Escrita:
                    View.General_Events = i.result.ToString();
                    DispensaTarjetaNew();
                    break;
                #endregion

                #region Default
                default:
                    break;
                #endregion
            }

        }
        #endregion

        #region Funciones
        public void ConectarDispensadorNew()
        {
            oDispenserDevice.ConectarDispensador(Globales.sPuerto);
        }
        public void AlistarTarjetaNew()
        {
            oDispenserDevice.AlistarTarjeta();
        }
        public void Escribirtarjeta()
        {

            string sKey = ObtenerValorParametro(Parametros.KeyCard).ToString();
            oDispenserDevice.Escribirtarjeta(View.Tarjeta, sKey);
        }
        public void CapturarTarjetaNew()
        {
            oDispenserDevice.CapturarTarjeta();
        }
        public void LimpiarTarjetaNew()
        {
            string sKey = ObtenerValorParametro(Parametros.KeyCard).ToString();
            oDispenserDevice.Limpiartarjeta(View.Tarjeta, sKey);
        }
        public void StateDispenserNew()
        {
            oDispenserDevice.EstadoStacker();
        }
        public void DispensaTarjetaNew()
        {
            oDispenserDevice.DispensaTarjeta();
        }
        public void DevolverTarjetaNew()
        {
            oDispenserDevice.DevolverTarjeta();
        }
        //public void Escribirtarjeta()
        //{
        //    string sKey = ObtenerValorParametro(Parametros.KeyCard).ToString();
        //    oDispenserDevice.Escribirtarjeta(View.Tarjeta, sKey);
        //}

        #endregion

        #endregion


        #region Control

        #region Eventos
        void oControlDevice_DeviceMessage(object sender, EventArgs e)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            var i = (EventArgsControlDevice)e;

            switch (i.result)
            {
                #region Error_Conexion
                case StatesControl.Error_Conexion:
                    View.ControlReady = false;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region Conexion_Exitosa
                case StatesControl.Conexion_Exitosa:
                    View.ControlReady = true;
                    //oResultadoOperacion = i.resultString;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region BotonPresionado
                case StatesControl.BotonPresionado:
                    View.BotonPresionado = true;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region NoBotonPresionado
                case StatesControl.NoBoton:
                    View.BotonPresionado = false;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region VehiculoMueble
                case StatesControl.VehiculoMueble:
                    View.VehiculoMueble = true;                    
                    if (i.resultString.EntidadDatos != null)
                    {
                        if (i.resultString.EntidadDatos == "Carro")
                        {
                            View.Moto = false;
                        }
                        else
                        {
                            View.Moto = true;
                        }
                    }
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region VehiculoTalanquera
                case StatesControl.VehiculoTalanquera:
                    View.VehiculoTalanquera = true;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region VahiculoSalioTalanquera
                case StatesControl.VahiculoSalioTalanquera:
                    View.VehiculoTalanquera =true;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region NoHayCarro
                case StatesControl.NoHayCarro:
                    View.VehiculoMueble = false;
                    View.VehiculoTalanquera = false;
                    View.General_Events = oResultadoOperacion.Mensaje;
                    break;
                #endregion

                #region Default
                default:
                    break;
                #endregion
            }

        }
        #endregion

        #region Funciones
        public void ConectarControl()
        {
            oControlDevice.ConectarControl(Globales.sPuertoPLC);
        }
        public void EstadoControl()
        {
            oControlDevice.EstadoControl();
        }
        public void AperturaBarrera()
        {
            oControlDevice.AperturaBarrera();
        }
        #endregion

        #endregion

        #endregion

        #region Service
        public bool ValidarIngresoAuto(string IDCARD)
        {
            bool ok = false;

            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = Model.ValidarIngreso(IDCARD);
            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                if (Convert.ToBoolean(oResultadoOperacion.EntidadDatos) == true)
                {
                    ok = true;
                }
                else 
                {
                    ok = false;
                }
            }
            else
            {
                View.General_Events = "Error (Presenter EnviarImagenesServidor): " + oResultadoOperacion.Mensaje;

                ok = true;
            }

            return ok;
        }
        public bool EnvioImagenes(List<Imagen> olstImagenes, Transaccion oTransaccion)
        {
            bool ok = false;

            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = Model.EnviarImagenesServidor(olstImagenes, oTransaccion);
            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.General_Events = "(Presenter EnviarImagenesServidor) " + oResultadoOperacion.Mensaje;
                ok = true;
            }
            else
            {
                View.General_Events = "Error (Presenter EnviarImagenesServidor): " + oResultadoOperacion.Mensaje;
                
                ok = false;
            }

            return ok;
        }
        #endregion

        #region Printer

        public DataSetEntrada GenerarTicketEntrada(Transaccion transaccion)
        {
            DataSetEntrada dataSetEntrada = new DataSetEntrada();


            DataSetEntrada.TablaDatosEntradaRow tablaDatosEntradaRow = dataSetEntrada.TablaDatosEntrada.NewTablaDatosEntradaRow();

            // Crear una instancia de BarcodeWriter
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.CODE_128;
                
            // Generar el código de barras como un objeto Bitmap
            Bitmap barcodeBitmap = barcodeWriter.Write(Convert.ToString(transaccion.IdTransaccion));
            string rutaGuardar = View.imgUrl;
            // Guardar el código de barras en un archivo con el nombre IdTransaccion
            string codigoBarrasFileName = rutaGuardar + "\\" + transaccion.IdTransaccion + ".png";
            barcodeBitmap.Save(codigoBarrasFileName);
            View.imgUrl = codigoBarrasFileName;
            // Limpieza
            barcodeBitmap.Dispose();
            tablaDatosEntradaRow.PlacaEntrada = Convert.ToString(transaccion.PlacaEntrada);
            tablaDatosEntradaRow.FechaEntrada = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tablaDatosEntradaRow.ModuloEntrada = transaccion.ModuloEntrada;
            if (transaccion.TipoVehiculo == 1)
            {
                tablaDatosEntradaRow.TipoVehiculo = "Carro";
            }
            else
            {
                tablaDatosEntradaRow.TipoVehiculo = "Moto";
            }
            tablaDatosEntradaRow.Estacionamiento = "EDIFICIO PLAZA CENTAL PH";
            tablaDatosEntradaRow.Direccion = "CRA 16 # 33-44";
            tablaDatosEntradaRow.Telefono = "6076700040";
            dataSetEntrada.TablaDatosEntrada.AddTablaDatosEntradaRow(tablaDatosEntradaRow);

            return dataSetEntrada;
        }
        #endregion

    }
}
