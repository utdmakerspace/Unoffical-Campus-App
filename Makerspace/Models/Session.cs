using System;
using Xamarin.Forms;

namespace Makerspace
{
	public class Session
	{
		public DateTime end { get; set; }
		public DateTime start { get; set; }
		public string Start { get; set;}
		public string End { get; set; }
		public string @abstract { get; set; }
		public string title { get; set; }
		public string presenter { get; set; }
		public string biography { get; set; }
		public string image { get; set; } = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRw7N10JPWrASTfN7e5eWzGQVm93nsv7GfUNhDf2P8_0lpdnUgSbDIBy8s"; //default comets image
		public string room { get; set; }
		public int attendeeCount { get; set;}
		public string theme { get; set;} = "#008080"; //Default color teal 

		public Color color
		{
			get { return Color.FromHex(theme);}
		
		}	
	}
}

