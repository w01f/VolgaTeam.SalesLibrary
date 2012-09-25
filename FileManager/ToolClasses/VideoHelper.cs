using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolClasses
{
    class VideoHelper
    {
        private static VideoHelper _instance;

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
            string analizerPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.VideoConverterPath, "ffprobe.exe");
            string converterPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.VideoConverterPath, "ffmpeg.exe");
            string postProcessorPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.VideoConverterPath, "qt-faststart.exe");
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

        public void ExportOgv(string sourceFilePath, string destinationPath)
        {
            bool allowToExit = false;
            string analizerPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.VideoConverterPath, "ffprobe.exe");
            string converterPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.VideoConverterPath, "ffmpeg.exe");
            if (File.Exists(sourceFilePath) && File.Exists(analizerPath) && File.Exists(converterPath))
            {
                Process videoConverter = null;
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

                        videoConverter = new Process()
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
                        videoConverter.Exited += new EventHandler((converterSender, converterE) =>
                        {
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

        public void OpenMediaPlayer(string filePath)
        {
            try
            {
                Process process = new Process();
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
                Process process = new Process();
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
                Process process = new Process();
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
