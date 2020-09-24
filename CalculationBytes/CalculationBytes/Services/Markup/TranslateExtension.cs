using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculationBytes.Services.Markup
{
	[ContentProperty("Text")]
	public class TranslateExtension : ViewModel.BaseViewModel, IMarkupExtension
	{
		readonly CultureInfo culInfo;
		const string ResourcePath = "Resources";

		public string Name { get; set; }
		public string ClassName { get; set; }

		ResourceManager resmgr;

		// พอ PushAsync(new Page()) มา เมื่อเรียกใช้ Text="{Tran:Translate Name=convert,ClassName=StringResource}" มันจะมาทำทุกครั้งที่เรียก ก็จะมีการเปลี่ยนภาษาเกิดขึ้น
		public TranslateExtension()
		{
			culInfo = CultureInfo.DefaultThreadCurrentCulture;
		}

		// จะได้ string ตามภาษา return กลับไปโชว์
		public object ProvideValue(IServiceProvider serviceProvider)
		{
			// แบบ Get Assembly มาก่อน แล้วค่อยไป Get Name
			var assembly = typeof(App).GetTypeInfo().Assembly;
			string _namespace = assembly.GetName().Name;
			//// แบบ Get ไปที่ Namespace แทน
			//string _namespace = typeof(App).Namespace;
			string _className = ClassName;

			string _resourceId = _namespace + "." + ResourcePath + "." + _className;

			if (Name == null)
				return "";

			resmgr = new ResourceManager(_resourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

			var translate = resmgr.GetString(Name, culInfo);

			return translate;
		}
	}
}
