using System.ComponentModel.DataAnnotations.Schema;
using TeiasProxy.Helpers;

namespace TeiasProxy.Models
{
    public class PlantCredentials
    {
        public int Id { get; set; }

        public string PlantName { get; set; }

        public string EncryptedUsername { get; set; }
        public string EncryptedPassword { get; set; }

        [NotMapped]
        public string Username => EncryptionHelper.Decrypt(EncryptedUsername);

        [NotMapped]
        public string Password => EncryptionHelper.Decrypt(EncryptedPassword);
    }
}
