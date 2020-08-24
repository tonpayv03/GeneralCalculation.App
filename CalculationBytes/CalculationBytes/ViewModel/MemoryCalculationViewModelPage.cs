using CalculationBytes.Resources;
using CalculationBytes.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CalculationBytes.ViewModel
{
	public class MemoryCalculationViewModelPage : BaseViewModel
	{
		#region Properties
		private List<TypeClass> _fromType;
		public List<TypeClass> FromType
		{
			get { return _fromType; }
			set { if (_fromType != value) { _fromType = value; OnPropertyChanged(); } }
		}

		private List<TypeClass> _toType;
		public List<TypeClass> ToType
		{
			get { return _toType; }
			set { if (_toType != value) { _toType = value; OnPropertyChanged(); } }
		}

		private TypeClass _selectFromType;
		public TypeClass SelectFromType
		{
			get { return _selectFromType; }
			set { if (_selectFromType != value) { _selectFromType = value; OnPropertyChanged(); } }
		}

		private TypeClass _selectToType;
		public TypeClass SelectToType
		{
			get { return _selectToType; }
			set { if (_selectToType != value) { _selectToType = value; OnPropertyChanged(); } }
		}

		private string _numberOfSize;
		public string NumberOfSize
		{
			get { return _numberOfSize; }
			set
			{
				if (value.OnlyNumber())
				{
					if (_numberOfSize != value)
					{
						_numberOfSize = value;
						OnPropertyChanged();
						Convert_ClickCommand.ChangeCanExecute();
					}
				}
				else 
				{
					// ถ้าไม่มี OnPropertyChanged(); ถ้าใส่ค่ามาที่ไม่ใช่ตัวเลขตามที่ validate ไว้ 
					// ถ้าใส่ผิด value จะอัพเดทให้มีค่าที่ไม่ใช่ตัวเลข และโชว์ออกทางหน้าจอ
					// ต้องมี OnPropertyChanged(); เพื่อให้มันอัพเดท value ให้มันไม่เก็บค่าที่ไม่ใช่ตัวเลข
					// ถ้าใส่ผิด value จะเท่าเดิม ถ้าใส่ตัวเลข value จะอัพเดท
					OnPropertyChanged();
				}
			}
		}

		private string _countFrom;
		public string CountFrom
		{
			get { return _countFrom; }
			set { if (_countFrom != value) { _countFrom = value; OnPropertyChanged(); } }
		}

		private string _typeFrom;
		public string TypeFrom
		{
			get { return _typeFrom; }
			set { if (_typeFrom != value) { _typeFrom = value; OnPropertyChanged(); } }
		}

		private string _countTo;
		public string CountTo
		{
			get { return _countTo; }
			set { if (_countTo != value) { _countTo = value; OnPropertyChanged(); } }
		}

		private string _typeTo;
		public string TypeTo
		{
			get { return _typeTo; }
			set { if (_typeTo != value) { _typeTo = value; OnPropertyChanged(); } }
		}

		private Color _frameToPickerColor = Color.White;
		public Color FrameToPickerColor
		{
			get { return _frameToPickerColor; }
			set { if (_frameToPickerColor != value) { _frameToPickerColor = value; OnPropertyChanged(); } }
		}
		#endregion

		#region Declare Command
		public Command Convert_ClickCommand { get; set; }
		public Command SwitchType_ClickCommand { get; set; }

		#endregion


		public MemoryCalculationViewModelPage()
		{
			Convert_ClickCommand = new Command(Calculation, () =>
			{
				if (string.IsNullOrEmpty(NumberOfSize))
				{
					return false;
				}
				else
				{
					return true;
				}
			});

			SwitchType_ClickCommand = new Command(SwithType);

			SetDataType();
		}

		private void SwithType()
		{
			if (SelectFromType != null && SelectToType != null)
			{
				var fromtype = SelectFromType;
				var totype = SelectToType;

				SelectFromType = totype;
				SelectToType = fromtype;
			}
		}

		private void SetDataType()
		{
			var listType = new List<TypeClass>()
			{
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Bit" , TypeName = MemoryAndSpeedResource.bit_tyte , Exponent = 0 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Kb" ,  TypeName = MemoryAndSpeedResource.Kbit , Exponent = 1 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Mb" ,  TypeName = MemoryAndSpeedResource.Mbit, Exponent = 2 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Gb" ,  TypeName = MemoryAndSpeedResource.Gbit, Exponent = 3 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Tb" ,  TypeName = MemoryAndSpeedResource.Tbit, Exponent = 4 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Pb" ,  TypeName = MemoryAndSpeedResource.Pbit, Exponent = 5 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Bit , TypeNameForSelect = "Eb" ,  TypeName = MemoryAndSpeedResource.Ebit, Exponent = 6 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "Byte" , TypeName = MemoryAndSpeedResource.byte_type, Exponent = 0 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "KB" ,  TypeName = MemoryAndSpeedResource.KByte, Exponent = 1 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "MB" ,  TypeName = MemoryAndSpeedResource.MByte, Exponent = 2 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "GB" ,  TypeName = MemoryAndSpeedResource.GByte, Exponent = 3 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "TB" ,  TypeName = MemoryAndSpeedResource.TByte, Exponent = 4 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "PB" ,  TypeName = MemoryAndSpeedResource.PByte, Exponent = 5 },
				new TypeClass(){ DataType = Enums.Enum.DataType.Byte , TypeNameForSelect = "EB" ,  TypeName = MemoryAndSpeedResource.EByte, Exponent = 6 },
			};

			FromType = listType;
			ToType = listType;
		}

		private void Calculation()
		{
			if (!IsCheckPicker())
			{
				return;
			}

			var byteValue = CalculationToByte();
			var result = CalculationValueConvert(byteValue);

			var index = result.ToString().IndexOf('.'); // หาตำแหน่งของจุด
			var resultNoDot = result.ToString().Substring(index + 1); // substring เอาเฉพาะหลังจุด
			int countZero = 0;

			foreach (var item in resultNoDot)
			{
				if (item.ToString() == "0") // หาว่าหลังจุด มี 0 กี่ตัว
				{
					countZero++;
				}
				else
				{
					break;
				}
			}

			var decimals = 0;
			if (countZero <= 2) // น้อยกว่าหรือเท่ากับ 2 ตัว ให้มีทศนิยม 8 ตำแหน่ง
			{
				decimals = 8;
			}
			else if (countZero > 2) // มากกว่า 2 ตัว ให้มีทศนิยม 10 ตำแหน่ง
			{
				decimals = 10;
			}
			CountTo = string.Format("{0}", Math.Round(result, decimals));
			CountFrom = NumberOfSize;
		}

		private double CalculationToByte()
		{
			double byteValue = 0;
			double number = 0;
			bool check = double.TryParse(NumberOfSize, out number);

			// Math.Pow(1024, 3) (value,จำนวนที่ต้องการยกกำลัง) ยกกำลังแล้วเอามาคูณ จะได้ค่าเพิ่มขึ้น
			switch (SelectFromType.DataType)
			{
				case Enums.Enum.DataType.Bit:
					byteValue = Convert.ToDouble(number) / 8 * Convert.ToDouble(Math.Pow(1024, SelectFromType.Exponent));
					TypeFrom = SelectFromType.TypeName;
					break;
				case Enums.Enum.DataType.Byte:
					byteValue = Convert.ToDouble(number) * Convert.ToDouble(Math.Pow(1024, SelectFromType.Exponent));
					TypeFrom = SelectFromType.TypeName;
					break;
				default:
					break;
			}

			NumberOfSize = number.ToString();

			return byteValue;
		}

		private decimal CalculationValueConvert(double byteValue)
		{
			decimal result = 0;
			// Math.Pow(1024, 3)(value,จำนวนที่ต้องการยกกำลัง) ยกกำลังแล้วเอามาหาร จะได้ค่าลดลง
			switch (SelectToType.DataType)
			{
				case Enums.Enum.DataType.Bit:
					result = Convert.ToDecimal(byteValue * 8 / Math.Pow(1024, SelectToType.Exponent));
					TypeTo = SelectToType.TypeName;
					break;
				case Enums.Enum.DataType.Byte:
					result = Convert.ToDecimal(byteValue / Math.Pow(1024, SelectToType.Exponent));
					TypeTo = SelectToType.TypeName;
					break;
				default:
					break;
			}

			return result;
		}

		private bool IsCheckPicker()
		{
			FrameToPickerColor = Color.White;
			if (SelectFromType == null && SelectToType == null)
			{
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_from_to_type, StringResource.ok);
				return false;
			}
			else if (SelectFromType == null)
			{
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_from_type, StringResource.ok);
				return false;
			}
			else if (SelectToType == null)
			{
				Application.Current.MainPage.DisplayAlert("", StringResource.choose_to_type, StringResource.ok);
				return false;
			}
			else if (SelectFromType.TypeNameForSelect == SelectToType.TypeNameForSelect)
			{
				FrameToPickerColor = Color.Red;
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	public class TypeClass
	{
		public Enums.Enum.DataType DataType { get; set; }
		public string TypeNameForSelect { get; set; }
		public string ThaiTypeName { get; set; }
		public double Exponent { get; set; }
		public string TypeName { get; set; }
	}
}
