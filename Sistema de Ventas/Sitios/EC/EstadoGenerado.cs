using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace EC
{
    public class EstadoGenerado
    {
        
 
        private DateTime _FechaEst;
        private EstadoVenta _unEstado;

        //propiedades

        public DateTime FechaEst
        {
            get { return _FechaEst; }
            set { _FechaEst = value; }
        }

        public EstadoVenta UnEstado
        {
            get { return _unEstado; }
            set{ _unEstado = value;}
        }

        public EstadoGenerado( DateTime EG_FechaEst, EstadoVenta EG_EstadoVenta)
        {
            FechaEst = EG_FechaEst;
            UnEstado = EG_EstadoVenta;
        }

        public EstadoGenerado() { }

        public void Validar()
        {
            if (this.UnEstado == null)
                throw new Exception("Debe Seleccionar un Estado");
        }


    }
}