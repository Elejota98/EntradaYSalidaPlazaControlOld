﻿using MC.BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ModuloSalida.WinForm.Model
{
    public partial interface IModel
    {
        ResultadoOperacion ObtenerPartesModulo(Modulo oModulo);
        ResultadoOperacion RegistrarMovimiento(Transaccion oTransaccion);
        ResultadoOperacion RegistrarAlarma(Alarma oAlarma);
        ResultadoOperacion ObtenerInfoModulo(Modulo oModulo);
        ResultadoOperacion ObtenerParametrosModulo(long IdEstacionamiento);
        ResultadoOperacion ObtenerAutorizado(Autorizado oAutorizado);
        ResultadoOperacion SolucionarTodasAlarmas(Modulo oModulo);
        ResultadoOperacion ObtenerTarjetas(Modulo oModulo);
        ResultadoOperacion ObtenerEvento(long Secuencia);
        ResultadoOperacion ObtenerEventoDispo(Modulo oModulo);
        ResultadoOperacion ActualizarEventoDispositivo(long IdEvento);
        ResultadoOperacion ValidarSalidaAuto(string IDCARD);
        ResultadoOperacion ValidarPlacaSalida(string IdModulo);
        ResultadoOperacion ObtenerAutorizadoPlaca(Autorizado oAutorizado);
        ResultadoOperacion ObtenerDatosPagosSalida(string idTransaccion);
        ResultadoOperacion ValidarSalida(long idTransaccion);
        ResultadoOperacion ValidarCortesia(long idTransaccion);
    }
}
