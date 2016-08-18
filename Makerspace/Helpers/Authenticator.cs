using System;
using Amazon;
using Amazon.CognitoIdentity;

namespace Makerspace
{
	public class Authenticator
	{
		public Authenticator()
		{
		}

		public CognitoAWSCredentials getCredentials()
		{
			var credentials = new CognitoAWSCredentials(
				Keys.AWSCognito,
				RegionEndpoint.USEast1);

			return credentials;
		}
	}
}

