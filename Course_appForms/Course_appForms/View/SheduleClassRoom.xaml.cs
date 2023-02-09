using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Course_appForms.Models;
using Course_appForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SheduleClassRoom : ContentPage
	{
        public SheduleClassRoom()
		{
			InitializeComponent ();
            Refesh();
        }
        public async void Refesh()
        {
            var classRooms = await SheduleService.GetSheduleClassRooms("по адресу Красный проспект 72");
            PicClassRoom.ItemsSource = classRooms;
            PicClassRoom.IsEnabled = true;
        }
        public async void Refesh2()
        {
            PicClassRoom.IsEnabled = false;
            var temp = (ClassRoom)PicClassRoom.SelectedItem;
            var classRoom = await SheduleService.GetSheduleClassRoom(temp.Title);
            CvSheduleClassRoom.ItemsSource = classRoom;
            PicClassRoom.IsEnabled =true;
        }

        private void PicClassRoom_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Refesh2();
        }
    }
}