using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    public interface ILogicaArticulo
    {
        void Alta(Articulo unArt, Empleado unE);
        void Baja(Articulo unA, Empleado unE);
        void Modificar(Articulo unA, Empleado unE);

        Articulo BuscarArticuloActivo(string CodigoArt, Empleado unE);

        List<Articulo> ListarArticulo(Empleado unE);
    }
}
