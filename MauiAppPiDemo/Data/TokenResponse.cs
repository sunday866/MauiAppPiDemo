using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppPiDemo.Data
{
    public class TokenResponse
    {
        public License license { get; set; }
        public string token { get; set; }
        public string version { get; set; }
    }

    public class License
    {
        public string edition { get; set; }
    }
}
