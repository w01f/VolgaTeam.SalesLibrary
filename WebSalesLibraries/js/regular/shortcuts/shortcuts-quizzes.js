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

			$.SalesPortal.Content.fillContent(
				quizzesData.content,
				{
					title: quizzesData.options.headerTitle,
					icon: quizzesData.options.headerIcon
				},
				quizzesData.actions
			);

			$.SalesPortal.QuizManager.loadItems();

			initActionButtons();

			$(window).off('resize.quizzes').on('resize.quizzes', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			//var shortcutActionsContainer = $('#shortcut-action-container');
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.QuizManager.updateContentSize();
		};
	};
})(jQuery);
