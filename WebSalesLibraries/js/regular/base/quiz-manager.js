(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var QuizManager = function ()
	{
		var that = this;
		var loadInProgress = false;
		this.currentItem = null;

		var initList = function (itemList)
		{
			var isScrolling;
			itemList.find('li.quiz-group')
				.off('click').on('click', function (event)
				{
					openGroup($(this));
					event.stopPropagation();
				})
				.off('touchstart').on('touchstart', function ()
				{
					isScrolling = false;
				})
				.off('touchmove').on('touchmove', function ()
				{
					isScrolling = true;
				})
				.off('touchend').on('touchend', function (e)
				{
					if (!isScrolling)
						openGroup($(this));
					e.stopPropagation();
					e.preventDefault();
					return false;
				});
			itemList.find('li.quiz-item')
				.off('click').on('click', function (event)
				{
					openQuiz($(this), false);
					event.stopPropagation();
				})
				.off('touchstart').on('touchstart', function ()
				{
					isScrolling = false;
				})
				.off('touchmove').on('touchmove', function ()
				{
					isScrolling = true;
				})
				.off('touchend').on('touchend', function (e)
				{
					if (!isScrolling)
						openQuiz($(this), false);
					e.stopPropagation();
					e.preventDefault();
					return false;
				});
		};

		var openGroup = function (groupItem)
		{
			if (loadInProgress) return;
			groupItem.siblings().find('.quizzes-list').hide();
			groupItem.siblings('.quiz-group').find('.glyphicon-folder-open').removeClass('glyphicon-folder-open').addClass('glyphicon-folder-close');
			groupItem.find('>a>span.glyphicon-folder-close').removeClass('glyphicon-folder-close').addClass('glyphicon-folder-open');

			var itemName = groupItem.find('>a>span').html();
			$.cookie("selectedQuizItemName", itemName, {
				expires: (60 * 60 * 24 * 7)
			});

			var itemList = groupItem.find('>.quizzes-list');
			if (itemList.length == 0)
			{
				var itemId = groupItem.find('>div.service-data').find('.item-id').html();
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "quizzes/getQuizList",
					data: {
						parentId: itemId
					},
					beforeSend: function ()
					{
						loadInProgress = true;
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						loadInProgress = false;
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						itemList = $(msg);
						groupItem.append(itemList);
						initList(itemList);
					},
					async: true,
					dataType: 'html'
				});
			}
			else
			{
				itemList.show();
				initList(itemList);
			}
			openQuiz(null, false);
		};

		var openQuiz = function (listItem, runQuiz)
		{
			if (loadInProgress) return;
			that.currentItem = listItem;
			var navigator = $('#quizzes-navigator');
			navigator.find('li a').removeClass('opened');
			var quizPanel = $('#quiz-panel');
			quizPanel.html('');
			if (that.currentItem == null) return;
			that.currentItem.children('a').addClass('opened');

			var itemName = that.currentItem.find('>a>span').html();
			$.cookie("selectedQuizItemName", itemName, {
				expires: (60 * 60 * 24 * 7)
			});

			var itemId = that.currentItem.children('.service-data').find('.item-id').html();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "quizzes/getQuizPanel",
				data: {
					quizId: itemId
				},
				beforeSend: function ()
				{
					loadInProgress = true;
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					loadInProgress = false;
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					quizPanel.html(msg);
					if (runQuiz == true)
						runCurrentQuiz();
					quizPanel.find('.quiz-run').off('click').on('click', function ()
					{
						runCurrentQuiz();
					});
				},
				async: true,
				dataType: 'html'
			});

			$.SalesPortal.LogHelper.write({
				type: 'Quizzes',
				subType: 'Quiz Selected',
				data: {
					name: itemName
				}
			});
		};

		var runCurrentQuiz = function ()
		{
			var quizPanel = $('#quiz-panel');
			var itemId = that.currentItem.children('.service-data').find('.item-id').html();
			var quiz = new Quiz(itemId, quizPanel.find('.quiz-data'));
			quiz.run();
		};

		this.updateContentSize = function ()
		{
			$.SalesPortal.Content.updateSize();
			var height = $.SalesPortal.Content.getContentObject().height();
			$('#quizzes-navigator').find('> div').css({
				'height': height + 'px'
			});
		};

		this.refreshQuizPanel = function (reTake)
		{
			openQuiz(that.currentItem, reTake);
		};

		this.loadItems = function ()
		{
			var navigator = $('#quizzes-navigator');
			var topLevelList = navigator.find('>div');
			var selectedQuizItem = navigator.find('a.opened').parent();
			if (selectedQuizItem.length > 0)
			{
				openQuiz(selectedQuizItem, false);
				initList(topLevelList)
			}
			else
				openGroup(topLevelList);
		};
	};

	var Quiz = function (id, config)
	{
		var title = config.find('.config Title').html();
		var extendedTitle = config.find('.config Subtitle').html() + ' - ' +
			title + ' - ' +
			config.find('.config Date').html();

		var uniqueId = config.find('.config ID').html();

		var cover = {
			content: null
		};
		var end = {
			content: null
		};

		var questions = [];
		var sortQuestions = function (a, b)
		{
			if (a.order < b.order)
				return -1;
			if (a.order > b.order)
				return 1;
			return 0;
		};
		config.find('.config Question').each(function ()
		{
			var question = {
				order: parseInt($(this).find('Order').html()),
				selectedAnswer: -1,
				content: null,
				nextQuestion: null,
				prevQuestion: null
			};
			questions.push(question);
		});

		var questionsCount = questions.length;
		if (questionsCount > 0)
		{
			questions.sort(sortQuestions);

			for (var i = 0; i < questionsCount; i++)
			{
				if (i > 0)
					questions[i].prevQuestion = questions[i - 1];
				if (i < (questionsCount - 1))
					questions[i].nextQuestion = questions[i + 1];
			}
		}

		var getResult = function ()
		{
			var results = [];
			$(questions).each(function (index, value)
			{
				var result = {
					question: this.order,
					answer: value.selectedAnswer
				};
				results.push(result);
				value.selectedAnswer = -1;
				value.content = null;
			});
			cover.content = null;
			cover.end = null;
			return results;
		};

		var showQuestion = function (question)
		{
			var openForm = function (content)
			{
				$.fancybox.close();
				$.fancybox({
					content: content,
					title: 'Question ' + question.order + ' of ' + questionsCount,
					width: 750,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
					afterShow: function ()
					{
						var innerContent = $('.fancybox-inner');

						var answerIdPrefix = 'quiz' + id + 'question' + question.order + 'answer';
						if (question.selectedAnswer != -1)
							$('#' + answerIdPrefix + question.selectedAnswer).addClass('active');
						else
							innerContent.find('.next-question .btn').addClass('disabled');

						innerContent.find('.answer-selector .btn').on('click', function ()
						{
							innerContent.find('.answer-selector .btn').removeClass('active').blur();
							$(this).addClass('active');
							innerContent.find('.next-question .btn').removeClass('disabled');

							question.selectedAnswer = parseInt($(this).attr('id').replace(answerIdPrefix, ''));
						});

						var buttonNext = innerContent.find('.next-question .btn');
						if (question.nextQuestion != null)
							buttonNext.off('click').on('click', function ()
							{
								if (!$(this).hasClass('disabled'))
									showQuestion(question.nextQuestion);
							});
						else
						{
							buttonNext.html('Finish');
							buttonNext.off('click').on('click', function ()
							{
								if (!$(this).hasClass('disabled'))
									showEnd();
							});
						}

						var buttonPrev = innerContent.find('.prev-question .btn');
						if (question.prevQuestion != null)
							buttonPrev.off('click').on('click', function ()
							{
								showQuestion(question.prevQuestion);
							});
						else
							buttonPrev.hide();
					},
					helpers: {
						overlay: {
							closeClick: false
						}
					}
				});
			};
			if (question.content == null)
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "quizzes/getQuizQuestion",
					data: {
						quizId: id,
						quizQuestion: question.order
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						question.content = msg;
						openForm(question.content);
					},
					async: true,
					dataType: 'html'
				});
			else
				openForm(question.content);
		};

		var showCover = function ()
		{
			var openForm = function (content)
			{
				$.fancybox.close();
				$.fancybox({
					content: content,
					title: title,
					width: 750,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
					helpers: {
						overlay: {
							closeClick: false
						}
					},
					afterShow: function ()
					{
						var innerContent = $('.fancybox-inner');
						innerContent.find('.next-question .btn').off('click').on('click', function ()
						{
							if (questionsCount > 0)
								showQuestion(questions[0]);
						});
					}
				});
			};
			if (cover.content == null)
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "quizzes/getQuizCover",
					data: {
						quizId: id
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						cover.content = $(msg);
						openForm(cover.content);
					},
					async: true,
					dataType: 'html'
				});
			else
				openForm(cover.content);
		};

		var showEnd = function ()
		{
			var openForm = function (content)
			{
				$.fancybox.close();
				$.fancybox({
					content: content,
					title: 'EXAM COMPLETE',
					width: 750,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
					helpers: {
						overlay: {
							closeClick: false
						}
					},
					afterShow: function ()
					{
						var innerContent = $('.fancybox-inner');
						var successful = innerContent.find('.success').length > 0;
						if (successful)
							$.SalesPortal.QuizManager.currentItem.removeClass('not-passed').addClass('passed');
						innerContent.find('.btn.quiz-start').off('click').on('click', function ()
						{
							$.fancybox.close();
							$.SalesPortal.QuizManager.refreshQuizPanel(true);
						});
						innerContent.find('.btn.quiz-exit').off('click').on('click', function ()
						{
							$.fancybox.close();
							$.SalesPortal.QuizManager.refreshQuizPanel(false);
						});
					}
				});
			};
			if (end.content == null)
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "quizzes/getQuizEnd",
					data: {
						quizId: id,
						results: $.toJSON(getResult())
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						end.content = msg;
						openForm(end.content);
					},
					async: true,
					dataType: 'html'
				});
			else
				openForm(end.content);

			$.SalesPortal.LogHelper.write({
				type: 'Quizzes',
				subType: 'Quiz Finished',
				data: {
					name: extendedTitle,
					id: uniqueId
				}
			});
		};

		this.run = function ()
		{
			showCover();

			$.SalesPortal.LogHelper.write({
				type: 'Quizzes',
				subType: 'Quiz Started',
				data: {
					name: extendedTitle,
					id: uniqueId
				}
			});
		};
	};
	$.SalesPortal.QuizManager = new QuizManager();
})(jQuery);