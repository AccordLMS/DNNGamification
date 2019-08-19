<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitySummary.ascx.cs" Inherits="DNNGamification.ActivitySummary" %>

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
<%@ Register TagPrefix="interzoic" TagName="DateRangeSelector" Src="CommonControls/DateRangeSelector.ascx" %>

<style type="text/css">
  .dnnPrimaryAction2 {
    padding: 6px 20px 4px 20px;
    font-weight: bold;
    border-radius: 4px;
    border-color: #00a1de;
    border-width: 1px;
    color: #fff;
    background: #00a1de;
    font-size: 14px;
    text-decoration: none !important;
    text-shadow: none !important;
    box-shadow: none !important;
    margin: 6px 0;
    max-width: 300px;
    text-align: center;
    min-width: 100px;
    line-height: 24px !important; }
</style>

<tlr:RadAjaxLoadingPanel ID="alpMain" Skin="Default" runat="server" />

<asp:PlaceHolder ID="plhAjaxManager" runat="server">
    <%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxPanel ID="apMain" runat="server">
            <div class="gmfTotal">
                <span id="lblOtherModuleId" runat="server"></span>
            </div>
            <div class="gmfTotal">
                <span id="lblThisModuleId" runat="server"></span>
            </div>
    <div class="dnnFormItem" runat="server" id="divDateRange">
        <dnn:Label ResourceKey="DateRange.Label" runat="server" enableviewstate="False"></dnn:Label>
        <interzoic:DateRangeSelector runat="server" ID="ctlCompletionDate" ShowInterval="false" DateRange="AllTime" />
        <asp:LinkButton runat="server" ID="btnApplyFilters" CssClass="dnnPrimaryAction2" resourcekey="btnApplyFilters" OnClick="btnApplyFilters_Click"></asp:LinkButton>
    </div>
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
        
        
        
        <script type="text/javascript">

            

        </script>
    </div>
    <asp:Button runat="server" ID="btnHidden2" CssClass="dnnPrimaryAction" style="display:none" OnClick="btnHidden2_Click"></asp:Button>
</tlr:RadAjaxPanel>
