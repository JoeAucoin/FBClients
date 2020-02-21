using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security.Profile;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Services.Mail;
using DotNetNuke.UI.WebControls;
using DotNetNuke.UI.UserControls;
using DotNetNuke.UI.Utilities;

using GIBS.FBClients.Components;
using DotNetNuke.Common.Lists;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using DotNetNuke.Entities.Modules.Actions;
using System.Collections;
using System.Web;
using System.Data;
using System.ComponentModel;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Social.Notifications;
using System.Text;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Framework.JavaScriptLibraries;

namespace GIBS.Modules.FBClients
{
    public partial class ClientMerge : PortalModuleBase//, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // lblMsg.Text = Request.QueryString["clientid"];
            try
            {
                if (IsPostBack==false)
                {
                    GetLists();
                    LoadSettings();

                    if (Request.QueryString["clientid"] != null)
                    {
                        hidClientIDMaster.Value = Request.QueryString["clientid"].ToString();

                        LoadMaster();
                    }
                }




                
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            JavaScript.RequestRegistration(CommonJs.DnnPlugins);
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
        public void LoadMaster()
        {
            try
            {
                //ClientAge
                GridViewSearchMaster.Columns[8].Visible = true;
                //ClientDOB
                GridViewSearchMaster.Columns[9].Visible = true;


                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_Search(this.PortalId, txtLastName.Text.ToString().Replace("'", "''").Trim(),
                    "",
                    "",
                    hidClientIDMaster.Value,
                    "",
                    "", "", "0");

                GridViewSearchMaster.DataSource = items;
                GridViewSearchMaster.DataBind();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }
        public void LoadChild()
        {
            try
            {

                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();
                items = controller.FBClients_Search(this.PortalId, txtLastName.Text.ToString().Replace("'", "''").Trim(),
                    txtClientIdCard.Text.ToString().Trim(),
                    txtFirstName.Text.ToString().Replace("'", "''").Trim(),
                    txtClientId.Text.ToString(),
                    txtAddress.Text.ToString(),
                    ddlCity.SelectedValue.ToString(), "", "0");
                GridViewSearchChild.DataSource = items;
                GridViewSearchChild.DataBind();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }
        protected void GridViewSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        public void LoadSettings()
        {
            try
            {
                FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);
                if (settingsData.FocusableControl != null)
                {
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
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        protected void GridViewSearch_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void GridViewSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                LoadChild();
                GridViewSearchChild.PageIndex = e.NewPageIndex;
                GridViewSearchChild.DataBind();
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
               // lblFormMessage.Visible = false;
                LoadChild();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        protected void Merge( int MasterClientID, int ChildClientID)
        {
            FBClientsController controller = new FBClientsController();
            int result = controller.FBClients_Merge(MasterClientID, ChildClientID);
            if (result == 1)
            {

                lblMsg.Text = "Merge was done.";

            }
            else
            {
                lblMsg.Text = "Merge blew up.";


            }
        }
        protected void GridViewSearchChild_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            ////Get the row that contains this button
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            int selecteClient = Convert.ToInt32(btn.CommandArgument); 

            //lblMsg.Text = gvr.Cells[1].Text;
            if (Convert.ToInt32(hidClientIDMaster.Value) != selecteClient)
            {
                Merge(Convert.ToInt32(hidClientIDMaster.Value), selecteClient);
                // RELOAD CHILD GRID
                LoadChild();

            }
            else
            {
                lblMsg.Text = "You can't merge a client unto itself. Nice try tho. Please select another Client.";    
            }
        }
        //
        protected void GridViewSearchChild_SelectedIndexView(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;
        //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //lblMsg.Text = gvr.Cells[1].Text;
           // Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + gvr.Cells[1].Text));
        }
       
    }
}
