using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.FBClients.Components;
using DotNetNuke.Common;
using DotNetNuke.Common.Lists;

namespace GIBS.Modules.FBClients
{
    public partial class ViewFBClients : PortalModuleBase, IActionable
    {

     //   bool ShowAMFinGrid = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    GetLists();

               //     txtLastName.Focus();
                //    txtClientId.Focus();
                    LoadSettings();

                    if (Request.QueryString["clientid"] != null)
                    {
                        txtClientId.Text = Request.QueryString["clientid"].ToString();
                    }
                    if (Request.QueryString["address"] != null)
                    {
                        txtAddress.Text = Request.QueryString["address"].ToString();
                    }

                    if (Request.QueryString["fname"] != null)
                    {
                        txtFirstName.Text = Request.QueryString["fname"].ToString();
                    }

                    if (Request.QueryString["lname"] != null)
                    {
                        txtLastName.Text = Request.QueryString["lname"].ToString();
                        
                    }

                    if (Request.QueryString["inactive"] != null)
                    {
                        cbxIncludeInactive.Checked = true;
                    }

                    if (Request.QueryString["lname"] != null || Request.QueryString["fname"] != null || Request.QueryString["address"] != null || Request.QueryString["clientid"] != null)
                    {
                        SearchClients();
                    }


                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void LoadSettings()
        {
            try
            {


                FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);


                if (settingsData.FocusableControl != null)
                {
                    //Control control = this.FindControl(
                        //(Control)this.FindName("FocusableControl", this);
                    TextBox _SearchField = (TextBox)FindControl(settingsData.FocusableControl.ToString());
                    if (_SearchField != null)
                    {
                        _SearchField.Focus();
                    }
                }

                if (settingsData.ShowClientIdCard != null)
                {
                    divClientIDCard.Visible = Convert.ToBoolean(settingsData.ShowClientIdCard);
                }

                if (settingsData.IncludeInactiveOnSearch != null)
                {
                    cbxIncludeInactive.Checked = Convert.ToBoolean(settingsData.IncludeInactiveOnSearch);
                   
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void GetLists()
        {

            try
            {

                var town = new ListController().GetListEntryInfoItems("Town", "", this.PortalId);

                ddlCity.DataTextField = "Text";
                ddlCity.DataValueField = "Value";
                ddlCity.DataSource = town;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("-", ""));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                //actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                //    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                //     true, false);

                return actions;
            }
        }

        #endregion

        public void SearchClients()

        {

            try
            {
                // DEAL WITH INCLUDE INACTIVE CHECKBOX
                string Should_I_IncludeInactives = "";
                if (cbxIncludeInactive.Checked)
                {
                    Should_I_IncludeInactives = "0";
                }
                else
                {
                    Should_I_IncludeInactives = "1";
                }


                //ClientAge
                GridViewSearch.Columns[10].Visible = true;
                //ClientDOB
                GridViewSearch.Columns[11].Visible = true;

                
                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_Search(this.PortalId, txtLastName.Text.ToString().Replace("'","''").Trim(), 
                    txtClientIdCard.Text.ToString().Trim(),
                    txtFirstName.Text.ToString().Replace("'", "''").Trim(), 
                    txtClientId.Text.TrimStart(new Char[] { '0' }).ToString(),
                    txtAddress.Text.ToString(),
                    ddlCity.SelectedValue.ToString(), ddlClientType.SelectedValue.ToString(), Should_I_IncludeInactives.ToString(), txtClientDOB.Text.ToString().Trim());

                GridViewSearch.DataSource = items;
                GridViewSearch.DataBind();

                //ClAddFamMemFirstName
                GridViewSearch.Columns[15].Visible = false;
                //ClAddFamMemLastName
                GridViewSearch.Columns[16].Visible = false;

                if (items.Count == 1)
                {
                    string clientID = items[0].ClientID.ToString();
                  //  lblFormMessage.Text = "Found 1 Record";
                 //   lblFormMessage.Visible = true;
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + clientID ));
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        
        }

        public void SearchClientAFMs()
        {

            try
            {

                // DEAL WITH INCLUDE INACTIVE CHECKBOX
                string Should_I_IncludeInactives = "";
                if (cbxIncludeInactive.Checked)
                {
                    Should_I_IncludeInactives = "0";
                }
                else
                {
                    Should_I_IncludeInactives = "1";
                }



                GridViewSearch.Columns[15].Visible = true;
                GridViewSearch.Columns[16].Visible = true;
                
                //ClientAge
                GridViewSearch.Columns[10].Visible = false;
                //ClientDOB
                GridViewSearch.Columns[11].Visible = false;

                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_Search_AFM(this.PortalId, txtLastNameAFM.Text.ToString().Replace("'", "''").Trim(), 
                    txtFirstNameAFM.Text.ToString().Replace("'", "''").Trim(), Should_I_IncludeInactives.ToString(), txtAFMDOB.Text.ToString());

                //GridViewSearch.Rows. = null;

                GridViewSearch.DataSource = items;
                GridViewSearch.DataBind();








            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void GridViewSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            FBClientsController controller = new FBClientsController();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblLastVisitDate = (Label)e.Row.FindControl("lblLastVisitDate");




                int _clientid = Convert.ToInt32(lblLastVisitDate.Text.ToString());

                FBClientsInfo item = controller.FBClients_Visit_GetClientLastVisitDate(_clientid);

                if (item != null)
                {
                    lblLastVisitDate.Text = item.LastVisitDate.ToShortDateString();
                }

                //if (ShowAMFinGrid == true)
                //{
                //    e.Row.Cells[14].Visible = true;
                //    e.Row.Cells[15].Visible = true;

                //}
                //else
                //{
                //    e.Row.Cells[14].Visible = false;
                //    e.Row.Cells[15].Visible = false;     
                //}


            }

            //if (ShowAMFinGrid == true)
            //{
            //    GridViewSearch.Columns[14].Visible = true;
            //    GridViewSearch.Columns[15].Visible = true;
            //}
            //else
            //{
            //    GridViewSearch.Columns[14].Visible = false;
            //    GridViewSearch.Columns[15].Visible = false;
            //}



        }



        protected void GridViewSearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                
                // USED TO GIVE A RETURN LINK TO THE SEARCH RESULTS
                string SearchParams = "";

                if (txtClientIdCard.Text.ToString().Length > 0)
                {
                    SearchParams += "&cidcard=" + txtClientIdCard.Text.ToString().Trim();
                }
                if (txtFirstName.Text.ToString().Length > 0)
                {
                    SearchParams += "&fname=" + txtFirstName.Text.ToString().Trim();
                }
                if (txtLastName.Text.ToString().Length > 0)
                {
                    SearchParams += "&lname=" + txtLastName.Text.ToString().Trim();
                }
                //
                

                if (txtAddress.Text.ToString().Length > 0)
                {
                    SearchParams += "&address=" + txtAddress.Text.ToString().Trim();
                }

                if (txtClientId.Text.ToString().Length > 0)
                {
                    SearchParams += "&clientid=" + txtClientId.Text.ToString().Trim();
                }

                if (cbxIncludeInactive.Checked)
                {
                    SearchParams += "&inactive=" + cbxIncludeInactive.Checked.ToString();
                }
                
                int clientID = (int)GridViewSearch.DataKeys[e.NewEditIndex].Value;
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + clientID + SearchParams.ToString()));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        protected void GridViewSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                if (txtFirstNameAFM.Text.ToString() == "" && txtLastNameAFM.Text.ToString() == "")
                {
                    SearchClients();
                }
                else
                {
                    SearchClientAFMs();
                }
                
                GridViewSearch.PageIndex = e.NewPageIndex;
                GridViewSearch.DataBind();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtFirstNameAFM.Text = "";
                txtLastNameAFM.Text = "";
                txtAFMDOB.Text = "";
                lblFormMessage.Visible = false;

                //    ShowAMFinGrid = false;

                if (txtClientId.Text.ToString().Length >= 1)
                {
                    string clientID = txtClientId.Text.ToString().Trim();
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + clientID));
                }
                else
                {
                    SearchClients();
                }

                



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnSearchAFM_Click(object sender, EventArgs e)
        {
            try
            {
                txtClientId.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtAddress.Text = "";
                ddlCity.ClearSelection();
                
                lblFormMessage.Visible = false;

              //  ShowAMFinGrid = true;

                SearchClientAFMs();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }



        protected void btnAddNewClient_Click(object sender, EventArgs e)
        {
            try
            {
          //      Response.Redirect(EditUrl("EditClient"));
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&TabFocus=Client"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }



    }
}