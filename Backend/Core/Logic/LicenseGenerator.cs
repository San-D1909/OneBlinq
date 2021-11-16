using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Core.Logic
{
	public class LicenseGenerator
	{
		private IConfiguration _config;

		public LicenseGenerator(IConfiguration config) 
		{
			_config = config;
		}

		public string CreateLicenseKey(string email, string plugin, string variant)
		{
            var concat = email + plugin + variant + _config["Secret"] + Convert.ToString(DateTime.Now);
			
            //TODO: Add time to md5 hash to always generate unique keys

			MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(concat));

            var sBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				sBuilder.Append(result[i].ToString("x2"));
			}

			var formattedKey = FormatLicenseKey(sBuilder.ToString());

            return formattedKey;
		}

		private static string FormatLicenseKey(string productIdentifier)
        {
            // productIdentifier = productIdentifier.Substring(0, 28).ToUpper();
			productIdentifier = productIdentifier.ToUpper();
            char[] serialArray = productIdentifier.ToCharArray();
            StringBuilder licenseKey = new StringBuilder();

            int j = 0;
            for (int i = 0; i < 28; i++)
            {
                for (j = i; j < 4 + i; j++)
                {
                    licenseKey.Append(serialArray[j]);
                }
                if (j == 28)
                {
                    break;
                }
                else
                {
                    i = (j) - 1;
                    licenseKey.Append("-");
                }
            }
            return licenseKey.ToString();
        }
	}
}
