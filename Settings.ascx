<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FBClients.Settings" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<dnn:sectionhead id="sectPantryDetails" cssclass="Head" runat="server" text="Food Bank Settings" section="tblPantryDetails"
	includerule="False"></dnn:sectionhead>

<table id="tblPantryDetails" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Food Bank Settings">
    <tr>
        <td class="SubHead"><dnn:label id="lblFBName" runat="server" ResourceKey="lblFBName" controlname="txtFBName" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBName" width="300" maxlength="50" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblFBAddress" runat="server" ResourceKey="lblFBAddress" controlname="txtFBAddress" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBAddress" width="300" maxlength="50" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblFBCity" runat="server" ResourceKey="lblFBCity" controlname="txtFBCity" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBCity" width="300" maxlength="50" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblFBState" runat="server" ResourceKey="lblFBState" controlname="txtFBState" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBState" width="300" maxlength="50" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblFBZipCode" runat="server" ResourceKey="lblFBZipCode" controlname="txtFBZipCode" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBZipCode" width="300" maxlength="50" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblFBPhoneNumber" runat="server" ResourceKey="lblFBPhoneNumber" controlname="txtFBPhoneNumber" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBPhoneNumber" width="300" maxlength="50" runat="server" /></td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblFBFaxNumber" runat="server" ResourceKey="lblFBFaxNumber" controlname="txtFBFaxNumber" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFBFaxNumber" width="300" maxlength="50" runat="server" /></td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblAllowedIPAddress" runat="server" ResourceKey="lblAllowedIPAddress" controlname="txtAllowedIPAddress" suffix=":" /></td>
        <td><asp:TextBox ID="txtAllowedIPAddress" cssclass="NormalTextBox" runat="server" /></td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblGoogleAPIKey" runat="server" ResourceKey="lblGoogleAPIKey" controlname="txtGoogleAPIKey" suffix=":" /></td>
        <td><asp:TextBox ID="txtGoogleAPIKey" cssclass="NormalTextBox" runat="server" /></td>
    </tr>		

</table>


<dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="General Settings" section="tblGeneralSettings"
	includerule="False"></dnn:sectionhead>	



