using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public class BackgroundProcessManager
	{
		private readonly ConcurrentQueue<BackgroundProcess> _tasks = new ConcurrentQueue<BackgroundProcess>();
		private readonly FormProgressCommon _formProgress = new FormProgressCommon();
		private readonly Form _mainForm;
		private readonly string _title;

		private int _processesInQueue;
		private bool _startFormTrayed;

		public event EventHandler<EventArgs> Suspended;
		public event EventHandler<EventArgs> Resumed;

		public BackgroundProcessManager(Form mainForm, string title)
		{
			_mainForm = mainForm;
			_title = title;
		}

		public void Run(string title, Action<CancellationToken> process, Action afterComplete = null)
		{
			using (var form = new FormProgressCommon())
			{
				form.Title = title;
				form.StartPosition = FormStartPosition.CenterParent;
				RunWithProgress(form, false, process, cancellationToken => { afterComplete?.Invoke(); });
			}
		}

		public void RunStartProcess(string text, Action<CancellationToken> process, Action afterComplete = null)
		{
			using (var form = new FormStart(_title, _startFormTrayed))
			{
				form.SetText(text);
				form.Trayed += (o, e) => _startFormTrayed = true;
				form.Activated += (o, e) => _startFormTrayed = false;
				RunWithProgress(form, false, process, cancellationToken =>
				{
					afterComplete?.Invoke();
				});
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
				afterComplete?.Invoke(cancellationTokenSource.Token);
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
			Suspended?.Invoke(this, EventArgs.Empty);
		}

		public void ResumeProcess()
		{
			Resumed?.Invoke(this, EventArgs.Empty);
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
				if (!_mainForm.IsDisposed)
					_mainForm.Invoke(new MethodInvoker(() =>
					{
						_formProgress.Hide();
						if (_mainForm.WindowState != FormWindowState.Minimized)
							Utils.ActivateForm(_mainForm.Handle, _mainForm.WindowState == FormWindowState.Maximized, false);
					}));
				return;
			}
			if (!currentProcess.ShowProgress)
				_mainForm.Invoke(new MethodInvoker(() => _formProgress.Hide()));
			Interlocked.Increment(ref _processesInQueue);
			Task.Run(async () =>
			{
				if (currentProcess.ShowProgress && !_mainForm.IsDisposed)
					_mainForm.Invoke(new MethodInvoker(() =>
					{
						_formProgress.Visible = false;
						_formProgress.laProgress.Text = currentProcess.Title;
						_formProgress.StartPosition = FormStartPosition.Manual;
						_formProgress.Left = _mainForm.Left + (_mainForm.Width - _formProgress.Width) / 2;
						_formProgress.Top = _mainForm.Top + (_mainForm.Height - _formProgress.Height) / 2;
						_formProgress.Show(_mainForm);
						_formProgress.laProgress.Refresh();
						Application.DoEvents();
					}));
				await Task.Run(currentProcess.Process);
				currentProcess.AfterComplete?.Invoke();
				PerformQueueProcess();
			});
		}
	}
}
