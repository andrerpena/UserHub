using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserHub.Code
{
    public interface ITenancyResolver
    {
        string GetTenancy();
    }
}