using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Models;
using Course_appForms.Services;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
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
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {   
                await this.DisplayToastAsync("Нет подключения к сети");

                if (Preferences.Get("user", null) != null)
                {
                    var user = JsonConvert.DeserializeObject<User>(Preferences.Get("user", null));
                    Application.Current.Properties["user"] = user;
                    Application.Current.MainPage = new ShellAPP();
                }
                else
                {
                    Application.Current.MainPage = new Login();
                }
            }
            else
            {
                User user = await UserService.GetMe();
                if (user != null)
                {
                    Application.Current.Properties["user"] = user;
                    Preferences.Set("user", JsonConvert.SerializeObject(user));
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new ShellAPP();
                }
                else
                    Application.Current.MainPage = new Login();
            }



        }
    }
}