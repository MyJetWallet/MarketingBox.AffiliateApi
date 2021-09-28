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
using System.Linq;
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
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should be in the range 1..1000");

                return BadRequest();
            }

            var tenantId = this.GetTenantId();

            var response = await _boxService.SearchAsync(new BoxSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                BoxId = request.Id,
                Cursor = request.Cursor,
                Name = request.Name,
                Take = request.Limit,
                TenantId = tenantId
            });

            return Ok(
                response.Boxes.Select(Map)
                    .ToArray()
                    .Paginate(request, Url, x => x.Id));
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
            [Required, FromRoute] long boxId)
        {
            var tenantId = this.GetTenantId();

            var response = await _boxService.DeleteAsync(new Affiliate.Service.Grpc.Models.Boxes.Messages.BoxDeleteRequest()
            {
                BoxId = boxId,
            });

            return MapToResponseEmpty(response);
        }

        private ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Boxes.BoxResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            if (response.Box == null)
                return NotFound();

            return Ok(Map(response.Box));
        }

        private static BoxModel Map(MarketingBox.Affiliate.Service.Grpc.Models.Boxes.Box box)
        {
            return new BoxModel()
            {
                Sequence = box.Sequence,
                Name = box.Name,
                Id = box.Id
            };
        }

        private ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Boxes.BoxResponse response)
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