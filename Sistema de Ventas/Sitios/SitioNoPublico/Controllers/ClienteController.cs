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
    public class ClienteController : Controller
    {
        //// GET: Cliente
        public ActionResult FormListCliente(string Filtro, int? page)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Clientes> _lista = FabricaLogica.GetLogicaCliente().ListarCliente(unEmp);

                if (_lista.Count == 0)
                    throw new Exception("No se pueden listar los Clientes");


                if (!String.IsNullOrEmpty(Filtro))
                {
                    _lista = _lista
                             .Where(unCli => unCli.NombreCliente.ToUpper().StartsWith(Filtro.ToUpper()))
                             .ToList();
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(_lista.ToPagedList(pageNumber, pageSize));
            }

            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Articulo>().ToPagedList(1, 10));
            }

        }

        [HttpGet]
        public ActionResult FormAltaCliente()
        {
            return View();
        }

        [HttpPost]

        public ActionResult FormAltaCliente(Clientes unCli)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                unCli.Validar();
                FabricaLogica.GetLogicaCliente().Alta(unCli, unEmp);
                return RedirectToAction("FormListCliente", "Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult FormModificarCliente(string CiCli)
        {
            try
            {

                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Clientes _unCli = FabricaLogica.GetLogicaCliente().BuscarCliente(CiCli, unEmp);
                if (_unCli != null)
                    return View(_unCli);
                else
                    throw new Exception("El Cliente no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Clientes());
            }
        }

        [HttpPost]
        public ActionResult FormModificarCliente(Clientes unCli)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                unCli.Validar();
                new Clientes();

                FabricaLogica.GetLogicaCliente().Modificar(unCli, unEmp);
                return RedirectToAction("FormListCliente", "Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Clientes());
            }

        }

        [HttpGet]
        public ActionResult FormDetalleCliente(string CiCli)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                Clientes _unCli = FabricaLogica.GetLogicaCliente().BuscarCliente(CiCli, unEmp);
                if (_unCli != null)
                    return View(_unCli);
                else
                    throw new Exception("El Cliente no Existe");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new Clientes());
            }
        }

        public ActionResult FormListintCliente(string Filtro)
        {
            try
            {
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

                List<Clientes> _lista = FabricaLogica.GetLogicaCliente().ListarCliente(unEmp);

                if (_lista.Count == 0)
                    throw new Exception("No se pueden listar los Clientes");


                if (!String.IsNullOrEmpty(Filtro))
                {
                    _lista = _lista
                             .Where(unCli => unCli.NombreCliente.ToUpper().StartsWith(Filtro.ToUpper()))
                             .ToList();
                }

               

                return View(_lista);
            }

            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Articulo>());
            }

        }

        public ActionResult FormVentasCli(string CiCli)
        {
            try
            {
               
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

               
                List<Venta> listaVentas = FabricaLogica.GetIlogicaVenta().ListarVenta(unEmp);

                
                List<Venta> ventasFiltradas = (from v in listaVentas
                                               where v.UnCli.CiCli == CiCli
                                               orderby v.FechaVenta 
                                               select v).ToList();

               
                if (ventasFiltradas.Count == 0)
                {
                    throw new Exception("No se encontraron ventas asociadas al cliente.");
                }

                return View(ventasFiltradas);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Venta>());
            }
        }

        public ActionResult FormArticulosCli(string CiCli)
        {
            try
            {
                
                Empleado unEmp = (Empleado)Session["Logueo"];

                if (unEmp == null)
                {
                    return RedirectToAction("FormLogueo", "Usuario");
                }

             
                List<Venta> listaVentas = FabricaLogica.GetIlogicaVenta().ListarVenta(unEmp);

                List<Venta> ventasFiltradas = (from v in listaVentas
                                               where v.UnCli.CiCli == CiCli
                                               select v).ToList();

                
                List<Articulo> articulosUnicos = (from v in ventasFiltradas
                                                  from va in v.ListVArt
                                                  group va.unArt by va.unArt.CodigoArt into grupito
                                                  orderby grupito.First().NombreArt 
                                                  select grupito.First()) 
                                                 .ToList();

               
                if (articulosUnicos.Count == 0)
                {
                    throw new Exception("El cliente no ha comprado ningún artículo.");
                }

                return View(articulosUnicos);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Articulo>());
            }
        }
    }
}
