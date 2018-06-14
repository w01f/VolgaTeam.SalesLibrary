(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsQuizzes = function ()
	{
		var quizzesData = undefined;

		this.init = function (data)
		{
			quizzesData = data;

			$.SalesPortal.Content.fillContent({
				content: quizzesData.content,
				headerOptions: quizzesData.options.headerOptions,
				actions: quizzesData.actions,
				navigationPanel: quizzesData.navigationPanel,
				fixedPanels: quizzesData.fixedPanels,
				resizeCallback: updateContentSize
			});

			$.SalesPortal.QuizManager.loadItems();

			initActionButtons();

			$(window).off('resize.quizzes').on('resize.quizzes', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.QuizManager.updateContentSize();
		};
	};
})(jQuery);
