using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    public interface IPersistenciaVenta
    {
        void AltaVenta(Venta unaV, Empleado unE);

       //Operacion para Seguimiento
      
        List<Venta> ListarVenta(Empleado unE);

    }
}
