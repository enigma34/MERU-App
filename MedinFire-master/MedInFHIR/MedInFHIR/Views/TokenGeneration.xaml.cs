using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MedInFHIR.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedInFHIR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenGeneration : ContentPage
    {
        private string errorMessage = string.Empty;
        public TokenGeneration()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (MedInFHIR.App.token.Length>0)
            {
                token.Text = MedInFHIR.App.token;
            }
        }

        private void OnToken(object sender, EventArgs e)
        {
            if (MedInFHIR.App.clientId.Length > 0 && MedInFHIR.App.clientSecret.Length > 0 && MedInFHIR.App.deviceId.Length > 0)
            {
                var baseURL = "http://meruapi.meru.info/api/DeviceApi/GenerateToken";
                var uid = MedInFHIR.App.userId;
                var cleintId = MedInFHIR.App.clientId;
                var clientSecret = MedInFHIR.App.clientSecret;
                var deviceId = MedInFHIR.App.deviceId;
                var result = GenerateToken(baseURL, uid, cleintId, clientSecret, deviceId).Result;
                if (result!="Error")
                {
                    TokenModel bobj = JsonConvert.DeserializeObject<TokenModel>(result);
                    MedInFHIR.App.token = bobj.TokenKey;
                    token.Text = bobj.TokenKey;

                }
                else
                {
                    lblError.Text = "Error" + errorMessage;
                }
            }
        }

        async Task<string> GenerateToken( string endpoint, int? uid, string clientID, string clientSecret, string deviceId)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, endpoint);
                    var parameters = new Dictionary<string, string> { { "UserId", uid.ToString() },{ "ClientId", clientID }, { "ClientSecret", clientSecret }, { "DeviceId", deviceId } };
                    message.Content = new FormUrlEncodedContent(parameters);

                    HttpResponseMessage ResponseMessage = client.SendAsync(message).Result;
                    if (ResponseMessage.IsSuccessStatusCode)
                    {
                        var convertedSentence = ResponseMessage.Content.ReadAsStringAsync().Result;
                        result = convertedSentence;
                    }
                    else
                    {
                        errorMessage = ResponseMessage.StatusCode.ToString();
                        result = "Error";
                    }
                    return result;
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    //Console.WriteLine(e);
                    throw;
                }

            }
        }
    }
}