using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    public interface IPersistenciaEstadoVenta
    {
        
        List<EstadoVenta> ListarEstadoVenta(Empleado unE);
        EstadoVenta BuscarEstadoVenta(int IdEstado, Empleado unE);

        void AsignarEstado(Empleado unE, int unEs, Venta nroV); 

    }
}
