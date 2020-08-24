using CalculationBytes.Resources;
using CalculationBytes.Services;
using CalculationBytes.Services.Markup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculationBytes
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			UpdateUIMainPage();
		}

		private async void BitOrByte_Clicked(object sender, EventArgs e)
		{
			IsLoadingPanel.IsVisible = true;
			await Task.Delay(150);
			var pushPage = new View.MemoryCalculationPage();
			await Navigation.PushAsync(pushPage);
			IsLoadingPanel.IsVisible = false;
		}

		private async void DownloadTime_Clicked(object sender, EventArgs e)
		{
			IsLoadingPanel.IsVisible = true;
			await Task.Delay(150);
			var pushPage = new View.DownloadTimeCalculationPage();
			await Navigation.PushAsync(pushPage);
			IsLoadingPanel.IsVisible = false;
		}

		private async void SleepCal_Clicked(object sender, EventArgs e)
		{
			IsLoadingPanel.IsVisible = true;
			await Task.Delay(150);
			var pushPage = new View.SleepCalculationPage();
			await Navigation.PushAsync(pushPage);
			IsLoadingPanel.IsVisible = false;
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			string[] Languages = new string[]
			{
				LanguageResource.language_en,
				LanguageResource.language_th
			};

			string selectedLanguage = await DisplayActionSheet(MenuResource.menu_select_language, MenuResource.menu_cancel, null, Languages);

			if (selectedLanguage == Languages[0])
			{
				AppSetting.Current.Language = AppSetting.English;
			}
			else if (selectedLanguage == Languages[1])
			{
				AppSetting.Current.Language = AppSetting.Thai;
			}
			UpdateUIMainPage();
		}

		private void UpdateUIMainPage()
		{
			WelcomLbl.Text = MenuResource.welcom_calculation;
			BtnMemoryCal.Text = MenuResource.menu_convert_memory;
			BtnDownloadTimeCal.Text = MenuResource.menu_download_time;
			BtnSleepCal.Text = MenuResource.menu_sleep_cal;
			BtnSelectLanguage.Text = AppSetting.Current.Language;
		}

	}
}
