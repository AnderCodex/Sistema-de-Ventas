 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace EC
{
    public class Empleado
    {

        //atributos
        private string _UsuLog;
        private string _Nombre;
        private string _PassUsu;


        //Propiedades
        [Required(ErrorMessage = "Ingrese el usuario")]
        public string UsuLog
        {
            get { return _UsuLog; }
            set{ _UsuLog = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set{_Nombre = value;}
        }

        [Required(ErrorMessage = "Ingrese la contraseña")]
        public string PassUsu
        {
            get { return _PassUsu; }
            set{ _PassUsu = value;}
        }


        //Constructor 
        public Empleado(string pUsuLog, string pNomEmp, string pPassEmp)
        {
            UsuLog = pUsuLog;
            Nombre = pNomEmp;
            PassUsu = pPassEmp;
        }

        public Empleado()
        {
            
        }


        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(this.UsuLog) || this.UsuLog.Trim().Length < 3)
            {
                throw new Exception("El usuario debe tener al menos 3 caracteres.");
            }
            if (this.UsuLog.Trim().Length > 20)
            {
                throw new Exception("El usuario no puede exceder los 20 caracteres.");
            }

            if ((this.Nombre.Trim().Length > 20) || (this.Nombre.Trim().Length <= 0))
                throw new Exception("El empleado tiene que tener un  Nombre ");


            if (string.IsNullOrWhiteSpace(this.PassUsu))
            {
                throw new Exception("La contraseña no puede estar vacía.");
            }
            if (this.PassUsu.Trim().Length > 20)
            {
                throw new Exception("La contraseña no puede exceder los 20 caracteres.");
            }
        }

    }
}