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

    internal class PersistenciaArticulo : IPersistenciaArticulo
    {
        private static PersistenciaArticulo _instancia = null;

        private PersistenciaArticulo() { }


        public static PersistenciaArticulo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaArticulo();
            return _instancia;

        }

        public void AltaArticulo (Articulo unArt, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));

            SqlCommand _comando = new SqlCommand("AltaArticulo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@Codigo", unArt.CodigoArt);
            _comando.Parameters.AddWithValue("@TipoPresentacion", unArt.TipoPresentación);
            _comando.Parameters.AddWithValue("@Nombre", unArt.NombreArt);
            _comando.Parameters.AddWithValue("@Precio", unArt.Precio);
            _comando.Parameters.AddWithValue("@Tamaño", unArt.Tamaño);
            _comando.Parameters.AddWithValue("@FechaVenc", unArt.FechaVenc);
            _comando.Parameters.AddWithValue("@Codigo_Cate", unArt.UnaCat.Codigo_Cate);
            


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                int result = (int)_retorno.Value;

                // Manejo del código de retorno
                if (result == -1)
                {
                    throw new Exception("El articulo no Existe o no esta activo");
                }
                else if (result == -2)
                {
                    throw new Exception("Error al dar el alta al articulo");
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

        }
        public void ModificarArticulo(Articulo unArt, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("ModificarArticulo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@Codigo", unArt.CodigoArt);
            _comando.Parameters.AddWithValue("@TipoPresentacion", unArt.TipoPresentación);
            _comando.Parameters.AddWithValue("@Nombre", unArt.NombreArt);
            _comando.Parameters.AddWithValue("@Precio", unArt.Precio);
            _comando.Parameters.AddWithValue("@Tamaño", unArt.Tamaño);
            _comando.Parameters.AddWithValue("@FechaVenc", unArt.FechaVenc);
            _comando.Parameters.AddWithValue("@Codigo_Cate", unArt.UnaCat.Codigo_Cate);


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();

                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("Verifique el Articulo no Existe o no esta activo");

                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error al modificar verifique los datos");

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
        public Articulo BuscarArticuloActivo(string codArt, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Articulo _unArt = null;


            SqlCommand _comando = new SqlCommand("BuscarArticuloActivo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CodArt", codArt);


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    string Tpresentacion = (string)_lector["TipoPresentacion"];
                    string Nom = (string)_lector["Nombre"];
                    decimal Pre = (decimal)_lector["Precio"];
                    DateTime FecVen = (DateTime)_lector["FechaVenc"];
                    int Tamaño = (int)_lector["Tamaño"];
                    bool Activo = (bool)_lector["Activo"];
                    string Codigo_Cate = (string)_lector["Codigo_Cate"];

                    Categoria _unaC = PersistenciaCategoria.GetInstancia().BuscarCategoria(Codigo_Cate, unE);


                    _unArt = new Articulo(codArt, Tpresentacion, Nom, Pre, Tamaño, FecVen, _unaC);
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
            return _unArt;

        }
        public void BajaArticulo(Articulo unArt, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("BajaArticulo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@CodArt", unArt.CodigoArt);

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
                    throw new Exception("El Articulo no Existe");

                else if (resultado == -3)
                    throw new Exception("El Articulo Tiene Ventas Asociadas");

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


        public List<Articulo> ListarArticulo(Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            //Articulo _unArt = null;
            List<Articulo> _lista = new List<Articulo>();

            SqlCommand _comando = new SqlCommand("ListarArticulo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        Categoria _unaCat = null;
                        _unaCat = PersistenciaCategoria.GetInstancia().BuscarCategoria((string)_lector["Codigo_Cate"], unE);

                       Articulo _unArt = new Articulo(
                         (string)_lector["Codigo"],
                         (string)_lector["TipoPresentacion"],
                        (string)_lector["Nombre"],
                         (decimal)_lector["Precio"],
                        (int)_lector["Tamaño"],
                        (DateTime)_lector["FechaVenc"],

                         _unaCat);
                        _lista.Add(_unArt);
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

        internal Articulo BuscarArticulo(string codArt, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Articulo _unArt = null;
            
            

            SqlCommand _comando = new SqlCommand("BuscarArticulo", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CodArt", codArt);
            

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    string Tpresentacion = (string)_lector["TipoPresentacion"];
                    string Nom = (string)_lector["Nombre"];
                    decimal Pre = (decimal)_lector["Precio"];
                    DateTime FecVen = (DateTime)_lector["FechaVenc"];
                    int Tamaño = (int)_lector["Tamaño"];

                    Categoria _unaCat = null;
                    _unaCat  = PersistenciaCategoria.GetInstancia().BuscarCategoria((string) _lector["Codigo_Cate"], unE);
                    _unArt = new Articulo(codArt, Tpresentacion, Nom, Pre, Tamaño, FecVen, _unaCat);
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
            return _unArt;
        }


    }
}
