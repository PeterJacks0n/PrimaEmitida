using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimaEmitida.Entidades.Core;
using PrimaEmitida.AccesoDatos.Core;

namespace PrimaEmitida.LogicaNegocio.Core
{
    public class DatosReferenciaLN
    {
        public List<DatosReferencia> ObtenerTodos()
        {
            try
            {
                return new DatosReferenciaDA().ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de referencia: " + ex.Message, ex);
            }
        }

        public List<DatosReferencia> ProcesarYComparar()
        {
            try
            {
                return new DatosReferenciaDA().ActualizarYComparar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar y comparar primas: " + ex.Message, ex);
            }
        }
    }
}