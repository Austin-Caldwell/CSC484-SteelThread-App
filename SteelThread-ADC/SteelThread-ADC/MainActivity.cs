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
        //private EditText blobName;
        private EditText userText;
        private Button btnSendText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Find Text Field and Button
            //blobName = FindViewById<EditText>(Resource.Id.editTextBlobName);        // Allow user to specify a unique name for their blob
            userText = FindViewById<EditText>(Resource.Id.editTextUserInput);       // Allow user to enter text to store in the blob
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
            Toast.MakeText(this, "Sending text to Azure Function....", ToastLength.Long).Show();
            try
            {
                // HTTP POST to Azure Function
                using (var client = new HttpClient())
                {
                    var url = "https://austinproject.azurewebsites.net/api/Austin-SteelThread-HttpTrigger?code=r69oimkQXL54Emdp1WEaDu0/7CEZC0IP5dXMYm5ElpaF5xfuTBetow==";
                    //var postBody = new StringContent("{blobName:'" + blobName.Text + "',\nuserInput:'" + userText.Text + "'}", Encoding.UTF8, "application/json");
                    var postBody = new StringContent("{userInput:'" + userText.Text + "'}", Encoding.UTF8, "application/json");

                    client.PostAsync(new Uri(url), postBody).Result.EnsureSuccessStatusCode();  // Send the HTTP POST to Azure Functions.  A blob will be created.  It will contain the user-supplied text.
                }

                Toast.MakeText(this, "Text sent to Azure Function.", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error! " + ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}