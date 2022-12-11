namespace EspamaGS_2._0.Services {
    public static class GameKeyGenerator {
        public static string GenerateKey() {
            // Generate a random key
            string key = Guid.NewGuid().ToString().Substring(0, 18).ToUpper();

            // Return the key
            return key;
        }

    }
}
