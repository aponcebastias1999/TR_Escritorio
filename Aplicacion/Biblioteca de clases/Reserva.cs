using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Biblioteca_de_clases
{
    public class Reserva
    {
       
        public string ID_RESER { get; set; }
        public string DEPARTAMENTO_ID_DEPTO { get; set; }
        public string CLIENTE_ID_CLIENTE { get; set; }
        public string FECHA_INICIO { get; set; }
        public string FECHA_TERMINO { get; set; }
        public string MONTO_INICIAL { get; set; }
        public string POR_PAGAR { get; set; }
        public string TOTAL_ACTIVIDADES { get; set; }
        public string TOTAL_PAGAR { get; set; }

        public string CANCELADA { get; set; }
        public string  CORREO { get; set; }

    }
}
