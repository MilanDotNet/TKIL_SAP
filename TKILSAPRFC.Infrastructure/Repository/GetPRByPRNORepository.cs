using Microsoft.Extensions.Configuration;
using NwRfcNet;
using System.Data.SqlClient;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Core.Helpers.Interface;
using TKILSAPRFC.Infrastructure.Common;
using TKILSAPRFC.Infrastructure.Helper;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.ViewModels;
using static TKILSAPRFC.Model.ViewModels.mdl_PRNO_BKP;



namespace TKILSAPRFC.Infrastructure.Repository
{
    public class GetPRByPRNORepository : IGetPRByPRNORepository
    {
        private readonly ICurrentUser currentUser;
        private readonly IConfiguration _configuration;
        private readonly RfcConnection _Rfcconnection;

        public GetPRByPRNORepository(ICurrentUser currentUser, IConfiguration configuration, RfcConnection rfcconnection)
        {
            this.currentUser = currentUser;
            this._configuration = configuration;
            _Rfcconnection = rfcconnection;
        }

        public async Task<ResponseMsg> GetPRByPRNO(string PRNO)
        {
            ResponseMsg msg = new ResponseMsg();
            try
            {
                _Rfcconnection.Open();
                _Rfcconnection.Ping();
                Console.WriteLine("SAP connection is active.");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Start Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());

                var mapper = _Rfcconnection.Mapper;
                Configure(mapper);

                var func = _Rfcconnection.CallRfcFunction("ZPRCSNS_GET_PRCHS_RQSTN_BY_ID");
                var inputParameters = new PurchaseRequestInput
                {
                    IM_V_PR_ID = PRNO.TrimEnd()
                };
                func.Invoke(inputParameters);

                var output = func.GetOutputParameters<PurchaseRequestOutput>();
                if (output.EX_T_PR_INFO == null || !output.EX_T_PR_INFO.Any())
                {
                    Console.WriteLine("No data retrieved.");
                    msg.msg = "No data retrieved.";
                    msg.code = 204;
                    return msg;
                }
                int Insert = 0;
                for (int i = 0; i < output.EX_T_PR_INFO.Count(); i++)
                {
                    string PRHeaderDesc = "";
                    for (int q = 0; q < output.EX_T_PR_INFO[i].PR_HDR_TXT.Count(); q++)
                    {
                        PRHeaderDesc += output.EX_T_PR_INFO[i].PR_HDR_TXT[q].Data + "\n";
                    }
                    for (int j = 0; j < output.EX_T_PR_INFO[i].PR_ITM_INFO.Count(); j++)
                    {
                        Random rnd = new Random();
                        int uqNumb = rnd.Next(1000, 9999);
                        string UserPass = "A" + uqNumb.ToString() + "$";
                        string ItemLongDesc = "", MatPODesc = "", PurchageOrderText = "";
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TXT.Count(); q++)
                        {
                            ItemLongDesc += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TXT[q].Data + "\n";
                        }
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT.Count(); q++)
                        {
                            MatPODesc += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT[q].Data + "\n";
                        }
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT.Count(); q++)
                        {
                            PurchageOrderText += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT[q].Data + "\n";
                        }
                        if (output.EX_T_PR_INFO[i].PR_NUMBER == null || !output.EX_T_PR_INFO[i].PR_NUMBER.Any())
                        {
                            Console.WriteLine("No data retrieved.");
                            msg.msg = "No data retrieved.";
                            msg.code = 204;
                            return msg;
                        }
                        else
                        {
                            string BuyerName = "";
                            Nullable<DateTime> datePO = null, dateReqReleased = null;
                            datePO = output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_DATE;
                            dateReqReleased = output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RL_DATE;
                            SqlParameter[] paraColl1 = new SqlParameter[] {
                                           new SqlParameter("@P_OPERATION", 1),
                                           new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                           new SqlParameter("@P_PR_Type", output.EX_T_PR_INFO[i].PR_TYPE),
                                           new SqlParameter("@P_PR_DESCRIPTION",PRHeaderDesc.Trim()),
                                           new SqlParameter("@P_PRDate", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_ON),
                                           new SqlParameter("@P_APPROVED_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RL_DATE),
                                           new SqlParameter("@P_PR_LINE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_NO.Trim().TrimStart('0')),
                                           new SqlParameter("@P_ItemCode", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_MATERIAL.Trim().TrimStart('0')),
                                           new SqlParameter("@P_ITEM_DESCRIPTION",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_SHRT_TXT.Trim()),
                                           new SqlParameter("@P_ITEM_LONG_DESCRIPTION", ItemLongDesc.Trim()),
                                           new SqlParameter("@P_MAT_LONG_DESCRIPTION", MatPODesc.Trim()),
                                           new SqlParameter("@P_PurchageOrderText", PurchageOrderText.Trim()),
                                           new SqlParameter("@P_UOM", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_UOM.Trim()),
                                           new SqlParameter("@P_POQUANTITY", Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_TTL_QUAN.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_TTL_QUAN.Trim()) ),
                                           new SqlParameter("@P_QUANTITY", Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()) ),
                                           new SqlParameter("@P_UNIT_PRICE",Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim() )),
                                           new SqlParameter("@P_AMOUNT",Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()) *Convert.ToDecimal( output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim())),
                                           new SqlParameter("@P_CURRNCY_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRRNCY.Trim()),
                                           new SqlParameter("@P_PR_ITM_ACAS_CTGR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_ACAS_CTGR.Trim()),
                                           new SqlParameter("@P_PR_ITM_ACAS_CTGR_DESC", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_ACAS_CTGR_DESC.Trim()),
                                           new SqlParameter("@P_RequisitionDate", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTN_DATE),
                                           new SqlParameter("@P_PO_No", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_NUMBER.Trim()),
                                           new SqlParameter("@P_PR_ITM_DELETED", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_DELETED.Trim()),
                                           new SqlParameter("@P_PR_ITM_CLOSED", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CLOSE.Trim()),
                                           new SqlParameter("@P_PR_ITM_TRACKING", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TRACKING.Trim()),
                                           new SqlParameter("@P_PR_ITM_DELIVERY_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_DELIVERY_DATE),
                                           new SqlParameter("@P_PR_ITM_MATERIAL", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_MAT_GROUP.Trim()),
                                           new SqlParameter("@P_PR_ITM_COST_CNTR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_COST_CNTR.Trim()),
                                           new SqlParameter("@P_PR_ITM_WBS", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJ_NO.Trim()),
                                           new SqlParameter("@P_PR_WBS_Name", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_WBS_NAME.Trim()),
                                           new SqlParameter("@P_REQUESTOR",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTR),
                                           new SqlParameter("@P_REQUESTORNAME",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTR_FN),
                                           new SqlParameter("@P_CREATION_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_ON),
                                           new SqlParameter("@P_CREATED_BY",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_BY),
                                           new SqlParameter("@P_CREATED_BYNAME",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_BY_FN),
                                           new SqlParameter("@P_PROJECT_NUMBER", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJECT_NO.Trim()),
                                           new SqlParameter("@P_PR_PROJECT_NAME",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJECT_NAME.Trim()),
                                           new SqlParameter("@P_PR_ITM_ReqReleasedDate", dateReqReleased),
                                           new SqlParameter("@P_PR_ITM_TotalStock",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TOT_STOCK.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TOT_STOCK.Trim()),
                                           new SqlParameter("@P_USERPASSWORD",UserPass),
                                           new SqlParameter("@P_VENDOR_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_VENDOR.Trim()),
                                           new SqlParameter("@P_PR_ITM_PurchasingGroup", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PRCHNG_GRP.Trim()),
                                           new SqlParameter("@P_PLANT",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PLANT.Trim()),
                                           new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                                           new SqlParameter("@P_PR_LAST_REL_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_LAST_REL_CODE.Trim()),
                                           new SqlParameter("@P_PR_LAST_REL_TEXT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_LAST_REL_TEXT.Trim()),
                                           new SqlParameter("@P_GROSS_WEIGHT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].GROSS_WEIGHT.Trim()),
                                           new SqlParameter("@P_WEIGHT_UNIT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].WEIGHT_UNIT.Trim()),
                                           new SqlParameter("@P_PR_ITM_STRG_LCTN", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_STRG_LCTN.Trim()),
                                           new SqlParameter("@P_PR_ITM_STRG_LCTN_NAME", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_STRG_LCTN_NAME.Trim()),
                                           new SqlParameter("@P_BLOCK_INDICATOR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].BLOCK_INDICATOR.Trim()),
                                           new SqlParameter("@P_ITEM_CATEGORY", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_CATEGORY.Trim()),
                                           new SqlParameter("@P_ITEM_CATEGORY_NAME", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_CATEGORY_NAME.Trim()),
                            };
                            Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl1, this.currentUser.UserDatabase);
                            if (output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO.Count() != null)
                            {
                                for (int l = 0; l < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO.Count(); l++)
                                {
                                    string LongText = "";
                                    for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LONG_TEXT.Count(); q++)
                                    {
                                        LongText += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LONG_TEXT[q].Data + "\n";
                                    }
                                    SqlParameter[] paraColl2 = new SqlParameter[]  {
                                                      new SqlParameter("@P_OPERATION", 25),
                                                      new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                                      new SqlParameter("@P_PR_LINE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_NO.Trim().TrimStart('0')),
                                                      new SqlParameter("@P_ItemCode",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SERVICE_NUMBER.ToString()),
                                                      new SqlParameter("@P_ITEM_DESCRIPTION",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SHORT_TEXT_1),
                                                      new SqlParameter("@P_UOM",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].BASE_UNIT_OF_MEASURE),
                                                      new SqlParameter("@P_QUANTITY",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].QUANTITY_WITH_SIGN),
                                                      new SqlParameter("@P_UNIT_PRICE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].GROSS_VALUE),
                                                      new SqlParameter("@P_CURRNCY_CODE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].CURRENCY_KEY),
                                                      new SqlParameter("@P_SubLineRemarks",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].TAX_TARIFF_CODE),
                                                      new SqlParameter("@P_Line_Number",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LINE_NUMBER),
                                                      new SqlParameter("@P_Prise_Unit",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].PRICE_UNIT),
                                                      new SqlParameter("@P_Net_Value_In_Document_Currency",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].NET_VALUE_IN_DOCUMENT_CURRENCY),
                                                      new SqlParameter("@P_Material_Group",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].MATERIAL_GROUP),
                                                      new SqlParameter("@P_Serial_Number_For_Preq_Account",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SERIAL_NUMBER_FOR_PREQ_ACCOUNT),
                                                      new SqlParameter("@P_Gl_Account",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].GL_ACCOUNT),
                                                      new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                                                      new SqlParameter("@P_COST_CENTER",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].COST_CENTER),
                                                      new SqlParameter("@P_LongText",LongText),
                                                      new SqlParameter("@P_NETWORK",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].NETWORK),
                                    };
                                    Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl2, this.currentUser.UserDatabase);
                                }
                            }
                        }
                    }
                    if (output.EX_T_PR_INFO[i].PR_NUMBER == null || !output.EX_T_PR_INFO[i].PR_NUMBER.Any())
                    {
                        output.EX_T_PR_INFO[i].PR_ATTCH_INFO = null;
                        Console.WriteLine("No data retrieved.");
                        msg.msg = "No data retrieved.";
                        msg.code = 204;
                        return msg;
                    }
                    else
                    {
                        for (int j = 0; j < output.EX_T_PR_INFO[i].PR_ATTCH_INFO.Count(); j++)
                        {
                            byte[] EX_V_XSTRING = CommonFunc.DecodeStringToBytes(output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_CNTNT.TrimEnd());
                            //byte[] EX_V_XSTRING = output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_CNTNT;
                            string KeyName = string.Empty;
                            output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_CNTNT = null;
                            if (EX_V_XSTRING != null)
                            {
                                string FileWebName = output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_DESCR.Trim().Replace(" ", "_").Replace(".", "_").Replace("'", "") + "." + output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_TYPE.ToLower();
                                KeyName = Enums.PRAttachment + FileWebName;
                                Stream stream = new MemoryStream(EX_V_XSTRING);
                                AWSBucket.AWSBucketStorage(stream, KeyName, Enums.Upload);
                            }

                            SqlParameter[] paraColl3 = new SqlParameter[]  {
                                  new SqlParameter("@P_OPERATION", 4),
                                  new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                  new SqlParameter("@P_PR_UPLOADPATH", KeyName),
                                  new SqlParameter("@P_PR_FILENAME",System.IO.Path.GetFileName(KeyName)),
                                  new SqlParameter("@P_PR_ATTCHMNT_ID",output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_ID.Trim()),
                                  new SqlParameter("@P_PR_ATTCHMNT_DESCR", output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_DESCR.Trim()),
                                  new SqlParameter("@P_PR_ATTCHMNT_TYPE", output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_TYPE.ToUpper()),
                                  new SqlParameter("@P_PRATTACHMENTDATA", null),
                                  new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                             };
                            Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl3, this.currentUser.UserDatabase);
                        }
                    }
                    SqlParameter[] paraColl4 = new SqlParameter[]  {
                                            new SqlParameter("@P_OPERATION", 5),
                                            new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                            new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                    };
                    Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl4, this.currentUser.UserDatabase);
                }
                Console.WriteLine("Success End Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());
                msg.msg = "Successful. Data retrieved.";
                msg.code = 200;
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SAP connection is NOT active. : {ex.Message}, StackTrace: {ex.StackTrace}");
                msg.msg = "Connection failed. Error: " + ex.Message;
                msg.code = 500;
                return msg;
            }
        }

        public async Task<ResponseMsg> GetPRByPRNO_C_NA(string PRNO)
        {
            ResponseMsg msg = new ResponseMsg();
            try
            {

                var mapper = _Rfcconnection.Mapper;
                Configure(mapper);

                var func = _Rfcconnection.CallRfcFunction("ZPRCSNS_GET_PRCHS_RQSTN_BY_ID");
                var inputParameters = new PurchaseRequestInput
                {
                    IM_V_PR_ID = PRNO
                };
                func.Invoke(inputParameters);

                var output = func.GetOutputParameters<PurchaseRequestOutput>();
                if (output.EX_T_PR_INFO == null || !output.EX_T_PR_INFO.Any())
                {
                    Console.WriteLine("No data retrieved.");
                    msg.msg = "No data retrieved.";
                    msg.code = 204;
                    return msg;
                }
                int Insert = 0;
                for (int i = 0; i < output.EX_T_PR_INFO.Count(); i++)
                {
                    string PRHeaderDesc = "";
                    for (int q = 0; q < output.EX_T_PR_INFO[i].PR_HDR_TXT.Count(); q++)
                    {
                        PRHeaderDesc += output.EX_T_PR_INFO[i].PR_HDR_TXT[q].Data + "\n";
                    }
                    for (int j = 0; j < output.EX_T_PR_INFO[i].PR_ITM_INFO.Count(); j++)
                    {
                        Random rnd = new Random();
                        int uqNumb = rnd.Next(1000, 9999);
                        string UserPass = "A" + uqNumb.ToString() + "$";
                        string ItemLongDesc = "", MatPODesc = "", PurchageOrderText = "";
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TXT.Count(); q++)
                        {
                            ItemLongDesc += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TXT[q].Data + "\n";
                        }
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT.Count(); q++)
                        {
                            MatPODesc += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT[q].Data + "\n";
                        }
                        for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT.Count(); q++)
                        {
                            PurchageOrderText += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_MAT_PO_TXT[q].Data + "\n";
                        }
                        if (output.EX_T_PR_INFO[i].PR_NUMBER == null || !output.EX_T_PR_INFO[i].PR_NUMBER.Any())
                        {
                            Console.WriteLine("No data retrieved.");
                            msg.msg = "No data retrieved.";
                            msg.code = 204;
                            return msg;
                        }
                        else
                        {
                            string BuyerName = "";
                            Nullable<DateTime> datePO = null, dateReqReleased = null;
                            datePO = output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_DATE;
                            dateReqReleased = output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RL_DATE;
                            SqlParameter[] paraColl1 = new SqlParameter[] {
                                           new SqlParameter("@P_OPERATION", 1),
                                           new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                           new SqlParameter("@P_PR_Type", output.EX_T_PR_INFO[i].PR_TYPE),
                                           new SqlParameter("@P_PR_DESCRIPTION",PRHeaderDesc.Trim()),
                                           new SqlParameter("@P_PRDate", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_ON),
                                           new SqlParameter("@P_APPROVED_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RL_DATE),
                                           new SqlParameter("@P_PR_LINE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_NO.Trim().TrimStart('0')),
                                           new SqlParameter("@P_ItemCode", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_MATERIAL.Trim().TrimStart('0')),
                                           new SqlParameter("@P_ITEM_DESCRIPTION",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_SHRT_TXT.Trim()),
                                           new SqlParameter("@P_ITEM_LONG_DESCRIPTION", ItemLongDesc.Trim()),
                                           new SqlParameter("@P_MAT_LONG_DESCRIPTION", MatPODesc.Trim()),
                                           new SqlParameter("@P_PurchageOrderText", PurchageOrderText.Trim()),
                                           new SqlParameter("@P_UOM", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_UOM.Trim()),
                                           new SqlParameter("@P_POQUANTITY", Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_TTL_QUAN.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_TTL_QUAN.Trim()) ),
                                           new SqlParameter("@P_QUANTITY", Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()) ),
                                           new SqlParameter("@P_UNIT_PRICE",Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim() )),
                                           new SqlParameter("@P_AMOUNT",Convert.ToDecimal(output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_QUANTITY.Trim()) *Convert.ToDecimal( output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_VAL_PRICE.Trim())),
                                           new SqlParameter("@P_CURRNCY_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRRNCY.Trim()),
                                           new SqlParameter("@P_PR_ITM_ACAS_CTGR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_ACAS_CTGR.Trim()),
                                           new SqlParameter("@P_PR_ITM_ACAS_CTGR_DESC", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_ACAS_CTGR_DESC.Trim()),
                                           new SqlParameter("@P_RequisitionDate", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTN_DATE),
                                           new SqlParameter("@P_PO_No", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_NUMBER.Trim()),
                                           new SqlParameter("@P_PR_ITM_DELETED", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_DELETED.Trim()),
                                           new SqlParameter("@P_PR_ITM_CLOSED", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CLOSE.Trim()),
                                           new SqlParameter("@P_PR_ITM_TRACKING", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TRACKING.Trim()),
                                           new SqlParameter("@P_PR_ITM_DELIVERY_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_DELIVERY_DATE),
                                           new SqlParameter("@P_PR_ITM_MATERIAL", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_MAT_GROUP.Trim()),
                                           new SqlParameter("@P_PR_ITM_COST_CNTR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_COST_CNTR.Trim()),
                                           new SqlParameter("@P_PR_ITM_WBS", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJ_NO.Trim()),
                                           new SqlParameter("@P_PR_WBS_Name", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_WBS_NAME.Trim()),
                                           new SqlParameter("@P_REQUESTOR",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTR),
                                           new SqlParameter("@P_REQUESTORNAME",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_RQSTR_FN),
                                           new SqlParameter("@P_CREATION_DATE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_ON),
                                           new SqlParameter("@P_CREATED_BY",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_BY),
                                           new SqlParameter("@P_CREATED_BYNAME",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_CRTD_BY_FN),
                                           new SqlParameter("@P_PROJECT_NUMBER", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJECT_NO.Trim()),
                                           new SqlParameter("@P_PR_PROJECT_NAME",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_PROJECT_NAME.Trim()),
                                           new SqlParameter("@P_PR_ITM_ReqReleasedDate", dateReqReleased),
                                           new SqlParameter("@P_PR_ITM_TotalStock",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TOT_STOCK.Trim()=="" ? "0" :output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_TOT_STOCK.Trim()),
                                           new SqlParameter("@P_USERPASSWORD",UserPass),
                                           new SqlParameter("@P_VENDOR_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PO_VENDOR.Trim()),
                                           new SqlParameter("@P_PR_ITM_PurchasingGroup", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PRCHNG_GRP.Trim()),
                                           new SqlParameter("@P_PLANT",  output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_PLANT.Trim()),
                                           new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                                           new SqlParameter("@P_PR_LAST_REL_CODE", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_LAST_REL_CODE.Trim()),
                                           new SqlParameter("@P_PR_LAST_REL_TEXT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_LAST_REL_TEXT.Trim()),
                                           new SqlParameter("@P_GROSS_WEIGHT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].GROSS_WEIGHT.Trim()),
                                           new SqlParameter("@P_WEIGHT_UNIT", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].WEIGHT_UNIT.Trim()),
                                           new SqlParameter("@P_PR_ITM_STRG_LCTN", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_STRG_LCTN.Trim()),
                                           new SqlParameter("@P_PR_ITM_STRG_LCTN_NAME", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_STRG_LCTN_NAME.Trim()),
                                           new SqlParameter("@P_BLOCK_INDICATOR", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].BLOCK_INDICATOR.Trim()),
                                           new SqlParameter("@P_ITEM_CATEGORY", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_CATEGORY.Trim()),
                                           new SqlParameter("@P_ITEM_CATEGORY_NAME", output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_CATEGORY_NAME.Trim()),
                            };
                            Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl1, this.currentUser.UserDatabase);
                            if (output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO.Count() != null)
                            {
                                for (int l = 0; l < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO.Count(); l++)
                                {
                                    string LongText = "";
                                    for (int q = 0; q < output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LONG_TEXT.Count(); q++)
                                    {
                                        LongText += output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LONG_TEXT[q].Data + "\n";
                                    }
                                    SqlParameter[] paraColl2 = new SqlParameter[]  {
                                                      new SqlParameter("@P_OPERATION", 25),
                                                      new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                                      new SqlParameter("@P_PR_LINE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].PR_ITM_NO.Trim().TrimStart('0')),
                                                      new SqlParameter("@P_ItemCode",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SERVICE_NUMBER.ToString()),
                                                      new SqlParameter("@P_ITEM_DESCRIPTION",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SHORT_TEXT_1),
                                                      new SqlParameter("@P_UOM",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].BASE_UNIT_OF_MEASURE),
                                                      new SqlParameter("@P_QUANTITY",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].QUANTITY_WITH_SIGN),
                                                      new SqlParameter("@P_UNIT_PRICE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].GROSS_VALUE),
                                                      new SqlParameter("@P_CURRNCY_CODE",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].CURRENCY_KEY),
                                                      new SqlParameter("@P_SubLineRemarks",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].TAX_TARIFF_CODE),
                                                      new SqlParameter("@P_Line_Number",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].LINE_NUMBER),
                                                      new SqlParameter("@P_Prise_Unit",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].PRICE_UNIT),
                                                      new SqlParameter("@P_Net_Value_In_Document_Currency",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].NET_VALUE_IN_DOCUMENT_CURRENCY),
                                                      new SqlParameter("@P_Material_Group",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].MATERIAL_GROUP),
                                                      new SqlParameter("@P_Serial_Number_For_Preq_Account",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].SERIAL_NUMBER_FOR_PREQ_ACCOUNT),
                                                      new SqlParameter("@P_Gl_Account",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].GL_ACCOUNT),
                                                      new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                                                      new SqlParameter("@P_COST_CENTER",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].COST_CENTER),
                                                      new SqlParameter("@P_LongText",LongText),
                                                      new SqlParameter("@P_NETWORK",output.EX_T_PR_INFO[i].PR_ITM_INFO[j].ITEM_DETAIL_INFO[l].NETWORK),
                                    };
                                    Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl2, this.currentUser.UserDatabase);
                                }
                            }
                        }
                    }
                    if (output.EX_T_PR_INFO[i].PR_NUMBER == null || !output.EX_T_PR_INFO[i].PR_NUMBER.Any())
                    {
                        Console.WriteLine("No data retrieved.");
                        msg.msg = "No data retrieved.";
                        msg.code = 204;
                        return msg;
                    }
                    else
                    {
                        for (int j = 0; j < output.EX_T_PR_INFO[i].PR_ATTCH_INFO.Count(); j++)
                        {
                            byte[] EX_V_XSTRING = CommonFunc.DecodeStringToBytes(output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_CNTNT.TrimEnd());
                            //byte[] EX_V_XSTRING = output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_CNTNT;
                            string KeyName = string.Empty;

                            if (EX_V_XSTRING != null)
                            {
                                string FileWebName = output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_DESCR.Trim().Replace(" ", "_").Replace(".", "_").Replace("'", "") + "." + output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_TYPE.ToLower();
                                KeyName = Enums.PRAttachment + FileWebName;
                                Stream stream = new MemoryStream(EX_V_XSTRING);
                                AWSBucket.AWSBucketStorage(stream, KeyName, Enums.Upload);
                            }

                            SqlParameter[] paraColl3 = new SqlParameter[]  {
                                  new SqlParameter("@P_OPERATION", 4),
                                  new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                  new SqlParameter("@P_PR_UPLOADPATH", KeyName),
                                  new SqlParameter("@P_PR_FILENAME",System.IO.Path.GetFileName(KeyName)),
                                  new SqlParameter("@P_PR_ATTCHMNT_ID",output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_ID.Trim()),
                                  new SqlParameter("@P_PR_ATTCHMNT_DESCR", output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_DESCR.Trim()),
                                  new SqlParameter("@P_PR_ATTCHMNT_TYPE", output.EX_T_PR_INFO[i].PR_ATTCH_INFO[j].ATTCHMNT_TYPE.ToUpper()),
                                  new SqlParameter("@P_PRATTACHMENTDATA", null),
                                  new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                             };
                            Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl3, this.currentUser.UserDatabase);
                        }
                    }
                    SqlParameter[] paraColl4 = new SqlParameter[]  {
                                            new SqlParameter("@P_OPERATION", 5),
                                            new SqlParameter("@P_PRNo", output.EX_T_PR_INFO[i].PR_NUMBER),
                                            new SqlParameter("@P_ClientSAPId", AppSettings.Current.ClientSAPId.ToString()),
                    };
                    Insert = await DataAccess.CRUDOperation("SP_GETSAPPRDATA", paraColl4, this.currentUser.UserDatabase);
                }
                
                func.Dispose();
                output = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.WriteLine("Success End Time: " + CommonFunc.GetCurrentIndiaTimeFormatted());
                msg.msg = "Successful. Data retrieved.";
                msg.code = 200;
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SAP connection is NOT active. : {ex.Message}, StackTrace: {ex.StackTrace}");
                msg.msg = "Connection failed. Error: " + ex.Message;
                msg.code = 500;
                return msg;
            }
        }
    }
}
