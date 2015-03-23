<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Leaderboard.ascx.cs" Inherits="DNNGamification.Leaderboard" %>

<%--Register--%>
<%@ Register TagPrefix="gmf" Assembly="DNNGamification" Namespace="DNNGamification.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<%--Import--%>
<%@ Import Namespace="DNNGamification" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<tlr:RadAjaxLoadingPanel ID="alpMain" Skin="Default" runat="server" />

<asp:PlaceHolder ID="plhAjaxManager" runat="server">
    <%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxPanel ID="apMain" runat="server">
    <div class="gmfLeaderboard gmfScope">
        <asp:Repeater ID="rptLeaderboard" runat="server">
            <HeaderTemplate>
                <%# EvaluateHeader(Container.DataItem) %>
            </HeaderTemplate>
            <ItemTemplate>
                <%# EvaluateItem(Container.DataItem) %>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <%# EvaluateAlternating(Container.DataItem) %>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <%# EvaluateFooter(Container.DataItem) %>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel ID="pnlPagination" CssClass="gmfPagination dnnClear" runat="server">
            <div class="gmfNavigation">
                <div class="gmfPrevious dnnClear">
                    <asp:LinkButton ID="btnPrevious" CssClass="dnnSecondaryAction" OnClick="btnPrevious_Click" runat="server">
                        <%= LocalizeString("Previous.Paging") %>
                    </asp:LinkButton>
                </div>
                <div class="gmfNext dnnClear">
                    <asp:LinkButton ID="btnNext" CssClass="dnnSecondaryAction" OnClick="btnNext_Click" runat="server">
                        <%= LocalizeString("Next.Paging") %>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="gmfTotal">
                <asp:Label ID="lblTotal" Text="" runat="server" />
            </div>
        </asp:Panel>
    </div>
</tlr:RadAjaxPanel>
