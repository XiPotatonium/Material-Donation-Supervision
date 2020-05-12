using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public interface IReturn
    {
    }

    public interface IReturn<TReturn> : IReturn
    {
    }
}
