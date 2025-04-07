namespace SimpleCowApi.Helpers
{
    public static class IdGenerator
    {
        private static Random random = new Random();
        public static int GenerateRandomId() => random.Next(10000, 99999);
    }
}
