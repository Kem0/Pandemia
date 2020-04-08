using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandemia.Application.Repositories
{
  public interface IRepository<T> where T : class, new()
  {
    Task<T> InsertOrUpdateAsync(T record);

    Task InsertManyAsync(IEnumerable<T> records);

    Task DropAsync();
  }
}
