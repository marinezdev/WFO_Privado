<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errorSistema.aspx.cs" Inherits="wfip.errorSistema" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>FTO</title>
    <style type="text/css">
        body {
            background-color: #5e8bb2;
            font-family: Arial,Helvetica,sans-serif;
            font-size: 100%;
            color: #666666;
        }

        .divCajaAplicacion {
            width: 900px;
            margin: auto;
            background-color: white;
            border-radius: 8px 8px 8px 8px;
            min-height: 600px;
            -webkit-box-shadow: 10px 10px 5px 0px rgba(0,0,0,1);
            -moz-box-shadow: 10px 10px 5px 0px rgba(0,0,0,1);
            box-shadow: 10px 10px 5px 0px rgba(0,0,0,1);
        }
    </style>
    <script type="text/javascript">
        history.forward();
        document.oncontextmenu = function () { return false }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dvSeparaCabeza" style="width: 100%; min-height: 20px;">
        </div>

        <div id="dvContenedor" class="divCajaAplicacion">
        <div id="dvSeparaTopLogo" style="width: 100%; min-height: 20px;">
        </div>
        <div id="dvCajaLogo" style="width: 100%; height: 50px; background-color: white;">
            <img src="img/logo.gif" style="padding-left: 30px;" />
        </div>
        <div id="dvCajaMenuTop" style="width: 100%; min-height: 35px; background-color: #EEF3F6;">
        </div>
        <div style="width: 100%; height: 250px">
            <div id="dvCajaTblLogin" style="border: 1px solid #999999; width: 700px; margin-top: 50px; margin-left: auto; margin-right: auto; background-color: #F2F2F2; border-radius: 8px 8px 8px 8px; text-align:center;">
                <p style="color:black;">
                    Se generó un error, no es necesario reportarlo,<br />
                    se ha enviado automáticamente un informe al equipo de soporte del sistema WFO (soporteasae@asae.com.mx), sólo cierre el explorador.<br /><br />
                    Gracias por su comprensión.
                </p><br /><br />
                <center><p><asp:LinkButton ID="lnkReturn" runat="server" Text="Entrar de nuevo" OnClick="lnkReturn_Click"></asp:LinkButton></p></center>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
