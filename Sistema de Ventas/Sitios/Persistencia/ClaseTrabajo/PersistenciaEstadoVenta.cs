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
    internal class PersistenciaEstadoVenta : IPersistenciaEstadoVenta
    {
        private static PersistenciaEstadoVenta _instancia = null;

        private PersistenciaEstadoVenta() { }

        public static PersistenciaEstadoVenta GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEstadoVenta();
            return _instancia;
        }

        public List<EstadoVenta> ListarEstadoVenta(Empleado unE)
        {

            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            EstadoVenta _unEstadoV = null;
            List<EstadoVenta> _lista = new List<EstadoVenta>();

            SqlCommand _comando = new SqlCommand("ListarEstados", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;


            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unEstadoV = new EstadoVenta(
                            (int)_lector["IdEstado"],
                            (string)_lector["NombreEstado"]
                         );
                        _lista.Add(_unEstadoV);

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

        public EstadoVenta BuscarEstadoVenta(int IdEstado, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            EstadoVenta _unEV = null;

            SqlCommand _comando = new SqlCommand("BuscarEstado", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdEstado", IdEstado);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {

                    _lector.Read();


                    _unEV = new EstadoVenta(
                        (int)_lector["IdEstado"],
                        (string)_lector["NombreEstado"]
                    );
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
            return _unEV;
        }

        public void AsignarEstado(Empleado unE, int unEs, Venta nroV)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));

            SqlCommand _comando = new SqlCommand("AsignarEstadoAVenta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@NumVenta", nroV.NumVenta);
            _comando.Parameters.AddWithValue("@IdEstado", unEs);


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                int result = (int)_retorno.Value;


                if (result == -1)
                {
                    throw new Exception("La venta no Existe");
                }
                else if (result == -2)
                {
                    throw new Exception("No Existe el Estado");
                }
                else if (result == -3)
                {
                    throw new Exception("El estado ya fue Asignado");
                }
                else if (result == -4)
                {
                    throw new Exception("Error Al Ingresar los Datos");
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
    }
}
