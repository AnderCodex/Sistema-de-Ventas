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
    class PersistenciaCategoria : IPersistenciaCategoria
    {
        private static PersistenciaCategoria _instancia = null;

        private PersistenciaCategoria() { }

        public static PersistenciaCategoria GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCategoria();
            return _instancia;

        }

        public Categoria BuscarCategoriaActiva(string unC, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Categoria _unaCat = null;

            SqlCommand _comando = new SqlCommand("BuscarActivo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CodCat", unC);


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                 // string codCat = (string)_lector["Codigo_Cate"];
                    string nombre = (string)_lector["Nombre"];
                   // bool activo = (bool)_lector["Activo"];



                    _unaCat = new Categoria(unC, nombre);
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

            return _unaCat;
        }
        public void AltaCategoria (Categoria unaCat, Empleado unE)
        {

            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));

            SqlCommand _comando = new SqlCommand("AltaCategoria", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            //Parametros de entrada
            _comando.Parameters.AddWithValue("@Codigo_Cate", unaCat.Codigo_Cate);
            _comando.Parameters.AddWithValue("@Nombre", unaCat.Nombre);
           


            //retorno
            SqlParameter _pRetorno = new SqlParameter("@Retorono", SqlDbType.Int);
            _pRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_pRetorno);

            

            try
            {
                _cnn.Open();


                _comando.ExecuteNonQuery();


                if ((int)_pRetorno.Value == -1)
                    throw new Exception("La Categoria no existe verifique");
                else if ((int)_pRetorno.Value == -2)
                    throw new Exception("Verifique los Datos Ingresados");


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
        public void ModificarCategoria(Categoria unaCat, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("ModificarCategoria", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@Codigo_Cate", unaCat.Codigo_Cate);
            _comando.Parameters.AddWithValue("@Nombre", unaCat.Nombre);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("La categoría no existe o está inactiva.");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error al modificar la categoría.");
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
        public void BajaCategoria (Categoria unaCat, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("BajaCategoria", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            // Parámetro de entrada
            _comando.Parameters.AddWithValue("@Codigo_Cate", unaCat.Codigo_Cate);

            // Parámetro de retorno
            SqlParameter _retorno = new SqlParameter();
            _retorno.ParameterName = "@Retorno";
            _retorno.SqlDbType = System.Data.SqlDbType.Int;
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                int resultado = (int)_retorno.Value;

                if (resultado == -1)
                    throw new Exception("La categoría no existe.");

                if (resultado == -3)
                    throw new Exception("Error al eliminar la categoría.");
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
        public List<Categoria> ListarCategoria(Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Categoria _unaCat = null;
            List<Categoria> _lista = new List<Categoria>();

            SqlCommand _comando = new SqlCommand("ListarCategoria", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {

                        _unaCat = new Categoria(
                         (string)_lector["Codigo_Cate"],
                         (string)_lector["Nombre"]
                        );
                        _lista.Add(_unaCat);
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

        internal Categoria BuscarCategoria(string unC, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Categoria _unaCat = null;

            SqlCommand _comando = new SqlCommand("BuscarCategoria", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CodCat", unC);


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    string codCat = (string)_lector["Codigo_Cate"];
                    string nombre = (string)_lector["Nombre"];



                    _unaCat = new Categoria(codCat, nombre);
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

            return _unaCat;
        }
    }
}
