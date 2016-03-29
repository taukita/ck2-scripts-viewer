using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Ck2ScriptsViewer.MicroMvvm
{
	[Serializable]
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected void RaisePropertyChanged([CallerMemberName] String propertyName = "")
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected void RaisePropertyChanged<T>(Expression<Func<T>> changedProperty)
		{
			string propertyName = ((MemberExpression)changedProperty.Body).Member.Name;
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
	}
}
