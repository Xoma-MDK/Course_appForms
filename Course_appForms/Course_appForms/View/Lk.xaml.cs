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
		public Lk ()
		{

            InitializeComponent ();
            GetInfo();
		}

        protected async void GetInfo()
        {
            User user = await UserService.GetMe();
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
    }
}