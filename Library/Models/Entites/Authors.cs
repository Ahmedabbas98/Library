using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Entites
{
    public class Authors
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public bool IsDeleted { get; set; }
        public List<Book> Books { get; set; }
    }
}
