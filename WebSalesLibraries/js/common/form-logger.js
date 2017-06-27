(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.FormLogger = function ()
	{
		this.init = function (parameters)
		{
			var logObject = new LogObject(parameters.logObject);
			var formObject = parameters.formContent.hasClass('logger-form') ?
				parameters.formContent :
				parameters.formContent.find('.logger-form');

			var actionDefaultGroupName = formObject.data('log-group');
			var actionDefaultName = formObject.data('log-action');

			var logEventHandler = function ()
			{
				var logAction = $(this);

				var actionGroupName = logAction.data('log-group');
				var actionName = logAction.data('log-action');
				var actionData = logObject.isInitialized ?
				{
					name: logObject.name,
					file: logObject.fileName,
					originalFormat: logObject.format
				} :
				{};

				$.SalesPortal.LogHelper.write({
					type: actionGroupName !== undefined ? actionGroupName : actionDefaultGroupName,
					subType: actionName !== undefined ? actionName : actionDefaultName,
					linkId: logObject.linkId !== undefined ? logObject.linkId : undefined,
					qpageId: logObject.qpageId !== undefined ? logObject.qpageId : undefined,
					data: actionData
				});
			};
			formObject.find('.log-action').off('click.log').on('click.log', logEventHandler);
		}
	};

	var LogObject = function (source)
	{
		this.name = undefined;
		this.fileName = undefined;
		this.format = undefined;

		this.linkId = undefined;
		this.qpageId = undefined;

		if (source !== undefined)
		{
			for (var prop in source)
				if (source.hasOwnProperty(prop))
					this[prop] = source[prop];
			this.isInitialized = true;
		}
		else
			this.isInitialized = false;
	};
})(jQuery);
