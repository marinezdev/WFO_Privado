<%@ Page Title="" Language="C#" MasterPageFile="~/promotoria/promotoria.Master" AutoEventWireup="true" CodeBehind="pmCitasMedicas.aspx.cs" Inherits="wfip.promotoria.pmCitasMedicas" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v17.2.Core, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <asp:Panel ID="pnlCajaAgenda" runat="server" Style="width: 70%; margin: auto">
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month" Theme="Aqua">
            <Views>
                <WeekView Enabled="false"></WeekView>
                <FullWeekView Enabled="true">
                </FullWeekView>
            </Views>
        </dxwschs:ASPxScheduler>
    </asp:Panel>
</asp:Content>
