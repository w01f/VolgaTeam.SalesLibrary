using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Video;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.Services
{
	static class VideoHelper
	{
		public static void ExtractVideoInfo(
			string sourceFilePath,
			string destinationPath,
			CancellationToken cancellationToken)
		{
			var allowToExit = false;
			var analizerPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "ffprobe.exe");
			var destinationInfoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".txt"));
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath)) return;
			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("-v error -print_format json -show_streams -select_streams v:0 \"{0}\"", sourceFilePath))
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				var analyzeOutput = videoAnalyzer.StandardOutput.ReadToEnd();
				analyzeOutput += videoAnalyzer.StandardError.ReadToEnd();
				File.WriteAllText(destinationInfoFilePath, analyzeOutput);
				allowToExit = true;
			};
			videoAnalyzer.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
				if (cancellationToken.IsCancellationRequested)
				{
					try
					{
						videoAnalyzer.Kill();
					}
					catch { }
					allowToExit = true;
				}
			}
		}


		public static void ExportMp4(
			string sourceFilePath,
			string destinationPath,
			FFMpegData ffMpegData,
			CancellationToken cancellationToken)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "ffmpeg.exe");
			var postProcessorPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "qt-faststart.exe");
			var tmpVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".mp4"));
			var destinationVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".mp4"));
			if (!File.Exists(sourceFilePath) || !File.Exists(converterPath) || !File.Exists(postProcessorPath)) return;
			var videoConverter = new Process
			{
				StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -c:v libx264 -crf 23 -b {1} \"{2}\"", sourceFilePath,
					ffMpegData.Bitrate, tmpVideoFilePath))
				{
					UseShellExecute = false,
					RedirectStandardError = false,
					RedirectStandardOutput = false,
					CreateNoWindow = true,
				},
				EnableRaisingEvents = true
			};
			videoConverter.Exited += (converterSender, converterE) =>
			{
				var videoPostProcessor = new Process
				{
					StartInfo = new ProcessStartInfo(postProcessorPath, String.Format("{0} {1}", tmpVideoFilePath, destinationVideoFilePath))
					{
						UseShellExecute = false,
						RedirectStandardError = false,
						RedirectStandardOutput = false,
						CreateNoWindow = true,
					},
					EnableRaisingEvents = true
				};
				videoPostProcessor.Exited += (postProcessorSender, postProcessorE) =>
				{
					if (!File.Exists(destinationVideoFilePath))
						if (File.Exists(tmpVideoFilePath))
							File.Copy(tmpVideoFilePath, destinationVideoFilePath, true);
					if (File.Exists(tmpVideoFilePath))
						try
						{
							File.Delete(tmpVideoFilePath);
						}
						catch { }
					allowToExit = true;
				};
				videoPostProcessor.Start();
			};
			videoConverter.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
				if (cancellationToken.IsCancellationRequested)
				{
					try
					{
						videoConverter.Kill();
					}
					catch { }
					allowToExit = true;
				}
			}
		}

		public static void GenerateThumbnails(
			string sourceFilePath,
			string destinationPath,
			FFMpegData ffMpegData,
			CancellationToken cancellationToken)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "ffmpeg.exe");
			var destinationOutputFilePath = Path.Combine(destinationPath, String.Format("{0}%d.png", Path.GetFileNameWithoutExtension(sourceFilePath)));
			if (!File.Exists(sourceFilePath) || !File.Exists(converterPath)) return;

			int startDelay;
			int endDelay;
			string imageExtractOption;

			if (ffMpegData.Duration < 1)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = "-vf fps=1 -vframes 1";
			}
			else if (ffMpegData.Duration < 10)
			{
				startDelay = 1;
				endDelay = 0;
				imageExtractOption = "-vf fps=1";
			}
			else if (ffMpegData.Duration < 30)
			{
				startDelay = 5;
				endDelay = 2;
				imageExtractOption = "-vf fps=1";
			}
			else if (ffMpegData.Duration < 180)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = "-vf fps=1/2";
			}
			else if (ffMpegData.Duration < 300)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = "-vf fps=1/6";
			}
			else
			{
				startDelay = 7;
				endDelay = 7;
				imageExtractOption = "-vf fps=1/30";
			}

			var targetDuration = ffMpegData.Duration - startDelay - endDelay;

			var videoConverter = new Process
			{
				StartInfo = new ProcessStartInfo(
					converterPath,
					String.Format("-i \"{0}\" -ss {2} -t {3} {4} \"{1}\"",
						sourceFilePath,
						destinationOutputFilePath,
						startDelay,
						targetDuration,
						imageExtractOption
						)
					)
				{
					UseShellExecute = false,
					RedirectStandardError = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardOutput = false,
					CreateNoWindow = true,
				},
				EnableRaisingEvents = true
			};
			videoConverter.Exited += (converterSender, converterE) =>
			{
				allowToExit = true;
			};
			videoConverter.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
				if (cancellationToken.IsCancellationRequested)
				{
					try
					{
						videoConverter.Kill();
					}
					catch { }
					allowToExit = true;
				}
			}
		}

		public static void PlayVideo(string filePath)
		{
			try
			{
				Utils.OpenFile(filePath);
			}
			catch
			{
				MainController.Instance.PopupMessages.ShowWarning("Couldn't open video file");
			}
		}
	}
}
