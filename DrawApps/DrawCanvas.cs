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
using Android.Util;
using Android.Graphics;


namespace DrawApps
{
    public class DrawCanvas : View
    {
        //drawing path
        private Path drawPath;
        //drawing and canvas paint
        private Paint drawPaint, canvasPaint;
        //initial color
        private uint paintColor = 0xFF660000;
        //canvas 
        private Canvas drawCanvas;
        //canvas bitmap
        private Bitmap canvasBitmap;

        private float brushSize, lastBrushSize;

        //erase state
        private bool erase = false;

        public DrawCanvas(Context context, IAttributeSet attrs): base(context,attrs)
        {
            setupDrawing();
        }

        private void setupDrawing()
        {
            brushSize = Resources.GetInteger(Resource.Integer.medium_size);
            lastBrushSize = brushSize;
            drawPath = new Path();
            drawPaint = new Paint();
            drawPaint.Color = new Color((int)paintColor);
            drawPaint.AntiAlias = true;
            drawPaint.StrokeWidth = brushSize;
            drawPaint.SetStyle(Paint.Style.Stroke);
            drawPaint.StrokeJoin = Paint.Join.Round;
            drawPaint.StrokeCap = Paint.Cap.Round;
            canvasPaint = new Paint();
            canvasPaint.Dither = true;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            canvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            drawCanvas = new Canvas(canvasBitmap);
        }

        protected override void OnDraw(Canvas canvas)
        {
            //base.OnDraw(canvas);
            canvas.DrawBitmap(canvasBitmap, 0, 0, canvasPaint);
            canvas.DrawPath(drawPath, drawPaint);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            //return base.OnTouchEvent(e);
            float touchX = e.GetX();
            float touchY = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    drawPath.MoveTo(touchX, touchY);
                    break;
                case MotionEventActions.Move:
                    drawPath.LineTo(touchX, touchY);
                    break;
                case MotionEventActions.Up:
                    drawCanvas.DrawPath(drawPath, drawPaint);
                    drawPath.Reset();
                    break;
                default:
                    return false;
            }
            Invalidate();
            return true;
        }

        public void setColor(string newColor)
        {
            Invalidate();
            //paintColor = Convert.ToUInt32(Color.ParseColor(newColor));
            int kolor = Color.ParseColor(newColor);
            paintColor = (uint)kolor;
            //Log.Info("Info", Convert.ToUInt32(Color.ParseColor(newColor))+"");
            drawPaint.Color = new Color((int)paintColor);
        }

        public void setBrushSize(float newSize)
        {
            float pixelAmount = TypedValue.ApplyDimension(ComplexUnitType.Dip, newSize, Resources.DisplayMetrics);
            brushSize = pixelAmount;
            drawPaint.StrokeWidth = brushSize;
        }

        public void setLastBrushSize(float lastSize)
        {
            lastBrushSize = lastSize;
        }

        public float getLastBrushSize()
        {
            return lastBrushSize;
        }

        public void setErase(bool isErase)
        {
            erase = isErase;
            if (erase)
                drawPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
            else
                drawPaint.SetXfermode(null);
        }

        public void startNew()
        {
            drawCanvas.DrawColor(Color.Transparent ,PorterDuff.Mode.Clear);
            Invalidate();
        }
    }
}