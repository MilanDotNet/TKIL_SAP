namespace TKILSAPRFC.Core.Helpers
{
    public class PagedResult<T>
    {
        public class PagingInfo
        {
            public int PageNumber { get; set; }

            public int PageSize { get; set; }

            public int PageCount { get; set; }

            public long TotalRecords { get; set; }

        }
        public List<T> Data { get; private set; }

        public PagingInfo Paging { get; private set; }

        public PagedResult(IQueryable<T> items, int pageNumber, int pageSize, long totalRecords)
        {
            Data = new List<T>(items.Page(pageSize, pageNumber));
            Paging = new PagingInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                PageCount = totalRecords > 0
                    ? (int)Math.Ceiling(totalRecords / (double)pageSize)
                    : 0
            };
        }

        public PagedResult(IEnumerable<T> items, int pageNumber, int pageSize, long totalRecords)
        {
            Data = new List<T>(items.Page(pageSize, pageNumber));
            Paging = new PagingInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                PageCount = totalRecords > 0
                    ? (int)Math.Ceiling(totalRecords / (double)pageSize)
                    : 0
            };
        }
    }
}
