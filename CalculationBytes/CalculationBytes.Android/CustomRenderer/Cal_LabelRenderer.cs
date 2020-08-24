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
using CalculationBytes.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(Cal_LabelRenderer))]

namespace CalculationBytes.Droid.CustomRenderer
{
	public class Cal_LabelRenderer : LabelRenderer
	{
		public Cal_LabelRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				//Control?.SetIncludeFontPadding(false);
			}
		}
	}
}