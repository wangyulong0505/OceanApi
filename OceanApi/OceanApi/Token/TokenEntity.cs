using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OceanApi.Token
{
    public class TokenEntity
    {
        public string AccessToken { get; set; }

        public int Expired { get; set; }
    }
}
