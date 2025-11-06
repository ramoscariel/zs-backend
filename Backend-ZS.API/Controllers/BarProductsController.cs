using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;
using Backend_ZS.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ZS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarProductsController : ControllerBase
    {
        private readonly IBarProductRepository barProductRepository;
        private readonly IMapper mapper;
        public BarProductsController(IBarProductRepository barProductRepository, IMapper mapper)
        {
            this.barProductRepository = barProductRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Domain Models
            var products = await barProductRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var productsDto = mapper.Map<List<BarProductDto>>(products);

            return Ok(productsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Domain Model
            var barProductDomainModel = await barProductRepository.GetByIdAsync(id);

            if (barProductDomainModel == null)
            {
                return NotFound();
            }

            // 2 DTO
            var barProductDto = mapper.Map<BarProductDto>(barProductDomainModel);

            return Ok(barProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarProductRequestDto barProductRequestDto)
        {
            // Map RequestDto to Domain Model
            var barProductDomainModel = mapper.Map<BarProduct>(barProductRequestDto);

            // Create
            barProductDomainModel = await barProductRepository.AddAsync(barProductDomainModel);

            // Map Domain Model to Dto
            var barProductDto = mapper.Map<BarProductDto>(barProductDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = barProductDto.Id }, barProductDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BarProductRequestDto barProductRequestDto)
        {
            // Map RequestDto to Domain Model
            var barProductDomainModel = mapper.Map<BarProduct>(barProductRequestDto);

            barProductDomainModel = await barProductRepository.UpdateAsync(id, barProductDomainModel);

            if (barProductDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var barProductDto = mapper.Map<BarProductDto>(barProductDomainModel);

            return Ok(barProductDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete Resource
            var deletedbarProduct = await barProductRepository.DeleteAsync(id);

            if (deletedbarProduct == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var barProductDto = mapper.Map<BarProductDto>(deletedbarProduct);

            // return DTO to barProduct
            return Ok(barProductDto);
        }
    }
}
