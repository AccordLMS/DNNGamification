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
			<asp:GridView ID="grActivities" CssClass="dnnGrid" AutoGenerateColumns="false"
				OnRowDataBound="grActivities_OnDataBound" 
				AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
				PageSize="10" runat="server">

					<HeaderStyle CssClass="dnnGridHeader" HorizontalAlign="Center" Font-Bold="true" />
                    <rowstyle CssClass="dnnGridItem" />
                    <alternatingrowstyle CssClass="dnnGridAltItem" />
					<Columns>
						<asp:BoundField DataField="DesktopModuleName" HeaderText="DesktopModuleName" SortExpression="DesktopModuleName">
							<ItemStyle HorizontalAlign="Center" />
						</asp:BoundField>
						<asp:BoundField DataField="Name" HeaderText="ActivityName" SortExpression="Name">
							<ItemStyle HorizontalAlign="Center" />
							<HeaderStyle Width="50%" />
						</asp:BoundField>
						<asp:BoundField DataField="ActivityPoints" HeaderText="ActivityPoints" SortExpression="ActivityPoints">
							<ItemStyle HorizontalAlign="Center" />
						</asp:BoundField>
						<asp:TemplateField HeaderText="Once" SortExpression="Once">
							<ItemStyle HorizontalAlign="Center" />
							<ItemTemplate>
								<%# Utils.ConvertTo<bool>(Eval("Once")) == true ? "Yes" : "No" %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Actions">
							<ItemStyle HorizontalAlign="Center" />
							<HeaderStyle Width="50" />
							<ItemTemplate>
								<asp:HyperLink ID="hlEditDefinition" runat="server">
									<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditDefinition" runat="server" />
								</asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>

			</asp:GridView>
			<!-- Client ID -->
			<tlr:RadScriptBlock ID="sbActivities" runat="server">
				<script type="text/javascript">
					var grdActivitiesCID = '<%= grActivities.ClientID %>';
				</script>
			</tlr:RadScriptBlock>
		</div>
		<div id="tab-2" class="dnnClear">
			<div class="dnnRight">
				<asp:HyperLink ID="hlAddBadge" CssClass="dnnSecondaryAction gmfAddBadge"
					ResourceKey="AddBadge" runat="server" />
			</div>
			<div class="dnnClear">
				<asp:GridView ID="grBadges" CssClass="dnnGrid" AutoGenerateColumns="false"
					OnRowDataBound="grBadges_OnItemDataBound" OnRowCommand="grBadges_OnRowCommand"
					AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
					PageSize="10" runat="server">
					
					<HeaderStyle CssClass="dnnGridHeader" HorizontalAlign="Center" Font-Bold="true" />
                    <rowstyle CssClass="dnnGridItem" />
                    <alternatingrowstyle CssClass="dnnGridAltItem" />
						<Columns>
							<asp:TemplateField HeaderText="Image">
								<ItemStyle HorizontalAlign="Center" />
								<ItemTemplate>
									<img class="gmfBadgeThumbnail" src="<%# Eval("ImageFileUrl") %>" />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="Name" HeaderText="BadgeName" SortExpression="Name">
								<ItemStyle HorizontalAlign="Center" CssClass="withOutBorderLeft" />
								<HeaderStyle Width="80%" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="Actions">
								<ItemStyle HorizontalAlign="Center" CssClass="withOutBorderLeft" />
								<HeaderStyle Width="50px" />
								<ItemTemplate>
									<asp:HyperLink ID="hlEditBadge" runat="server">
										<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditBadge" runat="server" />
									</asp:HyperLink>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Actions">
								<ItemStyle HorizontalAlign="Center" CssClass="withOutBorderLeft"/>
								<HeaderStyle Width="50px"  />
								<ItemTemplate>
									<asp:LinkButton ID="hlDeleteBadge" CommandName="DeleteBadge"
										CommandArgument='<%# Eval("KeyID").ToString() %>' EnableViewState="true"
										runat="server">
										<dnn:DnnImage IconKey="Delete" AlternateText="Delete" ResourceKey="DeleteBadge" runat="server" />
									</asp:LinkButton>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					
				</asp:GridView>
				<!-- Client ID -->
				<tlr:RadScriptBlock ID="sbBadges" runat="server">
					<script type="text/javascript">
						var grdBadgesCID = '<%= grBadges.ClientID %>';
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
						<asp:GridView ID="grAssignment" CssClass="dnnGrid" AutoGenerateColumns="false"
							PageSize="10" OnRowDataBound="grdAssignment_OnItemDataBound"
							AllowSorting="True" AllowPaging="True" AllowCustomPaging="True"
							Visible="false" runat="server">

								<HeaderStyle HorizontalAlign="Center" Font-Bold="true" />

								<Columns>
									<asp:TemplateField HeaderText="ProfilePhoto">
										<ItemStyle HorizontalAlign="Center" />
										<ItemTemplate>
											<img class="gmfPhotoThumbnail" src="<%# Eval("ProfilePhotoUrl") %>" />
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="60%" />
									</asp:BoundField>
									<asp:BoundField DataField="ActivityPoints" HeaderText="ActivityPoints" SortExpression="ActivityPoints">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="20%" />
									</asp:BoundField>
									<asp:TemplateField HeaderText="Actions">
										<ItemStyle HorizontalAlign="Center" />
										<HeaderStyle Width="50" />
										<ItemTemplate>
											<asp:HyperLink ID="hlEditUser" runat="server">
												<dnn:DnnImage IconKey="Edit" AlternateText="Edit" ResourceKey="EditUser" runat="server" />
											</asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>

						</asp:GridView>
					</div>
				</div>
			</tlr:RadAjaxPanel>
			<!-- Client ID -->
			<tlr:RadScriptBlock ID="sbAssignment" runat="server">
				<script type="text/javascript">
					var grdAssignmentCID = '<%= grAssignment.ClientID %>';
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
