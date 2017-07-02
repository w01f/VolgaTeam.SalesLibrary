using System;
using System.IO;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class FileFormatHelper
	{
		private static string GetExtension(string filePath)
		{
			return Path.GetExtension(filePath)?.ToUpper();
		}

		public static bool IsPowerPointFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".PPT":
				case ".PPTX":
				case ".PPTM":
					return true;
				default:
					return false;
			}
		}

		public static bool IsWordFile(string filePath)
		{
			return IsWordFileNewFormat(filePath) || IsWordFileOldFormat(filePath);
		}

		public static bool IsWordFileNewFormat(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".DOCX":
					return true;
				default:
					return false;
			}
		}

		public static bool IsWordFileOldFormat(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".DOC":
					return true;
				default:
					return false;
			}
		}

		public static bool IsExcelFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".XLS":
				case ".XLSX":
					return true;
				default:
					return false;
			}
		}

		public static bool IsPdfFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".PDF":
					return true;
				default:
					return false;
			}
		}

		public static bool IsVideoFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".MP4":
				case ".WMV":
				case ".MPEG":
				case ".AVI":
				case ".WMZ":
				case ".ASF":
				case ".MOV":
				case ".MPG":
				case ".M4V":
				case ".FLV":
				case ".OGV":
				case ".OGM":
				case ".OGX":
					return true;
				default:
					return false;
			}
		}

		public static bool IsMp4File(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".MP4":
					return true;
				default:
					return false;
			}
		}

		public static bool IsPngFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".PNG":
					return true;
				default:
					return false;
			}
		}

		public static bool IsJpegFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".JPG":
				case ".JPEG":
					return true;
				default:
					return false;
			}
		}

		public static bool IsGifFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".GIF":
					return true;
				default:
					return false;
			}
		}

		public static bool IsMp3File(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".MP3":
					return true;
				default:
					return false;
			}
		}

		public static bool IsAppleDocumentFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".KEY":
					return true;
				default:
					return false;
			}
		}

		public static bool IsUrlFile(string filePath)
		{
			switch (GetExtension(filePath))
			{
				case ".URL":
					return true;
				default:
					return false;
			}
		}
	}
}
