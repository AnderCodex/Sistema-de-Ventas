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
    public class VentaController : Controller
    {
        [HttpGet]
        public ActionResult FormAltaVenta()
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Clientes> _ListaCli = FabricaLogica.GetLogicaCliente().ListarCliente(unEmp);
                ViewBag.ListarCliente = new SelectList(_ListaCli, "CiCli", "NombreCliente");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public ActionResult FormAltaVenta(Venta V, string uncli, string direccionEnvio)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                V.UsuLog = unEmp;
                V.ListVArt = new List<VentaArticulo>();


                if (!string.IsNullOrEmpty(uncli))
                {
                    string CI = uncli.Trim();
                    V.UnCli = FabricaLogica.GetLogicaCliente().BuscarCliente(CI, unEmp);

                    if (V.UnCli == null)
                    {
                        ViewBag.Mensaje = "Cliente no encontrado.";
                        return View(V);
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Seleccione un cliente.";
                    return View(V);
                }


                V.DirEnvio = direccionEnvio;

                V.ListEstado = new List<EstadoGenerado>();


                if (V.ListEstado == null)
                {
                    V.ListEstado = new List<EstadoGenerado>();
                }


                EstadoGenerado estadoArmado = new EstadoGenerado
                {
                    UnEstado = FabricaLogica.GetLogicaEstado().BuscarEstadoVenta(1, unEmp)
                };

                V.ListEstado.Add(estadoArmado);

                Session["Venta"] = V;


                return RedirectToAction("FormVentaArticuloAlta", "Venta");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(V);
            }
        }

        [HttpGet]
        public ActionResult FormVentaArticuloAlta()
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }


                Venta V = (Venta)Session["Venta"];
                if (V == null)
                {
                    return RedirectToAction("FormAltaVenta");
                }


                List<Articulo> _ListaArt = FabricaLogica.GetLogicaArticulo().ListarArticulo(unEmp);
                ViewBag.ListaArticulos = new SelectList(_ListaArt, "CodigoArt", "NombreArt");


                ViewBag.ArticulosAgregados = V.ListVArt;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult FormVentaArticuloAlta(VentaArticulo VA, string coaArt)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];
                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                if (coaArt.Trim().Length > 0)
                {
                    string _cod = Convert.ToString(coaArt);
                    VA.unArt = FabricaLogica.GetLogicaArticulo().BuscarArticuloActivo(_cod, unEmp);
                }

                VA.Validar();


                ((Venta)Session["Venta"]).ListVArt.Add(VA);


                return RedirectToAction("FormVentaArticuloAlta", "Venta");
            }
            catch (Exception ex)
            {
                Empleado unEmp = (Empleado)Session["Logueo"];
                List<Articulo> _ListaA = FabricaLogica.GetLogicaArticulo().ListarArticulo(unEmp);
                ViewBag.ListaArticulos = new SelectList(_ListaA, "CodigoArt", "NombreArt");
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult GuardarVenta()
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];


                Venta V = (Venta)Session["Venta"];
                if (V == null || V.ListVArt.Count == 0 && V.ListEstado.Count == 0)
                {
                    Session["ErrorVenta"] = "No se pueden guardar ventas sin artículos.";
                    return RedirectToAction("FomrErrorAlta", "Venta");
                }


                V.CalcularMontoTotal();




                V.Validar();
                FabricaLogica.GetIlogicaVenta().Alta(V, unEmp);


                Session["Venta"] = null;

                return RedirectToAction("FormAltaExito", "Venta");
            }
            catch (Exception ex)
            {
                Session["ErrorVenta"] = ex.Message;
                return RedirectToAction("FomrErrorAlta", "Venta");
            }
        }

        public ActionResult FormAltaExito()
        {
            return View();
        }

        public ActionResult FomrErrorAlta()
        {
            ViewBag.Mensaje = Session["ErrorVenta"].ToString();
            return View();
        }

        public ActionResult FormListarVenta(string FFecha, string estado)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];
                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Venta> _lista = FabricaLogica.GetIlogicaVenta().ListarVenta(unEmp);

                
                    if (!String.IsNullOrEmpty(estado))
                    {

                        _lista = (from unV in _lista
                                  where unV.ListEstado.Count() == Convert.ToInt32(estado.ToUpper())
                                  select unV).ToList();
                    }
                    else if (!String.IsNullOrEmpty(FFecha))
                    {

                        _lista = (from unA in _lista
                                  where unA.FechaVenta.Date == Convert.ToDateTime(FFecha.ToUpper())
                                  select unA).ToList();
                    }


                    return View(_lista);
                
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Venta>());

            }

        }

        public ActionResult GenerarEstado(int numVenta)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];
                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Venta> listaVenta = FabricaLogica.GetIlogicaVenta().ListarVenta(unEmp);
                Venta ventaSel = listaVenta.Where(v => v.NumVenta == numVenta).FirstOrDefault();

                EstadoVenta estadoVenta = ventaSel.ListEstado.Last().UnEstado;
                int idEstado = estadoVenta.IdEstado + 1;

                EstadoVenta estadoGenerado = FabricaLogica.GetLogicaEstado().BuscarEstadoVenta(idEstado,unEmp);

                if(estadoGenerado == null)
                {
                    throw new Exception("El estado no fue asignado");
                }

                FabricaLogica.GetLogicaEstado().AsignarEstado(unEmp, idEstado, ventaSel);

                return View("FormListarVenta", "Venta");
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
               return RedirectToAction("FormListarVenta", "Venta");
            }
        }
    }
}
