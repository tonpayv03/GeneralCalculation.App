using CalculationBytes.View.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculationBytes.ViewModel
{
	public class SleepCalculationViewModelPage : BaseViewModel
	{
		#region Global Variable
		private int year = DateTime.Now.Year - 1;
		private const int month = 5;
		private int day = 5;
		#endregion

		#region Global List
		public List<int> HoursList { get; set; }
		public List<string> MinuteList { get; set; }
		public List<string> TimeFormatList { get; set; }
		public List<string> BedtimeList { get; set; }
		public List<string> WakeUpList { get; set; }
		#endregion

		#region Properties
		private int _select_hours;
		public int Select_Hours
		{
			get { return _select_hours; }
			set { if (_select_hours != value) { _select_hours = value; OnPropertyChanged(); } }
		}


		private string _select_minute;
		public string Select_Minute
		{
			get { return _select_minute; }
			set { if (_select_minute != value) { _select_minute = value; OnPropertyChanged(); } }
		}


		private string _select_time_format;
		public string Select_Time_Format
		{
			get { return _select_time_format; }
			set { if (_select_time_format != value) { _select_time_format = value; OnPropertyChanged(); } }
		}
		#endregion


		#region Command
		public Command GoToBed_ClickCommand { get; set; }
		public Command WakeUpTime_ClickCommand { get; set; }
		//public Command BackLabel_TappedCommand { get; set; }
		#endregion

		public SleepCalculationViewModelPage()
		{
			SetData();
			GoToBed_ClickCommand = new Command(GotoBed_Clicked);
			WakeUpTime_ClickCommand = new Command(WakeUpTime_Clicked);
			//BackLabel_TappedCommand = new Command(BackLabel_Tapped);
		}

		private void SetData()
		{
			HoursList = new List<int>()
			{
				1, 2, 3, 4, 5 ,6, 7, 8, 9, 10, 11, 12
			};

			MinuteList = new List<string>()
			{
				 "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55"
			};

			TimeFormatList = new List<string>()
			{
				"AM" , "PM"
			};

			Select_Hours = HoursList.Find(it => it == 8);
			Select_Minute = MinuteList.FirstOrDefault();
			Select_Time_Format = TimeFormatList.FirstOrDefault();
		}

		private void CalculationGoToBedTime()
		{
			// 90 นาทีต่อ cycle + เวลาเฉลี่ยในการหลับ 15 นาที
			DateTime dateTime = new DateTime(year, month, day, Select_Time_Format.ToUpperInvariant() == "AM" ? Select_Hours : Select_Hours + 12, Convert.ToInt32(Select_Minute), 0, DateTimeKind.Local);

			List<string> bedTimeList = new List<string>(capacity: 6);

			for (int i = 1; i <= 6; i++)
			{
				var bt = dateTime.AddMinutes(-(90 * i + 15));
				bedTimeList.Add(bt.ToString("h:mm tt", CultureInfo.CreateSpecificCulture("en-US"))); // Set CultureInfo ให้เป็น "en-US" เพื่อให้ format ของเวลาเป็นแบบ 12 Hours ที่มี AM/PM
			}

			bedTimeList.Reverse();
			BedtimeList = bedTimeList;
		}

		private void CalculationWakeupTime()
		{
			var dateTime = DateTime.Now;
			List<string> wakeUpTimeList = new List<string>(capacity: 6);
			for (int i = 1; i <= 6; i++)
			{
				var wu = dateTime.AddMinutes((90 * i + 15));
				wakeUpTimeList.Add(wu.ToString("h:mm tt", CultureInfo.CreateSpecificCulture("en-US"))); // Set CultureInfo ให้เป็น "en-US" เพื่อให้ format ของเวลาเป็นแบบ 12 Hours ที่มี AM/PM
			}

			WakeUpList = wakeUpTimeList;
		}

		private async void GotoBed_Clicked()
		{
			IsLoadingPanel = true;
			await Task.Delay(150);
			CalculationGoToBedTime();
			await PopupNavigation.Instance.PushAsync(new GoToBedPopup(BedtimeList));
			IsLoadingPanel = false;
		}

		private async void WakeUpTime_Clicked()
		{
			IsLoadingPanel = true;
			await Task.Delay(150);
			CalculationWakeupTime();
			await PopupNavigation.Instance.PushAsync(new WakeUpTimePopup(WakeUpList));
			IsLoadingPanel = false;
		}

		//private async void BackLabel_Tapped()
		//{
		//	await PopupNavigation.Instance.PopAsync();
		//}
	}
}
