namespace Abyss.Infrastructure
{
    public struct Maybe<T>
    {
        public bool HasValue => Value != null;
        public T Value { get; private set; }
        public Maybe(T value) => Value = value;
        public static implicit operator Maybe<T>(T value) =>
            new Maybe<T>(value);
    }
}
