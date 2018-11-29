using System.Collections.Generic;

namespace Mde.Net.Reactive.Models {
    public class MediumParagraph {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Text { get; set; }
        public IEnumerable<MediumMarkup> Markups { get; set; }
    }
}