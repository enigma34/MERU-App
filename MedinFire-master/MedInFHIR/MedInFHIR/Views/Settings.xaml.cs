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
    public partial class Settings : ContentPage
    {
        private string errorMessage = string.Empty;
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (MedInFHIR.App.userId.HasValue)
                {
                    welcomeText.Text = "Welcome "+ MedInFHIR.App.userName;
                    loginButon.Text="Logout";
                    profileButton.IsEnabled = true;
                    deviceButton.IsEnabled = true;
                    tokenButton.IsEnabled = true;
                    historyButton.IsEnabled = true;
                    patientButton.IsEnabled = true;
                }
                else
                {
                    welcomeText.Text = "Welcome to MERU App";
                    loginButon.Text = "Login";
                    profileButton.IsEnabled = false;
                    deviceButton.IsEnabled = false;
                    tokenButton.IsEnabled = false;
                    historyButton.IsEnabled = false;
                    patientButton.IsEnabled = false;
                }
            }
            catch (Exception e)
            {
                //System.Console.WriteLine(e);
                throw;
            }
        }

        async void OnLogin(object sender, EventArgs e)
        {
           // if (Meru_Client.App.userId.HasValue)
            //{
                await Navigation.PushAsync(new Login());
                //Meru_Client.App.userId = null;
           // }
        }

        async void OnProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfile());
        }

        async void OnDevice(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterDevice());
        }

        async void OnToken(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TokenGeneration());
        }

        async void OnHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new History());
        }

        async void OnPatient(object sender, EventArgs e)
        {
            var baseURL = "http://meruapi.meru.info/api/Fhir/AccessFHIR";
            if (MedInFHIR.App.token.Length>0)
            {
                var token = MedInFHIR.App.token;
                var result = GetFHIRendpoint(baseURL,token).Result;
                if (result!="Error")
                {
                    fhirModel bobj = JsonConvert.DeserializeObject<fhirModel>(result);
                    MedInFHIR.App.FHIRapi = bobj.fhirEndpoint;
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    lblError.Text = "Error" + errorMessage;
                }
            }
            //await Navigation.PushAsync(new ());
        }

        async Task<string> GetFHIRendpoint(string endpoint,string token)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", token);
                    HttpResponseMessage ResponseMessage = client.GetAsync(endpoint).Result;
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