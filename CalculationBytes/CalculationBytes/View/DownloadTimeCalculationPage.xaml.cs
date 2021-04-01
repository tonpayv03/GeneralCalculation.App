using CalculationBytes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculationBytes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DownloadTimeCalculationPage : ContentPage
	{
		public DownloadTimeCalculationPage()
		{
			InitializeComponent();
			BindingContext = new DownloadTimeCalculationViewModelPage();
			// test
		}
	}
}