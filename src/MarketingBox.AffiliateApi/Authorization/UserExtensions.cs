using System;
using System.Linq;
using System.Security.Claims;

namespace MarketingBox.AffiliateApi.Authorization
{
    public static class UserExtensions
    {
        public static bool HasTenantId(this ClaimsPrincipal user)
        {
            return user.HasClaim("tenant-id");
        }

        public static bool HasUserId(this ClaimsPrincipal user)
        {
            return user.HasClaim("user-id");
        }

        public static bool HasApiKeyId(this ClaimsPrincipal user)
        {
            return user.HasClaim("api-key-id");
        }

        public static string GetTenantId(this ClaimsPrincipal user)
        {
            return user.GetTenantIdOrDefault() ?? throw new InvalidOperationException("There is no tenant-id claim");
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.GetUserIdOrDefault() ?? throw new InvalidOperationException("There is no user-id claim");
        }

        public static string GetApiKeyId(this ClaimsPrincipal user)
        {
            return user.GetApiKeyIdOrDefault() ?? throw new InvalidOperationException("There is no api-key-id claim");
        }

        public static string GetTenantIdOrDefault(this ClaimsPrincipal user)
        {
            return user.GetClaimOrDefault("tenant-id");
        }

        public static string GetUserIdOrDefault(this ClaimsPrincipal user)
        {
            return user.GetClaimOrDefault("user-id");
        }
        
        public static string GetApiKeyIdOrDefault(this ClaimsPrincipal user)
        {
            return user.GetClaimOrDefault("api-key-id");
        }

        public static bool HasClaim(this ClaimsPrincipal user, string claim)
        {
            return user.Identities
                .SelectMany(x => x.Claims)
                .Any(c => c.Type == claim);
        }

        public static string GetClaimOrDefault(this ClaimsPrincipal user, string claim)
        {
            return user.Identities
                .SelectMany(x => x.Claims)
                .Where(c => c.Type == claim)
                .Select(x => x.Value)
                .SingleOrDefault();
        }
    }
}