using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;

namespace GIBS.FBClients.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FBClientsSettings : ModuleSettingsBase
    {
        ModuleController controller;
        int tabModuleId;

        public FBClientsSettings(int tabModuleId)
        {
            controller = new ModuleController();
            this.tabModuleId = tabModuleId;
        }

        protected T ReadSetting<T>(string settingName, T defaultValue)
        {
            Hashtable settings = controller.GetTabModuleSettings(this.tabModuleId);

            T ret = default(T);

            if (settings.ContainsKey(settingName))
            {
                System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    ret = (T)tc.ConvertFrom(settings[settingName]);
                }
                catch
                {
                    ret = defaultValue;
                }
            }
            else
                ret = defaultValue;

            return ret;
        }

        protected void WriteSetting(string settingName, string value)
        {
            controller.UpdateTabModuleSetting(this.tabModuleId, settingName, value);
        }

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

                mc.UpdateTabModuleSetting(this.tabModuleId, "recaptcha", value.ToString());
            }
        }


        public string FBName
        {
            get { return ReadSetting<string>("fBName", null); }
            set { WriteSetting("fBName", value); }
        }

        public string FBAddress
        {

            get { return ReadSetting<string>("fBAddress", null); }
            set { WriteSetting("fBAddress", value); }
        }

        public string FBCity
        {

            get { return ReadSetting<string>("fBCity", null); }
            set { WriteSetting("fBCity", value); }
        }


        public string FBState
        {

            get { return ReadSetting<string>("fBState", null); }
            set { WriteSetting("fBState", value); }
        }

        public string FBZipCode
        {

            get { return ReadSetting<string>("fBZipCode", null); }
            set { WriteSetting("fBZipCode", value); }
        }

        public string FBPhoneNumber
        {

            get { return ReadSetting<string>("fBPhoneNumber", null); }
            set { WriteSetting("fBPhoneNumber", value); }
        }

        public string FBFaxNumber
        {
            get { return ReadSetting<string>("fBFaxNumber", null); }
            set { WriteSetting("fBFaxNumber", value); }
        }



        public string ClientManagerUserRole
        {
            get { return ReadSetting<string>("clientManagerUserRole", null); }
            set { WriteSetting("clientManagerUserRole", value); }
        }


        public string IDCardImagePath
        {
            get { return ReadSetting<string>("iDCardImagePath", null); }
            set { WriteSetting("iDCardImagePath", value); }
        }

        public string ClientManagerDeleteRecordRole
        {
            get { return ReadSetting<string>("clientManagerDeleteRecordRole", null); }
            set { WriteSetting("clientManagerDeleteRecordRole", value); }
        }

        public string DaysToValidVisit
        {
            get { return ReadSetting<string>("daysToValidVisit", null); }
            set { WriteSetting("daysToValidVisit", value); }
        }

        public string ShowSendText
        {
            get { return ReadSetting<string>("showSendText", null); }
            set { WriteSetting("showSendText", value); }
        }

        public string ShowClientServiceLocation
        {
            get { return ReadSetting<string>("showClientServiceLocation", null); }
            set { WriteSetting("showClientServiceLocation", value); }
        }

        //cbxIncludeInactiveOnSearch
        public string IncludeInactiveOnSearch
        {
            get { return ReadSetting<string>("includeInactiveOnSearch", null); }
            set { WriteSetting("includeInactiveOnSearch", value); }
        }


        public string ShowPhotoID
        {
            get { return ReadSetting<string>("showPhotoID", null); }
            set { WriteSetting("showPhotoID", value); }
        }

        public string ShowOneBagOnly
        {
            get { return ReadSetting<string>("showOneBagOnly", null); }
            set { WriteSetting("showOneBagOnly", value); }
        }

        public string ShowVisitStopLight
        {

            get { return ReadSetting<string>("showVisitStopLight", null); }
            set { WriteSetting("showVisitStopLight", value); }
        }

        //IncomeEligibilityGuidelines incomeEligibilityGuidelines
        public string IncomeEligibilityGuidelines
        {

            get { return ReadSetting<string>("incomeEligibilityGuidelines", null); }
            set { WriteSetting("incomeEligibilityGuidelines", value); }
        }

        // ToyTicketContent

        public string ShowIncExpSummary
        {

            get { return ReadSetting<string>("showIncExpSummary", null); }
            set { WriteSetting("showIncExpSummary", value); }
        }

        public string ShowExpense
        {

            get { return ReadSetting<string>("showExpense", null); }
            set { WriteSetting("showExpense", value); }
        }

        //ShowDisabilities showDisabilities
        public string ShowDisabilities
        {

            get { return ReadSetting<string>("showDisabilities", null); }
            set { WriteSetting("showDisabilities", value); }
        }

        public string ShowClientType
        {

            get { return ReadSetting<string>("showClientType", null); }
            set { WriteSetting("showClientType", value); }
        }

        public string ShowSuffix
        {

            get { return ReadSetting<string>("showSuffix", null); }
            set { WriteSetting("showSuffix", value); }
        }

        public string ShowRelationshipToClient
        {

            get { return ReadSetting<string>("showRelationshipToClient", null); }
            set { WriteSetting("showRelationshipToClient", value); }
        }


        public string ShowXmasToys
        {

            get { return ReadSetting<string>("showXmasToys", null); }
            set { WriteSetting("showXmasToys", value); }
        }


        public string XmasToysYear
        {

            get { return ReadSetting<string>("xmasToysYear", null); }
            set { WriteSetting("xmasToysYear", value); }
        }

        public string ShowXmasGiftFields
        {

            get { return ReadSetting<string>("showXmasGiftFields", null); }
            set { WriteSetting("showXmasGiftFields", value); }
        }

        //xmasRequireSizeAgeRange
        public string XmasRequireSizeAgeRange
        {

            get { return ReadSetting<string>("xmasRequireSizeAgeRange", null); }
            set { WriteSetting("xmasRequireSizeAgeRange", value); }
        }

        public string ToyTicketContent
        {
            get { return ReadSetting<string>("toyTicketContent", null); }
            set { WriteSetting("toyTicketContent", value); }
        }

        public string FocusableControl
        {

            get { return ReadSetting<string>("focusableControl", null); }
            set { WriteSetting("focusableControl", value); }
        }

        public string ShowClientIdCard
        {

            get { return ReadSetting<string>("showClientIdCard", null); }
            set { WriteSetting("showClientIdCard", value); }
        }

        public string ShowPrintShoppingLabel
        {

            get { return ReadSetting<string>("showPrintShoppingLabel", null); }
            set { WriteSetting("showPrintShoppingLabel", value); }
        }

        public string PrintShoppingLabelQuantity
        {
            get { return ReadSetting<string>("printShoppingLabelQuantity", null); }
            set { WriteSetting("printShoppingLabelQuantity", value); }
        }

        public string ShowPrintBarCodeLabel
        {

            get { return ReadSetting<string>("showPrintBarCodeLabel", null); }
            set { WriteSetting("showPrintBarCodeLabel", value); }
        }
        public string FlagForReviewNotify
        {

            get { return ReadSetting<string>("flagForReviewNotify", null); }
            set { WriteSetting("flagForReviewNotify", value); }
        }
        //BagAllowance
        public string BagAllowance
        {

            get { return ReadSetting<string>("bagAllowance", null); }
            set { WriteSetting("bagAllowance", value); }
        }

        public string GroupHomeBagAllowance
        {

            get { return ReadSetting<string>("groupHomeBagAllowance", null); }
            set { WriteSetting("groupHomeBagAllowance", value); }
        }

        public string ReqAFMVerified
        {

            get { return ReadSetting<string>("reqAFMVerified", null); }
            set { WriteSetting("reqAFMVerified", value); }
        }

        public string ReqAFMRelationship
        {

            get { return ReadSetting<string>("reqAFMRelationship", null); }
            set { WriteSetting("reqAFMRelationship", value); }
        }



        public string ReqGender
        {

            get { return ReadSetting<string>("reqGender", null); }
            set { WriteSetting("reqGender", value); }
        }

        public string ReqEthnicity
        {

            get { return ReadSetting<string>("reqEthnicity", null); }
            set { WriteSetting("reqEthnicity", value); }
        }

        public string AllowedIPAddress
        {
            get { return ReadSetting<string>("allowedIPAddress", null); }
            set { WriteSetting("allowedIPAddress", value); }
        }

        public string GoogleAPIKey
        {
            get { return ReadSetting<string>("googleAPIKey", null); }
            set { WriteSetting("googleAPIKey", value); }
        }

        public string TwilioAccountSid
        {
            get { return ReadSetting<string>("twilioAccountSid", null); }
            set { WriteSetting("twilioAccountSid", value); }
        }

        public string TwilioAuthToken
        {
            get { return ReadSetting<string>("twilioAuthToken", null); }
            set { WriteSetting("twilioAuthToken", value); }
        }

        public string TwilioPhoneNumber
        {
            get { return ReadSetting<string>("twilioPhoneNumber", null); }
            set { WriteSetting("twilioPhoneNumber", value); }
        }
        public string ClientOrderPage
        {
            get { return ReadSetting<string>("clientOrderPage", null); }
            set { WriteSetting("clientOrderPage", value); }
        }

        #endregion
    }
}
