using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;

namespace Persistencia
{
    internal class Conexion
    {

       internal static string Cnn(Empleado Usu = null)
        {
            if(Usu == null)

                return "Data Source =DESKTOP-HNCLSRI\\SQLEXPRESS; Initial Catalog = SistemaVentas; Integrated Security = true";

            else
                return "Data Source =DESKTOP-HNCLSRI\\SQLEXPRESS; Initial Catalog = SistemaVentas; User="
                                       + Usu.UsuLog + "; Password='" + Usu.PassUsu + "'";

        }

    }
}
