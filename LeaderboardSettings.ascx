<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaderboardSettings.ascx.cs" Inherits="DNNGamification.LeaderboardSettings" %>

<%--Register--%>
<%@ Register TagPrefix="gmf" Assembly="DNNGamification" Namespace="DNNGamification.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>

<div class="dnnForm dnnClear gmfLeaderboardSettings gmfScope">
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="PortalId.Label" runat="server" />
		<asp:DropDownList ID="ddrPortalId" CssClass="gmfCombobox" Visible="true" runat="server"></asp:DropDownList>
	</div>      
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="TemplateDirectory.Label" runat="server" />
		<%-- Control --%>
		<asp:DropDownList ID="cbTemplateDirectory" CssClass="gmfCombobox" Visible="true" runat="server">
			<%-- Here will be template directories --%>
		</asp:DropDownList>
	</div>
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="Mode.Label" runat="server" />
		<%-- Control --%>
		<asp:DropDownList ID="cbLeaderboardMode" CssClass="gmfCombobox" runat="server">
			<%-- Here will be modes --%>
			<Items>
				<asp:ListItem Value="0" ResourceKey="All.Mode" />
				<asp:ListItem Value="1" ResourceKey="GroupMembers.Mode" />
				<asp:ListItem Value="2" ResourceKey="UserCurrent.Mode" />
				<asp:ListItem Value="3" ResourceKey="UserProfile.Mode" />
				<asp:ListItem Value="4" ResourceKey="FriendsCurrent.Mode" />
				<asp:ListItem Value="5" ResourceKey="FriendsProfile.Mode" />
			</Items>
		</asp:DropDownList>
	</div>
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="ShowPaging.Label" Visible="true" runat="server" />
		<%-- Control --%>
		<asp:CheckBox ID="chbShowPaging" runat="server" />
	</div>
	<div class="dnnFormItem">
		<dnn:Label ResourceKey="PageSize.Label" runat="server" />
		<%-- Control --%>
		<asp:TextBox ID="txtPageSize" Text="" TextMode="SingleLine" runat="server" />
		<%-- Validators --%>
		<asp:CompareValidator ID="cmpPageSize" ControlToValidate="txtPageSize" ValueToCompare="0"
			Type="Integer" Operator="GreaterThan" CssClass="dnnFormMessage dnnFormError" Display="Dynamic"
			ResourceKey="PageSize.Compare" runat="server" />
		<asp:RequiredFieldValidator ID="rqvPageSize" ControlToValidate="txtPageSize"
			CssClass="dnnFormMessage dnnFormError" Display="Dynamic" ResourceKey="PageSize.Required"
			runat="server" />
	</div>
</div>
