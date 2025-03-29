using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    internal class LogicaArticulo : ILogicaArticulo
    {
        private static LogicaArticulo _instancia = null;
        private LogicaArticulo() { }
        public static LogicaArticulo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaArticulo();
            return _instancia;
        }

        public void Alta(Articulo unA, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaArticulo().AltaArticulo(unA, unE);
        }

        public void Baja(Articulo unA, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaArticulo().BajaArticulo(unA,unE);
        }

        public void Modificar(Articulo unA, Empleado unE)
        {
            FabricaPersistencia.GetPersistenciaArticulo().ModificarArticulo(unA, unE);
        }

        public List<Articulo> ListarArticulo(Empleado unE)
        {
            return (FabricaPersistencia.GetPersistenciaArticulo().ListarArticulo(unE));
        }

       

        public Articulo BuscarArticuloActivo(string codArt, Empleado unE)


        {
            return (FabricaPersistencia.GetPersistenciaArticulo().BuscarArticuloActivo(codArt, unE));
        }

    }
}
