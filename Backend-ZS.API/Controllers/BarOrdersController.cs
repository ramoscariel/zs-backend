using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Backend_ZS.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarOrdersController : ControllerBase
    {
        private readonly IBarOrderRepository barOrderRepository;
        private readonly IBarOrderService barOrderService;
        private readonly IMapper mapper;
        public BarOrdersController(IBarOrderRepository barOrderRepository, IBarOrderService barOrderService, IMapper mapper)
        {
            this.barOrderRepository = barOrderRepository;
            this.barOrderService = barOrderService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var barOrders = await barOrderRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var barOrdersDto = mapper.Map<List<BarOrderDto>>(barOrders);

            return Ok(barOrdersDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var barOrderDomainModel = await barOrderRepository.GetByIdAsync(id);

            if (barOrderDomainModel == null)
            {
                return NotFound();
            }

            // 2 DTO
            var barOrderDto = mapper.Map<BarOrderDto>(barOrderDomainModel);

            return Ok(barOrderDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarOrderRequestDto barOrderRequestDto)
        {
            // Map RequestDto to Domain Model
            var barOrderDomainModel = mapper.Map<BarOrder>(barOrderRequestDto);

            // Create a new BarOrder
            barOrderDomainModel = await barOrderRepository.AddAsync(barOrderDomainModel);

            // Map Domain Model to Dto
            var barOrderDto = mapper.Map<BarOrderDto>(barOrderDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = barOrderDto.Id }, barOrderDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedbarOrder = await barOrderRepository.DeleteAsync(id);

            if (deletedbarOrder == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var barOrderDto = mapper.Map<BarOrderDto>(deletedbarOrder);

            // return DTO to barOrder
            return Ok(barOrderDto);
        }


        // BarOrderDetail


        [HttpGet]
        [Route("{id:guid}/details/{barProductId:guid}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id, [FromRoute] Guid barProductId)
        {
            // Get Domain Model

            var detail = await barOrderRepository.GetDetailAsync(id, barProductId);
            if(detail == null)
            {
                return NotFound();
            }

            // 2 DTO
            var barOrderDetailDto = mapper.Map<BarOrderDetailDto>(detail);

            return Ok(barOrderDetailDto);
        }

        [HttpPost]
        [Route("{id:guid}/details")]
        public async Task<IActionResult> AddDetail([FromRoute] Guid id, [FromBody] BarOrderDetailCreateRequestDto barOrderDetailCreateRequestDto)
        {
            // Map RequestDto to Domain Model
            var barOrderDetailDomainModel = mapper.Map<BarOrderDetail>(barOrderDetailCreateRequestDto);

            // Add BarOrderId
            barOrderDetailDomainModel.BarOrderId = id;

            // Use service (handles stock validation, product qty update, total recalculation)
            barOrderDetailDomainModel = await barOrderService.AddDetailAsync(barOrderDetailDomainModel);

            // Map Domain Model to Dto
            var barOrderId = barOrderDetailDomainModel.BarOrderId;
            var barOrderDetailDto = mapper.Map<BarOrderDetailDto>(barOrderDetailDomainModel);

            return CreatedAtAction(nameof(GetDetail),
                new
                {
                    id = barOrderId,
                    barProductId = barOrderDetailDto.BarProduct.Id
                }, barOrderDetailDto);
        }

        [HttpPut]
        [Route("{id:guid}/details/{barProductId:guid}")]
        public async Task<IActionResult> UpdateDetail([FromRoute] Guid id, [FromRoute] Guid barProductId, [FromBody] BarOrderDetailUpdateRequestDto barOrderDetailUpdateRequestDto)
        {
            // Map RequestDto to Domain Model
            var barOrderDetailDomainModel = mapper.Map<BarOrderDetail>(barOrderDetailUpdateRequestDto);

            // Ensure the route values are applied so the service can find the existing detail
            barOrderDetailDomainModel.BarOrderId = id;
            barOrderDetailDomainModel.BarProductId = barProductId;

            barOrderDetailDomainModel = await barOrderService.UpdateDetailAsync(barOrderDetailDomainModel);

            if (barOrderDetailDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var barOrderDetailDto = mapper.Map<BarOrderDetailDto>(barOrderDetailDomainModel);

            return Ok(barOrderDetailDto);
        }

        [HttpDelete]
        [Route("{id:guid}/details/{barProductId:guid}")]
        public async Task<IActionResult> DeleteDetail([FromRoute] Guid id, [FromRoute] Guid barProductId)
        {
            // Delete Resource (service will restore product qty and update order total)
            var deletedBarOrderDetail = await barOrderService.DeleteDetailAsync(id, barProductId);

            if (deletedBarOrderDetail == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var barOrderDetailDto = mapper.Map<BarOrderDetailDto>(deletedBarOrderDetail);

            // return DTO to barOrder
            return Ok(barOrderDetailDto);

        }
    }
}
