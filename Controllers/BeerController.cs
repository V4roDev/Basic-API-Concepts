using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private ICommonService<BeerDto, BeerInsertDto,BeerUpdateDto> _service;


        public BeerController(IValidator<BeerInsertDto> beerValidator,
            ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> service)
        {
            _beerInsertValidator = beerValidator;
            _service = service;
        }


        //Solicitudes
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _service.Get();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id) 
        {
            var response = await _service.GetById(id);

            return response == null ? NotFound() : Ok(response);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(beerInsertDto))
            {
                return BadRequest(_service.Errors);
            }
            
            var response = await _service.Add(beerInsertDto);
                
            return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
        }
        
        
        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>>Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var response = await _service.Update(id, beerUpdateDto);

            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var response = await _service.Delete(id);

            return response == null ? NotFound() : Ok(response);
        }
        

        
    }
}
