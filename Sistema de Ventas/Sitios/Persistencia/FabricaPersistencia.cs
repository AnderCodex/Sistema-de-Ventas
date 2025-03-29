using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class FabricaPersistencia
    {

        public static IPersistenciaEmpleado GetPersitenciaEmpleado()
        {
            return (PersistenciaEmpleado.GetInstancia());
        }

        public static IPersistenciaArticulo GetPersistenciaArticulo()
        {
            return (PersistenciaArticulo.GetInstancia());
        }

        public static IPersistenciaCategoria GetPersistenciaCategoria()
        { 
            return (PersistenciaCategoria.GetInstancia());
        }

        public static IPersistenciaClientes GetPersistenciaClientes()
        {
            return (PersistenciaCliente.GetInstancia());
        }

        public static IPersistenciaEstadoVenta GetPersistenciaEstado()
        {
            return (PersistenciaEstadoVenta.GetInstancia());
        }

        public static IPersistenciaVenta GetPersistenciaVenta()
        {
            return (PersistenciaVenta.GetInstancia());
        }


    }
}
