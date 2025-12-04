<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FBClients.Settings" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<dnn:sectionhead id="sectPantryDetails" cssclass="Head" runat="server" text="Food Bank Settings" section="GeneralSection"
	includerule="False"></dnn:sectionhead>

<div id="GeneralSection" runat="server">
    <div class="dnnFormItem">
        <dnn:label id="lblFBName" runat="server" ResourceKey="lblFBName" controlname="txtFBName" suffix=":" />
        <asp:textbox id="txtFBName" cssclass="NormalTextBox" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblFBAddress" runat="server" ResourceKey="lblFBAddress" controlname="txtFBAddress" suffix=":" />
        <asp:textbox id="txtFBAddress" cssclass="NormalTextBox" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblFBCity" runat="server" ResourceKey="lblFBCity" controlname="txtFBCity" suffix=":" />
        <asp:textbox id="txtFBCity" cssclass="NormalTextBox" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblFBState" runat="server" ResourceKey="lblFBState" controlname="txtFBState" suffix=":" />
        <asp:textbox id="txtFBState" cssclass="NormalTextBox" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblFBZipCode" runat="server" ResourceKey="lblFBZipCode" controlname="txtFBZipCode" suffix=":" />
<asp:textbox id="txtFBZipCode" width="300" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblFBPhoneNumber" runat="server" ResourceKey="lblFBPhoneNumber" controlname="txtFBPhoneNumber" suffix=":" />
<asp:textbox id="txtFBPhoneNumber" width="300" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblFBFaxNumber" runat="server" ResourceKey="lblFBFaxNumber" controlname="txtFBFaxNumber" suffix=":" />
<asp:textbox id="txtFBFaxNumber" width="300" maxlength="50" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblAllowedIPAddress" runat="server" ResourceKey="lblAllowedIPAddress" controlname="txtAllowedIPAddress" suffix=":" />
<asp:TextBox ID="txtAllowedIPAddress" cssclass="NormalTextBox" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblGoogleAPIKey" runat="server" ResourceKey="lblGoogleAPIKey" controlname="txtGoogleAPIKey" suffix=":" />
<asp:TextBox ID="txtGoogleAPIKey" cssclass="NormalTextBox" runat="server" />
     </div>

</div>



<dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="General Settings" section="GeneralSettings2"
	includerule="False"></dnn:sectionhead>	

<div id="GeneralSettings2" runat="server">
    <div class="dnnFormItem">
        <dnn:label id="lblIDCardImagePath" runat="server" ResourceKey="lblIDCardImagePath" controlname="txtIDCardImagePath" suffix=":" />
        <asp:textbox id="txtIDCardImagePath" cssclass="NormalTextBox" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblDaysToValidVisit" runat="server" controlname="ddlDaysToValidVisit" suffix=":" />
<asp:DropDownList ID="ddlDaysToValidVisit" runat="server">
        </asp:DropDownList>
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowSendText" runat="server" controlname="cbxShowSendText" suffix=":" />
        <asp:CheckBox ID="cbxShowSendText" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowClientServiceLocation" runat="server" controlname="cbxShowClientServiceLocation" suffix=":" />
<asp:CheckBox ID="cbxShowClientServiceLocation" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowClientSuffix" runat="server" controlname="cbxShowSuffix" suffix=":" />
<asp:CheckBox ID="cbxShowSuffix" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowRelationshipToClient" runat="server" controlname="cbxShowRelationshipToClient" suffix=":" />
<asp:CheckBox ID="cbxShowRelationshipToClient" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowPhotoID" runat="server" controlname="cbxShowPhotoID" suffix=":" />
        <asp:CheckBox ID="cbxShowPhotoID" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowOneBagOnly" runat="server" controlname="cbxShowOneBagOnly" suffix=":" />
        <asp:CheckBox ID="cbxShowOneBagOnly" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowVisitStopLight" runat="server" controlname="cbxShowVisitStopLight" suffix=":" />
