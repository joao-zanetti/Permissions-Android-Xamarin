using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.App;
using Xamarin.Essentials;
using Android.Util;

namespace Localization
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textView;


        private readonly string[] strongPermissions = {
                Manifest.Permission.WriteExternalStorage,
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.ReadPhoneState
            };

        private readonly string[] weakPermissions = {
                Manifest.Permission.Camera,
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessWifiState,
                Manifest.Permission.ChangeWifiState,
            };

        private int np;

        //denied -1
        //granted 0
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);           
            textView = FindViewById<TextView>(Resource.Id.loc);
            np = (strongPermissions.Length)+ (weakPermissions.Length);
            CheckPermission(weakPermissions);
            CheckPermission(strongPermissions);
        }


        public void CheckPermission(string[] xPermissions)
        {
            foreach (var perm in xPermissions)
            {

                if (ContextCompat.CheckSelfPermission(this, perm) == (int)Permission.Granted)
                {
                    textView.Text = "PERMISSION ACEPT";
                }
                else
                {
                    ActivityCompat.ShouldShowRequestPermissionRationale(this, perm);
                    var requiredPermissions = new String[] {perm};
                    var dialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                            .SetTitle("title")
                            .SetMessage("msg")
                            .SetPositiveButton("ok", (s, e) =>
                            {
                                ActivityCompat.RequestPermissions(this, requiredPermissions, 0);
                            })
                            .SetCancelable(false);
                    dialog.Show();
                }
            }
        }
   
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if(requestCode== 0)
            {
                    // If request is cancelled, the result arrays are empty.
                    if (grantResults.Length > 0&& grantResults[0] == Permission.Granted)
                    {
                            Toast.MakeText(this, "Permission  OK", ToastLength.Short).Show();      
                    }
                    else
                    {
                        Toast.MakeText(this, "Permission DENIED", ToastLength.Short).Show();
                    }
            }
            else{
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }          
        }
    }
}
