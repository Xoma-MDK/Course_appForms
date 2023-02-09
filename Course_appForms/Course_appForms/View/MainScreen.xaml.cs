using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_appForms.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainScreen : ContentPage
	{
		public MainScreen ()
		{
			InitializeComponent ();
			var list = new List<Models.News>
            {
                new News
                {
                    Description = "Карьерный квест, IT-квартирник, кибердром. Всё это можно было увидеть на ярмарке IT-вакансий «COOKIE FEST»...",
                    ImageUrl = "a123.jpg",
                    Title = "Студенты и преподаватели колледжа приняли участие в Дне карьеры «COOKIE FEST»",
                    Date = "19.05.2022"
                },
                new News
                {
                    Description = "Карьерный квест, IT-квартирник, кибердром. Всё это можно было увидеть на ярмарке IT-вакансий «COOKIE FEST»...",
                    ImageUrl = "a123.jpg",
                    Title = "Студенты и преподаватели колледжа приняли участие в Дне карьеры «COOKIE FEST»",
                    Date = "19.05.2022"
                },
                new News
                {
                    Description = "Карьерный квест, IT-квартирник, кибердром. Всё это можно было увидеть на ярмарке IT-вакансий «COOKIE FEST»...",
                    ImageUrl = "a123.jpg",
                    Title = "Студенты и преподаватели колледжа приняли участие в Дне карьеры «COOKIE FEST»",
                    Date = "19.05.2022"
                }
            };
            cV.ItemsSource = list;
        }
	}
}