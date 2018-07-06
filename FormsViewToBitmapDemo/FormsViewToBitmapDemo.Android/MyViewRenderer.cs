using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FormsViewToBitmapDemo.Droid;
using Xamarin.Forms;
using FormsViewToBitmapDemo;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Java.IO;
using System.IO;
using Android.Content.PM;
using Android.Support.V4.App;

[assembly: ExportRenderer(typeof(MyView), typeof(MyViewRenderer))]
namespace FormsViewToBitmapDemo.Droid
{
    public class MyViewRenderer : ViewRenderer<MyView, Android.Views.View>
    {
        public MyViewRenderer(Context c) : base(c)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<MyView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                //register the event
                e.NewElement.OnDrawBitmap += NewElement_OnDrawBitmap;
            }
        }

        private void NewElement_OnDrawBitmap(object sender, EventArgs e)
        {
            if (this.ViewGroup != null)
            {
                //get the subview
                Android.Views.View subView = ViewGroup.GetChildAt(0);
                int width = subView.Width;
                int height = subView.Height;

                //create and draw the bitmap
                Bitmap b = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                Canvas c = new Canvas(b);
                ViewGroup.Draw(c);

                //save the bitmap to file
                SaveBitmapToFile(b);
            }
        }

        void SaveBitmapToFile(Bitmap bm)
        {
            //before save, we need to request permission
            RequestStoragePermission();

            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filePath = System.IO.Path.Combine(sdCardPath, "test.png");
            var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            bm.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Close();
        }

        void RequestStoragePermission()
        {
            var activity = Forms.Context as MainActivity;
            if (activity.CheckSelfPermission(Android.Manifest.Permission.WriteExternalStorage) == Permission.Granted)
            {
                return;
            }
            else
            {
                ActivityCompat.RequestPermissions(activity, new string[] { Android.Manifest.Permission.WriteExternalStorage }, 1);
            }
        }
    }
}