using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using SalesDepot.CoreObjects.InteropClasses;
using Application = Microsoft.Office.Interop.PowerPoint.Application;
using PrintRange = System.Drawing.Printing.PrintRange;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class PowerPointHelper
	{
		private static readonly PowerPointHelper instance = new PowerPointHelper();

		private Presentation _activePresentation;
		private SlideShowWindow _currentSlideShowWindow;

		private bool _isOpened;
		private Application _powerPointObject;
		private Presentation _slideSourcePresentationObject;

		private PowerPointHelper()
		{
			GetVersion();
		}

		public static PowerPointHelper Instance
		{
			get { return instance; }
		}

		public Application PowerPointObject
		{
			get
			{
				if (_powerPointObject == null)
					Connect();
				return _powerPointObject;
			}
		}

		public Presentation ActivePresentation
		{
			get
			{
				GetActivePresentation();
				return _activePresentation;
			}
		}

		public Presentation SlideSourcePresentation
		{
			get { return _slideSourcePresentationObject; }
		}

		public SlideShowWindow SlideShowWindow
		{
			get { return _currentSlideShowWindow; }
		}

		public bool IsLinkedWithApplication
		{
			get
			{
				Process[] proc = Process.GetProcessesByName("POWERPNT");
				if (!(proc.GetLength(0) > 0))
				{
					_powerPointObject = null;
					_isOpened = false;
				}
				else
				{
					try
					{
						string caption = _powerPointObject.Caption;
						_isOpened = true;
					}
					catch
					{
						_isOpened = false;
					}
				}
				return _isOpened;
			}
		}

		public bool PowerPointVisible
		{
			set
			{
				try
				{
					PowerPointObject.Visible = value ? MsoTriState.msoTrue : MsoTriState.msoFalse;
				}
				catch { }
			}
		}

		private void GetVersion()
		{
			try
			{
				var appVersion = new Microsoft.Office.Interop.Word.Application();
				appVersion.Visible = false;
				(appVersion).Quit();
			}
			catch { }
		}

		public bool Connect(bool background = false)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				if (background)
				{
					_powerPointObject = new Application();
				}
				else
				{
					try
					{
						_powerPointObject =
							Marshal.GetActiveObject("PowerPoint.Application") as Application;
					}
					catch { }
				}
				uint lpdwProcessId = 0;
				WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_powerPointObject.HWND), out lpdwProcessId);
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
				_isOpened = true;
				result = true;
			}
			catch
			{
				_powerPointObject = null;
				result = false;
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			CloseSlideSourcePresentation();
			Utils.ReleaseComObject(_powerPointObject);
			GC.Collect();
			_powerPointObject = null;
		}

		public void GetActivePresentation()
		{
			try
			{
				_activePresentation = PowerPointObject.ActivePresentation;
			}
			catch
			{
				try
				{
					_activePresentation = PowerPointObject.Presentations[1];
				}
				catch
				{
					_activePresentation = null;
				}
			}
		}

		public bool ConvertToPDF(string originalFileName, string pdfFileName)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(originalFileName))
				{
					if (PowerPointObject != null)
					{
						MessageFilter.Register();
						var presentationObject = PowerPointObject.Presentations.Open(originalFileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
						presentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
					}
				}
				else
					_slideSourcePresentationObject.SaveAs(pdfFileName, PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoCTrue);
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		private void CloseSlideSourcePresentation()
		{
			try
			{
				MessageFilter.Register();
				if (_slideSourcePresentationObject == null) return;
				_slideSourcePresentationObject.Close();
				Utils.ReleaseComObject(_slideSourcePresentationObject);
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
				_slideSourcePresentationObject = PowerPointObject.Presentations.Open(FileName: presentationFile.FullName, WithWindow: MsoTriState.msoFalse);
			}
			catch
			{
				_slideSourcePresentationObject = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ViewSlideShow()
		{
			MessageFilter.Register();
			try
			{
				_slideSourcePresentationObject.SlideShowSettings.ShowType = PpSlideShowType.ppShowTypeKiosk;
				_slideSourcePresentationObject.SlideShowSettings.ShowWithAnimation = MsoTriState.msoFalse;
				_slideSourcePresentationObject.SlideShowSettings.LoopUntilStopped = MsoTriState.msoTrue;
				_currentSlideShowWindow = _slideSourcePresentationObject.SlideShowSettings.Run();
			}
			catch
			{
				_currentSlideShowWindow = null;
				_slideSourcePresentationObject = null;
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ResizeSlideShow(IntPtr parentHandle, int parentHeight, int parentWidth)
		{
			if ((_currentSlideShowWindow == null) || parentHandle.Equals(IntPtr.Zero) || (parentHeight == 0) || (parentWidth == 0)) return;
			var currentSlideShowHandle = new IntPtr(_currentSlideShowWindow.HWND);
			if (currentSlideShowHandle == IntPtr.Zero) return;
			WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, 0);
			WinAPIHelper.SetParent(currentSlideShowHandle, IntPtr.Zero);
			_currentSlideShowWindow.Height = parentHeight;
			_currentSlideShowWindow.Width = parentWidth;
			WinAPIHelper.ShowWindowAsync(currentSlideShowHandle, WindowShowStyle.ShowNormal);
			WinAPIHelper.SetParent(currentSlideShowHandle, parentHandle);
		}

		public void ExitSlideShow()
		{
			if (_currentSlideShowWindow == null) return;
			try
			{
				MessageFilter.Register();
				_currentSlideShowWindow.View.Exit();
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				_currentSlideShowWindow = null;
			}
		}

		public void ChangeAspect()
		{
			if (ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationHorizontal)
				ActivePresentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationVertical;
			else if (ActivePresentation.PageSetup.SlideOrientation == MsoOrientation.msoOrientationVertical)
				ActivePresentation.PageSetup.SlideOrientation = MsoOrientation.msoOrientationHorizontal;
		}

		public Slide GetActiveSlide()
		{
			if (PowerPointObject.Windows.Count <= 0) return null;
			PowerPointObject.Activate();
			if (PowerPointObject.ActiveWindow == null) return null;
			PowerPointObject.ActiveWindow.ViewType = PpViewType.ppViewNormal;
			return (Slide)PowerPointObject.ActiveWindow.View.Slide;
		}

		public int GetActiveSlideIndex()
		{
			var slideIndex = -1;
			try
			{
				if (PowerPointObject.Windows.Count > 0)
				{
					PowerPointObject.Activate();
					if (PowerPointObject.ActiveWindow != null)
					{
						slideIndex = ((Slide)PowerPointObject.ActiveWindow.View.Slide).SlideIndex;
					}
				}
			}
			catch { }
			if (slideIndex != -1) return slideIndex;
			PowerPointObject.ActiveWindow.Selection.Unselect();
			return slideIndex;
		}

		public void AppendSlide(int slideIndex, string templatePath = "")
		{
			int currentSlideIndex = 0;

			try
			{
				MessageFilter.Register();
				var indexToPaste = GetActiveSlideIndex() + 1;

				if (!string.IsNullOrEmpty(templatePath))
					_slideSourcePresentationObject.ApplyTheme(templatePath);
				for (int i = 1; i <= _slideSourcePresentationObject.Slides.Count; i++)
				{
					if ((i != slideIndex) && (slideIndex != -1)) continue;
					if (indexToPaste <= 1) continue;
					var slide = _slideSourcePresentationObject.Slides[i];
					slide.Copy();
					var pastedRange = ActivePresentation.Slides.Paste(indexToPaste);
					indexToPaste++;
					var design = GetDesignFromSlide(slide, ActivePresentation);
					pastedRange.Design = design ?? slide.Design;
					pastedRange.ColorScheme = slide.ColorScheme;
					if (slide.FollowMasterBackground == MsoTriState.msoFalse)
					{
						pastedRange.FollowMasterBackground = MsoTriState.msoFalse;
						pastedRange.Background.Fill.Visible = slide.Background.Fill.Visible;
						pastedRange.Background.Fill.ForeColor = slide.Background.Fill.ForeColor;
						pastedRange.Background.Fill.BackColor = slide.Background.Fill.BackColor;

						switch (slide.Background.Fill.Type)
						{
							case MsoFillType.msoFillTextured:
								switch (slide.Background.Fill.TextureType)
								{
									case MsoTextureType.msoTexturePreset:
										pastedRange.Background.Fill.PresetTextured(slide.Background.Fill.PresetTexture);
										break;
								}
								break;
							case MsoFillType.msoFillSolid:
								pastedRange.Background.Fill.Transparency = 0;
								pastedRange.Background.Fill.Solid();
								break;
							case MsoFillType.msoFillPicture:
								if (slide.Shapes.Count > 0)
									(slide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
								MsoTriState masterShape = slide.DisplayMasterShapes;
								slide.DisplayMasterShapes = MsoTriState.msoFalse;
								slide.Export(Path.Combine(Utils.Manager.TempFolder.FullName, slide.SlideID + ".png"), "PNG", -1, -1);
								pastedRange.Background.Fill.UserPicture(Path.Combine(Utils.Manager.TempFolder.FullName, slide.SlideID + ".png"));
								var file = new FileInfo(Path.Combine(Utils.Manager.TempFolder.FullName, slide.SlideID + ".png"));
								if (file.Exists)
									file.Delete();
								slide.DisplayMasterShapes = masterShape;
								if (slide.Shapes.Count > 0)
									(slide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
								break;
							case MsoFillType.msoFillPatterned:
								pastedRange.Background.Fill.Patterned(slide.Background.Fill.Pattern);
								break;
							case MsoFillType.msoFillGradient:
								switch (slide.Background.Fill.GradientColorType)
								{
									case MsoGradientColorType.msoGradientTwoColors:
										pastedRange.Background.Fill.TwoColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant);
										break;
									case MsoGradientColorType.msoGradientPresetColors:
										pastedRange.Background.Fill.PresetGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.PresetGradientType);
										break;
									case MsoGradientColorType.msoGradientOneColor:
										pastedRange.Background.Fill.OneColorGradient(slide.Background.Fill.GradientStyle, slide.Background.Fill.GradientVariant, slide.Background.Fill.GradientDegree);
										break;
								}
								break;
						}
					}
					MakeDesignUnique(slide, pastedRange.Design);
					ActivePresentation.Slides[indexToPaste - 1].Select();
					currentSlideIndex = indexToPaste - 1;
				}
				if (currentSlideIndex != 0)
					ActivePresentation.Slides[currentSlideIndex].Select();
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

		public bool InsertVideoIntoActivePresentation(string videoFile, float left, float top, float width, float height)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				var presentationFile = new FileInfo(ActivePresentation.FullName);
				string newFile = Path.Combine(presentationFile.DirectoryName, Path.GetFileName(videoFile));
				File.Copy(videoFile, newFile, true);
				Slide slide = GetActiveSlide();
				if (slide != null)
				{
					Shape shape = slide.Shapes.AddMediaObject(newFile, left, top, width, height);
					shape.AnimationSettings.PlaySettings.PlayOnEntry = MsoTriState.msoTrue;
					shape.AnimationSettings.AdvanceTime = 0;
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

		public void PrintPresentation(int currentSlideIndex)
		{
			try
			{
				MessageFilter.Register();

				int fromPage = 1;
				int toPage = 1;

				var dlg = new PrintDialog();
				dlg.AllowCurrentPage = true;
				dlg.AllowPrintToFile = false;
				dlg.AllowSelection = false;
				dlg.AllowSomePages = true;
				dlg.ShowNetwork = true;
				dlg.UseEXDialog = true;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					switch (dlg.PrinterSettings.PrintRange)
					{
						case PrintRange.AllPages:
							fromPage = 1;
							toPage = _slideSourcePresentationObject.Slides.Count;
							break;
						case PrintRange.CurrentPage:
							fromPage = currentSlideIndex;
							toPage = currentSlideIndex;
							break;
						case PrintRange.SomePages:
							fromPage = dlg.PrinterSettings.FromPage;
							if (fromPage < 1)
								fromPage = 1;
							toPage = currentSlideIndex;
							if (toPage > _slideSourcePresentationObject.Slides.Count)
								toPage = _slideSourcePresentationObject.Slides.Count;
							break;
					}
					_slideSourcePresentationObject.PrintOptions.ActivePrinter = dlg.PrinterSettings.PrinterName;
					_slideSourcePresentationObject.PrintOptions.NumberOfCopies = dlg.PrinterSettings.Copies;
					if (dlg.PrinterSettings.Collate)
						_slideSourcePresentationObject.PrintOut(fromPage, toPage, string.Empty, dlg.PrinterSettings.Copies, MsoTriState.msoTrue);
					else
						_slideSourcePresentationObject.PrintOut(fromPage, toPage, string.Empty, dlg.PrinterSettings.Copies, MsoTriState.msoFalse);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ExportPresentationAsPDF(int slideIndex, string destinationFileName)
		{
			try
			{
				MessageFilter.Register();
				if (slideIndex == -1)
				{
					_slideSourcePresentationObject.SaveAs(destinationFileName, PpSaveAsFileType.ppSaveAsPDF);
				}
				else
				{
					string presentationName = Path.GetTempFileName();
					SaveSingleSlide(slideIndex, presentationName);
					Presentation singleSlidePresentation = PowerPointObject.Presentations.Open(presentationName, WithWindow: MsoTriState.msoFalse);
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

		public void SaveSingleSlide(int slideIndex, string destinationFileName)
		{
			Presentation singleSlidePresentation = PowerPointObject.Presentations.Add(MsoTriState.msoFalse);
			singleSlidePresentation.PageSetup.SlideOrientation = _slideSourcePresentationObject.PageSetup.SlideOrientation;
			Slide singleSlide = _slideSourcePresentationObject.Slides[slideIndex];
			singleSlide.Copy();
			SlideRange pastedRange = singleSlidePresentation.Slides.Paste();
			Design design = GetDesignFromSlide(singleSlide, singleSlidePresentation);
			if (design != null)
				pastedRange.Design = design;
			else
				pastedRange.Design = singleSlide.Design;
			pastedRange.ColorScheme = singleSlide.ColorScheme;
			if (singleSlide.FollowMasterBackground == MsoTriState.msoFalse)
			{
				pastedRange.FollowMasterBackground = MsoTriState.msoFalse;
				pastedRange.Background.Fill.Visible = singleSlide.Background.Fill.Visible;
				pastedRange.Background.Fill.ForeColor = singleSlide.Background.Fill.ForeColor;
				pastedRange.Background.Fill.BackColor = singleSlide.Background.Fill.BackColor;

				switch (singleSlide.Background.Fill.Type)
				{
					case MsoFillType.msoFillTextured:
						switch (singleSlide.Background.Fill.TextureType)
						{
							case MsoTextureType.msoTexturePreset:
								pastedRange.Background.Fill.PresetTextured(singleSlide.Background.Fill.PresetTexture);
								break;
						}
						break;
					case MsoFillType.msoFillSolid:
						pastedRange.Background.Fill.Transparency = 0;
						pastedRange.Background.Fill.Solid();
						break;
					case MsoFillType.msoFillPicture:
						if (singleSlide.Shapes.Count > 0)
							(singleSlide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
						MsoTriState masterShape = singleSlide.DisplayMasterShapes;
						singleSlide.DisplayMasterShapes = MsoTriState.msoFalse;
						singleSlide.Export(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png", "PNG", -1, -1);
						pastedRange.Background.Fill.UserPicture(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png");
						var file = new FileInfo(Path.GetTempPath() + @"\" + singleSlide.SlideID + ".png");
						if (file.Exists)
							file.Delete();
						singleSlide.DisplayMasterShapes = masterShape;
						if (singleSlide.Shapes.Count > 0)
							(singleSlide.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
						break;
					case MsoFillType.msoFillPatterned:
						pastedRange.Background.Fill.Patterned(singleSlide.Background.Fill.Pattern);
						break;
					case MsoFillType.msoFillGradient:
						switch (singleSlide.Background.Fill.GradientColorType)
						{
							case MsoGradientColorType.msoGradientTwoColors:
								pastedRange.Background.Fill.TwoColorGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant);
								break;
							case MsoGradientColorType.msoGradientPresetColors:
								pastedRange.Background.Fill.PresetGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant, singleSlide.Background.Fill.PresetGradientType);
								break;
							case MsoGradientColorType.msoGradientOneColor:
								pastedRange.Background.Fill.OneColorGradient(singleSlide.Background.Fill.GradientStyle, singleSlide.Background.Fill.GradientVariant, singleSlide.Background.Fill.GradientDegree);
								break;
						}
						break;
				}
			}
			MakeDesignUnique(singleSlide, pastedRange.Design);

			singleSlidePresentation.SaveAs(destinationFileName);
			singleSlidePresentation.Close();
		}
	}

	public class MessageFilter : IOleMessageFilter
	{
		//
		// Class containing the IOleMessageFilter
		// thread error-handling functions.

		// Start the filter.

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.

		#region IOleMessageFilter Members
		int IOleMessageFilter.HandleInComingCall(int dwCallType,
												 IntPtr hTaskCaller, int dwTickCount, IntPtr
																						  lpInterfaceInfo)
		{
			//Return the flag SERVERCALL_ISHANDLED.
			return 0;
		}

		// Thread call was rejected, so try again.
		int IOleMessageFilter.RetryRejectedCall(IntPtr
													hTaskCallee, int dwTickCount, int dwRejectType)
		{
			if (dwRejectType == 2)
			// flag = SERVERCALL_RETRYLATER.
			{
				// Retry the thread call immediately if return >=0 & 
				// <100.
				return 99;
			}
			// Too busy; cancel call.
			return -1;
		}

		int IOleMessageFilter.MessagePending(IntPtr hTaskCallee,
											 int dwTickCount, int dwPendingType)
		{
			//Return the flag PENDINGMSG_WAITDEFPROCESS.
			return 2;
		}
		#endregion

		public static void Register()
		{
			IOleMessageFilter newFilter = new MessageFilter();
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(newFilter, out oldFilter);
		}

		// Done with the filter, close it.
		public static void Revoke()
		{
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(null, out oldFilter);
		}

		// Implement the IOleMessageFilter interface.
		[DllImport("Ole32.dll")]
		private static extern int
			CoRegisterMessageFilter(IOleMessageFilter newFilter, out
				                                                     IOleMessageFilter oldFilter);
	}

	[ComImport, Guid("00000016-0000-0000-C000-000000000046"),
	 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IOleMessageFilter
	{
		[PreserveSig]
		int HandleInComingCall(
			int dwCallType,
			IntPtr hTaskCaller,
			int dwTickCount,
			IntPtr lpInterfaceInfo);

		[PreserveSig]
		int RetryRejectedCall(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwRejectType);

		[PreserveSig]
		int MessagePending(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwPendingType);
	}
}