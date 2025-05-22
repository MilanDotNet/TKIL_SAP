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
    public class mdl_PRNew
    {
        public class PurchaseRequestInput
        {
            public string IM_V_PR_CHNG_FRM_DATE { get; set; }
            public string IM_V_PR_CHNG_TO_DATE { get; set; }
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
            public PurchaseRequestItem[] PR_ITM_INFO { get; set; }
        }

        public class PurchaseRequestItem
        {
            public string PR_ITM_NO { get; set; }
            public string PR_ITM_STATUS { get; set; }
            public DateTime? PR_ITM_RL_DATE { get; set; }
            public string PR_ITM_MATERIAL { get; set; }
            public string PR_ITM_MATERIAL_DSC { get; set; }
            
        }

        public static void Configure(RfcMapper mapper)
        {
            // Input parameters
            mapper.Parameter<PurchaseRequestInput>()
                .Property(x => x.IM_V_PR_CHNG_FRM_DATE)
                .HasParameterName("IM_V_PR_CHNG_FRM_DATE")
                .HasParameterType(RfcFieldType.Char)
                .MaxLength(8);

            mapper.Parameter<PurchaseRequestInput>()
                .Property(x => x.IM_V_PR_CHNG_TO_DATE)
                .HasParameterName("IM_V_PR_CHNG_TO_DATE")
                .HasParameterType(RfcFieldType.Char)
                .MaxLength(8);

            // Output structure
            mapper.Parameter<PurchaseRequestOutput>()
                .Property(x => x.EX_T_PR_INFO)
                .HasParameterName("EX_T_PR_INFO")
                .HasParameterType(RfcFieldType.Table);


            // Table row structure
            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_NUMBER)
                .HasParameterName("PR_NUMBER")
                .HasParameterType(RfcFieldType.Char)
                .MaxLength(20);
            
            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_TYPE)
                .HasParameterName("PR_TYPE")
                .HasParameterType(RfcFieldType.Char)
                .MaxLength(15);
            
            mapper.Parameter<PurchaseRequest>()
                .Property(x => x.PR_TYPE_DESC)
                .HasParameterName("PR_TYPE_DESC")
                .HasParameterType(RfcFieldType.Char)
                .MaxLength(40);

            mapper.Parameter<PurchaseRequest>()
            .Property(x => x.PR_ITM_INFO)
            .HasParameterName("PR_ITM_INFO")
            .HasParameterType(RfcFieldType.Table);



            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_NO).HasParameterName("PR_ITM_NO").HasParameterType(RfcFieldType.Char).MaxLength(5);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_STATUS).HasParameterName("PR_ITM_STATUS").HasParameterType(RfcFieldType.Char).MaxLength(1);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_RL_DATE).HasParameterName("PR_ITM_RL_DATE").HasParameterType(RfcFieldType.Date);

            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MATERIAL).HasParameterName("PR_ITM_MATERIAL").HasParameterType(RfcFieldType.Char).MaxLength(100);
            
            mapper.Parameter<PurchaseRequestItem>()
                .Property(x => x.PR_ITM_MATERIAL_DSC).HasParameterName("PR_ITM_MATERIAL_DSC").HasParameterType(RfcFieldType.Char).MaxLength(100);

        }
    }
}
