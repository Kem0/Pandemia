using AutoMapper;
using Pandemia.Domain.ValueObjects;
using Pandemia.Persistance.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Persistance.Mappings
{
  public class CountryProfile : Profile
  {
    public CountryProfile()
    {
      CreateMap<Country, CountryModel>()
      .ReverseMap();
    }
  }
}
