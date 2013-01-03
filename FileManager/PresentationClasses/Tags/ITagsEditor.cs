using System;

namespace FileManager.PresentationClasses.Tags
{
	public interface ITagsEditor
	{
		void UpdateData();
		void ApplyData();
		event EventHandler<EventArgs> EditorChanged;
	}
}