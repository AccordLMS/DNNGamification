<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditBadge.ascx.cs" Inherits="DNNGamification.EditBadge" %>

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

<tlr:RadScriptBlock ID="sbLocals" runat="server">
	<script type="text/javascript">
	    var locals = ['<%= LocalizeString("Delete.Confirm") %>'];

	</script>
</tlr:RadScriptBlock>

<tlr:RadAjaxPanel ID="apMain" runat="server" ClientEvents-OnRequestStart="requestStart" >
	<div class="dnnForm dnnClear gmfEditBadge gmfScope">
		<div class="dnnFormItem">
			<dnn:Label ID="lblName" AssociatedControlID="tbName" ResourceKey="Name.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:TextBox ID="tbName" CssClass="dnnFormRequired" ValidationGroup="grpMain" MaxLength="100" runat="server" />
			<asp:RequiredFieldValidator ID="rqvName" ControlToValidate="tbName"
				ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError" ResourceKey="Name.Required" Display="Dynamic"
				Enabled="true" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label ID="lblDescription" AssociatedControlID="tbDescription"
				ResourceKey="Description.Label" runat="server" />
			<!-- Controls and validators -->
			<asp:TextBox ID="tbDescription" CssClass="dnnFormRequired" TextMode="MultiLine" MaxLength="512"
				ValidationGroup="grpMain" Rows="3" runat="server" />
		</div>
		<div class="dnnFormItem">
			<dnn:Label ID="lblExpirationPeriod" AssociatedControlID="tbExpirationPeriod"
				ResourceKey="ExpirationPeriod.Label" runat="server" />
			<!-- Controls and validators -->
			<div class="gmfExpirationPeriod">
				<div class="gmfQuantity">
					<asp:TextBox ID="tbQuantity" MaxLength="4" ValidationGroup="grpMain" runat="server" />
					<!-- Validators -->
					<asp:CustomValidator ID="csvQuantity" ControlToValidate="tbQuantity"
						ValidateEmptyText="true" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError"
						Display="Dynamic" ClientValidationFunction="editBadge.onValidateQuantity"
						OnServerValidate="csvQuantity_ServerValidate" ResourceKey="ExpirationQuantity.Custom"
						runat="server" />
					<asp:CompareValidator ID="cmpQuantity" ControlToValidate="tbQuantity" Type="Integer"
						ValueToCompare="0" Operator="GreaterThan" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError"
						Display="Dynamic" ResourceKey="ExpirationQuantity.Compare" runat="server" />
					<!-- Client ID -->
					<tlr:RadScriptBlock ID="sbQuantity" runat="server">
						<script type="text/javascript">
							var tbQuantityCID = '<%= tbQuantity.ClientID %>';
						</script>
					</tlr:RadScriptBlock>
				</div>
				<div class="gmfUnit">
					<asp:DropDownList ID="ddlUnit" CssClass="gmfCombobox"
						RenderingMode="Full" OnClientSelectedIndexChanged="editBadge.onUnitChanged"
						runat="server">
						<Items>
							<asp:ListItem Value="0" ResourceKey="ExpirationUnit.Days" />
							<asp:ListItem Value="1" ResourceKey="ExpirationUnit.Months" />
							<asp:ListItem Value="2" ResourceKey="ExpirationUnit.Years" />
						</Items>
					</asp:DropDownList>
					<!-- Client ID -->
					<tlr:RadScriptBlock ID="sbUnit" runat="server">
						<script type="text/javascript">
							var cbUnitCID = '<%= ddlUnit.ClientID %>';
						</script>
					</tlr:RadScriptBlock>
				</div>
				<div class="gmfReset">
					<asp:LinkButton ID="btnResetExpiration" Text="reset selection"
						OnClientClick="return editBadge.resetExpiration()" CommandName="Reset"
						runat="server" />
				</div>
			</div>
		</div>
		<div class="dnnFormItem">
			<dnn:Label ID="lblSelector" AssociatedControlID="fsBadgeThumbnail" ResourceKey="Image.Label" runat="server" />
			<!-- Controls and validators -->
			<tlr:RadAjaxPanel ID="apSelector" runat="server" ClientEvents-OnRequestStart="requestStart" >
				<gmf:DnnFileSelector ID="fsBadgeImage" FileFilter="jpg,png,gif" ShowUpload="true"
					CssClass="gmfFileSelectorScope" runat="server" />
			</tlr:RadAjaxPanel>
			<asp:CustomValidator ID="csvImage" ValidationGroup="grpMain" CssClass="dnnFormMessage dnnFormError"
				Display="Dynamic" ClientValidationFunction="editBadge.onValidateImage"
				OnServerValidate="csvImage_ServerValidate" ResourceKey="BadgeImage.Custom"
				runat="server" />
			<!-- Client ID -->
			<tlr:RadScriptBlock ID="sbBadgeImage" runat="server">
				<script type="text/javascript">
				    var fsBadgeImageCID = '<%= fsBadgeImage.ClientID %>';

				    function requestStart(sender, eventArgs) {
				        if (eventArgs.get_eventTarget().indexOf("btnSave") > -1) {
				            eventArgs.set_enableAjax(false);
                        }
				    }

				</script>
			</tlr:RadScriptBlock>
		</div>
		<tlr:RadAjaxPanel ID="apActivities" CssClass="gmfActivities" runat="server">
			<div class="dnnFormItem">
				<dnn:Label AssociatedControlID="ddlActivities" ResourceKey="Activities.Label" runat="server" />
				<!-- Controls and validators -->
				<div class="gmfEnclose gmfOverflow">
					<div class="gmfTable">
						<asp:Repeater ID="rptBadgeActivities" DataSourceID="odsBadgeActivities" OnItemCommand="rptBadgeActivities_OnItemCommand" runat="server">
							<HeaderTemplate>
								<table class="dnnGrid" cellspacing="0" border="0">
									<colgroup>
										<col style="width: 60%;">
										<col style="">
										<col style="width: 50px;">
									</colgroup>
									<thead>
										<tr class="dnnGridHeader">
											<th align="center" scope="col"><%= LocalizeString("Name.Header") %></th>
											<th align="center" scope="col"><%= LocalizeString("ActivityPoints.Header") %></th>
											<th align="center" scope="col"></th>
										</tr>
									</thead>
									<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="dnnGridItem" align="center">
									<td style="width: 50%"><%# Eval("DataItem.DisplayName") %></td>
									<td><%# Eval("DataItem.ActivityPoints") %></td>
									<td style="width: 30px;">
										<asp:LinkButton ID="btnRemoveItem" ClientIDMode="AutoID" OnClientClick="return confirm(locals[0])"
											CommandName="Click" CommandArgument='<%# Eval("UniqueKey") %>'
											runat="server">
												<dnn:DnnImage ID="DnnImage1" IconKey="Delete" AlternateText="Remove" ResourceKey="Remove" 
													runat="server" />
										</asp:LinkButton>
									</td>
								</tr>
							</ItemTemplate>
							<FooterTemplate>
									<% if (rptBadgeActivities.Items.Count == 0) { %>
										<tr class="dnnGridItem">
											<td colspan="3"><%= LocalizeString("NoRecords") %></td>
										</tr>
									<% } %>
									</tbody>
								</table>
							</FooterTemplate>
						</asp:Repeater>
					</div>
				</div>
			</div>
			<div class="dnnFormItem">
				<dnn:Label AssociatedControlID="ddlActivities" runat="server" />
				<!-- Controls and validators -->
				<div class="gmfEnclose gmfOverflow">
					<div class="gmfSelector">
						<asp:DropDownList ID="ddlActivities" CssClass="gmfCombobox"
							RenderingMode="Full" DataValueField="ActivityId" DataTextField="DisplayName"
							runat="server" />
						<asp:RequiredFieldValidator ID="rqvScroingActions" ControlToValidate="ddlActivities"
							ValidationGroup="grpActivities" CssClass="dnnFormMessage dnnFormError" Display="Dynamic" Enabled="true"
							ResourceKey="Activities.Required" runat="server" />
					</div>
					<div class="gmfPoints">
						<dnn:Label AssociatedControlID="tbPoints" ResourceKey="Points.Label" runat="server" />
						<!-- Controls and validators -->
						<asp:TextBox ID="tbPoints" ValidationGroup="grpActivities" MaxLength="4" Visible="true" runat="server" />
						<!-- Validators -->
						<asp:RequiredFieldValidator ID="rqvPoints" ControlToValidate="tbPoints"
							ValidationGroup="grpActivities" CssClass="dnnFormMessage dnnFormError" Display="Dynamic"
							ResourceKey="Points.Required" runat="server" />
						<asp:CompareValidator ID="cmpPoints" ControlToValidate="tbPoints" Type="Integer"
							ValueToCompare="0" Operator="GreaterThan" ValidationGroup="grpActivities" CssClass="dnnFormMessage dnnFormError"
							Display="Dynamic" ResourceKey="Points.Compare" runat="server" />
					</div>
					<div class="gmfActions">
						<asp:LinkButton ID="btnAddActivity" CssClass="dnnPrimaryAction" CommandName="Add"
							CausesValidation="true" ValidationGroup="grpActivities" OnClick="btnAddActivity_OnClick"
							ResourceKey="Add" runat="server" />
					</div>
				</div>
			</div>
		</tlr:RadAjaxPanel>
		<%-- ID hidden field --%>
		<asp:HiddenField ID="hfId" Value="-1" runat="server" />
		<%-- Actions--%>
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

<asp:ObjectDataSource ID="odsBadgeActivities" EnableCaching="false"
	TypeName="DNNGamification.EditBadge" SelectMethod="odsBadgeActivities_Select"
	runat="server" />

<tlr:RadScriptBlock ID="sbInit" runat="server">
	<script type="text/javascript">
		$(document).ready(function () { editBadge.init(); })
	</script>
</tlr:RadScriptBlock>
