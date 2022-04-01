using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.FBClients.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.FBClients.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */



        public abstract void FBClients_IDPhoto_Insert(int clientID, byte[] iDPhoto, int createdByUserID);

        public abstract void FBClients_DeleteClient(int clientID);

        // CLIENTS
        public abstract IDataReader FBClients_GetByID(int portalID, int clientID);
        public abstract IDataReader FBClients_GetAll(int portalID);
        public abstract IDataReader FBClients_Search(int portalId, string clientLastName, string clientIdCard, string clientFirstName, string clientID, string clientAddress, string clientCity, string clientType, string isActive, string clientDOB);

        // insert new client return clientid
        public abstract int FBClients_Insert(string clientFirstName, string clientMiddleInitial, string clientLastName, DateTime clientDOB, string clientAddress, string clientCity, string clientTown, string clientState, string clientZipCode, string clientEmailAddress, string clientIdCard, string clientPhone, string clientPhoneType, int clientCaseWorker, int moduleID, int createdByUserID, int portalID, string clientSuffix, bool clientDOBVerify, string clientEthnicity, string clientNote, string clientUnit, string clientGender, string clientType, DateTime clientVerifyDate, bool clientAddressVerify, DateTime clientAddressVerifyDate, bool subjectToReview, bool oneBagOnly, string disability, bool isActive, bool isLocked, string serviceLocation);
        public abstract void FBClients_Update(int clientID, string clientFirstName, string clientMiddleInitial, string clientLastName, DateTime clientDOB, string clientAddress, string clientCity, string clientTown, string clientState, string clientZipCode, string clientEmailAddress, string clientIdCard, string clientPhone, string clientPhoneType, int clientCaseWorker, int moduleID, int lastModifiedByUserID, int portalID, double latitude, double longitude, string clientSuffix, bool clientDOBVerify, string clientEthnicity, string clientNote, string clientUnit, string clientGender, string clientType, DateTime clientVerifyDate, bool clientAddressVerify, DateTime clientAddressVerifyDate, bool subjectToReview, bool oneBagOnly, string disability, bool isActive, bool isLocked, string serviceLocation);
        public abstract IDataReader FBClients_AgeGroupReport(int clientID);
        //   public abstract void FBClients_Photo_Update(int clientID, string ClientPhoto  );
        // CASEWORKERS
        public abstract IDataReader FBClientsGetCaseWorkers(int portalID, string roleName);

        public abstract IDataReader FBClients_Visit_GetClientLastVisitDate(int clientID);

        // CLIENTS AFM
        public abstract IDataReader FBClients_Search_AFM(int portalId, string clAddFamMemLastName, string clAddFamMemFirstName, string isActive, string aFMDOB);
        public abstract int FBClients_AFM_Insert(string clAddFamMemFirstName, string clAddFamMemLastName, DateTime clAddFamMemDOB, string aFMRelationship, int clientID, int createdByUserID, string aFMMiddleInitial, string aFMSuffix, bool aFMDOBVerify, string aFMEthnicity, string aFMGender);
        public abstract IDataReader FBClients_AFM_List(int clientID);
        public abstract IDataReader FBClients_AFM_GetByID(int clAddFamMemID);
        public abstract void FBClients_AFM_Update(int clAddFamMemID, string clAddFamMemFirstName, string clAddFamMemLastName, DateTime clAddFamMemDOB, string aFMRelationship, int clientID, int lastModifiedByUserID, string aFMMiddleInitial, string aFMSuffix, bool aFMDOBVerify, string aFMEthnicity, string aFMGender);
        public abstract void FBClients_AFM_Delete(int clAddFamMemID);

     // CHRISTMAS TOYS
        public abstract void FBxMas_AFM_DeleteRecord(int xMasID);

   
        public abstract IDataReader FBxMas_AFM_Get_CurrentYear(int clAddFamMemID, int xMasYear);
        public abstract int FBxMas_AFM_Insert_CurrentYear(int clientID, int clAddFamMemID, int xMasYear,
            string xMasSizes, bool bikeRaffle,
            string bikeAwardedDate, bool wantsToys,
            bool verifiedToys, string receivedToysDate,
            int lastModifiedByUserID, string xMasNotes);
        public abstract void FBxMas_AFM_Update_CurrentYear(int xMasID, string xMasSizes, bool bikeRaffle, string bikeAwardedDate, bool wantsToys, bool verifiedToys, string receivedToysDate, int lastModifiedByUserID, string xMasNotes);
        public abstract IDataReader FBxMas_AFM_PrintTicket(int clientID);
        public abstract IDataReader FBxMas_AFM_Get_History_By_AFM_ID(int clAddFamMemID);
        //FBxMas_AFM_Get_History_By_AFM_ID

        // CLIENT VISITS
        public abstract IDataReader FBClients_Visit_List(int clientID);
        public abstract void FBClients_Visit_Insert(DateTime visitDate, string visitNotes, int visitNumBags, int clientID, int createdByUserID, string serviceLocation);
        public abstract void FBClients_Visit_Update(int visitID, DateTime visitDate, string visitNotes, int visitNumBags, int clientID, int lastModifiedByUserID, string serviceLocation);
        public abstract IDataReader FBClients_Visit_GetByID(int visitID);

        // CLIENT INCOME & EXPENSE
        public abstract int FBClients_IncomeExpense_Insert(string ieType, string ieDescription, double ieAmount, int clientID, int createdByUserID);
        public abstract IDataReader FBClients_IncomeExpense_List(int clientID, string ieType);
        public abstract void FBClients_IncomeExpense_Delete(int ieID);
        public abstract IDataReader FBClients_IncomeExpense_GetByID(int ieID);
        public abstract void FBClients_IncomeExpense_Update(int ieID, string ieType, string ieDescription, double ieAmount, int clientID, int lastModifiedByUserID);

        // CLIENT TRUE FALSE QUESTIONS
        public abstract void FBClients_TrueFalseQuestions_InsertUpdate(string tfQuestion, bool tfAnswer, int clientID, int createdByUserID);
        public abstract IDataReader FBClients_TrueFalseQuestions_List(int clientID);
        public abstract void FBClients_TrueFalseQuestions_Delete(string TfQuestion, int clientID);

        //Client Merge
        public abstract int FBClients_Merge(int MasterClientID, int ChildClientID);

        #endregion

    }



}
