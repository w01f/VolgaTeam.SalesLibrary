using System.Collections.Generic;
using System.Drawing;

namespace SalesDepot.CoreObjects
{
    public class SearchGroup
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }
        public List<string> Tags { get; set; }

        public SearchGroup()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Tags = new List<string>();
        }
    }
}
