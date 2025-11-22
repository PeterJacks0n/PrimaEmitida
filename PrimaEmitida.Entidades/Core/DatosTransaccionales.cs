using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PrimaEmitida.Entidades.Core
{
    [DataContract]
    [Serializable]
    public class DatosTransaccionales
    {
        [DataMember]
        public int IdTransaccion { get; set; }

        [DataMember]
        public string PolTxtPresentacion { get; set; }

        [DataMember]
        public decimal PolNumPrima { get; set; }

        [DataMember]
        public DateTime FechaRegistro { get; set; }
    }
}