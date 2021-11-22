using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Core.Logic
{
	public class PasswordEncrypter
	{

		private IConfiguration _config;

		public PasswordEncrypter(IConfiguration config)
		{
			_config = config;
		}

		public string EncryptPassword(string password)
		{

			string input = password + _config["Secret"];

			MD5 md5Hasher = MD5.Create();

			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

			StringBuilder sBuilder = new StringBuilder(); 

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();

		}

	}
}
