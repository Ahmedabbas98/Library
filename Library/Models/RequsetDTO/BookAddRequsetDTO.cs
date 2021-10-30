using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.RequsetDTO
{
    public class BookAddRequsetDTO
    {
        public string distribute { get; set; }
        public double Price { get; set; }
        // public string imge { get; set; }
        public DateTime Date { get; set; }
        public int pageNumber { get; set; }
    }
}
