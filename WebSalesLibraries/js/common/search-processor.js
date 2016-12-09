(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var SearchHelper = function ()
	{
		this.runSearch = function (searchCondition, beforeSearch, completeCallback, successCallBack)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/search",
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
					exactMatch: true,
					onlyNewFiles: false
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
						if (this.showPowerPoint &&
							this.showWord &&
							this.showExcel &&
							this.showPdf &&
							this.showVideo &&
							this.showUrls &&
							this.showImages
						)
							return tags;
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
							tags.push("youtube");
							tags.push("lan");
							tags.push("quicksite");
							tags.push("html5");
							tags.push("app");
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
							else if (value == 'video' ||
								value == 'mp4' ||
								value == 'wmv' ||
								value == 'mp3')
								thatData.showVideo = true;
							else if (value == 'url' ||
								value == 'youtube' ||
								value == 'lan' ||
								value == 'quicksite' ||
								value == 'html5' ||
								value == 'app' ||
								value == 'internal library' ||
								value == 'internal page' ||
								value == 'internal window' ||
								value == 'internal link' ||
								value == 'internal shortcut')
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
			var startDate = data.simpleProperties.dateStart;
			var endDate = data.simpleProperties.dateEnd;
			if (startDate == null &&
				endDate == null &&
				data.simpleProperties.onlyNewFiles == true)
			{
				var currentDate = new Date();
				endDate = currentDate.toLocaleDateString();
				currentDate.setFullYear(currentDate.getFullYear() - 1);
				startDate = currentDate.toLocaleDateString();
			}
			return {
				text: data.simpleProperties.text,
				textExactMatch: data.simpleProperties.exactMatch,
				fileTypes: data.fileTypes.selectedTypeTags(),
				startDate: startDate,
				endDate: endDate,
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
			data.fileTypes.loadFromTags(source.fileTypes);
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

	$.SalesPortal.SearchViewOptions = function (source)
	{
		this.showCategory = undefined;
		this.categoryColumnName = undefined;
		this.showLibraries = undefined;
		this.librariesColumnName = undefined;
		this.showType = undefined;
		this.showDate = undefined;
		this.showRate = undefined;
		this.showViewsCount = undefined;
		this.showDeleteButton = undefined;
		this.reorderSourceField = undefined;

		for (var prop in source)
			if (source.hasOwnProperty(prop))
				this[prop] = source[prop];
	};

})(jQuery);