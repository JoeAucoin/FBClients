using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace GIBS.FBClients.Components
{
    public class FBClientsController : IPortable
    {

        #region public method


        // CLIENTS

        public void FBClients_IDPhoto_Insert(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.IDPhoto != null)
            {
                DataProvider.Instance().FBClients_IDPhoto_Insert(info.ClientID, info.IDPhoto, info.CreatedByUserID);
            }
        }

        public void FBClients_IDPhoto_DeleteByClientID(int clientID)
        {
            DataProvider.Instance().FBClients_IDPhoto_DeleteByClientID(clientID);
        }

        public int FBClients_Insert(FBClientsInfo info)
        {
            if (info.ClientLastName != string.Empty)
            {
                return Convert.ToInt32(DataProvider.Instance().FBClients_Insert(info.ClientFirstName, info.ClientMiddleInitial, info.ClientLastName, info.ClientDOB, info.ClientAddress, info.ClientCity, info.ClientTown, info.ClientState, info.ClientZipCode, info.ClientEmailAddress, info.ClientIdCard, info.ClientPhone, info.ClientPhoneType, info.ClientCaseWorker, info.ModuleId, info.CreatedByUserID, info.PortalId, info.ClientSuffix, info.ClientDOBVerify, info.ClientEthnicity, info.ClientNote, info.ClientUnit, info.ClientGender, info.ClientType, info.ClientVerifyDate, info.ClientAddressVerify, info.ClientAddressVerifyDate, info.SubjectToReview, info.OneBagOnly, info.Disability, info.IsActive, info.IsLocked, info.ServiceLocation, info.ClientLanguage));
            }
            else
            {
                return 0;
            }
        }

        public List<FBClientsInfo> FBClients_Search(int portalId, string clientLastName, string clientIdCard, string clientFirstName, string clientID, string clientAddress, string clientCity, string clientType, string isActive, string clientDOB)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_Search(portalId, clientLastName, clientIdCard, clientFirstName, clientID, clientAddress, clientCity, clientType, isActive, clientDOB));
        }


        public List<FBClientsInfo> FBClients_GetAll(int portalID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_GetAll(portalID));
        }

        public FBClientsInfo FBClients_GetByID(int portalID, int clientID)
        {
            //return (FBClientsInfo)CBO.FillObject(DataProvider.Instance().FBClients_GetByID(portalID, clientID), typeof(FBClientsInfo));
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_GetByID(portalID, clientID));
        }

        public FBClientsInfo FBClients_IDPhoto_GetByClientID(int clientID)
        {
            //return (FBClientsInfo)CBO.FillObject(DataProvider.Instance().FBClients_GetByID(clientID), typeof(FBClientsInfo));
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_IDPhoto_GetByClientID(clientID));
        }

        public List<FBClientsInfo> FBClients_AgeGroupReport(int clientID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_AgeGroupReport(clientID));
        }

        // CASEWORKERS
        public List<FBClientsInfo> FBClientsGetCaseWorkers(int portalID, string roleName)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClientsGetCaseWorkers(portalID, roleName));
        }


        public FBClientsInfo FBClients_Visit_GetClientLastVisitDate(int clientID)
        {
           // return (FBClientsInfo)CBO.FillObject(DataProvider.Instance().FBClients_Visit_GetClientLastVisitDate(clientID), typeof(FBClientsInfo));
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_Visit_GetClientLastVisitDate(clientID));
        }

        // CLIENT AFM
        public List<FBClientsInfo> FBClients_Search_AFM(int portalId, string clAddFamMemLastName, string clAddFamMemFirstName, string isActive,string aFMDOB)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_Search_AFM(portalId, clAddFamMemLastName, clAddFamMemFirstName, isActive, aFMDOB));
        }

        public int FBClients_Merge(int MasterClientID, int ChildClientID)
        {
            //return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_Merge(int MasterClientID, int ChildClientID));
            return Convert.ToInt32(DataProvider.Instance().FBClients_Merge(MasterClientID, ChildClientID));

        }

        public int FBClients_AFM_Insert(FBClientsInfo info)
        {
            if (info.ClAddFamMemLastName != string.Empty)
            {
                return Convert.ToInt32(DataProvider.Instance().FBClients_AFM_Insert(info.ClAddFamMemFirstName, info.ClAddFamMemLastName, info.ClAddFamMemDOB, info.AFMRelationship, info.ClientID, info.CreatedByUserID, info.AFMMiddleInitial, info.AFMSuffix, info.AFMDOBVerify, info.AFMEthnicity, info.AFMGender));
            }
            else
            {
                return 0;
            }
        }

        public List<FBClientsInfo> FBClients_AFM_List(int clientID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_AFM_List(clientID));
        }

        public FBClientsInfo FBClients_AFM_GetByID(int clAddFamMemID)
        {
            
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_AFM_GetByID(clAddFamMemID));
        }


        public void FBClients_AFM_Update(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.ClAddFamMemLastName != null)
            {
                DataProvider.Instance().FBClients_AFM_Update(info.ClAddFamMemID, info.ClAddFamMemFirstName, info.ClAddFamMemLastName, info.ClAddFamMemDOB, info.AFMRelationship, info.ClientID, info.LastModifiedByUserID, info.AFMMiddleInitial, info.AFMSuffix, info.AFMDOBVerify, info.AFMEthnicity, info.AFMGender);
            }
        }

        public void FBClients_Update(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.ClientLastName != string.Empty)
            {
                DataProvider.Instance().FBClients_Update(info.ClientID, info.ClientFirstName, info.ClientMiddleInitial, info.ClientLastName, info.ClientDOB, info.ClientAddress, info.ClientCity, info.ClientTown, info.ClientState, info.ClientZipCode, info.ClientEmailAddress, info.ClientIdCard, info.ClientPhone, info.ClientPhoneType, info.ClientCaseWorker, info.ModuleId, info.LastModifiedByUserID, info.PortalId, info.Latitude, info.Longitude, info.ClientSuffix, info.ClientDOBVerify, info.ClientEthnicity, info.ClientNote, info.ClientUnit, info.ClientGender, info.ClientType, info.ClientVerifyDate, info.ClientAddressVerify, info.ClientAddressVerifyDate, info.SubjectToReview, info.OneBagOnly, info.Disability, info.IsActive, info.IsLocked, info.ServiceLocation, info.ClientLanguage);
            }
        }

        public void FBClients_AFM_Delete(int clAddFamMemID)
        {
            DataProvider.Instance().FBClients_AFM_Delete(clAddFamMemID);
        }

        // CHRISTMAS TOYS

        public void FBxMas_AFM_DeleteRecord(int xMasID)
        {
            DataProvider.Instance().FBxMas_AFM_DeleteRecord(xMasID);
        }

        public FBClientsInfo FBxMas_AFM_Get_CurrentYear(int clAddFamMemID, int xMasYear)
        {
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBxMas_AFM_Get_CurrentYear(clAddFamMemID, xMasYear));
        }

        public void FBxMas_AFM_Insert_CurrentYear(FBClientsInfo info)
        {
            if (info.XmasYear.ToString() != string.Empty)
            {
                DataProvider.Instance().FBxMas_AFM_Insert_CurrentYear(info.ClientID, info.ClAddFamMemID,
                    info.XmasYear, info.XmasSizes,
                    info.BikeRaffle, info.BikeAwardedDate,
                    info.WantsToys, info.VerifiedToys,
                    info.ReceivedToysDate, info.LastModifiedByUserID, info.XmasNotes, info.XmasGift1, info.XmasGift2, info.XmasGift1Size, info.XMasGift2Size, info.XmasGiftRecordID);
            }
        }

        public void FBxMas_AFM_Update_CurrentYear(FBClientsInfo info)
        {

            DataProvider.Instance().FBxMas_AFM_Update_CurrentYear(info.XmasID, info.XmasSizes, info.BikeRaffle, info.BikeAwardedDate, info.WantsToys, info.VerifiedToys, info.ReceivedToysDate, info.LastModifiedByUserID, info.XmasNotes, info.XmasGift1, info.XmasGift2, info.XmasGift1Size, info.XMasGift2Size, info.XmasGiftRecordID);

        }

        public List<FBClientsInfo> FBxMas_AFM_PrintTicket(int clientID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBxMas_AFM_PrintTicket(clientID));
        }

        //
        public List<FBClientsInfo> FBxMas_AFM_Get_History_By_AFM_ID(int clAddFamMemID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBxMas_AFM_Get_History_By_AFM_ID(clAddFamMemID));
        }

        // CLIENT VISITS

        public List<FBClientsInfo> FBClients_Visit_List(int clientID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_Visit_List(clientID));
        }

        public void FBClients_Visit_Insert(FBClientsInfo info)
        {
            if (info.VisitDate.ToString() != string.Empty)
            {
                DataProvider.Instance().FBClients_Visit_Insert(info.VisitDate, info.VisitNotes, info.VisitNumBags, info.ClientID, info.CreatedByUserID, info.ServiceLocation, info.ClientSignature, info.OrderStatusCode);
            }
        }

        public void FBClients_Visit_Update(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.VisitDate != null)
            {
                DataProvider.Instance().FBClients_Visit_Update(info.VisitID, info.VisitDate, info.VisitNotes, info.VisitNumBags, info.ClientID, info.LastModifiedByUserID, info.ServiceLocation, info.ClientSignature);
            }
        }

        public FBClientsInfo FBClients_Visit_GetByID(int visitID)
        {
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_Visit_GetByID(visitID));
        }

        public void FBClients_Visit_Delete(int visitID)
        {
            DataProvider.Instance().FBClients_Visit_Delete(visitID);
        }

        // CLIENT INCOME & EXPENSE
        public int FBClients_IncomeExpense_Insert(FBClientsInfo info)
        {
            if (info.IeDescription != string.Empty)
            {
                return Convert.ToInt32(DataProvider.Instance().FBClients_IncomeExpense_Insert(info.IeType, info.IeDescription, info.IeAmount, info.ClientID, info.CreatedByUserID));
            }
            else
            {
                return 0;
            }
        }

        public List<FBClientsInfo> FBClients_IncomeExpense_List(int clientID, string ieType)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_IncomeExpense_List(clientID, ieType));
        }

        public void FBClients_IncomeExpense_Delete(int ieID)
        {
            DataProvider.Instance().FBClients_IncomeExpense_Delete(ieID);
        }

        public FBClientsInfo FBClients_IncomeExpense_GetByID(int ieID)
        {
            return CBO.FillObject<FBClientsInfo>(DataProvider.Instance().FBClients_IncomeExpense_GetByID(ieID));
        }

        public void FBClients_IncomeExpense_Update(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.IeDescription != string.Empty)
            {
                DataProvider.Instance().FBClients_IncomeExpense_Update(info.IeID, info.IeType, info.IeDescription, info.IeAmount, info.ClientID, info.LastModifiedByUserID);
            }
        }


        // CLIENT TRUE FALSE QUESTIONS
        public void FBClients_TrueFalseQuestions_InsertUpdate(FBClientsInfo info)
        {
            //check we have some content to update
            if (info.TfQuestion != null)
            {
                DataProvider.Instance().FBClients_TrueFalseQuestions_InsertUpdate(info.TfQuestion, info.TfAnswer, info.ClientID, info.CreatedByUserID);
            }
        }

        public List<FBClientsInfo> FBClients_TrueFalseQuestions_List(int clientID)
        {
            return CBO.FillCollection<FBClientsInfo>(DataProvider.Instance().FBClients_TrueFalseQuestions_List(clientID));
        }

        public void FBClients_TrueFalseQuestions_Delete(string TfQuestion, int clientID)
        {
            DataProvider.Instance().FBClients_TrueFalseQuestions_Delete(TfQuestion, clientID);
        }

        public void UpdateVisitOrderStatusCode(FBClientsInfo info)
        {
            if (info.VisitID > 0)
            {
                DataProvider.Instance().UpdateVisitOrderStatusCode(info.VisitID, info.OrderStatusCode);
            }
        }

        /// <summary>
        /// Adds a new FBClientsInfo object into the database
        /// </summary>
        /// <param name="info"></param>


        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFBClients(int clientID)
        {
            DataProvider.Instance().FBClients_DeleteClient(clientID);
        }


        #endregion

       

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public string ExportModule(int moduleID)
        {
            StringBuilder sb = new StringBuilder();

            List<FBClientsInfo> infos = FBClients_GetAll(moduleID);

            if (infos.Count > 0)
            {
                sb.Append("<FBClientss>");
                foreach (FBClientsInfo info in infos)
                {
                    sb.Append("<FBClients>");
                    sb.Append("<content>");
                    sb.Append(XmlUtils.XMLEncode(info.ClientLastName));
                    sb.Append("</content>");
                    sb.Append("</FBClients>");
                }
                sb.Append("</FBClientss>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "FBClientss");

            foreach (XmlNode info in infos.SelectNodes("FBClients"))
            {
                FBClientsInfo FBClientsInfo = new FBClientsInfo();
                FBClientsInfo.ModuleId = ModuleID;
                FBClientsInfo.ClientLastName = info.SelectSingleNode("ClientLastName").InnerText;
                FBClientsInfo.CreatedByUserID = UserID;
                FBClients_Insert(FBClientsInfo);

            }
        }

        #endregion
    }
}
