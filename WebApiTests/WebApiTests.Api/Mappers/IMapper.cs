using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Api.Mappers
{
    public interface IMapper<TSource, TDestination> 
        where TSource : class 
        where TDestination : class
    {
        TDestination Map(TSource value);

        TSource Map(TDestination value, TSource source = null);
    }
}
