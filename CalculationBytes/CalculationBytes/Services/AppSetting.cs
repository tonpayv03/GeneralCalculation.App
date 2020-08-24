using CalculationBytes.Resources;
using CalculationBytes.Services.Interfaces;
using CalculationBytes.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CalculationBytes.Services
{
	public class AppSetting : BaseViewModel
	{

		public const string Thai = "TH";
		public const string English = "EN";

		private string _language;
		public string Language
		{
			get { return _language; }
			set
			{
				if (_language != value)
				{
					_language = value; 
					OnPropertyChanged();
					SaveProperty("language", value);
					SyncLanguage();
				}
			}
		}

		#region Singleton Design Pattern
		// Object ของเราจะมีแค่ ตัวเดียวในโปรแกรม
		private static AppSetting _current;
		public static AppSetting Current
		{
			get { return _current; }
		}

		public static void Init()
		{
			if (_current == null)
			{
				_current = new AppSetting();
			}
		}
		#endregion
		public AppSetting()
		{
			SetLanguage();
		}

		private void SetLanguage()
		{
			if (Application.Current.Properties.ContainsKey("language"))
			{
				// load saved language
				string savedLanguage = Application.Current.Properties["language"] as string;
				Language = savedLanguage;
			}
			else
			{
				//string systemLanguage = DependencyService.Get<ILocalize>().GetSystemLanguage();
				var systemLanguage = CultureInfo.CurrentCulture.Parent.ToString();
				if (systemLanguage == Thai)
				{
					Language = Thai;
				}
				else
				{
					Language = English;
				}
			}
			SyncLanguage();
		}

		private void SyncLanguage()
		{
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = new CultureInfo(Language); // en , th
			}
			catch (Exception ex)
			{
				throw new Exception("Error new culture,", ex);
			}

			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			// Set culture ที่เลือกให้ CurrentUI พวกตัวแปรที่เป็น Resources ภาษา จะเป็นไปเป็นภาษาตามที่เราเลือก
			CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
		}

		private void SaveProperty(string propertyName, object val)
		{
			System.Diagnostics.Debug.WriteLine("AppSetting: SaveProperty " + propertyName + " ...");
			Device.BeginInvokeOnMainThread(async () =>
			{
				try
				{
					Application.Current.Properties[propertyName] = val;
					System.Diagnostics.Debug.WriteLine("AppSetting: SavePropertiesAsync " + propertyName + " ...");
					await Application.Current.SavePropertiesAsync();
					System.Diagnostics.Debug.WriteLine("AppSetting: SavePropertiesAsync ok");
					System.Diagnostics.Debug.WriteLine("AppSetting: SaveProperty " + propertyName + " done");
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("AppSetting: error occurs while saving property : " + ex.GetType().Name + " " + ex);
				}
			});
		}
	}
}
