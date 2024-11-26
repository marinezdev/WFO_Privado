<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprReporteMaestro.aspx.cs" Inherits="wfip.supervision.sprReporteMaestro" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState();
            UpdateText();
        }
        function UpdateSelectAllItemState() {
            IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
        }
        function IsAllSelected() {
            var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
            return checkListBox.GetSelectedItems().length == selectedDataItemCount;
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>
    <fieldset>
    
    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Theme="iOS"  EditFormat="Custom" Date="2009-11-02 09:23" Width="190" Caption="Hasta">
        <TimeSectionProperties Adaptive="true">
            <TimeEditProperties EditFormatString="hh:mm tt" />
        </TimeSectionProperties>
        <CalendarProperties>
            <FastNavProperties DisplayMode="Inline" />
        </CalendarProperties>
    </dx:ASPxDateEdit>
        
    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Theme="iOS"  EditFormat="Custom" Date="2009-11-02 09:23" Width="190" Caption="Hasta">
        <TimeSectionProperties Adaptive="true">
            <TimeEditProperties EditFormatString="hh:mm tt" />
        </TimeSectionProperties>
        <CalendarProperties>
            <FastNavProperties DisplayMode="Inline" />
        </CalendarProperties>
    </dx:ASPxDateEdit>

    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1"  runat="server" AnimationType="None" Theme="iOS">
        <DropDownWindowStyle BackColor="#EDEDED" />
        <DropDownWindowTemplate>
            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" Theme="Material"
                runat="server" Height="180">
                <Border BorderStyle="None" />
                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                <Items>
                    <dx:ListEditItem Text="(Select all)" />
                    <dx:ListEditItem Text="Chrome" Value="1" />
                    <dx:ListEditItem Text="Firefox" Value="2" />
                    <dx:ListEditItem Text="IE" Value="3" />
                    <dx:ListEditItem Text="Opera" Value="4" />
                    <dx:ListEditItem Text="Safari" Value="5" />
                </Items>
                <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
            </dx:ASPxListBox>
            <table style="width: 100%">
                <tr>
                    <td style="padding: 4px">
                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Cerrar" style="float: right">
                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </DropDownWindowTemplate>
        <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
    </dx:ASPxDropDownEdit> 

    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox2" ID="ASPxDropDownEdit2"  runat="server" AnimationType="None" Theme="iOS">
        <DropDownWindowStyle BackColor="#EDEDED" />
    </dx:ASPxDropDownEdit>

    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox3" ID="ASPxDropDownEdit3" runat="server" AnimationType="None" Theme="iOS">
        <DropDownWindowStyle BackColor="#EDEDED" />
    </dx:ASPxDropDownEdit>
    
    </fieldset>
</asp:Content>
    