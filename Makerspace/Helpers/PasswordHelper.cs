using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;

namespace Makerspace
{
	public class PasswordHelper
	{
		public PasswordHelper()
		{
		}

		public static async Task<bool> verifyAdminPassword(string password)
		{
			using (var client = new AmazonS3Client(new Authenticator().getCredentials(), Amazon.RegionEndpoint.USEast1))
			{
				using (var response = await client.GetObjectAsync("utdmakerspace", "adminpassword.txt"))
				{
					StreamReader reader = new StreamReader(response.ResponseStream);
					string text = reader.ReadToEnd();
					if(password.Equals(text))
					{
						return true;
					}
				}
			}

			return false;

		}

		public static async Task<bool> verifySTMPassword(string password)
		{
			using (var client = new AmazonS3Client(new Authenticator().getCredentials(), Amazon.RegionEndpoint.USEast1))
			{
				using (var response = await client.GetObjectAsync("utdmakerspace", "stmpassword.txt"))
				{
					StreamReader reader = new StreamReader(response.ResponseStream);
					string text = reader.ReadToEnd();
					if (password.Equals(text))
					{
						return true;
					}
				}
			}

			return false;

		}
	}
}

