using MarketingBox.AffiliateApi.Models.Partners;
using MarketingBox.AffiliateApi.Models.Partners.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.AffiliateApi.Models.Boxes;
using MarketingBox.AffiliateApi.Models.Boxes.Requests;
using MarketingBox.AffiliateApi.Models.Brands;
using MarketingBox.AffiliateApi.Models.Brands.Requests;

namespace MarketingBox.AffiliateApi.Controllers
{
    [ApiController]
    [Route("/api/brands")]
    public class BrandController : ControllerBase
    {
        public BrandController()
        {
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<BrandModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<BrandModel, long>>> SearchAsync(
            [FromQuery] BrandsSearchRequest request)
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
        [HttpPost]
        [ProducesResponseType(typeof(BrandModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandModel>> CreateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [FromBody] BrandCreateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{brandId}")]
        [ProducesResponseType(typeof(BrandModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandModel>> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long brandId,
            [FromBody] BrandUpdateRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{brandId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            [Required, FromHeader(Name = "X-Request-ID")] string requestId,
            [Required, FromRoute] long brandId)
        {
            return Ok();
        }
    }
}