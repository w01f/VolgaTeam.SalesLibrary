using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace SalesDepot.CoreObjects.InteropClasses
{
	public class PowerPointHelper
	{
		private static PowerPointHelper instance = new PowerPointHelper();

		private PowerPoint.Application _powerPointObject;

		private PowerPointHelper()
		{
		}

		public static PowerPointHelper Instance
		{
			get
			{
				return instance;
			}
		}

		public bool Connect()
		{
			bool result = false;
			MessageFilter.Register();
			try
			{
				_powerPointObject = new Microsoft.Office.Interop.PowerPoint.Application();
				_powerPointObject.DisplayAlerts = PowerPoint.PpAlertLevel.ppAlertsNone;
				result = true;
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			ToolClasses.Utils.ReleaseComObject(_powerPointObject);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public bool ExportPresentationAsImages(string sourceFilePath, string destinationFolderPath, bool connect = true)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				if (Connect() || !connect)
				{
					PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: sourceFilePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
					presentation.Export(Path: destinationFolderPath, FilterName: "PNG");
					presentation.Close();
					ToolClasses.Utils.ReleaseComObject(presentation);
					if (connect)
						Disconnect();
					result = true;
				}
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void ExportPresentationAllFormats(string sourceFilePath, string destinationFolderPath, out bool update)
		{
			string pdfDestination = Path.Combine(destinationFolderPath, "pdf");
			bool updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination, "*.pdf").Length > 0);
			if (!Directory.Exists(pdfDestination))
				Directory.CreateDirectory(pdfDestination);
			string pngDestination = Path.Combine(destinationFolderPath, "png");
			bool updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
			if (!Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			string pngPhoneDestination = Path.Combine(destinationFolderPath, "png_phone");
			bool updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination, "*.png").Length > 0);
			if (!Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			string jpgDestination = Path.Combine(destinationFolderPath, "jpg");
			bool updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
			if (!Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			string jpgPhoneDestination = Path.Combine(destinationFolderPath, "jpg_phone");
			bool updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination, "*.jpg").Length > 0);
			if (!Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			string thumbDestination = Path.Combine(destinationFolderPath, "thumbs");
			bool updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination, "*.png").Length > 0);
			if (!Directory.Exists(thumbDestination))
				Directory.CreateDirectory(thumbDestination);
			string thumbsPhoneDestination = Path.Combine(destinationFolderPath, "thumbs_phone");
			bool updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0);
			if (!Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);
			string pptDestination = Path.Combine(destinationFolderPath, "ppt");
			bool updatePpt = !(Directory.Exists(pptDestination) && Directory.GetFiles(pptDestination, "*.ppt").Length > 0);
			if (!Directory.Exists(pptDestination))
				Directory.CreateDirectory(pptDestination);
			string pptxDestination = Path.Combine(destinationFolderPath, "pptx");
			bool updatePptx = !(Directory.Exists(pptxDestination) && Directory.GetFiles(pptxDestination, "*.pptx").Length > 0);
			if (!Directory.Exists(pptxDestination))
				Directory.CreateDirectory(pptxDestination);
			string txtDestination = Path.Combine(destinationFolderPath, "txt");
			bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
			if (!Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			update = false;
			if (updatePdf || updatePng || updateJpg || updateThumbs || updatePpt || updatePptx || updateTxt || updatePngPhone || updateJpgPhone || updateThumbsPhone)
			{
				update = true;
				try
				{
					if (Connect())
					{
						MessageFilter.Register();
						PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: sourceFilePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);

						StringBuilder content = new StringBuilder();

						if (updatePng || updateJpg || updateThumbs || updatePpt || updatePptx || updateTxt || updatePngPhone || updateJpgPhone || updateThumbsPhone)
						{
							int i = 1;
							int thumbHeight = (int)presentation.PageSetup.SlideHeight / 10;
							int thumbWidth = (int)presentation.PageSetup.SlideWidth / 10;
							int phoneHeight = (int)(presentation.PageSetup.SlideHeight / 1.5);
							int phoneWidth = (int)(presentation.PageSetup.SlideWidth / 1.5);
							int thumbPhoneHeight = (int)presentation.PageSetup.SlideHeight / 4;
							int thumbPhoneWidth = (int)presentation.PageSetup.SlideWidth / 4;
							foreach (PowerPoint.Slide slide in presentation.Slides)
							{
								if (updatePng)
									slide.Export(Path.Combine(pngDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "png" })), "PNG");
								if (updatePngPhone)
									slide.Export(Path.Combine(pngPhoneDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "png" })), "PNG", phoneWidth, phoneHeight);
								if (updateJpg)
									slide.Export(Path.Combine(jpgDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "jpg" })), "JPG");
								if (updateJpgPhone)
									slide.Export(Path.Combine(jpgPhoneDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "jpg" })), "JPG", phoneWidth, phoneHeight);
								if (updateThumbs)
									slide.Export(Path.Combine(thumbDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "png" })), "PNG", thumbWidth, thumbHeight);
								if (updateThumbsPhone)
									slide.Export(Path.Combine(thumbsPhoneDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "png" })), "PNG", thumbPhoneWidth, thumbPhoneHeight);

								if (updatePpt || updatePptx)
								{
									PowerPoint.Presentation singleSlidePresentation = _powerPointObject.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
									CopyPasteSlide(slide, singleSlidePresentation);
									if (updatePpt)
										singleSlidePresentation.SaveCopyAs(Path.Combine(pptDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "ppt" })), PowerPoint.PpSaveAsFileType.ppSaveAsPresentation);
									if (updatePptx)
										singleSlidePresentation.SaveCopyAs(Path.Combine(pptxDestination, string.Format("Slide{0}.{1}", new string[] { i.ToString(), "pptx" })), PowerPoint.PpSaveAsFileType.ppSaveAsDefault);
									singleSlidePresentation.Close();
									ToolClasses.Utils.ReleaseComObject(singleSlidePresentation);
								}
								if (updateTxt)
								{
									foreach (PowerPoint.Shape shape in slide.Shapes)
										if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
											content.AppendLine(shape.TextFrame.TextRange.Text.Trim());

								}
								i++;
							}
						}

						if (updateTxt && content.Length > 0)
							using (StreamWriter sw = new StreamWriter(Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt")), false))
							{
								sw.Write(content.ToString());
								sw.Flush();
							}

						if (updatePdf)
							presentation.ExportAsFixedFormat(Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "pdf")), PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF);
						ToolClasses.Utils.ReleaseComObject(presentation);
					}
				}
				catch
				{
				}
				finally
				{
					MessageFilter.Revoke();
					Disconnect();
				}
			}
		}

		public void CopyPasteSlide(PowerPoint.Slide source, PowerPoint.Presentation destination)
		{
			try
			{
				source.Copy();
				PowerPoint.SlideRange pastedRange = destination.Slides.Paste();
				PowerPoint.Design design = GetDesignFromSlide(source, destination);
				if (design != null)
					pastedRange.Design = design;
				else
					pastedRange.Design = source.Design;
				pastedRange.ColorScheme = source.ColorScheme;
				if (source.FollowMasterBackground == Microsoft.Office.Core.MsoTriState.msoFalse)
				{
					pastedRange.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
					pastedRange.Background.Fill.Visible = source.Background.Fill.Visible;
					pastedRange.Background.Fill.ForeColor = source.Background.Fill.ForeColor;
					pastedRange.Background.Fill.BackColor = source.Background.Fill.BackColor;

					switch (source.Background.Fill.Type)
					{
						case Microsoft.Office.Core.MsoFillType.msoFillTextured:
							switch (source.Background.Fill.TextureType)
							{
								case Microsoft.Office.Core.MsoTextureType.msoTexturePreset:
									pastedRange.Background.Fill.PresetTextured(source.Background.Fill.PresetTexture);
									break;
							}
							break;
						case Microsoft.Office.Core.MsoFillType.msoFillSolid:
							pastedRange.Background.Fill.Transparency = 0;
							pastedRange.Background.Fill.Solid();
							break;
						case Microsoft.Office.Core.MsoFillType.msoFillPicture:
							if (source.Shapes.Count > 0)
								(source.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
							Microsoft.Office.Core.MsoTriState masterShape = source.DisplayMasterShapes;
							source.DisplayMasterShapes = Microsoft.Office.Core.MsoTriState.msoFalse;

							string tempFile = Path.GetTempFileName();
							source.Export(tempFile, "PNG");
							pastedRange.Background.Fill.UserPicture(tempFile);
							if (File.Exists(tempFile))
								File.Delete(tempFile);

							source.DisplayMasterShapes = masterShape;
							if (source.Shapes.Count > 0)
								(source.Shapes.Range(1)).Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
							break;
						case Microsoft.Office.Core.MsoFillType.msoFillPatterned:
							pastedRange.Background.Fill.Patterned(source.Background.Fill.Pattern);
							break;
						case Microsoft.Office.Core.MsoFillType.msoFillGradient:
							switch (source.Background.Fill.GradientColorType)
							{
								case Microsoft.Office.Core.MsoGradientColorType.msoGradientTwoColors:
									pastedRange.Background.Fill.TwoColorGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant);
									break;
								case Microsoft.Office.Core.MsoGradientColorType.msoGradientPresetColors:
									pastedRange.Background.Fill.PresetGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant, source.Background.Fill.PresetGradientType);
									break;
								case Microsoft.Office.Core.MsoGradientColorType.msoGradientOneColor:
									pastedRange.Background.Fill.OneColorGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant, source.Background.Fill.GradientDegree);
									break;
							}
							break;
					}
				}
				MakeDesignUnique(source, pastedRange.Design);
			}
			catch
			{
			}
		}

		private PowerPoint.Design GetDesignFromSlide(PowerPoint.Slide slide, PowerPoint.Presentation presentation)
		{
			foreach (PowerPoint.Design design in presentation.Designs)
				if (design.Name == slide.Design.Name)
					return design;
			return null;
		}

		private void MakeDesignUnique(PowerPoint.Slide slide, PowerPoint.Design design)
		{
			while (!(design.SlideMaster.Shapes.Count <= slide.Design.SlideMaster.Shapes.Count))
			{
				if (design.SlideMaster.Shapes.Count > 0)
					design.SlideMaster.Shapes[design.SlideMaster.Shapes.Count].Delete();
				else
					break;
			}
		}

		public void GetPresentationProperties(BusinessClasses.ILibraryFile file)
		{
			try
			{
				MessageFilter.Register();
				PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: file.OriginalPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
				if (file.PresentationProperties == null)
					file.PresentationProperties = new SalesDepot.CoreObjects.BusinessClasses.PresentationProperties();
				file.PresentationProperties.Height = presentation.PageSetup.SlideHeight / 72;
				file.PresentationProperties.Width = presentation.PageSetup.SlideWidth / 72;
				file.PresentationProperties.LastUpdate = DateTime.Now;
				presentation.Close();
				ToolClasses.Utils.ReleaseComObject(presentation);
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}

	public class MessageFilter : IOleMessageFilter
	{
		//
		// Class containing the IOleMessageFilter
		// thread error-handling functions.

		// Start the filter.
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

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.
		int IOleMessageFilter.HandleInComingCall(int dwCallType,
		  System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
		  lpInterfaceInfo)
		{
			//Return the flag SERVERCALL_ISHANDLED.
			return 0;
		}

		// Thread call was rejected, so try again.
		int IOleMessageFilter.RetryRejectedCall(System.IntPtr
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

		int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
		  int dwTickCount, int dwPendingType)
		{
			//Return the flag PENDINGMSG_WAITDEFPROCESS.
			return 2;
		}

		// Implement the IOleMessageFilter interface.
		[DllImport("Ole32.dll")]
		private static extern int
		  CoRegisterMessageFilter(IOleMessageFilter newFilter, out 
		  IOleMessageFilter oldFilter);
	}

	[ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
	InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	interface IOleMessageFilter
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
