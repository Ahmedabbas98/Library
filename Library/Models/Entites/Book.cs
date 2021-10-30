using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Entites
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string distribute { get; set; }
        public double Price { get; set; }
       
        public DateTime Date { get; set; }
        public int pageNumber { get; set; }

        public int AuthorID { get; set; }
        public Authors Authors { get; set; }
    }
}
