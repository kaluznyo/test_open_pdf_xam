using System;
using System.IO;
using Android.Content;
using Android.OS;
using LogyMobile.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveFile_Android))]
namespace LogyMobile.Droid.Services
{
    class SaveFile_Android : ISaveFile
    {
        public void SaveAndOpenFile(string filename, byte[] bytes)
        {
            try
            {
                string filePath = Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath + "/" + filename;
                File.WriteAllBytes(filePath, bytes);
                OpenFile(filePath);
            }
            catch (Exception e)
            {
                //Error
            }
        }
        public void OpenFile(string filePath)
        {
            try
            {
                var bytes = File.ReadAllBytes(filePath);

                string extension = Path.GetExtension(filePath);

                string application;
                switch (extension.ToLower())
                {
                    case ".doc":
                    case ".docx":
                        application = "application/msword";
                        break;
                    case ".pdf":
                        application = "application/pdf";
                        break;
                    case ".xls":
                    case ".xlsx":
                        application = "application/vnd.ms-excel";
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        application = "image/jpeg";
                        break;
                    default:
                        application = "*/*";
                        break;
                }

                Java.IO.File file = new Java.IO.File(filePath);
                file.SetReadable(true);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, application);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder(); StrictMode.SetVmPolicy(builder.Build());


                Device.BeginInvokeOnMainThread(() =>
                {
                    Android.App.Application.Context.StartActivity(intent);
                });
            }
            catch (Exception ex)
            {
                //Error
            }
        }

    }
}