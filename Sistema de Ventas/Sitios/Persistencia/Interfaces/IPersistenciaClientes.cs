using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    public interface IPersistenciaClientes
    {
        void AltaCliente(Clientes unCliente, Empleado unE);
        void ModificarCliente(Clientes uncliente, Empleado unE);

        Clientes BuscarCliente(string CiCli, Empleado unE);

        List<EC.Clientes> ListarCliente(Empleado unE);
    }
}
