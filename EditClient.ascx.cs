using System;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Abstractions;
using DotNetNuke.Entities.Modules;
using Microsoft.Extensions.DependencyInjection;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Services.Mail;
using GIBS.FBClients.Components;
using DotNetNuke.Common.Lists;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using DotNetNuke.Entities.Modules.Actions;
using System.Web;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Social.Notifications;
using System.Text;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Linq;
using System.Windows.Forms;
using DotNetNuke.Common;
using DotNetNuke.Framework;
using System.Data;
using System.IO;
using System.Web.UI;
using GIBS.Modules.FBClients.Components;
using System.Text.Encodings.Web;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;

//clientType_SelectedIndexChanged

namespace GIBS.Modules.FBClients
{
    public partial class EditClient : PortalModuleBase, IActionable
    {
        private INavigationManager _navigationManager;
        public static int clientId = Null.NullInteger;

        public double IncomeTotal = 0;
        public double ExpenseTotal = 0;
        public double _income = 0;
        public double _expense = 0;
        public double _budget = 0;
        DateTime d = new DateTime(1901, 1, 1);
        //int _bags = 0;

        static int _DaysToValidVisit;
        static bool _ShowVisitStopLight;
        public static string _ClientManagerUserRole;
        public string _IncomeEligibilityGuidelines;
        bool _ShowOneBagOnly;
        bool _ShowClientType;
        static bool _ShowSendText = false;
        static bool _ShowIncExpSummary = true;
        public bool _ShowExpense = false;
        public bool _ShowDisabilitiee = false;
        public bool _ShowAFMRelationship = false;
        public bool _ShowSuffix = false;
        static bool _ShowGiftFields = false;
        static bool _ShowClientServiceLocation = false;
        static string _XmasToysYear;
        static string _FlagForReviewNotify = "";
        //1=3,2=3,3=3,4=5,5=5,6=5,7=6
        static string _BagAllowance = "1=1";
        static string _GroupHomeBagAllowance = "5";
        public string _bagsQualifiedFor = "";
        public static string _ClientManagerDeleteRecordRole = "";
        static int _XmasRequireSizeMinimumAge = 10;
        static int _XmasRequireSizeMaxAge = 14;
        public bool _IsEthnicityRequired = false;
        public bool _ReqAFMVerified = false;
        static string _GoogleAPIKey = "";
        static string _ClientOrderPage = "";
        public bool _TwilioMessageSent = false;
        static string _TwilioAccountSid = "";
        static string _TwilioAuthToken = "";
        static string _TwilioPhoneNumber = "";
        public string _ClientCellNumber = "";
        static string _FBName = "";
        static string _IDCardImagePath = "";
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _navigationManager = DependencyProvider.GetRequiredService<INavigationManager>();
            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.4.1.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Watermark", (this.TemplateSourceDirectory + "/JavaScript/jquery.watermarkinput.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "SigWeb", (this.TemplateSourceDirectory + "/JavaScript/SigWebTablet.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Dymo", (this.TemplateSourceDirectory + "/JavaScript/dymo.connect.framework.js?1=1"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "PrintLabel", (this.TemplateSourceDirectory + "/JavaScript/PrintLabel.js?1=9"));
            //   Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Style", ("https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css"));

        }


