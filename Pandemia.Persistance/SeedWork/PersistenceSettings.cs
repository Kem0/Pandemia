﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Persistance.SeedWork
{
  public class PersistenceSettings
  {
    public MongoDbSettings MongoDbSettings { get; set;}
    public string CSSEGISandDataFolderPath { get; set; }
  }
}
