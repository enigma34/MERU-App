using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using MedInFHIR.Views;

namespace MedInFHIR
{
    public partial class App : Application
    {
        public bool logedIn = false;

        public static string deviceId = String.Empty;
        //public string deviceId=String.Empty;
        //public string token=String.Empty;
        public DateTime TokenExpiratioDateTime;
        public bool userActive = false;
        public bool profileUpdated = false;
        public bool deviceRegistered = false;
        public bool tokenExists = false;

        public static int? userId = null;
        public static string userName = String.Empty;
        public static string clientId = String.Empty;
        public static string clientSecret = String.Empty;
        public static string token = String.Empty;
        public static string FHIRapi = String.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Settings());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
