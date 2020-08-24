using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalculationBytes.Droid.Cal_EntryRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(Cal_EntryRenderer))]
namespace CalculationBytes.Droid.Cal_EntryRenderer
{
	public class Cal_EntryRenderer : EntryRenderer
	{
		public Cal_EntryRenderer(Context context) : base(context)
		{

		}
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
				Control.Gravity = GravityFlags.Center;

				// creating gradient drawable for the curved background 
				GradientDrawable gd = new GradientDrawable();
				//gd.SetShape(ShapeType.Rectangle);
				gd.SetColor(Android.Graphics.Color.White);
				// Thickness of the stroke line  
				gd.SetStroke(2, Android.Graphics.Color.White);
				// Radius for the curves  
				gd.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(8)));
				// set the background of the   
				Control.SetBackground(gd);
			}
		}
		public static float DpToPixels(Context context, float valueInDp)
		{
			Android.Util.DisplayMetrics metrics = context.Resources.DisplayMetrics;
			return Android.Util.TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, valueInDp, metrics);
		}
	}
}