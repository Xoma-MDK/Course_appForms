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
	public partial class Shedule2 : ContentPage
	{
        public Shedule2 ()
		{
			InitializeComponent ();
            refreshView2.Command = new RefreshCommand2();
            refreshView2.CommandParameter = this;
            refreshView2.IsRefreshing= true;
        }

        public async void Refesh()
        {
            User user = await UserService.GetMe();
            var list = await SheduleService.GetSheduleTeacher($"{user.Surname} {user.Name} {user.Patronymic}");
            Device.BeginInvokeOnMainThread(() => CvShedule2.ItemsSource = list);
            refreshView2.IsRefreshing = false;
        }
        public class RefreshCommand2 : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Shedule2 shedule = (Shedule2)parameter;
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