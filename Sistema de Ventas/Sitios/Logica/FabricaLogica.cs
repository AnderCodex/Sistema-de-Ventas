using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class FabricaLogica
    {
        public static  ILogicaArticulo GetLogicaArticulo()
        {
            return (LogicaArticulo.GetInstancia());
        }

        public static ILogicaCategoria GetLogicaCategoria()
        {
            return (LogicaCategoria.GetInstancia());
        }

        public static ILogicaCliente GetLogicaCliente()
        {
            return (LogicaCliente.GetInstancia());
        }
        public static ILogicaEmpleado GetLogicaEmpleado()
        {
            return (LogicaEmpleado.GetInstancia());
        }

        public static ILogicaEstadoVenta GetLogicaEstado()
        {
            return (LogicaEstadoVenta.GetInstancia());
        }

        public static IlogicaVenta GetIlogicaVenta()
        {
            return (LogicaVenta.GetInstancia());
        }


        
    }
}
