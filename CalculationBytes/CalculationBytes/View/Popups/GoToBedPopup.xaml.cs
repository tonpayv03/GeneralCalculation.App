using CalculationBytes.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculationBytes.View.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GoToBedPopup : Rg.Plugins.Popup.Pages.PopupPage
	{
		private SleepCalculationViewModelPage vm;
		private List<string> _bedTimeList = new List<string>();
		public GoToBedPopup(List<string> bedTime)
		{
			InitializeComponent();
			_bedTimeList = bedTime;
			SetContent();
		}

		private void SetContent()
		{
			gridContent.RowDefinitions.Add(new RowDefinition { Height = 45 });
			gridContent.RowDefinitions.Add(new RowDefinition { Height = 45 });
			gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 });
			gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 });
			gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = 80 });

			for (int i = 0; i < _bedTimeList.Count; i++)
			{
				Frame cardFrame = new Frame() { BackgroundColor = Color.FromHex("#5437B1") };
				if (i <= 2) // item 3 อันแรก
				{
					cardFrame.Content = new StackLayout()
					{
						Children =
						{
							new Label
							{
								Text = _bedTimeList[i],
								FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
								FontAttributes = FontAttributes.Bold,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								VerticalOptions = LayoutOptions.CenterAndExpand
							}
						}
					};

					if (i < 2) // เฉพาะ item 2 อันแรกที่จะต้องเป็นสีเ้ม
					{
						cardFrame.BackgroundColor = Color.FromHex("#3d2d71");
					}

					gridContent.Children.Add(cardFrame, i, 0);
				}
				else
				{
					cardFrame.Content = new StackLayout()
					{
						Children =
						{
							new Label
							{
								Text = _bedTimeList[i],
								FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
								FontAttributes = FontAttributes.Bold,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								VerticalOptions = LayoutOptions.CenterAndExpand
							}
						}
					};

					gridContent.Children.Add(cardFrame, i-3, 1); // i - จำนวนคอลัมธ์ เพื่อadd data เข้า grid (0,1) (1,1) (2,1)
				}
			}
		}

		private async void Back_Tapped(object sender, EventArgs e)
		{
			await PopupNavigation.Instance.PopAsync();
		}
	}
}