<asp:CheckBox ID="cbxShowVisitStopLight" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowIncExpSummary" runat="server" controlname="cbxShowIncExpSummary" suffix=":" />
        <asp:CheckBox ID="cbxShowIncExpSummary" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowExpense" runat="server" controlname="cbxShowExpense" suffix=":" />
<asp:CheckBox ID="cbxShowExpense" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowDisabilities" runat="server" controlname="cbxShowDisabilities" suffix=":" />
<asp:CheckBox ID="cbxShowDisabilities" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowClientType" runat="server" controlname="cbxShowClientType" suffix=":" />
<asp:CheckBox ID="cbxShowClientType" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowClientIdCard" runat="server" controlname="cbxShowClientIdCard" suffix=":" />
        <asp:CheckBox ID="cbxShowClientIdCard" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowPrintShoppingLabel" runat="server" controlname="cbxShowPrintShoppingLabel" suffix=":" />
<asp:CheckBox ID="cbxShowPrintShoppingLabel" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblPrintShoppingLabelQuantity" runat="server" controlname="txtPrintShoppingLabelQuantity" suffix=":" />
<asp:textbox id="txtPrintShoppingLabelQuantity" runat="server" width="300" Text="1" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblShowPrintBarCodeLabel" runat="server" controlname="cbxShowPrintBarCodeLabel" suffix=":" />
<asp:CheckBox ID="cbxShowPrintBarCodeLabel" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblIncludeInactiveOnSearch" runat="server" controlname="cbxIncludeInactiveOnSearch" suffix=":" />
<asp:CheckBox ID="cbxIncludeInactiveOnSearch" runat="server" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblFlagForReviewNotify" runat="server" ResourceKey="lblFlagForReviewNotify" controlname="txtFlagForReviewNotify" suffix=":" />
<asp:textbox id="txtFlagForReviewNotify" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblFocusableControl" runat="server" controlname="ddlFocusableControl" suffix=":" />
        <asp:DropDownList ID="ddlFocusableControl" runat="server">
                <asp:ListItem Text="ClientId" Value="txtClientId"></asp:ListItem>
                <asp:ListItem Text="Last Name" Value="txtLastName"></asp:ListItem>
                <asp:ListItem Text="First Name" Value="txtFirstName"></asp:ListItem>
                <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:DropDownList>
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblBagAllowance" runat="server" ResourceKey="lblBagAllowance" controlname="txtBagAllowance" suffix=":" />
        <asp:textbox id="txtBagAllowance" runat="server" cssclass="NormalTextBox" width="300" />
     </div>
    <div class="dnnFormItem">
<dnn:label id="lblGroupHomeBagAllowance" runat="server" ResourceKey="lblGroupHomeBagAllowance" controlname="txtGroupHomeBagAllowance" suffix=":" />
<asp:textbox id="txtGroupHomeBagAllowance" runat="server" cssclass="NormalTextBox" width="300" />
     </div>
</div>
	


<dnn:sectionhead id="sectTwilio" cssclass="Head" runat="server" text="Twilio Fields" section="TwilioSection" IsExpanded="true"
	includerule="False"></dnn:sectionhead>	

<div id="TwilioSection" runat="server">
    <div class="dnnFormItem">
        <dnn:label id="lblTwilioAccountSid" runat="server" controlname="txtTwilioAccountSid" suffix=":" />
        <asp:TextBox ID="txtTwilioAccountSid" cssclass="NormalTextBox" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblTwilioAuthToken" runat="server" controlname="txtTwilioAuthToken" suffix=":" />
        <asp:TextBox ID="txtTwilioAuthToken" cssclass="NormalTextBox" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblTwilioPhoneNumber" runat="server" controlname="txtTwilioPhoneNumber" suffix=":" />
        <asp:TextBox ID="txtTwilioPhoneNumber" cssclass="NormalTextBox" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblClientOrderPage" runat="server" controlname="txtClientOrderPage" suffix=":" />
        <asp:TextBox ID="txtClientOrderPage" cssclass="NormalTextBox" runat="server" />
     </div>
    
