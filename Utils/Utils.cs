using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PIN_izračun.Models;

namespace PIN_izračun.Utils
{
    public static class Utils
    {
        public static string GenerateHashPassword(string password)
        {
            // pokušaj šifriranja passworda (uspjeva šifrirat, ali ne želi provjerit)
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public static void CalculateAllTaxes(User user, float TaxReduction, float TaxCity)
        {

        }

        public static decimal CheckNumberByCity(int cityId)
        {
            decimal number = 0;

            switch (cityId)
            {
                case (int)City.Zagreb:
                    number = 0.18m;
                    break;
                case (int)City.VelikaGorica:
                    number = 0.12m;
                    break;
                case (int)City.SVNedelja:
                    number = 0;
                    break;
            }

            return number;
        }

        public static decimal CheckNumberTaxReduction(int taxId)
        {
            decimal number = 0;

            switch (taxId)
            {
                case (int)TaxtReduction.Nijedno:
                    number = 4000;
                    break;
                case (int)TaxtReduction.Jedno:
                    number = 5750;
                    break;
                case (int)TaxtReduction.Dvoje:
                    number = 6500;
                    break;
                case (int)TaxtReduction.Troje:
                    number = 7500;
                    break;
            }

            return number;
        }
    }
}
