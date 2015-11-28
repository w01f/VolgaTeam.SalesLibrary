using System;
using System.IO;
using System.Linq;

namespace SalesLibraries.Business.Entities.Calendars
{
	public class CalendarDay
	{
		private FileInfo _linkedFile;
		private bool _linkedFileReady;

		public CalendarDay(CalendarMonth parent)
		{
			Parent = parent;
		}

		public CalendarMonth Parent { get; private set; }
		public DateTime Date { get; set; }

		public bool IsSweepDay { get; set; }

		public FileInfo LinkedFile
		{
			get
			{
				if (!_linkedFileReady)
				{
					_linkedFileReady = true;
					_linkedFile = Parent.Parent.Parent.Files.FirstOrDefault(fileInfo => fileInfo.Name.Contains(Date.ToString("MMddyy")));
				}
				return _linkedFile;
			}
		}

		public void Reset()
		{
			_linkedFileReady = false;
		}
	}
}
