﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace HMGreenCityMLM.Models
{
    public class API:Common
    {
        public string Website { get; set; }
        public string LandLine { get; set; }
        public string ContactNo { get; set; }
        public string City1 { get; set; }
        public string State1 { get; set; }
        public string Pin1 { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string TaxAdded { get; set; }
        public string ValueBeforeTax { get; set; }
        public string AssociateAddress { get; set; }
        public string PurchaseDate { get; set; }
        public string TotalFinalAmountWords { get; set; }
        public string TotalFinalAmount { get; set; }
        public string OrderNo { get; set; }
        public string invid { get; set; }
        public string HSNCode { get; set; }
        public string FinalAmount { get; set; }
        public string SGST { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string MRP { get; set; }
        public string Quantity { get; set; }
        public List<API> lsttopupreport { get; set; }
        public string Amount { get; set; }
        public string ProductName { get; set; }
        public string UpgradtionDate { get; set; }
        public string SectorName { get; set; }
        public string SiteName { get; set; }
        public string FK_InvestmentID { get; set; }
        public string Package { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string Leg { get; set; }
        public string Gender { get; set; }
        public string RegistrationBy { get; set; }
        public string PanCard { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SponsorId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string PasswordType { get; set; }
        public string FranchiseAdminID { get; set; }
        public string Name { get; set; }
        public string UsertypeName { get; set; }
        public string Pk_adminId { get; set; }
        public string TransPassword { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string Profile { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public string LoginId { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        

        

    }
    public class LoginAPI
    {
        public string LoginId { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Pk_adminId { get; set; }
        public string FranchiseAdminID { get; set; }
        public string Profile { get; set; }
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
    }


    public class ChangePasswordAPI
    {
        public string PasswordType { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string UpdatedBy { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = { new SqlParameter("@PasswordType",PasswordType ) ,
                                      new SqlParameter("@OldPassword", OldPassword) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangePassword", para);
            return ds;
        }
    }


    public class RegistrationAPI
    {

        public string Status { get; set; }
        public string Message { get; set; }
        public string Leg { get; set; }

        public string Password { get; set; }
        public string RegistrationBy { get; set; }
        public string SponsorId { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        
        public string TransPassword { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string PanCard { get; set; }
        public string Gender { get; set; }

        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@PanCard",PanCard),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",Leg),
                                     new SqlParameter("@Password",Password)

                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }

    }

    public class TopUpAPI
    {
        public List<TopUpAPI> lsttopupreport { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Package { get; set; }
        public string UpgradtionDate { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string SiteName { get; set; }
        public string FK_InvestmentID { get; set; }
        public string SectorName { get; set; }


        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Package", Package),
                                      new SqlParameter("@ClaculationStatus", Status),
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTopupreport", para);
            return ds;
        }



    }
    public class PrintTopupAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string invid { get; set; }
        public string LoginId { get; set; }
        public string FK_InvestmentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string LandLine { get; set; }
        public string ContactNo { get; set; }
        public string City1 { get; set; }
        public string State1 { get; set; }
        public string PinCode { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string TaxAdded { get; set; }
        public string ValueBeforeTax { get; set; }
        public string AssociateAddress { get; set; }
        public string PurchaseDate { get; set; }
        public string TotalFinalAmountWords { get; set; }
        public string TotalFinalAmount { get; set; }
        public string Quantity { get; set; }
        public string MRP { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string FinalAmount { get; set; }
        public string HSNCode { get; set; }
        public string ProductName { get; set; }

        public List<PrintTopupAPI> lsttopupreport { get; set; }
        public string OrderNo { get; set; }



        public DataSet PrintTopUp()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_InvestmentId", LoginId), };
            DataSet ds = DBHelper.ExecuteQuery("PrintTopUpReport", para);
            return ds;
        }

    }

    public class PayoutReportAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string DisplayName { get; set; }
        public string PayoutNo { get; set; }
        public string ClosingDate { get; set; }
        public string BinaryIncome { get; set; }
        public string GrossAmount { get; set; }
        public string DirectIncome { get; set; }

        public string ToDate { get; set; }
        public string ProcessingFee { get; set; }

        public string LeadershipBonus { get; set; }
        public string TDSAmount { get; set; }
        public string NetAmount { get; set; }
        public string ProductWallet { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public List<PayoutReportAPI> lstPayoutDetail { get; set; }

        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),

                                         new SqlParameter("@FromDate", FromDate),
                                         new SqlParameter("@ToDate", ToDate),

            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutReportForMember", para);
            return ds;
        }
    }

    public class PayoutReportSearch
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string DisplayName { get; set; }
        public string PayoutNo { get; set; }
        public string ClosingDate { get; set; }
        public string BinaryIncome { get; set; }
        public string GrossAmount { get; set; }
        public string DirectIncome { get; set; }

        public string ToDate { get; set; }
        public string ProcessingFee { get; set; }

        public string LeadershipBonus { get; set; }
        public string TDSAmount { get; set; }
        public string NetAmount { get; set; }
        public string ProductWallet { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public List<PayoutReportSearch> lstPayoutDetail { get; set; }

        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),

                                         new SqlParameter("@FromDate", FromDate),
                                         new SqlParameter("@ToDate", ToDate),

            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutReportForMember", para);
            return ds;
        }
    }

    public class AssociateDashBoardAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalDownline { get; set; }
        public string TotalDirects { get; set; }
        public string PayoutWalletBalance { get; set; }
        public string TotalPayout { get; set; }
        public string TotalDeduction { get; set; }
        public string TotalAdvance { get; set; }
        public string TotalActive { get; set; }
        public string TotalInActive { get; set; }
      
        public string DirectBusiness { get; set; }
        public string Fk_UserId { get; set; }


        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }

    }

    public class AssoeDashInvstAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string Fk_UserId { get; set; }
        public List<AssoeDashInvstAPI> lstinvestment { get; set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
    }


    public class TotalBusinessAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PaidBusinessLeft { get; set; }
        public string PaidBusinessRight { get; set; }
        public string TotalBusinessLeft { get; set; }
        public string TotalBusinessRight { get; set; }
        public string Fk_UserId { get; set; }
        public string CarryLeft { get; set; }
        public string TeamBusiness { get; set; }
        public string CarryRight { get; set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
    }

    public class ViewProfileAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public string ProfilePicture { get; set; }

        public DataSet GetUserProfile()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }
    }

    public class UpdateProfileAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public string ProfilePicture { get; set; }
        public string BankBranch { get; set; }
        public string PK_UserID { get; set; }


        public DataSet UpdateProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      new SqlParameter("@FirstName", FirstName) ,
                                      new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", EmailId) ,
                                      new SqlParameter("@AccountNo", AccountNumber) ,
                                      new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@ProfilePic", ProfilePicture)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);
            return ds;
        }
    }


    public class PayoutLedgerAPI
    {
        public List<PayoutLedgerAPI> lstpayoutledger { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Fk_UserId { get; set; }
        public string FromDate { get; set; }
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string AddedOn { get; set; }
        public string PayoutBalance { get; set; }
        public string ToDate { get; set; }

        public DataSet PayoutLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                       new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutLedger", para);
            return ds;
        }

    }


    public class SponsorNameAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string sponsorId { get; set; }
        public string SponsorName { get; set; }


        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", sponsorId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
    }

}