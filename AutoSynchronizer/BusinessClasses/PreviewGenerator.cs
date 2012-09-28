using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class VideoPreviewGenerator : SalesDepot.CoreObjects.BusinessClasses.IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }

        public VideoPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
        }
        #endregion
    }
}
