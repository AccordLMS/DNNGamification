<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUser.ascx.cs" Inherits="DNNGamification.EditUser" %>

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

<asp:PlaceHolder ID="phAjaxManager" runat="server">
	<%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<tlr:RadAjaxPanel ID="apMain" runat="server">
	<div class="dnnForm dnnClear gmfEditUser gmfScope">
		<div class="dnnFormItem">
			<dnn:Label AssociatedControlID="tbActivityPoints" ResourceKey="ActivityPoints.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:TextBox ID="tbActivityPoints" ValidationGroup="grpMain" runat="server" />
			<asp:RequiredFieldValidator ID="rqvActivityPoints" ControlToValidate="tbActivityPoints"
				ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError" ResourceKey="ActivityPoints.Required" Display="Dynamic"
				Enabled="true" runat="server" />
			<asp:CompareValidator ID="cmpActivityPoints" ControlToValidate="tbActivityPoints" ValueToCompare="0"
				Type="Integer" Operator="GreaterThanEqual" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError"
				Display="Dynamic" ResourceKey="ActivityPoints.Compare" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label AssociatedControlID="cbUserBadges" ResourceKey="UserBadges.Label" runat="server" />
			<!-- Controls and validators -->
			<tlr:RadAjaxPanel ID="apUserBadges" CssClass="gmfUserBadges" runat="server">
				<div class="gmfTable dnnClear">
					<asp:Repeater ID="rptUserBadges" OnItemCommand="rptUserBadges_OnItemCommand" 
						OnItemDataBound="rptUserBadges_ItemDataBound" 
						runat="server">
						<HeaderTemplate>
							<table class="dnnGrid" cellspacing="0" border="0">
								<colgroup>
									<col style="width: 70px;">
									<col style="width: 40%;">
									<col style="width: 150px;">
									<col style="width: 150px;">
									<col style="width: 50px;">
								</colgroup>
								<thead>
									<tr class="dnnGridHeader">
										<th align="center" scope="col"><%= LocalizeString("Image.Header") %></th>
										<th align="center" scope="col"><%= LocalizeString("Name.Header") %></th>
										<th align="center" scope="col"><%= LocalizeString("Awarded.Header") %></th>
										<th align="center" scope="col"><%= LocalizeString("Expire.Header") %></th>
										<th align="center" scope="col"></th>
									</tr>
								</thead>
								<tbody>
						</HeaderTemplate>
						<ItemTemplate>
							<tr class="dnnGridItem" align="center">
								<td><img class="gmfBadgeThumbnail" src="<%# Eval("ImageFileUrl") %>" /></td>
								<td><%# Eval("Name") %></td>
								<td><%# Eval("CreateDateDisplay") %></td>
								<td><%# Eval("ExpireDisplay") %></td>
								<td>
									<asp:LinkButton ID="hlDeleteBadge" ClientIDMode="AutoID"
										CommandName="Click" CommandArgument='<%# Eval("KeyID") %>' EnableViewState="true"
										runat="server">
										<dnn:DnnImage IconKey="Delete" AlternateText="Remove" ResourceKey="Remove"
											runat="server" />
									</asp:LinkButton>
								</td>
							</tr>
						</ItemTemplate>
						<FooterTemplate>
								<% if (rptUserBadges.Items.Count == 0) { %>
									<tr class="dnnGridItem">
										<td colspan="5"><%= LocalizeString("NoRecords") %></td>
									</tr>
								<% } %>
								</tbody>
							</table>
						</FooterTemplate>
					</asp:Repeater>
				</div>
				<div class="gmfBadges">
					<dnn:DnnComboBox ID="cbBadges" CssClass="gmfCombobox" RenderingMode="Full"
						DataValueField="BadgeId" DataTextField="Name" OnItemDataBound="cbBadges_OnItemDataBound"
						runat="server" />
					<asp:RequiredFieldValidator ID="rqvBadges" Display="Dynamic" Enabled="true"
						ControlToValidate="cbBadges" ValidationGroup="grpUserBadges" CssClass="dnnFormMessage dnnFormError"
						ResourceKey="Badges.Required" runat="server" />
				</div>
				<div class="dnnClear">
					<asp:LinkButton ID="btnAddUserBadge" CssClass="dnnPrimaryAction" CommandName="Add"
						CausesValidation="true" ValidationGroup="grpUserBadges" OnClick="btnAddUserBadge_OnClick"
						ResourceKey="Add" runat="server" />
				</div>
			</tlr:RadAjaxPanel>
		</div>
		<asp:HiddenField ID="hfId" Value="-1" runat="server" />
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
		$(document).ready(function () { editUser.init(); })
	</script>
</tlr:RadScriptBlock>