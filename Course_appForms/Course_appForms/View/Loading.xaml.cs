using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {
        public Loading()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            User user = await UserService.GetMe();
            if (user != null)
            {
                if (user.Role == 1)
                    Application.Current.MainPage = new ShellAPP();
                if (user.Role == 2)
                    Application.Current.MainPage = new ShellAPP2();
            }
            else
                Application.Current.MainPage = new Login();
        }
    }
}