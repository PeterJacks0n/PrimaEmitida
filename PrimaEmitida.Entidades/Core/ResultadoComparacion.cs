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
    public class ResultadoComparacion
    {
        [DataMember]
        public string Presentacion { get; set; }

        [DataMember]
        public decimal PrimaEmitidaCalculada { get; set; }

        [DataMember]
        public decimal PrimaContabilidad { get; set; }

        [DataMember]
        public decimal Diferencia { get; set; }

        [DataMember]
        public bool EsCorrecto => Diferencia == 0;
    }
}
