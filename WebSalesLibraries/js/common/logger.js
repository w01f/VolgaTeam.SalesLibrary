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
			if (actionData.subType != "QBuilder Activity" && actionData.subType != "QBuilder" && actionData.subType != "Search Activity")
			{
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

	$(document).on('click', '.sales-requests-item-content-additional-sections ul li', function () {
		var name = $('.sales-requests-item-list-row.selected td:first-child .clickable-area').attr('title');
		ga('send', {
			hitType: 'pageview',
			title: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + name + "/" + $(this).text(),
			location: window.BaseUrl + $('.item-list-tabs ul li.active').text(),
			page: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + name + "/" + $(this).text()
		});

	});

	$(document).on('click', '.sales-contest-item-content-additional-sections ul li', function () {
		var name = $('.sales-contest-item-list-row.selected td:first-child .clickable-area').attr('title');
		ga('send', {
			hitType: 'pageview',
			title: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + name + "/" + $(this).text(),
			location: window.BaseUrl + $('.item-list-tabs ul li.active').text(),
			page: $('.item-list-tabs ul li.active').text().split('(')[0] + "/" + name + "/" + $(this).text()
		});

	});
	$(document).on('click', '.item-list-tabs ul li', function () {

		ga('send', {
			hitType: 'pageview',
			title: $(this).text().split('(')[0] + "/",
			location: window.BaseUrl + $(this).text().split('(')[0],
			page: $(this).text().split('(')[0] + "/"
		});

	});
	$(document).on('click', '.fc-next-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/next",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/next"
		});
	});
	$(document).on('click', '.fc-prev-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/prev",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/prev"
		});
	});
	$(document).on('click', '.fc-today-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/today",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/today"
		});
	});
	$(document).on('click', '.fc-agendaWeek-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/agendaWeek",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/agendaWeek"
		});
	});
	$(document).on('click', '.fc-month-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/month",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/month"
		});
	});

	$(document).on('click', '.fc-agendaDay-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/agendaDay",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/agendaDay"
		});
	});
	$(document).on('click', '.fc-listWeek-button', function () {
		ga('send', {
			hitType: 'pageview',
			title: $('.header-text').text() + "/listWeek",
			location: window.BaseUrl + $('.header-text').text(),
			page: $('.header-text').text() + "/listWeek"
		});
	});


	$(document).on('click', 'th.rate-image-container', function () {
		ga('send', {
			hitType: 'pageview',
			title: "Search Results/" + $.cookie('search-key') + "/" + "rateCell",
			location: window.BaseUrl + "Search Results/",
			page: "Search Results/" + $.cookie('search-key') + "/" + "rateCell"

		});
	});
	$(document).on('click', 'th.all', function () {

		if ($(this).hasClass('sorting_desc'))
		{
			ga('send', {
				hitType: 'pageview',
				title: "Search Results/" + $.cookie('search-key') + "/" + $(this).text() + "/desc",
				location: window.BaseUrl + "Search Results/",
				page: "Search Results/" + $.cookie('search-key') + "/" + $(this).text() + "/desc"
			});
		}
		else
		{
			ga('send', {
				hitType: 'pageview',
				title: "Search Results/" + $.cookie('search-key') + "/" + $(this).text() + "/asc",
				location: window.BaseUrl + "Search Results/",
				page: "Search Results/" + $.cookie('search-key') + "/" + $(this).text() + "/asc"
			});
		}


	});
	$(document).on('click', '.dataTables_wrapper .back-url', function () {

		ga('send', {
			hitType: 'pageview',
			title: $(this).text(),
			location: window.BaseUrl,
			page: $(this).text()
		});

	});
	$(document).on('click', 'ul.selectpicker li', function () {

		ga('send', {
			hitType: 'pageview',
			title: "Search Results/" + $.cookie('search-key') + "/items/" + $(this).text(),
			location: window.BaseUrl + "Search Results/",
			page: "Search Results/" + $.cookie('search-key') + "/items/" + $(this).text()
		});
	});
	$(document).on('click', 'li.paginate_button', function () {
		ga('send', {
			hitType: 'pageview',
			title: "Search Results/" + $.cookie('search-key') + "/" + $(this).text(),
			location: window.BaseUrl + "Search Results/",
			page: "Search Results/" + $.cookie('search-key') + "/" + $(this).text()
		});


	});

})(jQuery);
