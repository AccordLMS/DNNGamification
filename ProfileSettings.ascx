<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileSettings.ascx.cs" Inherits="DNNGamification.ProfileSettings" %>

<%--Register--%>
<%@ Register TagPrefix="gmf" Assembly="DNNGamification" Namespace="DNNGamification.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>



<div class="dnnForm dnnClear gmfProfileSettings gmfScope">
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="PortalId.Label" runat="server" />
		<asp:DropDownList ID="ddrPortalId" CssClass="gmfCombobox" runat="server" />
	</div>    
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="TemplateDirectory.Label" runat="server" />
		<%-- Control --%>
		<asp:DropDownList ID="ddrTemplateDirectory" CssClass="gmfCombobox" runat="server">
			<%-- Here will be template directories --%>
		</asp:DropDownList>
	</div>
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="ShowChart.Label" Visible="true" runat="server" />
		<%-- Control --%>
		<asp:CheckBox ID="chbShowChart" runat="server" />
	</div>
</div>
