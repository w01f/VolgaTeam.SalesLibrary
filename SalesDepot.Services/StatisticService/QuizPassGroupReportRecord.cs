﻿using System;
using System.Linq;

namespace SalesDepot.Services.StatisticService
{
	public partial class QuizPassGroupReportRecord
	{
		public string QuizzesPassed { get; set; }
		
		public int Taken { get; set; }
		public int Passed { get; set; }

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
