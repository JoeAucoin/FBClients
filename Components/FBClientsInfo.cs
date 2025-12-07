using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.FBClients.Components
{
    public class FBClientsInfo
    {
        //private vars exposed thro the
        //properties

        //COMMON
        private int moduleId;
        private int portalId;
        private int createdByUserID;
        private DateTime createdOnDate;
        private int lastModifiedByUserID;
        private DateTime lastModifiedOnDate;
        private string createdByUserName = null;
        private string lastModifiedByUserName = null;

        //CLIENTS
        private bool isLocked;
        private bool isActive;
        private int clientID;
        private string clientFirstName;
        private string clientMiddleInitial;
        private string clientLastName;
        private string clientSuffix;
        private DateTime clientDOB;
        private bool clientDOBVerify;
        private string clientAddress;
        private string clientCity;
        private string clientTown;
        private string clientState;
        private string clientZipCode;
        private string clientEmailAddress;
        private string clientIdCard;
        private string clientPhone;
        private string clientPhoneType;
        private int clientCaseWorker;
        private string caseWorkerName;
        private int clientAge;
        private int ageGroupCount;
        private string ageGroup;
        private DateTime lastVisitDate;
        private int aFMCount;
        private double latitude;
        private double longitude;
        private string clientPhoto;
        private string clientSignatureString;
        private string clientEthnicity;
        private string clientNote;
        private string clientUnit;
        private string clientGender;
        private string clientType;
        private DateTime clientVerifyDate;
        private string clientLanguage = "";

        private bool clientAddressVerify;
        private DateTime clientAddressVerifyDate;
        private bool subjectToReview;
        private bool oneBagOnly;
        private string disability;
        private byte[] iDPhoto;
        private byte[] clientSignature = null;

        // CLIENT ADDITIONAL FAMILY MEMBERS
        private int clAddFamMemID;
        private string clAddFamMemFirstName;
        private string clAddFamMemLastName;
        private DateTime clAddFamMemDOB;
        private int aFM_Age;
        private string aFMRelationship;
        private string aFMMiddleInitial;
        private string aFMSuffix;
        private bool aFMDOBVerify;
        private string aFMEthnicity;
        private string aFMGender;

        // CHRISTMAS TOYS
        private int xMasID;
        private int xMasYear;
        private string xMasSizes;
        private bool bikeRaffle;
        private string bikeAwardedDate;
        private bool wantsToys;
        private bool verifiedToys;
        private string receivedToysDate;
        private bool qualifiedToys;  // Meets Age Criteria --- < 16 years old
        private string xMasNotes;

        private string xMasGift1;
        private string xMasGift2;
        private string xMasGift1Size;
        private string xMasGift2Size;
        private string xMasGiftRecordID;

        // CLIENT INCOME/EXPENSE
        private int ieID;
        private string ieType;
        private string ieDescription;
        private double ieAmount;



        // CLIENT VISITS
        private int visitID;
        private DateTime visitDate;
        private string visitNotes;
        private int visitNumBags;
        private string serviceLocation;
        private int orderStatusCode;

        //      OrderStatusCode
        //0	OrderEntered //Paper Process
        //1	OrderLinkSent  //Client Sent Order Link via Text
        //2	OrderSubmitted  //Client Submitted Order via Text Web Link
        //3	Order Currently Being Processed  //Order Ready in process of Fulfillment
        //4	OrderFilled  //Order filled, ready for pickup

        // CLIENT TrueFalseQuestions
        private int tfID;
        private string tfQuestion;
        private bool tfAnswer;


        /// <summary>
        /// empty cstor
        /// </summary>
        public FBClientsInfo()
        {
        }


        #region properties

        #region IncomeExpense

        public int IeID
        {
            get { return ieID; }
            set { ieID = value; }
        }

        public string IeType
        {
            get { return ieType; }
            set { ieType = value; }
        }

        public string IeDescription
        {
            get { return ieDescription; }
            set { ieDescription = value; }
        }

        public double IeAmount
        {
            get { return ieAmount; }
            set { ieAmount = value; }
        }

        #endregion


        // CLIENTS
        #region clients

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }

        }

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }

        }

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public string ClientFirstName
        {
            get { return clientFirstName; }
            set { clientFirstName = value; }
        }

        public string ClientMiddleInitial
        {
            get { return clientMiddleInitial; }
            set { clientMiddleInitial = value; }
        }

        public string ClientLastName
        {
            get { return clientLastName; }
            set { clientLastName = value; }
        }

        public string ClientSuffix
        {
            get { return clientSuffix; }
            set { clientSuffix = value; }

        }

        public DateTime ClientDOB
        {
            get { return clientDOB; }
            set { clientDOB = value; }
        }

        public bool ClientDOBVerify
        {
            get { return clientDOBVerify; }
            set { clientDOBVerify = value; }

        }

        public string ClientAddress
        {
            get { return clientAddress; }
            set { clientAddress = value; }
        }

        public string ClientCity
        {
            get { return clientCity; }
            set { clientCity = value; }
        }

        public string ClientTown
        {
            get { return clientTown; }
            set { clientTown = value; }
        }

        public string ClientState
        {
            get { return clientState; }
            set { clientState = value; }
        }

        public string ClientZipCode
        {
            get { return clientZipCode; }
            set { clientZipCode = value; }
        }

        public string ClientEmailAddress
        {
            get { return clientEmailAddress; }
            set { clientEmailAddress = value; }
        }

        public string ClientIdCard
        {
            get { return clientIdCard; }
            set { clientIdCard = value; }
        }

        public string ClientPhone
        {
            get { return clientPhone; }
            set { clientPhone = value; }
        }

        public string ClientPhoneType
        {
            get { return clientPhoneType; }
            set { clientPhoneType = value; }
        }

        public int ClientCaseWorker
        {
            get { return clientCaseWorker; }
            set { clientCaseWorker = value; }
        }

        public string CaseWorkerName
        {
            get { return caseWorkerName; }
            set { caseWorkerName = value; }
        }

        public int ClientAge
        {
            get { return clientAge; }
            set { clientAge = value; }
        }

        public int AgeGroupCount
        {
            get { return ageGroupCount; }
            set { ageGroupCount = value; }
        }

        public string AgeGroup
        {
            get { return ageGroup; }
            set { ageGroup = value; }
        }

        public DateTime LastVisitDate
        {
            get { return lastVisitDate; }
            set { lastVisitDate = value; }
        }

        public int AFMCount
        {
            get { return aFMCount; }
            set { aFMCount = value; }
        }

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public string ClientPhoto
        {
            get { return clientPhoto; }
            set { clientPhoto = value; }
        }

        public string ClientSignatureString
        {
            get { return clientSignatureString; }
            set { clientSignatureString = value; }
        }
        public string ClientEthnicity
        {
            get { return clientEthnicity; }
            set { clientEthnicity = value; }
        }
        public string ClientNote
        {
            get { return clientNote; }
            set { clientNote = value; }
        }

        public string ClientUnit
        {
            get { return clientUnit; }
            set { clientUnit = value; }
        }

        public string ClientGender
        {
            get { return clientGender; }
            set { clientGender = value; }
        }

        public string ClientType
        {
            get { return clientType; }
            set { clientType = value; }
        }

        public DateTime ClientVerifyDate
        {
            get { return clientVerifyDate; }
            set { clientVerifyDate = value; }
        }



        public bool ClientAddressVerify
        {
            get { return clientAddressVerify; }
            set { clientAddressVerify = value; }

        }


        public DateTime ClientAddressVerifyDate
        {
            get { return clientAddressVerifyDate; }
            set { clientAddressVerifyDate = value; }
        }

        public bool SubjectToReview
        {
            get { return subjectToReview; }
            set { subjectToReview = value; }

        }

        //oneBagOnly
        public bool OneBagOnly
        {
            get { return oneBagOnly; }
            set { oneBagOnly = value; }

        }

        //disability 
        public string Disability
        {
            get { return disability; }
            set { disability = value; }
        }

        public byte[] IDPhoto
        {

            get { return iDPhoto; }
            set { iDPhoto = value; }
        }
       
        public byte[] ClientSignature
        {

            get { return clientSignature; }
            set { clientSignature = value; }
        }
        #endregion

        // CLIENT ADDITIONAL FAMILY MEMBERS
        #region Clients AFM

        public int ClAddFamMemID
        {
            get { return clAddFamMemID; }
            set { clAddFamMemID = value; }
        }

        public string ClAddFamMemFirstName
        {
            get { return clAddFamMemFirstName; }
            set { clAddFamMemFirstName = value; }
        }

        public string ClAddFamMemLastName
        {
            get { return clAddFamMemLastName; }
            set { clAddFamMemLastName = value; }
        }

        public DateTime ClAddFamMemDOB
        {
            get { return clAddFamMemDOB; }
            set { clAddFamMemDOB = value; }
        }

        public int AFM_Age
        {
            get { return aFM_Age; }
            set { aFM_Age = value; }
        }

        public string AFMRelationship
        {
            get { return aFMRelationship; }
            set { aFMRelationship = value; }
        }

        public string AFMMiddleInitial
        {
            get { return aFMMiddleInitial; }
            set { aFMMiddleInitial = value; }
        }

        public string AFMSuffix
        {
            get { return aFMSuffix; }
            set { aFMSuffix = value; }
        }

        public bool AFMDOBVerify
        {
            get { return aFMDOBVerify; }
            set { aFMDOBVerify = value; }

        }

        public string AFMEthnicity
        {
            get { return aFMEthnicity; }
            set { aFMEthnicity = value; }
        }

        public string AFMGender
        {
            get { return aFMGender; }
            set { aFMGender = value; }
        }
        //clientLanguage ClientLanguage
        public string ClientLanguage
        {
            get { return clientLanguage; }
            set { clientLanguage = value; }
        }

        // CHRISTMAS TOYS
        public int XmasID
        {
            get { return xMasID; }
            set { xMasID = value; }
        }

        public int XmasYear
        {
            get { return xMasYear; }
            set { xMasYear = value; }
        }

        public string XmasSizes
        {
            get { return xMasSizes; }
            set { xMasSizes = value; }
        }

        public bool BikeRaffle
        {
            get { return bikeRaffle; }
            set { bikeRaffle = value; }

        }

        public string BikeAwardedDate
        {
            get { return bikeAwardedDate; }
            set { bikeAwardedDate = value; }
        }

        public bool WantsToys
        {
            get { return wantsToys; }
            set { wantsToys = value; }

        }

        public bool VerifiedToys
        {
            get { return verifiedToys; }
            set { verifiedToys = value; }

        }

        //xMasNotes
        public string XmasNotes
        {
            get { return xMasNotes; }
            set { xMasNotes = value; }
        }

        public string XmasGift1
        {
            get { return xMasGift1; }
            set { xMasGift1 = value; }
        }

        public string XmasGift2
        {
            get { return xMasGift2; }
            set { xMasGift2 = value; }
        }

        public string XmasGift1Size
        {
            get { return xMasGift1Size; }
            set { xMasGift1Size = value; }
        }

        public string XMasGift2Size
        {
            get { return xMasGift2Size; }
            set { xMasGift2Size = value; }
        }

        public string XmasGiftRecordID
        {
            get { return xMasGiftRecordID; }
            set { xMasGiftRecordID = value; }
        }


        //qualifiedToys
        public bool QualifiedToys
        {
            get { return qualifiedToys; }
            set { qualifiedToys = value; }

        }

        public string ReceivedToysDate
        {
            get { return receivedToysDate; }
            set { receivedToysDate = value; }
        }


        #endregion

        // CLIENT VISITS
        #region ClientVisits
        public int VisitID
        {
            get { return visitID; }
            set { visitID = value; }
        }

        public DateTime VisitDate
        {
            get { return visitDate; }
            set { visitDate = value; }
        }

        public string VisitNotes
        {
            get { return visitNotes; }
            set { visitNotes = value; }
        }

        public int VisitNumBags
        {
            get { return visitNumBags; }
            set { visitNumBags = value; }
        }

        //mobileLocation
        public string ServiceLocation
        {
            get { return serviceLocation; }
            set { serviceLocation = value; }
        }


        #endregion


        // CLIENT TrueFalseQuestions
        public int TfID
        {
            get { return tfID; }
            set { tfID = value; }
        }

        public string TfQuestion
        {
            get { return tfQuestion; }
            set { tfQuestion = value; }
        }

        public bool TfAnswer
        {
            get { return tfAnswer; }
            set { tfAnswer = value; }

        }

        public int OrderStatusCode
        {
            get { return orderStatusCode; }
            set { orderStatusCode = value; }
        }

        // COMMON
        #region Common
        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int PortalId
        {
            get { return portalId; }
            set { portalId = value; }
        }

        public int CreatedByUserID
        {
            get { return createdByUserID; }
            set { createdByUserID = value; }
        }

        public DateTime CreatedOnDate
        {
            get { return createdOnDate; }
            set { createdOnDate = value; }
        }

        public int LastModifiedByUserID
        {
            get { return lastModifiedByUserID; }
            set { lastModifiedByUserID = value; }
        }

        public DateTime LastModifiedOnDate
        {
            get { return lastModifiedOnDate; }
            set { lastModifiedOnDate = value; }
        }

        public string LastModifiedByUserName
        {

            get { return lastModifiedByUserName; }
            set { lastModifiedByUserName = value; }

        }

        public string CreatedByUserName
        {
            get
            {
                if (createdByUserName == null)
                {
                    //int portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
                    int portalId = PortalController.Instance.GetCurrentSettings().PortalId;
                    UserController controller = new UserController();
                    UserInfo user = controller.GetUser(portalId, createdByUserID);
                    createdByUserName = user.DisplayName;
                }
                return createdByUserName;
            }
        }


        #endregion

        #endregion
    }
}
