<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="DateRangeSelector.ascx.cs" Inherits="DNNGamification.CommonControls.DateRangeSelector" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>




<script type="text/javascript">
    var invalidInputrdpStartDate = false;
    function OnDateSelectedrdpStartDate(sender, args) {
        invalidInputrdpStartDate = false;
    }

    function OnErrorrdpStartDate(sender, args) {
        invalidInputrdpStartDate = true;
    }

    function CustomValidaterdpStartDate(sender, args) {
        if (invalidInputrdpStartDate) {
            var datepicker = $find("<%= rdpStartDate.ClientID%>");
                var rpdInput = datepicker.get_dateInput();
                var value = datepicker.get_textBox().value;
                args.IsValid = !(value != "" && datepicker.get_selectedDate() == null);
            }
    }


    </script>




<script type="text/javascript">
    var invalidInputrdpEndDate = false;
    function OnDateSelectedrdpEndDate(sender, args) {
        invalidInputrdpEndDate = false;
    }

    function OnErrorrdpEndDate(sender, args) {
        invalidInputrdpEndDate = true;
    }

    function CustomValidaterdpEndDate(sender, args) {
        if (invalidInputrdpEndDate) {
            var datepicker = $find("<%= rdpEndDate.ClientID%>");
            var rpdInput = datepicker.get_dateInput();
            var value = datepicker.get_textBox().value;
            args.IsValid = !(value != "" && datepicker.get_selectedDate() == null);
        }
    }

    function changeDateRange2(){
	var trStartDate = document.getElementById("<%= TrStartDate.ClientID %>");
	var trEndDate = document.getElementById("<%= TrEndDate.ClientID %>"); 
        var cmbDate = document.getElementById("<%= cmbDateRange.ClientID %>"); 
        var validator = document.getElementById("<%= valDateRange.ClientID %>");
        switch (cmbDate.options[cmbDate.selectedIndex].value){ 
        	case 'Custom':
                	trStartDate.style.display = '';
                	trEndDate.style.display = '';
                	document.getElementById("<%= tblRangeValidator.ClientID %>").style.display = '';
                	ValidatorEnable(validator, true);
                	break;
                default: 
                	trStartDate.style.display = 'none'; 
               		trEndDate.style.display = 'none'; 
               		document.getElementById("<%= tblRangeValidator.ClientID %>").style.display = 'none'; 
                	ValidatorEnable(validator, false);
                	break;
        };      
    }

</script>

<asp:Panel runat="server" ID="pnlDateRange">
    <table id="tblDateRange" runat="server">
        <tr runat="server" id="trDateRange">
            <td colspan="2" runat="server" id="tdDrpDownDateRange">
                <asp:DropDownList runat="server" ID="cmbDateRange" CssClass="NormalTextBox" AutoPostBack="false"  Width="200px" onchange="changeDateRange2()"> <%--style="width:100%"--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="TrStartDate" style="display:none">
            <td runat="server" id="tdStartDate" >
                <asp:Label id="lblStartDate" resourcekey="plStartDate" runat="server"></asp:Label>
            </td>
            <td runat="server" id="tdTextboxStartDate">
                <telerik:RadDatePicker CssClass="dnnLeft" ID="rdpStartDate" runat="server">
                        <ClientEvents OnDateSelected="OnDateSelectedrdpStartDate" />
                        <DateInput ClientEvents-OnError="OnErrorrdpStartDate" />
                </telerik:RadDatePicker>
                <asp:CustomValidator EnableClientScript="true" ID="valEffectiveDate" runat="server"
                ClientValidationFunction="CustomValidaterdpStartDate"  resourcekey="valInvalidDate.Text" CssClass="izmlms-failurebold"></asp:CustomValidator> 
            </td>
        </tr>
        <tr runat="server" id="TrEndDate" style="display:none" nowrap>
            <td runat="server" id="tdEndDate" >
                <asp:Label id="lblEndDate" runat="server" resourcekey="plEndDate" ></asp:Label>
            </td>
            <td runat="server" id="tdTextboxEndDate">
                <telerik:RadDatePicker CssClass="dnnLeft" ID="rdpEndDate" runat="server" >
                     <ClientEvents OnDateSelected="OnDateSelectedrdpEndDate" />
                        <DateInput ClientEvents-OnError="OnErrorrdpEndDate" />
                </telerik:RadDatePicker>
                <asp:CustomValidator EnableClientScript="true" ID="CustomValidator1" runat="server"
                ClientValidationFunction="CustomValidaterdpEndDate"  resourcekey="valInvalidDate.Text" CssClass="izmlms-failurebold"></asp:CustomValidator> 
                
            </td>
        </tr>
        
        <tr id="tblRangeValidator" runat="server">
            <td colspan="2">
                <asp:CompareValidator ID="valDateRange" runat="server" Display="Dynamic" CssClass="izmlms-failure dnnLeft" Operator="GreaterThanEqual" Type="Date" ControlToCompare="rdpStartDate" ControlToValidate="rdpEndDate"></asp:CompareValidator>
            </td>
        </tr>
        
        <tr runat="server" id="TrInterval" visible="false" >
            <td runat="server" id="tdRBLInterval" valign="top" >
                <asp:Label id="lblInterval" runat="server" resourcekey="plInterval"></asp:Label>
            </td>
            <td runat="server" id="tdRBLIntervalCtl">
                <asp:RadioButtonList ID="RBLInterval" runat="server">
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>
