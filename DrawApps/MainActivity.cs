using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace DrawApps
{
    [Activity(Label = "DrawApps", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            DrawCanvas draw = new DrawCanvas(this);
            draw.SetBackgroundColor(Color.White);
            draw.start();
            //Set our view from the "main" layout resource
            //SetContentView(draw);
        }
    }
}

