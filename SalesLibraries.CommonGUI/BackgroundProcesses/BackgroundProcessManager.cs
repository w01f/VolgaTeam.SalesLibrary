using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public class BackgroundProcessManager
	{
		private readonly ConcurrentQueue<BackgroundProcess> _tasks = new ConcurrentQueue<BackgroundProcess>();
		private readonly FormProgressCommon _formProgress = new FormProgressCommon();
		private readonly Form _mainForm;
		private int _processesInQueue;

		public event EventHandler<EventArgs> Suspended;
		public event EventHandler<EventArgs> Resumed;

		public BackgroundProcessManager(Form mainForm)
		{
			_mainForm = mainForm;
		}

		public void Run(string title, Action<CancellationToken> process, Action afterComplete = null)
		{
			using (var form = new FormProgressCommon())
			{
				form.Title = title;
				RunWithProgress(form, false, process, cancellationToken => { if (afterComplete != null)afterComplete(); });
			}
		}

		public void RunStartProcess(string title, Action<CancellationToken> process, Action afterComplete = null)
		{
			using (var form = new FormStart())
			{
				form.SetTitle(title);
				RunWithProgress(form, false, process, cancellationToken => { if (afterComplete != null)afterComplete(); });
			}
		}

		public void RunWithProgress(FormProgressBase formProgress, bool showAsync, Action<CancellationToken> process, Action<CancellationToken> afterComplete = null)
		{
			var cancellationTokenSource = new CancellationTokenSource();

			var formOpacity = formProgress.Opacity;

			EventHandler<EventArgs> onSuspended = (sender, args) => formProgress.Invoke(new MethodInvoker(() =>
			{
				formProgress.Opacity = 0;
				Application.DoEvents();
			}));
			EventHandler<EventArgs> onResumed = (sender, args) => formProgress.Invoke(new MethodInvoker(() =>
			{
				formProgress.Opacity = formOpacity;
				Application.DoEvents();
			}));

			Suspended += onSuspended;
			Resumed += onResumed;

			formProgress.Shown += async (sender, args) =>
			{
				await Task.Run(() => process(cancellationTokenSource.Token), cancellationTokenSource.Token);
				if (afterComplete != null)
					afterComplete(cancellationTokenSource.Token);
				_mainForm.Invoke(new MethodInvoker(formProgress.Close));
				cancellationTokenSource.Dispose();
			};
			formProgress.ProcessAborted += (o, e) => cancellationTokenSource.Cancel();
			if (showAsync)
				formProgress.Show();
			else
				formProgress.ShowDialog(_mainForm);

			Suspended -= onSuspended;
			Resumed -= onResumed;
		}

		public void SuspendProcess()
		{
			if (Suspended != null)
				Suspended(this, EventArgs.Empty);
		}

		public void ResumeProcess()
		{
			if (Resumed != null)
				Resumed(this, EventArgs.Empty);
		}

		public void RunInQueue(string title, Action process, Action afterComplete = null, bool showProgress = true)
		{
			var backgroundProcess = new BackgroundProcess
			{
				Title = title,
				ShowProgress = showProgress,
				Process = process,
				AfterComplete = afterComplete
			};
			_tasks.Enqueue(backgroundProcess);
			if (_processesInQueue == 0)
				PerformQueueProcess();
		}

		private void PerformQueueProcess()
		{
			BackgroundProcess currentProcess;
			_tasks.TryDequeue(out currentProcess);
			if (_processesInQueue > 0)
				Interlocked.Decrement(ref _processesInQueue);
			if (currentProcess == null)
			{
				_mainForm.Invoke(new MethodInvoker(() => _formProgress.Hide()));
				return;
			}
			if (!currentProcess.ShowProgress)
				_mainForm.Invoke(new MethodInvoker(() => _formProgress.Hide()));
			Interlocked.Increment(ref _processesInQueue);
			Task.Run(async () =>
			{
				if (currentProcess.ShowProgress)
					_mainForm.Invoke(new MethodInvoker(() =>
					{
						_formProgress.laProgress.Text = currentProcess.Title;
						_formProgress.Visible = false;
						_formProgress.Show(_mainForm);
						_formProgress.laProgress.Refresh();
						Application.DoEvents();
					}));
				await Task.Run(currentProcess.Process);
				if (currentProcess.AfterComplete != null)
					currentProcess.AfterComplete();
				PerformQueueProcess();
			});
		}
	}
}
