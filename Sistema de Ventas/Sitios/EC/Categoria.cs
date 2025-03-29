using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EC
{
    public class Categoria
    {
        //atributos

        private string _Codigo_Cate;
        private string _Nombre;

        //propiedades


        [DisplayName("Codigo Categoria")]
        public string Codigo_Cate
        {
            get { return _Codigo_Cate; }
            set
            { _Codigo_Cate = value;}
        }

        [DisplayName("Nombre")]
        public string Nombre
        {
            get { return _Nombre; }
            set
            { _Nombre = value;}
        }

        //constructor

        public Categoria(string C_Codigo_Cate, string C_Nombre)
        {
            Codigo_Cate = C_Codigo_Cate;
            Nombre = C_Nombre;
        }
        public Categoria() { }


        public void Validar()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(this.Codigo_Cate, "[A-Za-z0-9]{6}"))
                throw new Exception("Error en Codigo de Categoria");

            if ((this.Nombre.Trim().Length > 50) || (this.Nombre.Trim().Length <= 0))
                throw new Exception("Error en Nombre de Categoria");

        }

        
    }
}