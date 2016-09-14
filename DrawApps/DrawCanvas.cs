using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;


namespace DrawApps
{
    public class DrawCanvas : View
    {
        public DrawCanvas(Context context) : base(context)
        {

        }

        private Path drawPath; //Variable menampung jalur2 coretan
        private Paint drawPaint, canvasPaint; // variable menampung properti gambar dan tempat gambar coretan
        private uint paintColor = 0xFF660000; // warna coretan/gambar 0xFF660000 = merah  -> link: http://colrd.com/color/0xff660000/
        private Canvas drawCanvas; // canvas tempat menggambar
        private Bitmap canvasBitmap; // citra bitmap hasil coretan setelah jari/pointer diangkat
        
        /*
         * start
         * berisi variable2 untuk proses inisiasi pemanggilan kelas DrawCanvas di MainActivity.cs
         */
        public void start()
        {
            drawPath = new Path();
            drawPaint = new Paint();
            drawPaint.Color = new Color((int)paintColor);
            drawPaint.AntiAlias = true;
            drawPaint.StrokeWidth = 10;
            drawPaint.SetStyle(Paint.Style.Stroke);
            drawPaint.StrokeJoin = Paint.Join.Round;
            drawPaint.StrokeCap = Paint.Cap.Round;
            canvasPaint = new Paint();
            canvasPaint.Dither = true;
        }

        /*
         * OnSizeChanged
         * event yang menghandle perubahan ukuran tempat menggambar. 
         * 
         */
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            // canvasBitmap = variable menampung gambar setelah jari/pointer diangkat
            canvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);

            // drawCanvas = variable menampung coretan yang telah digambar.
            drawCanvas = new Canvas(canvasBitmap);
        }


        /*
         * Event menghandle proses penggambaran.
         * 
         */ 
        protected override void OnDraw(Canvas canvas)
        {
            canvas.DrawBitmap(canvasBitmap, 0, 0, canvasPaint);
            canvas.DrawPath(drawPath, drawPaint);
        }


        /*
         * OnTouchEvent
         * Memberi aksi ketika layar disentuh dan melakukan aksi ketika jari/pointer digerakkan.
         * 
         */ 
        public override bool OnTouchEvent(MotionEvent e)
        {
            float touchX = e.GetX();
            float touchY = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down: // even ketika jari/pointer menyentuh layar.
                    drawPath.MoveTo(touchX, touchY);
                    break;
                case MotionEventActions.Move: // even ketika jari/pointer digerakkan sesudah menyentuh layar.
                    drawPath.LineTo(touchX, touchY);
                    break;
                case MotionEventActions.Up: // even ketika jari/pointer diangkat dari layar.
                    drawCanvas.DrawPath(drawPath, drawPaint); // menyimpan jalur2 coretan di "drawPath" dengan properti2 pada "drawPaint"
                    drawPath.Reset(); // reset jalur2 coretan jika jari/pointer menyentuh layar lagi.
                    break;
                default:
                    return false;
            }
            Invalidate();
            return true;
        }

    }
}