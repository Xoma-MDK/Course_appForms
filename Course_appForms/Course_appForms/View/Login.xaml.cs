using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;

namespace Course_appForms
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => { 
                await Launcher.OpenAsync(new Uri("https://t.me/xomic"));
            };
            LbRegister.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            LbLoad.IsVisible = true;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                loadingIndicator.IsVisible = false;
                LbLoad.IsVisible = false;
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            string login = EntLogin.Text;
            string password = EntPassword.Text;
            if (await AuthService.Login(login, password))
            {
                User user = await UserService.GetMe();
                Application.Current.Properties["user"] = user;
                if (user.Role == 1)
                    Application.Current.MainPage = new ShellAPP();
                if (user.Role == 2)
                    Application.Current.MainPage = new ShellAPP2();
            }
            else
            {
                loadingIndicator.IsVisible = false;
                LbLoad.IsVisible = false;
                await this.DisplayToastAsync("Не верный логин или пароль.", 3000);
            }
        }
    }
}
