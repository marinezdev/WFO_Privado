﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wfip.laboratorios.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="../css/bootstrap.css" rel="stylesheet" />
        <script src="../js/jquery-1.12.4.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script src="../js/bootbox.min.js"></script>
        <link rel="icon" href="img/logo.ico" sizes="32x32" />
        <title></title>
        <style>
            body {
                background: #eee !important;
            }
            .wrapper {
                margin-top: 50px;
                margin-bottom: 80px;
            }
            .form-signin {
                max-width: 380px;
                padding: 15px 35px 45px;
                margin: 0 auto;
                background-color: #fff;
                border: 1px solid rgba(0, 0, 0, 0.1);
                text-align: center;
            }
            .form-signin .form-signin-heading,
            .form-signin .checkbox {
                margin-bottom: 30px;
            }
            .form-signin .checkbox {
                font-weight: normal;
            }
            .header{
                text-align: center;
                background-size: cover;
                color: white;
                height: 80px;
                border-radius: 5px 5px 0 0;
            }
            .header h3{
                padding-top: 35px;
            }
            footer {
                position: absolute;
                bottom: 0;
                width: 100%;
                height: 100px;
                background-color: #fff;
                text-align:center;
                font-size:12px;
           }
        </style>

</head>
<body>
    <header>
    </header>
    <section>
        <div class="wrapper">

                <form id="form1" runat="server" class="form-signin">
                    <div>

                        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
                        <div class="header">
                            <img src="../img/logo_grap.jpg" width="200" height="50" />
                        </div>
                        <hr />
                        <h2 class="form-signin-heading">Acceso Laboratorios</h2>
                        <div class="input-group">
                            <span class="input-group-addon">
                            </span>
                            <asp:TextBox runat="server" ID="txUsuario" AutoCompleteType="Disabled" class="form-control" placeholder="Correo Electrónico" aria-describedby="sizing-addon2"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server" TargetControlID="txUsuario" FilterMode="ValidChars" FilterType="UppercaseLetters, LowercaseLetters, Custom "  ValidChars="1234567890.@abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ControlToValidate="txUsuario" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="LoginButton" runat="server" Text="Aceptar" CausesValidation="true" OnClick="LoginButton_Click" class="btn btn-default btn-lg btn-primary btn-block" /><br />
                        <div class="input-group">
                            <asp:DropDownList ID="ddlProveedor" runat="server" Visible="false" AutoPostBack="true" CssClass="form-control" Width="310px" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <br />
                        <div class="input-group">
                            <asp:DropDownList ID="ddlSucursal" runat="server" Visible="false" Width="310px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div style="color:#835F34" >
                            <asp:Literal ID="ltMsg" runat="server" EnableViewState="False" ></asp:Literal>
                        </div>
                        
                        <br />
                        <asp:HyperLink ID="lnkRecuperarClave" runat="server" Text="Recuperar Contraseña" NavigateUrl="~/recuperarClave.aspx"></asp:HyperLink>


                    </div>
                </form>
            </div>
        </section>
        <footer>
            <br />
            <img src="../img/asae.png" width="120" height="35" />
            <p>
                Si desea conocer más sobre Asae Consultores, mándenos un correo a <a href="mailto:asae_contactanos@asae.com.mx" target="_top">asae_contactanos@asae.com.mx</a><br />o llamen a la Ciudad de México al (55) 5322-0518
            </p>
        </footer>

</body>
</html>