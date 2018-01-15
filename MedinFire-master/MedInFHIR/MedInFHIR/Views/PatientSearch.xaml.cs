using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedInFHIR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientSearch : ContentPage
    {
        public PatientSearch()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //PatientInfo pi = new PatientInfo(new FhirClient(new Uri(Meru_Client.App.FHIRapi)));

            //pi.InitialisePatient(edPatientID.Text);

            //if (!pi.isExceptionEncountered)
            //    await Navigation.PushAsync(new TabParent(pi));
            //else
            //    lblError.Text = pi.ExceptionIssues.First().Diagnostics;
        }
    }
}