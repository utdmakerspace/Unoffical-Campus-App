using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Makerspace
{
	public class EventsWriterHelper
	{
		public EventsWriterHelper()
		{
			
		}

		public async static Task<string> getEventsList()
		{
			using (var client = new AmazonS3Client(new Authenticator().getCredentials(),Amazon.RegionEndpoint.USEast1))
			{
				using (var response = await client.GetObjectAsync("utdmakerspace", "events.json"))
				{
					StreamReader reader = new StreamReader(response.ResponseStream);
					string text = reader.ReadToEnd();
					oldImage = text;
					return text;
				}
			}

		}

		//use these to monitor and update the events
		public static string oldImage { get; set;}
		public static string newImage { get; set; }

		public static async Task pushChanges()
		{
			using (var client = new AmazonS3Client(new Authenticator().getCredentials(), Amazon.RegionEndpoint.USEast1))
			{
				using (Stream s = GenerateStreamFromString(newImage))
				{
					var updatedFile = new PutObjectRequest()
					{
						BucketName = "utdmakerspace",
						Key = "events.json",
						InputStream = s,
						CannedACL = S3CannedACL.PublicRead
					};

					var response = await client.PutObjectAsync(updatedFile);
			}

			}
		}

		public static Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}



	}
}

