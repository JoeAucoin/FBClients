using DotNetNuke.Entities.Modules;
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

        public int clientId = 0;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

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
            FillClientRecord(clientId);
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
            }

            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        //public void UploadImage(string imageData)
        //{
        //    try
        //    {
        //        if (clientId > 0)
        //        {
        //            string fname = clientId.ToString() + ".png";
        //            string fileNameWitPath = HttpContext.Current.Server.MapPath("/Portals/0/" + fname);
        //            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create, FileAccess.Write))
        //            {
        //                using (BinaryWriter bw = new BinaryWriter(fs))
        //                {

        //                    byte[] data = Convert.FromBase64String(imageData.Replace("data:image/png;base64,", String.Empty));

        //                    bw.Write(data);
        //                    bw.Flush();
        //                    bw.Close();
        //                }

        //            }

        //            FillClientRecord(clientId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}

        protected void ButtonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
              //  UploadImage(HiddenFieldImage.Value);
                SaveImageToDatabase(HiddenFieldImage.Value);
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
                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                    }

                }
                else
                {
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(), true);
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


    }
}