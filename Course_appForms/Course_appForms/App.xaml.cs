using System;
using System.IO;
using Course_appForms.Services;
using Course_appForms.View;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace Course_appForms
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "shedule.db";
        public static DBLocalService database;
        public static DBLocalService Database
        {
            get
            {
                if (database == null)
                {
                    database = new DBLocalService(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            MainPage = new Loading();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
