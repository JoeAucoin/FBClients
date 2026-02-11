using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.FBClients.Components;
using System.Collections;
using System.Web.UI.WebControls;

namespace GIBS.Modules.FBClients
{
    public partial class Settings : FBClientsSettings
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {

                    GetRoles();

                    
                //    FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);


                    if (IDCardImagePath != null)
                    {
                        txtIDCardImagePath.Text = IDCardImagePath;
                    }

                    if (AllowedIPAddress != null)
                    {
                        txtAllowedIPAddress.Text = AllowedIPAddress;
                    }

                    if (GoogleAPIKey != null)
                    {
                        txtGoogleAPIKey.Text = GoogleAPIKey;
                    }

                    if (ClientManagerUserRole != null)
                    {

                        ddlClientManagerUserRole.SelectedValue = ClientManagerUserRole;
                    }

                    if (ClientManagerDeleteRecordRole != null)
                    {

                        ddlClientManagerDeleteRole.SelectedValue = ClientManagerDeleteRecordRole;
                    }
                    
                    if (FBName != null)
                    {
                        txtFBName.Text = FBName;
                    }

                    if (FBAddress != null)
                    {
                        txtFBAddress.Text = FBAddress;
                    }

                    if (FBCity != null)
                    {
                        txtFBCity.Text = FBCity;
                    }

                    if (FBState != null)
                    {
                        txtFBState.Text = FBState;
                    }

                    if (FBZipCode != null)
                    {
                        txtFBZipCode.Text = FBZipCode;
                    }

                    if (FBPhoneNumber != null)
                    {
                        txtFBPhoneNumber.Text = FBPhoneNumber;
                    }

                    if (FBFaxNumber != null)
                    {
                        txtFBFaxNumber.Text = FBFaxNumber;
                    }

                    if (DaysToValidVisit != null)
                    {
                        ddlDaysToValidVisit.SelectedValue = DaysToValidVisit;
                    }


                    if (ShowSendText != null)
                    {
                        cbxShowSendText.Checked = Convert.ToBoolean(ShowSendText);
                    }

                    if (ShowClientServiceLocation != null)
                    {
                        cbxShowClientServiceLocation.Checked = Convert.ToBoolean(ShowClientServiceLocation);
                    }

                    if (ShowSuffix != null)
                    {
                        cbxShowSuffix.Checked = Convert.ToBoolean(ShowSuffix);

                    }


                    if (ShowRelationshipToClient != null)
                    {
                        cbxShowRelationshipToClient.Checked = Convert.ToBoolean(ShowRelationshipToClient);

                    }

                    if(ShowRequiredFields != null)
                    {
                        cbxShowRequiredFields.Checked = Convert.ToBoolean(ShowRequiredFields);
                    }

                    if (ShowPhotoID != null)
                    {
                        cbxShowPhotoID.Checked = Convert.ToBoolean(ShowPhotoID);
                    }

                    if (ShowOneBagOnly != null)
                    {
                        cbxShowOneBagOnly.Checked = Convert.ToBoolean(ShowOneBagOnly);
                    }	

                    if (ShowVisitStopLight != null)
                    {
                        cbxShowVisitStopLight.Checked = Convert.ToBoolean(ShowVisitStopLight);
                    }
                    
                    if (IncomeEligibilityGuidelines != null)
                    {
                        txtIncomeEligibilityGuidelines.Text = IncomeEligibilityGuidelines;
                    }

                    if (ShowIncExpSummary != null)
                    {
                        cbxShowIncExpSummary.Checked = Convert.ToBoolean(ShowIncExpSummary);
                    }
                    //IncludeInactiveOnSearch

                    if (IncludeInactiveOnSearch != null)
                    {
                        cbxIncludeInactiveOnSearch.Checked = Convert.ToBoolean(IncludeInactiveOnSearch);
                    }
           

                    if (ShowExpense != null)
                    {
                        cbxShowExpense.Checked = Convert.ToBoolean(ShowExpense);
                    }

                    if (ShowDisabilities != null)
                    {
                        cbxShowDisabilities.Checked = Convert.ToBoolean(ShowDisabilities);
                    }	

                    if (ShowClientType != null)
                    {
                        cbxShowClientType.Checked = Convert.ToBoolean(ShowClientType);
                    }

                    if (ShowXmasToys != null)
                    {
                        cbxShowXmasToys.Checked = Convert.ToBoolean(ShowXmasToys);
                    }
                    //ShowXmasGiftFields
                    if (ShowXmasGiftFields != null)
                    {
                        cbxShowXmasGiftFields.Checked = Convert.ToBoolean(ShowXmasGiftFields);
                    }

                    if (XmasToysYear != null)
                    {
                        txtXmasToysYear.Text = XmasToysYear;
                    }

                    if (XmasRequireSizeAgeRange != null)
                    {
                        txtXmasRequireSizeAgeRange.Text = XmasRequireSizeAgeRange;
                    }

                    if (ToyTicketContent != null)
                    {
                        txtToyTicketContent.Text = ToyTicketContent;
                    }

                    if (ShowClientIdCard != null)
                    {
                        cbxShowClientIdCard.Checked = Convert.ToBoolean(ShowClientIdCard);
                    }
                    if (ShowPrintShoppingLabel != null)
                    {

                        cbxShowPrintShoppingLabel.Checked = Convert.ToBoolean(ShowPrintShoppingLabel);
                    }

                    if (ShowPrintBarCodeLabel != null)
                    {
                        cbxShowPrintBarCodeLabel.Checked = Convert.ToBoolean(ShowPrintBarCodeLabel);
                    }
                    //     txtPrintShoppingLabelQuantity
                    if (PrintShoppingLabelQuantity != null)
                    {
                        txtPrintShoppingLabelQuantity.Text = PrintShoppingLabelQuantity;
                    }

                    if (FocusableControl != null)
                    {
                        ddlFocusableControl.SelectedValue = FocusableControl;
                    }

                    //txtFlagForReviewNotify
                    if (FlagForReviewNotify != null)
                    {
                        txtFlagForReviewNotify.Text = FlagForReviewNotify;
                    }

                    //txtBagAllowance
                    if (BagAllowance != null)
                    {
                        txtBagAllowance.Text = BagAllowance;
                    }
                    if (GroupHomeBagAllowance != null)
                    {
                        txtGroupHomeBagAllowance.Text = GroupHomeBagAllowance;
                    }
                    // REQUIRED FIELDS
                    if (ReqZipCode != null)
                    {
                        cbxReguireZipCode.Checked = Convert.ToBoolean(ReqZipCode);
                    }

                    if (ReqGender != null)
                    {
                        cbxReguireClientGender.Checked = Convert.ToBoolean(ReqGender);
                    }


                    if (ReqEthnicity != null)
                    {
                        cbxRequireClientEthnicity.Checked = Convert.ToBoolean(ReqEthnicity);
                    }


                    if (ShowRelationshipToClient != null)
                    {
                        cbxShowRelationshipToClient.Checked = Convert.ToBoolean(ReqAFMRelationship);
                    }

                    if (ReqAFMVerified != null)
                    {
                        cbxReqAFMVerified.Checked = Convert.ToBoolean(ReqAFMVerified);
                    }

                    if (TwilioAccountSid != null)
                    {
                        txtTwilioAccountSid.Text = TwilioAccountSid;
                    }

                    if (TwilioAuthToken != null)
                    {
                        txtTwilioAuthToken.Text = TwilioAuthToken;
                    }

                    if (TwilioPhoneNumber != null)
                    {
                        txtTwilioPhoneNumber.Text = TwilioPhoneNumber;
                    }

                    if (ClientOrderPage != null)
                    {
                        txtClientOrderPage.Text = ClientOrderPage;
                    }

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
              //  FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);
                ClientManagerUserRole = ddlClientManagerUserRole.SelectedValue;
                ClientManagerDeleteRecordRole = ddlClientManagerDeleteRole.SelectedValue;
                   
                FBName = txtFBName.Text;
                FBAddress = txtFBAddress.Text;
                FBCity = txtFBCity.Text;
                FBState = txtFBState.Text;
                FBZipCode = txtFBZipCode.Text;
                FBPhoneNumber = txtFBPhoneNumber.Text;
                FBFaxNumber = txtFBFaxNumber.Text;
                ShowVisitStopLight = cbxShowVisitStopLight.Checked.ToString();
                DaysToValidVisit = ddlDaysToValidVisit.SelectedValue.ToString();
                IncomeEligibilityGuidelines = txtIncomeEligibilityGuidelines.Text.ToString();
                ShowSendText= cbxShowSendText.Checked.ToString();
                ShowClientServiceLocation = cbxShowClientServiceLocation.Checked.ToString();
                ShowPhotoID = cbxShowPhotoID.Checked.ToString();
                ShowOneBagOnly = cbxShowOneBagOnly.Checked.ToString();
                ShowIncExpSummary = cbxShowIncExpSummary.Checked.ToString();
                ShowExpense = cbxShowExpense.Checked.ToString();
                ShowDisabilities = cbxShowDisabilities.Checked.ToString();
                ShowClientType = cbxShowClientType.Checked.ToString();
                ShowXmasToys = cbxShowXmasToys.Checked.ToString();
                ShowXmasGiftFields = cbxShowXmasGiftFields.Checked.ToString();
                IncludeInactiveOnSearch = cbxIncludeInactiveOnSearch.Checked.ToString();
                FocusableControl = ddlFocusableControl.SelectedValue.ToString();
                ShowClientIdCard = cbxShowClientIdCard.Checked.ToString();
                ShowPrintShoppingLabel = cbxShowPrintShoppingLabel.Checked.ToString();
                PrintShoppingLabelQuantity = txtPrintShoppingLabelQuantity.Text.ToString();
                ShowPrintBarCodeLabel = cbxShowPrintBarCodeLabel.Checked.ToString();
                XmasToysYear = txtXmasToysYear.Text;
                FlagForReviewNotify = txtFlagForReviewNotify.Text.ToString();
                BagAllowance = txtBagAllowance.Text.ToString();
                GroupHomeBagAllowance = txtGroupHomeBagAllowance.Text.ToString();
                ReqEthnicity = cbxRequireClientEthnicity.Checked.ToString();
                ReqGender = cbxReguireClientGender.Checked.ToString();
                ReqZipCode = cbxReguireZipCode.Checked.ToString();
                AllowedIPAddress = txtAllowedIPAddress.Text.ToString();
                GoogleAPIKey = txtGoogleAPIKey.Text.ToString();
                XmasRequireSizeAgeRange = txtXmasRequireSizeAgeRange.Text.ToString();
                ToyTicketContent = txtToyTicketContent.Text.ToString();
                ReqAFMRelationship = cbxShowRelationshipToClient.Checked.ToString();
                ReqAFMVerified = cbxReqAFMVerified.Checked.ToString();
                ShowRelationshipToClient = cbxShowRelationshipToClient.Checked.ToString();
                ShowSuffix = cbxShowSuffix.Checked.ToString();
                IDCardImagePath = txtIDCardImagePath.Text.ToString();
                TwilioAccountSid = txtTwilioAccountSid.Text.ToString();
                TwilioAuthToken = txtTwilioAuthToken.Text.ToString();
                TwilioPhoneNumber = txtTwilioPhoneNumber.Text.ToString();
                ClientOrderPage = txtClientOrderPage.Text.ToString();
                ShowRequiredFields = cbxShowRequiredFields.Checked.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void GetRoles()
        {
            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
            var myRoles = rc.GetRoles(this.PortalId);


            ddlClientManagerUserRole.DataSource = myRoles;
            ddlClientManagerUserRole.DataBind();

            ddlClientManagerDeleteRole.DataSource = myRoles;
            ddlClientManagerDeleteRole.DataBind();
            

            // ADD FIRST (NULL) ITEM
            ListItem item = new ListItem();
            item.Text = "-- Select Role to Assign --";
            item.Value = "";
            ddlClientManagerUserRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlClientManagerUserRole.Items.Remove("Administrators");
            ddlClientManagerUserRole.Items.Remove("Registered Users");
            ddlClientManagerUserRole.Items.Remove("Subscribers");


            ddlClientManagerDeleteRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlClientManagerDeleteRole.Items.Remove("Administrators");
            ddlClientManagerDeleteRole.Items.Remove("Registered Users");
            ddlClientManagerDeleteRole.Items.Remove("Subscribers");


            for (int i = 0; i < 30; i++)
            {
                ddlDaysToValidVisit.Items.Insert(i, new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }



        }


    }
}