using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FacebookLogin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace FacebookLogin.Services
{
    public class FacebookServices
    {

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl2 =
                "https://graph.facebook.com/v2.7/me?fields=name,picture&access_token="
                + accessToken;

            var httpClient = new HttpClient();
			var userJson = await httpClient.GetStringAsync(requestUrl2);

			var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

			return facebookProfile;
        }
    }
}
