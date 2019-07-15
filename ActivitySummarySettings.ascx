<%--Control--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitySummarySettings.ascx.cs" Inherits="DNNGamification.ActivitySummarySettings" %>

<%--Register--%>
<%@ Register TagPrefix="gmf" Assembly="DNNGamification" Namespace="DNNGamification.WebControls" %>
<%--Register--%>
<%@ Register TagPrefix="tlr" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<%--Register--%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/Controls/LabelControl.ascx" %>
<%@ Register TagPrefix="interzoic" TagName="DateRangeSelector" Src="CommonControls/DateRangeSelector.ascx" %>

<script language="javascript" type="text/javascript">

    function comboLearnerModuleWidth(sender, args) {
        //This function is used to give auto width to the telerik dropdown
        var i = 0, length = 0;
        var combo = $find("<%= cmbLearnerModule.ClientID%>");
        items = combo.get_items();
        for (i = 0; i < items.get_count() ; i++) {
            var comboItem = items.getItem(i);
            if (comboItem.get_text().length > length) {
                length = comboItem.get_text().length;
            }
        }
        var setwidth = length * 7;
        combo._dropDownWidth = setwidth;
    }

</script>

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
            <dnn:label id="plLearnerModule" runat="server" enableviewstate="False" ResourceKey="LearnerModule.Label"></dnn:label>
            <tlr:radcombobox id="cmbLearnerModule" enableembeddedskins="false" runat="server" nowrap="true" onclientdropdownopening="comboLearnerModuleWidth" maxheight="200px" width="35%" autopostback="true"></tlr:radcombobox>
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
    <div class="dnnFormItem">
            <dnn:Label ResourceKey="DateRange.Label" runat="server" enableviewstate="False"></dnn:Label>
            <interzoic:DateRangeSelector runat="server" ID="ctlCompletionDate" ShowInterval="false" DateRange="AllTime" />
    </div>
    <div class="dnnFormItem">
		<dnn:Label ResourceKey="ShowDateFilters.Label" Visible="true" runat="server" />
		<%-- Control --%>
		<asp:CheckBox ID="chkShowDateFilters" runat="server" />
	</div>
</div>
