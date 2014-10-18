using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace SalesDepot.CoreObjects.InteropClasses
{
	public class PowerPointHelper
	{
		private static readonly PowerPointHelper instance = new PowerPointHelper();

		private Application _powerPointObject;

		private PowerPointHelper() { }

		public static PowerPointHelper Instance
		{
			get { return instance; }
		}

		public bool Connect()
		{
			bool result = false;
			MessageFilter.Register();
			try
			{
				_powerPointObject = new Application();
				_powerPointObject.DisplayAlerts = PpAlertLevel.ppAlertsNone;
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			Utils.ReleaseComObject(_powerPointObject);
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
					Presentation presentation = _powerPointObject.Presentations.Open(sourceFilePath, WithWindow: MsoTriState.msoFalse);
					presentation.Export(destinationFolderPath, "PNG");
					presentation.Close();
					Utils.ReleaseComObject(presentation);
					if (connect)
						Disconnect();
					result = true;
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void ExportPresentationAllFormats(string sourceFilePath, string destinationFolderPath, bool generateImages, bool generateText, out bool update)
		{
			var pdfDestination = Path.Combine(destinationFolderPath, "pdf");
			var updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination, "*.pdf").Length > 0) && generateImages;
			if (updatePdf && !Directory.Exists(pdfDestination))
				Directory.CreateDirectory(pdfDestination);
			var pngDestination = Path.Combine(destinationFolderPath, "png");
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0) && generateImages;
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			var pngPhoneDestination = Path.Combine(destinationFolderPath, "png_phone");
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination, "*.png").Length > 0) && generateImages;
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			var jpgDestination = Path.Combine(destinationFolderPath, "jpg");
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0) && generateImages;
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			var jpgPhoneDestination = Path.Combine(destinationFolderPath, "jpg_phone");
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination, "*.jpg").Length > 0) && generateImages;
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			var thumbDestination = Path.Combine(destinationFolderPath, "thumbs");
			var updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination, "*.png").Length > 0) && generateImages;
			if (updateThumbs && !Directory.Exists(thumbDestination))
				Directory.CreateDirectory(thumbDestination);
			var thumbsPhoneDestination = Path.Combine(destinationFolderPath, "thumbs_phone");
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0) && generateImages;
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);
			var pptDestination = Path.Combine(destinationFolderPath, "ppt");
			var updatePpt = !(Directory.Exists(pptDestination) && Directory.GetFiles(pptDestination, "*.ppt").Length > 0) && generateImages;
			if (updatePpt && !Directory.Exists(pptDestination))
				Directory.CreateDirectory(pptDestination);
			var pptxDestination = Path.Combine(destinationFolderPath, "pptx");
			var updatePptx = !(Directory.Exists(pptxDestination) && Directory.GetFiles(pptxDestination, "*.pptx").Length > 0) && generateImages;
			if (updatePptx && !Directory.Exists(pptxDestination))
				Directory.CreateDirectory(pptxDestination);
			var txtDestination = Path.Combine(destinationFolderPath, "txt");
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0) && generateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			update = false;
			if (!updatePdf && !updatePng && !updateJpg && !updateThumbs && !updatePpt && !updatePptx && !updateTxt && !updatePngPhone && !updateJpgPhone && !updateThumbsPhone) return;
			update = true;
			try
			{
				if (Connect())
				{
					MessageFilter.Register();
					Presentation presentation = _powerPointObject.Presentations.Open(sourceFilePath, WithWindow: MsoTriState.msoFalse);

					var content = new StringBuilder();

					if (updatePng || updateJpg || updateThumbs || updatePpt || updatePptx || updateTxt || updatePngPhone || updateJpgPhone || updateThumbsPhone)
					{
						var i = 1;
						var thumbHeight = (int)presentation.PageSetup.SlideHeight / 10;
						var thumbWidth = (int)presentation.PageSetup.SlideWidth / 10;
						var phoneHeight = (int)(presentation.PageSetup.SlideHeight / 1.5);
						var phoneWidth = (int)(presentation.PageSetup.SlideWidth / 1.5);
						var thumbPhoneHeight = (int)presentation.PageSetup.SlideHeight / 4;
						var thumbPhoneWidth = (int)presentation.PageSetup.SlideWidth / 4;
						foreach (Slide slide in presentation.Slides)
						{
							if (updatePng)
								slide.Export(Path.Combine(pngDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "png" })), "PNG");
							if (updatePngPhone)
								slide.Export(Path.Combine(pngPhoneDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "png" })), "PNG", phoneWidth, phoneHeight);
							if (updateJpg)
								slide.Export(Path.Combine(jpgDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "jpg" })), "JPG");
							if (updateJpgPhone)
								slide.Export(Path.Combine(jpgPhoneDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "jpg" })), "JPG", phoneWidth, phoneHeight);
							if (updateThumbs)
								slide.Export(Path.Combine(thumbDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "png" })), "PNG", thumbWidth, thumbHeight);
							if (updateThumbsPhone)
								slide.Export(Path.Combine(thumbsPhoneDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "png" })), "PNG", thumbPhoneWidth, thumbPhoneHeight);

							if (updatePpt || updatePptx)
							{
								Presentation singleSlidePresentation = _powerPointObject.Presentations.Add(MsoTriState.msoFalse);
								CopyPasteSlide(slide, singleSlidePresentation);
								if (updatePpt)
									singleSlidePresentation.SaveCopyAs(Path.Combine(pptDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "ppt" })), PpSaveAsFileType.ppSaveAsPresentation);
								if (updatePptx)
									singleSlidePresentation.SaveCopyAs(Path.Combine(pptxDestination, string.Format("Slide{0}.{1}", new[] { i.ToString(), "pptx" })), PpSaveAsFileType.ppSaveAsDefault);
								singleSlidePresentation.Close();
								Utils.ReleaseComObject(singleSlidePresentation);
							}
							if (updateTxt)
							{
								foreach (Shape shape in slide.Shapes.Cast<Shape>().Where(shape => shape.HasTextFrame == MsoTriState.msoTrue))
									content.AppendLine(shape.TextFrame.TextRange.Text.Trim());
							}
							i++;
						}
					}

					if (updateTxt && content.Length > 0)
						using (var sw = new StreamWriter(Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt")), false))
						{
							sw.Write(content.ToString());
							sw.Flush();
						}

					if (updatePdf)
						presentation.ExportAsFixedFormat(Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "pdf")), PpFixedFormatType.ppFixedFormatTypePDF);
					Utils.ReleaseComObject(presentation);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				Disconnect();
			}
		}

		public void CopyPasteSlide(Slide source, Presentation destination)
		{
			try
			{
				source.Copy();
				SlideRange pastedRange = destination.Slides.Paste();
				Design design = GetDesignFromSlide(source, destination);
				if (design != null)
					pastedRange.Design = design;
				else
					pastedRange.Design = source.Design;
				pastedRange.ColorScheme = source.ColorScheme;
				if (source.FollowMasterBackground == MsoTriState.msoFalse)
				{
					pastedRange.FollowMasterBackground = MsoTriState.msoFalse;
					pastedRange.Background.Fill.Visible = source.Background.Fill.Visible;
					pastedRange.Background.Fill.ForeColor = source.Background.Fill.ForeColor;
					pastedRange.Background.Fill.BackColor = source.Background.Fill.BackColor;

					switch (source.Background.Fill.Type)
					{
						case MsoFillType.msoFillTextured:
							switch (source.Background.Fill.TextureType)
							{
								case MsoTextureType.msoTexturePreset:
									pastedRange.Background.Fill.PresetTextured(source.Background.Fill.PresetTexture);
									break;
							}
							break;
						case MsoFillType.msoFillSolid:
							pastedRange.Background.Fill.Transparency = 0;
							pastedRange.Background.Fill.Solid();
							break;
						case MsoFillType.msoFillPicture:
							if (source.Shapes.Count > 0)
								(source.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
							MsoTriState masterShape = source.DisplayMasterShapes;
							source.DisplayMasterShapes = MsoTriState.msoFalse;

							string tempFile = Path.GetTempFileName();
							source.Export(tempFile, "PNG");
							pastedRange.Background.Fill.UserPicture(tempFile);
							if (File.Exists(tempFile))
								File.Delete(tempFile);

							source.DisplayMasterShapes = masterShape;
							if (source.Shapes.Count > 0)
								(source.Shapes.Range(1)).Visible = MsoTriState.msoFalse;
							break;
						case MsoFillType.msoFillPatterned:
							pastedRange.Background.Fill.Patterned(source.Background.Fill.Pattern);
							break;
						case MsoFillType.msoFillGradient:
							switch (source.Background.Fill.GradientColorType)
							{
								case MsoGradientColorType.msoGradientTwoColors:
									pastedRange.Background.Fill.TwoColorGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant);
									break;
								case MsoGradientColorType.msoGradientPresetColors:
									pastedRange.Background.Fill.PresetGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant, source.Background.Fill.PresetGradientType);
									break;
								case MsoGradientColorType.msoGradientOneColor:
									pastedRange.Background.Fill.OneColorGradient(source.Background.Fill.GradientStyle, source.Background.Fill.GradientVariant, source.Background.Fill.GradientDegree);
									break;
							}
							break;
					}
				}
				MakeDesignUnique(source, pastedRange.Design);
			}
			catch { }
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

		public void GetPresentationProperties(ILibraryLink file)
		{
			try
			{
				MessageFilter.Register();
				Presentation presentation = _powerPointObject.Presentations.Open(file.OriginalPath, WithWindow: MsoTriState.msoFalse);
				if (file.PresentationProperties == null)
					file.PresentationProperties = new PresentationProperties();
				file.PresentationProperties.Height = presentation.PageSetup.SlideHeight / 72;
				file.PresentationProperties.Width = presentation.PageSetup.SlideWidth / 72;
				file.PresentationProperties.LastUpdate = DateTime.Now;
				presentation.Close();
				Utils.ReleaseComObject(presentation);
			}
			catch { }
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

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.
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