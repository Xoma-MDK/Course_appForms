using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Shedule : ContentPage
	{
        public Shedule ()
		{
			InitializeComponent ();
            refreshView.Command = new RefreshCommand();
            refreshView.CommandParameter = this;
            refreshView.IsRefreshing= true;
            GetInfo();
        }

        public async void Refesh()
        {
            User user = await UserService.GetMe();
            var list = await SheduleService.GetShedule(user.Studygroup, user.Substudygroup);
            Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
            refreshView.IsRefreshing = false;
        }
        protected async void GetInfo()
        {
            User user = await UserService.GetMe();
            lbGroup.Text = user.Studygroup;
        }
        public class RefreshCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Shedule shedule = (Shedule)parameter;
                shedule.Refesh();
            }
            public event EventHandler CanExecuteChanged
            {
                add { }
                remove { }
            }
        }
	}
}