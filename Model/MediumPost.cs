using System.Collections.Generic;

namespace Mde.Net.Reactive.Models
{
    public class MediumPost
    {
        public string Title { get; internal set; }
        public string Slug { get; internal set; }
        public IEnumerable<MediumParagraph> Paragraphs { get; set; }
    }
}