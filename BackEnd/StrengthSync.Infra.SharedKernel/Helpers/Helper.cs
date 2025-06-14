using Option = StrengthSync.Infra.SharedKernel.Helpers.Option<object>;

namespace StrengthSync.Infra.SharedKernel.Helpers
{
    public static class Helper
    {
        public static readonly NoneType None;

        private static readonly Unit unit;

        public static Option<T> Some<T>(T value)
        {
            return Option.Of(value);
        }

        public static Unit Unit()
        {
            return unit;
        }

        public static Func<T, Unit> ToFunc<T>(Action<T> action)
        {
            return delegate (T o)
            {
                action(o);
                return Unit();
            };
        }

        public static Func<Unit> ToFunc(Action action)
        {
            return delegate
            {
                action();
                return Unit();
            };
        }
    }
}
