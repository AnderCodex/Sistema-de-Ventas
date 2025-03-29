using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ModeloEF;

public partial class _Default : System.Web.UI.Page
{
    List<Articulo> _todosLosArticulos = null;
    List<Articulo> _artPNombre = null;
    List<Categoria> _catCompleta = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtDetalle.Enabled = false;
                GeneroContexto();
                CargarCategorias();
                CargarArticulo();
               
                txtDetalle.Visible = false;
            }


            else
            {
                _todosLosArticulos = Session["TodosLosArt"] as List<Articulo>;
                _catCompleta = Session["ListCategoria"] as List<Categoria>;
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }

    }
    private void CargarArticulo()
    {
        try
        {
            SistemaVentasEntities SFContext = (SistemaVentasEntities)Application["VEContext"];

            DateTime _fechaActual = DateTime.Now;


            _todosLosArticulos = (from art in SFContext.Articulo
                                  where art.FechaVenc > _fechaActual && art.Activo 
                                  orderby art.Nombre
                                  select art).ToList();

            if ( _todosLosArticulos.Count == 0)
            {
                throw new Exception("No se encontraron artículos.");
            }

            Session["TodosLosArt"] = _todosLosArticulos;


            GvListArticulo.DataSource = _todosLosArticulos;
            GvListArticulo.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    private void GeneroContexto()
    {
        try
        {
            if (Application["VEContext"] == null)
            {
                SistemaVentasEntities VEContext = new SistemaVentasEntities();
                Application["VEContext"] = VEContext;
            }

        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    protected void GvListArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            GvListArticulo.PageIndex = e.NewPageIndex;

            //_todosLosArticulos = (List<Articulo>)Session["TodosLosArt"];
            GvListArticulo.DataSource = _todosLosArticulos;
            GvListArticulo.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    private void CargarCategorias()
    {
        try
        {
            SistemaVentasEntities SFContext = (SistemaVentasEntities)Application["VEContext"];
            List<Categoria> categorias = (from c in SFContext.Categoria
                                          where c.Activo 
                                          orderby c.Nombre
                                          select c).ToList();

            DDLCategoria.DataSource = categorias;
            DDLCategoria.DataTextField = "Nombre";
            DDLCategoria.DataValueField = "Codigo_Cate";
            DDLCategoria.DataBind();

            // Agregar opción de "Todas"
            DDLCategoria.Items.Insert(0, new ListItem("Seleccione", "0"));
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    protected void DDLCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (_todosLosArticulos == null)
            {
                _todosLosArticulos = (List<Articulo>)Session["TodosLosArt"];
                if (_todosLosArticulos == null)
                {
                    throw new Exception("Error al Cargar los artículos.");
                }
            }

            string _nomArt = TxtNom.Text.Trim();

            _artPNombre = _todosLosArticulos.Where(a => a.Nombre.Contains(_nomArt)).ToList();

            if (_artPNombre.Count == 0)
            {
                throw new Exception("No Existe el Artículo con ese Nombre");
            }

            string selectedCategoria = DDLCategoria.SelectedValue;
            if (selectedCategoria != "0")
            {
                _artPNombre = _artPNombre.Where(a => a.Codigo_Cate == selectedCategoria).ToList();
            }

            GvListArticulo.DataSource = _artPNombre;
            GvListArticulo.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
    protected void GvListArticulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SistemaVentasEntities SFContext = (SistemaVentasEntities)Application["VEContext"];

            txtDetalle.Enabled = false;
            if (GvListArticulo.SelectedRow != null)
            {
                string articuloSeleccionado = GvListArticulo.SelectedDataKey.Value.ToString();

                List<VentaArticulo> artSel = (from unVa in SFContext.VentaArticulo
                                              where unVa.CodArt == articuloSeleccionado
                                              select unVa).ToList();

                if (artSel.Count > 0)
                {
                    VentaArticulo articulo = artSel.First();

                    string detallesTexto = "Código: " + articulo.Articulo.Codigo + "\n" +
                                           "Nombre: " + articulo.Articulo.Nombre + "\n" +
                                           "Presentación: " + articulo.Articulo.TipoPresentacion + "\n" +
                                           "Categoría: " + articulo.Articulo.Codigo_Cate + "\n" +
                                           "Nombre Categoría: " + articulo.Articulo.Categoria.Nombre + "\n" +
                                           "Vencimiento: " + articulo.Articulo.FechaVenc.ToString("dd/MM/yyyy") + "\n"+
                                           "Cantidad de Ventas: " + articulo.CantArticulos;

                    txtDetalle.Text = detallesTexto;
                    txtDetalle.Visible = true;
                    LblError.Text = "";
                }
                else
                {
                    throw new Exception("No se encontraron detalles para el artículo seleccionado.");
                }
            }
            else
            {
                throw new Exception("Debe seleccionar un artículo para ver los detalles.");
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
            txtDetalle.Visible = false;
        }
    }
    protected void GvListArticulo_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {

            GvListArticulo.PageIndex = e.NewPageIndex;

            //_todosLosArticulos = (List<Articulo>)Session["TodosLosArt"];
            GvListArticulo.DataSource = _todosLosArticulos;
            GvListArticulo.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }

    protected void BtnRest_Click(object sender, EventArgs e)
    {
        try
        {
            GeneroContexto();
            CargarCategorias();
            CargarArticulo();

            TxtNom.Text = "";
            txtDetalle.Visible = false;
            txtDetalle.Text = "";

            DDLCategoria.SelectedIndex = 0;

            LblError.Text = "";
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }


    }
}


