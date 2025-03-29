using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC;
using Persistencia;

namespace Logica
{
    public interface ILogicaEmpleado
    {
        Empleado Logueo(string U, string P);

        //Empleado Buscar(string UsuLog);
    }
}
