using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using DotNetNuke.Services.Exceptions;

public partial class ImageConversions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CreatePhoto();
    }

    void CreatePhoto()
    {
        try
        {
            //hidClientID
            string clientID = "";



            clientID = Session["ClientID"].ToString();

        //    clientID = Request.Form["hidClientID"]; //Get the image from flash file
            string strPhoto = Request.Form["imageData"]; //Get the image from flash file

            var filePathAndName = "C:\\CamPics\\" + clientID.ToString() + ".jpg";
            byte[] photo = Convert.FromBase64String(strPhoto);
            FileStream fs = new FileStream(filePathAndName.ToString(), FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter br = new BinaryWriter(fs);
            
            
            br.Write(photo);
            
            br.Flush();
            br.Close();
            fs.Close();




            Session.Remove("ClientID"); 
        }
        catch (Exception ex)
        {
            Exceptions.ProcessModuleLoadException(this, ex);
        }
    }
}
