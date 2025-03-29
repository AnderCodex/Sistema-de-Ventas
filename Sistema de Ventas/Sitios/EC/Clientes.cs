using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;




namespace EC
{
    public class Clientes
    {
         string _CiCli;
         string _NombreCliente;
         string _NumTarj;
         string _Telefono;

        //propiedades
        [DisplayName("Cedula")]
        public string CiCli
        {
            get { return _CiCli; }
            set{ _CiCli = value;}
        }

        [DisplayName("Nombre Completo")]
        public string NombreCliente
        {
            get { return _NombreCliente; }
            set{ _NombreCliente = value;}
        }

        [DisplayName("Numero de Tarjeta")]
        public string NumTarj
        {
            get { return _NumTarj; }
            set { _NumTarj = value;}
        }

        [DisplayName("Telefono de Contacto")]
        public string Telefono
        {
            get { return _Telefono; }
            set{_Telefono = value;}
        }

        //constructor

        public Clientes(string C_CiCli, string C_NombreCliente, string C_NumTarj, string C_Telefono)
        {
            CiCli = C_CiCli;
            NombreCliente = C_NombreCliente;
            NumTarj = C_NumTarj;
            Telefono = C_Telefono;
        }

        public Clientes() { }

        public void Validar()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(this.CiCli.Trim(), "[1-6]{1}[0-9]{7}"))
                throw new Exception("La CI debe tener 8 dígitos y comenzar con un número entre 1 y 6.");

            if (!System.Text.RegularExpressions.Regex.IsMatch(this.NombreCliente.Trim(), "[A-Za-z]{1,50}"))
                throw new Exception("Debe ingresar un Nombre Completo con una longitud entre 1 y 50 caracteres.");

            if (string.IsNullOrWhiteSpace(this.NumTarj) || !System.Text.RegularExpressions.Regex.IsMatch(this.NumTarj, "[0-9]{16}"))
            {
                throw new Exception("Error: El número de tarjeta debe tener exactamente 16 dígitos.");
            }

            
            if (string.IsNullOrWhiteSpace(this.Telefono) || !System.Text.RegularExpressions.Regex.IsMatch(this.Telefono, "[0-9]{9}"))
            {
                throw new Exception("Error: El teléfono debe tener entre 8 y 9 dígitos.");
            }
        }

    }
}