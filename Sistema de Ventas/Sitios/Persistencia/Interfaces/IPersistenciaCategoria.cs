using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    public interface IPersistenciaCategoria
    {
        void AltaCategoria(EC.Categoria unaCat, Empleado unE);
        void BajaCategoria(EC.Categoria unaCat, Empleado unE);
        void ModificarCategoria(EC.Categoria unaCat, Empleado unE);
        List<Categoria> ListarCategoria(Empleado unE);
        Categoria BuscarCategoriaActiva(string Codigo_Cate, Empleado unE);
        

    }
}
