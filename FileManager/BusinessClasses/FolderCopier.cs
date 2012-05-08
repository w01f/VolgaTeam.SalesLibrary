using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager.BusinessClasses
{
    class FolderCopier
    {
        public static LibraryFolder CopiedFolder { get; set; }
        public static bool PasteReady { get; set; }
        public static DataGridView CopiedGrid { get; set; }
        public static int SourceRowIndex { get; set; }

        public static void Copy(DataGridView sourceGrid, int rowIndex)
        {
            CopiedGrid = sourceGrid;
            PasteReady = true;
            SourceRowIndex = rowIndex;
        }

        public static void Paste(DataGridView destGrid)
        {
            CopiedGrid.Rows.RemoveAt(SourceRowIndex);

            if (destGrid.SelectedRows.Count > 0)
                destGrid.Rows.Insert(destGrid.SelectedRows[0].Index, CopiedFolder.Name, CopiedFolder.Identifier);
            else
                destGrid.Rows.Add(CopiedFolder.Name, CopiedFolder.Identifier);

            CopiedGrid = null;
            CopiedFolder = null;
            PasteReady = false;
            SourceRowIndex = -1;
        }
    }
}
