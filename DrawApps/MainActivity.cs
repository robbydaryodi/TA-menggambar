using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DrawApps
{
    [Activity(Label = "DrawApps", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DrawCanvas draw = new DrawCanvas(this);
            draw.start();
            // Set our view from the "main" layout resource
            SetContentView(draw);           
        }
    }
}

