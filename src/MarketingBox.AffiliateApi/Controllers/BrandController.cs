using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Affiliate.Service.Grpc.Models.Brands.Messages;
using MarketingBox.AffiliateApi.Extensions;
using MarketingBox.AffiliateApi.Models.Brands;
using MarketingBox.AffiliateApi.Models.Brands.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BrandCreateRequest = MarketingBox.AffiliateApi.Models.Brands.Requests.BrandCreateRequest;
using BrandUpdateRequest = MarketingBox.AffiliateApi.Models.Brands.Requests.BrandUpdateRequest;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
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

            var tenantId = this.GetTenantId();

            var response = await _brandService.SearchAsync(new BrandSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                BoxId = request.Id,
                Cursor = request.Cursor,
                Name = request.Name,
                Take = request.Limit,
                TenantId = tenantId
            });

            return Ok(
                response.Brands.Select(Map)
                    .ToArray()
                    .Paginate(request, Url, x => x.Id));
        }

        [HttpGet("{brandId}")]
        [ProducesResponseType(typeof(BrandModel), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<BrandModel, long>>> GetAsync(
            [FromRoute] long brandId)
        {
            var response = await _brandService.GetAsync(new BrandGetRequest()
            {
                 BrandId = brandId
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(BrandModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandModel>> CreateAsync(
            
            [FromBody] BrandCreateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _brandService.CreateAsync(new Affiliate.Service.Grpc.Models.Brands.Messages.BrandCreateRequest()
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
        [HttpPut("{brandId}")]
        [ProducesResponseType(typeof(BrandModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandModel>> UpdateAsync(
            
            [Required, FromRoute] long brandId,
            [FromBody] BrandUpdateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _brandService.UpdateAsync(new Affiliate.Service.Grpc.Models.Brands.Messages.BrandUpdateRequest()
            {
                Name = request.Name,
                TenantId = tenantId,
                Sequence = request.Sequence,
                BrandId = brandId
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{brandId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync(
            
            [Required, FromRoute] long brandId)
        {
            var tenantId = this.GetTenantId();
            var response = await _brandService.DeleteAsync(new Affiliate.Service.Grpc.Models.Brands.Messages.BrandDeleteRequest()
            {
                BrandId = brandId
            });

            return MapToResponse(response);
        }

        private ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Brands.BrandResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            return Ok(Map(response.Brand));
        }

        private static BrandModel Map(Affiliate.Service.Grpc.Models.Brands.Brand brand)
        {
            return new BrandModel()
            {
                Sequence = brand.Sequence,
                Name = brand.Name,
                Id = brand.Id
            };
        }

        private ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Brands.BrandResponse response)
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