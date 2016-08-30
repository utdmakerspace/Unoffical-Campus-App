using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Makerspace
{
	public class EventListHelper
	{
		public EventListHelper()
		{
		}

		public static async Task addNewEvent(Session newEvent)
		{
			var eventList = JsonConvert.DeserializeObject<List<Session>>(EventsWriterHelper.oldImage);
			eventList.Insert(0, newEvent);
			EventsWriterHelper.newImage = JsonConvert.SerializeObject(eventList);
			await EventsWriterHelper.pushChanges();
		}

		public static async Task addNewEvent(Session newEvent, int index)
		{
			var eventList = JsonConvert.DeserializeObject<List<Session>>(EventsWriterHelper.oldImage);
			eventList.Insert(index, newEvent);
			EventsWriterHelper.newImage = JsonConvert.SerializeObject(eventList);
			await EventsWriterHelper.pushChanges();
		}

		public static async Task removeEvent(int eventIndex)
		{
			var eventList = JsonConvert.DeserializeObject<List<Session>>(EventsWriterHelper.oldImage);
			eventList.RemoveAt(eventIndex);
			EventsWriterHelper.newImage = JsonConvert.SerializeObject(eventList);
			await EventsWriterHelper.pushChanges();
		}

		public static async Task incrementAttendeeCount()
		{
			var eventList = JsonConvert.DeserializeObject<List<Session>>(EventsWriterHelper.oldImage);
			eventList[0].attendeeCount += 1;
			EventsWriterHelper.newImage = JsonConvert.SerializeObject(eventList);
			await EventsWriterHelper.pushChanges();
		}

	}
}

