using System;
using System.Data;
using System.Configuration;
using System.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System.Web.UI.WebControls;

namespace GIBS.FBClients.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FBClientsSettings : ModuleSettingsBase
    {
        //   ModuleController controller;
        //int tabModuleId;

        //public FBClientsSettings(int tabModuleId)
        //{
        //    controller = new ModuleController();
        //    this.tabModuleId = tabModuleId;
        //}

        //protected T ReadSetting<T>(string settingName, T defaultValue)
        //{
        //    Hashtable settings = controller.GetTabModuleSettings(this.tabModuleId);

        //    T ret = default(T);

        //    if (settings.ContainsKey(settingName))
        //    {
        //        System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
        //        try
        //        {
        //            ret = (T)tc.ConvertFrom(settings[settingName]);
        //        }
        //        catch
        //        {
        //            ret = defaultValue;
        //        }
        //    }
        //    else
        //        ret = defaultValue;

        //    return ret;
        //}

        //protected void WriteSetting(string settingName, string value)
        //{
        //    controller.UpdateTabModuleSetting(this.tabModuleId, settingName, value);
        //}

        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>

        public string Recaptcha
        {
            get
            {
                if (TabModuleSettings.Contains("recaptcha"))
                    return TabModuleSettings["recaptcha"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();

                mc.UpdateTabModuleSetting(this.TabModuleId, "recaptcha", value.ToString());
            }
        }


        //public string FBName
        //{
        //    get { return ReadSetting<string>("fBName", null); }
        //    set { WriteSetting("fBName", value); }
        //}

        public string FBName
        {
            get
            {
                if (Settings.Contains("fBName"))
                    return Settings["fBName"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBName", value.ToString());
            }

        }

        public string FBAddress
        {
            get
            {
                if (Settings.Contains("fBAddress"))
                    return Settings["fBAddress"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBAddress", value.ToString());
            }

        }

        public string FBCity
        {
            get
            {
                if (Settings.Contains("fBCity"))
                    return Settings["fBCity"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBCity", value.ToString());
            }

        }

        //public string FBCity
        //{

        //    get { return ReadSetting<string>("fBCity", null); }
        //    set { WriteSetting("fBCity", value); }
        //}


        public string FBState
        {
            get
            {
                if (Settings.Contains("fBState"))
                    return Settings["fBState"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBState", value.ToString());
            }

        }

        public string FBZipCode
        {
            get
            {
                if (Settings.Contains("fBZipCode"))
                    return Settings["fBZipCode"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBZipCode", value.ToString());
            }

        }

        public string FBPhoneNumber
        {
            get
            {
                if (Settings.Contains("fBPhoneNumber"))
                    return Settings["fBPhoneNumber"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBPhoneNumber", value.ToString());
            }
        }

        public string FBFaxNumber
        {
            get
            {
                if (Settings.Contains("fBFaxNumber"))
                    return Settings["fBFaxNumber"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "fBFaxNumber", value.ToString());
            }
        }



        public string ClientManagerUserRole
        {
            get
            {
                if (Settings.Contains("clientManagerUserRole"))
                    return Settings["clientManagerUserRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "clientManagerUserRole", value.ToString());
            }
        }


        public string IDCardImagePath
        {
            get
            {
                if (Settings.Contains("iDCardImagePath"))
                    return Settings["iDCardImagePath"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "iDCardImagePath", value.ToString());
            }
        }

        public string ClientManagerDeleteRecordRole
        {
            get
            {
                if (Settings.Contains("clientManagerDeleteRecordRole"))
                    return Settings["clientManagerDeleteRecordRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "clientManagerDeleteRecordRole", value.ToString());
            }
        }

        public string DaysToValidVisit
        {
            get
            {
                if (Settings.Contains("daysToValidVisit"))
                    return Settings["daysToValidVisit"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "daysToValidVisit", value.ToString());
            }
        }

        public string ShowSendText
        {
            get
            {
                if (Settings.Contains("showSendText"))
                    return Settings["showSendText"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showSendText", value.ToString());
            }
        }

        public string ShowClientServiceLocation
        {
            get
            {
                if (Settings.Contains("showClientServiceLocation"))
                    return Settings["showClientServiceLocation"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showClientServiceLocation", value.ToString());
            }
        }

        //cbxIncludeInactiveOnSearch
        public string IncludeInactiveOnSearch
        {
            get
            {
                if (Settings.Contains("includeInactiveOnSearch"))
                    return Settings["includeInactiveOnSearch"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "includeInactiveOnSearch", value.ToString());
            }
        }


        public string ShowPhotoID
        {
            get
            {
                if (Settings.Contains("showPhotoID"))
                    return Settings["showPhotoID"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showPhotoID", value.ToString());
            }
        }

        public string ShowOneBagOnly
        {
            get
            {
                if (Settings.Contains("showOneBagOnly"))
                    return Settings["showOneBagOnly"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showOneBagOnly", value.ToString());
            }
        }

        public string ShowVisitStopLight
        {

            get
            {
                if (Settings.Contains("showVisitStopLight"))
                    return Settings["showVisitStopLight"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showVisitStopLight", value.ToString());
            }
        }

        //IncomeEligibilityGuidelines incomeEligibilityGuidelines
        public string IncomeEligibilityGuidelines
        {

            get
            {
                if (Settings.Contains("incomeEligibilityGuidelines"))
                    return Settings["incomeEligibilityGuidelines"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "incomeEligibilityGuidelines", value.ToString());
            }
        }

        // ToyTicketContent

        public string ShowIncExpSummary
        {

            get
            {
                if (Settings.Contains("showIncExpSummary"))
                    return Settings["showIncExpSummary"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showIncExpSummary", value.ToString());
            }
        }

        public string ShowExpense
        {

            get
            {
                if (Settings.Contains("showExpense"))
                    return Settings["showExpense"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showExpense", value.ToString());
            }
        }

        //ShowDisabilities showDisabilities
        public string ShowDisabilities
        {

            get
            {
                if (Settings.Contains("showDisabilities"))
                    return Settings["showDisabilities"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showDisabilities", value.ToString());
            }
        }

        public string ShowClientType
        {

            get
            {
                if (Settings.Contains("showClientType"))
                    return Settings["showClientType"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showClientType", value.ToString());
            }
        }

        public string ShowSuffix
        {

            get
            {
                if (Settings.Contains("showSuffix"))
                    return Settings["showSuffix"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showSuffix", value.ToString());
            }
        }

        public string ShowRelationshipToClient
        {

            get
            {
                if (Settings.Contains("showRelationshipToClient"))
                    return Settings["showRelationshipToClient"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showRelationshipToClient", value.ToString());
            }
        }


        public string ShowXmasToys
        {

            get
            {
                if (Settings.Contains("showXmasToys"))
                    return Settings["showXmasToys"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showXmasToys", value.ToString());
            }
        }


        public string XmasToysYear
        {

            get
            {
                if (Settings.Contains("xmasToysYear"))
                    return Settings["xmasToysYear"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "xmasToysYear", value.ToString());
            }
        }

        public string ShowXmasGiftFields
        {

            get
            {
                if (Settings.Contains("showXmasGiftFields"))
                    return Settings["showXmasGiftFields"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showXmasGiftFields", value.ToString());
            }
        }

        //xmasRequireSizeAgeRange
        public string XmasRequireSizeAgeRange
        {

            get
            {
                if (Settings.Contains("xmasRequireSizeAgeRange"))
                    return Settings["xmasRequireSizeAgeRange"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "xmasRequireSizeAgeRange", value.ToString());
            }
        }

        public string ToyTicketContent
        {
            get
            {
                if (Settings.Contains("toyTicketContent"))
                    return Settings["toyTicketContent"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "toyTicketContent", value.ToString());
            }
        }

        public string FocusableControl
        {

            get
            {
                if (Settings.Contains("focusableControl"))
                    return Settings["focusableControl"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "focusableControl", value.ToString());
            }
        }

        public string ShowClientIdCard
        {

            get
            {
                if (Settings.Contains("showClientIdCard"))
                    return Settings["showClientIdCard"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showClientIdCard", value.ToString());
            }
        }

        public string ShowPrintShoppingLabel
        {

            get
            {
                if (Settings.Contains("showPrintShoppingLabel"))
                    return Settings["showPrintShoppingLabel"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showPrintShoppingLabel", value.ToString());
            }
        }

        public string PrintShoppingLabelQuantity
        {
            get
            {
                if (Settings.Contains("printShoppingLabelQuantity"))
                    return Settings["printShoppingLabelQuantity"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "printShoppingLabelQuantity", value.ToString());
            }
        }

        public string ShowPrintBarCodeLabel
        {

            get
            {
                if (Settings.Contains("showPrintBarCodeLabel"))
                    return Settings["showPrintBarCodeLabel"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "showPrintBarCodeLabel", value.ToString());
            }
        }
        public string FlagForReviewNotify
        {

            get
            {
                if (Settings.Contains("flagForReviewNotify"))
                    return Settings["flagForReviewNotify"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "flagForReviewNotify", value.ToString());
            }
        }
        //BagAllowance
        public string BagAllowance
        {

            get
            {
                if (Settings.Contains("bagAllowance"))
                    return Settings["bagAllowance"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "bagAllowance", value.ToString());
            }
        }

        public string GroupHomeBagAllowance
        {

            get
            {
                if (Settings.Contains("groupHomeBagAllowance"))
                    return Settings["groupHomeBagAllowance"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "groupHomeBagAllowance", value.ToString());
            }
        }

        public string ReqAFMVerified
        {

            get
            {
                if (Settings.Contains("reqAFMVerified"))
                    return Settings["reqAFMVerified"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "reqAFMVerified", value.ToString());
            }
        }

        public string ReqAFMRelationship
        {

            get
            {
                if (Settings.Contains("reqAFMRelationship"))
                    return Settings["reqAFMRelationship"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "reqAFMRelationship", value.ToString());
            }
        }



        public string ReqGender
        {

            get
            {
                if (Settings.Contains("reqGender"))
                    return Settings["reqGender"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "reqGender", value.ToString());
            }
        }

        public string ReqEthnicity
        {

            get
            {
                if (Settings.Contains("reqEthnicity"))
                    return Settings["reqEthnicity"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "reqEthnicity", value.ToString());
            }
        }

        public string AllowedIPAddress
        {
            get
            {
                if (Settings.Contains("allowedIPAddress"))
                    return Settings["allowedIPAddress"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "allowedIPAddress", value.ToString());
            }
        }

        public string GoogleAPIKey
        {
            get
            {
                if (Settings.Contains("googleAPIKey"))
                    return Settings["googleAPIKey"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "googleAPIKey", value.ToString());
            }
        }

        public string TwilioAccountSid
        {
            get
            {
                if (Settings.Contains("twilioAccountSid"))
                    return Settings["twilioAccountSid"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "twilioAccountSid", value.ToString());
            }
        }

        public string TwilioAuthToken
        {
            get
            {
                if (Settings.Contains("twilioAuthToken"))
                    return Settings["twilioAuthToken"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "twilioAuthToken", value.ToString());
            }
        }

        public string TwilioPhoneNumber
        {
            get
            {
                if (Settings.Contains("twilioPhoneNumber"))
                    return Settings["twilioPhoneNumber"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "twilioPhoneNumber", value.ToString());
            }
        }
        public string ClientOrderPage
        {
            get
            {
                if (Settings.Contains("clientOrderPage"))
                    return Settings["clientOrderPage"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "clientOrderPage", value.ToString());
            }
        }

        #endregion
    }
}