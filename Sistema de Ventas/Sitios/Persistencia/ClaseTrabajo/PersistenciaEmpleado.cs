using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using EC;

namespace Persistencia
{
    internal class PersistenciaEmpleado: IPersistenciaEmpleado
    {

        //Aplicamos Singleton
        private static PersistenciaEmpleado _instancia = null;

        private PersistenciaEmpleado() { }


        public static PersistenciaEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEmpleado();
            return _instancia;

        }


     

        public Empleado Logueo(string UsuLog, string PassUsu)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn());
            Empleado _unEmpleado = null;

            SqlCommand _comando = new SqlCommand("LogueoEmpleado", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuLog", UsuLog);
            _comando.Parameters.AddWithValue("@PassUsu", PassUsu);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unEmpleado = new Empleado(
                        (string)_lector["UsuLog"], 
                        (string)_lector["Nombre"], 
                        (string)_lector["PassUsu"]);
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                _cnn.Close();
            }
            return _unEmpleado;


        }


        internal Empleado BuscarEmp(string unE, Empleado Emp)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(Emp));
            Empleado _unE = null;

            SqlCommand _comando = new SqlCommand("BuscarEmp", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuLog", unE);


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    string usuLog = (string)_lector["UsuLog"];
                    string nombre = (string)_lector["Nombre"];
                    string passUsu = (string)_lector["PassUsu"];


                    _unE = new Empleado(usuLog, nombre, passUsu);
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                _cnn.Close();
            }

            return _unE;
        }

    }
}
