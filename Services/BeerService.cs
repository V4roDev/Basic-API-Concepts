using AutoMapper;
using Backend.Automappers;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
{
    private IRepository<Beer> _repository;
    private IMapper _mapper;
    
    public List<string> Errors { get; }
    
    public BeerService(IRepository<Beer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        Errors = new List<string>();
    }

    
    public async Task<IEnumerable<BeerDto>> Get()
    {
        var beers = await _repository.Get();

        return beers.Select(b => _mapper.Map<BeerDto>(b));
    }
    
    public async Task<BeerDto> GetById(int id)
    {
        var beer = await _repository.GetById(id);

        if (beer != null)
        {
            return _mapper.Map<BeerDto>(beer);
        }
        
        return (null)!;
    }
    
    public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
    {
        var beer = _mapper.Map<Beer>(beerInsertDto);

        await _repository.Add(beer);
        await _repository.Save();

        return _mapper.Map<BeerDto>(beer);
    }
    
    public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _repository.GetById(id);

            if (beer != null)
            {
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);
                
                _repository.Update(beer);
                await _repository.Save();

                return _mapper.Map<BeerDto>(beer);
            }

            return (null)!;
        }
    
    public async Task<BeerDto> Delete(int id)
    {
        var beer = await _repository.GetById(id);

        if (beer != null)
        {
            _repository.Delete(beer);
            await _repository.Save();

            return _mapper.Map<BeerDto>(beer);
        }

        return (null)!;
    }

    
    public bool Validate(BeerInsertDto dto)
    {
        if (_repository.Search(b => b.Name == dto.Name).Any())
        {
            Errors.Add("No pueden existir dos cervezas con el mismo nombre");
            return false;
        }

        return true;
    }

    public bool Validate(BeerUpdateDto dto)
    {
        if (_repository.Search(b => b.Name == dto.Name && b.Id != dto.Id).Any())
        {
            Errors.Add("No pueden existir dos cervezas con el mismo nombre");
            return false;
        }
        
        return true;
    }
}