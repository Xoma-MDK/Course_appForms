﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace Course_appForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SheduleClassRoom : ContentPage
	{
        public SheduleClassRoom()
		{
			InitializeComponent ();
            LoadBuilds();
            RVClassRoom.Command = new RefreshCommand();
            RVClassRoom.CommandParameter = this;
        }
        public async void LoadClassRooms(string build)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            var classRooms = await SheduleService.GetSheduleClassRooms(build);
            PicClassRoom.ItemsSource = classRooms;
            PicClassRoom.IsEnabled = true;
        }
        public async void LoadBuilds()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            var buildsList = await SheduleService.GetSheduleBuilds();
            PicBuild.ItemsSource = buildsList;
            PicBuild.IsEnabled = true;
        }
        public async void Refesh2()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                RVClassRoom.IsRefreshing = false;
                return;
            }
            PicClassRoom.IsEnabled = false;
            var temp = (ClassRoom)PicClassRoom.SelectedItem;
            var classRoom = await SheduleService.GetSheduleClassRoom(temp.Title);
            CvSheduleClassRoom.ItemsSource = classRoom;
            PicClassRoom.IsEnabled =true;
            RVClassRoom.IsRefreshing = false;
        }

        private void PicClassRoom_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if(PicClassRoom.SelectedIndex != -1) Refesh2();
        }
        public class RefreshCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                
                SheduleClassRoom shedule = (SheduleClassRoom)parameter;
                if (shedule.PicClassRoom.SelectedIndex == -1)
                {
                    shedule.DisplayToastAsync("Аудитория не выбрана");
                    shedule.RVClassRoom.IsRefreshing = false;
                    return;
                }
                shedule.Refesh2();
            }
            public event EventHandler CanExecuteChanged
            {
                add { }
                remove { }
            }
        }

        private void PicBuild_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PicBuild.IsEnabled = false;
            var temp = (Build)PicBuild.SelectedItem;
            LoadClassRooms(temp.Title);
            PicBuild.IsEnabled = true;
        }
    }
}