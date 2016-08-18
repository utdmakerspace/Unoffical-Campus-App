using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Makerspace
{
	public class SNSUtils
	{

		public enum Platform
		{
			Android,
			IOS,
			WindowsPhone
		}


		private static IAmazonSimpleNotificationService _snsClient;

		private static IAmazonSimpleNotificationService SnsClient
		{
			get
			{
				if (_snsClient == null)
					_snsClient = new AmazonSimpleNotificationServiceClient(new Authenticator().getCredentials());
				return _snsClient;
			}
		}

		public static async Task RegisterDevice(Platform platform, string registrationId)
		{
			var arn = string.Empty;
			string _endpointArn = string.Empty;
			switch (platform)
			{
				case Platform.Android:
					arn = Keys.GCMEndpointARN;
					break;
				case Platform.IOS:
					arn = Keys.APNSEndpointARN;
					break;
			}

			var response = await SnsClient.CreatePlatformEndpointAsync(new CreatePlatformEndpointRequest
			{
				Token = registrationId,
				PlatformApplicationArn = arn
			}
			);

			_endpointArn = response.EndpointArn;

		}

	}
}