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
    public class DatosReferencia
    {
        [DataMember]
        public int IdReferencia { get; set; }

        [DataMember]
        public string IdPeriodo { get; set; }

        [DataMember]
        public string Presentacion { get; set; }

        [DataMember]
        public decimal? PrimaEmitida { get; set; }

        [DataMember]
        public decimal PrimaContabilidad { get; set; }

        [DataMember]
        public decimal? Diferencia { get; set; }
    }
}
