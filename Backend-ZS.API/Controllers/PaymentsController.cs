using AutoMapper;
using Backend_ZS.API.Data;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;
        private readonly ZsDbContext dbContext;
        public PaymentsController(IPaymentRepository paymentRepository, IMapper mapper, ZsDbContext dbContext)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var payments = await paymentRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var paymentsDto = mapper.Map<List<PaymentDto>>(payments);

            return Ok(paymentsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var paymentDomainModel = await paymentRepository.GetByIdAsync(id);

            if (paymentDomainModel == null)
            {
                return NotFound();
            }

            // 2 DTO
            var paymentDto = mapper.Map<PaymentDto>(paymentDomainModel);

            return Ok(paymentDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentRequestDto paymentRequestDto)
        {
            // Validate TransactionId exists
            var transactionExists = await dbContext.Transactions.AnyAsync(t => t.Id == paymentRequestDto.TransactionId);
            if (!transactionExists)
            {
                return BadRequest("La transacción especificada no existe.");
            }

            // Map RequestDto to Domain Model
            var paymentDomainModel = mapper.Map<Payment>(paymentRequestDto);

            // Create
            paymentDomainModel = await paymentRepository.AddAsync(paymentDomainModel);

            // Map Domain Model to Dto
            var paymentDto = mapper.Map<PaymentDto>(paymentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = paymentDto.Id }, paymentDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PaymentRequestDto paymentRequestDto)
        {
            // Map RequestDto to Domain Model
            var paymentDomainModel = mapper.Map<Payment>(paymentRequestDto);

            paymentDomainModel = await paymentRepository.UpdateAsync(id, paymentDomainModel);

            if (paymentDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var paymentDto = mapper.Map<PaymentDto>(paymentDomainModel);

            return Ok(paymentDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedpayment = await paymentRepository.DeleteAsync(id);

            if (deletedpayment == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var paymentDto = mapper.Map<PaymentDto>(deletedpayment);

            // return DTO to payment
            return Ok(paymentDto);
        }
    }
}
