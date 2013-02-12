using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolClasses
{
	internal class VideoHelper
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

		public void ExportMp4(string sourceFilePath, string destinationPath)
		{
			var allowToExit = false;
			var analizerPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffprobe.exe");
			var converterPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffmpeg.exe");
			var postProcessorPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "qt-faststart.exe");
			var tmpVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".mp4"));
			var destinationVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".mp4"));
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath) || !File.Exists(converterPath) || !File.Exists(postProcessorPath)) return;
			Process videoConverter = null;
			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("\"{0}\"", sourceFilePath)) { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true },
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var b = videoAnalyzer.StandardOutput.ReadToEnd();
					if (!b.Contains("bitrate:"))
						b = videoAnalyzer.StandardError.ReadToEnd();
					var kBitRate = ExtractBitrate(b, 0);
					videoConverter = new Process
					{
						StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -vcodec libx264 -xerror -b {1}k \"{2}\"", sourceFilePath,
																					kBitRate, tmpVideoFilePath))
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
						allowToExit = true;
					};
					videoConverter.Start();
				}
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
					if (videoConverter != null)
						try
						{
							videoConverter.Kill();
						}
						catch { }
					allowToExit = true;
				}
			}
		}

		public void ExportOgv(string sourceFilePath, string destinationPath)
		{
			bool allowToExit = false;
			string analizerPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffprobe.exe");
			string converterPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffmpeg.exe");
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath) || !File.Exists(converterPath)) return;
			Process videoConverter = null;
			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("\"{0}\"", sourceFilePath)) { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true },
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					string b = videoAnalyzer.StandardOutput.ReadToEnd();

					if (!b.Contains("bitrate:"))
						b = videoAnalyzer.StandardError.ReadToEnd();

					int kBitRate = ExtractBitrate(b, 0);

					videoConverter = new Process
					{
						StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -acodec libvorbis -xerror -b {1}k \"{2}\"", sourceFilePath,
																					kBitRate, Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".ogv"))))
						{
							UseShellExecute = false,
							RedirectStandardError = false,
							RedirectStandardOutput = false,
							CreateNoWindow = true,
						},
						EnableRaisingEvents = true
					};
					videoConverter.Exited += (converterSender, converterE) => { allowToExit = true; };
					videoConverter.Start();
				}
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
					if (videoConverter != null)
						try
						{
							videoConverter.Kill();
						}
						catch { }
					allowToExit = true;
				}
			}
		}

		public void ExportWmv(string sourceFilePath, string destinationPath)
		{
			bool allowToExit = false;
			string analizerPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffprobe.exe");
			string converterPath = Path.Combine(SettingsManager.Instance.VideoConverterPath, "ffmpeg.exe");
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath) || !File.Exists(converterPath)) return;
			Process videoConverter = null;
			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("\"{0}\"", sourceFilePath)) { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true },
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					string b = videoAnalyzer.StandardOutput.ReadToEnd();

					if (!b.Contains("bitrate:"))
						b = videoAnalyzer.StandardError.ReadToEnd();

					int kBitRate = ExtractBitrate(b, 0);

					videoConverter = new Process
					{
						StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -vcodec msmpeg4 -xerror -b {1}k \"{2}\"", sourceFilePath,
							Math.Ceiling(kBitRate * 1.5), Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".wmv"))))
						{
							UseShellExecute = false,
							RedirectStandardError = false,
							RedirectStandardOutput = false,
							CreateNoWindow = true,
						},
						EnableRaisingEvents = true
					};
					videoConverter.Exited += (converterSender, converterE) => { allowToExit = true; };
					videoConverter.Start();
				}
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
					if (videoConverter != null)
						try
						{
							videoConverter.Kill();
						}
						catch { }
					allowToExit = true;
				}
			}
		}

		private int ExtractBitrate(string b, int defaultValue)
		{
			try
			{
				var r = new Regex(@"bitrate: (?<bitrate>\d{1,10}) kb/s", RegexOptions.IgnoreCase);

				if (r.IsMatch(b))
				{
					int bitrate;
					if (Int32.TryParse(r.Match(b).Groups["bitrate"].Value, out bitrate))
						return bitrate;
				}
			}
			catch { }
			return defaultValue;
		}

		public void OpenMediaPlayer(string filePath)
		{
			try
			{
				var process = new Process();
				process.StartInfo.FileName = "wmplayer.exe";
				process.StartInfo.Arguments = string.Format("\"{0}\"", filePath);
				process.Start();
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open video file");
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

		public void OpenFirefox(string filePath)
		{
			try
			{
				var process = new Process();
				process.StartInfo.FileName = "firefox.exe";
				process.StartInfo.Arguments = string.Format("file://{0}", filePath.Replace(" ", "%20").Replace("&", "%26"));
				process.Start();
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open video file");
			}
		}
	}
}