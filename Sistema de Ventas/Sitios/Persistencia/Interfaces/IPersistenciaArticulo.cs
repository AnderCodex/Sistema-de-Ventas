using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    public interface IPersistenciaArticulo
    {
        void AltaArticulo(Articulo unArt, Empleado unE);
        void BajaArticulo(Articulo unA, Empleado unE);
        void ModificarArticulo(Articulo unA, Empleado unE);
        Articulo BuscarArticuloActivo(string CodigoArt, Empleado unE);
        List<Articulo> ListarArticulo(Empleado unE);
    }
}