        protected void Page_Load(object sender, EventArgs e)
        {


            ErrorMessage.Visible = false;


            DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(cmdDelete, Localization.GetString("DeleteConfirm", this.LocalResourceFile));
            DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(btnAddClientVisit, Localization.GetString("ConfirmAddVisit", this.LocalResourceFile));

            //CLIENT DOB RANGE
            Range1.MinimumValue = DateTime.Today.AddYears(-100).ToShortDateString();
            Range1.MaximumValue = DateTime.Today.AddMonths(-191).ToShortDateString();
            //AFM DOB RANGE
            Range2.MinimumValue = DateTime.Today.AddYears(-100).ToShortDateString();
            Range2.MaximumValue = DateTime.Today.AddDays(-1).ToShortDateString();

            if (Request.QueryString["cid"] != null)
            {
                clientId = Int32.Parse(Request.QueryString["cid"]);
            }
            else
            {
                clientId = Null.NullInteger;
            }

            if (Request.QueryString["TabFocus"] != null)
            {
                if (Request.QueryString["cid"] == null)
                {
                    litMapCenter.Text = "var uluru = { lat: 41.6973484, lng: -70.1030107 };";
                    lblMessage.Text = Localization.GetString("msgReadyForNewRecord", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                    
                }
            }

            SetPhotoIDLink();

            if (!IsPostBack)
            {
                // ALLOW ONLY 1 SELECTION FOR DISABILITY
                cblClientDisability.Attributes.Add("onclick", "return HandleOnCheck()");

                LoadSettings();

                ddlCity.Attributes.Add("onchange", "DisableCheck();");

                btnAddClientIncome.OnClientClick = "DisableCheck();";
                //ddlClientExpense.Attributes.Add("onchange", "DisableCheck();");

                if (UserInfo.IsInRole(_ClientManagerDeleteRecordRole))
                {
                    cmdDelete.Visible = true;
                    cmdDelete.Enabled = true;
                    bikeAwardedDate.Visible = true;
                    LinkButtonMerge.Visible = true;
                    cbxIsLocked.Enabled = true;
                //    PanelNumTimesInsterted.Visible = true;
                    LinkButtonDeleteXMasRecord.Visible = true;
                    //    ddlClientServiceLocation.Enabled = true;
                    //  reqClientEthnicity.Enabled = false;
                }
                else
                {
                    bikeAwardedDate.Visible = false;
                    cbxIsLocked.Enabled = false;
                    cmdDelete.Visible = false;
                    cmdDelete.Enabled = false;
                    LinkButtonDeleteXMasRecord.Visible = false;
                }

                if (Request.QueryString["TabFocus"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("dnnTabs-tabs-client");
                    myCookie.HttpOnly = false;
                    switch (Request.QueryString["TabFocus"])
                    {
                        case "Client":
                            myCookie.Value = "";
                            txtClientFirstName.Focus();
                            
                            break;

                        case "IncomeExpense":
                            myCookie.Value = "1";
                            break;

                        case "AFM":
                            myCookie.Value = "2";
                            break;

                        case "Visits":
                            myCookie.Value = "3";
                            break;

                        default:
                            myCookie.Value = "";
                            break;
                    }
                    Response.Cookies.Add(myCookie);
                }



                GetLists();
                GetCaseWorkers();

                //SET DEFAULT VALUES

                txtVisitDate.Text = DateTime.Now.Date.ToShortDateString();

                //check we have a client to lookup
                if (!Null.IsNull(clientId))
                {
                    FillClientRecord(clientId);
                }
                else
                {
                    HyperLinkPhotoID.Visible = false;

                }

                XmasReportLink();
            }
            else
            {
                // DO NOTHING
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

                string _clientAge = "";
                string _phoneType = "";

                if (item != null)
                {
                   


                    if (item.ClientType == "Individual")
                    {
                        txtClientFirstName.Enabled = true;
                        txtClientMiddleInitial.Enabled = true;
                        PanelNumTimesInsterted.Visible = false;
                    }
                    else if(item.ClientType == "Pallet")
                    {
                        txtClientFirstName.Enabled = false;
                        txtClientMiddleInitial.Enabled = false;
                        txtClientDOB.Enabled = false;

                        reqClientEthnicity.Enabled = false;
                        reqClientGender.Enabled = false;
                        lblClientLastName.Text = "Pallet Name";
                        PanelNumTimesInsterted.Visible = true;

                    }

                    else
                    {
                        lblClientLastName.Text = "Group Home Name";
                        txtClientFirstName.Enabled = false;
                        txtClientMiddleInitial.Enabled = false;
                        txtClientDOB.Enabled = false;

                        reqClientEthnicity.Enabled = false;
                        reqClientGender.Enabled = false;
                        PanelNumTimesInsterted.Visible = false;
                    }

                    BindDisabilities(item.Disability);
                    string _clientImage = "/Portals/"+ this.PortalId + "/" + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";
                    if (File.Exists(Server.MapPath(_clientImage)))
                    {
                        string format1 = "Mddyyyyhhmmsstt";
                        var myTimeStamp = String.Format("{0}", DateTime.Now.ToString(format1));
                        ImageIDClient.Visible = true;
                        ImageIDClient.ImageUrl = _clientImage + "?v=" + myTimeStamp.ToString();
                        ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                        GetClientPhoto(clientID);
                    }

                  


                    
                    ListItem liClientServiceLocation = ddlClientServiceLocation.Items.FindByValue(item.ServiceLocation.ToString());
                    if (liClientServiceLocation != null)
                    {
                        // value found - select it
                        ddlClientServiceLocation.SelectedValue = item.ServiceLocation.ToString();
                    }
                    else
                    {
                        //Value not found - add it and then select it
                        ddlClientServiceLocation.Items.Insert(1, new ListItem(item.ServiceLocation.ToString(), item.ServiceLocation.ToString()));
                        ddlClientServiceLocation.SelectedValue = item.ServiceLocation.ToString();
                    }

                    if (item.ServiceLocation.ToString().Length > 0)
                    {
                        ddlClientServiceLocation.Enabled = false;
                    }

                    if (UserInfo.IsInRole(_ClientManagerDeleteRecordRole))
                    {
                        ddlClientServiceLocation.Enabled = true;
                    }


                    //  Response.Write(item);

                    ListItem liClientEthnicity = ddlClientEthnicity.Items.FindByValue(item.ClientEthnicity.ToString());
                    if (liClientEthnicity != null)
                    {
                        // value found - select it
                        ddlClientEthnicity.SelectedValue = item.ClientEthnicity.ToString();
                    }
                    else
                    {
                        //Value not found - add it and then select it
                        ddlClientEthnicity.Items.Insert(1, new ListItem(item.ClientEthnicity.ToString(), item.ClientEthnicity.ToString()));
                        ddlClientEthnicity.SelectedValue = item.ClientEthnicity.ToString();
                    }

                    ListItem liClientLanguage = ddlClientLanguage.Items.FindByValue(item.ClientLanguage.ToString());
                    if (liClientLanguage != null)
                    {
                        // value found - select it
                        ddlClientLanguage.SelectedValue = item.ClientLanguage.ToString();
                    }
                    else
                    {
                        //Value not found - add it and then select it
                        ddlClientLanguage.Items.Insert(1, new ListItem(item.ClientLanguage.ToString(), item.ClientLanguage.ToString()));
                        ddlClientLanguage.SelectedValue = item.ClientLanguage.ToString();
                    }

                    ddlClientGender.SelectedValue = item.ClientGender;
                    ddlClientType.SelectedValue = item.ClientType;
                    ddlClientStatus.SelectedValue = item.IsActive.ToString();
                    ddlClientSuffix.SelectedValue = item.ClientSuffix;
                    cbxDOB_Verified.Checked = item.ClientDOBVerify;
                    if (item.ClientDOBVerify == false)
                    {
                        lblMessage.Text += "<br />" + Localization.GetString("ErrorNoClientVerify", this.LocalResourceFile);
                        ErrorMessage.Visible = true;
                    }
                    txtClientFirstName.Text = item.ClientFirstName;
                    txtAddress.Text = item.ClientAddress;

                    ListItem li = ddlCity.Items.FindByValue(item.ClientCity);
                    if (li != null)
                    {
                        // value found
                        ddlCity.SelectedValue = item.ClientCity;

                        if (ddlCity.SelectedValue.ToString().ToLower() == "other")
                        {
                            txtOtherTown.Visible = true;
                            ddlTown.Visible = false;
                            txtOtherTown.Text = item.ClientTown;
                        }
                        else
                        {
                            txtOtherTown.Visible = false;
                            ddlTown.Visible = true;

                            var village = new ListController().GetListEntryInfoItems("Village", "Town." + ddlCity.SelectedValue.ToString(), this.PortalId);

                            ddlTown.DataTextField = "Text";
                            ddlTown.DataValueField = "Text";
                            ddlTown.DataSource = village;
                            ddlTown.DataBind();
                            ddlTown.Items.Insert(0, new ListItem("Select " + Localization.GetString("lblClientVillage", this.LocalResourceFile), "-1"));

                            ListItem litown = ddlTown.Items.FindByValue(item.ClientTown);
                            if (litown != null)
                            {
                                // value found
                                ddlTown.SelectedValue = item.ClientTown;
                            }
                            else
                            {
                                //Value not found
                                //   ddlTown.SelectedValue = item.ClientTown;
                            }
                        }

                    }
                    else
                    {
                        //Value not found
                      //  ddlCity.SelectedValue = "OTHER";
                        ddlTown.Visible = true;
                        txtOtherTown.Visible = false;
                        txtOtherTown.Text = item.ClientTown;
                    }

                    //   txtCity.Text = item.ClientCity;


                    txtClientIdCard.Text = item.ClientIdCard;
                    txtClientLastName.Text = item.ClientLastName;
                    txtClientMiddleInitial.Text = item.ClientMiddleInitial;
                    txtEmail.Text = item.ClientEmailAddress;
                    txtPhone.Text = item.ClientPhone;
                    txtZip.Text = item.ClientZipCode;
                    ddlClientPhoneType.SelectedValue = item.ClientPhoneType;

                    if (item.ClientPhoneType == "0")
                    {
                        _phoneType = "";
                    }

                    else
                    {
                        _phoneType = item.ClientPhoneType + " ";
                    }
                    if(_phoneType.ToString().Trim() == "Cell" && item.ClientPhone.ToString().Length > 8)
                    {
                        var formatPhoneNumber = item.ClientPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").ToString();
                        _ClientCellNumber = "+1" + formatPhoneNumber.ToString();
                        hidClientCellNumber.Value = _ClientCellNumber;
                    }


                    if (item.ClientCaseWorker > 1)
                    {
                        if (ddlCaseWorker.Items.FindByValue(item.ClientCaseWorker.ToString()) != null)
                        {
                            ddlCaseWorker.SelectedValue = item.ClientCaseWorker.ToString();
                        }
                        
                    }

                    if (item.ClientDOB >= d)
                    {
                        txtClientDOB.Text = item.ClientDOB.ToShortDateString();
                        _clientAge = "<br />DOB: " + item.ClientDOB.ToShortDateString() + "&nbsp;&nbsp;Age: " + item.ClientAge;
                    }
                    else
                    {

                        lblMessage.Text += "<br />" + Localization.GetString("ErrorNoDateOfBirth", this.LocalResourceFile);
                        ErrorMessage.Visible = true;

                    }

                    if (item.ClientVerifyDate >= d)
                    {
                        txtVerifyDate.Text = item.ClientVerifyDate.ToShortDateString();
                    }
                    //ErrorVerifyDateOverdue
                    if ((DateTime.Now - item.ClientVerifyDate).TotalDays > 365)
                    {
                        lblMessage.Text += "<br />" + Localization.GetString("ErrorVerifyDateOverdue", this.LocalResourceFile);
                        ErrorMessage.Visible = true;
                    }


                    cbxIsLocked.Checked = item.IsLocked;
                    if (item.IsLocked)
                    {
                        btnAddClientVisit.Enabled = false;
                        txtVisitNotes.Text = "THIS ACCOUNT IS LOCKED - YOU CANNOT ADD A VISIT!";
                    }

                    // VERIFY ADDRESS CHECKBOX

                    cbxClientAddressVerify.Checked = item.ClientAddressVerify;
                    if (item.ClientAddressVerify == false)
                    {
                        lblMessage.Text += "<br />" + Localization.GetString("ErrorNoClientAddressVerify", this.LocalResourceFile);
                        ErrorMessage.Visible = true;
                    }


                    // VERIFY ADDRESS DATE
                    if (item.ClientAddressVerifyDate >= d)
                    {

                        txtClientAddressVerifyDate.Text = item.ClientAddressVerifyDate.ToShortDateString();
                    }
                    // VERIFY ADDRESS TIME SPAN
                    if ((DateTime.Now - item.ClientAddressVerifyDate).TotalDays > 365)
                    {
                        lblMessage.Text += "<br />" + Localization.GetString("ErrorVerifyAddress", this.LocalResourceFile);
                        ErrorMessage.Visible = true;
                    }


                    // FLAG FOR REVIEW
                    cbxSubjectToReview.Checked = item.SubjectToReview;

                    // ONE BAGGER
                    cbxOneBagOnly.Checked = item.OneBagOnly;

                    if (item.SubjectToReview)
                    {
                        ImgFlagged.Visible = true;
                    }


                    // CALCULATE # OF BAGS FROM TOTAL_IN_HOUSEHOLD
                    int _thh = item.AFMCount + 1;

                    // _BagAllowance
                    string dataForArray = _BagAllowance.ToString();
                    //       Response.Write(dataForArray + "<br>");
                    string[] firstArray = dataForArray.Split(',');
                    string[] secondArray;
                    string _bagsQualifiedFor = "";
                    int masterBag = 0;
                    for (int i = 0; i < firstArray.Length; i++)
                    {
                        secondArray = firstArray[i].Split('=');
                        if (_thh == Convert.ToInt16(secondArray[0]))
                        {
                            masterBag = Convert.ToInt16(secondArray[1]);
                        }
                    }
                    if (masterBag == 0)
                    {
                        secondArray = firstArray[firstArray.Length - 1].Split('=');
                        masterBag = Convert.ToInt16(secondArray[1]);
                    }


                    if (item.OneBagOnly)
                    {
                        masterBag = 1;
                    }
                    if (item.ClientType == "Group Home")
                    {
                        masterBag = Int32.Parse(_GroupHomeBagAllowance.ToString());
                    }

                    _bagsQualifiedFor = "Qualifies for " + masterBag.ToString() + " Bag(s)";
                    ddlVisitNumBags.SelectedValue = masterBag.ToString();

                    hidLabelContent.Value = item.ClientFirstName.ToString() + " " + item.ClientLastName.ToString();
                    hidLabelContent2.Value = item.ClientID.ToString() + "  -  " + item.ClientTown.ToString();
                    hidLabelContent3.Value = "# Bags: " + masterBag.ToString();
                    string sBarCodeClientIDNumber = item.ClientID.ToString().PadLeft(11, '0');
                    hidLabelContentBarCode.Value = sBarCodeClientIDNumber.ToString();
                    

                    int _LastVisitNumberofDays = Convert.ToInt32((DateTime.Now - item.LastVisitDate).TotalDays);
                    string _StopGoLightVisitCheck = "<img src='" + this.TemplateSourceDirectory + "/Images/red_light.png' title='Last visit was " + _LastVisitNumberofDays.ToString() + " days ago' alt='Last visit more then 21 days ago' width='16' /> &nbsp;";
                    if ((DateTime.Now - item.LastVisitDate).TotalDays >= _DaysToValidVisit)
                    {
                        _StopGoLightVisitCheck = "<img src='" + this.TemplateSourceDirectory + "/Images/green_light.png' title='Last visit was " + _LastVisitNumberofDays.ToString() + " days ago' alt='Last visit was " + _LastVisitNumberofDays.ToString() + " days ago' width='16' /> &nbsp;";
                    }
                    // CHECK SETTING TO SEE IF STOP LIGHT SHOULD DISPLAY
                    if (_ShowVisitStopLight == false)
                    {
                        _StopGoLightVisitCheck = "";
                    }

                    //Set Page Name
                    SetPageName(item.ClientFirstName + " " + item.ClientLastName);


                    // QUICK VIEW SECTION
                    lblClientNameHeader.Text = item.ClientFirstName + " " + item.ClientLastName + " - " + item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode
                        + "<br />" + _phoneType.ToString() + "Phone: " + item.ClientPhone
                        + _clientAge.ToString()
                        + "<br />Entry Date: " + item.CreatedOnDate.ToShortDateString()
                        + "<br /><font color='Red'>Client ID: " + item.ClientID + "</font>";



                    lblClientNameHeaderRight.Text = "<font color='Blue'>" + _bagsQualifiedFor.ToString() + "</font>"
                        + "<br />" + _StopGoLightVisitCheck.ToString() + "Last Visit: " + item.LastVisitDate.ToShortDateString() + " - (" + _LastVisitNumberofDays.ToString() + " Days Ago)"
                        + "<br />Total in Household: " + (item.AFMCount + 1).ToString();


                    lblClientNoteQuickView.Text = item.ClientNote.ToString();

                    // END QUICK VIEW SECTION


                    hidClientID.Value = item.ClientID.ToString();

                    txtLocAddress.Text = item.ClientAddress + ", " + item.ClientTown + ", " + item.ClientState + " " + item.ClientZipCode;

                    if (item.Latitude != Null.NullDouble)
                    {
                        txtLatitude.Text = item.Latitude.ToString();
                        txtLongitude.Text = item.Longitude.ToString();
                   //     litMapCenter.Text = "var center = new GLatLng(" + txtLatitude.Text + ", " + txtLongitude.Text + ");map.setCenter(center, 15);";
                        litMapCenter.Text = "var uluru = { lat: " + txtLatitude.Text + ", lng: " + txtLongitude.Text + " };";

                        
                        

                    }
                    else
                    {
                        litMapCenter.Text = "var uluru = { lat: 41.6973484, lng: -70.1030107 };";

                    }

                    txtClientNote.Text = item.ClientNote.ToString();
                    txtUnit.Text = item.ClientUnit.ToString();
                    txtLastUpdatedBy.Text = item.LastModifiedOnDate.ToString() + " by " + item.LastModifiedByUserName;


                    FillVisitsGrid();
                    FillAFMGrid();
                    FillIncomeExpenseGrids();
                    FillBudget();
                    BindTrueFalseQuestions();
                    FillClientAgeGroups();
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

        public void GetClientPhoto(int clientID)
        {

            try
            {
                //load the item
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_IDPhoto_GetByClientID(clientID);

                if (item != null)
                {

                    if (item.IDPhoto != null)
                    {
                        
                        byte[] imagem = (byte[])(item.IDPhoto);
                     //   var PROFILE_PIC = Convert.ToBase64String(imagem);
                     ////   HiddenFieldClientPicture.Value = item.IDPhoto.ToString();
                     //   ImageIDClient.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                     //   ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;

                        MemoryStream ms = new MemoryStream(imagem);
                                              
                        //write to file
                        string _ClientPhoto = PortalSettings.HomeDirectoryMapPath + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";

                        if (File.Exists(_ClientPhoto))
                        {
                            File.Delete(_ClientPhoto);
                        }
 

                        FileStream file = new FileStream(_ClientPhoto.ToString(), FileMode.Create, FileAccess.Write);
                        

                        ms.WriteTo(file);
                        //img.Dispose();
                        ms.Close();
                        ms.Dispose();
                        file.Close();
                        file.Dispose();

                        //COMPRESS PHOTO - _ClientPhoto
                        //
                        CompressImage(_ClientPhoto, 90);

                        // New image created, delete database image . . 
                        controller.FBClients_IDPhoto_DeleteByClientID(clientID);

                        string _clientImage = "/Portals/" + this.PortalId + "/" + _IDCardImagePath.ToString() + clientID.ToString() + ".jpg";
                        ImageIDClient.Visible = true;
                        ImageIDClient.ImageUrl = _clientImage;
                        ImageIDClient.AlternateText = item.ClientFirstName + ' ' + item.ClientLastName;
                  
                    }
                    else
                    {
                        ImageIDClient.Visible = false;
                    }

                }
                else
                {
                    ImageIDClient.Visible = false;
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

        public byte[] ReduceImageSize(byte[] imageBytes, int jpegQuality)
        {
            var inputStream = new MemoryStream(imageBytes);
            var image = System.Drawing.Image.FromStream(inputStream);
            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (int)jpegQuality);

            var outputStream = new MemoryStream();
            image.Save(outputStream, jpegEncoder, encoderParameters);
            return outputStream.ToArray();
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

        public bool CheckIPAddress(string _IPAddress)
        {
            try
            {


                if (HttpContext.Current.Request.UserHostAddress.ToString() == _IPAddress)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return false;
            }
        }

        public void LoadSettings()
        {
            try
            {
                //var Shifts = new ListController().GetListEntryInfoItems("Shifts", "", this.PortalId);

                //ddlShift.DataTextField = "Text";
                //ddlShift.DataValueField = "Text";
                //ddlShift.DataSource = Shifts;
                //ddlShift.DataBind();
                //ddlShift.Items.Insert(0, new ListItem("--Select--", ""));

                var MobileLocations = new ListController().GetListEntryInfoItems("ClientServiceLocation", "", this.PortalId);

                ddlMobileLocations.DataTextField = "Text";
                ddlMobileLocations.DataValueField = "Text";
                ddlMobileLocations.DataSource = MobileLocations;
                ddlMobileLocations.DataBind();
                ddlMobileLocations.Items.Insert(0, new ListItem("--Select--", "Pantry"));

                ddlClientServiceLocation.DataTextField = "Text";
                ddlClientServiceLocation.DataValueField = "Text";
                ddlClientServiceLocation.DataSource = MobileLocations;
                ddlClientServiceLocation.DataBind();
                ddlClientServiceLocation.Items.Insert(0, new ListItem("--Select--", "Pantry"));


             //   FBClientsSettings settingsData = new FBClientsSettings(this.TabModuleId);

                //static string _ClientCellNumber = "";

                if (Settings.Contains("fBName"))
                {

                    _FBName = (Settings["fBName"].ToString());
                }

                if (Settings.Contains("iDCardImagePath"))
                {

                    _IDCardImagePath = (Settings["iDCardImagePath"].ToString());
                }
                else
                {
                    _IDCardImagePath = "";
                }
                if (Settings.Contains("twilioAccountSid"))
                {

                    _TwilioAccountSid = (Settings["twilioAccountSid"].ToString());
                }

                if (Settings.Contains("twilioAuthToken"))
                {

                    _TwilioAuthToken = (Settings["twilioAuthToken"].ToString());
                }

                if (Settings.Contains("twilioPhoneNumber"))
                {

                    _TwilioPhoneNumber = (Settings["twilioPhoneNumber"].ToString());
                }

                if (Settings.Contains("clientOrderPage"))
                {

                    _ClientOrderPage = (Settings["clientOrderPage"].ToString());
                }

                //_ShowClientServiceLocation
                if (Settings.Contains("showClientServiceLocation") && Settings["showClientServiceLocation"] != null)
                {
                    _ShowClientServiceLocation = Convert.ToBoolean(Settings["showClientServiceLocation"].ToString());
                    divclientservicelocations.Visible = _ShowClientServiceLocation;
                }

                //_GoogleAPIKey
                if (Settings.Contains("googleAPIKey") && Settings["googleAPIKey"] != null)
                {
                    _GoogleAPIKey = Settings["googleAPIKey"].ToString();
                }

                if (Settings.Contains("showSendText") && Settings["showSendText"] != null)
                {
                    _ShowSendText = Convert.ToBoolean(Settings["showSendText"].ToString());
                }

                //printDivBarCodeLabel  printDivLabel
                if (Settings.Contains("showPrintShoppingLabel") && Settings["showPrintShoppingLabel"] != null)
                {
                    printDivLabel.Visible = Convert.ToBoolean(Settings["showPrintShoppingLabel"].ToString());
                }

                //hidPrintShoppingLabelQuantity.Value = masterBag.ToString();
                if (Settings.Contains("printShoppingLabelQuantity") && Settings["printShoppingLabelQuantity"] != null)
                {
                    hidPrintShoppingLabelQuantity.Value = Settings["printShoppingLabelQuantity"].ToString();
                }
                else
                {
                    hidPrintShoppingLabelQuantity.Value = "1";
                }

                if (Settings.Contains("showPrintBarCodeLabel") && Settings["showPrintBarCodeLabel"] != null)
                {
                    printDivBarCodeLabel.Visible = Convert.ToBoolean(Settings["showPrintBarCodeLabel"].ToString());
                }

                if (Settings.Contains("showPhotoID") && Settings["showPhotoID"] != null)
                {
                    HyperLinkPhotoID.Visible = bool.Parse(Settings["showPhotoID"].ToString());
                }


                if (Settings.Contains("clientManagerDeleteRecordRole") && Settings["clientManagerDeleteRecordRole"] != null)
                {
                    _ClientManagerDeleteRecordRole = Settings["clientManagerDeleteRecordRole"].ToString();

                }

                //  settingsData.ReqAFMVerified
                if (Settings.Contains("reqAFMVerified") && Settings["reqAFMVerified"] != null)
                {
                    _ReqAFMVerified = bool.Parse(Settings["reqAFMVerified"].ToString());

                }
                reqAFM_DOB.Enabled = _ReqAFMVerified;

                // if (settingsData.AllowedIPAddress != null)
                if (Settings.Contains("allowedIPAddress"))
                {
                    if (CheckIPAddress(Settings["allowedIPAddress"].ToString()))
                    {
                        reqMobileLocations.Enabled = false;
                        lblMobileLocations.Visible = false;
                        ddlMobileLocations.Visible = false;

                        //ServiceLocation
                        reqClientOrigination.Enabled = false;
                        //   lblClientServiceLocation.Visible = false;
                        //   ddlClientServiceLocation.Visible = false;
                    }
                    else
                    {
                        // if they are remote change initial value requirement so they must select A REMOTE LOCATION
                        reqMobileLocations.InitialValue = "Pantry";

                        reqClientOrigination.InitialValue = "Pantry";
                    }
                }
                else
                {

                }
                if (UserInfo.IsInRole(_ClientManagerDeleteRecordRole))
                {

                    reqMobileLocations.Enabled = false;
                    lblMobileLocations.Visible = true;
                    ddlMobileLocations.Visible = true;

                    //ServiceLocation
                    reqClientOrigination.Enabled = false;
                    lblClientServiceLocation.Visible = true;
                    ddlClientServiceLocation.Visible = true;


                }



                //_XmasRequireSizeAgeRange
                //_XmasRequireSizeMinimumAge
                //_XmasRequireSizeMaxAge
                if (Settings.Contains("xmasRequireSizeAgeRange") && Settings["xmasRequireSizeAgeRange"] != null)
                {
                    //  _BagAllowance = settingsData.XmasRequireSizeAgeRange.ToString();
                    string s = Settings["xmasRequireSizeAgeRange"].ToString();
                    string[] values = s.Split(',').Select(sValue => sValue.Trim()).ToArray();
                    _XmasRequireSizeMinimumAge = Int32.Parse(values[0]);
                    _XmasRequireSizeMaxAge = Int32.Parse(values[1]);
                }


                //         txtBagAllowance.Text = settingsData.BagAllowance;
                if (Settings.Contains("bagAllowance") && Settings["bagAllowance"] != null)
                {
                    _BagAllowance = Settings["bagAllowance"].ToString();
                }
                if (Settings.Contains("groupHomeBagAllowance") && Settings["groupHomeBagAllowance"] != null)
                {
                    if (Settings["groupHomeBagAllowance"].ToString().Length > 0)
                    {
                        _GroupHomeBagAllowance = Settings["groupHomeBagAllowance"].ToString();
                    }
                }
                //_GroupHomeBagAllowance



                //_FlagForReviewNotify
                if (Settings.Contains("flagForReviewNotify") && Settings["flagForReviewNotify"] != null)
                {
                    _FlagForReviewNotify = Settings["flagForReviewNotify"].ToString();
                }


                if (Settings.Contains("daysToValidVisit") && Settings["daysToValidVisit"] != null)
                {
                    _DaysToValidVisit = Int32.Parse(Settings["daysToValidVisit"].ToString());
                }

                if (Settings.Contains("showVisitStopLight") && Settings["showVisitStopLight"] != null)
                {
                    _ShowVisitStopLight = bool.Parse(Settings["showVisitStopLight"].ToString());
                }

                if (Settings.Contains("clientManagerUserRole") && Settings["clientManagerUserRole"] != null)
                {
                    _ClientManagerUserRole = Settings["clientManagerUserRole"].ToString();

                }

                if (Settings.Contains("showClientServiceLocation") && Settings["showClientServiceLocation"] != null)
                {
                    divClientServiceLocation.Visible = Convert.ToBoolean(Settings["showClientServiceLocation"]);
                }


                if (Settings.Contains("incomeEligibilityGuidelines") && Settings["incomeEligibilityGuidelines"] != null)
                {
                    _IncomeEligibilityGuidelines = Settings["incomeEligibilityGuidelines"].ToString();
                }
                else
                {
                    _IncomeEligibilityGuidelines = "";
                }

                if (Settings.Contains("showOneBagOnly") && Settings["showOneBagOnly"] != null)
                {
                    _ShowOneBagOnly = bool.Parse(Settings["showOneBagOnly"].ToString());
                }

                if (Settings.Contains("showClientType") && Settings["showClientType"] != null)
                {
                    _ShowClientType = bool.Parse(Settings["showClientType"].ToString());
                }

                if (Settings.Contains("showSuffix") && Settings["showSuffix"] != null)
                {
                    _ShowSuffix = bool.Parse(Settings["showSuffix"].ToString());
                }

                PanelShowSuffix.Visible = _ShowSuffix;
                PanelShowAFMSuffix.Visible = _ShowSuffix;

                if (Settings.Contains("showXmasGiftFields") && Settings["showXmasGiftFields"] != null)
                {
                    _ShowGiftFields = bool.Parse(Settings["showXmasGiftFields"].ToString());
                }
                ShowXmasGiftFieldsSection.Visible = _ShowGiftFields;



                if (Settings.Contains("showClientIdCard") && Settings["showClientIdCard"] != null)
                {
                    txtClientIdCard.Visible = Convert.ToBoolean(Settings["showClientIdCard"]);
                    lblClientIdCard.Visible = Convert.ToBoolean(Settings["showClientIdCard"]);
                }

                if (_ShowOneBagOnly == false)
                {
                    PanelShowOneBagOnly.Visible = false;
                }

                if (_ShowClientType == false)
                {
                    PanelClientType.Visible = false;
                }

                if (Settings.Contains("showIncExpSummary") && Settings["showIncExpSummary"] != null)
                {
                    _ShowIncExpSummary = bool.Parse(Settings["showIncExpSummary"].ToString());
                }

                if (Settings.Contains("showExpense") && Settings["showExpense"] != null)
                {
                    _ShowExpense = bool.Parse(Settings["showExpense"].ToString());
                }
                ExpenseRow.Visible = _ShowExpense;
                if (_ShowExpense)
                {
                    lblIncomeExpense.Text = "Income & Expense";
                }
                else
                {
                    lblIncomeExpense.Text = "Income";
                }


                if (Settings.Contains("showRelationshipToClient") && Settings["showRelationshipToClient"] != null)
                {
                    _ShowAFMRelationship = bool.Parse(Settings["showRelationshipToClient"].ToString());
                }
                PanelShowAFMRelationship.Visible = bool.Parse(Settings["showRelationshipToClient"].ToString());
                gvAFM.Columns[13].Visible = _ShowAFMRelationship;
                //      gvAFM.

                if (Settings.Contains("showDisabilities") && Settings["showDisabilities"] != null)
                {
                    _ShowDisabilitiee = bool.Parse(Settings["showDisabilities"].ToString());
                }
                DisabilitiesRow.Visible = _ShowDisabilitiee;

                if (Settings.Contains("showXmasToys") && Settings["showXmasToys"] != null)
                {

                    gvAFM.Columns[2].Visible = bool.Parse(Settings["showXmasToys"].ToString());
                    if (bool.Parse(Settings["showXmasToys"].ToString()) == true)
                    {

                        HyperLinkXmas.Visible = true;
                    }
                    else
                    {
                        HyperLinkXmas.Visible = false;
                    }
                }
                else
                {
                    gvAFM.Columns[2].Visible = false;
                }

                if (Settings.Contains("xmasToysYear") && Settings["xmasToysYear"] != null)
                {
                    _XmasToysYear = Settings["xmasToysYear"].ToString();
                }
                else
                {
                    _XmasToysYear = DateTime.Now.Year.ToString();
                }

                if (Settings.Contains("reqEthnicity") && Settings["reqEthnicity"] != null)
                {
                    reqClientEthnicity.Enabled = bool.Parse(Settings["reqEthnicity"].ToString());
                    reqAFMEthnicity.Enabled = bool.Parse(Settings["reqEthnicity"].ToString());
                    _IsEthnicityRequired = bool.Parse(Settings["reqEthnicity"].ToString());
                }

                if (Settings.Contains("reqGender") && Settings["reqGender"] != null)
                {
                    reqClientGender.Enabled = bool.Parse(Settings["reqGender"].ToString());
                }

                LiteralIncomeEligibilityGuidelines.Text = HttpUtility.HtmlDecode(_IncomeEligibilityGuidelines.ToString());





            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        public void XmasReportLink()
        {

            try
            {
                //var strAddOn = '?SkinSrc=[G]';

                string myLink = _navigationManager.NavigateURL("XmasReport", "mid=" + this.ModuleId);
                myLink += "?cid=" + hidClientID.Value.ToString() + "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/portals/_default/containers/_default/no%20container");

             //   HyperLinkXmas.Visible = true;
                HyperLinkXmas.NavigateUrl = myLink.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void CameraLink()
        {

            try
            {
                //var strAddOn = '?SkinSrc=[G]';

                string myLink = _navigationManager.NavigateURL("Camera", "mid=" + this.ModuleId);
                myLink += "?cid=" + hidClientID.Value.ToString() + "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/portals/_default/containers/_default/no%20container");
                
                HyperLinkXmas.Visible = true;
                HyperLinkXmas.NavigateUrl = myLink.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }





        public void FillClientAgeGroups()
        {

            try
            {
                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_AgeGroupReport(Int32.Parse(hidClientID.Value.ToString()));

                rp_AgeGroupReport.DataSource = items;
                rp_AgeGroupReport.DataBind();


                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }
        protected string GetMapUrl()
        {
          //  return ;

            return "https://maps.googleapis.com/maps/api/js?key=" + _GoogleAPIKey.ToString() + "&callback=load";
        }

        protected string GetLabelContent()
        {
            //  return ;

            return "JOE TESTFRIDAY/nClientID: 6/n3 Bags";
        }

        public void FillBudget()
        {
            _budget = (_income - _expense);
            lblBudget.Text = Localization.GetString("lblBudget", this.LocalResourceFile) + _budget.ToString("C", CultureInfo.CurrentCulture);
            if (_expense > _income)
            {
                // lblBudget.BackColor = System.Drawing.Color.Pink;
                lblBudget.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblBudget.ForeColor = System.Drawing.Color.Green;
            }


            if (_ShowIncExpSummary == false)
            {
                lblBudget.Text = "";
            }

        }
        protected void gvAFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                FillAFMGrid();
                gvAFM.PageIndex = e.NewPageIndex;
                gvAFM.DataBind();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        //gvAFM_OnDataBinding
        //protected void gvAFM_OnDataBinding(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        bool _isValid = true;

        //        foreach (GridViewRow tt in gvAFM.Rows)
        //        {
        //            if (tt.RowType == DataControlRowType.DataRow)
        //            {
        //                string age = tt.Cells[6].Text;

        //                if (age == "-1")
        //                {
        //                    _isValid = false;
        //                }
        //            }

        //        }

        //        if (_isValid == false)
        //        {
        //            lblMessage.Text += "<br />" + Localization.GetString("ErrorNoDateOfBirthAFM", this.LocalResourceFile);
        //            lblMessage.Visible = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}


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

                    int afmID = Convert.ToInt32(e.CommandArgument);

                    FBClientsController controller = new FBClientsController();

                    controller.FBClients_AFM_Delete(afmID);
                    //controller.FBClients_IncomeExpense_Delete(ieID);
                    //FillIncomeExpenseGrids();
                    FillAFMGrid();

                }

                if (e.CommandName == "Edit")
                {
                    int aFMID = Convert.ToInt32(e.CommandArgument);


                    FillAFMEdit(aFMID);

                }

                if (e.CommandName == "Christmas")
                {
                    int aFMID = Convert.ToInt32(e.CommandArgument);

                    FillAFMEdit(aFMID);
                    LoadChristmasToys(aFMID);
                    // SHOW CHRISTMAS FORM
                    formChristmas.Visible = true;


                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        //
        public void LoadChristmasToys(int itemID)
        {

            try
            {
                // CURRENT YEAR - GET FROM SETTINGS
                int _currentYear = Int32.Parse(_XmasToysYear.ToString());
                //load the item  
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBxMas_AFM_Get_CurrentYear(itemID, _currentYear);
                //controller.FBClients_Visit_GetByID(itemID);

                if (item != null)
                {
                    //if (UserInfo.IsInRole(_ClientManagerDeleteRecordRole))
                    //{
                    //    LinkButtonDeleteXMasRecord.Visible = true;
                    //}
                    
                    hidXmasID.Value = item.XmasID.ToString();
                    txtxMasYear.Text = item.XmasYear.ToString();
                    ddlSizes.SelectedValue = item.XmasSizes.ToString();
                    cbxBikeRaffle.Checked = item.BikeRaffle;
                    if (item.AFM_Age >= _XmasRequireSizeMinimumAge && item.AFM_Age <= _XmasRequireSizeMaxAge)
                    {
                        XmasSizes.Visible = true;
                        RequiredFieldValidatorXmasSizes.Enabled = true;
                    }
                    else
                    {
                        XmasSizes.Visible = false;
                        RequiredFieldValidatorXmasSizes.Enabled = false;
                    }
                    if (item.AFM_Age <= 8)
                    {
                        cbxBikeRaffle.Text = "DISABLED - Child must be 9 or older to enter.";
                        cbxBikeRaffle.Enabled = false;
                    }
                    else
                    {
                        cbxBikeRaffle.Text = "";
                        cbxBikeRaffle.Enabled = true;
                    }

                    if (_ShowGiftFields == true)
                    {
                        XmasSizes.Visible = false;
                        RequiredFieldValidatorXmasSizes.Enabled = false;
                    }

                    if (item.BikeAwardedDate.Length > 10)
                    {
                        txtBikeAwardedDate.Text = DateTime.Parse(item.BikeAwardedDate.ToString()).ToShortDateString();
                    }
                    else
                    {
                        txtBikeAwardedDate.Text = "";
                    }
                    //txtBikeAwardedDate.Text = item.BikeAwardedDate.ToString();

                    txtXmasNotes.Text = item.XmasNotes.ToString();


                    if (item.ReceivedToysDate.Length >= 10)
                    {
                        txtReceivedToysDate.Text = DateTime.Parse(item.ReceivedToysDate.ToString()).ToShortDateString();
                    }

                    txtXmasGift1.Text = item.XmasGift1.ToString();
                    txtXmasGift1Size.Text = item.XmasGift1Size.ToString();
                    txtXmasGift2.Text = item.XmasGift2.ToString();
                    txtXmasGift2Size.Text = item.XMasGift2Size.ToString();
                    txtXmasGiftRecordID.Text = item.XmasGiftRecordID.ToString();
                    LabelXmasRecordID.Text=item.XmasID.ToString();
                }
                else
                {
                    txtxMasYear.Text = _XmasToysYear.ToString();
                }

                //GridViewXmasHistory
                List<FBClientsInfo> history;
                history = controller.FBxMas_AFM_Get_History_By_AFM_ID(itemID);
                GridViewXmasHistory.DataSource = history;
                GridViewXmasHistory.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void FillAFMEdit(int itemID)
        {

            try
            {

                //load the item  
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_AFM_GetByID(itemID);
                //controller.FBClients_Visit_GetByID(itemID);

                if (item != null)
                {
                    ddlAFMGender.SelectedValue = item.AFMGender.ToString();
                    hidClientAFMID.Value = item.ClAddFamMemID.ToString();
                    txtAFM_FirstName.Text = item.ClAddFamMemFirstName.ToString();
                    txtAFM_LastName.Text = item.ClAddFamMemLastName.ToString();
                    txtAFM_MiddleInitial.Text = item.AFMMiddleInitial.ToString();
                    ddlAFMSuffix.SelectedValue = item.AFMSuffix.ToString();
                    cbxAFMDOB_Verified.Checked = item.AFMDOBVerify;
                    //  radAFM_DOB.SelectedDate = item.ClAddFamMemDOB;
                    if (item.ClAddFamMemDOB >= d)
                    {
                        txtAFM_DOB.Text = item.ClAddFamMemDOB.ToShortDateString();
                    }

                    ddlAFMRelationship.SelectedValue = item.AFMRelationship.ToString();

                   // ddlAFMEthnicity.SelectedValue = item.AFMEthnicity.ToString();

                    if (ddlAFMEthnicity.Items.FindByText(item.AFMEthnicity.ToString().TrimEnd()) != null)
                    {
                        ddlAFMEthnicity.SelectedValue = item.AFMEthnicity.ToString().TrimEnd();
                    }


                    if (item.AFM_Age >= _XmasRequireSizeMinimumAge && item.AFM_Age <= _XmasRequireSizeMaxAge)
                    {
                        XmasSizes.Visible = true;
                        RequiredFieldValidatorXmasSizes.Enabled = true;
                    }
                    else
                    {
                        XmasSizes.Visible = false;
                        RequiredFieldValidatorXmasSizes.Enabled = false;

                    }

                    if (_ShowGiftFields == true)
                    {
                        XmasSizes.Visible = false;
                        RequiredFieldValidatorXmasSizes.Enabled = false;
                    }

                    if (item.AFM_Age < 9)
                    {
                        cbxBikeRaffle.Text = "DISABLED - Child must be 9 or older to enter.";
                        cbxBikeRaffle.Enabled = false;
                    }

                        lblToySignup.Text = "Christmas Toy Sign-up for " + item.ClAddFamMemFirstName.ToString() + " " + item.ClAddFamMemLastName.ToString() + " - " + item.AFMGender.ToString() + " - Age " + item.AFM_Age.ToString();

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvIncome_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No requirement to implement code here
        }

        protected void gvIncome_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Prevents the GridView from going into EDIT MODE (textboxes)
            e.Cancel = true;
        }

        protected void gvIncome_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Delete")
                {

                    int ieID = Convert.ToInt32(e.CommandArgument);

                    FBClientsController controller = new FBClientsController();

                    controller.FBClients_IncomeExpense_Delete(ieID);
                    FillIncomeExpenseGrids();

                }

                if (e.CommandName == "Edit")
                {
                    int ieID = Convert.ToInt32(e.CommandArgument);

                    FillIncomeExpenseEdit(ieID);


                }




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvExpense_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No need to implement code here
        }

        protected void gvExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Delete")
                {
                    String ID = e.CommandArgument.ToString();

                    int ieID = Int32.Parse(ID.ToString());

                    FBClientsController controller = new FBClientsController();

                    controller.FBClients_IncomeExpense_Delete(ieID);
                    FillIncomeExpenseGrids();

                }

                if (e.CommandName == "Edit")
                {
                    int ieID = Convert.ToInt32(e.CommandArgument);

                    FillIncomeExpenseEdit(ieID);

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvExpense_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Prevents the GridView from going into EDIT MODE (textboxes)
            e.Cancel = true;

        }



        public void FillIncomeExpenseEdit(int itemID)
        {

            try
            {

                //load the item
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_IncomeExpense_GetByID(itemID);


                if (item != null)
                {

                    if (item.IeType == "IN")
                    {
                        ddlClientIncome.SelectedValue = item.IeDescription.ToString();
                        txtIeAmount.Text = item.IeAmount.ToString();
                        hidIeID.Value = item.IeID.ToString();
                    }
                    else
                    {
                        ddlClientExpense.SelectedValue = item.IeDescription.ToString();
                        txtIeExpenseAmount.Text = item.IeAmount.ToString();
                        hidIeExpenseID.Value = item.IeID.ToString();
                    }


                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvAFM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             
                string columnValue = ((System.Web.UI.WebControls.Label)e.Row.FindControl("lblAFMEthnicity")).Text;

                if ((columnValue.ToString().Length < 1) && (_IsEthnicityRequired == true))
                {
                    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoEthnicity", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                    //return;
                }
                else
                // Hide Ethnicity column
                {
                    gvAFM.Columns[11].Visible = false;

                }
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

                items = controller.FBClients_AFM_List(Int32.Parse(hidClientID.Value.ToString()));

                gvAFM.DataSource = items;
                gvAFM.DataBind();


                bool _isValidBOB = true;
                bool _isVerified = true;
                foreach (GridViewRow tt in gvAFM.Rows)
                {
                    if (tt.RowType == DataControlRowType.DataRow)
                    {
                        string age = tt.Cells[7].Text;

                        if (age == "-1")
                        {
                            _isValidBOB = false;
                            break;
                        }

                    }
                }

                foreach (GridViewRow vv in gvAFM.Rows)
                {
                    if (vv.RowType == DataControlRowType.DataRow)
                    {
                        
                        string verify = vv.Cells[8].Text;
                        
                        if (verify.ToString().ToLower() == "false")
                        {

                            _isVerified = false;
                            break;
                        }
                        
                    }
                }

                gvAFM.Columns[8].Visible = false;


                if (_isValidBOB == false)
                {
                    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoDateOfBirthAFM", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                }
                if (_isVerified == false)
                {
                    lblMessage.Text += "<br />" + Localization.GetString("ErrorNoAFMVerify", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }




        public void FillVisitsGrid()
        {

            try
            {
                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_Visit_List(Int32.Parse(hidClientID.Value.ToString()));

                gvVisits.DataSource = items;
                gvVisits.DataBind();

                if (UserInfo.IsInRole(_ClientManagerDeleteRecordRole))
                {
                    gvVisits.Columns[2].Visible = true;
                }
                else
                {
                    gvVisits.Columns[2].Visible = false;
                }

                if (_ShowSendText)
                {
                    gvVisits.Columns[0].Visible = true;
                }
                else
                {
                    gvVisits.Columns[0].Visible = false;
                }

                //JMA
                //for (int i = 0; i < items.Count; i++) // Loop through List with for
                //{
                //    Console.WriteLine(items[i]);
                //}


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        // CLIENT VISITS
        protected void gvVisits_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                FillVisitsGrid();
                gvVisits.PageIndex = e.NewPageIndex;
                gvVisits.DataBind();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        //


        protected void gvVisits_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Prevents the GridView from going into EDIT MODE (textboxes)
            e.Cancel = true;

        }

        protected void gvVisits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
             //   var commandkeyValue = e.CommandName;

                if (e.CommandName == "SendOrderSheet")
                {
                    var commandkey = e.CommandArgument;
                    string key = commandkey.ToString();
                    char separator = '-'; // Space character
                    string[] keys = key.Split(separator); // returned array
                    int visitID = Int32.Parse(keys[0].ToString());
                    int clientID = Int32.Parse(keys[1].ToString());
                  //  var enstr1 = WebUtility.HtmlEncode(key.ToString());
                    
                    //// Part 2: reverse the change.
                    // string decoded = WebUtility.HtmlDecode(encoded);

                    string orderLink = _FBName.ToString() + " Ordering: " + _ClientOrderPage.ToString() + key.ToString();

                    //lblMessage.Text = _ClientCellNumber.ToString() + "<br />" + orderLink.ToString();
                    //ErrorMessage.Visible = true;
                    if (hidClientCellNumber.Value.ToString().Length > 0)
                    {
                        SendTwilioMessage(orderLink.ToString());
                        
                        FBClientsController controller = new FBClientsController();
                        FBClientsInfo item = new FBClientsInfo();
                        item.OrderStatusCode = 1;
                        item.VisitID = visitID;
                        controller.UpdateVisitOrderStatusCode(item);

                        _TwilioMessageSent = true;
                        FillVisitsGrid();
                    }

                }

                if (e.CommandName == "Edit")
                {
                    int visitID = Convert.ToInt32(e.CommandArgument);


                    FillVisitEdit(visitID);

                }
                if (e.CommandName == "DeleteVisit")
                {
                    int visitID = Convert.ToInt32(e.CommandArgument);

                    FBClientsController controller = new FBClientsController();
                    controller.FBClients_Visit_Delete(visitID);

                    FillVisitsGrid();
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //static string _TwilioAccountSid = "";   
        //static string _TwilioAuthToken = "";   JMA
        //static string _TwilioPhoneNumber = "";
        //static string _ClientCellNumber = "";

        public void SendTwilioMessage(string _message)
        {
            string accountSid = _TwilioAccountSid.ToString();   
            string authToken = _TwilioAuthToken.ToString();       
            string twilioPhoneNumber = _TwilioPhoneNumber.ToString();

            TwilioSMS twilioSMS = new TwilioSMS(accountSid, authToken, twilioPhoneNumber);
            twilioSMS.SendSMS(hidClientCellNumber.Value.ToString(), _message.ToString());

            lblMessage.Text += "<br />" + Localization.GetString("TwilioMessageSent", this.LocalResourceFile);
            ErrorMessage.Visible = true;
        }


        public void FillVisitEdit(int itemID)
        {

            try
            {

                //load the item
                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = controller.FBClients_Visit_GetByID(itemID);

                if (item != null)
                {
                    DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(btnAddClientVisit, Localization.GetString("ConfirmEditVisit", this.LocalResourceFile));

                    lblMobileLocations.Visible = true;
                    ddlMobileLocations.Visible = true;

                    txtVisitDate.Text = item.VisitDate.Date.ToShortDateString();
                    ddlVisitNumBags.SelectedValue = item.VisitNumBags.ToString();
                    txtVisitNotes.Text = item.VisitNotes;
                    hidVisitID.Value = item.VisitID.ToString();
                    
                    if (item.ClientSignature != null)
                    {
                        lblSignatureOnFile.Visible = true;
                        ImgInitials.Visible = true;
                        byte[] imagem = (byte[])(item.ClientSignature);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        HiddenFieldSignData.Value = PROFILE_PIC;

                        ImgInitials.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                        ImgInitials.AlternateText = "Signature";

                    }
                    else
                    {
                        ImgInitials.Visible = false;

                    }

                    if (ddlMobileLocations.Items.FindByText(item.ServiceLocation.ToString().TrimEnd()) != null)
                    {
                        ddlMobileLocations.SelectedValue = item.ServiceLocation.ToString().TrimEnd();
                    }
                    else
                    {
                        //  ddlMobileLocations.SelectedValue = item.ServiceLocation.ToString();
                        //  ddlMobileLocations.Items.Insert(1, new ListItem(item.ServiceLocation.ToString().Trim(), item.ServiceLocation.ToString().TrimEnd()));
                        ddlMobileLocations.SelectedValue = item.ServiceLocation.ToString().TrimEnd();
                    }



                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        public void GetCaseWorkers()
        {

            try
            {
                string roleName = _ClientManagerUserRole.ToString();

                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClientsGetCaseWorkers(PortalId, roleName);


                ddlCaseWorker.DataSource = items;
                ddlCaseWorker.DataTextField = "CaseWorkerName";
                ddlCaseWorker.DataValueField = "clientCaseWorker";

                ddlCaseWorker.DataBind();
                ddlCaseWorker.Items.Insert(0, new ListItem("--Select--", "0"));

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

                var regions = new ListController().GetListEntryInfoItems("Region", "Country.US", this.PortalId);

                ddlState.DataTextField = "Value";
                ddlState.DataValueField = "Value";
                ddlState.DataSource = regions;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("-Select-", "-1"));
                ddlState.SelectedValue = "MA";


                var AFMRelationShips = new ListController().GetListEntryInfoItems("ClientAFMRelationship", "", this.PortalId);

                ddlAFMRelationship.DataTextField = "Text";
                ddlAFMRelationship.DataValueField = "Value";
                ddlAFMRelationship.DataSource = AFMRelationShips;
                ddlAFMRelationship.DataBind();
                ddlAFMRelationship.Items.Insert(0, new ListItem("--Select--", ""));

                // ddlClientIncome

                var ClientIncome = new ListController().GetListEntryInfoItems("ClientIncome", "", this.PortalId);

                ddlClientIncome.DataTextField = "Text";
                ddlClientIncome.DataValueField = "Value";
                ddlClientIncome.DataSource = ClientIncome;
                ddlClientIncome.DataBind();
                ddlClientIncome.Items.Insert(0, new ListItem("--Select--", ""));

                var ClientExpense = new ListController().GetListEntryInfoItems("ClientExpense", "", this.PortalId);

                ddlClientExpense.DataTextField = "Text";
                ddlClientExpense.DataValueField = "Value";
                ddlClientExpense.DataSource = ClientExpense;
                ddlClientExpense.DataBind();
                ddlClientExpense.Items.Insert(0, new ListItem("--Select--", ""));

                //ddlClientSuffix
                var ClientSuffix = new ListController().GetListEntryInfoItems("ClientSuffix", "", this.PortalId);

                ddlClientSuffix.DataTextField = "Text";
                ddlClientSuffix.DataValueField = "Value";
                ddlClientSuffix.DataSource = ClientSuffix;
                ddlClientSuffix.DataBind();
                ddlClientSuffix.Items.Insert(0, new ListItem("--", ""));

                //ddlAFMSuffix

                ddlAFMSuffix.DataTextField = "Text";
                ddlAFMSuffix.DataValueField = "Value";
                ddlAFMSuffix.DataSource = ClientSuffix;
                ddlAFMSuffix.DataBind();
                ddlAFMSuffix.Items.Insert(0, new ListItem("--", ""));

                //Ethnicity
                var Ethnicity = new ListController().GetListEntryInfoItems("ClientEthnicity", "", this.PortalId);

                ddlClientEthnicity.DataTextField = "Text";
                ddlClientEthnicity.DataValueField = "Value";
                ddlClientEthnicity.DataSource = Ethnicity;
                ddlClientEthnicity.DataBind();
                ddlClientEthnicity.Items.Insert(0, new ListItem("--", ""));

                //ddlAFMEthnicity

                ddlAFMEthnicity.DataTextField = "Text";
                ddlAFMEthnicity.DataValueField = "Value";
                ddlAFMEthnicity.DataSource = Ethnicity;
                ddlAFMEthnicity.DataBind();
                ddlAFMEthnicity.Items.Insert(0, new ListItem("--", ""));

                var cLanguage = new ListController().GetListEntryInfoItems("ClientLanguage", "", this.PortalId);

                ddlClientLanguage.DataTextField = "Text";
                ddlClientLanguage.DataValueField = "Value";
                ddlClientLanguage.DataSource= cLanguage; 
                ddlClientLanguage.DataBind();
                ddlClientLanguage.Items.Insert(0, new ListItem("--", ""));

                // cblClientTrueFalseQuestions
                var TFQuestions = new ListController().GetListEntryInfoItems("ClientTrueFalseQuestions", "", this.PortalId);

                cblClientTrueFalseQuestions.DataTextField = "Text";
                cblClientTrueFalseQuestions.DataValueField = "Value";
                //   cblClientTrueFalseQuestions.RepeatColumns = 1;
                cblClientTrueFalseQuestions.DataSource = TFQuestions;
                cblClientTrueFalseQuestions.DataBind();

                //cblClientDisability
                var Disabilities = new ListController().GetListEntryInfoItems("ClientDisability", "", this.PortalId);
                cblClientDisability.DataTextField = "Text";
                cblClientDisability.DataValueField = "Value";
                cblClientDisability.DataSource = Disabilities;
                cblClientDisability.DataBind();




                var town = new ListController().GetListEntryInfoItems("Town", "", this.PortalId);

                ddlCity.DataTextField = "Text";
                ddlCity.DataValueField = "Value";
                ddlCity.DataSource = town;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("Select " + Localization.GetString("lblCityStateZip", this.LocalResourceFile), "-1"));



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


        // CLIENT UPDATE
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveClientRecord();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        // CLIENT DELETE
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteClientRecord();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void DeleteClientRecord()
        {

            try
            {

                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = new FBClientsInfo();

                if (hidClientID.Value.ToString().Length > 0)
                {
                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());

                    controller.DeleteFBClients(item.ClientID);
                    // controller.FBClients_Update(item);

                }

                Response.Redirect(_navigationManager.NavigateURL(), true);

                //    FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void SaveClientRecord()
        {

            try
            {

                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = new FBClientsInfo();

                item.ModuleId = this.ModuleId;
                item.PortalId = this.PortalId;
                item.CreatedByUserID = this.UserId;

                item.ClientFirstName = txtClientFirstName.Text.ToString().Trim().ToUpper();
                item.ClientMiddleInitial = txtClientMiddleInitial.Text.ToString().Trim().ToUpper();
                item.ClientLastName = txtClientLastName.Text.ToString().Trim().ToUpper();
                item.ClientAddress = txtAddress.Text.ToString().Trim().ToUpper();
                //   item.ClientCity = txtCity.Text.ToString().Trim();
                item.ClientCity = ddlCity.SelectedValue.ToString();

                if (ddlCity.SelectedValue.ToString().ToLower() == "other")
                {
                    ddlTown.Visible = false;
                    item.ClientTown = txtOtherTown.Text.ToString().ToUpper();
                }
                else
                {
                    item.ClientTown = ddlTown.SelectedValue.ToString();

                }


                item.ClientZipCode = txtZip.Text.ToString().Trim();
                item.ClientState = ddlState.SelectedValue.ToString();
                item.ClientPhone = txtPhone.Text.ToString();
                item.ClientPhoneType = ddlClientPhoneType.SelectedValue.ToString();
                item.ClientCaseWorker = Int32.Parse(ddlCaseWorker.SelectedValue);
                item.ClientEmailAddress = txtEmail.Text.ToString().Trim();
                item.ClientIdCard = txtClientIdCard.Text.ToString().Trim();
                if (txtClientDOB.Text.ToString().Length > 0)
                {
                    item.ClientDOB = Convert.ToDateTime(txtClientDOB.Text.ToString());
                }
                else
                {
                    item.ClientDOB = DateTime.MinValue;
                }

                //FBClients_Insert
                if (txtVerifyDate.Text.ToString().Length > 0)
                {
                    item.ClientVerifyDate = Convert.ToDateTime(txtVerifyDate.Text.ToString());
                }
                else
                {
                    item.ClientVerifyDate = DateTime.MinValue;
                }

                item.ClientSuffix = ddlClientSuffix.SelectedValue.ToString();
                item.ClientDOBVerify = cbxDOB_Verified.Checked;
                item.ClientEthnicity = ddlClientEthnicity.SelectedValue.ToString();
                item.ClientLanguage = ddlClientLanguage.SelectedValue.ToString();
                item.ClientNote = txtClientNote.Text.ToString().Trim();
                item.ClientUnit = txtUnit.Text.ToString().Trim();
                item.ClientGender = ddlClientGender.SelectedValue.ToString();
                item.ClientType = ddlClientType.SelectedValue.ToString();
                item.IsActive = Convert.ToBoolean(ddlClientStatus.SelectedValue.ToString());
                item.IsLocked = cbxIsLocked.Checked;
                // VERIFY ADDRESS
                item.ClientAddressVerify = cbxClientAddressVerify.Checked;

                if (txtClientAddressVerifyDate.Text.ToString().Length > 0)
                {
                    item.ClientAddressVerifyDate = Convert.ToDateTime(txtClientAddressVerifyDate.Text.ToString());
                }
                else
                {
                    item.ClientAddressVerifyDate = DateTime.MinValue;
                }

                item.OneBagOnly = cbxOneBagOnly.Checked;
                item.Disability = GetDisabilities();

                item.ServiceLocation = ddlClientServiceLocation.SelectedValue.ToString();

                //RECORD FLAGGED FOR REVIEW - NEED TO SEND AN EMAIL
                item.SubjectToReview = cbxSubjectToReview.Checked;

                if (item.SubjectToReview)
                {

                    EmailNotificationHTML(BuildEmailContent(item.ClientFirstName + " " + item.ClientLastName + "<br />" + item.ClientNote), item.ClientFirstName + " " + item.ClientLastName + " Flagged For Review");
                }


                if (hidClientID.Value.ToString().Length > 0)
                {
                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());
                    item.LastModifiedByUserID = this.UserId;
                    //UPDATE CLIENT RECORD


                    if (txtLatitude.Text.ToString().Length > 2 || txtLongitude.Text.ToString().Length > 2)
                    {
                        item.Latitude = double.Parse(txtLatitude.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        item.Longitude = double.Parse(txtLongitude.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {

                    }



                    controller.FBClients_Update(item);

                    lblMessage.Text = Localization.GetString("ClientUpdateSuccessful", this.LocalResourceFile) + "<br />";
                    ErrorMessage.Visible = true;

                    if (item.Latitude != Null.NullDouble)
                    {
                     //   litMapCenter.Text = "var center = new GLatLng(" + txtLatitude.Text + ", " + txtLongitude.Text + ");map.setCenter(center, 15);";
                        litMapCenter.Text = "var uluru = { lat: " + txtLatitude.Text + ", lng: " + txtLongitude.Text + " };";

                    }

                }
                else
                {
                    //INSERT CLIENT RECORD
                    item.CreatedByUserID = this.UserId;


                    int MyNewID = Null.NullInteger;
                    MyNewID = controller.FBClients_Insert(item);
                    hidClientID.Value = Convert.ToString(MyNewID);
                                        
                    clientId = MyNewID;

                    XmasReportLink();

                    lblMessage.Text = Localization.GetString("ClientInsertSuccessful", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                    LoadSettings();
                    SetPhotoIDLink();

                }
                UpdateTrueFalseQuestions();
                FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void EmailNotificationHTML(string content, string subject)
        {

            try
            {

                string _subject = subject.ToString();
                string body = content.ToString();


                if (_FlagForReviewNotify.ToString().Length > 1)
                {

                    string[] valuePair = _FlagForReviewNotify.Split(new char[] { ',' });

                    for (int i = 0; i <= valuePair.Length - 1; i++)
                    {
                        UserInfo _currentUser = DotNetNuke.Entities.Users.UserController.GetUserById(this.PortalId, Int32.Parse(valuePair[i].ToString().Trim()));
                        var notificationType = NotificationsController.Instance.GetNotificationType("HtmlNotification");
                        // NEED THE PORTALID HERE AND AGENTID
                        var sender = UserController.GetUserById(this.PortalId, this.UserId);
                        var notification = new Notification { NotificationTypeID = notificationType.NotificationTypeId, Subject = subject, Body = body, IncludeDismissAction = true, SenderUserID = sender.UserID };
                        NotificationsController.Instance.SendNotification(notification, this.PortalId, null, new List<UserInfo> { _currentUser });

                        // DotNetNuke.Services.Mail.Mail.SendEmail(FromPurchaserEmail, valuePair[i].ToString().Trim(), settingsData.EmailSubject, EmailContent.ToString());
                        //   DotNetNuke.Services.Mail.Mail.SendMail(FromPurchaserEmail, valuePair[i].ToString().Trim(), "", settingsData.EmailSubject, EmailContent.ToString(), "", "HTML", "", "", "", "");
                    }

                    //RESET VALUE TO PREVENT MULTIPLE NOTIFICATIONS
                    _FlagForReviewNotify = "";

                }


                // NEED THE PORTALID HERE INSTEAD OF A ZERO


                //string EmailContent = content;

                ////GET THE FROM ADDRESS
                //PortalSettings mySettings = new PortalSettings();
                //string EmailFrom = mySettings.Email.ToString();


                //DotNetNuke.Services.Mail.Mail.SendMail(EmailFrom.ToString(), emailto.ToString(), "", subject, EmailContent.ToString(), "", "HTML", "", "", "", "");

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }

        }

        public string BuildEmailContent(string ClientName)
        {
            try
            {

                var portalAlias = PortalSettings.Current.PrimaryAlias;


                StringBuilder EmailContentHTML = new StringBuilder();
                EmailContentHTML.Capacity = 5000;


                EmailContentHTML.Append("<style type=\"text/css\">" + Environment.NewLine);
                EmailContentHTML.Append(".Section{font-weight: bold; font-family: Verdana, Tahoma;font-size: 14px;}" + Environment.NewLine);
                EmailContentHTML.Append(".Value{font-weight: normal; font-family: Verdana, Tahoma;font-size: 12px;}" + Environment.NewLine);
                EmailContentHTML.Append(".Footer{font-weight: normal; font-family: Verdana, Tahoma;font-size: 10px;line-height:150%;}" + Environment.NewLine);
                EmailContentHTML.Append("</style>" + Environment.NewLine + Environment.NewLine);



                EmailContentHTML.Append(Environment.NewLine);
                EmailContentHTML.Append("<p class=\"Section\">Client Flagged for Review.</p>" + Environment.NewLine);
                EmailContentHTML.Append("<p class=\"Value\">" + ClientName.ToString() + "</p>" + Environment.NewLine);


                EmailContentHTML.Append("<p class=\"Value\"><a href=\"https://" + PortalSettings.PortalAlias.HTTPAlias.ToString() 
                    + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "\">CLICK HERE TO VIEW THE CLIENT</a></p>" + Environment.NewLine);

                EmailContentHTML.Append("<p class=\"Footer\">Some e-mail clients do not support links, cut 'n paste the URL below into a web browser.<br />https://" + PortalSettings.PortalAlias.HTTPAlias.ToString() + System.Web.HttpContext.Current.Request.RawUrl.ToString() + "</p>" + Environment.NewLine);
                // EmailContentHTML.Append("</tr>" + Environment.NewLine);

                              

                return EmailContentHTML.ToString();
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return "ERROR: " + ex.ToString();
            }
        }


        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = "";

                if (Request.QueryString["lname"] != null)
                {
                    SearchParams += "&lname=" + Request.QueryString["lname"].ToString();
                }

                if (Request.QueryString["cidcard"] != null)
                {
                    SearchParams += "&cidcard=" + Request.QueryString["cidcard"].ToString();
                }

                if (Request.QueryString["fname"] != null)
                {
                    SearchParams += "&fname=" + Request.QueryString["fname"].ToString();
                }

                if (Request.QueryString["address"] != null)
                {
                    SearchParams += "&address=" + Request.QueryString["address"].ToString();
                }
                if (Request.QueryString["clientid"] != null)
                {
                    SearchParams += "&clientid=" + Request.QueryString["clientid"].ToString();
                }

                if (Request.QueryString["inactive"] != null)
                {
                    SearchParams += "&inactive=" + Request.QueryString["inactive"].ToString();
                }

                Response.Redirect(_navigationManager.NavigateURL() + "?search=1" + SearchParams.ToString(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void btnAddAFM_Click(object sender, EventArgs e)
        {
            try
            {



                if (hidClientID.Value.ToString().Length > 0)
                {

                    FBClientsController controller = new FBClientsController();
                    FBClientsInfo item = new FBClientsInfo();

                    item.ClAddFamMemFirstName = txtAFM_FirstName.Text.ToString().Trim().ToUpper();
                    item.ClAddFamMemLastName = txtAFM_LastName.Text.ToString().Trim().ToUpper();
                    item.ClAddFamMemDOB = Convert.ToDateTime(txtAFM_DOB.Text.ToString());
                    item.AFMRelationship = ddlAFMRelationship.SelectedValue.ToString();
                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());
                    item.CreatedByUserID = this.UserId;
                    item.AFMDOBVerify = cbxAFMDOB_Verified.Checked;
                    item.AFMSuffix = ddlAFMSuffix.SelectedValue.ToString();
                    item.AFMMiddleInitial = txtAFM_MiddleInitial.Text.ToString().ToUpper();
                    item.AFMEthnicity = ddlAFMEthnicity.SelectedValue.ToString();
                    item.AFMGender = ddlAFMGender.SelectedValue.ToString();

                    if (hidClientAFMID.Value.Length > 0)
                    {
                        item.ClAddFamMemID = Int32.Parse(hidClientAFMID.Value.ToString());
                        item.LastModifiedByUserID = this.UserId;

                        controller.FBClients_AFM_Update(item);

                    }
                    else
                    {
                        int MyNewID = Null.NullInteger;
                        MyNewID = controller.FBClients_AFM_Insert(item);
                        //     hidClientAFMID.Value = Convert.ToString(MyNewID);
                    }


                    if (formChristmas.Visible == true)
                    {
                        FBClientsInfo itemXmas = new FBClientsInfo();
                        itemXmas.ClAddFamMemID = Int32.Parse(hidClientAFMID.Value.ToString());
                        itemXmas.ClientID = Int32.Parse(hidClientID.Value.ToString());
                        if (txtReceivedToysDate.Text.ToString().Length > 6)
                        {
                            itemXmas.ReceivedToysDate = txtReceivedToysDate.Text.ToString();
                        }
                        else
                        {
                            itemXmas.ReceivedToysDate = null;
                        }
                        itemXmas.XmasYear = Int32.Parse(txtxMasYear.Text.ToString());
                        XmasSizes.Visible = true;
                        itemXmas.XmasSizes = ddlSizes.SelectedValue.ToString();

                        if (txtBikeAwardedDate.Text.ToString().Length > 6)
                        {
                            itemXmas.BikeAwardedDate = txtBikeAwardedDate.Text.ToString();
                        }
                        else
                        {
                            itemXmas.BikeAwardedDate = null;
                        }

                        itemXmas.BikeRaffle = cbxBikeRaffle.Checked;
                        itemXmas.WantsToys = true;
                        itemXmas.VerifiedToys = true;
                        itemXmas.LastModifiedByUserID = this.UserId;
                        itemXmas.XmasNotes = txtXmasNotes.Text.ToString();
                        itemXmas.XmasGift1 = txtXmasGift1.Text.ToString();
                        itemXmas.XmasGift1Size = txtXmasGift1Size.Text.ToString();
                        itemXmas.XmasGift2 = txtXmasGift2.Text.ToString();
                        itemXmas.XMasGift2Size=txtXmasGift2Size.Text.ToString();
                        itemXmas.XmasGiftRecordID= txtXmasGiftRecordID.Text.ToString();

                        if (hidXmasID.Value.ToString().Length > 0)
                        {
                            itemXmas.XmasID = Int32.Parse(hidXmasID.Value.ToString());
                            controller.FBxMas_AFM_Update_CurrentYear(itemXmas);
                        }
                        else
                        {
                            controller.FBxMas_AFM_Insert_CurrentYear(itemXmas);
                        }

                    }
                    //CLEAR CHRISTMAS FIELDS
                    
                  //  cbxBikeRaffle.Enabled = true;

                    txtXmasNotes.Text = "";
                    cbxBikeRaffle.Checked = false;
                    cbxBikeRaffle.Text = "";
                    cbxBikeRaffle.Enabled = true;
                    txtReceivedToysDate.Text = "";
                    ddlSizes.SelectedIndex = 0;
                    txtBikeAwardedDate.Text = "";

                    txtXmasGift1.Text = "";
                    txtXmasGift1Size.Text = "";
                    txtXmasGift2.Text = "";
                    txtXmasGift2Size.Text = "";
                    txtXmasGiftRecordID.Text = "";

                    hidXmasID.Value = "";
                    // hide the xmas form
                    formChristmas.Visible = false;

                    // CLEAR AFM FIELDS
                    hidClientAFMID.Value = "";
                    txtAFM_DOB.Text = "";

                    txtAFM_FirstName.Text = "";
                    txtAFM_LastName.Text = "";
                    txtAFM_MiddleInitial.Text = "";
                    ddlAFMRelationship.SelectedIndex = 0;
                    ddlAFMGender.SelectedIndex = 0;
                    ddlAFMSuffix.SelectedIndex = 0;
                    ddlAFMEthnicity.SelectedIndex = 0;
                    cbxAFMDOB_Verified.Checked = false;

                    lblMessage.Text = Localization.GetString("ClientUpdateAFMSuccessful", this.LocalResourceFile) + "<br />";
                    // 

                    // Load Client Rexcord
                    FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));

                }
                else
                {
                    lblMessage.Text = Localization.GetString("ErrorInsertSubRecord", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        protected void btnAddClientVisit_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] imageBytes = null;

                if (HiddenFieldSignData.Value.Length > 15)
                {
                    
                    var myImage = HiddenFieldSignData.Value.ToString();
                    // byte[] imagem = Convert.FromBase64String(myImage);
                    imageBytes = Convert.FromBase64String(myImage.Replace("data:image/png;base64,", String.Empty));
                    //string PROFILE_PIC = Convert.ToBase64String(imageBytes);
                    //ImgInitials.Visible = true;
                    //ImgInitials.ImageUrl = String.Format("data:image/png;base64,{0}", PROFILE_PIC);
                    //ImgInitials.AlternateText = "Initials";
                }
                else
                {
                //    ImgInitials.Visible = false;
                    imageBytes = null;
                }


                if (hidClientID.Value.ToString().Length > 0)
                {
                    FBClientsController controller = new FBClientsController();
                    FBClientsInfo item = new FBClientsInfo();
                    
                    item.ServiceLocation = ddlMobileLocations.SelectedValue.ToString();
                    item.VisitDate = DateTime.Parse(txtVisitDate.Text.ToString());
                    item.VisitNotes = txtVisitNotes.Text.ToString();
                    item.VisitNumBags = Int32.Parse(ddlVisitNumBags.SelectedValue.ToString());
                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());
                    item.CreatedByUserID = this.UserId;
                    item.OrderStatusCode = 0;
                    item.ClientSignature = imageBytes;
                    if (hidVisitID.Value.Length > 0)
                    {
                        item.VisitID = Int32.Parse(hidVisitID.Value.ToString());
                        item.LastModifiedByUserID = this.UserId;

                        controller.FBClients_Visit_Update(item);

                        lblMessage.Text = Localization.GetString("ClientUpdateVisitSuccessful", this.LocalResourceFile) + "<br />";

                    }
                    else
                    {
                        item.CreatedByUserID = this.UserId;

                        int count = Int32.Parse(ddlNumTimesInsterted.SelectedValue.ToString());

                        for (int i = 0; i < count; i++)
                        {
                            controller.FBClients_Visit_Insert(item);
                        }
        
                        lblMessage.Text = Localization.GetString("ClientInsertVisitSuccessful", this.LocalResourceFile) + "<br />";
                    }
                    
                    // RESET VALUES FOR VISIT FORM
                    txtVisitDate.Text = DateTime.Now.Date.ToShortDateString();
                    txtVisitNotes.Text = "";
                    ddlVisitNumBags.SelectedValue = "0";
                    hidVisitID.Value = "";
                    HiddenFieldSignData.Value = "";
                    HiddenFieldImgData.Value = "";
                    ddlMobileLocations.ClearSelection();
                    SigDiv.Attributes.Add("style", "display:none;");
                    ErrorMessage.Visible = true;
                    ImgInitials.Visible = false;
                    lblSignatureOnFile.Visible = false;
                    // Load Client Rexcord
                   // FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));
                    FillVisitsGrid();

                }
                else
                {
                    lblMessage.Text = Localization.GetString("ErrorInsertSubRecord", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void AddEditIncomeExpense(string _ieType, string _description, string _amount, int _ieID)
        {

            try
            {
                if (hidClientID.Value.ToString().Length > 0)
                {
                    FBClientsController controller = new FBClientsController();
                    FBClientsInfo item = new FBClientsInfo();

                    item.IeType = _ieType.ToString();
                    item.IeDescription = _description.ToString();
                    item.IeAmount = Convert.ToDouble(_amount.ToString());


                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());


                    if (_ieID > 0)
                    {
                        item.IeID = Int32.Parse(_ieID.ToString());
                        item.LastModifiedByUserID = this.UserId;
                        // UPDATE THE RECORD
                        controller.FBClients_IncomeExpense_Update(item);

                    }
                    else
                    {
                        item.LastModifiedByUserID = this.UserId;
                        item.CreatedByUserID = this.UserId;
                        controller.FBClients_IncomeExpense_Insert(item);

                    }


                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //ADD UPDATE INCOME EXPENSE
        protected void btnAddClientIncome_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidClientID.Value.ToString().Length > 0)
                {

                    if (ddlClientIncome.SelectedIndex > 0 && txtIeAmount.Text.ToString().Length > 0)
                    {
                        AddEditIncomeExpense("IN", ddlClientIncome.SelectedItem.ToString(), txtIeAmount.Text.ToString(), Int32.Parse(hidIeID.Value.ToString()));
                    }
                    if (ddlClientExpense.SelectedIndex > 0 && txtIeExpenseAmount.Text.ToString().Length > 0)
                    {
                        AddEditIncomeExpense("EX", ddlClientExpense.SelectedItem.ToString(), txtIeExpenseAmount.Text.ToString(), Int32.Parse(hidIeExpenseID.Value.ToString()));
                    }


                    ddlClientExpense.SelectedIndex = 0;
                    ddlClientIncome.SelectedIndex = 0;
                    txtIeAmount.Text = "";
                    txtIeExpenseAmount.Text = "";
                    hidIeID.Value = "0";
                    hidIeExpenseID.Value = "0";


                    FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));

                    //gvIncome.Dispose();
                    //gvExpense.Dispose();

                    //FillIncomeExpenseGrids(); 
                }
                else
                {
                    lblMessage.Text = Localization.GetString("ErrorInsertSubRecord", this.LocalResourceFile);
                    ErrorMessage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void FillIncomeExpenseGrids()
        {

            try
            {
                List<FBClientsInfo> income;
                FBClientsController controller = new FBClientsController();

                income = controller.FBClients_IncomeExpense_List(Int32.Parse(hidClientID.Value.ToString()), "IN");

                gvIncome.DataSource = income;
                gvIncome.DataBind();

                List<FBClientsInfo> expense;

                expense = controller.FBClients_IncomeExpense_List(Int32.Parse(hidClientID.Value.ToString()), "EX");

                gvExpense.DataSource = expense;
                gvExpense.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }




        protected void gvIncome_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IncomeTotal += double.Parse(e.Row.Cells[3].Text.TrimStart('$').ToString());

            }

            lblTotalIncome.Text = "TOTAL MONTHLY INCOME: " + IncomeTotal.ToString("C", CultureInfo.CurrentCulture)
                + "<br />TOTAL ANNUAL INCOME: " + (IncomeTotal * 12).ToString("C", CultureInfo.CurrentCulture);
            _income = IncomeTotal;
        }

        protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ExpenseTotal += double.Parse(e.Row.Cells[3].Text.TrimStart('$').ToString());

            }

            lblTotalExpense.Text = "TOTAL MONTHLY EXPENSES: " + ExpenseTotal.ToString("C", CultureInfo.CurrentCulture);
            _expense = ExpenseTotal;

        }


        protected void ddlClientIncome_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientExpense.SelectedIndex = 0;
        }

        protected void ddlClientExpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientIncome.SelectedIndex = 0;
        }

        protected void btnSaveMap_Click(object sender, EventArgs e)
        {
            try
            {
                SaveClientRecord();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            Session["ClientID"] = hidClientID.Value.ToString();
            var queryString = "?ClientID=" + hidClientID.Value.ToString() + "&contactname=" + txtClientFirstName.Text.ToString() + " " + txtClientLastName.Text.ToString();

            // myIframe.Attributes.Add("src", this.TemplateSourceDirectory + "/Camera/WebCamera.html" + queryString.ToString());
            //CamCapture
            //   myIframe.Attributes.Add("src", this.TemplateSourceDirectory + "/CamCapture/test.html" + queryString.ToString());

            //            myIframe.Attributes.Add("src", this.TemplateSourceDirectory + "/WebcamCapture/Default.aspx" + queryString.ToString());
        }

        protected void btnTrueFalseQuestions_Click(object sender, EventArgs e)
        {


            UpdateTrueFalseQuestions();

        }


        public string GetDisabilities()
        {

            try
            {
                string _disabilities = "";


                for (int i = 0; i < cblClientDisability.Items.Count; i++)
                {
                    if (cblClientDisability.Items[i].Selected)
                    {
                        _disabilities += cblClientDisability.Items[i].Text.ToString() + ',';
                    }

                }

                return _disabilities.TrimEnd(',');

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return "";
            }

        }

        public void UpdateTrueFalseQuestions()
        {

            try
            {

                FBClientsController controller = new FBClientsController();
                FBClientsInfo item = new FBClientsInfo();

                for (int i = 0; i < cblClientTrueFalseQuestions.Items.Count; i++)
                {
                    item.TfQuestion = cblClientTrueFalseQuestions.Items[i].Value.ToString();
                    item.TfAnswer = cblClientTrueFalseQuestions.Items[i].Selected;
                    item.ClientID = Int32.Parse(hidClientID.Value.ToString());
                    item.CreatedByUserID = this.UserId;
                    controller.FBClients_TrueFalseQuestions_InsertUpdate(item);
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void BindDisabilities(string _disabilities)
        {
            try
            {
                string s = _disabilities.ToString();

                if (_disabilities.ToString().Length > 0)
                {
                    string[] values = s.Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        ListItem li = cblClientDisability.Items.FindByValue(values[i].ToString().Trim());
                        if (li != null)
                        {
                            // value found
                            cblClientDisability.Items.FindByValue(values[i].ToString().Trim()).Selected = true;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void BindTrueFalseQuestions()
        {
            try
            {
                List<FBClientsInfo> items;
                FBClientsController controller = new FBClientsController();

                items = controller.FBClients_TrueFalseQuestions_List(Int32.Parse(hidClientID.Value.ToString()));
                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; ++i)
                    {
                        if (items[i].TfAnswer == true)
                        {
                            ListItem li = cblClientTrueFalseQuestions.Items.FindByValue(items[i].TfQuestion.ToString());
                            if (li != null)
                            {
                                // value found
                                cblClientTrueFalseQuestions.Items.FindByValue(items[i].TfQuestion.ToString()).Selected = true;
                            }
                            else
                            {
                                //Value not found NEED TO DELETE IT
                                FBClientsController dcontroller = new FBClientsController();
                                dcontroller.FBClients_TrueFalseQuestions_Delete(items[i].TfQuestion.ToString(), Int32.Parse(hidClientID.Value.ToString()));

                            }


                        }
                        else
                        {
                            ListItem li = cblClientTrueFalseQuestions.Items.FindByValue(items[i].TfQuestion.ToString());
                            if (li == null)
                            {
                                //Value not found NEED TO DELETE IT
                                FBClientsController dcontroller = new FBClientsController();
                                dcontroller.FBClients_TrueFalseQuestions_Delete(items[i].TfQuestion.ToString(), Int32.Parse(hidClientID.Value.ToString()));

                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var village = new ListController().GetListEntryInfoItems("Village", "Town." + ddlCity.SelectedValue.ToString(), this.PortalId);

            ddlTown.DataTextField = "Text";
            ddlTown.DataValueField = "Text";
            ddlTown.DataSource = village;
            ddlTown.DataBind();
            ddlTown.Items.Insert(0, new ListItem("Select " + Localization.GetString("lblClientVillage", this.LocalResourceFile), "-1"));

            if (ddlCity.SelectedValue.ToString().ToLower() == "other")
            {
                txtOtherTown.Visible = true;
                ddlTown.Visible = false;
                reqTown.Enabled = false;
            }
            else
            {
                txtOtherTown.Visible = false;
                ddlTown.Visible = true;
                reqTown.Enabled = true;
            }

            ddlTown.Focus();


        }

        protected void cmdMerge_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteClientRecord();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void LinkButtonMerge_Click(object sender, EventArgs e)
        {
            FBClientsController controller = new FBClientsController();
            FBClientsInfo item = new FBClientsInfo();

            if (hidClientID.Value.ToString().Length > 0)
            {
                Response.Redirect(_navigationManager.NavigateURL(PortalSettings.ActiveTab.TabID, "ClientMerge", "mid=" + ModuleId.ToString() + "&clientid=" + hidClientID.Value.ToString()));

                //item.ClientID = Int32.Parse(hidClientID.Value.ToString());

                //controller.DeleteFBClients(item.ClientID);
                // controller.FBClients_Update(item);

            }

        }

        protected void GridViewXmasHistory_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                bool _isWinner = false;
                string win_date = "";

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    win_date = e.Row.Cells[5].Text.ToString();


                    if (win_date.Length > 7)
                    {

                        DateTime temp;
                        if (DateTime.TryParse(win_date.ToString(), out temp))
                        {
                            DateTime lastWinDate = temp.AddYears(5);
                            if (lastWinDate < DateTime.Now)
                            {
                                // do something
                                _isWinner = false;
                            }
                            else
                            {
                                // Yay :)
                                _isWinner = true;
                            }


                        }
                        else
                        {
                            // Aww.. :(
                            _isWinner = false;
                        }

                        //   _isWinner = true;
                    }

                }


                if (_isWinner == true)
                {
                    cbxBikeRaffle.Text = "DISABLED - This child has previously won a bike.";
                    cbxBikeRaffle.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        
        protected void clientType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlClientType.SelectedValue == "Individual")
            {
                txtClientFirstName.Enabled = true;
                lblClientLastName.Text = "Last Name";
                reqClientEthnicity.Enabled = true;
                reqClientGender.Enabled = true;
                txtClientMiddleInitial.Enabled = true;
                txtClientDOB.Enabled = true;
                PanelNumTimesInsterted.Visible = false;
            }

            else if (ddlClientType.SelectedValue == "Pallet")
            {
                txtClientFirstName.Text = "Pallet";
                txtClientFirstName.Enabled = false;
                lblClientLastName.Text = "Pallet Name";
                reqClientEthnicity.Enabled = false;
                reqClientGender.Enabled = false;
                txtClientMiddleInitial.Enabled = false;
                txtClientDOB.Text = "07/07/1997";
                txtClientDOB.Enabled = false;
                PanelNumTimesInsterted.Visible = true;

            }

            else
            {
                txtClientFirstName.Text = "Group Home";
                txtClientFirstName.Enabled = false;
                lblClientLastName.Text = "Group Home Name";
                reqClientEthnicity.Enabled = false;
                reqClientGender.Enabled = false;
                txtClientMiddleInitial.Enabled = false;
                txtClientDOB.Text = "07/07/1997";
                txtClientDOB.Enabled = false;
                PanelNumTimesInsterted.Visible = false;
            }
        }

        public void SetPhotoIDLink()
        {

            //HyperLinkPhotoID
            try
            {

                string myLink = _navigationManager.NavigateURL("Camera", "mid=" + this.ModuleId);
                //myLink += "?cid=" + clientId.ToString() + "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "?cid=" + clientId.ToString();

                myLink += "&SkinSrc=[G]" + DotNetNuke.Common.Globals.QueryStringEncode(DotNetNuke.UI.Skins.SkinController.RootSkin + "/" + DotNetNuke.Common.Globals.glbHostSkinFolder + "/" + "popUpSkin");
                myLink += "&ContainerSrc=";
                myLink += DotNetNuke.Common.Globals.QueryStringEncode("/Portals/_default/Containers/_default/No Container");
                //string mytemp = "https://www.gibs.com/Default.aspx?1=1";
               // myLink = "javascript:dnnModal.show('" + myLink.ToString() + "&popUp=true',false,550,950,false);";
               // myLink = "javascript:dnnModal.show('" + myLink.ToString() + "&popUp=true',false,550,950,false);";
               // myLink = myLink.Replace("&#39", "'").Replace("&amp;", "&").ToString();

                string redirectUrl = UrlUtils.PopUpUrl(myLink, this, PortalSettings, false, true);

                HyperLinkPhotoID.NavigateUrl = redirectUrl.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void btnDeleteAFMXmasRecord_Click(object sender, EventArgs e)
        {
            try
            {

                

                    FBClientsController controller = new FBClientsController();

                    controller.FBxMas_AFM_DeleteRecord(Int32.Parse(hidXmasID.Value.ToString()));

                //CLEAR CHRISTMAS FIELDS
                txtXmasNotes.Text = "";
                cbxBikeRaffle.Checked = false;
                cbxBikeRaffle.Text = "";
                cbxBikeRaffle.Enabled = true;
                txtReceivedToysDate.Text = "";
                ddlSizes.SelectedIndex = 0;
                txtBikeAwardedDate.Text = "";

                hidXmasID.Value = "";
                // hide the xmas form
               
                LinkButtonDeleteXMasRecord.Visible = false;
                formChristmas.Visible = false;

                // CLEAR AFM FIELDS
                hidClientAFMID.Value = "";
                txtAFM_DOB.Text = "";

                txtAFM_FirstName.Text = "";
                txtAFM_LastName.Text = "";
                txtAFM_MiddleInitial.Text = "";
                ddlAFMRelationship.SelectedIndex = 0;
                ddlAFMGender.SelectedIndex = 0;
                ddlAFMSuffix.SelectedIndex = 0;
                ddlAFMEthnicity.SelectedIndex = 0;
                cbxAFMDOB_Verified.Checked = false;

                lblMessage.Text = Localization.GetString("ClientDeleteXmasRecordSuccessful", this.LocalResourceFile) + "<br />";
                // 

                // Load Client Rexcord
                FillClientRecord(Int32.Parse(hidClientID.Value.ToString()));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void gvVisits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {             
                LinkButton btnLink = (LinkButton)e.Row.FindControl("LinkButtonSendOrderSheet"); 
                string visitdate = DataBinder.Eval(e.Row.DataItem, "VisitDate").ToString();
                DateTime compareDate = Convert.ToDateTime(visitdate.ToString());
                if (compareDate.Date == DateTime.Now.Date && hidClientCellNumber.Value.ToString().Length >10 )
                {
                    btnLink.Visible = true;
                }
                else
                {
                    btnLink.Visible = false;
                }
                if( _TwilioMessageSent == true)
                {
                    btnLink.Visible = false;
                }


            }
        }



    }
}