using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalculationBytes.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalculationBytes.Droid.Services.Localize))]
namespace CalculationBytes.Droid.Services
{
	public class Localize : ILocalize
	{
		public Localize()
		{

		}
		public string GetSystemLanguage()
		{
			return Java.Util.Locale.Default.Language;
		}
	}
}