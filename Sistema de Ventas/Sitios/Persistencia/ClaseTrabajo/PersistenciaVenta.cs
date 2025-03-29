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
    class PersistenciaVenta : IPersistenciaVenta
    {

        private static PersistenciaVenta _instancia = null;
        private PersistenciaVenta() { }
        public static PersistenciaVenta GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaVenta();
            return _instancia;

        }

        public void AltaVenta(Venta unaV, Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            SqlCommand _comando = new SqlCommand("AltaVenta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@MontoTotal", unaV.MontoTotal);
            _comando.Parameters.AddWithValue("@DirEnvio", unaV.DirEnvio);
            _comando.Parameters.AddWithValue("@CiCli", unaV.UnCli.CiCli);
            _comando.Parameters.AddWithValue("@UsuLog", unaV.UsuLog.UsuLog);

           
            SqlParameter _pNumVenta = new SqlParameter("@NumVenta", SqlDbType.Int);
            _pNumVenta.Direction = ParameterDirection.Output;
            _comando.Parameters.Add(_pNumVenta);

            
            SqlParameter _pRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _pRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_pRetorno);

            SqlTransaction transaction = null;

            try
            {
                _cnn.Open();
                transaction = _cnn.BeginTransaction();
                _comando.Transaction = transaction;

                _comando.ExecuteNonQuery(); 

                int resultado = (int)_pRetorno.Value; 

                if (resultado == -1)
                    throw new Exception("El Cliente no existe");
                else if (resultado == -2)
                    throw new Exception("El Empleado tiene que estar logueado");

                int numGenerado = (int)_pNumVenta.Value; 

                
                foreach (VentaArticulo VA in unaV.ListVArt)
                    new PersistenciaVentaArticulo().AltaVentaArticulo(numGenerado, VA, transaction);

                
                foreach (EstadoGenerado unEs in unaV.ListEstado)
                    new PersistenciaEstadoGenerado().AsignarEstadoGenerado(numGenerado, unEs.UnEstado.IdEstado, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }
        }



        public List<Venta> ListarVenta(Empleado unE)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unE));
            Venta _unaV = null;
            List<Venta> _lista = new List<Venta>();

            SqlCommand _comando = new SqlCommand("ListarVenta", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {


                        int _numV = (int)_lector["NumVenta"];
                        decimal mT = (decimal)_lector["MontoTotal"];
                        DateTime fecha =(DateTime)_lector["FechaVenta"];
                        string env = (string)_lector["DirEnvio"];
                        string _unC = (string)_lector["CiCli"];
                        Clientes c =   PersistenciaCliente.GetInstancia().BuscarCliente(_unC, unE);
                        string _unEN = (string)_lector["UsuLog"];
                        Empleado unEmp = PersistenciaEmpleado.GetInstancia().BuscarEmp(_unEN, unE);

                        Venta v = new Venta(_numV, mT, fecha, env, c, unEmp,
                                             new PersistenciaVentaArticulo().ListarVentaArt(unE, _numV),
                                             new PersistenciaEstadoGenerado().ListarHistoricoVenta(_numV, unE));   

                         
                        _lista.Add(v);
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
