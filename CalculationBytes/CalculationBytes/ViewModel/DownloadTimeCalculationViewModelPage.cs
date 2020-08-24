using CalculationBytes.Models;
using CalculationBytes.Resources;
using CalculationBytes.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalculationBytes.ViewModel
{
	public class DownloadTimeCalculationViewModelPage : BaseViewModel
	{
		#region Properties
		public List<FileSizeModel> FileSize { get; set; }
		public List<SpeedSizeModel> SpeedSize { get; set; }

		private string _numberOfFileSize;
		public string NumberOfFileSize
		{
			get { return _numberOfFileSize; }
			set
			{
				if (value.OnlyNumber())
				{
					if (_numberOfFileSize != value)
					{
						_numberOfFileSize = value;
						OnPropertyChanged();
					}
				}
				else
				{
					OnPropertyChanged();
				}
			}
		}

		private string _numberOfSpeed;
		public string NumberOfSpeed
		{
			get { return _numberOfSpeed; }
			set
			{
				if (value.OnlyNumber())
				{
					if (_numberOfSpeed != value)
					{
						_numberOfSpeed = value;
						OnPropertyChanged();
					}
				}
				else
				{
					OnPropertyChanged();
				}
			}
		}

		private FileSizeModel _selectFileType;
		public FileSizeModel SelectFileType
		{
			get { return _selectFileType; }
			set { if (_selectFileType != value) { _selectFileType = value; OnPropertyChanged(); } }
		}

		private SpeedSizeModel _selectSpeedType;
		public SpeedSizeModel SelectSpeedType
		{
			get { return _selectSpeedType; }
			set { if (_selectSpeedType != value) { _selectSpeedType = value; OnPropertyChanged(); } }
		}

		private Color _frameFilePickerColor;
		public Color FrameFilePickerColor
		{
			get { return _frameFilePickerColor; }
			set { if (_frameFilePickerColor != value) { _frameFilePickerColor = value; OnPropertyChanged(); } }
		}

		private Color _frameSpeedPickerColor;
		public Color FrameSpeedPickerColor
		{
			get { return _frameSpeedPickerColor; }
			set { if (_frameSpeedPickerColor != value) { _frameSpeedPickerColor = value; OnPropertyChanged(); } }
		}

		private string _downLoadTime;
		public string DownLoadTIme
		{
			get { return _downLoadTime; }
			set { if (_downLoadTime != value) { _downLoadTime = value; OnPropertyChanged(); } }
		}

		private string _speedPerSec;
		public string SpeedPerSec
		{
			get { return _speedPerSec; }
			set { if (_speedPerSec != value) { _speedPerSec = value; OnPropertyChanged(); } }
		}
		#endregion

		public Command Calculate_ClickCommand { get; set; }
		public DownloadTimeCalculationViewModelPage()
		{
			FileSize = new List<FileSizeModel>()
			{
				new FileSizeModel(){ TypeSize = "KB", Exponent = 1 },
				new FileSizeModel(){ TypeSize = "MB", Exponent = 2 },
				new FileSizeModel(){ TypeSize = "GB", Exponent = 3 },
			};

			SpeedSize = new List<SpeedSizeModel>()
			{
				new SpeedSizeModel(){ TypeSize = "Kbps", Exponent = 1 , TypeSizePerSec = MemoryAndSpeedResource.type_name_per_sec_KB }, // 1024
				new SpeedSizeModel(){ TypeSize = "Mbps", Exponent = 2 , TypeSizePerSec = MemoryAndSpeedResource.type_name_per_sec_MB }, // 1024*1024 = 1048576
				new SpeedSizeModel(){ TypeSize = "Gbps", Exponent = 3 , TypeSizePerSec = MemoryAndSpeedResource.type_name_per_sec_GB }, // 1024*1024*1024 = 1073741824
			};

			Calculate_ClickCommand = new Command(Calculate, () => false);
		}

		private void Calculate()
		{
			if (!IsCheckPicker())
			{
				return;
			}

			double fileSize = 0;
			double ownSpeed = 0;
			bool check = double.TryParse(NumberOfFileSize, out fileSize);
			bool checkNumSpeed = double.TryParse(NumberOfSpeed, out ownSpeed);

			CalculationDownLoadTime(fileSize, ownSpeed);
			CalculationSpeedPerSec(ownSpeed);
		}

		private void CalculationDownLoadTime(double fileSize, double ownSpeed)
		{

			var filetime = (fileSize * Convert.ToDouble(Math.Pow(1024, SelectFileType.Exponent)) * 8) / (ownSpeed * Convert.ToDouble(Math.Pow(1024, SelectSpeedType.Exponent)));

			var hourmod = filetime % 3600;
			var hour = Math.Floor(filetime / 3600);
			var minute = Math.Floor(hourmod / 60);
			var second = Math.Floor(filetime % 60);

			var hourStr = string.Empty;
			var minuteStr = string.Empty;
			var secondStr = string.Empty;

			if (hour <= 9)
			{
				hourStr = $"0{hour}";
			}
			else
			{
				hourStr = hour.ToString();
			}
			if (minute <= 9)
			{
				minuteStr = $"0{minute}";
			}
			else
			{
				minuteStr = minute.ToString();
			}
			if (second <= 9)
			{
				secondStr = $"0{second}";
			}
			else
			{
				secondStr = second.ToString();
			}

			DownLoadTIme = hourStr + ":" + minuteStr + ":" + secondStr;
		}

		private void CalculationSpeedPerSec(double ownSpeed)
		{
			double byteValue = 0;
			var downloadSpeedStr = string.Empty;

			var bitDevide = Convert.ToDouble(ownSpeed) / 8;

			if (bitDevide < 1)
			{
				// ถ้าน้อยกว่า 1 เอาไปคำนวณหาค่าเพื่อแสดง downloadSpeed ที่ต่ำกว่า type ที่เลือก
				byteValue = bitDevide * Convert.ToDouble(Math.Pow(1024, SelectSpeedType.Exponent));
				var result = Convert.ToDecimal(byteValue / Math.Pow(1024, SelectSpeedType.Exponent - 1));

				if (SelectSpeedType.Exponent == 1) // Type Kbps ให้แสดง Byte/s
				{
					downloadSpeedStr = $"{result} {MemoryAndSpeedResource.type_name_per_sec_Byte}";
				}
				else
				{
					// Type อื่นให้แสดง Type ที่ตำกว่า 1 step
					downloadSpeedStr = $"{result} {SpeedSize[SelectSpeedType.Exponent - 2].TypeSizePerSec}";
				}
			}
			else
			{  // ถ้ามากกว่าเท่ากับ 1 แสดง Type ที่เลือกเลย
				downloadSpeedStr = $"{bitDevide} {SelectSpeedType.TypeSizePerSec}";
			}
			SpeedPerSec = downloadSpeedStr;
		}

		private bool IsCheckPicker()
		{
			FrameFilePickerColor = Color.White;
			FrameSpeedPickerColor = Color.White;
			if (SelectFileType == null && SelectSpeedType == null)
			{
				FrameFilePickerColor = Color.Red;
				FrameSpeedPickerColor = Color.Red;
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_file_speed_type, StringResource.ok);
				return false;
			}
			else if (SelectFileType == null)
			{
				FrameFilePickerColor = Color.Red;
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_file_type, StringResource.ok);
				return false;
			}
			else if (SelectSpeedType == null)
			{
				FrameSpeedPickerColor = Color.Red;
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_speed_type, StringResource.ok);
				return false;
			}
			else
			{
				return true;
			}
		}

	}

	public class FileSizeModel : BaseTypeModel
	{
	}

	public class SpeedSizeModel : BaseTypeModel
	{
		public string TypeSizePerSec { get; set; }
	}
}
