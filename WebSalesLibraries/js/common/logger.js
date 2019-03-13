(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var LogHelper = function () {

		this.write = function (actionData) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: actionData,
				async: true,
				dataType: 'json'
			});

			var type = actionData.data.originalFormat;

			if (typeof actionData.subType == 'undefined')
			{
				if (typeof type === 'undefined')
					type = actionData.type;
			}
			else
			{
				if (actionData.subType == 'Page Open')
					type = 'Library/' + actionData.data.Name;
				else
					type = actionData.subType;
			}

			var name = actionData.data.name;
			if (typeof name == 'undefined')
				name = actionData.type;
			if (actionData.subType == 'Page Open')
				name = actionData.data.pageName;


			if (actionData.subType == 'Preview Page')
				name = actionData.linkId + "/" + name;
			if (actionData.type == 'Shortcut Tile')
			{
				if (actionData.data.File != 'undefined')
					name = actionData.data.File;
				else
					name = "";
			}

			if (actionData.subType != "Search Activity")
			{
				if (actionData.type == "Shortcut Tile")
				{
					if (actionData.subType == "Landing page")
					{
						$.cookie("lastloc", actionData.linkId, {expires: 10});

						document.cookie = "lastloc=" + actionData.linkId + ";path=/";
						document.cookie = "lastname=" + actionData.data.File + ";path=/";
						document.cookie = "lasttype=" + actionData.subType + ";path=/";
					}
					else if (actionData.subType == "LibraryPage")
					{
						$.cookie("lastloc", actionData.linkId, {expires: 10});

						document.cookie = "lastloc=" + actionData.linkId + ";path=/";
						document.cookie = "lastname=" + actionData.data.File + ";path=/";
						document.cookie = "lasttype=" + actionData.subType + ";path=/";
					}
				}

				ga('send', {
					hitType: 'pageview',
					title: type + "/" + name,
					location: window.BaseUrl + "/" + actionData.data.linkId,
					page: type + "/" + name
				});
			}
		};
	};
	$.SalesPortal.LogHelper = new LogHelper();
})(jQuery);
