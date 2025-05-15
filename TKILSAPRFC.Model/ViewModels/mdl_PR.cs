using Newtonsoft.Json;
using NwRfcNet.TypeMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Model.ViewModels
{
    public class mdl_PR
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
            public string PR_NUMBER { get; set; } // Replace with actual column name
            public string PR_TYPE { get; set; } // Replace with actual column name
            public string PR_TYPE_DESC { get; set; } // Replace with actual column name
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
        }
    }
}
