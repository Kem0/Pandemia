using System;
using System.Collections.Generic;
using System.Text;

namespace Pandemia.Application.SeedWork
{

  public class CommandResult<T>
  {
    public T ResultObject { get; set; }

    public List<Object> Errors { get; set; } = new List<Object>();

    public CommandResultStatus Status
    {
      get { return Errors.Count == 0 ? CommandResultStatus.Success : CommandResultStatus.Error; }
    }

  }

}
