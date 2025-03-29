using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EC
{
    public class Venta
    {
        //atributos
        private int _NumVenta;
        private decimal _MontoTotal;
        private DateTime _FechaVenta;
        private string _DirEnvio;

       
        private  Clientes _unCli;
        private  Empleado _unUsu;
        private List<VentaArticulo> _unVArt;
        private List<EstadoGenerado> _listEstado;


        //propiedades
        [DisplayName("Numero")]
        public int NumVenta
        {
            get { return _NumVenta; }
            set { _NumVenta = value; }
        }

        [DisplayName("Monto Total")]
        public decimal MontoTotal
        {
            get { return _MontoTotal; }
            set{_MontoTotal = value;}
        }

        [DisplayName("Fechaa de la Venta")]
        [DataType(DataType.Date)]
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        [DisplayName("Direccion de Envio")]
        public string DirEnvio
        {
            get { return _DirEnvio; }
            set { _DirEnvio = value;}
        }


        [DisplayName("Cliente")]
        public Clientes UnCli
        {
            get { return _unCli; }
            set{ _unCli = value;}
        }

        [DisplayName("Empleado")]
        public Empleado UsuLog
        {
            get { return _unUsu; }
            set{ _unUsu = value;}
        }

        public List<VentaArticulo> ListVArt
        {
            get { return _unVArt; }
            set{ _unVArt = value; }
        }

        public List<EstadoGenerado> ListEstado
        {
            get { return _listEstado; }
            set { _listEstado = value; }
        }



        public Venta(int V_NumVenta, decimal V_MontoTotal, DateTime V_FechaVenta, 
            string V_DirEnvio, Clientes V_unCli, Empleado V_unUsu, List<VentaArticulo> V_unVArt, List<EstadoGenerado> V_Estado)
        {
            NumVenta = V_NumVenta;
            MontoTotal = V_MontoTotal;
            FechaVenta = V_FechaVenta;
            DirEnvio = V_DirEnvio;
            UnCli = V_unCli;
            UsuLog = V_unUsu;
            ListVArt = V_unVArt;
            ListEstado = V_Estado;

            

        }

        public Venta() 
        {
           
        }

        public void CalcularMontoTotal()
        {
            MontoTotal = ListVArt.Sum(va => va.unArt.Precio * va.CantArticulos);
        }

        public void Validar()
        {
            if (this.MontoTotal <= 0)
                throw new Exception("El monto total tiene q ser positivo");
            if ((this.DirEnvio.Trim().Length > 30) || (this.DirEnvio.Trim().Length <= 0))
                throw new Exception("Error en la Direccion del Envio");
            if (this.UsuLog == null)
                throw new Exception("Se debe saber el Usuario que Genera la Venta");
            if (this.UnCli == null)
                throw new Exception("La venta tiene que tener un Cliente");
            if (this.ListVArt == null)
                throw new Exception("Debe ingresar un Articulo para realizar una venta");
            if (this.ListVArt.Count == 0)
                throw new Exception("Debe seleccionar al menos un articulo obligatoriamente");

            if (this.ListEstado == null)
                throw new Exception("Debe ingresar un Estado para realizar una venta");
            if (this.ListEstado.Count == 0)
                throw new Exception("Debe seleccionar al menos un Estado obligatoriamente");
        }

    }
}