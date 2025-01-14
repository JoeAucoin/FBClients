
using System;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Exceptions;
using GIBS.FBClients.Components;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Spire.Barcode;

namespace GIBS.Modules.FBClients
{
    public partial class MakeID : PortalModuleBase
    {

        public int clientId = 0;

        public string _ClientPhoto = "";
        public string _ClientPhotoRS = "";
        public string _BarCodeImage = "";
        public string _PdfFilename = "";
        public string _IDCardImagePath = "";
        


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
           //     JavaScript.RequestRegistration(CommonJs.jQueryUI);
            //    JavaScript.RequestRegistration(CommonJs.DnnPlugins);
          //  Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CameraScript", (this.TemplateSourceDirectory + "/JavaScript/Camera.js"));


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cid"] != null)
            {
                clientId = Int32.Parse(Request.QueryString["cid"]);
            }
            LoadSettings();

            string _clientImage = "/Portals/" + this.PortalId + "/" + _IDCardImagePath.ToString() + clientId.ToString() + ".jpg";
            if (File.Exists(Server.MapPath(_clientImage)))
            {
                string format1 = "Mddyyyyhhmmsstt";
                var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format1));
                ImageIDClient.Visible = true;
                ImageIDClient.ImageUrl = _clientImage + "?v=" + myTimeStamp.ToString();
                ImageIDClient.AlternateText = "picture";
                _ClientPhoto = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientId.ToString() + ".jpg";
            }
            else
            {
                ImageIDClient.Visible = false;
            }


            FillClientRecord(clientId);
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

                    LabelClientInfo.Text = item.ClientFirstName + ' ' + item.ClientLastName;
                  //  LabelClientInfo.Text += item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode;
                    if (item.IDPhoto != null)
                    {
                        ImageIDClient.Visible = true;
                        byte[] imagem = (byte[])(item.IDPhoto);
                        //var PROFILE_PIC = Convert.ToBase64String(imagem);
                        //HiddenFieldClientPicture.Value = item.IDPhoto.ToString();
                        //ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        //ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;

                        MemoryStream ms = new MemoryStream(imagem);
                        //write to file
                        //string myidLogo = PortalSettings.HomeDirectoryMapPath + "IDCard1.png"; // "Images\\Logo.png";
                        _ClientPhoto = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";

                        if (File.Exists(_ClientPhoto))
                        {
                            File.Delete(_ClientPhoto);
                        }

                        FileStream file = new FileStream(_ClientPhoto.ToString(), FileMode.Create, FileAccess.Write);
                        
                        ms.WriteTo(file);
                        ms.Close();
                        file.Close();
                        file.Dispose();

                        CompressImage(_ClientPhoto, 90);


                        string _clientImage = "/Portals/" + this.PortalId + "/" + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";
                        string format1 = "Mddyyyyhhmmsstt";
                        var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format1));
                        ImageIDClient.Visible = true;
                        ImageIDClient.ImageUrl = _clientImage + "?v=" + myTimeStamp.ToString();
                        ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;

                        // New image created, delete database image . .
                        controller.FBClients_IDPhoto_DeleteByClientID(clientID);
  

                    }
                    //else
                    //{
                    //    ImageIDClient.Visible = false;
                    //}
