using System;
using Xamarin.Forms;

namespace Makerspace
{
	public class AndroidMenuItem
	{
		public string Title { get; set; }

		public string IconSource { get; set; }

		public Color theme { get; set; } = Color.Black;

		public Type TargetType { get; set; }
	}
}

