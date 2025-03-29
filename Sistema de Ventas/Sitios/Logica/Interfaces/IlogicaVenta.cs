using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    public interface IlogicaVenta
    {
        void Alta(EC.Venta unaV, Empleado unE);
        List<Venta> ListarVenta(Empleado unE);
       
    }
}
