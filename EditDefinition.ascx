<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDefinition.ascx.cs" Inherits="DNNGamification.EditDefinition" %>

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

<tlr:RadAjaxLoadingPanel Skin="Default" ID="alpMain" runat="server" />

<asp:PlaceHolder ID="phAjaxManager" runat="server">
	<%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxPanel ID="apMain" runat="server">
	<div class="dnnForm dnnClear gmfEditDefinition gmfScope">
		<div class="dnnFormItem">
			<dnn:Label ID="lblDescription" AssociatedControlID="tbDescription"
				ResourceKey="Description.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:TextBox ID="tbDescription" CssClass="dnnFormRequired" TextMode="MultiLine"
				ValidationGroup="grpMain" Rows="5" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label ID="lblActivityPoints" AssociatedControlID="tbActivityPoints"
				ResourceKey="ActivityPoints.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:TextBox ID="tbActivityPoints" CssClass="dnnFormRequired" ValidationGroup="grpMain" runat="server" />
			<asp:RequiredFieldValidator ID="rqvActivityPoints" ControlToValidate="tbActivityPoints"
				ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError" ResourceKey="ActivityPoints.Required" Display="Dynamic"
				Enabled="true" runat="server" />
			<asp:CompareValidator ID="cmpActivityPoints" ControlToValidate="tbActivityPoints" Type="Integer"
				Operator="DataTypeCheck" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError" Display="Dynamic"
				ResourceKey="ActivityPoints.Compare" Enabled="true" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label ID="lblOnce" AssociatedControlID="chbOnce"
				ResourceKey="Once.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:CheckBox ID="chbOnce" Checked="false" ValidationGroup="grpMain"
				runat="server" />
		</div>
		<%-- ID hidden field --%>
		<asp:HiddenField ID="hfId" runat="server" />
		<%-- Actions --%>
		<ul class="dnnActions dnnClear">
			<li>
				<asp:LinkButton ID="btnPrimary" CssClass="dnnPrimaryAction"
					OnClick="btnPrimary_OnClick" ValidationGroup="grpMain" CausesValidation="true" CommandName="Save"
					ResourceKey="Save" runat="server" />
			</li>
			<li>
				<asp:HyperLink ID="btnCancel" CssClass="dnnSecondaryAction"
					NavigateUrl="javascript:void(0)" EnableViewState="true" ResourceKey="Cancel"
					runat="server" />
			</li>
		</ul>
	</div>
</tlr:RadAjaxPanel>

<tlr:RadScriptBlock ID="sbInit" runat="server">
	<script type="text/javascript">
		$(document).ready(function () { editDefinition.init(); })
	</script>
</tlr:RadScriptBlock>
