﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ColumnTitleControl : UserControl
    {
        public ColumnTitleControl()
        {
            InitializeComponent();
        }

        public int GetHeight()
        {
            Size labelSize = new Size(this.Width, Int32.MaxValue);
            return TextRenderer.MeasureText(laColumnTitle.Text, laColumnTitle.Font, labelSize, TextFormatFlags.WordBreak).Height + 10;
        }
    }
}
