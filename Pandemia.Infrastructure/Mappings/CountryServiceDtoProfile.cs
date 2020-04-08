using AutoMapper;
using Pandemia.Domain.ValueObjects;
using Pandemia.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Infrastructure.Mappings
{
  public class CountryServiceDtoProfile : Profile
  {
    public CountryServiceDtoProfile()
    {
      CreateMap<CountryServiceDto, Country>()
      .ReverseMap();
    }

  }
}
