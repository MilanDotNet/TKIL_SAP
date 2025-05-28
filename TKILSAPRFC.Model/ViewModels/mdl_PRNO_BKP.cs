using Newtonsoft.Json;
using NwRfcNet.TypeMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Model.ViewModels
{
    public class mdl_PRNO_BKP
    {
        public class PurchaseRequestInput
        {
            public string IM_V_PR_ID { get; set; }
        }

        public class PurchaseRequestOutput
        {
            public PurchaseRequest[] EX_T_PR_INFO { get; set; }
        }

        public class PurchaseRequest
        {
            public string PR_NUMBER { get; set; }
            public string PR_TYPE { get; set; }
            public string PR_TYPE_DESC { get; set; }
            public PurchaseRequestHeaderText[] PR_HDR_TXT { get; set; }
            public PurchaseRequestItem[] PR_ITM_INFO { get; set; }
            public PurchaseRequestAttachment[] PR_ATTCH_INFO { get; set; }
        }

        public class PurchaseRequestHeaderText
        {
            public dynamic Data { get; set; }
        }

        public class PurchaseRequestItem
        {
            public string PR_ITM_NO { get; set; }
            public string PR_ITM_VNDR { get; set; }
            public string PR_ITM_DELETED { get; set; }
            public string PR_ITM_STATUS { get; set; }
            public string PR_ITM_FULLY_RLSD { get; set; }
            public string PR_ITM_RL_IND { get; set; }
            public string PR_ITM_RL_STS { get; set; }
            public DateTime? PR_ITM_RL_DATE { get; set; }
            public string PR_ITM_PRCHNG_GRP { get; set; }
            public string PR_ITM_PRCHNG_GRP_DESC { get; set; }
            public string PR_PROJ_NO { get; set; }
            public string PR_ITM_ACAS_CTGR { get; set; }
            public string PR_ITM_ACAS_CTGR_DESC { get; set; }
            public string PR_ITM_ACAS_INFO { get; set; }
            public string PR_ITM_CRTD_BY { get; set; }
            public string PR_ITM_CRTD_BY_FN { get; set; }
            public DateTime? PR_ITM_CRTD_ON { get; set; }
            public string PR_ITM_RQSTR { get; set; }
            public string PR_ITM_RQSTR_FN { get; set; }
            public DateTime? PR_ITM_RQSTN_DATE { get; set; }
            public string PR_ITM_SHRT_TXT { get; set; }
            public string PR_ITM_MATERIAL { get; set; }
            public string PR_ITM_MATERIAL_DSC { get; set; }
            public string PR_ITM_PLANT { get; set; }
            public string PR_ITM_PLANT_NAME { get; set; }
            public string PR_ITM_STRG_LCTN { get; set; }
            public string PR_ITM_STRG_LCTN_NAME { get; set; }
            public string PR_ITM_MAT_GROUP { get; set; }
            public string PR_ITM_MAT_GROUP_DESC { get; set; }
            public string PR_ITM_PRCHNG_ORG { get; set; }
            public string PR_ITM_PRCHNG_ORG_DESC { get; set; }
            public string PR_ITM_QUANTITY { get; set; }
            public string PR_ITM_UOM { get; set; }
            public string PR_ITEM_PRICE { get; set; }
            public string PR_ITM_CRRNCY { get; set; }
            public string PR_ITM_PRICE_UNIT { get; set; }
            public DateTime? PR_ITM_DELIVERY_DATE { get; set; }
            public string PR_ITM_REVNO { get; set; }
            public string PR_ITM_PO_NUMBER { get; set; }
            public string PR_ITM_PO_ITM_NUMBER { get; set; }
            public string PR_ITM_PO_TTL_QUAN { get; set; }
            public DateTime? PR_ITM_PO_DATE { get; set; }
            public string PR_ITM_TRACKING { get; set; }
            public string PR_ITM_VAL_PRICE { get; set; }
            public string PR_ITM_WBS { get; set; }
            public string PR_ITM_COST_CNTR { get; set; }
            public string PR_ITM_ORD_NMBR { get; set; }
            public string PR_ITM_ASST_NMBR { get; set; }
            public string PR_ITM_TOT_STOCK { get; set; }
            public string PR_ITM_CLOSE { get; set; }
            public string PR_ITM_TOTAL_VALUE { get; set; }
            public PurchaseRequestItem_TBL1 PR_RLS_INFO { get; set; }
            public PurchaseRequestItem_TBL2[] PR_ITM_TXT { get; set; }
            public PurchaseRequestItem_TBL3[] PR_ITM_MAT_PO_TXT { get; set; }
            public PurchaseRequestItem_TBL4[] PR_MAT_PO_TXT { get; set; }
            public string PR_ITM_PO_VENDOR { get; set; }
            public PurchaseRequestItem_TBL5[] ITEM_DETAIL_INFO { get; set; }
            public string PR_LAST_REL_CODE { get; set; }
            public string PR_LAST_REL_TEXT { get; set; }
            public string WEIGHT_UNIT { get; set; }
            public string GROSS_WEIGHT { get; set; }
            public string NET_WEIGHT { get; set; }
            public string PR_WBS_NAME { get; set; }
            public string PR_PROJECT_NO { get; set; }
            public string PR_PROJECT_NAME { get; set; }
            public string BLOCK_INDICATOR { get; set; }
            public string ITEM_CATEGORY { get; set; }
            public string ITEM_CATEGORY_NAME { get; set; }
        }

        public class PurchaseRequestItem_TBL1
        {
            public string REL_GROUP { get; set; }
            public string REL_1_CODE { get; set; }
            public string REL_1_CODE_TXT { get; set; }
            public string REL_1_COMPLETED { get; set; }
            public string REL_1_USER_SAP_ID { get; set; }
            public string REL_1_USR_EMAIL { get; set; }
            public string REL_2_CODE { get; set; }
            public string REL_2_CODE_TXT { get; set; }
            public string REL_2_COMPLETED { get; set; }
            public string REL_2_USER_SAP_ID { get; set; }
            public string REL_2_USR_EMAIL { get; set; }
            public string REL_3_CODE { get; set; }
            public string REL_3_CODE_TXT { get; set; }
            public string REL_3_COMPLETED { get; set; }
            public string REL_3_USER_SAP_ID { get; set; }
            public string REL_3_USR_EMAIL { get; set; }
            public string REL_4_CODE { get; set; }
            public string REL_4_CODE_TXT { get; set; }
            public string REL_4_COMPLETED { get; set; }
            public string REL_4_USER_SAP_ID { get; set; }
            public string REL_4_USR_EMAIL { get; set; }
            public string REL_5_CODE { get; set; }
            public string REL_5_CODE_TXT { get; set; }
            public string REL_5_COMPLETED { get; set; }
            public string REL_5_USER_SAP_ID { get; set; }
            public string REL_5_USR_EMAIL { get; set; }
            public string REL_6_CODE { get; set; }
            public string REL_6_CODE_TXT { get; set; }
            public string REL_6_COMPLETED { get; set; }
            public string REL_6_USER_SAP_ID { get; set; }
            public string REL_6_USR_EMAIL { get; set; }
            public string REL_7_CODE { get; set; }
            public string REL_7_CODE_TXT { get; set; }
            public string REL_7_COMPLETED { get; set; }
            public string REL_7_USER_SAP_ID { get; set; }
            public string REL_7_USR_EMAIL { get; set; }
            public string REL_8_CODE { get; set; }
            public string REL_8_CODE_TXT { get; set; }
            public string REL_8_COMPLETED { get; set; }
            public string REL_8_USER_SAP_ID { get; set; }
            public string REL_8_USR_EMAIL { get; set; }
        }

        public class PurchaseRequestItem_TBL2
        {
            public dynamic Data { get; set; }
        }
        public class PurchaseRequestItem_TBL3
        {
            public dynamic Data { get; set; }
        }
        public class PurchaseRequestItem_TBL4
        {
            public dynamic Data { get; set; }
        }
        public class PurchaseRequestItem_TBL5
        {
            public string LINE_NUMBER { get; set; }
            public string SERVICE_NUMBER { get; set; }
            public string QUANTITY_WITH_SIGN { get; set; }
            public string BASE_UNIT_OF_MEASURE { get; set; }
            public string PRICE_UNIT { get; set; }
            public string GROSS_VALUE { get; set; }
            public string NET_VALUE_IN_DOCUMENT_CURRENCY { get; set; }
            public string SHORT_TEXT_1 { get; set; }
            public PurchaseRequestItem_TBL5_TBL_1[] LONG_TEXT { get; set; }
            public string LONG_TEXT2 { get; set; }
            public string TAX_TARIFF_CODE { get; set; }
            public string MATERIAL_GROUP { get; set; }
            public string SERIAL_NUMBER_FOR_PREQ_ACCOUNT { get; set; }
            public string COST_CENTER { get; set; }
            public string NETWORK { get; set; }
            public string NETWORK_ACTIVITY { get; set; }
            public string WBS_ELEMENT { get; set; }
            public string GL_ACCOUNT { get; set; }
            public string CURRENCY_KEY { get; set; }
        }

        public class PurchaseRequestItem_TBL5_TBL_1
        {
            public dynamic Data { get; set; }
        }

        public class PurchaseRequestAttachment
        {
            public string ATTCHMNT_ID { get; set; }
            public string ATTCHMNT_TYPE { get; set; }
            public string ATTCHMNT_DESCR { get; set; }
            public string ATTCHMNT_LANGU { get; set; }
            public string ATTCHMNT_CRTD_BY { get; set; }
            public string ATTCHMNT_CRTD_NAME { get; set; }
            public string ATTCHMNT_CREAT_DATE { get; set; }
            public string ATTCHMNT_CREAT_TIME { get; set; }
            public string ATTCHMNT_DOC_SIZE { get; set; }
            public string ATTCHMNT_CNTNT { get; set; }
        }

        public static void Configure(RfcMapper mapper)
        {
            
            //Input param
            mapper.Parameter<PurchaseRequestInput>()
                .Property(x => x.IM_V_PR_ID).HasParameterName("IM_V_PR_ID").HasParameterType(RfcFieldType.Char).MaxLength(50);


            //Output param
            mapper.Parameter<PurchaseRequestOutput>()
                .Property(x => x.EX_T_PR_INFO).HasParameterName("EX_T_PR_INFO").HasParameterType(RfcFieldType.Table);

            //Inner Table 
            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_NUMBER).HasParameterName("PR_NUMBER").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_TYPE).HasParameterName("PR_TYPE").HasParameterType(RfcFieldType.Char).MaxLength(4);

            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_TYPE_DESC).HasParameterName("PR_TYPE_DESC").HasParameterType(RfcFieldType.Char).MaxLength(20);

            mapper.Parameter<PurchaseRequest>()
               .Property(x => x.PR_HDR_TXT).HasParameterName("PR_HDR_TXT").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_ITM_INFO).HasParameterName("PR_ITM_INFO").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_ATTCH_INFO).HasParameterName("PR_ATTCH_INFO").HasParameterType(RfcFieldType.Table);


            //Inner Sub Field Map

            //Header
            mapper.Parameter<PurchaseRequestHeaderText>()
                .Property(x => x.Data).HasParameterName("").HasParameterType(RfcFieldType.Char).MaxLength(150);

            //Attch
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_ID).HasParameterName("ATTCHMNT_ID").HasParameterType(RfcFieldType.Char).MaxLength(46);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_TYPE).HasParameterName("ATTCHMNT_TYPE").HasParameterType(RfcFieldType.Char).MaxLength(3);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_DESCR).HasParameterName("ATTCHMNT_DESCR").HasParameterType(RfcFieldType.Char).MaxLength(50);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_LANGU).HasParameterName("ATTCHMNT_LANGU").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_CRTD_BY).HasParameterName("ATTCHMNT_CRTD_BY").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_CRTD_NAME).HasParameterName("ATTCHMNT_CRTD_NAME").HasParameterType(RfcFieldType.Char).MaxLength(35);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_CREAT_DATE).HasParameterName("ATTCHMNT_CREAT_DATE").HasParameterType(RfcFieldType.Char).MaxLength(8);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_CREAT_TIME).HasParameterName("ATTCHMNT_CREAT_TIME").HasParameterType(RfcFieldType.Char).MaxLength(6);
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_DOC_SIZE).HasParameterName("ATTCHMNT_DOC_SIZE").HasParameterType(RfcFieldType.Char).MaxLength(12);
            
            mapper.Parameter<PurchaseRequestAttachment>()
                .Property(x => x.ATTCHMNT_CNTNT).HasParameterName("ATTCHMNT_CNTNT").HasParameterType(RfcFieldType.Char).MaxLength(99999999);

            //Item
            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_NO).HasParameterName("PR_ITM_NO").HasParameterType(RfcFieldType.Char).MaxLength(5);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_VNDR).HasParameterName("PR_ITM_VNDR").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_DELETED).HasParameterName("PR_ITM_DELETED").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_STATUS).HasParameterName("PR_ITM_STATUS").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_FULLY_RLSD).HasParameterName("PR_ITM_FULLY_RLSD").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RL_IND).HasParameterName("PR_ITM_RL_IND").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RL_STS).HasParameterName("PR_ITM_RL_STS").HasParameterType(RfcFieldType.Char).MaxLength(8);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RL_DATE).HasParameterName("PR_ITM_RL_DATE").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PRCHNG_GRP).HasParameterName("PR_ITM_PRCHNG_GRP").HasParameterType(RfcFieldType.Char).MaxLength(3);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PRCHNG_GRP_DESC).HasParameterName("PR_ITM_PRCHNG_GRP_DESC").HasParameterType(RfcFieldType.Char).MaxLength(18);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_PROJ_NO).HasParameterName("PR_PROJ_NO").HasParameterType(RfcFieldType.Char).MaxLength(24);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_ACAS_CTGR).HasParameterName("PR_ITM_ACAS_CTGR").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_ACAS_CTGR_DESC).HasParameterName("PR_ITM_ACAS_CTGR_DESC").HasParameterType(RfcFieldType.Char).MaxLength(20);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_ACAS_INFO).HasParameterName("PR_ITM_ACAS_INFO").HasParameterType(RfcFieldType.Char).MaxLength(50);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_CRTD_BY).HasParameterName("PR_ITM_CRTD_BY").HasParameterType(RfcFieldType.Char).MaxLength(12);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_CRTD_BY_FN).HasParameterName("PR_ITM_CRTD_BY_FN").HasParameterType(RfcFieldType.Char).MaxLength(80);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_CRTD_ON).HasParameterName("PR_ITM_CRTD_ON").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RQSTR).HasParameterName("PR_ITM_RQSTR").HasParameterType(RfcFieldType.Char).MaxLength(12);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RQSTR_FN).HasParameterName("PR_ITM_RQSTR_FN").HasParameterType(RfcFieldType.Char).MaxLength(80);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RQSTN_DATE).HasParameterName("PR_ITM_RQSTN_DATE").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_SHRT_TXT).HasParameterName("PR_ITM_SHRT_TXT").HasParameterType(RfcFieldType.Char).MaxLength(40);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MATERIAL).HasParameterName("PR_ITM_MATERIAL").HasParameterType(RfcFieldType.Char).MaxLength(18);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MATERIAL_DSC).HasParameterName("PR_ITM_MATERIAL_DSC").HasParameterType(RfcFieldType.Char).MaxLength(40);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PLANT).HasParameterName("PR_ITM_PLANT").HasParameterType(RfcFieldType.Char).MaxLength(4);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PLANT_NAME).HasParameterName("PR_ITM_PLANT_NAME").HasParameterType(RfcFieldType.Char).MaxLength(30);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_STRG_LCTN).HasParameterName("PR_ITM_STRG_LCTN").HasParameterType(RfcFieldType.Char).MaxLength(4);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_STRG_LCTN_NAME).HasParameterName("PR_ITM_STRG_LCTN_NAME").HasParameterType(RfcFieldType.Char).MaxLength(16);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MAT_GROUP).HasParameterName("PR_ITM_MAT_GROUP").HasParameterType(RfcFieldType.Char).MaxLength(9);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MAT_GROUP_DESC).HasParameterName("PR_ITM_MAT_GROUP_DESC").HasParameterType(RfcFieldType.Char).MaxLength(20);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PRCHNG_ORG).HasParameterName("PR_ITM_PRCHNG_ORG").HasParameterType(RfcFieldType.Char).MaxLength(4);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PRCHNG_ORG_DESC).HasParameterName("PR_ITM_PRCHNG_ORG_DESC").HasParameterType(RfcFieldType.Char).MaxLength(20);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_QUANTITY).HasParameterName("PR_ITM_QUANTITY").HasParameterType(RfcFieldType.Char).MaxLength(13);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_UOM).HasParameterName("PR_ITM_UOM").HasParameterType(RfcFieldType.Char).MaxLength(3);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITEM_PRICE).HasParameterName("PR_ITEM_PRICE").HasParameterType(RfcFieldType.Char).MaxLength(11);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_CRRNCY).HasParameterName("PR_ITM_CRRNCY").HasParameterType(RfcFieldType.Char).MaxLength(5);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PRICE_UNIT).HasParameterName("PR_ITM_PRICE_UNIT").HasParameterType(RfcFieldType.Char).MaxLength(5);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_DELIVERY_DATE).HasParameterName("PR_ITM_DELIVERY_DATE").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_REVNO).HasParameterName("PR_ITM_REVNO").HasParameterType(RfcFieldType.Char).MaxLength(8);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PO_NUMBER).HasParameterName("PR_ITM_PO_NUMBER").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PO_ITM_NUMBER).HasParameterName("PR_ITM_PO_ITM_NUMBER").HasParameterType(RfcFieldType.Char).MaxLength(5);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PO_TTL_QUAN).HasParameterName("PR_ITM_PO_TTL_QUAN").HasParameterType(RfcFieldType.Char).MaxLength(13);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PO_DATE).HasParameterName("PR_ITM_PO_DATE").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_TRACKING).HasParameterName("PR_ITM_TRACKING").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_VAL_PRICE).HasParameterName("PR_ITM_VAL_PRICE").HasParameterType(RfcFieldType.Char).MaxLength(11);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_WBS).HasParameterName("PR_ITM_WBS").HasParameterType(RfcFieldType.Char).MaxLength(8);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_COST_CNTR).HasParameterName("PR_ITM_COST_CNTR").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_ORD_NMBR).HasParameterName("PR_ITM_ORD_NMBR").HasParameterType(RfcFieldType.Char).MaxLength(12);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_ASST_NMBR).HasParameterName("PR_ITM_ASST_NMBR").HasParameterType(RfcFieldType.Char).MaxLength(12);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_TOT_STOCK).HasParameterName("PR_ITM_TOT_STOCK").HasParameterType(RfcFieldType.Char).MaxLength(13);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_CLOSE).HasParameterName("PR_ITM_CLOSE").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_TOTAL_VALUE).HasParameterName("PR_ITM_TOTAL_VALUE").HasParameterType(RfcFieldType.Char).MaxLength(15);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_RLS_INFO).HasParameterName("PR_RLS_INFO").HasParameterType(RfcFieldType.Structure);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_TXT).HasParameterName("PR_ITM_TXT").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MAT_PO_TXT).HasParameterName("PR_ITM_MAT_PO_TXT").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_MAT_PO_TXT).HasParameterName("PR_MAT_PO_TXT").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_PO_VENDOR).HasParameterName("PR_ITM_PO_VENDOR").HasParameterType(RfcFieldType.Char).MaxLength(10);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.ITEM_DETAIL_INFO).HasParameterName("ITEM_DETAIL_INFO").HasParameterType(RfcFieldType.Table);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_LAST_REL_CODE).HasParameterName("PR_LAST_REL_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_LAST_REL_TEXT).HasParameterName("PR_LAST_REL_TEXT").HasParameterType(RfcFieldType.Char).MaxLength(20);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.WEIGHT_UNIT).HasParameterName("WEIGHT_UNIT").HasParameterType(RfcFieldType.Char).MaxLength(3);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.GROSS_WEIGHT).HasParameterName("GROSS_WEIGHT").HasParameterType(RfcFieldType.Char).MaxLength(13);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.NET_WEIGHT).HasParameterName("NET_WEIGHT").HasParameterType(RfcFieldType.Char).MaxLength(13);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_WBS_NAME).HasParameterName("PR_WBS_NAME").HasParameterType(RfcFieldType.Char).MaxLength(40);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_PROJECT_NO).HasParameterName("PR_PROJECT_NO").HasParameterType(RfcFieldType.Char).MaxLength(50);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_PROJECT_NAME).HasParameterName("PR_PROJECT_NAME").HasParameterType(RfcFieldType.Char).MaxLength(40);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.BLOCK_INDICATOR).HasParameterName("BLOCK_INDICATOR").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.ITEM_CATEGORY).HasParameterName("ITEM_CATEGORY").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.ITEM_CATEGORY_NAME).HasParameterName("ITEM_CATEGORY_NAME").HasParameterType(RfcFieldType.Char).MaxLength(20);



            //tbl1
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_GROUP).HasParameterName("REL_GROUP").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_1_CODE).HasParameterName("REL_1_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_1_CODE_TXT).HasParameterName("REL_1_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_1_COMPLETED).HasParameterName("REL_1_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_1_USER_SAP_ID).HasParameterName("REL_1_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_1_USR_EMAIL).HasParameterName("REL_1_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_2_CODE).HasParameterName("REL_2_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_2_CODE_TXT).HasParameterName("REL_2_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_2_COMPLETED).HasParameterName("REL_2_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_2_USER_SAP_ID).HasParameterName("REL_2_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_2_USR_EMAIL).HasParameterName("REL_2_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_3_CODE).HasParameterName("REL_3_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_3_CODE_TXT).HasParameterName("REL_3_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_3_COMPLETED).HasParameterName("REL_3_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_3_USER_SAP_ID).HasParameterName("REL_3_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_3_USR_EMAIL).HasParameterName("REL_3_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_4_CODE).HasParameterName("REL_4_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_4_CODE_TXT).HasParameterName("REL_4_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_4_COMPLETED).HasParameterName("REL_4_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_4_USER_SAP_ID).HasParameterName("REL_4_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_4_USR_EMAIL).HasParameterName("REL_4_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_5_CODE).HasParameterName("REL_5_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_5_CODE_TXT).HasParameterName("REL_5_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_5_COMPLETED).HasParameterName("REL_5_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_5_USER_SAP_ID).HasParameterName("REL_5_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_5_USR_EMAIL).HasParameterName("REL_5_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_6_CODE).HasParameterName("REL_6_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_6_CODE_TXT).HasParameterName("REL_6_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_6_COMPLETED).HasParameterName("REL_6_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_6_USER_SAP_ID).HasParameterName("REL_6_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_6_USR_EMAIL).HasParameterName("REL_6_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_7_CODE).HasParameterName("REL_7_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_7_CODE_TXT).HasParameterName("REL_7_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_7_COMPLETED).HasParameterName("REL_7_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_7_USER_SAP_ID).HasParameterName("REL_7_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_7_USR_EMAIL).HasParameterName("REL_7_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_8_CODE).HasParameterName("REL_8_CODE").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_8_CODE_TXT).HasParameterName("REL_8_CODE_TXT").HasParameterType(RfcFieldType.Char).MaxLength(20);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_8_COMPLETED).HasParameterName("REL_8_COMPLETED").HasParameterType(RfcFieldType.Char).MaxLength(1);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_8_USER_SAP_ID).HasParameterName("REL_8_USER_SAP_ID").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL1>()
                .Property(x => x.REL_8_USR_EMAIL).HasParameterName("REL_8_USR_EMAIL").HasParameterType(RfcFieldType.Char).MaxLength(241);

            //tbl2
            mapper.Parameter<PurchaseRequestItem_TBL2>()
                .Property(x => x.Data).HasParameterName("").HasParameterType(RfcFieldType.Char).MaxLength(150);
            //tbl3
            mapper.Parameter<PurchaseRequestItem_TBL3>()
                .Property(x => x.Data).HasParameterName("").HasParameterType(RfcFieldType.Char).MaxLength(150);
            //tbl4
            mapper.Parameter<PurchaseRequestItem_TBL4>()
                .Property(x => x.Data).HasParameterName("").HasParameterType(RfcFieldType.Char).MaxLength(150);
            //tbl5
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.LINE_NUMBER).HasParameterName("LINE_NUMBER").HasParameterType(RfcFieldType.Char).MaxLength(10);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.SERVICE_NUMBER).HasParameterName("SERVICE_NUMBER").HasParameterType(RfcFieldType.Char).MaxLength(18);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.QUANTITY_WITH_SIGN).HasParameterName("QUANTITY_WITH_SIGN").HasParameterType(RfcFieldType.Char).MaxLength(13);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.BASE_UNIT_OF_MEASURE).HasParameterName("BASE_UNIT_OF_MEASURE").HasParameterType(RfcFieldType.Char).MaxLength(3);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.PRICE_UNIT).HasParameterName("PRICE_UNIT").HasParameterType(RfcFieldType.Char).MaxLength(5);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.GROSS_VALUE).HasParameterName("GROSS_VALUE").HasParameterType(RfcFieldType.Char).MaxLength(11);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.NET_VALUE_IN_DOCUMENT_CURRENCY).HasParameterName("NET_VALUE_IN_DOCUMENT_CURRENCY").HasParameterType(RfcFieldType.Char).MaxLength(15);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.SHORT_TEXT_1).HasParameterName("SHORT_TEXT_1").HasParameterType(RfcFieldType.Char).MaxLength(40);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.LONG_TEXT).HasParameterName("LONG_TEXT").HasParameterType(RfcFieldType.Table);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.LONG_TEXT2).HasParameterName("LONG_TEXT2").HasParameterType(RfcFieldType.Char).MaxLength(100);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.TAX_TARIFF_CODE).HasParameterName("TAX_TARIFF_CODE").HasParameterType(RfcFieldType.Char).MaxLength(16);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.MATERIAL_GROUP).HasParameterName("MATERIAL_GROUP").HasParameterType(RfcFieldType.Char).MaxLength(9);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.SERIAL_NUMBER_FOR_PREQ_ACCOUNT).HasParameterName("SERIAL_NUMBER_FOR_PREQ_ACCOUNT").HasParameterType(RfcFieldType.Char).MaxLength(2);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.COST_CENTER).HasParameterName("COST_CENTER").HasParameterType(RfcFieldType.Char).MaxLength(10);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.NETWORK).HasParameterName("NETWORK").HasParameterType(RfcFieldType.Char).MaxLength(12);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.NETWORK_ACTIVITY).HasParameterName("NETWORK_ACTIVITY").HasParameterType(RfcFieldType.Char).MaxLength(4);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.WBS_ELEMENT).HasParameterName("WBS_ELEMENT").HasParameterType(RfcFieldType.Char).MaxLength(8);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.GL_ACCOUNT).HasParameterName("GL_ACCOUNT").HasParameterType(RfcFieldType.Char).MaxLength(10);
            mapper.Parameter<PurchaseRequestItem_TBL5>()
               .Property(x => x.CURRENCY_KEY).HasParameterName("CURRENCY_KEY").HasParameterType(RfcFieldType.Char).MaxLength(5);
            //tbl5_1
            mapper.Parameter<PurchaseRequestItem_TBL5_TBL_1>()
                .Property(x => x.Data).HasParameterName("").HasParameterType(RfcFieldType.Char).MaxLength(150);

        }
    }

}

