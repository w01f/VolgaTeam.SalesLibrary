(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var SearchHelper = function ()
	{
		this.runSearch = function (searchCondition, beforeSearch, completeCallback, successCallBack)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/searchByContent",
				data: searchCondition,
				beforeSend: beforeSearch,
				complete: completeCallback,
				success: successCallBack,
				error: undefined,
				async: true,
				dataType: 'html'
			});
		};

		this.runSearchJson = function (searchCondition, beforeSearch, completeCallback, successCallBack)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/searchJson",
				data: searchCondition,
				beforeSend: beforeSearch,
				complete: completeCallback,
				success: successCallBack,
				error: undefined,
				async: true,
				dataType: 'json'
			});
		};
	};
	$.SalesPortal.SearchHelper = new SearchHelper();

	$.SalesPortal.SearchConditions = function (onChange)
	{
		var that = this;
		var data = undefined;

		var onChangeHandler = undefined;

		this.set = function (optionName, value)
		{
			data.simpleProperties[optionName] = value;
			that.raiseOnChange();
		};

		this.get = function (optionName)
		{
			return data.simpleProperties[optionName];
		};

		this.clear = function ()
		{
			data =
			{
				simpleProperties: {
					text: null,

					dateStart: null,
					dateEnd: null,

					onlyFileNames: false,
					exactMatch: true
				},

				fileTypes: {
					showPowerPoint: true,
					showVideo: true,
					showPdf: true,
					showWord: true,
					showExcel: true,
					showImages: true,
					showUrls: true,
					selectedTypeDescriptions: function ()
					{
						var descriptions = [];
						if (this.showPowerPoint &&
							this.showVideo &&
							this.showPdf &&
							this.showWord &&
							this.showExcel &&
							this.showImages &&
							this.showUrls)
							descriptions.push('All');
						else
						{
							if (this.showPowerPoint)
								descriptions.push('PowerPoint');
							if (this.showVideo)
								descriptions.push('Video');
							if (this.showPdf)
								descriptions.push('Adobe PDF');
							if (this.showWord)
								descriptions.push('Word');
							if (this.showExcel)
								descriptions.push('Excel');
							if (this.showImages)
								descriptions.push('JPEG/PNG');
							if (this.showUrls)
								descriptions.push('Web URL');
						}
						return descriptions;
					},
					selectedTypeTags: function ()
					{
						var tags = [];
						if (this.showPowerPoint)
							tags.push("ppt");
						if (this.showWord)
							tags.push("doc");
						if (this.showExcel)
							tags.push("xls");
						if (this.showPdf)
							tags.push("pdf");
						if (this.showVideo)
						{
							tags.push("video");
							tags.push("mp4");
							tags.push("wmv");
							tags.push("mp3");
						}
						if (this.showUrls)
						{
							tags.push("url");
							tags.push("url365");
						}
						if (this.showImages)
						{
							tags.push("png");
							tags.push("jpeg");
						}
						return tags;
					},
					loadFromTags: function (tags)
					{
						var thatData = this;
						$.each(tags, function (index, value)
						{
							if (value == 'ppt')
								thatData.showPowerPoint = true;
							else if (value == 'doc')
								thatData.showWord = true;
							else if (value == 'xls')
								thatData.showExcel = true;
							else if (value == 'pdf')
								thatData.showPdf = true;
							else if (value == 'video' || value == 'mp4' || value == 'wmv' || value == 'mp3')
								thatData.showVideo = true;
							else if (value == 'url' || value == 'url365')
								thatData.showUrls = true;
							else if (value == 'png' || value == 'jpeg')
								thatData.showImages = true;

						});
					}
				},

				categories: {
					items: [],
					selectedCategoriesDescriptions: function ()
					{
						if (this.items.length == 0)
							return [];
						var categories = [];
						$.each(this.items, function (groupIndex, group)
						{
							$.each(group.items, function (itemIndex, item)
							{
								categories.push(item);
							});
						});
						return categories;
					}
				},

				superFilters: [],
				libraries: {
					items: [],
					selectedLibrariesDescriptions: function ()
					{
						if (this.items.length == 0)
							return [];
						var libraryNames = [];
						$.each(this.items, function (index, value)
						{
							libraryNames.push(value.name);
						});
						return libraryNames;
					}
				}
			};

			if (onChangeHandler != undefined)
				onChangeHandler();
		};

		this.raiseOnChange = function ()
		{
			if (onChangeHandler != undefined)
				onChangeHandler();
		};

		this.getConditionsFormatted = function ()
		{
			return {
				text: data.simpleProperties.text,
				textExactMatch: data.simpleProperties.exactMatch,
				fileTypes: data.fileTypes.selectedTypeTags(),
				startDate: data.simpleProperties.dateStart,
				endDate: data.simpleProperties.dateEnd,
				libraries: data.libraries.items,
				superFilters: data.superFilters,
				categories: data.categories.items,
				onlyWithCategories: false,
				onlyByName: data.simpleProperties.onlyFileNames,
				baseDatasetKey: null
			};
		};

		this.loadFromConditionsFormatted = function (source)
		{
			data.simpleProperties.text = source.text;
			data.simpleProperties.exactMatch = source.textExactMatch;
			data.fileTypes.loadFromTags(source.fileTypes) ;
			data.simpleProperties.dateStart = source.startDate;
			data.simpleProperties.dateEnd = source.endDate;
			data.libraries.items = source.libraries;
			data.superFilters = source.superFilters;
			data.categories.items = source.categories;
			data.simpleProperties.onlyFileNames = source.onlyByName;
		};

		this.getFileTypesDescription = function ()
		{
			return data.fileTypes.selectedTypeDescriptions();
		};

		this.getFileTypesSettings = function ()
		{
			return data.fileTypes;
		};

		this.setFileTypesSettings = function (fileTypeSettings)
		{
			data.fileTypes.showPowerPoint = fileTypeSettings.showPowerPoint;
			data.fileTypes.showVideo = fileTypeSettings.showVideo;
			data.fileTypes.showPdf = fileTypeSettings.showPdf;
			data.fileTypes.showWord = fileTypeSettings.showWord;
			data.fileTypes.showExcel = fileTypeSettings.showExcel;
			data.fileTypes.showImages = fileTypeSettings.showImages;
			data.fileTypes.showUrls = fileTypeSettings.showUrls;
			that.raiseOnChange();
		};

		this.getCategoryDescription = function ()
		{
			return data.categories.selectedCategoriesDescriptions();
		};

		this.getCategorySettings = function ()
		{
			return data.categories.items;
		};

		this.setCategorySettings = function (items)
		{
			data.categories.items = items;
			that.raiseOnChange();
		};

		this.getSuperFiltersSettings = function ()
		{
			return data.superFilters;
		};

		this.setSuperFiltersSettings = function (items)
		{
			data.superFilters = items;
			that.raiseOnChange();
		};

		this.getLibrariesDescription = function ()
		{
			return data.libraries.selectedLibrariesDescriptions();
		};

		this.getLibrarySettings = function ()
		{
			return data.libraries.items;
		};

		this.setLibrarySettings = function (items)
		{
			data.libraries.items = items;
			that.raiseOnChange();
		};

		that.clear();
		onChangeHandler = onChange;
	};

	$.SalesPortal.SearchResult = function (datasetKey, dataset)
	{
		this.datasetKey = datasetKey;
		this.dataset = dataset;
	};
})(jQuery);