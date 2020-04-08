using AutoMapper;
using Pandemia.Domain.ValueObjects;
using Pandemia.Persistance.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Persistance.Mappings
{
  public class CountryModelProfile : Profile
  {
    public CountryModelProfile()
    {
      CreateMap<Country, CountryModel>()
      .ReverseMap();
    }
  }
}
