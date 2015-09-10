using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using FileManager.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolClasses
{
	public class VideoHelper
	{
		private static VideoHelper _instance;

		private VideoHelper() { }
		public static VideoHelper Instance
		{
			get
			{
				if (_instance == null)
					_instance = new VideoHelper();
				return _instance;
			}
		}

		public void ExtractVideoInfo(string sourceFilePath, string destinationPath)
		{
			var allowToExit = false;
			var analizerPath = Path.Combine(SettingsManager.Instance.FFMpegPath, "ffprobe.exe");
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
				if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
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

		public void ExportMp4(
			string sourceFilePath,
			string destinationPath,
			FFMpegData ffMpegData)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(SettingsManager.Instance.FFMpegPath, "ffmpeg.exe");
			var postProcessorPath = Path.Combine(SettingsManager.Instance.FFMpegPath, "qt-faststart.exe");
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
				if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
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

		public void GenerateThumbnails(
			string sourceFilePath,
			string destinationPath,
			FFMpegData ffMpegData)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(SettingsManager.Instance.FFMpegPath, "ffmpeg.exe");
			var tmpOutputFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".png"));
			var destinationOutputFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".png"));
			if (!File.Exists(sourceFilePath) || !File.Exists(converterPath)) return;
			var videoConverter = new Process
			{
				StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -ss {2} -vframes 1 -y \"{1}\"", sourceFilePath, tmpOutputFilePath,
					(int)Math.Floor(ffMpegData.Duration / 100.0 * 25)))
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
				if (!File.Exists(destinationOutputFilePath) && File.Exists(tmpOutputFilePath))
					File.Copy(tmpOutputFilePath, destinationOutputFilePath, true);
				if (File.Exists(tmpOutputFilePath))
					try
					{
						File.Delete(tmpOutputFilePath);
					}
					catch { }
				allowToExit = true;
			};
			videoConverter.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
				if (!((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
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

		public void OpenQuickTime(string filePath)
		{
			try
			{
				var process = new Process();
				process.StartInfo.FileName = "QuickTimePlayer.exe";
				process.StartInfo.Arguments = string.Format("\"{0}\"", filePath);
				process.Start();
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open video file");
			}
		}
	}
}