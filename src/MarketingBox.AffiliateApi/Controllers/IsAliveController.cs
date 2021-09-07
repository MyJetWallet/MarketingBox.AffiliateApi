using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("api/isalive")]
    public class IsAliveController : ControllerBase
    {
        //[HttpGet]
        //[ProducesResponseType(typeof(IsAliveResponse), StatusCodes.Status200OK)]
        //public IsAliveResponse Get()
        //{
        //    var response = new IsAliveResponse
        //    {
        //        Name = ApplicationInformation.AppName,
        //        Version = ApplicationInformation.AppVersion,
        //        StartedAt = ApplicationInformation.StartedAt,
        //        Env = ApplicationEnvironment.Environment,
        //        HostName = ApplicationEnvironment.HostName,
        //        StateIndicators = new List<IsAliveResponse.StateIndicator>()
        //    };

        //    return response;
        //}
    }

    //[Authorize]
}