</div>


<dnn:sectionhead id="sectToyTicket" cssclass="Head" runat="server" text="Christmas Toys" section="ToyTicketSection" IsExpanded="false"
	includerule="False"></dnn:sectionhead>	

<div id="ToyTicketSection" runat="server">

	<div class="dnnFormItem">
        <dnn:label id="lblShowXmasToys" runat="server" controlname="cbxShowXmasToys" suffix=":" />
        <asp:CheckBox ID="cbxShowXmasToys" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblXmasToysYear" runat="server" ResourceKey="lblXmasToysYear" controlname="txtXmasToysYear" suffix=":" />'
        <asp:textbox id="txtXmasToysYear" cssclass="NormalTextBox" maxlength="4" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblXmasRequireSizeAgeRange" runat="server" ResourceKey="lblXmasRequireSizeAgeRange" controlname="txtXmasRequireSizeAgeRange" suffix=":" />
        <asp:textbox id="txtXmasRequireSizeAgeRange" cssclass="NormalTextBox" maxlength="5" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblShowXmasGiftFields" runat="server" controlname="cbxShowXmasGiftFields" suffix=":" />
        <asp:CheckBox ID="cbxShowXmasGiftFields" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblToyTicketContent" runat="server" controlname="txtToyTicketContent" suffix=":" />
        <dnn:texteditor id="txtToyTicketContent" runat="server" height="300" width="100%" />
     </div>
  
</div>



<dnn:sectionhead id="sectInstructions" cssclass="Head" runat="server" text="Instructions" section="InstructionsSection" IsExpanded="false"
	includerule="False"></dnn:sectionhead>	
<div id="InstructionsSection" runat="server">
	<div class="dnnFormItem">
        <dnn:label id="lblIncomeEligibilityGuidelines" runat="server" controlname="txtIncomeEligibilityGuidelines" suffix=":" />
        <dnn:texteditor id="txtIncomeEligibilityGuidelines" runat="server" height="300" width="100%" />
     </div>
       
    
</div>


<dnn:sectionhead id="sectRequiredFields" cssclass="Head" runat="server" text="Required Fields" section="RequiredFieldsSetion" IsExpanded="true"
	includerule="False"></dnn:sectionhead>	

<div id="RequiredFieldsSetion" runat="server">

	<div class="dnnFormItem">
        <dnn:label id="lblReguireClientGender" runat="server" controlname="cbxReguireClientGender" suffix=":" />
        <asp:CheckBox ID="cbxReguireClientGender" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblRequireClientEthnicity" runat="server" controlname="cbxRequireClientEthnicity" suffix=":" />
        <asp:CheckBox ID="cbxRequireClientEthnicity" runat="server" />
     </div>
    <div class="dnnFormItem">
        <dnn:label id="lblReqAFMVerified" runat="server" controlname="cbxReqAFMVerified" suffix=":" />
        <asp:CheckBox ID="cbxReqAFMVerified" runat="server" />
     </div>
    <div class="dnnFormItem">

     </div>
    <div class="dnnFormItem">

     </div>
    <div class="dnnFormItem">

     </div>   
    
</div>




<dnn:sectionhead id="sectPermissions" cssclass="Head" runat="server" text="Permissions" section="PermissionsSection"
	includerule="False"></dnn:sectionhead>	

<div id="PermissionsSection" runat="server">

	<div class="dnnFormItem">
        <dnn:label id="lblClientManagerUserRole" runat="server" controlname="ddlClientManagerUserRole" suffix=":" />
        <asp:DropDownList ID="ddlClientManagerUserRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName" Enabled="true">
                </asp:DropDownList>
     </div>
	<div class="dnnFormItem">
        <dnn:label id="lblClientManagerDeleteRole" runat="server" controlname="ddlClientManagerDeleteRole" suffix=":" />
        <asp:DropDownList ID="ddlClientManagerDeleteRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
                </asp:DropDownList>
     </div>       
    
</div>


