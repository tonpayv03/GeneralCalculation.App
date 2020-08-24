using CalculationBytes.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculationBytes
{
	public partial class App : Application
	{
		public App()
		{

			AppSetting.Init();

			InitializeComponent();

			MainPage = new NavigationPage(new MainPage()
			{
				BackgroundImageSource = "bg.jpg"
			});
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
