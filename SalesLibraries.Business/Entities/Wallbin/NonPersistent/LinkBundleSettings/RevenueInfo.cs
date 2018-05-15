using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class RevenueInfo : ICollectionItem
	{
		public const string InfoTypeBroadcast = "Broadcast";
		public const string InfoTypeDigital = "Digital";
		public const string InfoTypeMobile = "Mobile";
		public const string InfoTypeNonSpot = "Non-Spot (NTR)";
		public const string InfoTypeEvents = "Events";
		public const string InfoTypeProduction = "Production";
		public const string InfoTypeTotal = "Total Revenue";

		public static string[] AvailableInfoTypes = {
			InfoTypeBroadcast,
			InfoTypeDigital,
			InfoTypeMobile,
			InfoTypeNonSpot,
			InfoTypeEvents,
			InfoTypeProduction,
			InfoTypeTotal
		};

		public string Title { get; set; }

		private int _collectionOrder;
		public int CollectionOrder
		{
			get => _collectionOrder;
			set
			{
				if (_collectionOrder != value)
					ParentRevenue?.MarkAsModified();
				_collectionOrder = value;
			}
		}

		private decimal? _value;
		public decimal? Value
		{
			get => _value;
			set
			{
				if (_value != value)
					ParentRevenue?.MarkAsModified();
				_value = value;
			}
		}

		[JsonIgnore]
		public RevenueItem ParentRevenue { get; set; }

		[JsonIgnore]
		public IChangable Parent
		{
			get => ParentRevenue;
			set => ParentRevenue = value as RevenueItem;
		}

		[JsonConstructor]
		private RevenueInfo() { }

		public RevenueInfo(RevenueItem parent, string title)
		{
			ParentRevenue = parent;
			Title = title;
		}
	}
}
