using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EC;
using Logica;

namespace SitioNoPublico.Controllers
{
    public class CategoriaController : Controller
    {

        public ActionResult FormListCategoria(string Filtro)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Categoria> _lista = FabricaLogica.GetLogicaCategoria().ListarCategoria(unEmp);

                if (_lista.Count >= 1)
                {
                    if (String.IsNullOrEmpty(Filtro))
                        return View(_lista);
                    else
                    {
                        _lista = (from unC in _lista
                                  where unC.Nombre.ToUpper().StartsWith(Filtro.ToUpper())
                                  select unC).ToList();
                        return View(_lista);
                    }
                }
                else
                    throw new Exception("No se puede listar las Categorias");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Categoria>());
            }
        }

        [HttpGet]
        public ActionResult FormAltaCate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormAltaCate(Categoria unC)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                 unC.Validar();
                FabricaLogica.GetLogicaCategoria().Alta(unC, unEmp);
                return RedirectToAction("FormListCategoria", "Categoria");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult FromModificarCategoria(string Cod_Cate)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Categoria _unC = FabricaLogica.GetLogicaCategoria().BuscarCategoriaActiva(Cod_Cate, unEmp);
                if (_unC != null)
                    return View(_unC);
                else
                    throw new Exception("La Categoria no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Categoria());
            }
        }

        [HttpPost]
        public ActionResult FromModificarCategoria(Categoria unC)
        {
            try
            {

                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                unC.Validar();
                

                FabricaLogica.GetLogicaCategoria().Modificar(unC, unEmp);
                return RedirectToAction("FormListCategoria", "Categoria");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Categoria());
            }

        }

        [HttpGet]
        public ActionResult FormDetalleCate(string Cod_Cate)
        {
            try
            {

                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Categoria _unC = FabricaLogica.GetLogicaCategoria().BuscarCategoriaActiva(Cod_Cate, unEmp);
                if (_unC != null)
                    return View(_unC);
                else
                    throw new Exception("La Categoria no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Categoria());
            }
        }

        [HttpGet]
        public ActionResult FormEliminarCate(string Cod_Cate)
        {
            try
            {

                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Categoria _unC = FabricaLogica.GetLogicaCategoria().BuscarCategoriaActiva(Cod_Cate, unEmp);
                if (_unC != null)
                    return View(_unC);
                else
                    throw new Exception("La Categoria no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Categoria());
            }

        }

        [HttpPost]
        public ActionResult FormEliminarCate(Categoria C)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                new Categoria();

                FabricaLogica.GetLogicaCategoria().Baja(C, unEmp);
                return RedirectToAction("FormListCategoria", "Categoria");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Categoria());
            }
        }
    }
}