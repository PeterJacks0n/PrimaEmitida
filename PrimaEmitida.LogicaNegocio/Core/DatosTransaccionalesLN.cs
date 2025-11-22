using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimaEmitida.Entidades.Core;
using PrimaEmitida.AccesoDatos.Core;

namespace PrimaEmitida.LogicaNegocio.Core
{
    public class DatosTransaccionalesLN
    {
        public bool CargarDatos(List<DatosTransaccionales> datos)
        {
            try
            {
                // Limpiar datos previos
                new DatosTransaccionalesDA().LimpiarDatos();

                // Insertar nuevos datos
                return new DatosTransaccionalesDA().InsertarMasivo(datos);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar datos transaccionales: " + ex.Message, ex);
            }
        }
    }
}