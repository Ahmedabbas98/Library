using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Helper
{
    public class ErrorClass
    {
        private readonly IConfiguration configuration;

        public ErrorClass(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string ErrorCode { get; set; }
        public string ErrorProp { get; set; }
        public string ErrorMassege { get; set; }
        public void loadError(string ErrorCode)
        {
            this.ErrorCode = ErrorCode;
            var eSections = configuration.GetSection("Error");
            eSections.Bind(ErrorCode, this);

        }
    }
}
