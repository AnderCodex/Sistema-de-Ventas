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
    internal class PersistenciaEstadoGenerado
    {
        internal List<EstadoGenerado> ListarHistoricoVenta(int NVenta, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            List<EstadoGenerado> _lista = new List<EstadoGenerado>();

            SqlCommand _comando = new SqlCommand("ListadoHistoricoVenta", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NVenta", NVenta);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        EstadoVenta unEV = PersistenciaEstadoVenta.GetInstancia().BuscarEstadoVenta(NVenta, unE);

                        EstadoGenerado _unEstadoG = new EstadoGenerado(
                            (DateTime)_lector["Fecha"],
                            unEV
                        );

                        _lista.Add(_unEstadoG);
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

        internal void AsignarEstadoGenerado(int NumV, int IdEstado, SqlTransaction trn)
        {

            SqlCommand _comando = new SqlCommand("AsignarEstadoAVenta", trn.Connection);
            _comando.CommandType = CommandType.StoredProcedure;

            
            _comando.Parameters.Add(new SqlParameter("@NumVenta", NumV));
            _comando.Parameters.Add(new SqlParameter("@IdEstado", IdEstado));

         
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_Retorno);

            try
            {
                _comando.Transaction = trn;

                
                _comando.ExecuteNonQuery();

               
                int oAfectado = (int)_Retorno.Value;


                if (oAfectado == -1)
                    throw new Exception("La venta No Existe");

                else if (oAfectado == -2)
                    throw new Exception("El estado No Existe");

                else if (oAfectado == -3)
                    throw new Exception("El Estado ya esta asignado");

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
