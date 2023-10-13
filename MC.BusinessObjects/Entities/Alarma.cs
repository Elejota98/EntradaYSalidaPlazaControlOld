using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.BusinessObjects.Entities
{
    public class Autorizado
    {
        private string _IdTarjeta;
        private string _PlacaAuto;

        public string PlacaAuto
        {
            get { return _PlacaAuto; }
            set { _PlacaAuto = value; }
        }
        
        public string IdTarjeta
        {
            get { return _IdTarjeta; }
            set { _IdTarjeta = value; }
        }

    }
}
