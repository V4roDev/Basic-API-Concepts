using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BeerInsertDto, Beer>();
        CreateMap<BeerUpdateDto, Beer>();
        CreateMap<Beer, BeerDto>();
    }
}