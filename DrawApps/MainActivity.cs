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

            /*
             * DrawCanvas = kelas yang sudah dimasuakkan ka Main.axml dan dipanggil dengan id = "drawcanvas_main",cek Main.axml 
             */ 
            DrawCanvas dc = FindViewById<DrawCanvas>(Resource.Id.drawcanvas_main);
            
            //event button clicked.
            btn.Click += (o, e) =>
            {
                if (btn.Checked)
                {

                    //DrawCanvas draw = new DrawCanvas(this);
                    //draw.SetBackgroundColor(Color.White);
                    //draw.start();
                    btn.SetBackgroundColor(Color.Green); //Set button background color = green
                    dc.drawAble = true; //draw is enabled 
                    //dc.Visibility = ViewStates.Visible;
                    //LinearLayout view = FindViewById<LinearLayout>(Resource.Id.view);
                    //view.
                    //SetContentView(draw);

                }
                else
                {
                    btn.SetBackgroundColor(Color.Red); //toggle off then cannot draw on canvas
                    dc.drawAble = false; // draw is disabled
                    //dc.Visibility = ViewStates.Gone;
                    //dc.Reset();
                    //SetContentView(Resource.Layout.Main);

                }
                
            };

            // Set our view from the "main" layout resource

        }
    }
}

