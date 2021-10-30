using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ResponseDTO
{
    public class AuthorResponseDTO
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int BookCount { get; set; }
    }
}
