using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MedInFHIR.Models;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedInFHIR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterDevice : ContentPage
    {
        private string errorMessage = string.Empty; 
        public RegisterDevice()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (MedInFHIR.App.clientId.Length>0 && MedInFHIR.App.clientSecret.Length>0)
            {
                clientId.Text = MedInFHIR.App.clientId;
                clientSecret.Text = MedInFHIR.App.clientSecret;
            }
        }

        private void RegisterDevice_OnClicked(object sender, EventArgs e)
        {
            var currentDeviceId = CrossDeviceInfo.Current.Id;
            var deviceModel = CrossDeviceInfo.Current.Model;
            var devicePlatform = CrossDeviceInfo.Current.Platform;
            var devieVersion = CrossDeviceInfo.Current.Version;

            var baseURL = "http://meruapi.meru.info/api/DeviceApi/RegisterDevice?UserId="+ MedInFHIR.App.userId+"&DeviceId="+currentDeviceId;

            var reesult = RegisterDeviceApi(baseURL).Result;
            if (reesult != "Error")
            {
                RegisterDeviceModel bsObj = JsonConvert.DeserializeObject<RegisterDeviceModel>(reesult);


                MedInFHIR.App.clientId= bsObj.ClientId;
                MedInFHIR.App.clientSecret = bsObj.ClientSecret;
                MedInFHIR.App.deviceId = currentDeviceId;

                clientId.Text = bsObj.ClientId;
                clientSecret.Text = bsObj.ClientSecret;
            }
            else
            {
                lblError.Text = "Error"+ errorMessage;
            }

            var deviceInfo = "Device Id: " + currentDeviceId + "\n" +"Device Model: " +deviceModel + "\n" + "Device platform: " +devicePlatform + "\n" +"Device Version: " +devieVersion;
            deviceDetails.Text = deviceInfo;
        }

        private async Task<string> RegisterDeviceApi(string endpoint)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("POST"), endpoint);
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