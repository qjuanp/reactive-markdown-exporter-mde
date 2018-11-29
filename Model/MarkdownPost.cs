using System;

namespace Mde.Net.Reactive.Models
{
    public class MarkdownPost
    {
        public MediumPost Post { get; set; }
        public IObservable<string> Content { get; set; }
    }
}