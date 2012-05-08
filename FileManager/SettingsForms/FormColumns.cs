using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormColumns : Form
    {
        public BusinessClasses.Library Library { get; set; }
        private BusinessClasses.LibraryPage _currentPage;
        private BusinessClasses.LibraryFolder _currentFolder;
        private bool _stateChanges = false;
        private bool _changesDone = false;

        bool _column1FirstSelect = true;
        bool _column2FirstSelect = true;
        bool _column3FirstSelect = true;

        public FormColumns()
        {
            InitializeComponent();
        }

        #region Base Methods
        private string FontToString(Font font)
        {
            string str = font.Name + ", " + ((int)font.Size).ToString();
            if (font.Bold)
                str = str + ", Bold";
            if (font.Italic)
                str = str + ", Italic";
            if (font.Underline)
                str = str + ", Underline";
            if (font.Strikeout)
                str = str + ", Strikeout";
            return str;
        }

        private void GetColumnSettings()
        {
            grColumn1.Rows.Clear();
            grColumn2.Rows.Clear();
            grColumn3.Rows.Clear();

            if (_currentPage != null)
            {
                foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders.OrderBy(x => x.RowOrder))
                {
                    switch (folder.ColumnOrder)
                    {
                        case 0:
                            grColumn1.Rows.Add(folder.Name, folder.Identifier);
                            break;
                        case 1:
                            grColumn2.Rows.Add(folder.Name, folder.Identifier);
                            break;
                        case 2:
                            grColumn3.Rows.Add(folder.Name, folder.Identifier);
                            break;
                    }
                }
                grColumn1.ClearSelection();
                grColumn2.ClearSelection();
                grColumn3.ClearSelection();
            }
        }

        private void SaveColumnSettings()
        {
            if (_currentPage != null)
            {
                List<Guid> existedFolderIDs = new List<Guid>();
                BusinessClasses.LibraryFolder templateFolder = null;

                if (this.Library.ApplyForAllWindows)
                    templateFolder = _currentPage.Folders.FirstOrDefault();

                foreach (DataGridViewRow row in grColumn1.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);
                foreach (DataGridViewRow row in grColumn2.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);
                foreach (DataGridViewRow row in grColumn3.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);

                foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders.Where(x => !existedFolderIDs.Contains(x.Identifier)).ToArray())
                    folder.RemoveFromParent();

                foreach (DataGridViewRow row in grColumn1.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 0;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }

                foreach (DataGridViewRow row in grColumn2.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 1;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }

                foreach (DataGridViewRow row in grColumn3.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 2;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }
            }
        }

        private void GetWindowSettings()
        {
            if (_currentFolder != null)
            {
                pnWindows.Enabled = true;
                laLocationValue.Text = "Window " + (_currentFolder.RowOrder + 1).ToString() + " - Column " + (_currentFolder.ColumnOrder + 1).ToString();

                if (ckApllyForAllWindows.Checked)
                {
                    BusinessClasses.LibraryFolder currentPageFolder = _currentPage.Folders.FirstOrDefault();
                    if (currentPageFolder != null)
                    {
                        colorEditHeaderBack.Color = currentPageFolder.BackgroundHeaderColor;
                        colorEditHeaderFont.Color = currentPageFolder.ForeHeaderColor;
                        colorEditWindowBack.Color = currentPageFolder.BackgroundWindowColor;
                        colorEditWindowFont.Color = currentPageFolder.ForeWindowColor;
                        if (currentPageFolder.HeaderFont != null)
                        {
                            buttonEditHeaderFont.Tag = currentPageFolder.HeaderFont;
                            buttonEditHeaderFont.EditValue = FontToString(currentPageFolder.HeaderFont);
                        }
                        else
                            buttonEditHeaderFont.EditValue = string.Empty;
                    }
                }
                else
                {
                    colorEditHeaderBack.Color = _currentFolder.BackgroundHeaderColor;
                    colorEditHeaderFont.Color = _currentFolder.ForeHeaderColor;
                    colorEditWindowBack.Color = _currentFolder.BackgroundWindowColor;
                    colorEditWindowFont.Color = _currentFolder.ForeWindowColor;
                    if (_currentFolder.HeaderFont != null)
                    {
                        buttonEditHeaderFont.Tag = _currentFolder.HeaderFont;
                        buttonEditHeaderFont.EditValue = FontToString(_currentFolder.HeaderFont);
                    }
                    else
                        buttonEditHeaderFont.EditValue = string.Empty;
                }
            }
        }

        private void ClearWindowSettings()
        {
            pnWindows.Enabled = false;
            laLocationValue.Text = string.Empty;
            colorEditHeaderBack.Color = Color.White;
            colorEditHeaderFont.Color = Color.White;
            colorEditWindowBack.Color = Color.White;
            colorEditWindowFont.Color = Color.White;
            buttonEditHeaderFont.EditValue = string.Empty;
            _currentFolder = null;
        }

        private void SaveWindowSettings()
        {
            if (_currentFolder != null)
            {
                if (ckApllyForAllWindows.Checked)
                {
                    foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                    {
                        folder.BackgroundHeaderColor = colorEditHeaderBack.Color;
                        folder.ForeHeaderColor = colorEditHeaderFont.Color;
                        folder.BackgroundWindowColor = colorEditWindowBack.Color;
                        folder.ForeWindowColor = colorEditWindowFont.Color;
                        folder.HeaderFont = buttonEditHeaderFont.Tag as Font;
                    }
                    this.Library.ApplyForAllWindows = true;
                }
                else
                {
                    if (_currentFolder != null)
                    {
                        _currentFolder.BackgroundHeaderColor = colorEditHeaderBack.Color;
                        _currentFolder.ForeHeaderColor = colorEditHeaderFont.Color;
                        _currentFolder.BackgroundWindowColor = colorEditWindowBack.Color;
                        _currentFolder.ForeWindowColor = colorEditWindowFont.Color;
                        _currentFolder.HeaderFont = buttonEditHeaderFont.Tag as Font;
                        this.Library.ApplyForAllWindows = false;
                    }
                }
            }
        }

        private void GetColumnTitles()
        {
            if (_currentPage != null)
            {
                if (_currentPage.ColumnTitles.Count == 3)
                {
                    bool temp = _stateChanges;
                    tbColumn1.Text = _currentPage.ColumnTitles[0].Name;
                    colorEditColumn1Back.Color = _currentPage.ColumnTitles[0].BackgroundColor;
                    colorEditColumn1Fore.Color = _currentPage.ColumnTitles[0].ForeColor;
                    buttonEditColumn1HeaderFont.Tag = _currentPage.ColumnTitles[0].HeaderFont;
                    buttonEditColumn1HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[0].HeaderFont);
                    tbColumn2.Text = _currentPage.ColumnTitles[1].Name;
                    colorEditColumn2Back.Color = _currentPage.ColumnTitles[1].BackgroundColor;
                    colorEditColumn2Fore.Color = _currentPage.ColumnTitles[1].ForeColor;
                    buttonEditColumn2HeaderFont.Tag = _currentPage.ColumnTitles[1].HeaderFont;
                    buttonEditColumn2HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[1].HeaderFont);
                    tbColumn3.Text = _currentPage.ColumnTitles[2].Name;
                    colorEditColumn3Back.Color = _currentPage.ColumnTitles[2].BackgroundColor;
                    colorEditColumn3Fore.Color = _currentPage.ColumnTitles[2].ForeColor;
                    buttonEditColumn3HeaderFont.Tag = _currentPage.ColumnTitles[2].HeaderFont;
                    buttonEditColumn3HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[2].HeaderFont);
                    ckEnableColumnTitles.Checked = _currentPage.EnableColumnTitles;
                    ckApplyForAllColumnTitles.Checked = _currentPage.ApplyForAllColumnTitles;
                    _stateChanges = temp;
                }
            }
        }

        private void SaveColumnTitles()
        {
            if (_currentPage != null)
            {
                _currentPage.ColumnTitles.Clear();
                
                BusinessClasses.ColumnTitle column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 0;
                column.Name = tbColumn1.Text;
                column.BackgroundColor = colorEditColumn1Back.Color;
                column.ForeColor = colorEditColumn1Fore.Color;
                column.HeaderFont = buttonEditColumn1HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);

                column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 1;
                column.Name = tbColumn2.Text;
                column.BackgroundColor = colorEditColumn2Back.Color;
                column.ForeColor = colorEditColumn2Fore.Color;
                column.HeaderFont = buttonEditColumn2HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);

                column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 2;
                column.Name = tbColumn3.Text;
                column.BackgroundColor = colorEditColumn3Back.Color;
                column.ForeColor = colorEditColumn3Fore.Color;
                column.HeaderFont = buttonEditColumn3HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);


                if (ckEnableColumnTitles.Checked && (string.IsNullOrEmpty(tbColumn1.Text.Trim()) || string.IsNullOrEmpty(tbColumn2.Text.Trim()) || string.IsNullOrEmpty(tbColumn3.Text.Trim())))
                {
                    AppManager.Instance.ShowWarning("You did not set all column titles and they will be disabled");
                    ckEnableColumnTitles.Checked = false;
                }
                _currentPage.EnableColumnTitles = ckEnableColumnTitles.Checked;
                _currentPage.ApplyForAllColumnTitles = ckApplyForAllColumnTitles.Checked;
            }
        }

        private void PopulatePagesList()
        {
            comboBoxEditPages.Properties.Items.Clear();

            comboBoxEditPages.Properties.Items.AddRange(this.Library.Pages.Select(y => y.Name).ToArray());
            if (comboBoxEditPages.Properties.Items.Count > 0)
                comboBoxEditPages.SelectedIndex = 0;
            if (comboBoxEditPages.Properties.Items.Count > 1)
                pnPages.Visible = true;
            else
                pnPages.Visible = false;
        }

        private void PopulateWindowsList()
        {
            comboBoxEditWindows.Properties.Items.Clear();
            ClearWindowSettings();
            if (_currentPage != null)
            {
                comboBoxEditWindows.Properties.Items.AddRange(_currentPage.Folders.Select(x => x.Name).ToArray());
                comboBoxEditWindows.SelectedIndex = -1;
                if (comboBoxEditWindows.Properties.Items.Count > 0)
                    comboBoxEditWindows.SelectedIndex = 0;
            }
        }

        #endregion

        #region Base GUI
        private void Form_Load(object sender, EventArgs e)
        {
            grColumn1.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            grColumn2.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            grColumn3.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            ckApllyForAllWindows.Checked = this.Library.ApplyForAllWindows;
            PopulatePagesList();
            _stateChanges = false;
        }


        private void StateChanges_TextChanged(object sender, EventArgs e)
        {
            _stateChanges = true;
        }

        private void StateChanges_CheckedChanged(object sender, EventArgs e)
        {
            _stateChanges = true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveColumnSettings();
            SaveColumnTitles();
            if (_currentFolder == null)
            {
                if (this.Library.ApplyForAllWindows)
                {
                    BusinessClasses.LibraryFolder currentPageFolder = _currentPage.Folders.FirstOrDefault();
                    if (currentPageFolder != null)
                    {
                        foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                        {
                            folder.BackgroundHeaderColor = currentPageFolder.BackgroundHeaderColor;
                            folder.ForeHeaderColor = currentPageFolder.ForeHeaderColor;
                            folder.BackgroundWindowColor = currentPageFolder.BackgroundWindowColor;
                            folder.ForeWindowColor = currentPageFolder.ForeWindowColor;
                            folder.HeaderFont = currentPageFolder.HeaderFont;
                        }
                    }
                }
            }
            else
                SaveWindowSettings();
            this.Library.Save();
            AppManager.Instance.ShowInfo("Wall Bin Settings are saved");
            _changesDone = true;
            _stateChanges = false;
        }

        private void DWBSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_stateChanges)
            {
                if (AppManager.Instance.ShowQuestion("Before you EXIT, do you want to save the changes you made?") == DialogResult.Yes)
                {
                    btSave_Click(null, null);
                }
                else
                {
                    if (MessageBox.Show("You are about to lose your changes.\nThe changes will be LOST FOREVER & EVER & EVER!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        e.Cancel = true;
                }
            }
            if (_changesDone)
                this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Columns Tab GUI
        private void comboBoxEditPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessClasses.FolderCopier.PasteReady = false;
            SaveWindowSettings();
            SaveColumnSettings();
            SaveColumnTitles();
            _currentFolder = null;
            if (comboBoxEditPages.EditValue != null)
            {
                _currentPage = this.Library.Pages.Where(x => x.Name.Equals(comboBoxEditPages.EditValue.ToString())).FirstOrDefault();
                GetColumnSettings();
                GetColumnTitles();
                PopulateWindowsList();
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            grColumn1.EndEdit();
            grColumn1.ClearSelection();
            grColumn2.EndEdit();
            grColumn2.ClearSelection();
            grColumn3.EndEdit();
            grColumn3.ClearSelection();
        }

        private void grColumn_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo = ((DataGridView)sender).HitTest(e.X, e.Y);
            if (hitTestInfo.Type != DataGridViewHitTestType.Cell)
            {
                grColumn1.EndEdit();
                grColumn1.ClearSelection();
                grColumn2.EndEdit();
                grColumn2.ClearSelection();
                grColumn3.EndEdit();
                grColumn3.ClearSelection();
            }
        }

        private void grColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tmiCut.Enabled = false;
                tmiPaste.Enabled = BusinessClasses.FolderCopier.PasteReady;
                contextMenuStrip.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void grColumn_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
                if (((DataGridView)sender).SelectedRows.Count > 0)
                    tmiCut.Enabled = true;
                else
                    tmiCut.Enabled = false;
                tmiPaste.Enabled = BusinessClasses.FolderCopier.PasteReady;
                contextMenuStrip.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void grColumn1_SelectionChanged(object sender, EventArgs e)
        {
            if (_column1FirstSelect)
                _column1FirstSelect = false;
            else
                grColumn1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void grColumn2_SelectionChanged(object sender, EventArgs e)
        {
            if (_column2FirstSelect)
                _column2FirstSelect = false;
            else
                grColumn2.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void grColumn3_SelectionChanged(object sender, EventArgs e)
        {
            if (_column3FirstSelect)
                _column3FirstSelect = false;
            else
                grColumn3.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void tmiCut_Click(object sender, EventArgs e)
        {
            DataGridView workingGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    workingGrid = grColumn1;
                    break;
                case 1:
                    workingGrid = grColumn2;
                    break;
                case 2:
                    workingGrid = grColumn3;
                    break;
            }

            BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(workingGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
            if (selectedFolder != null)
            {
                BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                BusinessClasses.FolderCopier.Copy(workingGrid, workingGrid.SelectedRows[0].Index);
            }
        }

        private void tmiPaste_Click(object sender, EventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    BusinessClasses.FolderCopier.Paste(grColumn1);
                    break;
                case 1:
                    BusinessClasses.FolderCopier.Paste(grColumn2);
                    break;
                case 2:
                    BusinessClasses.FolderCopier.Paste(grColumn3);
                    break;
            }
            _stateChanges = true;
        }

        private void btAddWindow_Click(object sender, EventArgs e)
        {
            int rowNumber;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    rowNumber = grColumn1.Rows.Add("Window " + (grColumn1.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn1.CurrentCell = grColumn1.Rows[rowNumber].Cells[0];
                    grColumn1.Focus();
                    grColumn1.BeginEdit(true);
                    break;
                case 1:
                    rowNumber = grColumn2.Rows.Add("Window " + (grColumn2.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn2.CurrentCell = grColumn2.Rows[rowNumber].Cells[0];
                    grColumn2.Focus();
                    grColumn2.BeginEdit(true);
                    break;
                case 2:
                    rowNumber = grColumn3.Rows.Add("Window " + (grColumn3.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn3.CurrentCell = grColumn3.Rows[rowNumber].Cells[0];
                    grColumn3.Focus();
                    grColumn3.BeginEdit(true);
                    break;
            }
            _stateChanges = true;
        }

        private void btRemoveWindow_Click(object sender, EventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    foreach (DataGridViewRow row in grColumn1.SelectedRows)
                        grColumn1.Rows.Remove(row);
                    break;
                case 1:
                    foreach (DataGridViewRow row in grColumn2.SelectedRows)
                        grColumn2.Rows.Remove(row);
                    break;
                case 2:
                    foreach (DataGridViewRow row in grColumn3.SelectedRows)
                        grColumn3.Rows.Remove(row);
                    break;
            }
            _stateChanges = true;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            DataGridView columnGrid = null;
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    columnGrid = grColumn1;
                    break;
                case 1:
                    columnGrid = grColumn2;
                    break;
                case 2:
                    columnGrid = grColumn3;
                    break;
            }

            if (columnGrid.SelectedRows.Count > 0)
            {
                if (columnGrid.SelectedRows[0].Index > 0)
                {
                    string windowName = "";
                    Guid windowIdentifier = Guid.Empty;

                    columnGrid.SuspendLayout();

                    windowName = columnGrid.SelectedRows[0].Cells[0].Value.ToString();
                    windowIdentifier = (Guid)columnGrid.SelectedRows[0].Cells[1].Value;

                    columnGrid.SelectedRows[0].Cells[0].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[0].Value;
                    columnGrid.SelectedRows[0].Cells[1].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[1].Value;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[0].Value = windowName;
                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[1].Value = windowIdentifier;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Selected = true;

                    columnGrid.ResumeLayout();
                }
            }
            _stateChanges = true;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            DataGridView columnGrid = null;
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    columnGrid = grColumn1;
                    break;
                case 1:
                    columnGrid = grColumn2;
                    break;
                case 2:
                    columnGrid = grColumn3;
                    break;
            }

            if (columnGrid.SelectedRows.Count > 0)
            {
                if (columnGrid.SelectedRows[0].Index < columnGrid.Rows.Count - 1)
                {
                    string windowName = "";
                    Guid windowIdentifier = Guid.Empty;

                    columnGrid.SuspendLayout();

                    windowName = columnGrid.SelectedRows[0].Cells[0].Value.ToString();
                    windowIdentifier = (Guid)columnGrid.SelectedRows[0].Cells[1].Value;

                    columnGrid.SelectedRows[0].Cells[0].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[0].Value;
                    columnGrid.SelectedRows[0].Cells[1].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[1].Value;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[0].Value = windowName;
                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[1].Value = windowIdentifier;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Selected = true;

                    columnGrid.ResumeLayout();
                }
            }
            _stateChanges = true;
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            DataGridView sourceGrid = null;
            DataGridView destGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    sourceGrid = grColumn1;
                    destGrid = grColumn2;
                    break;
                case 1:
                    sourceGrid = grColumn2;
                    destGrid = grColumn3;
                    break;
            }
            if (sourceGrid.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(sourceGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
                if (selectedFolder != null)
                {
                    BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                    BusinessClasses.FolderCopier.Copy(sourceGrid, sourceGrid.SelectedRows[0].Index);
                    BusinessClasses.FolderCopier.Paste(destGrid);
                }
            }
            _stateChanges = true;
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            DataGridView sourceGrid = null;
            DataGridView destGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 1:
                    sourceGrid = grColumn2;
                    destGrid = grColumn1;
                    break;
                case 2:
                    sourceGrid = grColumn3;
                    destGrid = grColumn2;
                    break;
            }
            if (sourceGrid.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(sourceGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
                if (selectedFolder != null)
                {
                    BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                    BusinessClasses.FolderCopier.Copy(sourceGrid, sourceGrid.SelectedRows[0].Index);
                    BusinessClasses.FolderCopier.Paste(destGrid);
                }
            }
            _stateChanges = true;
        }

        private void xtraTabControlSettings_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.PrevPage == xtraTabPageColumns)
            {
                SaveColumnSettings();
                PopulateWindowsList();
            }
        }

        private void xtraTabControlWindows_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    buttonXRight.Enabled = true;
                    buttonXLeft.Enabled = false;
                    break;
                case 1:
                    buttonXRight.Enabled = true;
                    buttonXLeft.Enabled = true;
                    break;
                case 2:
                    buttonXRight.Enabled = false;
                    buttonXLeft.Enabled = true;
                    break;
            }
        }
        #endregion

        #region Windows Tab GUI
        private void cbWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentFolder != null)
                SaveWindowSettings();
            if (comboBoxEditWindows.SelectedIndex >= 0)
                _currentFolder = _currentPage.Folders[comboBoxEditWindows.SelectedIndex];
            else
                _currentFolder = null;
            GetWindowSettings();
            if (comboBoxEditWindows.SelectedIndex == 0)
            {
                ckApllyForAllWindows.Visible = true;
                colorEditHeaderBack.Enabled = true;
                colorEditHeaderFont.Enabled = true;
                colorEditWindowBack.Enabled = true;
                colorEditWindowFont.Enabled = true;
                buttonEditHeaderFont.Enabled = true;
            }
            else
            {
                ckApllyForAllWindows.Visible = false;
                colorEditHeaderBack.Enabled = !ckApllyForAllWindows.Checked;
                colorEditHeaderFont.Enabled = !ckApllyForAllWindows.Checked;
                colorEditWindowBack.Enabled = !ckApllyForAllWindows.Checked; ;
                colorEditWindowFont.Enabled = !ckApllyForAllWindows.Checked;
                buttonEditHeaderFont.Enabled = !ckApllyForAllWindows.Checked;
            }
        }

        private void FontEdit_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit fontEdit = sender as DevExpress.XtraEditors.ButtonEdit;
            if (fontEdit != null)
            {
                dlgFont.Font = fontEdit.Tag as Font;
                if (dlgFont.ShowDialog() == DialogResult.OK)
                {
                    fontEdit.Tag = dlgFont.Font;
                    fontEdit.EditValue = FontToString(dlgFont.Font);
                    _stateChanges = true;
                }
            }
        }

        private void FontEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FontEdit_Click(this, null);
        }
        #endregion

        #region Column Titles Tab GUI
        private void ckEnableColumnTitles_CheckedChanged(object sender, EventArgs e)
        {
            gbColumnTitle1.Enabled = ckEnableColumnTitles.Checked;
            gbColumnTitle2.Enabled = ckEnableColumnTitles.Checked;
            gbColumnTitle3.Enabled = ckEnableColumnTitles.Checked;
            ckApllyForAllWindows.Enabled = ckEnableColumnTitles.Checked;
            _stateChanges = true;
        }

        private void ckApplyForAllColumnTitles_CheckedChanged(object sender, EventArgs e)
        {
            colorEditColumn2Back.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn2Fore.Enabled = !ckApplyForAllColumnTitles.Checked;
            buttonEditColumn2HeaderFont.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn3Back.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn3Fore.Enabled = !ckApplyForAllColumnTitles.Checked;
            buttonEditColumn3HeaderFont.Enabled = !ckApplyForAllColumnTitles.Checked;
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Back.Color = colorEditColumn1Back.Color;
                colorEditColumn2Fore.Color = colorEditColumn2Fore.Color;
                buttonEditColumn2HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
                colorEditColumn3Back.Color = colorEditColumn1Back.Color;
                colorEditColumn3Fore.Color = colorEditColumn2Fore.Color;
                buttonEditColumn3HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
            }
            _stateChanges = true;
        }

        private void colorEditColumn1Back_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Back.Color = colorEditColumn1Back.Color;
                colorEditColumn3Back.Color = colorEditColumn1Back.Color;
            }
            _stateChanges = true;
        }

        private void colorEditColumn1Fore_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Fore.Color = colorEditColumn1Fore.Color;
                colorEditColumn3Fore.Color = colorEditColumn1Fore.Color;
            }
            _stateChanges = true;
        }

        private void buttonEditColumn1HeaderFont_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                buttonEditColumn2HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
                buttonEditColumn3HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
            }
            _stateChanges = true;
        }
        #endregion
    }
}
