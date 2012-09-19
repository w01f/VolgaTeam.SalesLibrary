using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

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
                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                    {
                        var b = videoAnalyzer.StandardOutput.ReadToEnd();

                        if (!b.Contains("bitrate:"))
                            b = videoAnalyzer.StandardError.ReadToEnd();

                        int kBitRate = ExtractBitrate(b, 0);

                        videoConverter = new Process()
                        {
                            StartInfo = new ProcessStartInfo(converterPath, String.Format("-i \"{0}\" -vcodec libx264 -moov_size 524288 -xerror -b {1}k \"{2}\"", sourceFilePath,
                                kBitRate, Path.Combine(destinationPath, Path.ChangeExtension(Path.GetFileName(sourceFilePath), ".mp4"))))
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
                    if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
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
                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
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
                    if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
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
