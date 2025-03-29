using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;


namespace Logica
{
    public interface ILogicaCliente
    {
        void Alta(EC.Clientes unCliente, Empleado unE);
        void Modificar(EC.Clientes uncliente, Empleado unE);

        Clientes BuscarCliente(string CiCli, Empleado unE);

        List<EC.Clientes> ListarCliente(Empleado unE);
    }
}
