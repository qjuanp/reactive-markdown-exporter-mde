using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System;
using System.IO;

namespace Mde.Net.Reactive.Extensions
{
    public static class WriteExtension
    {
        public static void WriteToStream(
            this IObservable<string> source, string path)
        {
            Observable.Using(() => File.Create(path),
                stream => Observable.Using(() => new StreamWriter(stream),
                writer => source.Select(entry => new { entry, writer })))
            .Subscribe(x => x.writer.Write(x.entry));

        }
    }
}