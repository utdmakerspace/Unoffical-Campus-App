using System;
namespace Makerspace
{
	public class DiningItem
	{
		
		public string Name { get; set; }
		public Timings Timings { get; set; }
		public string image { get; set; }

		string currentTime = "No timings available";

		public string currentTimings {
			get 
			{
				var day = DateTime.Now.DayOfWeek;

				switch(day)
				{
					case DayOfWeek.Sunday:
						currentTime = Timings.Sunday;
						break;
					case DayOfWeek.Monday:
						currentTime = Timings.Monday;
						break;
					case DayOfWeek.Tuesday:
						currentTime = Timings.Tuesday;
						break;	
					case DayOfWeek.Wednesday:
						currentTime = Timings.Wednesday;
						break;
					case DayOfWeek.Thursday:
						currentTime = Timings.Thursday;
						break;
					case DayOfWeek.Friday:
						currentTime = Timings.Friday;
						break;
					case DayOfWeek.Saturday:
						currentTime = Timings.Saturday;
						break;
				}


				return currentTime; 
			}
		}


	}


	public class Timings
	{
		public string Monday { get; set; }
		public string Tuesday { get; set; }
		public string Wednesday { get; set; }
		public string Thursday { get; set; }
		public string Friday { get; set; }
		public string Saturday { get; set; }
		public string Sunday { get; set; }
	}

}

