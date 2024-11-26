<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detalleMesaRDescargaV2.aspx.cs" Inherits="wfip.supervision.detalleMesaRDescargaV2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <a href="../ArchivosExcel/">../ArchivosExcel/</a>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
 <form id="form1" runat="server">
    
    <nav class="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
  <a class="navbar-brand" href="#">DESCARGA</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarCollapse">
    
  </div>
</nav>

<main role="main" class="container">
    <div class="jumbotron">
        <h1>Reporte sabana</h1>
        <asp:Panel ID="InformacionFin" runat="server" Visible="false">
             <p>Descarga Finalizada.</p>
        </asp:Panel>
        <asp:Panel ID="Informacion" runat="server">
            <p>Tu reporte está siendo procesado, no cierras la ventana de tu navegador hasta aparecer tu descarga.</p>
            <div class="row">
                <div class="col-md-4 col-sm-4">
                    <strong>Fecha Inicio: </strong>
                    <asp:Label runat="server" ID="LabelFechaInicio" Text="" Font-Bold="True" ></asp:Label>
                </div>
                <div class="col-md-4 col-sm-4">
                    <strong>Fecha Fin:</strong>
                    <asp:Label runat="server" ID="LabelFechaFin" Text="" Font-Bold="True" ></asp:Label>
                </div>
                <div class="col-md-1 col-sm-2">
                    <asp:Label runat="server" ID="LabelNum" Text="" ></asp:Label>
                </div>
                <div class="col-md-1 col-sm-2">
                    <img src="../img/spinneer2.gif" width="100%" />
                </div>
        </div> 
        </asp:Panel>
        <div style="visibility:collapse">
            <asp:Button ID="BtnContinuar" runat="server"  AutoPostBack="True" Text="Iniciar Descarga" Class="btn btn-success" OnClick="BtnDescargar_Click"/>
        </div>
    </div>
</main>
     

    <script src="../js/jquery-1.12.4.min.js">
    </script>
    <script>
        $(document).ready(function () {
            setTimeout(function () {
                document.getElementById("<%= BtnContinuar.ClientID %>").click();
                //alert(1);
            },1000); // el tiempo a que pasara antes de ejecutar el cod
        });
    </script>
     </form>
</body>
</html>
