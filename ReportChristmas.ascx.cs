using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Abstractions;
using DotNetNuke.Entities.Modules;
using Microsoft.Extensions.DependencyInjection;
using DotNetNuke.Common.Utilities;
using GIBS.FBClients.Components;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using System.Text;

namespace GIBS.Modules.FBClients
{
    public partial class ReportChristmas : PortalModuleBase
    {
        private INavigationManager _navigationManager;
        int clientId = Null.NullInteger;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _navigationManager = DependencyProvider.GetRequiredService<INavigationManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            lblPrintDate.Text = "Generated: " + DateTime.Now.ToString("MM-dd-yyyy h:mmtt");

            if (Request.QueryString["cid"] != null)
            {
                clientId = Int32.Parse(Request.QueryString["cid"]);
            }

            if (!IsPostBack)
            {
                
                FillAFMGrid();
                FillClientRecord(clientId);
                LoadSettings();
            }

        }

        public void FillClientRecord(int clientID)
        {

            try
            {
                //    lblMessage.Text = "";


                //load the item
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_GetByID(this.PortalId, clientID);



                if (item != null)
                {
                    //  Response.Write(item);

                    StringBuilder builder = new StringBuilder();
                    string ClientInfo = "";


                    string _phoneType = "";


                    if (item.ClientPhoneType == "0")
                    {
                        _phoneType = "";
                    }

                    else
                    {
                        _phoneType = item.ClientPhoneType + " ";
                    }





                    //Set Page Name
                    SetPageName("Christmas Toys Voucher for " + item.ClientFirstName + " " + item.ClientLastName);


                    // QUICK VIEW SECTION
                    ClientInfo = item.ClientFirstName + " " + item.ClientLastName + " - " + item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode
                        + "<br />" + _phoneType.ToString() + "Phone: " + item.ClientPhone
                        
                        + "<br />Entry Date: " + item.CreatedOnDate.ToShortDateString()
                        + "<br /><font color='Red'>Client ID: " + item.ClientID + "</font>";

                    lblMessage.Text = ClientInfo.ToString();


                }
                else
                {
                    Response.Redirect(_navigationManager.NavigateURL(), true);
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void SetPageName(string ClientName)
        {
            try
            {

                DotNetNuke.Framework.CDefault CustomPageName = (DotNetNuke.Framework.CDefault)this.Page;

                CustomPageName.Title = ClientName.ToString();
                //   CustomPageName.KeyWords = vTitle.ToString() + "," + Settings["Keywords"].ToString();
                //  CustomPageName.Description = vTitle.ToString() + " " + Settings["PageTitle"].ToString() + ". " + Settings["PageDescription"].ToString();

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
                string _ToyTicketContent = "";


                if (Settings.Contains("toyTicketContent"))
                {

                    _ToyTicketContent = (Settings["toyTicketContent"].ToString());
                }
                else
                {
                    _ToyTicketContent = "<h1>We need content. Update Module Settings!</h1>";
                }


                LiteralToyTicketContent.Text = HttpUtility.HtmlDecode(_ToyTicketContent.ToString());




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void FillAFMGrid()
        {

            try
            {
                gvAFM.DataSource = "";
                // gvAFM.DataBind();

                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBxMas_AFM_PrintTicket(clientId);

                gvAFM.DataSource = items;
                gvAFM.DataBind();


                //bool _isValidBOB = true;
                //bool _isVerified = true;
                //foreach (GridViewRow tt in gvAFM.Rows)
                //{
                //    if (tt.RowType == DataControlRowType.DataRow)
                //    {
                //        string age = tt.Cells[7].Text;

                //        if (age == "-1")
                //        {
                //            _isValidBOB = false;
                //            break;
                //        }

                //    }
                //}

                //foreach (GridViewRow vv in gvAFM.Rows)
                //{
                //    if (vv.RowType == DataControlRowType.DataRow)
                //    {
                //        //  gvAFM.Columns[7].Visible = true;
                //        string verify = vv.Cells[8].Text;

                //        if (verify.ToString().ToLower() == "false")
                //        {

                //            _isVerified = false;
                //            break;
                //        }



                //    }
                //}

                //gvAFM.Columns[8].Visible = false;


                //if (_isValidBOB == false)
                //{
                //    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoDateOfBirthAFM", this.LocalResourceFile);
                //    lblMessage.Visible = true;
                //}
                //if (_isVerified == false)
                //{
                //    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoAFMVerify", this.LocalResourceFile);
                //    lblMessage.Visible = true;
                //}

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvAFM_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Prevents the GridView from going into EDIT MODE (textboxes)
            e.Cancel = true;

        }

        protected void gvAFM_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No requirement to implement code here
            e.Cancel = true;
        }

        protected void gvAFM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "Delete")
                {

                    //int afmID = Convert.ToInt32(e.CommandArgument);

                    //FBClientsController controller = new FBClientsController();

                    //controller.FBClients_AFM_Delete(afmID);
                    ////controller.FBClients_IncomeExpense_Delete(ieID);
                    ////FillIncomeExpenseGrids();
                    //FillAFMGrid();

                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvAFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //FillAFMGrid();
                //gvAFM.PageIndex = e.NewPageIndex;
                //gvAFM.DataBind();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        //gvAFM_OnDataBinding
        protected void gvAFM_OnDataBinding(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //bool _isValid = true;

                //foreach (GridViewRow tt in gvAFM.Rows)
                //{
                //    if (tt.RowType == DataControlRowType.DataRow)
                //    {
                //        string age = tt.Cells[6].Text;

                //        if (age == "-1")
                //        {
                //            _isValid = false;
                //        }
                //    }

                //}

                //if (_isValid == false)
                //{
                //    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoDateOfBirthAFM", this.LocalResourceFile);
                //    lblMessage.Visible = true;
                //}

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }



    }
}