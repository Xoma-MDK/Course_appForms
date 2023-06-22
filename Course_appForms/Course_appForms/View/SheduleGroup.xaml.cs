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
    public partial class SheduleGroup : ContentPage
    {
        public SheduleGroup()
        {
            InitializeComponent();
            LoadBuilds();
            RVGroup.Command = new RefreshCommand();
            RVGroup.CommandParameter = this;
        }
        public async void LoadBuilds()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            var groupsList = await SheduleService.GetSheduleGroups();
            PicGroup.ItemsSource = groupsList;
            PicGroup.IsEnabled = true;
        }
        public async void Refesh()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                RVGroup.IsRefreshing = false;
                return;
            }
            PicGroup.IsEnabled = false;
            var group = (Group)PicGroup.SelectedItem;
            var sheduleGroup = await SheduleService.GetShedule(group.Title, 1);
            CvSheduleGroup.ItemsSource = sheduleGroup;
            PicGroup.IsEnabled = true;
            RVGroup.IsRefreshing = false;
        }
        public class RefreshCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {

                SheduleGroup shedule = (SheduleGroup)parameter;
                if (shedule.PicGroup.SelectedIndex == -1)
                {
                    shedule.DisplayToastAsync("Группа не выбрана");
                    shedule.RVGroup.IsRefreshing = false;
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

        private void PicGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PicGroup.SelectedIndex != -1) Refesh();
        }
    }
}