using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Course_appForms.View;
using Newtonsoft.Json;

namespace Course_appForms.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Shedule : ContentPage
    {
        private User _user;
        private string _group;
        private string _teacher;
        private bool _offline;
        public Shedule()
		{
			InitializeComponent ();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                if (!App.Database.GetItems().Any())
                {
                    this.DisplayToastAsync("Нет подключения к сети", 2000);
                    return;
                }
                _user = (User)Application.Current.Properties["user"];
                Debug.Print(App.Database.GetItems().FirstOrDefault().SheduleDamp);
                var list = JsonConvert.DeserializeObject<List<Models.Shedule>>(App.Database.GetItems().FirstOrDefault().SheduleDamp);
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
                if (_user.Role != 2) lbGroup.Text = _user.Studygroup;
                _offline = true;
                refreshView.Command = new RefreshCommand();
                refreshView.CommandParameter = this;
                this.DisplayToastAsync("Загруженные данные", 2000);
            }
            else
            {
                _user = (User)Application.Current.Properties["user"];
                Load();
                refreshView.Command = new RefreshCommand();
                refreshView.CommandParameter = this;
                refreshView.IsRefreshing = true;
            }


        }
        public Shedule(string Group)
        {
            InitializeComponent();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            _user = (User)Application.Current.Properties["user"];
            _group = Group;
            Load();
            refreshView.Command = new RefreshCommand();
            refreshView.CommandParameter = this;
            refreshView.IsRefreshing = true;

        }
        public Shedule(string Teacher, bool temp = false)
        {
            InitializeComponent();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                this.DisplayToastAsync("Нет подключения к сети", 5000);
                return;
            }
            _user = (User)Application.Current.Properties["user"];
            _teacher = Teacher;
            Load();
            refreshView.Command = new RefreshCommand();
            refreshView.CommandParameter = this;
            refreshView.IsRefreshing = true;

        }

        public async void Refesh()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await this.DisplayToastAsync("Нет подключения к сети", 5000);
                refreshView.IsRefreshing = false;
                _offline = true;
            }
            else
            {
                _offline = false;
            }
            if (_offline)
            {
                refreshView.IsRefreshing = false;
                return;
            }
            //var user = await UserService.GetMe();
            if (_group != null)
            {
                var list = await SheduleService.GetShedule(_group, 1);
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
            }
            else if (_teacher != null)
            {
                var list = await SheduleService.GetSheduleTeacher($"{_teacher}");
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
            }
            else if (_user.Role == 1)
            {
                var list = await SheduleService.GetShedule(_user.Studygroup, _user.Substudygroup);
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
                App.Database.TruncateTable();
                App.Database.SaveItem(new SheduleDB {SheduleDamp = JsonConvert.SerializeObject(list)});
            }
            else if (_user.Role == 2)
            {
                var list = await SheduleService.GetSheduleTeacher($"{_user.Surname} {_user.Name} {_user.Patronymic}");
                Device.BeginInvokeOnMainThread(() => CvShedule.ItemsSource = list);
                Application.Current.Properties["Shedule"] = list;
                App.Database.TruncateTable();
                App.Database.SaveItem(new SheduleDB {SheduleDamp = JsonConvert.SerializeObject(list)});
            }
            refreshView.IsRefreshing = false;
        }
        protected void Load()
        {
            if (_group == null && _teacher == null)
            {
                if (_user.Role != 2) lbGroup.Text = _user.Studygroup;
            }else if (_group != null)
            {
                lbInfo.Text = _group;
                lbInfo.IsVisible = true;
            }
            else if (_teacher != null)
            {
                lbInfo.Text = _teacher;
                lbInfo.IsVisible = true;
            }

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

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (_group == null && _teacher == null && _offline == false)
            {
                var LbSender = (IdLable)sender;
                if (LbSender.LbId == 0)
                {
                    //await this.DisplayToastAsync($"{LbSender.Text}", 2000);
                    await Navigation.PushModalAsync(new Shedule(Teacher:LbSender.Text));
                }
                else if (LbSender.LbId == 1)
                {
                    //await this.DisplayToastAsync($"{LbSender.Text}", 2000);
                    await Navigation.PushModalAsync(new Shedule(Group:LbSender.Text));
                }
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