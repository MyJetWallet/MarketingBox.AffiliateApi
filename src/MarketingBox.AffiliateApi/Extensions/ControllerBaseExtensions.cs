using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetTenantId(this ControllerBase controllerBase)
        {
            //controllerBase.User.Claims;
            return "Test-Tenant";
        }
    }
}
