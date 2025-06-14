using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthSync.Infra.SharedKernel.Helpers
{
    public static class ResultExtensions
    {
        public static Result<Exception, TSuccess> Run<TSuccess>(this Func<TSuccess> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Result<Exception, TSuccess> Run<TSuccess>(this Exception ex)
        {
            return ex;
        }

        public static Result<Exception, IQueryable<TSuccess>> AsResult<TSuccess>(this IEnumerable<TSuccess> source)
        {
            return Run(() => source.AsQueryable());
        }

        public static async Task<Result<Exception, TSuccess>> Run<TSuccess>(Func<Task<TSuccess>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
