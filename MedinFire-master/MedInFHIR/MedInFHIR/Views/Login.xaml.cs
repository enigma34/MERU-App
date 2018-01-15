using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedInFHIR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (MedInFHIR.App.userId.HasValue)
            {
                MedInFHIR.App.userId = null;
            }
        }

        async void OnLogin(object sender, EventArgs e)
        {
            int returnUserId;
            //errorLable.IsEnabled = false;
           
            //else
            //{
                if (Username.Text.Length > 0 && Password.Text.Length > 0)
                {
                    //Models.Login lm = new Models.Login
                    //{
                    //    UserName = Username.Text,
                    //    Password = Password.Text
                    //};
                    var ApiBaseUrl = "http://meruapi.meru.info/api/Login";
                    var usrnme = Username.Text;
                    var pwd = Password.Text;
                    var result = loginUser(ApiBaseUrl, usrnme, pwd).Result;
                    if (int.TryParse(result, out returnUserId))
                    {
                        MedInFHIR.App.userId = returnUserId;
                        MedInFHIR.App.userName = usrnme;
                        await Navigation.PopAsync();
                    }
                    else
                    {
                    // lblError.IsEnabled = true;
                        lblError.Text = result;
                    }

                }
                else
                {
                //errorLable.IsEnabled = true;
                    lblError.Text = "Error: Check username or password";
                }
            //}
        }

            void OnCancel(object sender, EventArgs e)
            {
                Username.Text = "";
                Password.Text = "";
            }

            async Task<string> loginUser(string endpoint, string username, string password)
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("POST"), endpoint);
                //message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //message.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                // List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>();
                var parameters = new Dictionary<string, string> {{"UserName", username}, {"Password", password}};
                //nameValueCollection.Add(new KeyValuePair<string, string>("display_name", displayName));
                message.Content = new FormUrlEncodedContent(parameters);
                try
                {
                    HttpResponseMessage httpResponseMessage = client.SendAsync(message).Result;
                    httpResponseMessage.EnsureSuccessStatusCode();
                    HttpContent httpContent = httpResponseMessage.Content;
                    string responseString = httpContent.ReadAsStringAsync().Result;
                    return responseString;
                }
                catch (Exception ex)
                {
                    string errorType = ex.GetType().ToString();
                    string errorMessage = errorType + ": " + ex.Message;
                    throw new Exception(errorMessage, ex.InnerException);
                }
            }
        
    }
}