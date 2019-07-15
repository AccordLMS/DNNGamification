<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DateRange.ascx.vb"
    Inherits="Interzoic.LMS.DateRange" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">
    function StartDateChanged()
    {        
        ValidatorValidate(document.getElementById("<%= valStartDateReq.ClientID %>"));
        ValidatorValidate(document.getElementById("<%= valDateRange.ClientID %>"));
    }
    
    function EndDateChanged()
    {        
        ValidatorValidate(document.getElementById("<%= valEndDateReq.ClientID %>"));
        ValidatorValidate(document.getElementById("<%= valDateRange.ClientID %>"));
    }
</script>

<asp:Panel runat="server" ID="pnlDateRange">
    <div id="tblDateRange" runat="server">
        <div runat="server" id="trDateRange">
            <div runat="server" id="tdDateRange">
                <dnn:label id="plDateRange" suffix=":" controlname="cmbDateRange" runat="server"></dnn:label>
            </div>
            <div runat="server" id="tdDrpDownDateRange">
                <asp:DropDownList runat="server" ID="cmbDateRange" CssClass="lms-SmallXCombo" AutoPostBack="false">
                </asp:DropDownList>
            </div>
        </div>
        <div runat="server" id="TrStartDate" style="display:none">
            <div runat="server" id="tdStartDate" >
                <dnn:label id="plStartDate" suffix=":" controlname="txtStartDate" runat="server"></dnn:label>
            </div>
            <div runat="server" id="tdTextboxStartDate">
                 <telerik:RadDatePicker CssClass="dnnLeft" ID="txtStartDate" runat="server" DateInput-ReadOnly="True" DateInput-Enabled="False" ></telerik:RadDatePicker>  
                <%--<asp:TextBox ID="txtStartDate" runat="server" CssClass="NormalTextBox" Width="90px"></asp:TextBox>&nbsp;--%>
                <asp:RequiredFieldValidator ID="valStartDateReq" Display="Dynamic" resourcekey="valInvalidDate.Text" ControlToValidate="txtStartDate" runat="server" Enabled="false"></asp:RequiredFieldValidator>
                <%--<asp:CompareValidator ID="valStarDateIsDate" runat="server" 
                    resourcekey="valInvalidDate.Text" ControlToValidate="txtStartDate" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                <%--<asp:HyperLink ID="cmdCalendar" onblur="StartDateChanged();" resourcekey="Calendar" CssClass="CommandButton" runat="server">
                    <asp:Label runat="server" ID="lblCalendar1" resourcekey="Calendar"></asp:Label>
                </asp:HyperLink>--%>
            </div>
        </div>
        <div runat="server" id="TrEndDate" style="display:none" nowrap>
            <div runat="server" id="tdEndDate" >
                <dnn:label id="plEndDate" runat="server" controlname="txtEndDate" suffix=":"></dnn:label>
            </div>
            <div runat="server" id="tdTextboxEndDate">
                <telerik:RadDatePicker CssClass="dnnLeft" ID="txtEndDate" runat="server"  DateInput-ReadOnly="True" DateInput-Enabled="False" ></telerik:RadDatePicker>  
                <%--<asp:TextBox ID="txtEndDate" runat="server" CssClass="NormalTextBox" Width="90px"></asp:TextBox>&nbsp;--%>
                <asp:RequiredFieldValidator ID="valEndDateReq" Display="Dynamic" resourcekey="valInvalidDate.Text" ControlToValidate="txtEndDate" runat="server" Enabked="false"></asp:RequiredFieldValidator>
                <%--<asp:CompareValidator ID="valEndDateIsDate" runat="server" 
                    resourcekey="valInvalidDate.Text" ControlToValidate="txtEndDate" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
               <%-- <asp:HyperLink ID="cmdCalendar2" onblur="EndDateChanged();" resourcekey="Calendar" CssClass="CommandButton" runat="server">
                    <asp:Label runat="server" ID="lblCalendar2" resourcekey="Calendar"></asp:Label>
                </asp:HyperLink>--%>
                
            </div>
        </div>
        
        <table id="tblRangeValidator" runat="server">
            <tr nowrap>
                <td nowrap>
                    <asp:CompareValidator ID="valDateRange" CssClass="izmlms-failure dnnleft"  runat="server" resourcekey="valDateRange.Text" Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        
        <div runat="server" id="TrInterval" visible="false" >
            <div runat="server" id="tdRBLInterval" >
                <dnn:label id="plInterval" runat="server" controlname="RBLInterval" suffix=":"></dnn:label>
            </div>
            <div runat="server" id="tdRBLIntervalCtl">
                <asp:RadioButtonList ID="RBLInterval" runat="server">
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
</asp:Panel>
