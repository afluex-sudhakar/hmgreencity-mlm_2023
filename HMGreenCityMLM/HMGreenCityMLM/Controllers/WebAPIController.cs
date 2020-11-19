﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMGreenCityMLM.Models;
using System.Web.Mvc;
using System.Data;
using HMGreenCityMLM.Filter;
using BusinessLayer;
using System.IO;

namespace HMGreenCityMLM.Controllers
{
    public class WebAPIController : Controller
    {
        #region Login
        public ActionResult Login(LoginAPI model)
        {


            LoginAPI obj = new LoginAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Password == "" || model.Password == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Password";
            }

            try
            {
                //string Password = model.Password;
                //model.Password = Crypto.Encrypt(Password);
                DataSet dsResult = model.Login();
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {
                            if (model.Password == Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString()))
                            {

                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                                obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                                obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                                obj.Password = dsResult.Tables[0].Rows[0]["Password"].ToString();
                                //obj.TransPassword = dsResult.Tables[0].Rows[0]["TransPassword"].ToString();
                                obj.Profile = dsResult.Tables[0].Rows[0]["Profile"].ToString();
                                obj.Status = dsResult.Tables[0].Rows[0]["Status"].ToString();
                                obj.Status = "0";
                                obj.Message = "Successfully Logged in";

                                return Json(obj, JsonRequestBehavior.AllowGet);

                            }
                            obj.Status = "1";
                            obj.Message = "Incorrect LoginId Or Password";
                            return Json(obj, JsonRequestBehavior.AllowGet);

                        }
                        else if (dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            obj.Status = "0";
                            obj.Message = "Successfully Logged in";
                            obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                            obj.Pk_adminId = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            obj.UserType = dsResult.Tables[0].Rows[0]["UsertypeName"].ToString();
                            obj.FullName = dsResult.Tables[0].Rows[0]["Name"].ToString();

                            if (dsResult.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                            {
                                obj.FranchiseAdminID = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            }

                        }
                        else
                        {
                            obj.Status = "1";
                            obj.Message = "Incorrect LoginId Or Password";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        obj.Status = "1";
                        obj.Message = "Invalid LoginId or Password.";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = "Invalid LoginId or Password.";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region ChangePAssword

        public ActionResult ChangePassword(ChangePasswordAPI model)
        {
            ChangePasswordAPI obj = new ChangePasswordAPI();
            if (model.PasswordType == "0" || model.PasswordType == null)
            {
                obj.Status = "1";
                obj.Message = "Please Select Password Type";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.NewPassword == "" || model.NewPassword == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter New Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.OldPassword == "" || model.OldPassword == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Old Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.UpdatedBy == "" || model.UpdatedBy == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Pk_userId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //  model.UpdatedBy = Session["Pk_userId"].ToString();
                string OldPassword = model.OldPassword;
                model.OldPassword = Crypto.Encrypt(OldPassword);
                string NewPassword = model.NewPassword;
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Password updated successfully..";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Registration

        public ActionResult Registration(RegistrationAPI model)
        {
            RegistrationAPI obj = new RegistrationAPI();
            if (model.SponsorId == "" || model.SponsorId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Sponsor Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.FirstName == "" || model.FirstName == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.MobileNo == "" || model.MobileNo == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Mobile No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Leg == "" || model.Leg == null)
            {
                obj.Status = "1";
                obj.Message = "Please Select Leg";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            model.SponsorId = model.SponsorId;
            try
            {
                string password = Common.GenerateRandom();
                model.Password = Crypto.Encrypt(password);
                model.RegistrationBy = "Mobile";
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.FullName = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.TransPassword = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        obj.Leg = model.Leg;
                        obj.RegistrationBy = model.RegistrationBy;
                        obj.SponsorId = model.SponsorId;
                        obj.Email = model.Email;
                        obj.LastName = model.LastName;
                        obj.Address = model.Address;
                        obj.PinCode = model.PinCode;
                        obj.PanCard = model.PanCard;
                        obj.Gender = model.Gender;

                        try
                        {
                            string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(model.MobileNo, str2);
                        }
                        catch { }
                        obj.Status = "0";
                        obj.Message = "Registered Successfully";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion

        #region TopupList

        public ActionResult TopupList(TopUpAPI model)
        {
            TopUpAPI obj = new TopUpAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                obj.Status = "1";
                obj.Message = "Please enter LoginId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DataSet ds = model.GetTopupReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<TopUpAPI> lstTopupReport = new List<TopUpAPI>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        TopUpAPI obj1 = new TopUpAPI();
                        obj1.FK_InvestmentID = Crypto.Encrypt(r["Pk_InvestmentId"].ToString());
                        obj1.Name = r["Name"].ToString() + " (" + r["LoginId"].ToString() + ")";
                        obj1.SiteName = r["SiteName"].ToString();
                        obj1.SectorName = r["SectorName"].ToString();
                        obj1.UpgradtionDate = r["UpgradtionDate"].ToString();
                        obj1.ProductName = r["Package"].ToString();
                        obj1.Amount = r["Amount"].ToString();
                        lstTopupReport.Add(obj1);
                    }
                    obj.lsttopupreport = lstTopupReport;
                    obj.Status = "0";
                    obj.Message = "TopupList";
                }
                else
                {
                    obj.Status = "1";
                    obj.Message = "No Data Found";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PrintTopup

        public ActionResult PrintTopup(PrintTopupAPI model)
        {
            PrintTopupAPI obj = new PrintTopupAPI();
            if (model.invid == "" || model.invid == null)
            {
                obj.Status = "1";
                obj.Message = "Please enter id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            List<PrintTopupAPI> list = new List<PrintTopupAPI>();

            if (model.invid != null)
            {
                model.LoginId = Crypto.Decrypt(model.invid);
                try
                {
                    DataSet ds = model.PrintTopUp();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            PrintTopupAPI obj1 = new PrintTopupAPI();

                            obj1.FK_InvestmentID = r["Pk_InvestmentId"].ToString();
                            //obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            //obj.ProductID = r["Fk_ProductId"].ToString();
                            obj1.Quantity = r["Quantity"].ToString();
                            obj1.MRP = r["Amount"].ToString();
                            obj1.IGST = r["IGST"].ToString();
                            obj1.CGST = r["CGST"].ToString();
                            obj1.SGST = r["SGST"].ToString();
                            obj1.FinalAmount = r["Amount"].ToString();
                            //obj1bj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj1.ProductName = r["ProductName"].ToString();
                            obj1.HSNCode = r["HSNCode"].ToString();

                            obj1.OrderNo = r["Pk_InvestmentId"].ToString();

                            obj1.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            obj1.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            obj1.PurchaseDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
                            obj1.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            obj1.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            obj1.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                            obj1.ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString();
                            obj1.TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString();

                            obj1.CompanyName = SoftwareDetails.CompanyName;
                            obj1.CompanyAddress = SoftwareDetails.CompanyAddress;
                            obj1.PinCode = SoftwareDetails.Pin1;
                            obj1.State1 = SoftwareDetails.State1;
                            obj1.City1 = SoftwareDetails.City1;
                            obj1.ContactNo = SoftwareDetails.ContactNo;
                            obj1.LandLine = SoftwareDetails.LandLine;
                            obj1.Website = SoftwareDetails.Website;
                            obj1.Email = SoftwareDetails.EmailID;
                            list.Add(obj1);

                        }
                        obj.lsttopupreport = list;
                        obj.Status = "0";
                        obj.Message = "Data Fetch Successfully";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = "No Data Found";
                        return Json(obj, JsonRequestBehavior.AllowGet);

                    }

                }
                catch (Exception ex)
                {
                    obj.Status = "1";
                    obj.Message = "No Data Found";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region PayoutReport


        public ActionResult PayoutReport(PayoutReportAPI payoutDetail)
        {
            List<PayoutReportAPI> lst1 = new List<PayoutReportAPI>();

            payoutDetail.LoginId = payoutDetail.LoginId;
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    PayoutReportAPI Obj = new PayoutReportAPI();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
                payoutDetail.Status = "0";
                payoutDetail.Message = "Data Fetched";
                return Json(payoutDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                payoutDetail.Status = "1";
                payoutDetail.Message = "No Data Found";
                return Json(payoutDetail, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region PayoutReportSearch

        public ActionResult PayoutReportBy(PayoutReportSearch payoutDetail)
        {
            List<PayoutReportSearch> lst1 = new List<PayoutReportSearch>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            payoutDetail.LoginId = payoutDetail.LoginId;
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    PayoutReportSearch Obj = new PayoutReportSearch();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
                payoutDetail.Status = "0";
                payoutDetail.Message = "Data Fetched";
                return Json(payoutDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                payoutDetail.Status = "1";
                payoutDetail.Message = "No Data Found";
                return Json(payoutDetail, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region AssociateDashboard

        public ActionResult AssociateDashBoardTotals(AssociateDashBoardAPI assocdash)
        {
            AssociateDashBoardAPI obj = new AssociateDashBoardAPI();

            try
            {

                DataSet ds = assocdash.GetAssociateDashboard();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() != "0")
                {

                    obj.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                    obj.TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                    //ViewBag.ProductWalletBalance = ds.Tables[0].Rows[0]["ProductWalletBalance"].ToString();
                    obj.PayoutWalletBalance = ds.Tables[0].Rows[0]["PayoutWalletBalance"].ToString();
                    obj.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                    obj.TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    obj.TotalAdvance = ds.Tables[0].Rows[0]["TotalAdvance"].ToString();
                    obj.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                    obj.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();

                    //ViewBag.ProductPaidBusinessLeft = ds.Tables[3].Rows[0]["PaidBusinessLeft"].ToString();
                    //ViewBag.ProductPaidBusinessRight = ds.Tables[3].Rows[0]["PaidBusinessRight"].ToString();
                    //ViewBag.ProductTotalBusinessLeft = ds.Tables[3].Rows[0]["TotalBusinessLeft"].ToString();
                    //ViewBag.ProductTotalBusinessRight = ds.Tables[3].Rows[0]["TotalBusinessRight"].ToString();
                    //ViewBag.ProductCarryLeft = ds.Tables[3].Rows[0]["CarryLeft"].ToString();
                    //ViewBag.ProductCarryRight = ds.Tables[3].Rows[0]["CarryRight"].ToString();

                    obj.Status = "0";
                    obj.Message = "Data Fetched";
                    return Json(obj, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    obj.Status = "1";
                    obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region AssociateDashboardInvestment


        public ActionResult AssociateDashboardInvestment(AssoeDashInvstAPI data)
        {
            AssoeDashInvstAPI obj = new AssoeDashInvstAPI();
            try
            {
                List<AssoeDashInvstAPI> lstinvestment = new List<AssoeDashInvstAPI>();
                DataSet ds = data.GetAssociateDashboard();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            AssoeDashInvstAPI Obj = new AssoeDashInvstAPI();
                            Obj.ProductName = r["ProductName"].ToString();
                            Obj.Amount = r["Amount"].ToString();
                            Obj.Status = r["Status"].ToString();

                            lstinvestment.Add(Obj);
                        }
                        obj.lstinvestment = lstinvestment;
                        obj.Status = "0";
                        obj.Message = "Data Fetched";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region AssoDashTotalBusiness


        public ActionResult AssoDashTotalBusiness(TotalBusinessAPI data)
        {
            TotalBusinessAPI obj = new TotalBusinessAPI();
            try
            {

                List<AssoeDashInvstAPI> lstinvestment = new List<AssoeDashInvstAPI>();
                DataSet ds = data.GetAssociateDashboard();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                        obj.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                        obj.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                        obj.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                        obj.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                        obj.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
                        obj.Status = "0";
                        obj.Message = "Data Fetched";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }




                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region ViewProfile

        public ActionResult ViewProfile(ViewProfileAPI data)
        {

            ViewProfileAPI obj = new ViewProfileAPI();
            try
            {

                List<ViewProfileAPI> lstprofile = new List<ViewProfileAPI>();

                DataSet ds = data.GetUserProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                    obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                    obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                    obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                    obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();

                    obj.Status = "0";
                    obj.Message = "Data Fetched";

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }else
                {

                    obj.Status = "0";
                    obj.Message = "No Data Fetch for this id";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region UpdateProfile

        public ActionResult UpdateProfile(HttpPostedFileBase fileProfilePicture, UpdateProfileAPI obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (fileProfilePicture != null)
                {
                    obj.ProfilePicture = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePicture)));
                }

                //Profile objProfile = new Profile();
                DataSet ds = obj.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Message = "Profile updated successfully..";
                        obj.Status = "0";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        obj.Status = "1";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
               obj.Status = "1";

                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PayoutLedger

        public ActionResult PayoutLedger(PayoutLedgerAPI PayoutLedger)
        {
            PayoutLedgerAPI objewallet = new PayoutLedgerAPI();

            //   objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            PayoutLedger.FromDate = string.IsNullOrEmpty(PayoutLedger.FromDate) ? null : Common.ConvertToSystemDate(PayoutLedger.FromDate, "dd/MM/yyyy");
            PayoutLedger.ToDate = string.IsNullOrEmpty(PayoutLedger.ToDate) ? null : Common.ConvertToSystemDate(PayoutLedger.ToDate, "dd/MM/yyyy");
            List<PayoutLedgerAPI> lst = new List<PayoutLedgerAPI>();
            DataSet ds = PayoutLedger.PayoutLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PayoutLedgerAPI Objload = new PayoutLedgerAPI();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["TransactionDate"].ToString();
                    Objload.PayoutBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return Json(objewallet,JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult GetSponsorName(SponsorNameAPI sponsorname)
        {
            SponsorNameAPI obj = new SponsorNameAPI();
            DataSet ds = sponsorname.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Status = "0";
                obj.Message = "Sponsor Name Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else {
                obj.Status = "1";
                obj.Message = "Invalid SponsorId"; return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

    }
}