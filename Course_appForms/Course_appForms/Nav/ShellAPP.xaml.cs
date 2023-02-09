using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_appForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShellAPP : Shell
	{
		public ShellAPP()
		{
			InitializeComponent ();
		}
	}
}