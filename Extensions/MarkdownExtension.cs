using System;
using System.Linq;
using System.Reactive.Linq;

namespace Mde.Net.Reactive.Models
{
    public static class MarkdownExtension
    {
        static readonly int[] availableTypes = new int[] {
            1, // paragraph
            3, // title
            13, // subtitle
            10, // bullet,
            9  //bullet
            };

        public static MarkdownPost ToMarkdown(this MediumPost post) =>
        new MarkdownPost{
            Post = post,
            Content = post
                .Paragraphs
                .ToObservable()
                .Where(paragraph => availableTypes.Contains(paragraph.Type))
                .Select(ToMarkdown)
        };
            

        private static string ToMarkdown(this MediumParagraph paragraph)
        {
            switch (paragraph.Type)
            {
                case 3:
                    return $"#{paragraph.Text}\n\n";
                case 12:
                    return $"##{paragraph.Text}\n\n";

                case 9:
                case 10:
                    return $"\t-{paragraph.Text}\n\n";

                case 1:
                default:
                    return $"{paragraph.Text}\n\n";
            }

        }
    }
}