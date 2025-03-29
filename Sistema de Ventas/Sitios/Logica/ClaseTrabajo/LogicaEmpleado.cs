using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using Persistencia;

namespace Logica
{
    internal class LogicaEmpleado: ILogicaEmpleado
    {
        private static LogicaEmpleado _instancia = null;
        private LogicaEmpleado() { }
        public static LogicaEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaEmpleado();
            return _instancia;
        }

       
        public Empleado Logueo(string U, string P)
        {
            return Persistencia.FabricaPersistencia.GetPersitenciaEmpleado().Logueo(U, P);
        }


       
    }
}
