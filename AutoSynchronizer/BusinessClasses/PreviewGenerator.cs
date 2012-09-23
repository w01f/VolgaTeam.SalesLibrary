using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class VideoPreviewGenerator : SalesDepot.CoreObjects.BusinessClasses.IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        //Mockup
        public void GeneratePreview()
        {
        }
        #endregion
    }
}
