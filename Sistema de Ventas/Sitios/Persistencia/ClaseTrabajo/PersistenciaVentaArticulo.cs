using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EC;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaVentaArticulo 
    {


        internal void AltaVentaArticulo( int NroV,  VentaArticulo unVArt, SqlTransaction trn)
        { 
            SqlCommand _comando = new SqlCommand("AltaVentaArt", trn.Connection);
            _comando.CommandType = CommandType.StoredProcedure;


            SqlParameter _nroVenta = new SqlParameter("@NumVenta", NroV);
            SqlParameter _cant = new SqlParameter("@CantArticulos", unVArt.CantArticulos);
            SqlParameter _codArt = new SqlParameter("@CodArt", unVArt.unArt.CodigoArt);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

           _comando.Parameters.Add(_nroVenta);
            _comando.Parameters.Add(_codArt);
            _comando.Parameters.Add(_cant);
            _comando.Parameters.Add(_Retorno);

            int oAfectado = -1;
            try
            {
                _comando.Transaction = trn;
                _comando.ExecuteNonQuery();
                oAfectado = (int)_comando.Parameters["@Retorno"].Value;


                if (oAfectado == -1)
                
                    throw new Exception("La venta no Existe");
                
                if (oAfectado == -2)
                
                    throw new Exception("Error al ingresar el articulo");

                if (oAfectado == -4)

                    throw new Exception("El Articulo ya fue ingresado");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<VentaArticulo> ListarVentaArt(Empleado unE, int unaV)
        {
            List<VentaArticulo> _lista = new List<VentaArticulo>();

            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("ListarVentaArt", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@NumVenta", unaV);

            SqlDataReader _lector;

            try
            {
                _cnn.Open();
                 _lector = _comando.ExecuteReader();
               
                    while (_lector.Read())
                    {

                        int _Cant = (int)_lector["CantArticulos"];  
                        string _CodA = (string) _lector["CodArt"];
                        Articulo A =  PersistenciaArticulo.GetInstancia().BuscarArticulo(_CodA, unE);
                        VentaArticulo V = new VentaArticulo(_Cant, A);
                    _lista.Add(V);
                    
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
