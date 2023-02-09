using Course_appForms.Models;
using Course_appForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Lk : ContentPage
    {
        private User user;
		public Lk ()
		{

            InitializeComponent ();
            user = (User)Application.Current.Properties["user"];
            Load();
		}

        protected void Load()
        {
            LbFIO.Text = $"{user.Surname} {user.Name}\n{user.Patronymic}";
            if (user.Role == 1)
            {
                LbGroup.Text = user.Studygroup;
                LbRole.Text = "Студент";
                LbSubGroup.Text = $"Подгруппа {user.Substudygroup}";
            }
            if (user.Role == 2)
            {
                LbRole.Text = "Преподаватель";
            }

        }
        private async void BtnMarksReport_OnClicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new MarkReport());
        }

        private async void BtnAchievements_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Achievements());
        }

        private void BtnLogout_OnClicked(object sender, EventArgs e)
        {
            AuthService.Logout();
            Application.Current.Properties.Remove("user");
            Application.Current.MainPage = new Login();
        }
    }
}