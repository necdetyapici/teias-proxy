using TeiasProxy.Data;
using TeiasProxy.Models;

namespace TeiasProxy.Helpers
{
    public static class DummyDataSeeder
    {
        public static void SeedIfNeeded(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProxyDbContext>();

            // 1. FirmCredentials seed
            if (!db.LagosCredentials.Any())
            {
                db.LagosCredentials.Add(new LagosCredentials
                {
                    EncryptedUsername = EncryptionHelper.Encrypt("lagos"),
                    EncryptedPassword = EncryptionHelper.Encrypt("123456")
                });
            }

            // 2. PlantCredentials seed
            if (!db.PlantCredentials.Any())
            {
                db.PlantCredentials.AddRange(
                    new PlantCredentials
                    {
                        PlantName = "FATMA RES",
                        EncryptedUsername = EncryptionHelper.Encrypt("teias_user_fatma"),
                        EncryptedPassword = EncryptionHelper.Encrypt("teias_pass_fatma")
                    },
                    new PlantCredentials
                    {
                        PlantName = "ULU RES",
                        EncryptedUsername = EncryptionHelper.Encrypt("teias_user_ulu"),
                        EncryptedPassword = EncryptionHelper.Encrypt("teias_pass_ulu")
                    }
                );
            }

            db.SaveChanges();
        }
    }
}
