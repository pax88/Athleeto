using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FacebookLogin.Models;
using FacebookLogin.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
namespace FacebookLogin.ViewModels
{
    public class FacebookViewModel : INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
           // var facebookServices = new FacebookServices();


			var requestUrl2 =
				"https://graph.facebook.com/v2.7/me?fields=name,picture.type(large),age_range&access_token="
				+ accessToken;
			//"https://graph.facebook.com/v2.7/me?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
			//"https://graph.facebook.com/v2.7/me?fields=name,picture&access_token=EAASsWWQcRbcBAFZAhPz61EHMJaL04R7cOA2vU5H6hnLu34b83heQYFYKoAcufBZAly5XUD3muv3nlNDLvnlMvt9VpVA1ZCit20vQE7HgBBRH2ZBoQvUzofXS4EZAGfoauZAuyUkCCKmYQZBaypPFUKKnwHRv1E09h1zySXSSr0IY6PLYRtHEZCHX"

			var httpClient = new HttpClient();
			System.Diagnostics.Debug.WriteLine(requestUrl2);

			var userJson = await httpClient.GetStringAsync(requestUrl2);

			var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);
			FacebookProfile = facebookProfile;
	
			//Database.AddAthlete(facebookProfile.Id, facebookProfile.Name,0,0,false);

          //  FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
