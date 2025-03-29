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
    internal class PersistenciaCliente : IPersistenciaClientes
    {
        private static PersistenciaCliente _instancia = null;

        private PersistenciaCliente() { }

        public static PersistenciaCliente GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCliente();
            return _instancia;

        }

        public void AltaCliente( Clientes unC, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));

            SqlCommand _comando = new SqlCommand("AltaCliente", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            //Parametros de entrada
            _comando.Parameters.AddWithValue("@CiCli", unC.CiCli);
            _comando.Parameters.AddWithValue("@Nombre", unC.NombreCliente);
            _comando.Parameters.AddWithValue("@NumTarj", unC.NumTarj);
            _comando.Parameters.AddWithValue("@Telefono", unC.Telefono);



            //retorno
            SqlParameter _pRetorno = new SqlParameter("@Retorono", SqlDbType.Int);
            _pRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_pRetorno);



            try
            {
                _cnn.Open();


                _comando.ExecuteNonQuery();


                if ((int)_pRetorno.Value == -1)
                    throw new Exception("El Cliente ya Existe");
               


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }
        public void ModificarCliente(Clientes unC, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));

            SqlCommand _comando = new SqlCommand("ModificarCliente", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@CiCli", unC.CiCli);
            _comando.Parameters.AddWithValue("@Nombre", unC.NombreCliente);
            _comando.Parameters.AddWithValue("@NumTarj", unC.NumTarj);
            _comando.Parameters.AddWithValue("@Telefono", unC.Telefono);


            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            

            try
            {
                _cnn.Open();
                
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -2)
                    throw new Exception("El Cliente no Existe ");

                

            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            finally
            {
                _cnn.Close();
            }
        }

        public Clientes BuscarCliente(string unCli, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Clientes _unCli = null;


            SqlCommand _comando = new SqlCommand("BuscarCliente", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CiCli", unCli);


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    string Nombre = (string)_lector["Nombre"];
                    string numTarj = (string)_lector["NumTarj"];
                    string tel = (string)_lector["Telefono"];

                   


                    _unCli = new Clientes(unCli, Nombre, numTarj, tel);
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
            return _unCli;

        }
        public List<Clientes> ListarCliente(Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Clientes _unCli = null;
            List<Clientes> _lista = new List<Clientes>();

            SqlCommand _comando = new SqlCommand("ListarCliente", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unCli = new Clientes(
                            (string)_lector["CiCli"],
                            (string)_lector["Nombre"],
                            (string)_lector["NumTarj"],
                            (string)_lector["Telefono"]
                         );
                        _lista.Add(_unCli);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }

    }
}
