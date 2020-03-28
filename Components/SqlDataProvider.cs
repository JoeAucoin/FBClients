using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.FBClients.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        public override void FBClients_IDPhoto_Insert(int clientID, byte[] iDPhoto, int createdByUserID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_IDPhoto_Insert"), clientID, iDPhoto, createdByUserID);
        }

        public override IDataReader FBClients_GetAll(int portalID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_GetAll"), portalID);
        }


        public override int FBClients_Merge(int MasterClientID, int ChildClientID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBClients_Merge"), MasterClientID, ChildClientID));
        }




        public override void FBClients_DeleteClient(int clientID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_DeleteClient"), clientID);
        }


        // CLIENTS
        public override int FBClients_Insert(string clientFirstName, string clientMiddleInitial, string clientLastName, DateTime clientDOB, string clientAddress, string clientCity, string clientTown, string clientState, string clientZipCode, string clientEmailAddress, string clientIdCard, string clientPhone, string clientPhoneType, int clientCaseWorker, int moduleID, int createdByUserID, int portalID, string clientSuffix, bool clientDOBVerify, string clientEthnicity, string clientNote, string clientUnit, string clientGender, string clientType, DateTime clientVerifyDate, bool clientAddressVerify, DateTime clientAddressVerifyDate, bool subjectToReview, bool oneBagOnly, string disability, bool isActive, bool isLocked)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBClients_Insert"), clientFirstName, clientMiddleInitial, clientLastName, clientDOB, clientAddress, clientCity, clientTown, clientState, clientZipCode, clientEmailAddress, clientIdCard, clientPhone, clientPhoneType, clientCaseWorker, moduleID, createdByUserID, portalID, clientSuffix, clientDOBVerify, clientEthnicity, clientNote, clientUnit, clientGender, clientType, clientVerifyDate, clientAddressVerify, clientAddressVerifyDate, subjectToReview, oneBagOnly, disability, isActive, isLocked));
        }

        public override IDataReader FBClients_Search(int portalId, string clientLastName, string clientIdCard, string clientFirstName, string clientID, string clientAddress, string clientCity, string clientType, string isActive)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_Search"), portalId, clientLastName, clientIdCard, clientFirstName, clientID, clientAddress, clientCity, clientType, isActive);
        }

        public override IDataReader FBClients_GetByID(int portalID, int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_GetByID"), portalID, clientID);
        }

        public override void FBClients_Update(int clientID, string clientFirstName, string clientMiddleInitial, string clientLastName, DateTime clientDOB, string clientAddress, string clientCity, string clientTown, string clientState, string clientZipCode, string clientEmailAddress, string clientIdCard, string clientPhone, string clientPhoneType, int clientCaseWorker, int moduleID, int lastModifiedByUserID, int portalID, double latitude, double longitude, string clientSuffix, bool clientDOBVerify, string clientEthnicity, string clientNote, string clientUnit, string clientGender, string clientType, DateTime clientVerifyDate, bool clientAddressVerify, DateTime clientAddressVerifyDate, bool subjectToReview, bool oneBagOnly, string disability, bool isActive, bool isLocked)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_Update"), clientID, clientFirstName, clientMiddleInitial, clientLastName, clientDOB, clientAddress, clientCity, clientTown, clientState, clientZipCode, clientEmailAddress, clientIdCard, clientPhone, clientPhoneType, clientCaseWorker, moduleID, lastModifiedByUserID, portalID, latitude, longitude, clientSuffix, clientDOBVerify, clientEthnicity, clientNote, clientUnit, clientGender, clientType, clientVerifyDate, clientAddressVerify, clientAddressVerifyDate, subjectToReview, oneBagOnly, disability, isActive, isLocked);
        }

        public override IDataReader FBClients_AgeGroupReport(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_AgeGroupReport"), clientID);
        }

        // CASEWORKERS
        public override IDataReader FBClientsGetCaseWorkers(int portalID, string roleName)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClientsGetCaseWorkers"), portalID, roleName);
        }

        public override IDataReader FBClients_Visit_GetClientLastVisitDate(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_Visit_GetClientLastVisitDate"), clientID);
        }


        // CLIENT AFM
        public override IDataReader FBClients_Search_AFM(int portalId, string clAddFamMemLastName, string clAddFamMemFirstName, string clientType)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_Search_AFM"), portalId, clAddFamMemLastName, clAddFamMemFirstName, clientType);
        }

        public override int FBClients_AFM_Insert(string clAddFamMemFirstName, string clAddFamMemLastName, DateTime clAddFamMemDOB, string aFMRelationship, int clientID, int createdByUserID, string aFMMiddleInitial, string aFMSuffix, bool aFMDOBVerify, string aFMEthnicity, string aFMGender)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBClients_AFM_Insert"), clAddFamMemFirstName, clAddFamMemLastName, clAddFamMemDOB, aFMRelationship, clientID, createdByUserID, aFMMiddleInitial, aFMSuffix, aFMDOBVerify, aFMEthnicity, aFMGender));
        }

        public override IDataReader FBClients_AFM_List(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_AFM_List"), clientID);
        }

        public override IDataReader FBClients_AFM_GetByID(int clAddFamMemID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_AFM_GetByID"), clAddFamMemID);
        }

        public override void FBClients_AFM_Update(int clAddFamMemID, string clAddFamMemFirstName, string clAddFamMemLastName, DateTime clAddFamMemDOB, string aFMRelationship, int clientID, int lastModifiedByUserID, string aFMMiddleInitial, string aFMSuffix, bool aFMDOBVerify, string aFMEthnicity, string aFMGender)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_AFM_Update"), clAddFamMemID, clAddFamMemFirstName, clAddFamMemLastName, clAddFamMemDOB, aFMRelationship, clientID, lastModifiedByUserID, aFMMiddleInitial, aFMSuffix, aFMDOBVerify, aFMEthnicity, aFMGender);
        }

        public override void FBClients_AFM_Delete(int clAddFamMemID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_AFM_Delete"), clAddFamMemID);
        }

        // CHRISTMAS TOYS
        public override IDataReader FBxMas_AFM_Get_CurrentYear(int clAddFamMemID, int xMasYear)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBxMas_AFM_Get_CurrentYear"), clAddFamMemID, xMasYear);
        }

        public override int FBxMas_AFM_Insert_CurrentYear(int clientID,
            int clAddFamMemID,
            int xMasYear,
            string xMasSizes,
            bool bikeRaffle,
            string bikeAwardedDate,
            bool wantsToys,
            bool verifiedToys,
            string receivedToysDate,
            int lastModifiedByUserID, string xMasNotes)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBxMas_AFM_Insert_CurrentYear"),
                clientID,
                clAddFamMemID,
                xMasYear,
                xMasSizes,
                bikeRaffle,
                bikeAwardedDate,
                wantsToys,
                verifiedToys,
                receivedToysDate,
                lastModifiedByUserID, xMasNotes));
        }

        public override void FBxMas_AFM_Update_CurrentYear(int xMasID, string xMasSizes, bool bikeRaffle, string bikeAwardedDate, bool wantsToys, bool verifiedToys, string receivedToysDate, int lastModifiedByUserID, string xMasNotes)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBxMas_AFM_Update_CurrentYear"), xMasID, xMasSizes, bikeRaffle, bikeAwardedDate, wantsToys, verifiedToys, receivedToysDate, lastModifiedByUserID, xMasNotes);
        }

        public override IDataReader FBxMas_AFM_PrintTicket(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBxMas_AFM_PrintTicket"), clientID);
        }

        public override IDataReader FBxMas_AFM_Get_History_By_AFM_ID(int clAddFamMemID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBxMas_AFM_Get_History_By_AFM_ID"), clAddFamMemID);
        }

        // CLIENT VISITS

        public override IDataReader FBClients_Visit_List(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_Visit_List"), clientID);
        }

        public override void FBClients_Visit_Insert(DateTime visitDate, string visitNotes, int visitNumBags, int clientID, int createdByUserID, string serviceLocation)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_Visit_Insert"), visitDate, visitNotes, visitNumBags, clientID, createdByUserID, serviceLocation);
        }

        public override void FBClients_Visit_Update(int visitID, DateTime visitDate, string visitNotes, int visitNumBags, int clientID, int lastModifiedByUserID, string serviceLocation)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_Visit_Update"), visitID, visitDate, visitNotes, visitNumBags, clientID, lastModifiedByUserID, serviceLocation);
        }

        public override IDataReader FBClients_Visit_GetByID(int visitID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_Visit_GetByID"), visitID);
        }

        // CLIENT TRUE FALSE QUESTIONS
        public override void FBClients_TrueFalseQuestions_InsertUpdate(string tfQuestion, bool tfAnswer, int clientID, int createdByUserID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_TrueFalseQuestions_InsertUpdate"), tfQuestion, tfAnswer, clientID, createdByUserID);
        }

        public override IDataReader FBClients_TrueFalseQuestions_List(int clientID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_TrueFalseQuestions_List"), clientID);
        }

        public override void FBClients_TrueFalseQuestions_Delete(string TfQuestion, int clientID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_TrueFalseQuestions_Delete"), TfQuestion, clientID);
        }


        // CLIENT INCOME & EXPENSE
        public override int FBClients_IncomeExpense_Insert(string ieType, string ieDescription, double ieAmount, int clientID, int createdByUserID)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBClients_IncomeExpense_Insert"), ieType, ieDescription, ieAmount, clientID, createdByUserID));
        }

        public override IDataReader FBClients_IncomeExpense_List(int clientID, string ieType)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_IncomeExpense_List"), clientID, ieType);
        }

        public override void FBClients_IncomeExpense_Delete(int ieID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_IncomeExpense_Delete"), ieID);
        }

        public override IDataReader FBClients_IncomeExpense_GetByID(int ieID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBClients_IncomeExpense_GetByID"), ieID);
        }

        public override void FBClients_IncomeExpense_Update(int ieID, string ieType, string ieDescription, double ieAmount, int clientID, int lastModifiedByUserID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBClients_IncomeExpense_Update"), ieID, ieType, ieDescription, ieAmount, clientID, lastModifiedByUserID);
        }

        #endregion
    }
}