<table id="tblGeneralSettings" runat="server" cellspacing="0" cellpadding="2" border="0" summary="General Settings">	
    <tr>
        <td class="SubHead"><dnn:label id="lblIDCardImagePath" runat="server" ResourceKey="lblIDCardImagePath" controlname="txtIDCardImagePath" suffix=":" /></td>
        <td><asp:textbox id="txtIDCardImagePath" width="300" runat="server" /></td>
    </tr>	    
    
    <tr>
        <td class="SubHead"><dnn:label id="lblDaysToValidVisit" runat="server" controlname="ddlDaysToValidVisit" suffix=":"></dnn:label></td>
        <td><asp:DropDownList ID="ddlDaysToValidVisit" runat="server">
        </asp:DropDownList></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblShowClientServiceLocation" runat="server" controlname="cbxShowClientServiceLocation" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowClientServiceLocation" runat="server" />
        </td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblShowClientSuffix" runat="server" controlname="cbxShowSuffix" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowSuffix" runat="server" />
        </td>
    </tr>	



    <tr>
        <td class="SubHead"><dnn:label id="lblShowRelationshipToClient" runat="server" controlname="cbxShowRelationshipToClient" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowRelationshipToClient" runat="server" />
        </td>
    </tr>	
	



    <tr>
        <td class="SubHead"><dnn:label id="lblShowPhotoID" runat="server" controlname="cbxShowPhotoID" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowPhotoID" runat="server" />
        </td>
    </tr>		

    <tr>
        <td class="SubHead"><dnn:label id="lblShowOneBagOnly" runat="server" controlname="cbxShowOneBagOnly" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowOneBagOnly" runat="server" />
        </td>
    </tr>	

		
    <tr>
        <td class="SubHead"><dnn:label id="lblShowVisitStopLight" runat="server" controlname="cbxShowVisitStopLight" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowVisitStopLight" runat="server" />
        </td>
    </tr>

     <tr>
        <td class="SubHead"><dnn:label id="lblShowIncExpSummary" runat="server" controlname="cbxShowIncExpSummary" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowIncExpSummary" runat="server" /></td>
    </tr>
     <tr>
        <td class="SubHead"><dnn:label id="lblShowExpense" runat="server" controlname="cbxShowExpense" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowExpense" runat="server" /></td>
    </tr>
    
     <tr>
        <td class="SubHead"><dnn:label id="lblShowDisabilities" runat="server" controlname="cbxShowDisabilities" suffix=":" /></td>
        <td><asp:CheckBox ID="cbxShowDisabilities" runat="server" /></td>
    </tr>    	  
    
    <tr>
        <td class="SubHead"><dnn:label id="lblShowClientType" runat="server" controlname="cbxShowClientType" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowClientType" runat="server" /></td>
    </tr>

    		     <tr>
        <td class="SubHead"><dnn:label id="lblShowClientIdCard" runat="server" controlname="cbxShowClientIdCard" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowClientIdCard" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblShowPrintShoppingLabel" runat="server" controlname="cbxShowPrintShoppingLabel" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowPrintShoppingLabel" runat="server" /></td>
    </tr>
    <tr>
        <td class="SubHead"><dnn:label id="lblPrintShoppingLabelQuantity" runat="server" controlname="txtPrintShoppingLabelQuantity" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtPrintShoppingLabelQuantity" runat="server" width="300" Text="1" /></td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblShowPrintBarCodeLabel" runat="server" controlname="cbxShowPrintBarCodeLabel" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowPrintBarCodeLabel" runat="server" /></td>
    </tr>	
   
     <tr>
        <td class="SubHead"><dnn:label id="lblIncludeInactiveOnSearch" runat="server" controlname="cbxIncludeInactiveOnSearch" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxIncludeInactiveOnSearch" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead"><dnn:label id="lblFlagForReviewNotify" runat="server" ResourceKey="lblFlagForReviewNotify" controlname="txtFlagForReviewNotify" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtFlagForReviewNotify" runat="server" /></td>
    </tr>	

    <tr>
        <td class="SubHead"><dnn:label id="lblFocusableControl" runat="server" controlname="ddlFocusableControl" suffix=":"></dnn:label></td>
        <td><asp:DropDownList ID="ddlFocusableControl" runat="server">
        <asp:ListItem Text="ClientId" Value="txtClientId"></asp:ListItem>
        <asp:ListItem Text="Last Name" Value="txtLastName"></asp:ListItem>
        <asp:ListItem Text="First Name" Value="txtFirstName"></asp:ListItem>
        <asp:ListItem Text="" Value=""></asp:ListItem>
        </asp:DropDownList></td>
    </tr>    

        <tr>
        <td class="SubHead"><dnn:label id="lblBagAllowance" runat="server" ResourceKey="lblBagAllowance" controlname="txtBagAllowance" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtBagAllowance" runat="server" width="300" /></td>
    </tr>

	    </tr>    

        <tr>
        <td class="SubHead"><dnn:label id="lblGroupHomeBagAllowance" runat="server" ResourceKey="lblGroupHomeBagAllowance" controlname="txtGroupHomeBagAllowance" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtGroupHomeBagAllowance" runat="server" width="300" /></td>
    </tr>

    	
</table>	


<dnn:sectionhead id="sectTwilio" cssclass="Head" runat="server" text="Twilio Fields" section="tblTwilio" IsExpanded="true"
	includerule="False"></dnn:sectionhead>	



<table id="tblTwilio" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Twilio Fields">	
   <tr>
   <td class="SubHead"><dnn:label id="lblTwilioAccountSid" runat="server" controlname="txtTwilioAccountSid" suffix=":" /></td>

        
         <td><asp:TextBox ID="txtTwilioAccountSid" cssclass="NormalTextBox" runat="server" /></td>
    </tr>

   <tr>
   <td class="SubHead"><dnn:label id="lblTwilioAuthToken" runat="server" controlname="txtTwilioAuthToken" suffix=":" /></td>

        
         <td><asp:TextBox ID="txtTwilioAuthToken" cssclass="NormalTextBox" runat="server" /></td>
    </tr>		
    <tr>
        <td class="SubHead"><dnn:label id="lblTwilioPhoneNumber" runat="server" controlname="txtTwilioPhoneNumber" suffix=":"></dnn:label></td>
         <td><asp:TextBox ID="txtTwilioPhoneNumber" cssclass="NormalTextBox" runat="server" /></td>
    </tr>	
    <tr>
        <td class="SubHead"><dnn:label id="lblClientOrderPage" runat="server" controlname="txtClientOrderPage" suffix=":"></dnn:label></td>
         <td><asp:TextBox ID="txtClientOrderPage" cssclass="NormalTextBox" runat="server" /></td>
    </tr>		
</table>


