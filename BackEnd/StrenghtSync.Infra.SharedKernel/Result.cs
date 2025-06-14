namespace StrenghtSync.Infra.SharedKernel
{
    public class Result<TFailure, TSuccess>
    {
        public TFailure Failure { get; internal set; }

        public TSuccess Success { get; internal set; }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        internal Result(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default;
        }

        internal Result(TSuccess success)
        {
            IsFailure = false;
            Failure = default;
            Success = success;
        }

        public static implicit operator Result<TFailure, TSuccess>(TFailure failure)
        {
            return new Result<TFailure, TSuccess>(failure);
        }

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success)
        {
            return new Result<TFailure, TSuccess>(success);
        }

        public TResult Match<TResult>(Func<TFailure, TResult> failure, Func<TSuccess, TResult> success)
        {
            return IsFailure ? failure(Failure) : success(Success);
        }

        public static Result<TFailure, TSuccess> Of(TSuccess obj) => obj;

        public static Result<TFailure, TSuccess> Of(TFailure obj) => obj;
    }

    // Métodos de extensão movidos para uma classe estática
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
