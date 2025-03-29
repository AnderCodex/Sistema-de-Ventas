using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;




namespace EC
{
    public class Articulo
    {
        //atributos
        private string _Codigo;
        private string _TipoPresentacion;
        private string _NombreA;
        private decimal _Precio;
        private int _Tamaño;
        private DateTime _FechaVenc;
        

        private Categoria _Categoria;


        //propiedades
        [DisplayName("Codigo")]
        public string CodigoArt
        {
            get { return _Codigo; }
            set
            {_Codigo = value; }
        }

        [DisplayName("Presentacion")]
        public string TipoPresentación
        {
            get { return _TipoPresentacion; }
            set
            { _TipoPresentacion = value; }
        }

        [DisplayName("Nombre de Articulo")]
        public string NombreArt
        {
            get { return _NombreA; }
            set
            { _NombreA = value; }
        }

        [DisplayName("Precio Unitario")]
        public decimal Precio
        {
            get { return _Precio; }
            set
            {_Precio = value; }
        }

        public int Tamaño
        {
            get { return _Tamaño; }
            set { _Tamaño = value; }
        }

        [DisplayName("Fecha de Vencimiento")]
        public DateTime FechaVenc
        {
            get { return _FechaVenc; }
            set { _FechaVenc = value; }
        }

        [DisplayName("Caategoria")]
        public  Categoria UnaCat
        {
            get { return _Categoria; }
            set
            { _Categoria = value;}
        }

        public Articulo(string A_Codigo, string A_TipoPresentacion, string A_NombreA, decimal A_Precio, 
            int A_Tamaño, DateTime A_FechaVenc,  Categoria A_Categoria)
        {
            CodigoArt = A_Codigo;
            TipoPresentación = A_TipoPresentacion;
            NombreArt = A_NombreA;
            Precio = A_Precio;
            Tamaño = A_Tamaño;
            FechaVenc = A_FechaVenc;
            UnaCat = A_Categoria;
        }

        public Articulo()
        {
           
        }

        public void Validar()
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(this.CodigoArt, "[A-Z0-9]{10}"))
                throw new Exception("Error El codigo de el articulo tiene que tener 10 letras/numeros");

            if ((this.TipoPresentación != "Unidad") && (this.TipoPresentación != "Blister") && 
                (this.TipoPresentación != "Sobre") && (this.TipoPresentación != "Frasco"))
                throw new Exception("Error: El tipo de presentación debe ser Unidad, Blister, Sobre o Frasco.");

            if ((this.NombreArt.Trim().Length > 50) || (this.NombreArt.Trim().Length <= 0))
                throw new Exception("El Articulo tiene que tener un Nombre ");

            if(this.Precio <= 0)
                throw new Exception("El Precio tiene que ser positivo");

            if (this.UnaCat == null)
                throw new Exception("Error en Categoria");

            if (this.Tamaño <= 0)
                throw new Exception("El tamaño tiene que ser numerico y acorde al Tipo de Presentacion");

            
        }

        public override string ToString()
        {
            return (this.CodigoArt + " -" + this.NombreArt.Trim() + " - " + this.TipoPresentación.Trim() + " - " + this.Tamaño.ToString() 
                +" - "+ this.Precio.ToString() + this.FechaVenc.ToString() + " -" + this.UnaCat);
        }
    }
}