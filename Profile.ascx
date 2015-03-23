<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="DNNGamification.Profile" %>

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

<div class="gmfProfile gmfScope">
	<asp:Panel ID="pnlChart" CssClass="gmfChart" runat="server">
		<tlr:RadHtmlChart ID="hcActivities" Width="280" Height="165" Transitions="true" runat="server">
			<Legend><Appearance Visible="false" /></Legend>
			<Appearance><FillStyle BackgroundColor="Transparent" /></Appearance>
			<PlotArea>
				<Appearance><FillStyle BackgroundColor="White" /></Appearance>
				<XAxis AxisCrossingValue="0" Color="#B3B3B3" MajorTickSize="0" MajorTickType="Outside" MinorTickSize="0" MinorTickType="Outside" DataLabelsField="XAxis">
					<LabelsAppearance RotationAngle="0" Visible="true">
						<TextStyle Color="#444444" Bold="false" Italic="false" />
					</LabelsAppearance>
					<MajorGridLines Color="#DEDEDE" Width="0" />
					<MinorGridLines Color="#DEDEDE" Width="0" />
					<TitleAppearance Position="Center" Text="Last 14 Days">
						<TextStyle FontSize="14" Color="#999999" Italic="false" Bold="false" />
					</TitleAppearance>
				</XAxis>
				<YAxis AxisCrossingValue="0" Color="#B3B3B3" MajorTickSize="0" MajorTickType="Outside" MinorTickSize="0" MinorTickType="Outside" Width="0" MinValue="0" Reversed="false">
					<LabelsAppearance RotationAngle="0" Visible="true">
						<TextStyle Color="#444444" Bold="false" Italic="false" />
					</LabelsAppearance>
					<MajorGridLines Color="#DEDEDE" Width="1" />
					<MinorGridLines Color="#DEDEDE" Width="0" />
					<TitleAppearance Position="Center" Text="Activity Points">
						<TextStyle FontSize="14" Color="#999999" Italic="false" Bold="false" />
					</TitleAppearance>
				</YAxis>
				<Series>
					<tlr:LineSeries Name="Last 14 Days" DataFieldY="YAxis">
						<Appearance><FillStyle BackgroundColor="#5ab7de" /></Appearance>
						<TooltipsAppearance Visible="false" />
						<MarkersAppearance MarkersType="Square" BackgroundColor="White" Size="8" BorderColor="#5ab7de" BorderWidth="2" Visible="false" />
						<LabelsAppearance DataField="XAxis" Visible="false" />
						<LineAppearance Width="1" />
					</tlr:LineSeries>
				</Series>
			</PlotArea>
		</tlr:RadHtmlChart>
	</asp:Panel>
	<%-- Achievements --%>
	<asp:Panel ID="pnlAchievements" CssClass="gmfAchievements" runat="server">
		<asp:Repeater ID="rptBadges" runat="server">
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
	</asp:Panel>
</div>

<tlr:RadScriptBlock ID="sbInit" runat="server">
	<script type="text/javascript">
		$(document).ready(function () { profile.init(); })
	</script>
</tlr:RadScriptBlock>
