using CalculationBytes.Resources;
using CalculationBytes.Services.Markup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CalculationBytes.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
	{

		private bool _isLoadingPanel = false;
		public bool IsLoadingPanel
		{
			get { return _isLoadingPanel; }
			set { if (_isLoadingPanel != value) { _isLoadingPanel = value; OnPropertyChanged(); } }
		}

		public BaseViewModel()
		{
			
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
