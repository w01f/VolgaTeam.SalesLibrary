﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.Cloner.ToolClasses
{
	class VideoHelper
	{
		private static VideoHelper _instance;
		private readonly string _videoConverterPath = Path.Combine(Path.GetDirectoryName(typeof(VideoHelper).Assembly.Location), "video converter");

		public static VideoHelper Instance
		{
			get
			{
				if (_instance == null)
					_instance = new VideoHelper();
				return _instance;
			}
		}

		private VideoHelper()
		{
		}

		public void ExportMp4(string sourceFilePath, string destinationPath)
		{
			bool allowToExit = false;
			string analizerPath = Path.Combine(_videoConverterPath, "ffprobe.exe");
			string converterPath = Path.Combine(_videoConverterPath, "ffmpeg.exe");
			string postProcessorPath = Path.Combine(_videoConverterPath, "qt-faststart.exe");
			string tmpVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".mp4"));
			string destinationVideoFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".mp4"));
			if (File.Exists(sourceFilePath) && File.Exists(analizerPath) && File.Exists(converterPath) && File.Exists(postProcessorPath))
			{
				Process videoConverter = null;
				Process videoPostProcessor = null;
				Process videoAnalyzer = new Process()
				{
					StartInfo = new ProcessStartInfo(analizerPath, String.Format("\"{0}\"", sourceFilePath)) { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true },
					EnableRaisingEvents = true
				};
				videoAnalyzer.Exited += new EventHandler((analyzerSender, analyzerE) =>
				{
					if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
					{
						var b = videoAnalyzer.StandardOutput.ReadToEnd();

						if (!b.Contains("bitrate:"))
							b = videoAnalyzer.StandardError.ReadToEnd();

						int kBitRate = ExtractBitrate(b, 0);
						var fsize = (new FileInfo(sourceFilePath)).Length;
						videoConverter = new Process()
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
						videoConverter.Exited += new EventHandler((converterSender, converterE) =>
						{
							videoPostProcessor = new Process()
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
							videoPostProcessor.Exited += new EventHandler((postProcessorSender, postProcessorE) =>
							{
								if (!File.Exists(destinationVideoFilePath))
									if (File.Exists(tmpVideoFilePath))
										File.Copy(tmpVideoFilePath, destinationVideoFilePath, true);

								if (File.Exists(tmpVideoFilePath))
									try
									{
										File.Delete(tmpVideoFilePath);
									}
									catch
									{
									}
								allowToExit = true;
							});
							videoPostProcessor.Start();
							allowToExit = true;
						});
						videoConverter.Start();
					}
				});
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
		}

		public void GenerateThumbnails(string sourceFilePath, string destinationPath)
		{
			var allowToExit = false;
			string analizerPath = Path.Combine(_videoConverterPath, "ffprobe.exe");
			string converterPath = Path.Combine(_videoConverterPath, "ffmpeg.exe");
			var tmpOutputFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(Path.GetTempFileName()), ".png"));
			var destinationOutputFilePath = Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".png"));
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath) || !File.Exists(converterPath)) return;
			Process videoConverter = null;

			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("\"{0}\"", sourceFilePath))
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var o = videoAnalyzer.StandardOutput.ReadToEnd();
					o += videoAnalyzer.StandardError.ReadToEnd();
					var videoLength = ExtractVideoLenght(o, 0);
					videoConverter = new Process
					{
						StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -ss {2} -vframes 1 -y \"{1}\"", sourceFilePath, tmpOutputFilePath,
							(int)Math.Floor(videoLength / 100.0 * 25)))
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

		private int ExtractVideoLenght(string b, int defaultValue)
		{
			try
			{
				var m = Regex.Match(b, @"Duration: (?<timestamp>\d\d:\d\d:\d\d.\d\d),");
				if (m.Success)
					return (int)Math.Floor(TimeSpan.Parse(m.Groups["timestamp"].Value).TotalSeconds);
			}
			catch { }
			return defaultValue;
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
			catch
			{
			}
			return defaultValue;
		}
	}
}
