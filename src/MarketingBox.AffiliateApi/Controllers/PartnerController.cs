using MarketingBox.AffiliateApi.Models.Partners;
using MarketingBox.AffiliateApi.Models.Partners.Requests;
using MarketingBox.AffiliateApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MarketingBox.Affiliate.Service.Client;
using MarketingBox.Affiliate.Service.Grpc;
using MarketingBox.Affiliate.Service.Grpc.Models.Partners.Messages;
using MarketingBox.Affiliate.Service.Grpc.Models.Partners.Requests;
using MarketingBox.AffiliateApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using PartnerCreateRequest = MarketingBox.AffiliateApi.Models.Partners.Requests.PartnerCreateRequest;
using PartnerUpdateRequest = MarketingBox.AffiliateApi.Models.Partners.Requests.PartnerUpdateRequest;
using System.Linq;

namespace MarketingBox.AffiliateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/partners")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Paginated<PartnerModel, long>), StatusCodes.Status200OK)]

        public async Task<ActionResult<Paginated<PartnerModel, long>>> SearchAsync(
            [FromQuery] PartnersSearchRequest request)
        {
            if (request.Limit < 1 || request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should not be in the range 1..1000");

                return BadRequest();
            }

            var tenantId = this.GetTenantId();
            var role = request.Role?.MapEnum<MarketingBox.Affiliate.Service.Grpc.Models.Partners.PartnerRole>();

            var response = await _partnerService.SearchAsync(new PartnerSearchRequest()
            {
                Asc = request.Order == PaginationOrder.Asc,
                Cursor = request.Cursor,
                Name = request.Name,
                AffiliateId = request.Id,
                CreatedAt = request.CreatedAt,
                Email = request.Email,
                Role = role,
                Username = request.Username,
                Take = request.Limit,
                TenantId = tenantId
            });

            return Ok(
                response.Partners.Select(Map)
                    .ToArray()
                    .Paginate(request, Url, x => x.AffiliateId));
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpGet("{partnerId}")]
        [ProducesResponseType(typeof(PartnerModel), StatusCodes.Status200OK)]

        public async Task<ActionResult<PartnerModel>> GetAsync(
            [FromRoute, Required] long partnerId)
        {
            var response = await _partnerService.GetAsync(new PartnerGetRequest()
            {
                AffiliateId = partnerId
            });


            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(PartnerModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PartnerModel>> CreateAsync(
            [FromBody] PartnerCreateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _partnerService.CreateAsync(new Affiliate.Service.Grpc.Models.Partners.Messages.PartnerCreateRequest()
            {
                TenantId = tenantId,
                Bank = new Affiliate.Service.Grpc.Models.Partners.PartnerBank()
                {
                    AccountNumber = request.Bank.AccountNumber,
                    BankAddress = request.Bank.BankAddress,
                    BankName = request.Bank.BankName,
                    BeneficiaryAddress = request.Bank.BeneficiaryAddress,
                    BeneficiaryName = request.Bank.BeneficiaryName,
                    Iban = request.Bank.Iban,
                    Swift = request.Bank.Swift
                },
                Company = new Affiliate.Service.Grpc.Models.Partners.PartnerCompany()
                {
                    Address = request.Company.Address,
                    Name = request.Company.Name,
                    RegNumber = request.Company.RegNumber,
                    VatId = request.Company.VatId
                },
                GeneralInfo = new Affiliate.Service.Grpc.Models.Partners.PartnerGeneralInfo()
                {
                    CreatedAt = request.GeneralInfo.CreatedAt,
                    Currency = request.GeneralInfo.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Email = request.GeneralInfo.Email,
                    Password = request.GeneralInfo.Password,
                    Phone = request.GeneralInfo.Phone,
                    Role = request.GeneralInfo.Role.MapEnum<Affiliate.Service.Grpc.Models.Partners.PartnerRole>(),
                    Skype = request.GeneralInfo.Skype,
                    State = request.GeneralInfo.State.MapEnum<Affiliate.Service.Grpc.Models.Partners.PartnerState>(),
                    Username = request.GeneralInfo.Username,
                    ZipCode = request.GeneralInfo.ZipCode,
                    ApiKey = request.GeneralInfo.ApiKey
                },
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPut("{partnerId}")]
        [ProducesResponseType(typeof(PartnerModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<PartnerModel>> UpdateAsync(
            [Required, FromRoute] long partnerId,
            [FromBody] PartnerUpdateRequest request)
        {
            var tenantId = this.GetTenantId();
            var response = await _partnerService.UpdateAsync(new Affiliate.Service.Grpc.Models.Partners.Messages.PartnerUpdateRequest()
            {
                Sequence = request.Sequence,
                AffiliateId = partnerId,
                TenantId = tenantId,
                Bank = new Affiliate.Service.Grpc.Models.Partners.PartnerBank()
                {
                    AccountNumber = request.Bank.AccountNumber,
                    BankAddress = request.Bank.BankAddress,
                    BankName = request.Bank.BankName,
                    BeneficiaryAddress = request.Bank.BeneficiaryAddress,
                    BeneficiaryName = request.Bank.BeneficiaryName,
                    Iban = request.Bank.Iban,
                    Swift = request.Bank.Swift
                },
                Company = new Affiliate.Service.Grpc.Models.Partners.PartnerCompany()
                {
                    Address = request.Company.Address,
                    Name = request.Company.Name,
                    RegNumber = request.Company.RegNumber,
                    VatId = request.Company.VatId
                },
                GeneralInfo = new Affiliate.Service.Grpc.Models.Partners.PartnerGeneralInfo()
                {
                    CreatedAt = request.GeneralInfo.CreatedAt,
                    Currency = request.GeneralInfo.Currency.MapEnum<Affiliate.Service.Grpc.Models.Common.Currency>(),
                    Email = request.GeneralInfo.Email,
                    Password = request.GeneralInfo.Password,
                    Phone = request.GeneralInfo.Phone,
                    Role = request.GeneralInfo.Role.MapEnum<Affiliate.Service.Grpc.Models.Partners.PartnerRole>(),
                    Skype = request.GeneralInfo.Skype,
                    State = request.GeneralInfo.State.MapEnum<Affiliate.Service.Grpc.Models.Partners.PartnerState>(),
                    Username = request.GeneralInfo.Username,
                    ZipCode = request.GeneralInfo.ZipCode,
                    ApiKey = request.GeneralInfo.ApiKey
                },
            });

            return MapToResponse(response);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpDelete("{partnerId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAsync(
            [Required, FromRoute] long partnerId)
        {
            var response = await _partnerService.DeleteAsync(new Affiliate.Service.Grpc.Models.Partners.Messages.PartnerDeleteRequest()
            {
                AffiliateId = partnerId,
            });

            return MapToResponseEmpty(response);
        }

        private ActionResult MapToResponse(Affiliate.Service.Grpc.Models.Partners.PartnerResponse response)
        {
            if (response.Error != null)
            {
                ModelState.AddModelError("", response.Error.Message);

                return BadRequest(ModelState);
            }

            if (response.Partner == null)
                return NotFound();

            return Ok(new PartnerModel()
            {
                AffiliateId = response.Partner.AffiliateId,
                Bank = new PartnerBank()
                {
                    AccountNumber = response.Partner.Bank.AccountNumber,
                    BankAddress = response.Partner.Bank.BankAddress,
                    BankName = response.Partner.Bank.BankName,
                    BeneficiaryAddress = response.Partner.Bank.BeneficiaryAddress,
                    BeneficiaryName = response.Partner.Bank.BeneficiaryName,
                    Iban = response.Partner.Bank.Iban,
                    Swift = response.Partner.Bank.Swift
                },
                Company = new PartnerCompany()
                {
                    Address = response.Partner.Company.Address,
                    Name = response.Partner.Company.Name,
                    RegNumber = response.Partner.Company.RegNumber,
                    VatId = response.Partner.Company.VatId
                },
                GeneralInfo = new PartnerGeneralInfo()
                {
                    CreatedAt = response.Partner.GeneralInfo.CreatedAt,
                    Currency = response.Partner.GeneralInfo.Currency.MapEnum<Currency>(),
                    Email = response.Partner.GeneralInfo.Email,
                    Password = response.Partner.GeneralInfo.Password,
                    Phone = response.Partner.GeneralInfo.Phone,
                    Role = response.Partner.GeneralInfo.Role.MapEnum<PartnerRole>(),
                    Skype = response.Partner.GeneralInfo.Skype,
                    State = response.Partner.GeneralInfo.State.MapEnum<PartnerState>(),
                    Username = response.Partner.GeneralInfo.Username,
                    ZipCode = response.Partner.GeneralInfo.ZipCode,
                    ApiKey = response.Partner.GeneralInfo.ApiKey
                },
                Sequence = response.Partner.Sequence
            });
        }

        private static PartnerModel Map(Affiliate.Service.Grpc.Models.Partners.Partner partner)
        {
            return new PartnerModel()
            {
                AffiliateId = partner.AffiliateId,
                Bank = new PartnerBank()
                {
                    AccountNumber = partner.Bank.AccountNumber,
                    BankAddress = partner.Bank.BankAddress,
                    BankName = partner.Bank.BankName,
                    BeneficiaryAddress = partner.Bank.BeneficiaryAddress,
                    BeneficiaryName = partner.Bank.BeneficiaryName,
                    Iban = partner.Bank.Iban,
                    Swift = partner.Bank.Swift
                },
                Company = new PartnerCompany()
                {
                    Address = partner.Company.Address,
                    Name = partner.Company.Name,
                    RegNumber = partner.Company.RegNumber,
                    VatId = partner.Company.VatId
                },
                GeneralInfo = new PartnerGeneralInfo()
                {
                    CreatedAt = partner.GeneralInfo.CreatedAt,
                    Currency = partner.GeneralInfo.Currency.MapEnum<Currency>(),
                    Email = partner.GeneralInfo.Email,
                    Password = partner.GeneralInfo.Password,
                    Phone = partner.GeneralInfo.Phone,
                    Role = partner.GeneralInfo.Role.MapEnum<PartnerRole>(),
                    Skype = partner.GeneralInfo.Skype,
                    State = partner.GeneralInfo.State.MapEnum<PartnerState>(),
                    Username = partner.GeneralInfo.Username,
                    ZipCode = partner.GeneralInfo.ZipCode,
                    ApiKey = partner.GeneralInfo.ApiKey
                },
                Sequence = partner.Sequence
            };
        }

        private ActionResult MapToResponseEmpty(Affiliate.Service.Grpc.Models.Partners.PartnerResponse response)
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