using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.FBClients.Components;
using System.Collections;
using System.Web.UI.WebControls;

namespace GIBS.Modules.FBClients
{
    public partial class Settings : ModuleSettingsBase
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
                    
                    FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);

                    if (settingsData.AllowedIPAddress != null)
                    {
                        txtAllowedIPAddress.Text = settingsData.AllowedIPAddress;
                    }
                    
                    if (settingsData.ClientManagerUserRole != null)
                    {

                        ddlClientManagerUserRole.SelectedValue = settingsData.ClientManagerUserRole;
                    }

                    if (settingsData.ClientManagerDeleteRecordRole != null)
                    {

                        ddlClientManagerDeleteRole.SelectedValue = settingsData.ClientManagerDeleteRecordRole;
                    }
                    
                    if (settingsData.FBName != null)
                    {
                        txtFBName.Text = settingsData.FBName;
                    }

                    if (settingsData.FBAddress != null)
                    {
                        txtFBAddress.Text = settingsData.FBAddress;
                    }

                    if (settingsData.FBCity != null)
                    {
                        txtFBCity.Text = settingsData.FBCity;
                    }

                    if (settingsData.FBState != null)
                    {
                        txtFBState.Text = settingsData.FBState;
                    }

                    if (settingsData.FBZipCode != null)
                    {
                        txtFBZipCode.Text = settingsData.FBZipCode;
                    }

                    if (settingsData.FBPhoneNumber != null)
                    {
                        txtFBPhoneNumber.Text = settingsData.FBPhoneNumber;
                    }

                    if (settingsData.FBFaxNumber != null)
                    {
                        txtFBFaxNumber.Text = settingsData.FBFaxNumber;
                    }

                    if (settingsData.DaysToValidVisit != null)
                    {
                        ddlDaysToValidVisit.SelectedValue = settingsData.DaysToValidVisit;
                    }

                    if (settingsData.ShowPhotoID != null)
                    {
                        cbxShowPhotoID.Checked = Convert.ToBoolean(settingsData.ShowPhotoID);
                    }

                    if (settingsData.ShowOneBagOnly != null)
                    {
                        cbxShowOneBagOnly.Checked = Convert.ToBoolean(settingsData.ShowOneBagOnly);
                    }	

                    if (settingsData.ShowVisitStopLight != null)
                    {
                        cbxShowVisitStopLight.Checked = Convert.ToBoolean(settingsData.ShowVisitStopLight);
                    }
                    
                    if (settingsData.IncomeEligibilityGuidelines != null)
                    {
                        txtIncomeEligibilityGuidelines.Text = settingsData.IncomeEligibilityGuidelines;
                    }

                    if (settingsData.ShowIncExpSummary != null)
                    {
                        cbxShowIncExpSummary.Checked = Convert.ToBoolean(settingsData.ShowIncExpSummary);
                    }

                    if (settingsData.ShowExpense != null)
                    {
                        cbxShowExpense.Checked = Convert.ToBoolean(settingsData.ShowExpense);
                    }

                    if (settingsData.ShowDisabilities != null)
                    {
                        cbxShowDisabilities.Checked = Convert.ToBoolean(settingsData.ShowDisabilities);
                    }	

                    if (settingsData.ShowClientType != null)
                    {
                        cbxShowClientType.Checked = Convert.ToBoolean(settingsData.ShowClientType);
                    }

                    if (settingsData.ShowXmasToys != null)
                    {
                        cbxShowXmasToys.Checked = Convert.ToBoolean(settingsData.ShowXmasToys);
                    }
                    if (settingsData.XmasToysYear != null)
                    {
                        txtXmasToysYear.Text = settingsData.XmasToysYear;
                    }

                    if (settingsData.XmasRequireSizeAgeRange != null)
                    {
                        txtXmasRequireSizeAgeRange.Text = settingsData.XmasRequireSizeAgeRange;
                    }

                    if (settingsData.ToyTicketContent != null)
                    {
                        txtToyTicketContent.Text = settingsData.ToyTicketContent;
                    }

                    if (settingsData.ShowClientIdCard != null)
                    {
                        cbxShowClientIdCard.Checked = Convert.ToBoolean(settingsData.ShowClientIdCard);
                    }

                    if (settingsData.FocusableControl != null)
                    {
                        ddlFocusableControl.SelectedValue = settingsData.FocusableControl;
                    }

                    //txtFlagForReviewNotify
                    if (settingsData.FlagForReviewNotify != null)
                    {
                        txtFlagForReviewNotify.Text = settingsData.FlagForReviewNotify;
                    }

                    //txtBagAllowance
                    if (settingsData.BagAllowance != null)
                    {
                        txtBagAllowance.Text = settingsData.BagAllowance;
                    }
                    if (settingsData.GroupHomeBagAllowance != null)
                    {
                        txtGroupHomeBagAllowance.Text = settingsData.GroupHomeBagAllowance;
                    }
                    // REQUIRED FIELDS
                    if (settingsData.ReqGender != null)
                    {
                        cbxReguireClientGender.Checked = Convert.ToBoolean(settingsData.ReqGender);
                    }


                    if (settingsData.ReqEthnicity != null)
                    {
                        cbxRequireClientEthnicity.Checked = Convert.ToBoolean(settingsData.ReqEthnicity);
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
                FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);
                settingsData.ClientManagerUserRole = ddlClientManagerUserRole.SelectedValue;
                settingsData.ClientManagerDeleteRecordRole = ddlClientManagerDeleteRole.SelectedValue;
                   
                settingsData.FBName = txtFBName.Text;
                settingsData.FBAddress = txtFBAddress.Text;
                settingsData.FBCity = txtFBCity.Text;
                settingsData.FBState = txtFBState.Text;
                settingsData.FBZipCode = txtFBZipCode.Text;
                settingsData.FBPhoneNumber = txtFBPhoneNumber.Text;
                settingsData.FBFaxNumber = txtFBFaxNumber.Text;
                settingsData.ShowVisitStopLight = cbxShowVisitStopLight.Checked.ToString();
                settingsData.DaysToValidVisit = ddlDaysToValidVisit.SelectedValue.ToString();
                settingsData.IncomeEligibilityGuidelines = txtIncomeEligibilityGuidelines.Text.ToString();
                settingsData.ShowPhotoID = cbxShowPhotoID.Checked.ToString();
                settingsData.ShowOneBagOnly = cbxShowOneBagOnly.Checked.ToString();
                settingsData.ShowIncExpSummary = cbxShowIncExpSummary.Checked.ToString();
                settingsData.ShowExpense = cbxShowExpense.Checked.ToString();
                settingsData.ShowDisabilities = cbxShowDisabilities.Checked.ToString();
                settingsData.ShowClientType = cbxShowClientType.Checked.ToString();
                settingsData.ShowXmasToys = cbxShowXmasToys.Checked.ToString();
                settingsData.FocusableControl = ddlFocusableControl.SelectedValue.ToString();
                settingsData.ShowClientIdCard = cbxShowClientIdCard.Checked.ToString();
                settingsData.XmasToysYear = txtXmasToysYear.Text;
                settingsData.FlagForReviewNotify = txtFlagForReviewNotify.Text.ToString();
                settingsData.BagAllowance = txtBagAllowance.Text.ToString();
                settingsData.GroupHomeBagAllowance = txtGroupHomeBagAllowance.Text.ToString();
                settingsData.ReqEthnicity = cbxRequireClientEthnicity.Checked.ToString();
                settingsData.ReqGender = cbxReguireClientGender.Checked.ToString();
                settingsData.AllowedIPAddress = txtAllowedIPAddress.Text.ToString();
                settingsData.XmasRequireSizeAgeRange = txtXmasRequireSizeAgeRange.Text.ToString();
                settingsData.ToyTicketContent = txtToyTicketContent.Text.ToString();
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