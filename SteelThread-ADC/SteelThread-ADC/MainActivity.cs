using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Text;

namespace SteelThread_ADC
{
    [Activity(Label = "Austin Caldwell's SteelThread App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private EditText userText;
        private Button btnSendText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Find Text Field and Button
            userText = FindViewById<EditText>(Resource.Id.editTextUserInput);
            btnSendText = FindViewById<Button>(Resource.Id.btnSendText);
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Register Event Handlers
            btnSendText.Click += BtnSendText_OnClick;
        }

        protected override void OnStop()
        {
            // DeRegister Event Handlers
            btnSendText.Click -= BtnSendText_OnClick;

            base.OnStop();
        }

        private void BtnSendText_OnClick(object sender, EventArgs e)
        {
            // HTTP POST to Azure Function
            using (var client = new HttpClient())
            {
                var url = "https://austinproject.azurewebsites.net/api/Austin-SteelThread-HttpTrigger?code=KiQSAoH2CCFPlVPKzELQirn1eenI6oeWxrfPgjjN0sH3WUpJCoC0Yw==";
                var postBody = new StringContent("{name:'" + userText.Text + "'}", Encoding.UTF8, "application/json");

                var resultFromAzure = client.PostAsync(new Uri(url), postBody).Result.Content.ReadAsStringAsync().Result;

                var expectedResult = "\"Hello " + userText.Text + "\"";

                if (resultFromAzure == expectedResult)
                {
                    Toast.MakeText(this, "Success! Results matched.", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Failure! Results did not match.", ToastLength.Long).Show();
                }

                //client.PostAsync(new Uri(url), postBody).Result.EnsureSuccessStatusCode();
            }
        }
    }
}