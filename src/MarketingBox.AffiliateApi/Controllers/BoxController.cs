using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Affiliate.Service.Grpc.Models.Boxes.Messages;
using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Models.Boxes;
using MarketingBox.AffiliateApi.Models.Boxes.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BoxCreateRequest = MarketingBox.AffiliateApi.Models.Boxes.Requests.BoxCreateRequest;
using BoxUpdateRequest = MarketingBox.AffiliateApi.Models.Boxes.Requests.BoxUpdateRequest;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/boxes")]
    public class BoxController : ControllerBase
    {
        private readonly IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<BoxModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<BoxModel, long>>> SearchAsync(
            [FromQuery] BoxesSearchRequest request)
        {
            if (request.Limit < 1 || request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should not be in the range 1..1000");

                return BadRequest();
            }

            //return Ok(
            //    many.Select(MapToResponse)
            //        .ToArray()
            //        .Paginate(request, Url, x => x.Id));

            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet("{boxId}")]
        [ProducesResponseType(typeof(BoxModel), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<BoxModel, long>>> SearchAsync(
            [Required, FromRoute] long boxId)
        {
            var response = await _boxService.GetAsync(new BoxGetRequest()
            {
                BoxId = boxId
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(BoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BoxModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] BoxCreateRequest request)
        {
            var tenantId = this.GetTenantId();

            var response = await _boxService.CreateAsync(new Affiliate.Service.Grpc.Models.Boxes.Messages.BoxCreateRequest()
            {
                Name = request.Name,
                TenantId = tenantId
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{boxId}")]
        [ProducesResponseType(typeof(BoxModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BoxModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long boxId,
            [FromBody] BoxUpdateRequest request)
        {
            var tenantId = this.GetTenantId();

            var response = await _boxService.UpdateAsync(new Affiliate.Service.Grpc.Models.Boxes.Messages.BoxUpdateRequest()
            {
                Name = request.Name,
                TenantId = tenantId,
                BoxId = boxId,
                Sequence = request.Sequence
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{boxId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long boxId)
        {
            var tenantId = this.GetTenantId();

            var response = await _boxService.DeleteAsync(new Affiliate.Service.Grpc.Models.Boxes.Messages.BoxDeleteRequest()
            {
                BoxId = boxId,
            });

            return MapToResponseEmpty(response);
        }

        public ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Boxes.BoxResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok(new BoxModel()
            {
                Sequence = response.Box.Sequence,
                Name = response.Box.Name,
                Id = response.Box.Id
            });
        }

        public ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Boxes.BoxResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}