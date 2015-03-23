<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mechanics.ascx.cs" Inherits="DNNGamification.Mechanics" %>

<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<%--Import--%>
<%@ Import Namespace="DNNGamification" %>
<%--Import--%>
<%@ Import Namespace="DotNetNuke.Services" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<%--Import--%>
<%@ Import Namespace="System.Data" %>

<tlr:RadAjaxLoadingPanel Skin="Default" ID="alpMechanics" runat="server" />

<asp:PlaceHolder ID="plhAjaxManager" runat="server">
	<%-- Here will be ajax manager --%>
</asp:PlaceHolder>

<div class="gmfMechanics gmfScope">
	<div id="divTabs" class="dnnForm dnnClear" style="padding-bottom: 10px;">
		<ul class="dnnAdminTabNav">
			<li>
				<a href="#tab-1">
					<tlr:RadCodeBlock runat="server">
						<%= LocalizeString("Activities.Tab") %>
					</tlr:RadCodeBlock>
				</a>
			</li>
			<li>
				<a href="#tab-2">
					<tlr:RadCodeBlock runat="server">
						<%= LocalizeString("Badges.Tab") %>
					</tlr:RadCodeBlock>
				</a>
			</li>
			<li>
				<a href="#tab-3">
					<tlr:RadCodeBlock runat="server">
						<%= LocalizeString("Assignment.Tab") %>
					</tlr:RadCodeBlock>
				</a>
			</li>
		</ul>
		<div id="tab-1" class="dnnClear">
			<dnn:DnnGrid ID="grdActivities" CssClass="dnnGrid" AutoGenerateColumns="false"
				OnNeedDataSource="grdActivities_OnNeedDataSource" OnItemDataBound="grdActivities_OnItemDataBound"
				AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
				PageSize="10" runat="server">
				<MasterTableView>
					<HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
					<SortExpressions>
						<tlr:GridSortExpression FieldName="Name" SortOrder="Ascending" />
					</SortExpressions>
					<Columns>
						<dnn:DnnGridBoundColumn DataField="DesktopModuleName" HeaderText="DesktopModuleName" SortExpression="DesktopModuleName">
							<ItemStyle HorizontalAlign="Center" />
						</dnn:DnnGridBoundColumn>
						<dnn:DnnGridBoundColumn DataField="Name" HeaderText="ActivityName" SortExpression="Name">
							<ItemStyle HorizontalAlign="Center" />
							<HeaderStyle Width="50%" />
						</dnn:DnnGridBoundColumn>
						<dnn:DnnGridBoundColumn DataField="ActivityPoints" HeaderText="ActivityPoints" SortExpression="ActivityPoints">
							<ItemStyle HorizontalAlign="Center" />
						</dnn:DnnGridBoundColumn>
						<dnn:DnnGridTemplateColumn DataField="Once" HeaderText="Once" SortExpression="Once">
							<ItemStyle HorizontalAlign="Center" />
							<ItemTemplate>
								<%# Utils.ConvertTo<bool>(Eval("Once")) == true ? "Yes" : "No" %>
							</ItemTemplate>
						</dnn:DnnGridTemplateColumn>
						<dnn:DnnGridTemplateColumn HeaderText="Actions">
							<ItemStyle HorizontalAlign="Center" />
							<HeaderStyle Width="50" />
							<ItemTemplate>
								<asp:HyperLink ID="hlEditDefinition" runat="server">
									<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditDefinition" runat="server" />
								</asp:HyperLink>
							</ItemTemplate>
						</dnn:DnnGridTemplateColumn>
					</Columns>
				</MasterTableView>
				<SortingSettings EnableSkinSortStyles="false" />
			</dnn:DnnGrid>
			<!-- Client ID -->
			<tlr:RadScriptBlock ID="sbActivities" runat="server">
				<script type="text/javascript">
					var grdActivitiesCID = '<%= grdActivities.ClientID %>';
				</script>
			</tlr:RadScriptBlock>
		</div>
		<div id="tab-2" class="dnnClear">
			<div class="dnnRight">
				<asp:HyperLink ID="hlAddBadge" CssClass="dnnSecondaryAction gmfAddBadge"
					ResourceKey="AddBadge" runat="server" />
			</div>
			<div class="dnnClear">
				<dnn:DnnGrid ID="grdBadges" CssClass="dnnGrid" AutoGenerateColumns="false"
					OnNeedDataSource="grdBadges_OnNeedDataSource" OnItemCommand="grdBadges_OnItemCommand" OnItemDataBound="grdBadges_OnItemDataBound"
					AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
					PageSize="10" runat="server">
					<MasterTableView>
						<HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
						<SortExpressions>
							<tlr:GridSortExpression FieldName="Name" SortOrder="Ascending" />
						</SortExpressions>
						<Columns>
							<dnn:DnnGridTemplateColumn HeaderText="Image">
								<ItemStyle HorizontalAlign="Center" />
								<ItemTemplate>
									<img class="gmfBadgeThumbnail" src="<%# Eval("ImageFileUrl") %>" />
								</ItemTemplate>
							</dnn:DnnGridTemplateColumn>
							<dnn:DnnGridBoundColumn DataField="Name" HeaderText="BadgeName" SortExpression="Name">
								<ItemStyle HorizontalAlign="Center" />
								<HeaderStyle Width="80%" />
							</dnn:DnnGridBoundColumn>
							<dnn:DnnGridTemplateColumn HeaderText="Actions">
								<ItemStyle HorizontalAlign="Center" />
								<HeaderStyle Width="50px" />
								<ItemTemplate>
									<asp:HyperLink ID="hlEditBadge" runat="server">
										<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditBadge" runat="server" />
									</asp:HyperLink>
								</ItemTemplate>
							</dnn:DnnGridTemplateColumn>
							<dnn:DnnGridTemplateColumn HeaderText="Actions">
								<ItemStyle HorizontalAlign="Center" />
								<HeaderStyle Width="50px" />
								<ItemTemplate>
									<asp:LinkButton ID="hlDeleteBadge" CommandName="DeleteBadge"
										CommandArgument='<%# Eval("KeyID").ToString() %>' EnableViewState="true"
										runat="server">
										<dnn:DnnImage IconKey="Delete" AlternateText="Delete" ResourceKey="DeleteBadge" runat="server" />
									</asp:LinkButton>
								</ItemTemplate>
							</dnn:DnnGridTemplateColumn>
						</Columns>
					</MasterTableView>
					<SortingSettings EnableSkinSortStyles="false" />
				</dnn:DnnGrid>
				<!-- Client ID -->
				<tlr:RadScriptBlock ID="sbBadges" runat="server">
					<script type="text/javascript">
						var grdBadgesCID = '<%= grdBadges.ClientID %>';
					</script>
				</tlr:RadScriptBlock>
			</div>
		</div>
		<div id="tab-3" class="dnnClear">
			<tlr:RadAjaxPanel ID="apAssignment" CssClass="gmfAssignment" runat="server">
				<div class="dnnForm">
					<div class="dnnFormItem gmfSearchBox">
						<asp:TextBox ID="txtUserSearch" AutoPostBack="false"  runat="server" onkeypress="return mechanics.keyPressed(event);" />
						<%-- Search button --%>
						<asp:Button ID="btnSearch" CssClass="dnnSecondaryAction" OnClick="btnSearch_Click"
							ResourceKey="Search" runat="server" />
						<!-- Client ID -->
						<tlr:RadScriptBlock ID="sbSearch" runat="server">
							<script type="text/javascript">
								var btnSearchCID = '<%= btnSearch.ClientID %>';
							</script>
						</tlr:RadScriptBlock>
					</div>
					<div class="dnnClear">
						<dnn:DnnGrid ID="grdAssignment" CssClass="dnnGrid" AutoGenerateColumns="false"
							PageSize="10" OnNeedDataSource="grdAssignment_OnNeedDataSource" OnItemDataBound="grdAssignment_OnItemDataBound"
							AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
							Visible="false" runat="server">
							<MasterTableView>
								<HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
								<SortExpressions>
									<tlr:GridSortExpression FieldName="UserName" SortOrder="Ascending" />
								</SortExpressions>
								<Columns>
									<dnn:DnnGridTemplateColumn HeaderText="ProfilePhoto">
										<ItemStyle HorizontalAlign="Center" />
										<ItemTemplate>
											<img class="gmfPhotoThumbnail" src="<%# Eval("ProfilePhotoUrl") %>" />
										</ItemTemplate>
									</dnn:DnnGridTemplateColumn>
									<dnn:DnnGridBoundColumn DataField="UserName" HeaderText="UserName" SortExpression="UserName">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="60%" />
									</dnn:DnnGridBoundColumn>
									<dnn:DnnGridBoundColumn DataField="ActivityPoints" HeaderText="ActivityPoints" SortExpression="ActivityPoints">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="20%" />
									</dnn:DnnGridBoundColumn>
									<dnn:DnnGridTemplateColumn HeaderText="Actions">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="50" />
										<ItemTemplate>
											<asp:HyperLink ID="hlEditUser" runat="server">
												<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditUser" runat="server" />
											</asp:HyperLink>
										</ItemTemplate>
									</dnn:DnnGridTemplateColumn>
								</Columns>
							</MasterTableView>
							<SortingSettings EnableSkinSortStyles="false" />
						</dnn:DnnGrid>
					</div>
				</div>
			</tlr:RadAjaxPanel>
			<!-- Client ID -->
			<tlr:RadScriptBlock ID="sbAssignment" runat="server">
				<script type="text/javascript">
					var grdAssignmentCID = '<%= grdAssignment.ClientID %>';
				</script>
			</tlr:RadScriptBlock>
		</div>
	</div>
</div>
<tlr:RadScriptBlock ID="sbInit" runat="server">
	<script type="text/javascript">
		$(document).ready(function () { mechanics.init(); });
	</script>
</tlr:RadScriptBlock>
