 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;



namespace EC
{
    public class VentaArticulo
    {
        
        private int _CantArticulos;
        private  Articulo _unArt;

        [DisplayName("Cantdad de Articulo")]
        public int CantArticulos
        {
            get { return _CantArticulos; }
            set { _CantArticulos = value; }
        }


        [DisplayName("Articulo")]
        public Articulo unArt
        {
            get { return _unArt; }
            set{ _unArt = value;}
        }

        public VentaArticulo(int VA_CantArt, Articulo VA_Articulo)
        {
            
            CantArticulos = VA_CantArt;
            unArt = VA_Articulo;

        }

        public VentaArticulo() { }

        public void Validar()
        {
            if (this.unArt == null)
                throw new Exception("Debe Seleccionar un Articulo");

            if (this.CantArticulos <= 0)
                throw new Exception("La cantidad tiene que ser Positiva");
        }
    }
}