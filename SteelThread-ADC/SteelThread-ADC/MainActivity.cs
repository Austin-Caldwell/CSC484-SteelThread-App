using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;

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
            // Register Event Handlers
            btnSendText.Click += BtnSendText_OnClick;
        }

        protected override void OnStop()
        {
            // DeRegister Event Handlers
            btnSendText.Click -= BtnSendText_OnClick;
        }

        private void BtnSendText_OnClick(object sender, EventArgs e)
        {
            // Include HTTP POST to Azure Function Here
        }
    }
}

