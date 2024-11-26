<%@ Page Title="" Language="C#" MasterPageFile="~/administracion/adminsysMaster.Master" AutoEventWireup="true" CodeBehind="pruCfgMesa.aspx.cs" Inherits="wfip.administracion.pruCfgMesa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../css/jQuerySortable.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-sortable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () { $("ol.listaOrdenada").sortable(); });

            // Procedimiento para serializar cada vez que se hace Drop
            // --------------------------------------------------------
            //var group = $("ol.listaOrdenada").sortable({
            //    group: 'listaOrdenada',
            //    delay: 500,
            //    onDrop: function ($item, container, _super, event) {
            //        var data = group.sortable("serialize").get();

            //        var jsonString = JSON.stringify(data, null, ' ');

            //        $('#serialize_output2').text(jsonString);
            //        _super($item, container);
            //    }
            //});
        });

        function serializaCfg() {
            var data = $("ol.listaOrdenada").sortable("serialize").get();
            var jsonString = JSON.stringify(data, null, ' ');
            $('#cph_areaTrabajo_hfOrdenResultado').val(jsonString);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <fieldset>
        <legend>Prueba configurar mesas</legend>
        <div style="width: 400px; margin: auto; ">
            <ol class='listaOrdenada'>
                <li data-id='0' data-name='CERO'>CERO
			        <ol></ol>
                </li>
                <li data-id='1' data-name='UNO'>UNO
			        <ol></ol>
                </li>
                <li data-id='2' data-name='DOS'>DOS
			        <ol>
                        <li data-id='3' data-name='TRES'>TRES</li>
                        <li data-id='4' data-name='CUATRO'>CUATRO</li>
                        <li data-id='5' data-name='CINCO'>CINCO
				            <ol>
                                <li data-id='6' data-name='SEIS'>SEIS</li>
                                <li data-id='7' data-name='SIETE'>SIETE</li>
                                <li data-id='8' data-name='OCHO'>OCHO</li>
                                <li data-id='9' data-name='NUEVE'>NUEVE</li>
                            </ol>
                        </li>
                    </ol>
                </li>
                <li data-id='10' data-name='DIEZ'>DIEZ</li>
                <li data-id='11' data-name='ONCE'>ONCE</li>
                <li data-id='12' data-name='DOCE'>DOCE</li>
            </ol>
        </div>
        <asp:Button ID="BtnSerializa" runat="server" Text="Serializa" OnClientClick="return serializaCfg();" OnClick="BtnSerializa_Click" />
        <asp:HiddenField ID="hfOrdenResultado" runat="server" />
        <pre id='serialize_output2'><asp:Literal ID="ltResultadoTexto" runat="server"></asp:Literal></pre>
    </fieldset>
</asp:Content>