// CREATE THE BARCODE
                        string s = clientID.ToString().PadLeft(11, '0');
                        CreateBarCode(s.ToString());
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


        public static void CompressImage(string SoucePath, int quality)
        {
            FileStream fs = new FileStream(SoucePath, FileMode.Open);
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(fs);
            fs.Close();

            using (Bitmap bmp1 = new Bitmap(imgPhoto))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder QualityEncoder = System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

                myEncoderParameters.Param[0] = myEncoderParameter;
                File.Delete(SoucePath);
                bmp1.Save(SoucePath, jpgEncoder, myEncoderParameters);

            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void CreateBarCode(string barcodeText)
        {

            try
            {
               
                BarcodeSettings bs = new BarcodeSettings();

                bs.Type = BarCodeType.Code128;
                bs.Data = barcodeText.ToString();
                bs.ShowText = false;
                bs.AutoResize = false;
                bs.Unit = GraphicsUnit.Millimeter;
                bs.BarHeight = 9;

                bs.ImageWidth = 290;
                bs.ImageHeight = 9;

                BarCodeGenerator bg = new BarCodeGenerator(bs);

                bg.GenerateImage().Save(PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientId.ToString() + "BarCode.jpg");

                _BarCodeImage = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientId.ToString() + "BarCode.jpg";


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            MakeIDCard(clientId.ToString());

            DeleteImages();
        }



        public void MakeIDCard(string itemID)
        {
            try
            {

                string myPortalName = this.PortalSettings.PortalName.ToString();

                Document document = new Document();
                document.Info.Author = "Joseph Aucoin - www.gibs.com";
                document.Info.Keywords = "Food Pantry ID Card";
                document.Info.Title = myPortalName.ToString() + " ID Card - " + LabelClientInfo.Text.ToString();

                // Get the A4 page size
                MigraDoc.DocumentObjectModel.Unit width, height;
                PageSetup.GetPageSize(PageFormat.A6, out width, out height);
                 
                width = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(86));
                height = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(54));

                // Add a section to the document and configure it such that it will be in the centre
                // of the page
                Section section = document.AddSection();
                section.PageSetup.PageHeight = height;
                section.PageSetup.PageWidth = width;
                section.PageSetup.LeftMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.RightMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.TopMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));
                section.PageSetup.BottomMargin = (MigraDoc.DocumentObjectModel.Unit.FromMillimeter(2));


                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                //++++++++++++++++++++++++++++++
                // CREATE THE TABLE  HeaderTable
                //  HeaderTable


                MigraDoc.DocumentObjectModel.Tables.Table HeaderTable = new MigraDoc.DocumentObjectModel.Tables.Table();
                HeaderTable.Borders.Width = 0; // Default to show borders 1 pixel wide Column
                HeaderTable.LeftPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.RightPadding = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(0);
                HeaderTable.Borders.Left.Visible = true;
                HeaderTable.Borders.Right.Visible = true;


                
                Column column0 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(41));
                Column column1 = HeaderTable.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromMillimeter(41));


                MigraDoc.DocumentObjectModel.Tables.Row row1 = HeaderTable.AddRow();
          //      row1.Borders.Width = 1;
                row1.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(8);
                MigraDoc.DocumentObjectModel.Tables.Cell cell1 = row1.Cells[0];
                MigraDoc.DocumentObjectModel.Tables.Cell cell2 = row1.Cells[1];
                //      row1.KeepWith = 1;
                cell1.Format.Alignment = ParagraphAlignment.Center;
                
                cell1.AddParagraph(LabelClientInfo.Text.ToString());

 
                cell2.Format.Alignment = ParagraphAlignment.Center;
                cell2.AddParagraph(clientId.ToString());



                /////// ROW 2 ////////
                ///////

                Row row2 = HeaderTable.AddRow();
                row2.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(31.75);
              //  row2.KeepWith = 2;
                MigraDoc.DocumentObjectModel.Tables.Cell idPhotoCell = row2.Cells[0];
                MigraDoc.DocumentObjectModel.Tables.Cell idLogoCell = row2.Cells[1];


                idPhotoCell.Format.Alignment = ParagraphAlignment.Center;
           //     idPhotoCell.Borders.Width = 1;
                idPhotoCell.Borders.Left.Visible = true;
                idPhotoCell.Borders.Right.Visible = true;
                idPhotoCell.Borders.Bottom.Visible = true;

                idLogoCell.Format.Alignment = ParagraphAlignment.Center;
                idLogoCell.Borders.Width = 0;
                idLogoCell.Borders.Left.Visible = true;
                idLogoCell.Borders.Right.Visible = true;
                idLogoCell.Borders.Bottom.Visible = true;

                string myidLogo = PortalSettings.HomeDirectoryMapPath + "IDCard.png";



                //   XImage myNewimage = XImage.FromFile(_ClientPhoto.ToString())
                //      myNewimage.Width = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(24);

                //Image img = new Image(ImageDataFactory.create(_ClientPhoto.ToString()));
                //img.scaleToFit(PageSize.A4.getWidth(), PageSize.A4.getHeight());
               
                MigraDoc.DocumentObjectModel.Shapes.Image imagePhoto = idPhotoCell.AddImage(_ClientPhoto.ToString());
                imagePhoto.Width = "3.96875cm";
                imagePhoto.LockAspectRatio = true;
                //    MigraDoc.DocumentObjectModel.Shapes.Image MyName Image;


                // LockAspectRatio = true;
                MigraDoc.DocumentObjectModel.Shapes.Image imageLogo = idLogoCell.AddParagraph().AddImage(myidLogo.ToString());
                imageLogo.Width = "3.5cm";
                imageLogo.LockAspectRatio = true;
                

                Row row3 = HeaderTable.AddRow();

                MigraDoc.DocumentObjectModel.Tables.Cell barCodeCell = row3.Cells[0];
                
                barCodeCell.MergeRight = 1;
                barCodeCell.AddParagraph().AddImage(_BarCodeImage.ToString());
                barCodeCell.Format.Alignment = ParagraphAlignment.Center;

                barCodeCell.Borders.Width = 0;
                barCodeCell.Borders.Left.Visible = true;
                barCodeCell.Borders.Right.Visible = true;
                barCodeCell.Borders.Bottom.Visible = true;

               

                section.Add(HeaderTable);


                ///// SPACING
                //MigraDoc.DocumentObjectModel.Paragraph paragraph = section.AddParagraph();
                //paragraph.Format.LineSpacingRule = MigraDoc.DocumentObjectModel.LineSpacingRule.Exactly;
                //paragraph.Format.LineSpacing = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(02.0);



                // Create a renderer
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

                // Associate the MigraDoc document with a renderer
                pdfRenderer.Document = document;

                // Layout and render document to PDF
                pdfRenderer.RenderDocument();


                _PdfFilename = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + "ID" + clientId.ToString() + ".pdf";

                if (File.Exists(_PdfFilename))
                {
                    File.Delete(_PdfFilename);
                }

                pdfRenderer.PdfDocument.Save(_PdfFilename);

                HyperLinkPDF.Visible = true;
                HyperLinkPDF.NavigateUrl = PortalSettings.HomeDirectory + _IDCardImagePath.ToString() + "ID" + clientId.ToString() + ".pdf";



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


                if (settingsData.IDCardImagePath != null)
                {
                    _IDCardImagePath = settingsData.IDCardImagePath.ToString();
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

        public void DeleteImages()
        {

            try
            {
                //  _ClientPhoto
                //if (File.Exists(_ClientPhoto))
                //{
                //    File.Delete(_ClientPhoto);
                //}

                //if (File.Exists(_ClientPhotoRS))
                //{
                //    File.Delete(_ClientPhotoRS);
                //}

                if (File.Exists(_BarCodeImage))
                {
                    File.Delete(_BarCodeImage);
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ButtonReturnToClientManager_Click(object sender, EventArgs e)
        {

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "EditClient", "mid=" + ModuleId.ToString() + "&cid=" + clientId.ToString()));

        }
        static string MigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" +
                   Convert.ToBase64String(image);
        }

        static byte[] LoadImage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                stream.Close();
                return data;
            }
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            System.Drawing.Image newImage = new System.Drawing.Bitmap(image, newWidth, newHeight); // I specify the new image from the original together with the new width and height
            using (Graphics graphicsHandle = Graphics.FromImage(image))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(newImage, 0, 0, newWidth, newHeight);
                graphicsHandle.Dispose();
            }
            
            return newImage;
        }


    }
}