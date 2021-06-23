using Dapper;
using DeliveryRoomWatcher.Config;
using DeliveryRoomWatcher.Hooks;
using DeliveryRoomWatcher.Models;
using DeliveryRoomWatcher.Models.Common;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryRoomWatcher.Repositories
{
    public class InventoryRepo
    {
        CompanyRepository _company = new CompanyRepository();
        public ResponseModel getlistofrequest(InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"select * from prbydept where deptcode=@id",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        } 
        public ResponseModel getlistofrequestsearched(InventoryModel.listofrequestseareched listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string request = "";
                       if (listofrequest.status=="For Approval" && listofrequest.date!="")
                        {
                            request = $@"SELECT * FROM prbydept WHERE deptcode=@dept  AND STATUS=@status AND reqdate=DATE_FORMAT(@date,'%Y-%m-%d')";

                        }
                        else if (listofrequest.status == "Approved" && listofrequest.date != "")
                        {
                            request = $@"SELECT * FROM prbydept WHERE deptcode=@dept AND STATUS=@status  AND apprdate=DATE_FORMAT(@date,'%Y-%m-%d')";
                        }
                        else if (listofrequest.status == "Cancelled" && listofrequest.date != "")
                        {
                            request = $@"SELECT * FROM prbydept WHERE deptcode=@dept  AND STATUS=@status AND cancelleddate=DATE_FORMAT(@date,'%Y-%m-%d')" ;
                        }
                        else if(listofrequest.date == "" && listofrequest.status !="")
                        {
                            request = $@"SELECT * FROM prbydept WHERE deptcode=@dept  AND STATUS=@status";
                        }
                        else
                        {
                            request = $@"SELECT * FROM prbydept WHERE deptcode=@dept  AND dateencoded=DATE_FORMAT(@date,'%Y-%m-%d')";
                        }

                        var data = con.Query(request,listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getlistofrequestbystatus(InventoryModel.listofrequestbydept listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"select * from prbydept where deptcode=@id and STATUS=@status",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getlistofreqyestApproved(InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT rd.prno,rd.reqby,rs.`dateencoded`,rs.`apprbyname`,rs.`apprdate`,rd.`STATUS` FROM prbydept rd JOIN prheader rs ON rd.`prno`=rs.`prno` JOIN department dp ON rs.`deptcode`=dp.`deptcode`  WHERE rs.`deptcode`='@id'  AND rd.`STATUS`='Approved'",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        } 
        public ResponseModel getlistofreqyestCancelled(InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT rd.prno,rd.reqby,rs.`dateencoded`,rs.`apprbyname`,rs.`apprdate`,rd.`STATUS` FROM prbydept rd JOIN prheader rs ON rd.`prno`=rs.`prno` JOIN department dp ON rs.`deptcode`=dp.`deptcode`  WHERE rs.`deptcode`='@id' AND rd.`STATUS`='Cancelled'",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }  
        public ResponseModel getlistofreqyestForApproval(InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT rd.prno,rd.reqby,rs.`dateencoded`,rs.`apprbyname`,rs.`apprdate`,rd.`STATUS` FROM prbydept rd JOIN prheader rs ON rd.`prno`=rs.`prno` JOIN department dp ON rs.`deptcode`=dp.`deptcode`  WHERE rs.`deptcode`='@id' AND rd.`STATUS`='For Approval'",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }  
        public ResponseModel getlistofreqyestIssued(InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT rd.prno,rd.reqby,rs.`dateencoded`,rs.`apprbyname`,rs.`apprdate`,rd.`STATUS` FROM prbydept rd JOIN prheader rs ON rd.`prno`=rs.`prno` JOIN department dp ON rs.`deptcode`=dp.`deptcode`  WHERE rs.`deptcode`='@id' AND rd.`STATUS`='Issued'",
                          listofrequest, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }  
        public ResponseModel getnoninventoryitem()
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT inv.stockcode,inv.stockdesc,inv.packdesc,inv.unitdesc FROM invmaster inv WHERE inv.invitem='N'",
                           transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getinventoryitem(mdlInventory.invmaster inv)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT inv.stockcode,inv.stockdesc,inv.packdesc,inv.unitdesc,inv.averagecost FROM invmaster inv where active=@active and stockdesc LIKE concat('%',@stockdesc,'%')",
                           inv,transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        } 
        public ResponseModel getdepartmentList()
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT dp.deptcode,dp.`deptname`FROM department dp WHERE deptstock='Y'",
                           transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }  
        public ResponseModel getsectionlist(string deptcode)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT * FROM deptsection WHERE deptcode=@deptcode",
                           new { deptcode },transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getprsuppliers(string prno)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT scode as value,sname as label FROM prsuppliers where prno=@prno",new { prno }, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        } 
        public ResponseModel getitemforsuppliers(string prno)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT stockcode,stockdesc FROM prdetails WHERE prno=@prno",new { prno }, transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getPRPdf(string prno)
        {
            try
            {
                using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
                {
                    con.Open();
                    using (var tran = con.BeginTransaction())
                    {
                        string brand_logo = _company.hospitalLogo().data.ToString();
                        string brand_name = con.QuerySingleOrDefault<string>("SELECT datval FROM defvalues WHERE remarks = 'hospname' limit 1;", null, transaction: tran);
                        string brand_phone = con.QuerySingleOrDefault<string>("SELECT datval FROM defvalues WHERE remarks = 'SMSNUMBER' limit 1;", null, transaction: tran);
                        string brand_address = con.QuerySingleOrDefault<string>("SELECT datval FROM defvalues WHERE remarks = 'hospadd' limit 1;", null, transaction: tran);
                        string brand_email = "-";



                        ListOfItems pr_request = con.QuerySingle<ListOfItems>($@"
                                       SELECT pr.*,dept.deptname FROM `prheader` pr JOIN department dept ON pr.deptcode=dept.deptcode WHERE prno=@prno LIMIT 1; 
                            ", new { prno }, transaction: tran);


                        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        //QRCodeData qrCodeData = qrGenerator.CreateQrCode(hash_req_pk, QRCodeGenerator.ECCLevel.Q);
                        //QRCode qrCode = new QRCode(qrCodeData);


                        //var brand_logo_bitmap = UseFileParser.Base64StringToBitmap(brand_logo);
                        //Bitmap qrCodeImage = qrCode.GetGraphic(35, Color.Black, Color.White, brand_logo_bitmap, 25);
                        //string qr_with_brand_logo = UseFileParser.BitmapToBase64(qrCodeImage);

                        
                        pr_request.pritems = con.Query<ListofItemDetails>("select * from prdetails where prno=@prno;",
                             new { prno }, transaction: tran).ToList();


                        var pr_pdf = HtmltoPdf.PRHtmlPdf.geratePRPdf(brand_name, brand_logo, brand_address, brand_phone, brand_email, pr_request);



                        return new ResponseModel
                        {
                            success = true,
                            data = Convert.ToBase64String(pr_pdf)
                        };

                    }
                }

            }
            catch (Exception err)
            {

                return new ResponseModel
                {
                    success = false,
                    message = err.Message
                };
            }
        }
        public ResponseModel InsertNewRequest(mdlRequestHeader requests)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string prno = con.QuerySingle<string>($@"SELECT NextPRNo() as prno", null, transaction: tran);
                        requests.prno = prno;
                        string sql_insert_request_header = $@"INSERT INTO prheader SET prno = @prno,deptcode = @deptcode,sectioncode = '',reqdate = NOW(),reqby = @reqby,reqbyposition = '',apprbycode = NULL,
                        apprbyname = NULL,apprdate = NULL,reqtype = 'O',reqstatus = 'O',trantype = 'T',
                        headapprovebycode = NULL,headapprovebyname = NULL,headdateapprove = NULL,cancelledbycode = NULL,cancelledbyname = NULL,datecancelled = NULL,reqremarks = @reqremarks,encodedby = @reqby,
                        dateencoded = NOW(),tsreference = NOW()";
                        int insert_user_information = con.Execute(sql_insert_request_header, requests, transaction: tran);
                        
                    
                        if (insert_user_information > 0)
                            {
                            int i = requests.lisrequesttdtls.Count;

                            foreach (var rdl in requests.lisrequesttdtls)
                                 {
                                  rdl.prno = prno;
                                  string sql_add_dtls = $@"INSERT INTO prdetails SET prno = @prno,lineno = @lineno,linestatus = 'O',deptcode = @deptcode,sectioncode = '',stockcode = @stockcode,
                                    stockdesc = @stockdesc,prqty = @prqty,prprice = @averagecost,unitdesc = @unitdesc,itemremarks = @itemremarks,docdate = NOW()";

                                        int insert_prdetails_result = con.Execute(sql_add_dtls, rdl, transaction: tran);
                                        if (insert_prdetails_result <= 0)
                                        {
                                    tran.Rollback();
                                    return new ResponseModel
                                            {
                                                success = false,
                                                message = "Database error has occured. No affected rows"
                                            };
                                        }
                                 }
                            if (i > 0)
                            {
                                int supp = requests.lisrequesttsuppliers.Count;

                                foreach (var rdl in requests.lisrequesttsuppliers)
                                {
                                    rdl.prno = prno;
                                    string sql_add_suppliers = $@"INSERT INTO prsuppliers SET prno = @prno,scode = @value,sname = @label,dateencoded = NOW()";

                                    int insert_prdetails_suppliers_result = con.Execute(sql_add_suppliers, rdl, transaction: tran);
                                    if (insert_prdetails_suppliers_result <= 0)
                                    {
                                        tran.Rollback();
                                        return new ResponseModel
                                        {

                                            success = false,
                                            message = "Database error has occured. No affected rows"
                                        };
                                    }
                                }
                                if (supp > 0)
                                {
                                    tran.Commit();
                                    return new ResponseModel
                                    {
                                        success = true,
                                        message = $@"Your Purchase Request has been Added sucessfully,Purchase Request No.{requests.prno}"
                                    };
                                }
                                else
                                {
                                    tran.Rollback();
                                    return new ResponseModel
                                    {

                                        success = false,
                                        message = "Database error has occured. No affected rows"
                                    };

                                }
                            }
                            else
                            {
                                tran.Rollback();
                                return new ResponseModel
                                {

                                    success = false,
                                    message = "Database error has occured. No affected rows"
                                };
                            }
                        }
                        
                        else
                        {
                            tran.Rollback();
                            return new ResponseModel
                            {
                                    
                            success = false,
                                message = "Database error has occured. No affected rows"
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }
        } 
        public ResponseModel InsertSupplier(mdlInsertSupplier requestssupplier)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        
                                int supp = requestssupplier.lisrequesttsuppliers.Count;
                                var prno = requestssupplier.prno;
                                foreach (var rdl in requestssupplier.lisrequesttsuppliers)
                                {
                            rdl.prno = prno;
                                    string sql_add_suppliers = $@"INSERT INTO prsuppliers SET prno =@prno,scode = @value,sname = @label,dateencoded = NOW()";

                                    int insert_prdetails_suppliers_result = con.Execute(sql_add_suppliers, rdl, transaction: tran);
                                    if (insert_prdetails_suppliers_result <= 0)
                                    {
                                        tran.Rollback();
                                        return new ResponseModel
                                        {
                                            success = false,
                                            message = "Database error has occured. No affected rows"
                                        };
                                    }
                                }
                                if (supp > 0)
                                {
                                    tran.Commit();
                                    return new ResponseModel
                                    {
                                        success = true,
                                        message = $@"Supplier added successfully"
                                    };
                                }
                                else
                                {
                                    tran.Rollback();
                                    return new ResponseModel
                                    {

                                        success = false,
                                        message = "Database error has occured. No affected rows"
                                    };

                                }
                           
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }
        }
        public ResponseModel InsertNewRequestWithoutSupplier(mdlRequestHeader requests)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string prno = con.QuerySingle<string>($@"SELECT NextPRNo() as prno", null, transaction: tran);
                        requests.prno = prno;
                        string sql_insert_request_header = $@"INSERT INTO prheader SET prno = @prno,deptcode = @deptcode,sectioncode = '',reqdate = NOW(),reqby = @reqby,reqbyposition = '',apprbycode = NULL,
                        apprbyname = NULL,apprdate = NULL,reqtype = 'O',reqstatus = 'O',trantype = 'T',
                        headapprovebycode = NULL,headapprovebyname = NULL,headdateapprove = NULL,cancelledbycode = NULL,cancelledbyname = NULL,datecancelled = NULL,reqremarks = @reqremarks,encodedby = @reqby,
                        dateencoded = NOW(),tsreference = NOW()";
                        int insert_user_information = con.Execute(sql_insert_request_header, requests, transaction: tran);


                        if (insert_user_information > 0)
                        {
                            int i = requests.lisrequesttdtls.Count;

                            foreach (var rdl in requests.lisrequesttdtls)
                            {
                                rdl.prno = prno;
                                string sql_add_dtls = $@"INSERT INTO prdetails SET prno = @prno,lineno = @lineno,linestatus = 'O',deptcode = @deptcode,sectioncode = '',stockcode = @stockcode,
                                    stockdesc = @stockdesc,prqty = @prqty,prprice = @averagecost,unitdesc = @unitdesc,itemremarks = @itemremarks,docdate = NOW()";

                                int insert_prdetails_result = con.Execute(sql_add_dtls, rdl, transaction: tran);
                                if (insert_prdetails_result <= 0)
                                {
                                    tran.Rollback();
                                    return new ResponseModel
                                    {
                                        success = false,
                                        message = "Database error has occured. No affected rows"
                                    };
                                }
                            }
                            if (i > 0)
                            {
                                
                                    tran.Commit();
                                    return new ResponseModel
                                    {
                                        success = true,
                                        message = $@"Your Purchase Request has been Added sucessfully,Purchase Request No.{requests.prno}"
                                    };
                             
                            }
                            else
                            {
                                tran.Rollback();
                                return new ResponseModel
                                {

                                    success = false,
                                    message = "Database error has occured. No affected rows"
                                };
                            }
                        }

                        else
                        {
                            tran.Rollback();
                            return new ResponseModel
                            {

                                success = false,
                                message = "Database error has occured. No affected rows"
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }
        }
        public ResponseModel updaterequestApproved(mdlSingleRequest.SingleRequestApprove requests)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string sql_insert_request_header = $@"UPDATE prheader SET apprbycode=@apprbycode,apprbyname=@apprbyname,apprdate=NOW() WHERE prno=@reqno";
                        int insert_user_information = con.Execute(sql_insert_request_header, requests, transaction: tran);
                        if (insert_user_information > 0)
                        {
                            tran.Commit();
                            return new ResponseModel
                            {
                                success = true,
                                message = $@"Request No.{requests.reqno} has been Approved sucessfully"
                            };
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                success = false,
                                message = "Something Went Wrong"
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }
        }

        public ResponseModel updaterequestCancelled(mdlSingleRequest.SingleRequestCancelled requests)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string sql_insert_request_header = $@"UPDATE prheader SET cancelledbycode=@cancelledbycode,cancelledbyname=@cancelledbyname,datecancelled=NOW() WHERE prno=@reqno";
                        int insert_user_information = con.Execute(sql_insert_request_header, requests, transaction: tran);
                        if (insert_user_information > 0)
                        {
                            tran.Commit();
                            return new ResponseModel
                            {
                                success = true,
                                message = $@"Request No.{requests.reqno} has been Cancelled"
                            };
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                success = false,
                                message = "Something Went Wrong"
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }
        }
            
            
       public ResponseModel getsupplier()
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT scode,sname FROM supplier ORDER BY sname",
                           transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getsinglerequestheader (mdlSingleRequest singleRequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT prno,deptcode,reqby,CASE  WHEN apprbycode IS NOT NULL AND cancelledbycode IS NULL  THEN 'Approved' WHEN cancelledbycode IS NOT NULL THEN 'Cancelled' ELSE 'For Approval' END reqstatus,reqremarks FROM prheader WHERE prno=@reqno",
                           singleRequest,transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        } public ResponseModel dashboardnumbers (InventoryModel.listofrequest listofrequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT  COUNT(CASE  WHEN apprbycode IS NOT NULL AND cancelledbycode IS NULL  THEN 'Approved' END) approved,COUNT(CASE WHEN cancelledbycode IS NOT NULL THEN 'Cancelled' END) cancelled,COUNT(CASE WHEN apprbycode IS NULL THEN 'For Approval' END) forapproval FROM prheader WHERE deptcode=@id",
                            listofrequest,transaction: tran
                            );

                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
        public ResponseModel getsinglerequestdtls(mdlSingleRequest singleRequest)
        {
            using (var con = new MySqlConnection(DatabaseConfig.GetConnection()))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        var data = con.Query($@"SELECT stockcode,stockdesc,unitdesc,prqty,itemremarks,prprice FROM prdetails where prno=@reqno",
                           singleRequest, transaction: tran);


                        return new ResponseModel
                        {
                            success = true,
                            data = data
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseModel
                        {
                            success = false,
                            message = $@"External server error. {e.Message.ToString()}",
                        };
                    }

                }
            }

        }
    }
}
