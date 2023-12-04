using MC.BusinessService.DataTransferObject;
using MC.BusinessService.Entities;
using MC.BusinessService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.DataService
{
    public partial class DataService : IDataService
    {
        public ResultadoOperacion ObtenerInfoPartes(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoParteModulo> olstDtoParteModulo = new List<DtoParteModulo>();

            DataSetTransacciones.P_ListarInfoPartesDataTable _InfoPartesTable = new DataSetTransacciones.P_ListarInfoPartesDataTable();
            DataSetTransaccionesTableAdapters.P_ListarInfoPartesTableAdapter _InfoPartesAdapter = new DataSetTransaccionesTableAdapters.P_ListarInfoPartesTableAdapter();

            try
            {
                _InfoPartesTable.Constraints.Clear();

                if (_InfoPartesAdapter.Fill(_InfoPartesTable, oModulo.IdModulo, oModulo.IdEstacionamiento) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Partes OK";

                    for (int i = 0; i < _InfoPartesTable.Rows.Count; i++)
                    {
                        DtoParteModulo oDtoParteModulo = new DtoParteModulo();

                        oDtoParteModulo.IdParte = Convert.ToInt64(_InfoPartesTable.Rows[i][0]);
                        oDtoParteModulo.IdModulo = _InfoPartesTable.Rows[i][1].ToString();
                        oDtoParteModulo.IdEstacionamiento = Convert.ToInt64(_InfoPartesTable.Rows[i][2]);
                        oDtoParteModulo.TipoParte = _InfoPartesTable.Rows[i][3].ToString();
                        oDtoParteModulo.NombreParte = _InfoPartesTable.Rows[i][4].ToString();

                        string Denominacion = _InfoPartesTable.Rows[i][5].ToString();
                        string CantidadMin = _InfoPartesTable.Rows[i][6].ToString();
                        string CantidadMax = _InfoPartesTable.Rows[i][7].ToString();

                        if (Denominacion != string.Empty)
                        {
                            oDtoParteModulo.Denominacion = Convert.ToDouble(Denominacion);
                        }
                        else 
                        {
                            oDtoParteModulo.Denominacion = 0;
                        }
                        if (CantidadMin != string.Empty)
                        {
                            oDtoParteModulo.CantidadMin = Convert.ToDouble(CantidadMin);
                        }
                        else 
                        {
                            oDtoParteModulo.CantidadMin = 0;
                        }
                        if (CantidadMax != string.Empty)
                        {
                            oDtoParteModulo.CantidadMax = Convert.ToDouble(CantidadMax);
                        }
                        else 
                        {
                            oDtoParteModulo.CantidadMax = 0;
                        }
                        
                        oDtoParteModulo.IPDispositivo = _InfoPartesTable.Rows[i][8].ToString();


                        string CantidadAlarma = _InfoPartesTable.Rows[i][9].ToString();
                        string DineroActual = _InfoPartesTable.Rows[i][10].ToString();
                        string CantidadActual = _InfoPartesTable.Rows[i][11].ToString();

                        if (CantidadAlarma != string.Empty)
                        {
                            oDtoParteModulo.CantidadAlarma = Convert.ToDouble(CantidadAlarma);
                        }
                        else 
                        {
                            oDtoParteModulo.CantidadAlarma = 0;
                        }

                        if (DineroActual != string.Empty)
                        {
                            oDtoParteModulo.DineroActual = Convert.ToDouble(DineroActual);
                        }
                        else 
                        {
                            oDtoParteModulo.DineroActual = 0;
                        }

                        if (CantidadActual != string.Empty)
                        {
                            oDtoParteModulo.CantidadActual = Convert.ToDouble(CantidadActual);
                        }
                        else 
                        {
                            oDtoParteModulo.CantidadActual = 0;
                        }
                        
                        
                        oDtoParteModulo.Prioritario = Convert.ToBoolean(_InfoPartesTable.Rows[i][12]);
                        oDtoParteModulo.Estado = Convert.ToBoolean(_InfoPartesTable.Rows[i][13]);

                        olstDtoParteModulo.Add(oDtoParteModulo);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoParteModulo;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin partes en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }
        
        public ResultadoOperacion RegistrarTransaccion(Transaccion oTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DataSetTransacciones.P_RegistrarIngresoDataTable _TransaccionTable = new DataSetTransacciones.P_RegistrarIngresoDataTable();
            DataSetTransaccionesTableAdapters.P_RegistrarIngresoTableAdapter _TransaccionAdapter = new DataSetTransaccionesTableAdapters.P_RegistrarIngresoTableAdapter();

            try
            {
                _TransaccionTable.Constraints.Clear();

                if (_TransaccionAdapter.Fill(_TransaccionTable, oTransaccion.IdTransaccion, oTransaccion.CarrilEntrada, oTransaccion.ModuloEntrada, oTransaccion.IdEstacionamiento,oTransaccion.IdAutorizado, oTransaccion.IdTarjeta, oTransaccion.PlacaEntrada,oTransaccion.IdTipoVehiculo) > 0)
                {
                    for (int i = 0; i < _TransaccionTable.Rows.Count; i++)
                    {
                        
                        long code = Convert.ToInt64(_TransaccionTable.Rows[i][0].ToString());
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.EntidadDatos = code;                       
                    }
                }
            }
            catch (Exception ex)
            {
                //Generar LOG DataBase Exception
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            return oResultadoOperacion;
        }        

        public ResultadoOperacion RegistrarAlarma(Alarma oAlarma)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            int idcajero = 0;

            DataSetTransacciones.P_RegistrarAlarmaDataTable _CrearAlarmaTable = new DataSetTransacciones.P_RegistrarAlarmaDataTable();
            DataSetTransaccionesTableAdapters.P_RegistrarAlarmaTableAdapter _CrearAlarmaAdapter = new DataSetTransaccionesTableAdapters.P_RegistrarAlarmaTableAdapter();

            try
            {
                _CrearAlarmaTable.Constraints.Clear();

                if (_CrearAlarmaAdapter.Fill(_CrearAlarmaTable,oAlarma.IdEstacionamiento,oAlarma.NivelError,oAlarma.TipoError,oAlarma.Parte,oAlarma.Descripcion,oAlarma.IdModulo) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Registro Alarma ok";

                    oResultadoOperacion.EntidadDatos = idcajero;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Usuario no registrado en base de datos";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerInfoModulo(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DtoModulo oDtoModulo = new DtoModulo();

            DataSetTransacciones.P_ObtenerInfoModuloDataTable _InfoModuloTable = new DataSetTransacciones.P_ObtenerInfoModuloDataTable();
            DataSetTransaccionesTableAdapters.P_ObtenerInfoModuloTableAdapter _InfoModuloAdapter = new DataSetTransaccionesTableAdapters.P_ObtenerInfoModuloTableAdapter();

            try
            {
                _InfoModuloTable.Constraints.Clear();

                if (_InfoModuloAdapter.Fill(_InfoModuloTable, oModulo.IdModulo) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Modulo OK";

                    for (int i = 0; i < _InfoModuloTable.Rows.Count; i++)
                    {
                        oDtoModulo.IdModulo = _InfoModuloTable.Rows[i][0].ToString();
                        oDtoModulo.IdEstacionamiento = Convert.ToInt64(_InfoModuloTable.Rows[i][1]);
                        oDtoModulo.NombreEstacionamiento = _InfoModuloTable.Rows[i][2].ToString();
                        oDtoModulo.IP = _InfoModuloTable.Rows[i][3].ToString();
                        oDtoModulo.Carril = Convert.ToInt32(_InfoModuloTable.Rows[i][5]);
                        oDtoModulo.IdTipoModulo = Convert.ToInt32(_InfoModuloTable.Rows[i][6]);
                        oDtoModulo.Extension = _InfoModuloTable.Rows[i][8].ToString();
                        oDtoModulo.Estado = Convert.ToBoolean(_InfoModuloTable.Rows[i][9]);                        
                        oDtoModulo.EstadoEstacionamiento = Convert.ToBoolean(_InfoModuloTable.Rows[i][10]);
                        oDtoModulo.NombreSede = _InfoModuloTable.Rows[i][11].ToString();
                        oDtoModulo.Ciudad = _InfoModuloTable.Rows[i][12].ToString();
                        oDtoModulo.EstadoSede = Convert.ToBoolean(_InfoModuloTable.Rows[i][13]);
                    }

                    oResultadoOperacion.EntidadDatos = oDtoModulo;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerParametros(long IdEstacionamiento)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoParametros> olstDtoParametros = new List<DtoParametros>();

            DataSetTransacciones.P_ObtenerParametrosDataTable _InfoParametrosTable = new DataSetTransacciones.P_ObtenerParametrosDataTable();
            DataSetTransaccionesTableAdapters.P_ObtenerParametrosTableAdapter _InfoParametrosAdapter = new DataSetTransaccionesTableAdapters.P_ObtenerParametrosTableAdapter();

            try
            {
                _InfoParametrosTable.Constraints.Clear();

                if (_InfoParametrosAdapter.Fill(_InfoParametrosTable, IdEstacionamiento) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info parametros OK";

                    for (int i = 0; i < _InfoParametrosTable.Rows.Count; i++)
                    {
                        DtoParametros oDtoParametros = new DtoParametros();

                        oDtoParametros.IdEstacionamiento = Convert.ToInt64(_InfoParametrosTable.Rows[i][0]);
                        oDtoParametros.Codigo = _InfoParametrosTable.Rows[i][1].ToString();
                        oDtoParametros.Valor = _InfoParametrosTable.Rows[i][2].ToString();
                        oDtoParametros.Descripcion = _InfoParametrosTable.Rows[i][3].ToString();
                        oDtoParametros.Estado = Convert.ToBoolean(_InfoParametrosTable.Rows[i][4]);
                        olstDtoParametros.Add(oDtoParametros);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoParametros;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error al consultar registros en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerInfoAutorizado(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoAutorizado> olstDtoAutorizado = new List<DtoAutorizado>();

            DataSetTransacciones.P_ValidarAutorizadoDataTable _InfoAutorizadoTable = new DataSetTransacciones.P_ValidarAutorizadoDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarAutorizadoTableAdapter _InfoAutorizadoAdapter = new DataSetTransaccionesTableAdapters.P_ValidarAutorizadoTableAdapter();

            try
            {
                _InfoAutorizadoTable.Constraints.Clear();

                if (_InfoAutorizadoAdapter.Fill(_InfoAutorizadoTable, oAutorizado.IdTarjeta,oAutorizado.Placa1) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Autorizado OK";

                    for (int i = 0; i < _InfoAutorizadoTable.Rows.Count; i++)
                    {
                        DtoAutorizado oDtoAutorizado = new DtoAutorizado();

                        oDtoAutorizado.Documento = _InfoAutorizadoTable.Rows[i][0].ToString();
                        oDtoAutorizado.IdAutorizacion = Convert.ToInt64(_InfoAutorizadoTable.Rows[i][1]);
                        oDtoAutorizado.Estado = Convert.ToBoolean(_InfoAutorizadoTable.Rows[i][2]);
                        oDtoAutorizado.EstadoAutorizacion = Convert.ToBoolean(_InfoAutorizadoTable.Rows[i][3]);
                        oDtoAutorizado.IdTarjeta = _InfoAutorizadoTable.Rows[i][4].ToString();
                        oDtoAutorizado.NombresAutorizado = _InfoAutorizadoTable.Rows[i][5].ToString();
                        oDtoAutorizado.FechaInicial = Convert.ToDateTime(_InfoAutorizadoTable.Rows[i][6]);
                        oDtoAutorizado.FechaFinal = Convert.ToDateTime(_InfoAutorizadoTable.Rows[i][7]);
                        oDtoAutorizado.IdEstacionamiento = Convert.ToInt64(_InfoAutorizadoTable.Rows[i][8]);

                        olstDtoAutorizado.Add(oDtoAutorizado);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoAutorizado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerInfoAutorizadoPlacaS(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoAutorizado> olstDtoAutorizado = new List<DtoAutorizado>();

            DataSetTransacciones.P_ValidarAutorizadoPlacaDataTable _InfoAutorizadoTable = new DataSetTransacciones.P_ValidarAutorizadoPlacaDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarAutorizadoPlacaTableAdapter _InfoAutorizadoAdapter = new DataSetTransaccionesTableAdapters.P_ValidarAutorizadoPlacaTableAdapter();

            try
            {
                _InfoAutorizadoTable.Constraints.Clear();

                if (_InfoAutorizadoAdapter.Fill(_InfoAutorizadoTable, oAutorizado.Placa1) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Autorizado OK";

                    for (int i = 0; i < _InfoAutorizadoTable.Rows.Count; i++)
                    {
                        DtoAutorizado oDtoAutorizado = new DtoAutorizado();

                        oDtoAutorizado.IdTarjeta = _InfoAutorizadoTable.Rows[i][0].ToString();

                        olstDtoAutorizado.Add(oDtoAutorizado);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoAutorizado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerInfoAutorizadoPlaca(Autorizado oAutorizado)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoAutorizado> olstDtoAutorizado = new List<DtoAutorizado>();

            DataSetTransacciones.P_ValidarAutorizadoPlacaDataTable _InfoAutorizadoTable = new DataSetTransacciones.P_ValidarAutorizadoPlacaDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarAutorizadoPlacaTableAdapter _InfoAutorizadoAdapter = new DataSetTransaccionesTableAdapters.P_ValidarAutorizadoPlacaTableAdapter();

            try
            {
                _InfoAutorizadoTable.Constraints.Clear();

                if (_InfoAutorizadoAdapter.Fill(_InfoAutorizadoTable, oAutorizado.Placa1) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Autorizado OK";

                    for (int i = 0; i < _InfoAutorizadoTable.Rows.Count; i++)
                    {
                        DtoAutorizado oDtoAutorizado = new DtoAutorizado();

                        oDtoAutorizado.IdTarjeta = _InfoAutorizadoTable.Rows[i][0].ToString();

                        olstDtoAutorizado.Add(oDtoAutorizado);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoAutorizado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion SolucionarTodasAlarmas(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string Resultado = string.Empty;

            DataSetTransacciones.P_SolucionarAlarmasModuloDataTable _SolucionarTodasAlarmasTable = new DataSetTransacciones.P_SolucionarAlarmasModuloDataTable();
            DataSetTransaccionesTableAdapters.P_SolucionarAlarmasModuloTableAdapter _SolucionarTodasAlarmasAdapter = new DataSetTransaccionesTableAdapters.P_SolucionarAlarmasModuloTableAdapter();

            try
            {
                _SolucionarTodasAlarmasTable.Constraints.Clear();

                if (_SolucionarTodasAlarmasAdapter.Fill(_SolucionarTodasAlarmasTable, oModulo.IdModulo,oModulo.IdEstacionamiento) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Solucion Alarmas OK";

                    oResultadoOperacion.EntidadDatos = Resultado;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "error al ejecutar el proceso en base de datos";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerTarjetas(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            List<DtoTarjetas> olstDtoTarjetas = new List<DtoTarjetas>();

            DataSetTransacciones.P_ListarTarjetasDataTable _InfoTarjetasTable = new DataSetTransacciones.P_ListarTarjetasDataTable();
            DataSetTransaccionesTableAdapters.P_ListarTarjetasTableAdapter _InfoTarjetasAdapter = new DataSetTransaccionesTableAdapters.P_ListarTarjetasTableAdapter();

            try
            {
                _InfoTarjetasTable.Constraints.Clear();

                if (_InfoTarjetasAdapter.Fill(_InfoTarjetasTable, oModulo.IdEstacionamiento) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Tarjetas OK";

                    for (int i = 0; i < _InfoTarjetasTable.Rows.Count; i++)
                    {
                        DtoTarjetas oDtoTarjetas = new DtoTarjetas();

                        oDtoTarjetas.IdEstacionamiento = Convert.ToInt64(_InfoTarjetasTable.Rows[i][0]);
                        oDtoTarjetas.IdTarjeta = _InfoTarjetasTable.Rows[i][1].ToString();
                        oDtoTarjetas.Estado = Convert.ToBoolean(_InfoTarjetasTable.Rows[i][4]);

                        olstDtoTarjetas.Add(oDtoTarjetas);
                    }

                    oResultadoOperacion.ListaEntidadDatos = olstDtoTarjetas;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin tarjetas en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion RegistrarTransaccionSalida(Transaccion oTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DataSetTransacciones.P_RegistrarSalidaDataTable _TransaccionTable = new DataSetTransacciones.P_RegistrarSalidaDataTable();
            DataSetTransaccionesTableAdapters.P_RegistrarSalidaTableAdapter _TransaccionAdapter = new DataSetTransaccionesTableAdapters.P_RegistrarSalidaTableAdapter();

            try
            {
                _TransaccionTable.Constraints.Clear();

                if (_TransaccionAdapter.Fill(_TransaccionTable, oTransaccion.IdTransaccion, oTransaccion.CarrilSalida, oTransaccion.ModuloSalida, oTransaccion.IdEstacionamiento, oTransaccion.IdTarjeta, oTransaccion.PlacaSalida) > 0)
                {
                    for (int i = 0; i < _TransaccionTable.Rows.Count; i++)
                    {

                        long code = Convert.ToInt64(_TransaccionTable.Rows[i][0].ToString());
                        oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                        oResultadoOperacion.EntidadDatos = code;
                    }
                }
            }
            catch (Exception ex)
            {
                //Generar LOG DataBase Exception
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.ToString();
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerEvento(long Secuencia)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            long lSecuencia = Convert.ToInt64(Secuencia);
            long IdEvento = 0;
            int Horas = 0;

            DtoTransaccion oDtoTransaccion = new DtoTransaccion();

            DataSetTransacciones.P_ObtenerDatosEventoDataTable _InfoEventoTable = new DataSetTransacciones.P_ObtenerDatosEventoDataTable();
            DataSetTransaccionesTableAdapters.P_ObtenerDatosEventoTableAdapter _InfoEventoAdapter = new DataSetTransaccionesTableAdapters.P_ObtenerDatosEventoTableAdapter();

            try
            {
                _InfoEventoTable.Constraints.Clear();

                if (_InfoEventoAdapter.Fill(_InfoEventoTable, lSecuencia) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info transaccion OK";

                    for (int i = 0; i < _InfoEventoTable.Rows.Count; i++)
                    {
                        IdEvento = Convert.ToInt64(_InfoEventoTable.Rows[i][3]);
                        Horas = Convert.ToInt32(_InfoEventoTable.Rows[i][6]);
                    }

                    oResultadoOperacion.EntidadDatos = IdEvento + ";" + Horas;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerEventoDisposito(Modulo oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string Modulo = string.Empty;
            long IdEvento = 0;

            DataSetTransacciones.P_ObtenerEventoDispositivoDataTable _InfoEventoTable = new DataSetTransacciones.P_ObtenerEventoDispositivoDataTable();
            DataSetTransaccionesTableAdapters.P_ObtenerEventoDispositivoTableAdapter _InfoEventoAdapter = new DataSetTransaccionesTableAdapters.P_ObtenerEventoDispositivoTableAdapter();

            try
            {
                _InfoEventoTable.Constraints.Clear();

                if (_InfoEventoAdapter.Fill(_InfoEventoTable, oModulo.IdModulo) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info transaccion OK";

                    for (int i = 0; i < _InfoEventoTable.Rows.Count; i++)
                    {
                        Modulo = _InfoEventoTable.Rows[i][0].ToString();
                        IdEvento = Convert.ToInt64(_InfoEventoTable.Rows[i][1]);
                        
                    }

                    oResultadoOperacion.EntidadDatos = Modulo + ";" + IdEvento;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ActualizarEvento(long IdEvento)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string resultado = string.Empty;

            DataSetTransacciones.P_ActualizarEventoDispositivoDataTable _InfoEventoTable = new DataSetTransacciones.P_ActualizarEventoDispositivoDataTable();
            DataSetTransaccionesTableAdapters.P_ActualizarEventoDispositivoTableAdapter _InfoEventoAdapter = new DataSetTransaccionesTableAdapters.P_ActualizarEventoDispositivoTableAdapter();

            try
            {
                _InfoEventoTable.Constraints.Clear();

                if (_InfoEventoAdapter.Fill(_InfoEventoTable,IdEvento) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Registro transaccion OK";

                    for (int i = 0; i < _InfoEventoTable.Rows.Count; i++)
                    {
                        resultado = _InfoEventoTable.Rows[i][0].ToString();
                    }

                    oResultadoOperacion.EntidadDatos = resultado;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarIngresoAuto(string IDCard)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DataSetTransacciones.P_ValidarIngresoAutorizadoDataTable _ValidarIngresoAutoTable = new DataSetTransacciones.P_ValidarIngresoAutorizadoDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarIngresoAutorizadoTableAdapter _ValidarIngresoAutoAdapter = new DataSetTransaccionesTableAdapters.P_ValidarIngresoAutorizadoTableAdapter();
            
            
            string resultado = string.Empty;

            try
            {
                _ValidarIngresoAutoTable.Constraints.Clear();

                if (_ValidarIngresoAutoAdapter.Fill(_ValidarIngresoAutoTable, IDCard) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Partes OK";

                    for (int i = 0; i < _ValidarIngresoAutoTable.Rows.Count; i++)
                    {
                        resultado = _ValidarIngresoAutoTable.Rows[i][0].ToString();
                    }

                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.Mensaje = "Usuario no registrado en base de datos";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                string exMessage = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarIngreso(long idTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DataSetTransacciones.P_ValidarIngresoDataTable _ValidarIngresoTable = new DataSetTransacciones.P_ValidarIngresoDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarIngresoTableAdapter _ValidarIngresoAdapter = new DataSetTransaccionesTableAdapters.P_ValidarIngresoTableAdapter();


            string resultado = string.Empty;

            try
            {
                _ValidarIngresoTable.Constraints.Clear();

                if (_ValidarIngresoAdapter.Fill(_ValidarIngresoTable, idTransaccion) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Partes OK";

                    for (int i = 0; i < _ValidarIngresoTable.Rows.Count; i++)
                    {
                        resultado = _ValidarIngresoTable.Rows[i][0].ToString();
                    }

                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.Mensaje = "Usuario no registrado en base de datos";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                string exMessage = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarCortesia(long idTransaccion)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            DataSetTransacciones.P_ValidarCortesiaDataTable _ValidarCortesiaTable = new DataSetTransacciones.P_ValidarCortesiaDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarCortesiaTableAdapter _ValidarCortesiaAdapter = new DataSetTransaccionesTableAdapters.P_ValidarCortesiaTableAdapter();


            string resultado = string.Empty;

            try
            {
                _ValidarCortesiaTable.Constraints.Clear();

                if (_ValidarCortesiaAdapter.Fill(_ValidarCortesiaTable, idTransaccion) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info Partes OK";

                    for (int i = 0; i < _ValidarCortesiaTable.Rows.Count; i++)
                    {
                        resultado = _ValidarCortesiaTable.Rows[i][0].ToString();
                    }

                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.EntidadDatos = resultado;
                    oResultadoOperacion.Mensaje = "Usuario no registrado en base de datos";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                string exMessage = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }


        public ResultadoOperacion ValidarPlacaSalida(string oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string PlacaSalida = string.Empty;

            DataSetTransacciones.P_ValidarPlacaSalidaDataTable _InfoPlacaSalidaTable = new DataSetTransacciones.P_ValidarPlacaSalidaDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarPlacaSalidaTableAdapter _InfoPlacaSalidaAdapter = new DataSetTransaccionesTableAdapters.P_ValidarPlacaSalidaTableAdapter();

            try
            {
                _InfoPlacaSalidaTable.Constraints.Clear();

                if (_InfoPlacaSalidaAdapter.Fill(_InfoPlacaSalidaTable, oModulo) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info parametros OK";

                    for (int i = 0; i < _InfoPlacaSalidaTable.Rows.Count; i++)
                    {
                        PlacaSalida = Convert.ToString(_InfoPlacaSalidaTable.Rows[i][0]);

                    }

                    oResultadoOperacion.EntidadDatos = PlacaSalida;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error al consultar registros en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ValidarPlacaEntrada(string oModulo)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            string PlacaEntrada = string.Empty;

            DataSetTransacciones.P_ValidarPlacaEntradaDataTable _InfoPlacaEntradaTable = new DataSetTransacciones.P_ValidarPlacaEntradaDataTable();
            DataSetTransaccionesTableAdapters.P_ValidarPlacaEntradaTableAdapter _InfoPlacaEntradaAdapter = new DataSetTransaccionesTableAdapters.P_ValidarPlacaEntradaTableAdapter();

            try
            {
                _InfoPlacaEntradaTable.Constraints.Clear();

                if (_InfoPlacaEntradaAdapter.Fill(_InfoPlacaEntradaTable, oModulo) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info parametros OK";

                    for (int i = 0; i < _InfoPlacaEntradaTable.Rows.Count; i++)
                    {
                        PlacaEntrada = Convert.ToString(_InfoPlacaEntradaTable.Rows[i][0]);

                    }

                    oResultadoOperacion.EntidadDatos = PlacaEntrada;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Error al consultar registros en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

        public ResultadoOperacion ObtenerDatosPago(string Secuencia)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            long lSecuencia = Convert.ToInt64(Secuencia);
            string sFechaPago = string.Empty;

            DataSetLiquidacion.P_ObtenerDatosPagoDataTable _InfoTransaccionTable = new DataSetLiquidacion.P_ObtenerDatosPagoDataTable();
            DataSetLiquidacionTableAdapters.P_ObtenerDatosPagoTableAdapter _InfoTransaccionAdapter = new DataSetLiquidacionTableAdapters.P_ObtenerDatosPagoTableAdapter();

            try
            {
                _InfoTransaccionTable.Constraints.Clear();

                if (_InfoTransaccionAdapter.Fill(_InfoTransaccionTable, lSecuencia) > 0)
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                    oResultadoOperacion.Mensaje = "Info pago OK";

                    for (int i = 0; i < _InfoTransaccionTable.Rows.Count; i++)
                    {
                        sFechaPago = _InfoTransaccionTable.Rows[i][0].ToString();
                    }

                    oResultadoOperacion.EntidadDatos = sFechaPago;
                    oResultadoOperacion.oEstado = TipoRespuesta.Exito;
                }
                else
                {
                    oResultadoOperacion.oEstado = TipoRespuesta.Error;
                    oResultadoOperacion.Mensaje = "Módulo sin registro en base de datos.";
                }
            }
            catch (Exception ex)
            {
                // Generar LOG DataBase Exception
                oResultadoOperacion.Mensaje = ex.ToString();
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
            }

            return oResultadoOperacion;
        }

    }
}
