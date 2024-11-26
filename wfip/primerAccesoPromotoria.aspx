<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="primerAccesoPromotoria.aspx.cs" Inherits="wfip.primerAccesoPromotoria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .boton {
            -moz-box-shadow: inset 0px 1px 0px 0px #bbdaf7;
            -webkit-box-shadow: inset 0px 1px 0px 0px #bbdaf7;
            box-shadow: inset 0px 1px 0px 0px #bbdaf7;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #79bbff), color-stop(1, #378de5));
            background: -moz-linear-gradient(top, #79bbff 5%, #378de5 100%);
            background: -webkit-linear-gradient(top, #79bbff 5%, #378de5 100%);
            background: -o-linear-gradient(top, #79bbff 5%, #378de5 100%);
            background: -ms-linear-gradient(top, #79bbff 5%, #378de5 100%);
            background: linear-gradient(to bottom, #79bbff 5%, #378de5 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#79bbff', endColorstr='#378de5',GradientType=0);
            background-color: #79bbff;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            border: 1px solid #84bbf3;
            display: inline-block;
            cursor: pointer;
            color: #ffffff;
            font-family: Arial;
            font-size: 12px;
            padding: 6px 12px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #528ecc;
            min-width: 80px;
        }

            .boton:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #378de5), color-stop(1, #79bbff));
                background: -moz-linear-gradient(top, #378de5 5%, #79bbff 100%);
                background: -webkit-linear-gradient(top, #378de5 5%, #79bbff 100%);
                background: -o-linear-gradient(top, #378de5 5%, #79bbff 100%);
                background: -ms-linear-gradient(top, #378de5 5%, #79bbff 100%);
                background: linear-gradient(to bottom, #378de5 5%, #79bbff 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#378de5', endColorstr='#79bbff',GradientType=0);
                background-color: #378de5;
            }

            .boton:active {
                position: relative;
                top: 1px;
            }
    </style>
    <script type="text/javascript">
        history.forward();
        document.oncontextmenu = function () { return false }
        function minimosCaracteres() {
            resultado = false;
            longUsuario = document.getElementById("txUsuario").value.length > 7;
            longClave = document.getElementById("txClave").value.length > 7;
            if (longUsuario && longClave) resultado = true
            else alert("La contraseña debe tener minimo 8 letras y/o números");
            return resultado;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
        <div id="dvSeparaCabeza" style="width: 100%; min-height: 20px;">
        </div>
        <div id="dvContenedor" class="divCajaAplicacion">
            <div id="dvSeparaTopLogo" style="width: 100%; min-height: 20px;">
            </div>
            <div id="dvCajaLogo" style="width: 100%; background-color: white;">
                <table id="tblCajaLogos" style="width: 100%">
                    <tr>
                        <td style="width: 33%">
                            <img src="img/logo.gif" style="padding-left: 30px;" />
                        </td>
                        <td style="width: 33%; text-align: center; font-weight: bold;">
                            <span style="font-size: 28px; color: #007CC3; font-family: Arial">FTO</span>
                            <br />
                            Flujo de Trabajo Operación
                        </td>
                        <td style="text-align: right;">
                            <img src="img/logo_asae.png" style="padding-right: 30px;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvCajaMenuTop" style="width: 100%; min-height: 35px; background-color: #EEF3F6;">
            </div>
            <div style="width: 100%; min-height: 250px">
                <div id="dbMsgPrimer acceso" style="width: 100%; text-align: center">
                    <h2>
                        <asp:Literal ID="ltUsuario" runat="server"></asp:Literal></h2>
                    <h3>ESTE ES TU PRIMER INGRESO A ESTE PORTAL, POR FAVOR CREA TU CONTRASEÑA</h3>
                </div>
                <asp:Panel ID="pnlCajaCaptura" runat="server" Width="100%">
                    <div id="dvCajaTblLogin" style="border: 1px solid #999999; width: 400px; margin-top: 50px; margin-left: auto; margin-right: auto; background-color: #F2F2F2; border-radius: 8px 8px 8px 8px;">
                        <table id="tblLogin" style="width: 100%; font-family: Arial; font-size: 10px">
                            <tr>
                                <td colspan="2" style="height: 15px"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">CONTRASEÑA:
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txUsuario" MaxLength="64"
                                        AutoCompleteType="Disabled" Width="150px"
                                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server"
                                        TargetControlID="txUsuario"
                                        FilterMode="ValidChars"
                                        ValidChars="1234567890.abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ControlToValidate="txUsuario" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">CONFIRMA CONTRASEÑA:
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txClave" TextMode="Password" MaxLength="32"
                                        AutoCompleteType="Disabled" autocomplete="off" Width="150px"
                                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#999999">adminsiap</asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClave" runat="server"
                                        TargetControlID="txClave"
                                        FilterMode="ValidChars"
                                        ValidChars="1234567890.&$#?abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfv_txClave" runat="server" ControlToValidate="txClave" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compara_txClave" runat="server" ErrorMessage="No son iguales" ForeColor="Red" ControlToValidate="txClave" ControlToCompare="txUsuario"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 15px; text-align: center; color: red">
                                    <asp:Literal ID="ltMsg" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 35px; text-align: center;">
                                    <asp:Button ID="LoginButton" runat="server" Text="Aceptar" CausesValidation="true" OnClientClick="return minimosCaracteres();" OnClick="LoginButton_Click" CssClass="boton" />
                                    &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click" CssClass="boton" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlRespuestaCreacion" runat="server" Width="100%" Visible="false" HorizontalAlign="Center">
                    <br />
                    <br />
                    <asp:Literal ID="ltAceptaCreacion" runat="server"></asp:Literal><br />
                    <asp:Button ID="btnAceptaCreacion" runat="server" Text="Aceptar" CausesValidation="false" OnClick="btnAceptaCreacion_Click" CssClass="boton" />
                </asp:Panel>
                <div id="dvSeparaPie" style="height: 220px">
                </div>
                <div id="dvCajaPie" style="width: 100%;">
                    <div style="width: 90%; margin: auto; text-align: center; font-size: 12px;">
                        <hr />
                        Si desea conocer más sobre Asae Consultores, mándenos un correo a <a href="mailto:asae_contactanos@asae.com.mx" target="_top">asae_contactanos@asae.com.mx</a><br />
                        o llamen a la ciudad de México al (55) 5322-0518
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
