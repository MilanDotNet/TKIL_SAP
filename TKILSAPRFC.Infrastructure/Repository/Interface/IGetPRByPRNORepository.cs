using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKILSAPRFC.Infrastructure.Repository.Interface
{
    public interface IGetPRByPRNORepository
    {
        public Task<ResponseMsg> GetPRByPRNO(string PRNO);
        public Task<ResponseMsg> GetPRByPRNO_C_NA(string PRNO);
    }
}
