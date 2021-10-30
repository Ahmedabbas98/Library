using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Helper
{
    public class pagingDetailes
    {
        public int TotalRow { get; set; }
        public int TotalPaging { get; set; }
        public int CurPage { get; set; }
        public Boolean HasNextPage { get; set; }
        public Boolean HasPrevPage { get; set; }

        public string NextPageUrl { get; set; }
        public string PrevPageUrl { get; set; }
    }
}
