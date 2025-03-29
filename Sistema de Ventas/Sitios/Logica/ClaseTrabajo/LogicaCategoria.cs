using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;
using Persistencia;


namespace Logica
{
    internal class LogicaCategoria : ILogicaCategoria
    {
        private static LogicaCategoria _instancia = null;
        private LogicaCategoria() { }
        public static LogicaCategoria GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCategoria();
            return _instancia;
        }
        public void Alta(Categoria unaCat, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaCategoria().AltaCategoria(unaCat,unE);
        }

        public void Baja(Categoria unaCat, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaCategoria().BajaCategoria(unaCat, unE);
        }

        public void Modificar(Categoria unaC, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaCategoria().ModificarCategoria(unaC,unE);
        }

        public List<Categoria> Listar(Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaCategoria().ListarCategoria(unE));
        }


        public Categoria BuscarCategoriaActiva(string unC, Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaCategoria().BuscarCategoriaActiva(unC, unE));
        }

        public List<Categoria> ListarCategoria(Empleado unE)
        {
            return Persistencia.FabricaPersistencia.GetPersistenciaCategoria().ListarCategoria(unE);
        }

    }
}
