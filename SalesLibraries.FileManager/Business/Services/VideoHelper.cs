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
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("-v error -print_format json -show_format -show_streams -select_streams v:0 \"{0}\"", sourceFilePath))
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
			int? crf,
			CancellationToken cancellationToken)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "ffmpeg.exe");
			var postProcessorPath = Path.Combine(MainController.Instance.Settings.FFMpegPackagePath, "qt-faststart.exe");
			var tmpVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".mp4"));
			var destinationVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".mp4"));
			if (!File.Exists(sourceFilePath) || !File.Exists(converterPath) || !File.Exists(postProcessorPath)) return;

			var crfOption = crf.HasValue ? String.Format(" -crf {0}", crf.Value) : String.Empty;

			var fileType = Path.GetExtension(sourceFilePath)?.ToUpper();
			string converterParameters;
			switch (fileType)
			{
				case ".MP4":
					converterParameters = String.Format("-i \"{0}\" -c:v libx264{2} -b:v 2800k -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".MPG":
				case ".MPEG":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -c:v libx264{2} -movflags faststart \"{1}\"" :
						"-i \"{0}\" -c:v libx264{2} -b:v 2800k -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".MOV":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -pix_fmt yuv420p -vcodec h264{2} -acodec aac -strict -2 -movflags faststart \"{1}\"" :
						"-i \"{0}\" -pix_fmt yuv420p -vcodec h264{2} -b:v 2800k -b:a 192k -acodec aac -strict -2 -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".WMV":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -c:v libx264{2} -movflags faststart \"{1}\"" :
						"-i \"{0}\" -c:v libx264{2} -b:v 2800k -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".ASF":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -c:v libx264{2} -movflags faststart \"{1}\"" :
						"-i \"{0}\" -c:v libx264{2} -b:v 2800k -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".M4V":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -c:v libx264{2} -movflags faststart \"{1}\"" :
						"-i \"{0}\" -c:v libx264{2} -b:v 2800k -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				case ".MKV":
					converterParameters = String.Format(ffMpegData.IsBitrateNormal ?
						"-i \"{0}\" -c:v libx264{2} -movflags faststart \"{1}\"" :
						"-i \"{0}\" -c:v libx264{2} -b:v 5M -b:a 192k -movflags faststart \"{1}\"", sourceFilePath, tmpVideoFilePath, crfOption);
					break;
				default:
					converterParameters = String.Format("-i \"{0}\" -c:v libx264{3} -b {1} \"{2}\"", sourceFilePath,
						ffMpegData.Bitrate, tmpVideoFilePath, crfOption);
					break;
			}
			var videoConverter = new Process
			{
				StartInfo = new ProcessStartInfo(converterPath, converterParameters)
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
			string imageExtractOption = "-vf \"{0},scale=-1:'min(ih,360)'\"";

			if (ffMpegData.Duration < 1)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1") + " -vframes 1";
			}
			else if (ffMpegData.Duration < 10)
			{
				startDelay = 1;
				endDelay = 0;
				imageExtractOption = String.Format(imageExtractOption, "fps=1");
			}
			else if (ffMpegData.Duration < 30)
			{
				startDelay = 5;
				endDelay = 2;
				imageExtractOption = String.Format(imageExtractOption, "fps=1");
			}
			else if (ffMpegData.Duration < 180)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/2");
			}
			else if (ffMpegData.Duration < 300)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/6");
			}
			else
			{
				startDelay = 7;
				endDelay = 7;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/30");
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
