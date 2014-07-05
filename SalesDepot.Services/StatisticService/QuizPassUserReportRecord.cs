using System;
using System.Linq;

namespace SalesDepot.Services.StatisticService
{
	public partial class QuizPassUserReportRecord
	{
		private string _fullName;
		public string FullName
		{
			get
			{
				return String.IsNullOrEmpty(_fullName) ?
				(firstName + " " + lastName).Trim() :
				_fullName;
			}
			set
			{
				_fullName = value;
			}
		}

		private string _groupName;
		public string GroupName
		{
			get
			{
				return String.IsNullOrEmpty(_groupName) ?
					(!String.IsNullOrEmpty(group) ? group : null) :
					_groupName;
			}
			set
			{
				_groupName = value;
			}
		}

		public string QuizzesPassed { get; set; }
		public string TotalPassed { get; set; }

		public DateTime? QuizPassDate
		{
			get
			{
				DateTime temp;
				if (DateTime.TryParse(quizPassDate, out temp))
					return temp;
				return null;
			}
		}

		public DateTime? Date
		{
			get
			{
				if (String.IsNullOrEmpty(quizName)) return null;
				var quizNameParts = quizName.Split('-');
				if (!quizNameParts.Any()) return null;
				DateTime temp;
				if (!DateTime.TryParse(quizNameParts.Last().Trim(), out temp)) return null;
				return temp;
			}
		}
	}
}
