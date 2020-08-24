using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalculationBytes.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(Cal_PickerRenderer))]
namespace CalculationBytes.Droid.CustomRenderer
{
	public class Cal_PickerRenderer : PickerRenderer
	{
		public Cal_PickerRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
				Control.Gravity = GravityFlags.Center;

				//// creating gradient drawable for the curved background 
				//GradientDrawable gd = new GradientDrawable();
				////gd.SetShape(ShapeType.Rectangle);
				//gd.SetColor(Android.Graphics.Color.White);
				//// Thickness of the stroke line  
				//gd.SetStroke(2, Android.Graphics.Color.White);
				//// Radius for the curves  
				//gd.SetCornerRadius(Cal_EntryRenderer.Cal_EntryRenderer.DpToPixels(this.Context, Convert.ToSingle(10)));
				//// set the background of the   
				//Control.SetBackground(gd);
			}
		}
	}
}