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
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                var baseURL = "http://meruapi.meru.info/api/UserApi/UserProfile?UserId="+ MedInFHIR.App.userId;
                var result = GetUserProfile(baseURL).Result;
                UserProfileModel bobj = JsonConvert.DeserializeObject<UserProfileModel>(result);
                fName.Text = bobj.FirstName;
                lName.Text = bobj.LastName;
                email.Text = bobj.EmailId;
                priContactNumb.Text = bobj.ContactNoPrimary.ToString();
                secContactNumb.Text = bobj.ContactNoSecondary.ToString();
            }
            catch (Exception e)
            {
                //System.Console.WriteLine(e);
                throw;
            }
        }

        async Task<string> GetUserProfile(string endpoint)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    HttpResponseMessage ResponseMessage =  client.GetAsync(endpoint).Result;
                    if (ResponseMessage.IsSuccessStatusCode)
                    {
                        var convertedSentence = ResponseMessage.Content.ReadAsStringAsync().Result;
                        result = convertedSentence;
                    }
                    else
                    {
                        result = "Error";
                    }
                    return result;
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    throw;
                }

            }
        }
    }
}