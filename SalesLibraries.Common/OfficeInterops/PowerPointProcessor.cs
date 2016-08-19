using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.PowerPoint;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace SalesLibraries.Common.OfficeInterops
{
	public abstract class PowerPointProcessor
	{
		private bool _isFirstLaunch;
		public Application PowerPointObject { get; private set; }
		public Presentation SlideSourcePresentation { get; private set; }
		public SlideShowWindow SlideShowWindow { get; private set; }

		public IntPtr WindowHandle => PowerPointObject != null ? new IntPtr(PowerPointObject.HWND) : IntPtr.Zero;

		public bool IsLinkedWithApplication
		{
			get
			{
				if (!Process.GetProcessesByName("POWERPNT").Any())
				{
					PowerPointObject = null;
					return false;
				}
				try
				{
					if (PowerPointObject == null)
						PowerPointObject = GetExistedPowerPoint();
					return PowerPointObject.Caption != "";
				}
				catch
				{
					PowerPointObject = null;
					return false;
				}
			}
		}

		public virtual bool Connect(bool forceNewObject = false)
		{
			try
			{
				MessageFilter.Register();
				if (forceNewObject)
				{
					_isFirstLaunch = GetExistedPowerPoint() == null;
					PowerPointObject = CreateNewPowerPoint();
				}
				else
					PowerPointObject = GetExistedPowerPoint();
				PowerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
			}
			catch
			{
				PowerPointObject = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return PowerPointObject != null;
		}

		private Application CreateNewPowerPoint()
		{
			return new Application();
		}

		private Application GetExistedPowerPoint()
		{
			try
			{
				_isFirstLaunch = false;
				return Marshal.GetActiveObject("PowerPoint.Application") as Application;
			}
			catch
			{
				return null;
			}
		}

		public void Disconnect(bool closeIfFirstLaunch = false)
		{
			CloseSlideSourcePresentation();
			if (_isFirstLaunch && closeIfFirstLaunch)
			{
				Close();
				_isFirstLaunch = false;
			}
			Utils.ReleaseComObject(PowerPointObject);
			PowerPointObject = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		protected void Close()
		{
			try
			{
				PowerPointObject.Quit();
				uint lpdwProcessId;
				WinAPIHelper.GetWindowThreadProcessId(WindowHandle, out lpdwProcessId);
				var powerPointProcessId = (int)lpdwProcessId;
				Process.GetProcessById(powerPointProcessId).CloseMainWindow();
			}
			catch { }
			finally
			{
				try
				{
					Process.GetProcessesByName("POWERPNT").ToList().ForEach(p => p.Kill());
				}
				catch { }
			}
		}

		public Presentation GetActivePresentation(bool create = true)
		{
			Presentation presentation;
			try
			{
				presentation = PowerPointObject.ActivePresentation;
			}
			catch
			{
				presentation = null;
				if (create)
				{
					try
					{
						MessageFilter.Register();
						if (PowerPointObject.Presentations.Count == 0)
						{
							var presentations = PowerPointObject.Presentations;
							presentation = presentations.Add(MsoTriState.msoCTrue);
							Utils.ReleaseComObject(presentations);
							var slides = presentation.Slides;
							slides.Add(1, PpSlideLayout.ppLayoutTitle);
							Utils.ReleaseComObject(slides);
						}
						else
						{
							var presentations = PowerPointObject.Presentations;
							presentation = presentations[1];
							Utils.ReleaseComObject(presentations);
						}
					}
					catch
					{
						presentation = null;
					}
					finally
					{
						MessageFilter.Revoke();
					}
				}
			}
			return presentation;
		}

		public int GetActiveSlideIndex()
		{
			var slideIndex = -1;
			try
			{
				slideIndex = ((Slide)PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
			}
			catch
			{
				try
				{
					PowerPointObject.Activate();
					slideIndex = ((Slide)PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
				}
				catch { }
			}
			return slideIndex;
		}

		public bool ExportPresentationAsImages(string sourceFilePath, string destinationFolderPath)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				var presentation = PowerPointObject.Presentations.Open(sourceFilePath, WithWindow: MsoTriState.msoFalse);
				presentation.Final = false;
				presentation.Export(destinationFolderPath, "PNG");
				presentation.Close();
				Utils.ReleaseComObject(presentation);
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public bool ExportPresentationAsPdf(string originalFileName, string pdfFileName)
		{
			bool result = false;
			try
			{
				if (!String.IsNullOrEmpty(originalFileName))
				{
					if (PowerPointObject != null)
					{
						MessageFilter.Register();
						var presentationObject = PowerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
						presentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
						presentationObject.Close();
					}
				}
				else
					SlideSourcePresentation.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void ExportSlideAsPdf(int slideIndex, string destinationFileName)
		{
			try
			{
				MessageFilter.Register();
				if (slideIndex == -1)
				{
					ExportPresentationAsPdf(null, destinationFileName);
				}
				else
				{
					var presentationName = Path.GetTempFileName();
					SaveSingleSlide(slideIndex, presentationName);
					var singleSlidePresentation = PowerPointObject.Presentations.Open(presentationName, WithWindow: MsoTriState.msoFalse);
					singleSlidePresentation.SaveAs(destinationFileName, PpSaveAsFileType.ppSaveAsPDF);
					singleSlidePresentation.Close();
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AppendSlide(int slideIndex, string templatePath = "")
		{
			try
			{
				MessageFilter.Register();
				var indexToPaste = GetActiveSlideIndex();
				if (!string.IsNullOrEmpty(templatePath))
					SlideSourcePresentation.ApplyTheme(templatePath);
				for (var i = 1; i <= SlideSourcePresentation.Slides.Count; i++)
				{
					if ((i != slideIndex) && (slideIndex != -1)) continue;
					var slide = SlideSourcePresentation.Slides[i];
					var activeSlides = GetActivePresentation().Slides;
					activeSlides.InsertFromFile(SlideSourcePresentation.FullName, indexToPaste, i, i);
					indexToPaste++;
					var insertedSlide = activeSlides[indexToPaste];
					var design = GetDesignFromSlide(slide, GetActivePresentation());
					insertedSlide.Design = design ?? slide.Design;
					insertedSlide.ColorScheme = slide.ColorScheme;
					insertedSlide.Select();
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		private Design GetDesignFromSlide(Slide slide, Presentation presentation)
		{
			foreach (Design design in presentation.Designs)
				if (design.Name == slide.Design.Name)
					return design;
			return null;
		}

		private void MakeDesignUnique(Slide slide, Design design)
		{
			while (!(design.SlideMaster.Shapes.Count <= slide.Design.SlideMaster.Shapes.Count))
			{
				if (design.SlideMaster.Shapes.Count > 0)
					design.SlideMaster.Shapes[design.SlideMaster.Shapes.Count].Delete();
				else
					break;
			}
		}

		public void RenameDefaultOfficeDesigns(string sourcePresentationPath)
		{
			try
			{
				MessageFilter.Register();

				var sourcePresentation = PowerPointObject.Presentations.Open(sourcePresentationPath,
					WithWindow: MsoTriState.msoFalse);

				var random = new Random();
				const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

				var needTosave = false;
				foreach (Design design in sourcePresentation.Designs)
				{
					if (String.IsNullOrEmpty(design.Name)) continue;
					if (!design.Name.ToUpper().Contains("OFFICE THEME")) continue;
					design.Name = String.Format("asa_{0}",
						new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray()));
					needTosave = true;
				}
				if (needTosave)
					sourcePresentation.Save();
				sourcePresentation.Close();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void GetPresentationProperties(string path, out double width, out double height)
		{
			height = 0;
			width = 0;
			try
			{
				MessageFilter.Register();
				var presentation = PowerPointObject.Presentations.Open(path, WithWindow: MsoTriState.msoFalse);
				height = presentation.PageSetup.SlideHeight / 72;
				width = presentation.PageSetup.SlideWidth / 72;
				presentation.Close();
				Utils.ReleaseComObject(presentation);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public SlideSettings GetSlideSettings()
		{
			try
			{
				MessageFilter.Register();
				var settings = new SlideSettings();
				if (PowerPointObject?.ActivePresentation == null) return null;
				settings.SlideSize.Width = Math.Round(Convert.ToDecimal(PowerPointObject.ActivePresentation.PageSetup.SlideWidth / 72), 3);
				settings.SlideSize.Height = Math.Round(Convert.ToDecimal(PowerPointObject.ActivePresentation.PageSetup.SlideHeight / 72), 3);
				return settings;
			}
			catch
			{
				return null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void SetSlideSettings(SlideSettings settings)
		{
			if (PowerPointObject == null) return;
			if (PowerPointObject.ActivePresentation == null) return;
			SetSlideSettings(PowerPointObject.ActivePresentation, settings);
		}

		private void SetSlideSettings(Presentation presentation, SlideSettings settings)
		{
			try
			{
				MessageFilter.Register();
				presentation.PageSetup.SlideWidth = (float)settings.SlideSize.Width * 72;
				presentation.PageSetup.SlideHeight = (float)settings.SlideSize.Height * 72;

				switch (settings.SlideSize.Orientation)
				{
					case SlideOrientationEnum.Landscape:
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
						break;
					case SlideOrientationEnum.Portrait:
						presentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
						break;
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void OpenSlideSourcePresentation(FileInfo presentationFile)
		{
			MessageFilter.Register();
			try
			{
				CloseSlideSourcePresentation();
				SlideSourcePresentation = PowerPointObject.Presentations.Open(presentationFile.FullName, WithWindow: MsoTriState.msoFalse);
			}
			catch
			{
				SlideSourcePresentation = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		private void CloseSlideSourcePresentation()
		{
			try
			{
				MessageFilter.Register();
				if (SlideSourcePresentation != null)
				{
					SlideSourcePresentation.Close();
					Utils.ReleaseComObject(SlideSourcePresentation);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void PrintPresentation(string presentationPath, int currentSlideIndex, Action<Action> printActionWrapper)
		{
			using (var dlg = new PrintDialog
			{
				AllowCurrentPage = true,
				AllowPrintToFile = false,
				AllowSelection = false,
				AllowSomePages = true,
				ShowNetwork = true,
				UseEXDialog = true
			})
			{
				if (dlg.ShowDialog() != DialogResult.OK) return;

				var fromPage = dlg.PrinterSettings.FromPage;
				var toPage = 1;
				var collate = dlg.PrinterSettings.Collate;
				var copies = dlg.PrinterSettings.Copies;
				var printRange = dlg.PrinterSettings.PrintRange;
				var printerName = dlg.PrinterSettings.PrinterName;

				printActionWrapper(() =>
				{
					try
					{
						MessageFilter.Register();
						var presentation = PowerPointObject.Presentations.Open(presentationPath, WithWindow: MsoTriState.msoFalse);
						switch (printRange)
						{
							case System.Drawing.Printing.PrintRange.AllPages:
								fromPage = 1;
								toPage = presentation.Slides.Count;
								break;
							case System.Drawing.Printing.PrintRange.CurrentPage:
								fromPage = currentSlideIndex;
								toPage = currentSlideIndex;
								break;
							case System.Drawing.Printing.PrintRange.SomePages:
								if (fromPage < 1)
									fromPage = 1;
								toPage = currentSlideIndex;
								if (toPage > presentation.Slides.Count)
									toPage = presentation.Slides.Count;
								break;
						}
						presentation.PrintOptions.PrintInBackground = MsoTriState.msoFalse;
						presentation.PrintOptions.ActivePrinter = printerName;
						presentation.PrintOptions.NumberOfCopies = copies;
						presentation.PrintOut(
							fromPage,
							toPage,
							String.Empty,
							copies,
							collate ? MsoTriState.msoTrue : MsoTriState.msoFalse);
						presentation.Close();
						Utils.ReleaseComObject(presentationPath);
					}
					catch { }
					finally
					{
						MessageFilter.Revoke();
					}
				});
			}
		}

		public void SaveSingleSlide(int slideIndex, string destinationFileName)
		{
			var singleSlidePresentation = PowerPointObject.Presentations.Add(MsoTriState.msoFalse);
			singleSlidePresentation.PageSetup.SlideOrientation = SlideSourcePresentation.PageSetup.SlideOrientation;
			var slide = SlideSourcePresentation.Slides[slideIndex];
			var destinationSlides = singleSlidePresentation.Slides;
			destinationSlides.InsertFromFile(SlideSourcePresentation.FullName, 0, slideIndex, slideIndex);
			var insertedSlide = destinationSlides[1];
			insertedSlide.Design = slide.Design;
			insertedSlide.ColorScheme = slide.ColorScheme;
			singleSlidePresentation.SaveAs(destinationFileName);
			singleSlidePresentation.Close();
		}

		public void SetVisibility(bool visible)
		{
			PowerPointObject.Visible = visible ? MsoTriState.msoTrue : MsoTriState.msoFalse;
		}

		public void ViewSlideShow()
		{
			MessageFilter.Register();
			try
			{
				SlideSourcePresentation.SlideShowSettings.ShowType = PpSlideShowType.ppShowTypeKiosk;
				SlideSourcePresentation.SlideShowSettings.ShowWithAnimation = MsoTriState.msoFalse;
				SlideSourcePresentation.SlideShowSettings.LoopUntilStopped = MsoTriState.msoTrue;
				SlideShowWindow = SlideSourcePresentation.SlideShowSettings.Run();
				if (PowerPointObject.Windows.Count > 0)
					PowerPointObject.Activate();
			}
			catch
			{
				SlideShowWindow = null;
				SlideSourcePresentation = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ResizeSlideShow(IntPtr parentHandle, int parentHeight, int parentWidth)
		{
			if ((SlideShowWindow == null) || parentHandle.Equals(IntPtr.Zero) || (parentHeight == 0) || (parentWidth == 0)) return;
			var currentSlideShowHandle = new IntPtr(SlideShowWindow.HWND);
			if (currentSlideShowHandle == IntPtr.Zero) return;
			WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, 0);
			WinAPIHelper.SetParent(currentSlideShowHandle, IntPtr.Zero);
			SlideShowWindow.Height = parentHeight;
			SlideShowWindow.Width = parentWidth;
			WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, WindowShowStyle.ShowNormal);
			WinAPIHelper.SetParent(currentSlideShowHandle, parentHandle);
		}

		public void ExitSlideShow()
		{
			if (SlideShowWindow == null) return;
			try
			{
				MessageFilter.Register();
				SlideShowWindow.View.Exit();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				SlideShowWindow = null;
			}
		}

		public void ChangeAspect()
		{
			if (GetActivePresentation().PageSetup.SlideOrientation == MsoOrientation.msoOrientationHorizontal)
				GetActivePresentation().PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
			else if (GetActivePresentation().PageSetup.SlideOrientation == MsoOrientation.msoOrientationVertical)
				GetActivePresentation().PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
		}

		public Slide GetActiveSlide()
		{
			if (PowerPointObject.Windows.Count <= 0) return null;
			PowerPointObject.Activate();
			if (PowerPointObject.ActiveWindow == null) return null;
			PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
			return (Slide)PowerPointObject.ActiveWindow.View.Slide;
		}

		public bool InsertVideoIntoActivePresentation(string videoFile)
		{
			bool result;
			try
			{
				MessageFilter.Register();
				var newFile = Path.Combine(Path.GetDirectoryName(GetActivePresentation().FullName), Path.GetFileName(videoFile));
				File.Copy(videoFile, newFile, true);
				var slide = GetActiveSlide();
				if (slide != null)
				{
					var shape = slide.Shapes.AddMediaObject2(newFile);
					shape.AnimationSettings.PlaySettings.PlayOnEntry = MsoTriState.msoTrue;
					shape.AnimationSettings.AdvanceTime = 0;
					float maxWidth = (GetActivePresentation().PageSetup.SlideWidth / 10) * 9; // 5% border
					if (maxWidth < shape.Width)
					{
						shape.LockAspectRatio = MsoTriState.msoTrue;
						shape.Width = maxWidth;
					}
					shape.Left = (GetActivePresentation().PageSetup.SlideWidth - shape.Width) / 2;
					shape.Top = (GetActivePresentation().PageSetup.SlideHeight - shape.Height) / 2;
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}
	}
}
