using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    internal class LogicaCliente : ILogicaCliente
    {

        private static LogicaCliente _instancia = null;
        private LogicaCliente() { }
        public static LogicaCliente GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCliente();
            return _instancia;
        }
        public void Alta(Clientes unC, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaClientes().AltaCliente(unC,unE);
        }

        public void Modificar(Clientes unC, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaClientes().ModificarCliente(unC,unE);
        }

        public List<Clientes> ListarCliente(Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaClientes().ListarCliente(unE));
        }

        public Clientes BuscarCliente(string unCli, Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaClientes().BuscarCliente(unCli, unE));
        }
    }
}
