﻿using MC.BaseService.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MC.SalidaService.Messages
{
    [DataContract(Namespace = "http://www.MillensCorp.com/types/")]
    public class getObtenerDatosPagos_Request : RequestBase
    {
        [DataMember]
        public long sIdTransaccion;
    }
}
