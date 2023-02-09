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
            GetInfo();
            refreshView.Command = new RefreshCommand();
            refreshView.CommandParameter = this;
            refreshView.IsRefreshing= true;
            
        }

        public async void Refesh()
        {
            var user = await UserService.GetMe();
            if (user.Role == 1)
            {
                var list = await SheduleService.GetShedule(user.Studygroup, user.Substudygroup);
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
            }
            else if (user.Role == 2)
            {
                var list = await SheduleService.GetSheduleTeacher($"{user.Surname} {user.Name} {user.Patronymic}");
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
            }
            refreshView.IsRefreshing = false;
        }
        protected async void GetInfo()
        {
            var user = await UserService.GetMe();
            if(user.Role == 2)lbGroup.Text = user.Studygroup;
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
    public class SheduleDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate TeacherTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((Lesson)item).Mode == 0) return StudentTemplate;
            if (((Lesson)item).Mode == 1) return TeacherTemplate;
            return StudentTemplate;
        }
    }
}