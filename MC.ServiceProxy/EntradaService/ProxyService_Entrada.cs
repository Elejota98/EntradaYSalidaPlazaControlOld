﻿using MC.BusinessObjects.DataTransferObject;
using MC.BusinessObjects.Entities;
using MC.BusinessObjects.Enums;
using MC.ServiceProxy.DataTransferObjectMapper;
using MC.ServiceProxy.MC_EntradaService;
using MC.Utilidades;
using MC.WCFProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ServiceProxy
{
    public partial class ProxyService : IProxyService
    {

        public ResultadoOperacion ObtenerPartesModulo(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoParteModulo> lstDtoParteModulo = new List<DtoParteModulo>();

            getPartesModulo_Request request = new getPartesModulo_Request();
            request.RequestId = NuevoRequestId;

            ServiceModulo oServiceModulo = new ServiceModulo();
            oServiceModulo.IdModulo = oModulo.IdModulo;
            oServiceModulo.IdEstacionamiento = oModulo.IdEstacionamiento;

            request.oModulo = oServiceModulo;

            getPartesModulo_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getPartesModulo(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerPartesModulo() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        lstDtoParteModulo = Mapper.FromDataTransferObjects(response.olstDtoParteModulo);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerPartesModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerPartesModulo() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = lstDtoParteModulo;

            return oResultadoOperacion;
        }

        public ResultadoOperacion RegistrarMovimiento(Transaccion oTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            long IdTransaccion = 0; 

            setRegistrarTransaccion_Request request = new setRegistrarTransaccion_Request();
            request.RequestId = NuevoRequestId;

            ServiceTransaccion oServiceTransaccion = new ServiceTransaccion();
            oServiceTransaccion.CarrilEntrada = oTransaccion.CarrilEntrada;
            oServiceTransaccion.IdEstacionamiento = oTransaccion.IdEstacionamiento;
            oServiceTransaccion.IdTarjeta = oTransaccion.IdTarjeta;
            oServiceTransaccion.IdTransaccion = oTransaccion.IdTransaccion;
            oServiceTransaccion.ModuloEntrada = oTransaccion.ModuloEntrada;
            oServiceTransaccion.PlacaEntrada = oTransaccion.PlacaEntrada;
            oServiceTransaccion.IdAutorizado = oTransaccion.IdAutorizado;
            oServiceTransaccion.IdTipoVehiculo = oTransaccion.TipoVehiculo;

            request.oTransaccion = oServiceTransaccion;

            setRegistrarTransaccion_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.setRegistrarTransaccion(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        IdTransaccion = response.lIdTransaccion;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "RegistrarTransaccion() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.EntidadDatos = IdTransaccion;

            return oResultadoOperacion;
        }

        public ResultadoOperacion RegistrarAlarma(Alarma oAlarma)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            setRegistrarAlarma_Request request = new setRegistrarAlarma_Request();
            request.RequestId = NuevoRequestId;

            ServiceAlarma oServiceAlarma = new ServiceAlarma();
            oServiceAlarma.Descripcion = oAlarma.Descripcion;
            oServiceAlarma.IdEstacionamiento = oAlarma.IdEstacionamiento;
            oServiceAlarma.IdModulo = oAlarma.IdModulo;
            oServiceAlarma.NivelError = oAlarma.NivelError;
            oServiceAlarma.Parte = oAlarma.Parte;
            oServiceAlarma.TipoError = oAlarma.TipoError;
            
            request.oAlarma = oServiceAlarma;


            setRegistrarAlarma_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.setRegistrarAlarma(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error conexion Modulo Service";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {

                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Respuesta Invalida Modulo Service: setCrearAlarma";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error Respuesta Modulo Service: setCrearAlarma";
                return oResultadoOperacion;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerInformacionModulo(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DtoModulo oDtoModulo = new DtoModulo();

            getInformacionModulo_Request request = new getInformacionModulo_Request();
            request.RequestId = NuevoRequestId;

            ServiceModulo oServiceModulo = new ServiceModulo();
            oServiceModulo.IdModulo = oModulo.IdModulo;
            oServiceModulo.IdEstacionamiento = oModulo.IdEstacionamiento;

            request.oModulo = oServiceModulo;

            getInformacionModulo_Response response = null;


            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getInformacionModulo(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        oDtoModulo = Mapper.FromDataTransferObject(response.oDatosModulo);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerInformacionModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerInformacionModulo() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.EntidadDatos = oDtoModulo;
            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerParametros(long IdEstacionamiento)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoParametros> olstDtoParametros = new List<DtoParametros>();

            getParametros_Request request = new getParametros_Request();
            request.RequestId = NuevoRequestId;

            request.IdEstacionamiento = IdEstacionamiento;

            getParametros_Response response = null;


            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getParametros(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        olstDtoParametros = Mapper.FromDataTransferObjects(response.olstDtoParametros);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerInformacionModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerInformacionModulo() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = olstDtoParametros;
            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerAutorizado(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoAutorizado> olstDtoAutorizado = new List<DtoAutorizado>();


            getInfoAutorizado_Request request = new getInfoAutorizado_Request();
            request.RequestId = NuevoRequestId;

            ServiceAutorizado oServiceAutorizado = new ServiceAutorizado();
            oServiceAutorizado.IdTarjeta = oAutorizado.IdTarjeta;
            oServiceAutorizado.Placa1 = oAutorizado.PlacaAuto;

            request.oAutorizado = oServiceAutorizado;

            getInfoAutorizado_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getInfoAutorizado(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        olstDtoAutorizado = Mapper.FromDataTransferObjects(response.olstDtoAutorizado);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "RegistrarTransaccion() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = olstDtoAutorizado;
            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerAutorizadoPlaca(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoAutorizado> olstDtoAutorizado = new List<DtoAutorizado>();


            getInfoAutorizado_Request request = new getInfoAutorizado_Request();
            request.RequestId = NuevoRequestId;

            ServiceAutorizado oServiceAutorizado = new ServiceAutorizado();
            oServiceAutorizado.Placa1 = oAutorizado.PlacaAuto;


            request.oAutorizado = oServiceAutorizado;

            getInfoAutorizado_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getInfoAutorizadoPlaca(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        olstDtoAutorizado = Mapper.FromDataTransferObjects(response.olstDtoAutorizado);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "RegistrarTransaccion() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "RegistrarTransaccion() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = olstDtoAutorizado;
            return oResultadoOperacion;
        }

        public ResultadoOperacion SolucionarTodasAlarmas(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string Resultado = string.Empty;


            setSolucionarAlarmas_Request request = new setSolucionarAlarmas_Request();
            request.RequestId = NuevoRequestId;

            ServiceModulo oServiceModulo = new ServiceModulo();
            oServiceModulo.IdModulo = oModulo.IdModulo;
            oServiceModulo.IdEstacionamiento = oModulo.IdEstacionamiento;

            request.oModulo = oServiceModulo;


            setSolucionarAlarmas_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.setSolucionarAlarmas(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error conexion Modulo Service";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        Resultado = response.sResult;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Respuesta Invalida Modulo Service: setSolucionarTodasAlarmas";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error Respuesta Modulo Service: setSolucionarTodasAlarmas";
                return oResultadoOperacion;
            }


            oResultadoOperacion.EntidadDatos = Resultado;

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerTarjetas(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoTarjetas> lstDtoTarjetas = new List<DtoTarjetas>();

            getTarjetas_Request request = new getTarjetas_Request();
            request.RequestId = NuevoRequestId;

            ServiceModulo oServiceModulo = new ServiceModulo();
            oServiceModulo.IdEstacionamiento = oModulo.IdEstacionamiento;

            request.oModulo = oServiceModulo;

            getTarjetas_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getTarjetas(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        lstDtoTarjetas = Mapper.FromDataTransferObjects(response.olstDtoTarjetas);
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = lstDtoTarjetas;

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerEventoDispositivo(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            getEventosDispositivo_Request request = new getEventosDispositivo_Request();
            request.RequestId = NuevoRequestId;

            ServiceModulo oServiceModulo = new ServiceModulo();
            oServiceModulo.IdModulo = oModulo.IdModulo;

            request.oModulo = oServiceModulo;

            getEventosDispositivo_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getEventosDispositivo(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.Mensaje = response.Message;
                        oResultadoOperacion.EntidadDatos = response.oResult;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error response == null";
                return oResultadoOperacion;
            }


            return oResultadoOperacion;
        }

        public ResultadoOperacion ActualizarEventoDispositivo(long IdEventoDsipo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            bool bRegistroExitoso = false;

            getEventosDispositivo_Request request = new getEventosDispositivo_Request();
            request.RequestId = NuevoRequestId;

            request.oIdEvento = IdEventoDsipo;


            getEventosDispositivo_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.SetActualizarEventoDispositivo(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() Exception";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        bRegistroExitoso = response.bRegistro;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error request.RequestId != response.CorrelationId";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "ObtenerTarjetasModulo() error response == null";
                return oResultadoOperacion;
            }

            oResultadoOperacion.EntidadDatos = bRegistroExitoso;

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarIngresoAuto(string IDCARD)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            bool bIngreso = false;

            getValidarIngresoAutorizado_Request request = new getValidarIngresoAutorizado_Request();
            request.RequestId = NuevoRequestId;


            request.sIdCard = IDCARD;
            

            getValidarIngresoAutorizado_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getValidarIngresoAutorizado(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error conexion Modulo Service";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        bIngreso = response.bAutoIngreso;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Respuesta Invalida Modulo Service: getValidarClave";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error Respuesta Modulo Service: getValidarClave";
                return oResultadoOperacion;
            }

            oResultadoOperacion.EntidadDatos = bIngreso;

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarPlacaEntrada(string IdModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string sPlacaIngreso = string.Empty;

            getValidarPlacaEntrada_Request request = new getValidarPlacaEntrada_Request();
            request.RequestId = NuevoRequestId;


            request.sModulo = IdModulo;


            getValidarPlacaEntrada_Response response = null;

            try
            {
                SafeProxy.DoAction<EntradaServiceClient>(_MC_EntradaServices, client =>
                { response = client.getValidarPlacaEntrada(request); });
            }
            catch (System.Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error conexion Modulo Service";
                return oResultadoOperacion;
            }

            if (response != null)
            {
                if (request.RequestId == response.CorrelationId)
                {
                    if (response.Acknowledge == ServiceProxy.MC_EntradaService.AcknowledgeType.Success)
                    {
                        sPlacaIngreso = response.sPlacaRespuesta;
                    }
                    else
                    {
                        oResultadoOperacion.oEstado = TipoRespuesta.Error;
                        oResultadoOperacion.Mensaje = response.Message;
                        return oResultadoOperacion;
                    }
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Respuesta Invalida Modulo Service: getValidarClave";
                    return oResultadoOperacion;
                }
            }
            else
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error Respuesta Modulo Service: getValidarClave";
                return oResultadoOperacion;
            }

            oResultadoOperacion.EntidadDatos = sPlacaIngreso;

            return oResultadoOperacion;
        }

    }
}
