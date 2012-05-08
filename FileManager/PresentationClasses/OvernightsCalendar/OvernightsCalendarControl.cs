using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.OvernightsCalendar
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OvernightsCalendarControl : UserControl
    {
        public OvernightsCalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
    }
}
