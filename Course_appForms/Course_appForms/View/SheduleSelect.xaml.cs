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
	public partial class SheduleSelect : ContentPage
	{
		public SheduleSelect ()
		{
			InitializeComponent ();
		}

        private async void BtnClassrooms_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SheduleClassroom());
        }

        private async void BtnGroups_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SheduleGroup());
        }

        private async void BtnTeachers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SheduleTeacher());
        }
    }
}