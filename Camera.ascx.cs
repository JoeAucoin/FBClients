
using DotNetNuke.Common.Utilities;
using DotNetNuke.Abstractions;
using DotNetNuke.Entities.Modules;
using Microsoft.Extensions.DependencyInjection;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Exceptions;
using GIBS.FBClients.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GIBS.Modules.FBClients
{
    public partial class Camera : PortalModuleBase
    {
        private INavigationManager _navigationManager;
        public int clientId = 0;
        public string _IDCardImagePath = "";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _navigationManager = DependencyProvider.GetRequiredService<INavigationManager>();

            JavaScript.RequestRegistration(CommonJs.jQuery);
        //    JavaScript.RequestRegistration(CommonJs.jQueryUI);
        //    JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CameraScript", (this.TemplateSourceDirectory + "/JavaScript/Camera.js"));
           

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cid"] != null)
            {
                clientId = Int32.Parse(Request.QueryString["cid"]);
            }
            LoadSettings();
            FillClientRecord(clientId);
            SetMakeIDLink();
        }

        public void LoadSettings()
        {
            try
            {

                if (Settings.Contains("iDCardImagePath"))
                {

                    _IDCardImagePath = (Settings["iDCardImagePath"].ToString());
                }
                else
                {
                    _IDCardImagePath = "";
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void SaveImageToDatabase(string imageData)
        {
            try
            {
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = new FBClientsInfo();

                byte[] imageBytes = Convert.FromBase64String(imageData.Replace("data:image/png;base64,", String.Empty));
                item.ClientID = clientId;
                item.IDPhoto = imageBytes;
                item.CreatedByUserID = this.UserId;

                controller.FBClients_IDPhoto_Insert(item);

                FillClientRecord(clientId);

                string _clientImage = "/Portals/" + this.PortalId + "/" + _IDCardImagePath.ToString() + clientId.ToString() + ".jpg";
                if (File.Exists(Server.MapPath(_clientImage)))
                {
                    File.Delete(Server.MapPath(_clientImage));
                }

            }

            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }



        protected void ButtonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
              //  UploadImage(HiddenFieldImage.Value);
                SaveImageToDatabase(HiddenFieldImage.Value);
                HyperLinkMakeID.Visible = true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void FillClientRecord(int clientID)
        {

            try
            {               
                //load the item
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_GetByID(this.PortalId, clientID);

                if (item != null)
                {

                    LabelClientInfo.Text = item.ClientFirstName + ' ' + item.ClientLastName + " - ";
                    LabelClientInfo.Text += item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode;

                    Session["ClientID"] = clientID.ToString();
                    var queryString = "?ClientID=" + Session["ClientID"].ToString() + "&contactname=" + item.ClientFirstName.ToString() + " " + item.ClientLastName.ToString();


                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;
                        HyperLinkMakeID.Visible = true;
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                        HyperLinkMakeID.Visible = false;

                        string _clientImage = "/Portals/" + this.PortalId + "/" + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";
                        if (File.Exists(Server.MapPath(_clientImage)))
                        {
                            HyperLinkMakeID.Visible=true;
                            string format1 = "Mddyyyyhhmmsstt";
                            var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format1));
                            ImageIDClient.Visible = true;
                            ImageIDClient.ImageUrl = _clientImage + "?v=" + myTimeStamp.ToString();
                            ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;
                        }
                        else
                        {
                            HyperLinkMakeID.Visible=false;
                            ImageIDClient.Visible = false;
                            
                        }
                    }

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

        public void SetMakeIDLink()
        {

            //HyperLinkPhotoID
            try
            {

                string myLink = _navigationManager.NavigateURL("MakeID", "mid=" + this.ModuleId);
                
                myLink += "?cid=" + clientId.ToString();

                myLink += "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/Portals/_default/Containers/_default/No Container");
                

                string redirectUrl = UrlUtils.PopUpUrl(myLink, this, PortalSettings, false, true);

                HyperLinkMakeID.NavigateUrl = redirectUrl.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ButtonReturnToClientManager_Click(object sender, EventArgs e)
        {
            
           Response.Redirect(_navigationManager.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + clientId.ToString()));
     
        }
    }
}