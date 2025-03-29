<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 225px;
        }
        .auto-style4 {
            width: 71%;
        }
        .auto-style5 {
            width: 1024px;
        }
        .auto-style6 {
            width: 227px;
        }
        .auto-style7 {
            width: 196px;
        }
        .auto-style8 {
            width: 194px;
        }
        .auto-style9 {
            width: 194px;
            height: 17px;
        }
        .auto-style10 {
            width: 227px;
            height: 17px;
        }
        .auto-style11 {
            height: 17px;
        }
        .auto-style12 {
            width: 194px;
            height: 23px;
        }
        .auto-style13 {
            width: 227px;
            height: 23px;
        }
        .auto-style14 {
            height: 23px;
        }
        .auto-style15 {
            width: 194px;
            height: 26px;
        }
        .auto-style16 {
            width: 227px;
            height: 26px;
        }
        .auto-style17 {
            height: 26px;
        }
        .auto-style18 {
            width: 88%;
        }
        .auto-style19 {
            width: 225px;
            height: 420px;
        }
        .auto-style20 {
            height: 420px;
        }
        .auto-style21 {
            width: 31px;
        }
        .auto-style22 {
            width: 100%;
            height: 80px;
        }
        .auto-style23 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Listado de Articulo</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">Filtros:
                        <table class="auto-style4">
                            <tr>
                                <td class="auto-style9"></td>
                                <td class="auto-style10"></td>
                                <td class="auto-style11"></td>
                            </tr>
                            <tr>
                                <td class="auto-style12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Buscar:</td>
                                <td class="auto-style13">
                                    <asp:TextBox ID="TxtNom" runat="server" Width="268px"></asp:TextBox>
                                </td>
                                <td class="auto-style14">
                                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
                                &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="BtnRest" runat="server" OnClick="BtnRest_Click" Text="Reiniciar" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style15"></td>
                                <td class="auto-style16"></td>
                                <td class="auto-style17"></td>
                            </tr>
                            <tr>
                                <td class="auto-style8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Categoria:</td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="DDLCategoria" runat="server" Width="197px" AutoPostBack="true" OnSelectedIndexChanged="DDLCategoria_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">
                        <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">
                        <table class="auto-style18">
                            <tr>
                                <td class="auto-style19">
                                    <asp:GridView ID="GvListArticulo" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="Codigo" OnSelectedIndexChanged="GvListArticulo_SelectedIndexChanged" AllowPaging="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="GvListArticulo_PageIndexChanging1">
    <Columns>
        <asp:BoundField DataField="Codigo" HeaderText="Código" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
        <asp:BoundField DataField="TipoPresentacion" HeaderText="Presentación" />
        <asp:BoundField DataField="FechaVenc" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:CommandField ShowSelectButton="True" SelectText="Ver Detalles" />
    </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
</asp:GridView>
                                </td>
                                <td rowspan="3" class="auto-style21">
                                    &nbsp;</td>
                                <td class="auto-style20">
                                    <table class="auto-style22">
                                        <tr>
                                            <td>
                        <asp:TextBox ID="txtDetalle" runat="server" TextMode="MultiLine" Rows="6" Columns="50" CssClass="auto-style23" Height="154px"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style5">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
