using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserHub.Code
{
    public class TenancyResolver : ITenancyResolver
    {
        public string GetTenancy()
        {
            return "fuck";
        }
    }
}