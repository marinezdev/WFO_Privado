<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="wfip.operacion.WebForm1" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>WFO</title>
    <link href="../css/metro-bootstrap.css" rel="stylesheet" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
<div class="container-fluid">
 <div class="panel panel-default">
     <div class="pri-nav">
         <div class="container">
         <img class="navbar-left posi" src="../img/logo.png" />
             <p class="navbar-text navbar-center text-center posi"><b class="txt1">WFO</b> <br />Flujo de Trabajo Operación </p>
             <p class="navbar-text navbar-right posi">Operador Asae</p>
              
        </div>
     </div>
     <nav class="navbar navbar-default">
  <div class="container-fluid">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
    </div>
 <!-- Menu Móviles -->
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
        <li class="active"><a href="inicio.aspx">Inicio <span class="sr-only">(current)</span></a></li>
        <li><a href="listaTramite.aspx">Pendiente</a></li>
          <li class="dropdown">
              <a class="dropdown-toggle" data-toggle="dropdown" href="#">Opciones
              <span class="caret"></span></a>
              <ul class="dropdown-menu">
                 <li><a href="inicio.aspx">Cambiar contraseña</a></li>
              </ul>
         </li>
         <li><a href="citasMedicas.aspx">Citas Medicas</a></li> 
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li><a href="#">Salir</a></li>
    
      </ul>
    </div><!-- /.navbar-collapse -->
  </div><!-- /.container-fluid -->
</nav>
   <div class="panel-body">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-blue">
                <asp:LinkButton ID="admision" runat="server" CssClass="fa-links" CommandArgument="93" CommandName="abreMesa" OnCommand="admision_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-home"></i>
			        <div><br /><br /><h4>Admisión</h4></div>
		        </asp:LinkButton>
		    </div>
	    </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-green">
                <asp:LinkButton ID="revisionDocumental" runat="server" CssClass="fa-links" CommandArgument="94" CommandName="abreMesa" OnCommand="revisionDocumental_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-folder-open"></i>
			        <div><br /><br /><h4>Revisión Docum.</h4></div>
                </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-red">
                <asp:LinkButton ID="revisionPlad" runat="server" CssClass="fa-links" CommandArgument="95" CommandName="abreMesa" OnCommand="revisionPlad_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-search"></i>
			        <div><br /><br /><h4>Revisión Plad</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-yellow">
                <asp:LinkButton ID="Seleccion" runat="server" CssClass="fa-links" CommandArgument="96" CommandName="abreMesa" OnCommand="Seleccion_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-hand-o-up"></i>
			        <div><br /><br /><h4>Selección</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-md-2"></div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-pink">
                <asp:LinkButton ID="revisionTecnica" runat="server" CssClass="fa-links" CommandArgument="97" CommandName="abreMesa" OnCommand="revisionTecnica_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-clipboard"></i>
			        <div><br /><br /><h4>Revisión Técnica</h4></div>
		        </asp:LinkButton>
		    </div>
	    </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-purple">
                <asp:LinkButton ID="Latam" runat="server" CssClass="fa-links" CommandArgument="98" CommandName="abreMesa" OnCommand="Latam_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-envelope"></i> 
			        <div><br /><br /><h4>Latam</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-lime">
                <asp:LinkButton ID="revisionMedica" runat="server" CssClass="fa-links" CommandArgument="99" CommandName="abreMesa" OnCommand="Latam_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-user-md"></i>
			        <div><br /><br /><h4>Revisión Médica</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-magenta">
                <asp:LinkButton ID="citasMedicas" runat="server" CssClass="fa-links" CommandArgument="100" CommandName="abreMesa" OnCommand="citasMedicas_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-calendar-o"></i>
			        <div><br /><br /><h4>Citas Médicas</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-md-2"></div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-teal">
                <asp:LinkButton ID="Captura" runat="server" CssClass="fa-links" CommandArgument="101" CommandName="abreMesa" OnCommand="Captura_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-archive"></i>
			        <div><br /><br /><h4>Captura</h4></div>
		        </asp:LinkButton>
		    </div>
	    </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-turquoise">
                <asp:LinkButton ID="Control" runat="server" CssClass="fa-links" CommandArgument="102" CommandName="abreMesa" OnCommand="Control_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-users"></i>
			        <div><br /><br /><h4>Control</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	        <div class="thumbnail tile tile-medium tile-green-sea">
                <asp:LinkButton ID="Ejecucion" runat="server" CssClass="fa-links" CommandArgument="103" CommandName="abreMesa" OnCommand="Ejecucion_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-play"></i>
			        <div><br /><br /><h4>Ejecución</h4></div>
		        </asp:LinkButton>
	        </div>
        </div>
        <div class="col-sm-6 col-md-2">
	            <div class="thumbnail tile tile-medium tile-emerald">
                <asp:LinkButton ID="Cobranza" runat="server" CssClass="fa-links" CommandArgument="104" CommandName="abreMesa" OnCommand="Cobranza_Command">
                    <div><br /></div>
                    <i class="fa fa-4x fa-dollar"></i>
			        <div><br /><br /><h4>Cobranza</h4></div>
		        </asp:LinkButton>
	            </div>
            </div>
        <div class="col-md-2"></div>
    </div>
</form>
   </div>
  </div>
</div>
    <dx:ASPxLoadingPanel ID="pnlMsgProcesando" runat="server" ClientInstanceName="pnlMsgProcesando" Modal="true" Text="Procesando...">
    </dx:ASPxLoadingPanel>
<asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>

<script src="../js/jquery-1.12.4.min.js"></script>
<script src="../js/bootstrap.js"></script>
</body>
</html>
