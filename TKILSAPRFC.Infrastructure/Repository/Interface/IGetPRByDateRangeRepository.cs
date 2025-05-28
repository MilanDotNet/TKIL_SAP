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
    public interface IGetPRByDateRangeRepository
    {
        public Task<ResponseMsg> PRByDateRange(string FromDate, string ToDate);
    }
}
