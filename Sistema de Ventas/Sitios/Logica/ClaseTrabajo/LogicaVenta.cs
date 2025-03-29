using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    internal class LogicaVenta :IlogicaVenta
    {
        private static LogicaVenta _instancia = null;
        private LogicaVenta() { }
        public static LogicaVenta GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaVenta();
            return _instancia;
        }

        public void Alta(Venta unaV, Empleado unE)
        {
           
            FabricaPersistencia.GetPersistenciaVenta().AltaVenta(unaV, unE);
        }

        public List<Venta> ListarVenta(Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaVenta().ListarVenta(unE));
        }

      

    }

}
