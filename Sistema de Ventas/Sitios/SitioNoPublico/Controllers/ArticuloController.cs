using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;
using EC;
using Logica;

namespace SitioNoPublico.Controllers
{
    public class ArticuloController : Controller
    {
        public ActionResult FormListArticulo(string Filtro)
        {
            try
            {

               Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }



                List<Articulo> _lista = FabricaLogica.GetLogicaArticulo().ListarArticulo(unEmp);
                   


                    if (_lista.Count >= 1)
                    {

                        if (String.IsNullOrEmpty(Filtro))
                            return View(_lista);
                        else
                        {

                            _lista = (from unA in _lista
                                      where unA.NombreArt.ToUpper().StartsWith(Filtro.ToUpper())
                                      select unA).ToList();
                            return View(_lista);
                        }
                    }

                    else
                        throw new Exception("No hay articulo para mostrar");

                    }


                 catch (Exception ex)
                   {
                      ViewBag.Mensaje = ex.Message;
                       return View(new List<Articulo>());
                   }
                }

        [HttpGet]
        public ActionResult FormAltaAart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormAltaAart(Articulo unA)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                

                unA.Validar();
                FabricaLogica.GetLogicaArticulo().Alta(unA, unEmp);
                return RedirectToAction("FormListArticulo", "Articulo");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult FormModificarArt(string CodArt)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

              

                Articulo _unA = FabricaLogica.GetLogicaArticulo().BuscarArticuloActivo(CodArt, unEmp);
                if (_unA != null)
                    return View(_unA);
                else
                    throw new Exception("El Articulo no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Articulo());
            }
        }

        [HttpPost]
        public ActionResult FormModificarArt(Articulo unA)
        {
            try
            {

                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                unA.Validar();
                new Articulo();

                FabricaLogica.GetLogicaArticulo().Modificar(unA, unEmp);
                return RedirectToAction("FormListArticulo", "Articulo");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Articulo());
            }

        }


        [HttpGet]
        public ActionResult FormEliminarArt(string CodArt)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Articulo _unA = FabricaLogica.GetLogicaArticulo().BuscarArticuloActivo(CodArt, unEmp);

                if (_unA == null)
                {
                    ViewBag.Mensaje = "El artículo no existe o no está activo.";
                    return View(new Articulo()); 
                }

                return View(_unA);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Articulo());
            }
        }

        [HttpPost]
        public ActionResult FormEliminarArt(Articulo unA)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                if (unA == null || string.IsNullOrEmpty(unA.CodigoArt))
                {
                    ViewBag.Mensaje = "Error: El artículo no es válido.";
                    return View(new Articulo()); 
                }

                
                FabricaLogica.GetLogicaArticulo().Baja(unA, unEmp);

                
                return RedirectToAction("FormListArticulo", "Articulo");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(unA);
            }

        }

        [HttpGet]
        public ActionResult FormDetalleArt(string CodArt)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Articulo _unA = FabricaLogica.GetLogicaArticulo().BuscarArticuloActivo(CodArt, unEmp);
                if (_unA != null)
                    return View(_unA);
                else
                    throw new Exception("El Articulo no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Articulo());
            }
        }


        public ActionResult FormListIntArticulo(string FiltroNom, string FiltroC)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Articulo> _lista = FabricaLogica.GetLogicaArticulo().ListarArticulo(unEmp);

                if (_lista.Count >= 1)
                {
                    if (!String.IsNullOrEmpty(FiltroNom))
                    {
                       
                        _lista = (from unA in _lista
                                  where unA.NombreArt.ToUpper().StartsWith(FiltroNom.ToUpper())
                                  select unA).ToList();
                    }
                    else if (!String.IsNullOrEmpty(FiltroC))
                    {
                        
                        _lista = (from unC in _lista
                                  where unC.UnaCat.Nombre.ToUpper().StartsWith(FiltroC.ToUpper())
                                  select unC).ToList();
                    }

                    
                    return View(_lista);
                }
                else
                {
                    throw new Exception("No hay artículos para mostrar");
                }


            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Articulo>());
            }
        }

        public ActionResult FormVentasAsociadas(string Cod)
        {
            try
            {
               
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

               
                List<Venta> listaVentas = FabricaLogica.GetIlogicaVenta().ListarVenta(unEmp);

                if (listaVentas.Count >= 1)
                {
                    List<Venta> ventasFiltradas = (from v in listaVentas
                                                   from va in v.ListVArt
                                                   where va.unArt.CodigoArt == Cod
                                                   select v).ToList();

                    return View(ventasFiltradas);
                }
                else
                {
                    throw new Exception("No hay  Nada");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Venta>());
            }
        }


        public ActionResult FormDetCompleto(string Cod)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Articulo articulo = FabricaLogica.GetLogicaArticulo().BuscarArticuloActivo(Cod, unEmp);

                if (articulo == null)
                {
                    throw new Exception("No se encontró el artículo con el código: " + Cod);
                }

                return View(articulo);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }


    }
}