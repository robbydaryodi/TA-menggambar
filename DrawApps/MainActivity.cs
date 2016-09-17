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
        private DrawCanvas drawView;
        private ImageButton currPaint;
        Context context;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            drawView = FindViewById<DrawCanvas>(Resource.Id.drawing);
            LinearLayout paintLayout = FindViewById<LinearLayout>(Resource.Id.paint_colors);
            currPaint = (ImageButton)paintLayout.GetChildAt(0);
            currPaint.SetImageResource(Resource.Drawable.paint_pressed);
        }

        public void paintClicked(View view)
        {
            if (view != currPaint)
            {
                ImageButton imgView = (ImageButton)view;
                String color = view.Tag.ToString(); //view.Tag.ToString();

                drawView.setColor(color);
                imgView.SetImageResource(Resource.Drawable.paint_pressed);
                currPaint.SetImageResource(Resource.Drawable.paint);
                currPaint = (ImageButton)view;
            }
        }
    }
}

