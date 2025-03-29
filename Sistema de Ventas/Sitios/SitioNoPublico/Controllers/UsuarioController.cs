using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EC;
using Logica;

namespace SitioNoPublico.Controllers
{
    public class UsuarioController : Controller
    {
       
        [HttpGet]
        public ActionResult FormLogueo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormLogueo(Empleado U)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Empleado unE = FabricaLogica.GetLogicaEmpleado().Logueo(U.UsuLog, U.PassUsu);

                    if (unE == null)
                    {
                        ViewBag.Mensaje = "Verifique los datos ingresados.";
                        return View(U);
                    }

                     
                    Session["Logueo"] = unE;
                    return RedirectToAction("FormListIntArticulo", "Articulo");
                    
                }
                else
                {
                    return View(U);
                }
            }

            

            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(U);
            }
        }
    


        public ActionResult Deslogueo()
        {
            return RedirectToAction("index", "Home");
        }
    }
}