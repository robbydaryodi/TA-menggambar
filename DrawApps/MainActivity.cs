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

            ToggleButton btn = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            btn.Click += (o, e) =>
            {
                if (btn.Checked)
                {

                    DrawCanvas draw = new DrawCanvas(this);
                    draw.SetBackgroundColor(Color.White);
                    draw.start();
                    SetContentView(draw);
                }
                else
                {
                    SetContentView(Resource.Layout.Main);
                }
                
            };

            // Set our view from the "main" layout resource

        }
    }
}

