using System;

namespace FileManager.PresentationClasses.Tags
{
	public interface ITagsEditor
	{
		bool NeedToApply { get; set; }
		void UpdateData();
		void ApplyData();
		event EventHandler<EventArgs> EditorChanged;
	}
}