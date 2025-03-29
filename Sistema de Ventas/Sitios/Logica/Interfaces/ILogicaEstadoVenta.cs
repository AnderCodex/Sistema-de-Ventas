using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    public interface ILogicaEstadoVenta
    {
       EstadoVenta  BuscarEstadoVenta(int IdEstado, Empleado unE);

        List<EstadoVenta> ListarEstadoVenta(Empleado unE);

        void AsignarEstado(Empleado unE, int unEs, Venta nroV);
    }
}