<dnn:sectionhead id="sectToyTicket" cssclass="Head" runat="server" text="Christmas Toys" section="tblToyTicket" IsExpanded="false"
	includerule="False"></dnn:sectionhead>	



<table id="tblToyTicket" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Christmas Toys">	
    <tr>
        <td class="SubHead"><dnn:label id="lblShowXmasToys" runat="server" controlname="cbxShowXmasToys" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxShowXmasToys" runat="server" /></td>
    </tr>

    <tr>
        <td class="SubHead"><dnn:label id="lblXmasToysYear" runat="server" ResourceKey="lblXmasToysYear" controlname="txtXmasToysYear" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtXmasToysYear" width="300" maxlength="4" runat="server" /></td>
    </tr>
    <tr>
        <td class="SubHead"><dnn:label id="lblXmasRequireSizeAgeRange" runat="server" ResourceKey="lblXmasRequireSizeAgeRange" controlname="txtXmasRequireSizeAgeRange" suffix=":"></dnn:label></td>
        <td><asp:textbox id="txtXmasRequireSizeAgeRange" width="300" maxlength="5" runat="server" /></td>
    </tr>
    <tr>
   <td class="SubHead"><dnn:label id="lblShowXmasGiftFields" runat="server" controlname="cbxShowXmasGiftFields" suffix=":" /></td>

        
        <td><asp:CheckBox ID="cbxShowXmasGiftFields" runat="server" /></td>
    </tr>	 
    <tr>
   <td class="SubHead"><dnn:label id="lblToyTicketContent" runat="server" controlname="txtToyTicketContent" suffix=":"></dnn:label></td>
  
        
        <td><dnn:texteditor id="txtToyTicketContent" runat="server" height="300" width="100%" /></td>
    </tr>

		

	
</table>


<dnn:sectionhead id="sectInstructions" cssclass="Head" runat="server" text="Instructions" section="tblInstructions" IsExpanded="false"
	includerule="False"></dnn:sectionhead>	



<table id="tblInstructions" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Instructions">	
   <tr>
   <td class="SubHead"><dnn:label id="lblIncomeEligibilityGuidelines" runat="server" controlname="txtIncomeEligibilityGuidelines" suffix=":"></dnn:label></td>
   </tr>
    <tr>
        
        <td><dnn:texteditor id="txtIncomeEligibilityGuidelines" runat="server" height="300" width="100%" /></td>
    </tr>

		

	
</table>


<dnn:sectionhead id="sectRequiredFields" cssclass="Head" runat="server" text="Required Fields" section="tblRequiredFields" IsExpanded="true"
	includerule="False"></dnn:sectionhead>	



<table id="tblRequiredFields" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Required Fields">	
   <tr>
   <td class="SubHead"><dnn:label id="lblReguireClientGender" runat="server" controlname="cbxReguireClientGender" suffix=":" /></td>

        
        <td><asp:CheckBox ID="cbxReguireClientGender" runat="server" /></td>
    </tr>

   <tr>
   <td class="SubHead"><dnn:label id="lblRequireClientEthnicity" runat="server" controlname="cbxRequireClientEthnicity" suffix=":" /></td>

        
        <td><asp:CheckBox ID="cbxRequireClientEthnicity" runat="server" /></td>
    </tr>		
    <tr>
        <td class="SubHead"><dnn:label id="lblReqAFMVerified" runat="server" controlname="cbxReqAFMVerified" suffix=":"></dnn:label></td>
        <td><asp:CheckBox ID="cbxReqAFMVerified" runat="server" />
        </td>
    </tr>	
	
</table>



<dnn:sectionhead id="sectPermissions" cssclass="Head" runat="server" text="Permissions" section="tblPermissions"
	includerule="False"></dnn:sectionhead>	



<table id="tblPermissions" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Permissions">	
    <tr>
        <td class="SubHead"><dnn:label id="lblClientManagerUserRole" runat="server" controlname="ddlClientManagerUserRole" suffix=":"></dnn:label></td>
        <td><asp:DropDownList ID="ddlClientManagerUserRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName" Enabled="true">
        </asp:DropDownList></td>
    </tr>

		
    <tr>
        <td class="SubHead"><dnn:label id="lblClientManagerDeleteRole" runat="server" controlname="ddlClientManagerDeleteRole" suffix=":"></dnn:label></td>
        <td><asp:DropDownList ID="ddlClientManagerDeleteRole" runat="server" datavaluefield="RoleName" datatextfield="RoleName">
        </asp:DropDownList></td>
    </tr>
	
</table>

