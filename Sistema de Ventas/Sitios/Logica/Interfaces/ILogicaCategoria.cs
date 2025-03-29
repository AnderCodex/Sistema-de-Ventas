using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;


namespace Logica
{
    public interface ILogicaCategoria
    {
        void Alta(EC.Categoria unaCat, Empleado unE);
        void Baja(EC.Categoria unaCat, Empleado unE);
        void Modificar(EC.Categoria unaCat, Empleado unE);
        List<Categoria> ListarCategoria(Empleado unE);
        Categoria BuscarCategoriaActiva(string Codigo_Cate, Empleado unE);
        
    }
}
