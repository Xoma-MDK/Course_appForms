﻿using System;
using Course_appForms.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms
{
    public partial class App : Application
    {
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
