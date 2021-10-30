using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ResponseDTO
{
    public class AuthResult
    {
        public Boolean Success { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
