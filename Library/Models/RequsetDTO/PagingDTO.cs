using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.RequsetDTO
{
    public class PagingDTO
    {
        private int rowCount1 = 10;
        public int rowCount { get => rowCount1; set => rowCount1 = Math.Min(10, value); }
        public int PagingNumber { get; set; } = 1;
    }
}
