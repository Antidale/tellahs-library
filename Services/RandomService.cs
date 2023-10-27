namespace tellahs_library.Services
{
    public class RandomService
    {
        private readonly Random _random = new();

        public int Next() => _random.Next();
        public int Next(int count) => _random.Next(count);
    }
}
