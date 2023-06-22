using Course_appForms.Models;
using Course_appForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheduleTeacher : ContentPage
    {
        public SheduleTeacher()
        {
            InitializeComponent();
            LoadBuilds();
            RVTeacher.Command = new RefreshCommand();
            RVTeacher.CommandParameter = this;
        }
        public async void LoadBuilds()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            var TeacherList = await SheduleService.GetSheduleTeachers();
            PicTeacher.ItemsSource = TeacherList;
            PicTeacher.IsEnabled = true;
        }
        public async void Refesh()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                RVTeacher.IsRefreshing = false;
                return;
            }
            PicTeacher.IsEnabled = false;
            var teacher = (Teacher)PicTeacher.SelectedItem;
            var sheduleTeacher = await SheduleService.GetSheduleTeacher(teacher.Title);
            CvSheduleTeacher.ItemsSource = sheduleTeacher;
            PicTeacher.IsEnabled = true;
            RVTeacher.IsRefreshing = false;
        }
        public class RefreshCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {

                SheduleTeacher shedule = (SheduleTeacher)parameter;
                if (shedule.PicTeacher.SelectedIndex == -1)
                {
                    shedule.DisplayToastAsync("Группа не выбрана");
                    shedule.RVTeacher.IsRefreshing = false;
                    return;
                }
                shedule.Refesh();
            }
            public event EventHandler CanExecuteChanged
            {
                add { }
                remove { }
            }
        }

        private void PicTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PicTeacher.SelectedIndex != -1) Refesh();
        }
    }
}