using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    internal class LogicaEstadoVenta :ILogicaEstadoVenta
    {
        private static LogicaEstadoVenta _instancia = null;
        private LogicaEstadoVenta() { }
        public static LogicaEstadoVenta GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaEstadoVenta();
            return _instancia;
        }

        public List<EstadoVenta> ListarEstadoVenta(Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaEstado().ListarEstadoVenta( unE));
        }

        public EstadoVenta BuscarEstadoVenta(int IdEstado, Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaEstado().BuscarEstadoVenta(IdEstado, unE));
        }

        public void AsignarEstado(Empleado unE, int unEs, Venta nroV)
        {
            FabricaPersistencia.GetPersistenciaEstado().AsignarEstado(unE, unEs, nroV);
        }
    }
}
