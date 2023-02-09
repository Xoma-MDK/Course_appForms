using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.Forms;

namespace Course_appForms
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            string login = EntLogin.Text;
            string password = EntPassword.Text;
            if (await AuthService.Login(login, password))
            {
                User user = await UserService.GetMe();
                if (user.Role == 1)
                    Application.Current.MainPage = new ShellAPP();
                if (user.Role == 2)
                    Application.Current.MainPage = new ShellAPP2();
            }
            else
            {
                await DisplayAlert("Ошибка!", $"Не верный логин или пароль.", "close");
            }
        }
    }
}
