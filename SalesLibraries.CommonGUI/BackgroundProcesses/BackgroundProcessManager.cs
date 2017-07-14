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
		private FormProgressCommon _formProgress = new FormProgressCommon();
		private readonly Func<Form> _mainFormAccessor;

		private int _processesInQueue;

		public event EventHandler<EventArgs> Suspended;
		public event EventHandler<EventArgs> Resumed;

		public BackgroundProcessManager(Func<Form> mainFormAccessor)
		{
			_mainFormAccessor = mainFormAccessor;
		}

		public void Release()
		{
			try
			{
				_formProgress?.Dispose();
				_formProgress = null;
			}
			catch { }
		}

		public void Run(string title, Action<CancellationToken, FormProgressCommon> process, Action afterComplete = null, Action<Exception> onException = null)
		{
			using (var form = new FormProgressCommon())
			{
				form.Title = title;
				form.StartPosition = FormStartPosition.CenterParent;
				RunWithProgress(form, false, process, cancellationToken => { afterComplete?.Invoke(); }, onException);
			}
		}

		public void RunWithProgress<TForm>(
			TForm formProgress,
			bool showAsync,
			Action<CancellationToken, TForm> process,
			Action<CancellationToken> afterComplete = null,
			Action<Exception> onException = null)
			where TForm : FormProgressBase
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
				try
				{
					await Task.Run(() => process(cancellationTokenSource.Token, formProgress), cancellationTokenSource.Token);
					afterComplete?.Invoke(cancellationTokenSource.Token);
					_mainFormAccessor().Invoke(new MethodInvoker(formProgress.Close));
				}
				catch (Exception ex)
				{
					_mainFormAccessor().Invoke(new MethodInvoker(() =>
					{
						formProgress.Visible = false;
						formProgress.Close();
						Application.DoEvents();

						onException?.Invoke(ex);
					}));
				}
				finally
				{
					cancellationTokenSource.Dispose();
				}
			};
			formProgress.ProcessAborted += (o, e) => cancellationTokenSource.Cancel();
			if (showAsync)
				formProgress.Show();
			else
				formProgress.ShowDialog(_mainFormAccessor());

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
				if (!_mainFormAccessor().IsDisposed)
					_mainFormAccessor().Invoke(new MethodInvoker(() =>
					{
						_formProgress.Hide();
						if (_mainFormAccessor().WindowState != FormWindowState.Minimized)
							Utils.ActivateForm(_mainFormAccessor().Handle, _mainFormAccessor().WindowState == FormWindowState.Maximized, false);
					}));
				return;
			}
			if (!currentProcess.ShowProgress)
				_mainFormAccessor().Invoke(new MethodInvoker(() => _formProgress.Hide()));
			Interlocked.Increment(ref _processesInQueue);
			Task.Run(async () =>
			{
				if (currentProcess.ShowProgress && !_mainFormAccessor().IsDisposed)
					_mainFormAccessor().Invoke(new MethodInvoker(() =>
					{
						_formProgress.Visible = false;
						_formProgress.laProgress.Text = currentProcess.Title;
						_formProgress.StartPosition = FormStartPosition.Manual;
						_formProgress.Left = _mainFormAccessor().Left + (_mainFormAccessor().Width - _formProgress.Width) / 2;
						_formProgress.Top = _mainFormAccessor().Top + (_mainFormAccessor().Height - _formProgress.Height) / 2;
						_formProgress.Show();
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
