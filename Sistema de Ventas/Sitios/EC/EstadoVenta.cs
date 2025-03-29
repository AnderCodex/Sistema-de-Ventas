using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace EC
{
    public class EstadoVenta
    {
        private int _IdEstado;
        private string _NombreEstado;

        //propiedad
        public int IdEstado
        {
            get { return _IdEstado; }
            set { _IdEstado = value; }
        }

        public string NombreEstado
        {
            get { return _NombreEstado; }
            set{ _NombreEstado = value;}
        }

        //constructor
        public EstadoVenta(int EV_IdEstado, string EV_NombreEstado)
        {
            IdEstado = EV_IdEstado;
            NombreEstado = EV_NombreEstado;
        }

        public EstadoVenta() { }
        
    }


}