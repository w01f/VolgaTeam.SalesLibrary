(function ($)
{
	var QuizManager = function ()
	{
		var currentItem = null;
		var loadItems = function ()
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
			$.updateContentAreaDimensions();
		};

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
			var otherGroupItems = groupItem.siblings();
			groupItem.siblings().find('.quizzes-list').hide();
			groupItem.siblings('.quiz-group').find('.icon-folder-open').removeClass('icon-folder-open').addClass('icon-folder-close');
			groupItem.find('>a>i.icon-folder-close').removeClass('icon-folder-close').addClass('icon-folder-open');

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
					url: "quizzes/getQuizList",
					data: {
						parentId: itemId
					},
					beforeSend: function ()
					{
						$.showOverlayLight();
					},
					complete: function ()
					{
						$.hideOverlayLight();
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
			currentItem = listItem;
			var navigator = $('#quizzes-navigator');
			navigator.find('li a').removeClass('opened');
			var quizPanel = $('#quiz-panel');
			quizPanel.html('');
			if (currentItem == null) return;
			currentItem.children('a').addClass('opened');

			var itemName = currentItem.find('>a>span').html();
			$.cookie("selectedQuizItemName", itemName, {
				expires: (60 * 60 * 24 * 7)
			});

			var itemId = currentItem.children('.service-data').find('.item-id').html();
			$.ajax({
				type: "POST",
				url: "quizzes/getQuizPanel",
				data: {
					quizId: itemId
				},
				beforeSend: function ()
				{
					$.showOverlay();
				},
				complete: function ()
				{
					$.hideOverlay();
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
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Quizzes',
					subType: 'Quiz Selected',
					data: $.toJSON({
						Name: itemName
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		var runCurrentQuiz = function ()
		{
			var quizPanel = $('#quiz-panel');
			var itemId = currentItem.children('.service-data').find('.item-id').html();
			var quiz = new Quiz(itemId, quizPanel.find('.quiz-data'));
			quiz.run();
		};

		this.refreshQuizPanel = function (reTake)
		{
			openQuiz(currentItem, reTake);
		};

		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: "quizzes/getQuizzesView",
				beforeSend: function ()
				{
					$('#content').html('');
					$.showOverlay();
				},
				complete: function ()
				{
					$.hideOverlay();
				},
				success: function (msg)
				{
					$('#content').html(msg);
					loadItems();
				},
				async: true,
				dataType: 'html'
			});
		};
	};

	var Quiz = function (id, config)
	{
		var title = config.find('.config Title').html();

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
			$(questions).each(function ()
			{
				var result = {
					question: this.order,
					answer: this.selectedAnswer
				};
				results.push(result);
				this.selectedAnswer = -1;
				this.content = null;
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
					width: 550,
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
							innerContent.find('.answer-selector .btn').removeClass('active');
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
					}
				});
			};
			if (question.content == null)
				$.ajax({
					type: "POST",
					url: "quizzes/getQuizQuestion",
					data: {
						quizId: id,
						quizQuestion: question.order
					},
					beforeSend: function ()
					{
						$.showOverlayLight();
					},
					complete: function ()
					{
						$.hideOverlayLight();
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
					width: 550,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
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
					url: "quizzes/getQuizCover",
					data: {
						quizId: id
					},
					beforeSend: function ()
					{
						$.showOverlay();
					},
					complete: function ()
					{
						$.hideOverlay();
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
					width: 550,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none',
					afterShow: function ()
					{
						var innerContent = $('.fancybox-inner');
						innerContent.find('.btn.quiz-start').off('click').on('click', function ()
						{
							$.fancybox.close();
							$.QuizManager.refreshQuizPanel(true);
						});
						innerContent.find('.btn.quiz-exit').off('click').on('click', function ()
						{
							$.fancybox.close();
							$.QuizManager.refreshQuizPanel(false);
						});
					}
				});
			};
			if (end.content == null)
				$.ajax({
					type: "POST",
					url: "quizzes/getQuizEnd",
					data: {
						quizId: id,
						results: $.toJSON(getResult())
					},
					beforeSend: function ()
					{
						$.showOverlay();
					},
					complete: function ()
					{
						$.hideOverlay();
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
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Quizzes',
					subType: 'Quiz Finished',
					data: $.toJSON({
						Name: title
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		this.run = function ()
		{
			showCover();
			$.ajax({
				type: "POST",
				url: "statistic/writeActivity",
				data: {
					type: 'Quizzes',
					subType: 'Quiz Started',
					data: $.toJSON({
						Name: title
					})
				},
				async: true,
				dataType: 'html'
			});
		};
	};

	$(document).ready(function ()
	{
		$.QuizManager = new QuizManager();
	});
})
	(jQuery);