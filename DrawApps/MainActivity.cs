using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Java.Util;
using Android.Provider;


namespace DrawApps
{
    [Activity(Label = "DrawApps", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, View.IOnClickListener
    {
        private DrawCanvas drawView;
        private ImageButton currPaint, drawBtn, eraseBtn, newBtn, saveBtn;
        private float smallBrush, mediumBrush, largeBrush;
        private string lastColor;
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.RequestWindowFeature(WindowFeatures.NoTitle);
            this.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            SetContentView(Resource.Layout.Main);

            smallBrush = Resources.GetInteger(Resource.Integer.small_size);
            mediumBrush = Resources.GetInteger(Resource.Integer.medium_size);
            largeBrush = Resources.GetInteger(Resource.Integer.large_size);
            drawBtn = FindViewById<ImageButton>(Resource.Id.draw_btn);
            drawBtn.SetOnClickListener(this);
            eraseBtn = FindViewById<ImageButton>(Resource.Id.erase_btn);
            eraseBtn.SetOnClickListener(this);
            newBtn = FindViewById<ImageButton>(Resource.Id.new_btn);
            newBtn.SetOnClickListener(this);
            saveBtn = FindViewById<ImageButton>(Resource.Id.save_btn);
            saveBtn.SetOnClickListener(this);

            //Initialize Color on the first time program run
            lastColor = "#FF660000"; //red maroon

            //Set medium brush as disabled as it's already clicked 
            //LinearLayout brushchooser = FindViewById<LinearLayout>(Resource.Id.brush_size_chooser);
            //ImageButton mediumBtn = brushchooser.FindViewById<ImageButton>(Resource.Id.medium_brush);
            //mediumBtn.Enabled = false;


            // Set our view from the "main" layout resource


            drawView = FindViewById<DrawCanvas>(Resource.Id.drawing);
            drawView.setLastBrushSize(mediumBrush);
            LinearLayout paintLayout = FindViewById<LinearLayout>(Resource.Id.paint_colors);
            currPaint = (ImageButton)paintLayout.GetChildAt(0);
            currPaint.SetImageResource(Resource.Drawable.paint_pressed);

            
        }


        [Java.Interop.Export("paintClicked")]
        public void paintClicked(View view)
        {
            if (view != currPaint)
            {
                drawView.setErase(false);
                drawView.setBrushSize(drawView.getLastBrushSize());

                ImageButton imgView = (ImageButton)view;
                String color = view.Tag.ToString(); //view.Tag.ToString();
                lastColor = color;

                drawView.setColor(color);
                imgView.SetImageResource(Resource.Drawable.paint_pressed);
                currPaint.SetImageResource(Resource.Drawable.paint);
                currPaint = (ImageButton)view;
                
                
            }
        }


        public void OnClick(View view)
        {
            if (view.Id == Resource.Id.draw_btn)
            {
                Dialog brushDialog = new Dialog(this);
                brushDialog.SetTitle("Brush Size:");
                brushDialog.SetContentView(Resource.Layout.brush_chooser);

                //Set Erase button false and set color
                drawView.setErase(false);

                ImageButton smallBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.small_brush);
                smallBtn.Click += delegate
                {
                    drawView.setColor(lastColor);
                    drawView.setBrushSize(smallBrush);
                    drawView.setLastBrushSize(smallBrush);
                    drawView.setErase(false);
                    brushDialog.Dismiss();
                };

                ImageButton mediumBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.medium_brush);
                mediumBtn.Click += delegate
                {
                    drawView.setColor(lastColor);
                    drawView.setBrushSize(mediumBrush);
                    drawView.setLastBrushSize(mediumBrush);
                    drawView.setErase(false);
                    brushDialog.Dismiss();
                };

                ImageButton largeBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.large_brush);
                largeBtn.Click += delegate
                {
                    drawView.setColor(lastColor);
                    drawView.setBrushSize(largeBrush);
                    drawView.setLastBrushSize(largeBrush);
                    drawView.setErase(false);
                    brushDialog.Dismiss();
                };

                brushDialog.Show();
            }else if(view.Id == Resource.Id.erase_btn)
            {
                Dialog brushDialog = new Dialog(this);
                brushDialog.SetTitle("Erase Size:");
                brushDialog.SetContentView(Resource.Layout.brush_chooser);

                ImageButton smallBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.small_brush);
                smallBtn.Click += delegate
                {
                    drawView.setErase(true);
                    drawView.setBrushSize(smallBrush);
                    brushDialog.Dismiss();
                };

                ImageButton mediumBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.medium_brush);
                mediumBtn.Click += delegate
                {
                    drawView.setErase(true);
                    drawView.setBrushSize(mediumBrush);
                    brushDialog.Dismiss();
                };

                ImageButton largeBtn = brushDialog.FindViewById<ImageButton>(Resource.Id.large_brush);
                largeBtn.Click += delegate
                {
                    drawView.setErase(true);
                    drawView.setBrushSize(largeBrush);
                    brushDialog.Dismiss();
                };
                brushDialog.Show();
            }else if(view.Id == Resource.Id.new_btn)
            {
                AlertDialog.Builder newDialog = new AlertDialog.Builder(this);
                newDialog.SetTitle("New Drawing");
                newDialog.SetMessage("Start new drawing(you will lose the current drawing)?");
                newDialog.SetPositiveButton("Yes", delegate
                {
                    drawView.startNew();
                    Dismiss();
                });


                newDialog.SetNegativeButton("Cancel", delegate
                {
                    Dismiss();
                });
                newDialog.Show();

            }else if(view.Id == Resource.Id.save_btn)
            {
                AlertDialog.Builder saveDialog = new AlertDialog.Builder(this);
                saveDialog.SetTitle("Save Drawing");
                saveDialog.SetMessage("Save drawing to device gallery?");
                saveDialog.SetPositiveButton("Yes", delegate
                {
                    drawView.DrawingCacheEnabled = true;
                    string imgSaved = MediaStore.Images.Media.InsertImage(ContentResolver, drawView.DrawingCache, UUID.RandomUUID().ToString() + ".png", "drawing");
                    if(imgSaved != null)
                    {
                        Toast savedToast = Toast.MakeText(ApplicationContext, "Drawing Saved to Gallery!", ToastLength.Short);
                        savedToast.Show();
                    }else
                    {
                        Toast unsavedToast = Toast.MakeText(ApplicationContext, "Image could not be saved!", ToastLength.Short);
                        unsavedToast.Show();
                    }
                    drawView.DestroyDrawingCache();
                });


                saveDialog.SetNegativeButton("Cancel", delegate
                {
                    Dismiss();
                });
                saveDialog.Show();

                

            }
        }

        private AlertDialog _dialog;
        public void Dismiss()
        {
            if (_dialog != null)
                _dialog.Dismiss();
        }

        //endfile
    }
}

