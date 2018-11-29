using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mde.Net.Reactive.Extensions;
using Mde.Net.Reactive.Models;

namespace Mde.Net.Reactive
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 3, 5, 8 };

            var observable = array.ToObservable();

            observable.Subscribe(i => Console.WriteLine(i));

            var parameters = new Dictionary<string, string>{
                {"key1","val1"},
                {"key2","val2"},
                {"key3","val3"},
            };

            parameters
                .ToQueryString()
                .Subscribe(s => Console.WriteLine($"Query : {s}"));

            // new MediumApi()
            //     .GetUri("@qjuanp", new Dictionary<string, string> { 
            //         { "format", "json" }
            //     })
            //     .Subscribe(uri => Console.WriteLine(uri));

            new Medium()
                .PostsUrlsFrom("qjuanp")
                .Subscribe(Console.WriteLine, ex => Console.WriteLine($"Ex:{ex.ToString()}\n{ex.InnerException?.ToString()}\n{ex.TargetSite}"));
            // .Subscribe(rs => Console.WriteLine($"Post : {rs}"), ex => Console.WriteLine($"Ex:{ex.ToString()}\n{ex.InnerException?.ToString()}\n{ex.TargetSite}"));

            var medium = new Medium();

            medium
                .PostsUrlsFrom("qjuanp")
                .SelectMany(postUrl => medium.GetPost(postUrl))
                .Select(p => p.ToMarkdown())
                .SelectMany(post =>
                    Observable.Using(
                        () => File.Create(Path.Combine(Directory.GetCurrentDirectory(), $"{post.Post.Slug}.md")),
                        stream => Observable.Using(
                                () => new StreamWriter(stream),
                                writer => post.Content.Select(content => (entry: content, writer: writer)))
                        )
                )
                .Subscribe(save => save.writer.Write(save.entry));
            Console.ReadLine();
        }

    }
}
