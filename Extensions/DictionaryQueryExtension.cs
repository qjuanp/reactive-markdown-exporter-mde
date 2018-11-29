using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System;

namespace Mde.Net.Reactive.Extensions
{
    public static class DictionaryQueryExtension
    {

        public static IObservable<string> ToQueryString(this IDictionary<string, string> parameters) =>
            parameters
                .ToObservable()
                .Aggregate(string.Empty, (accumulated, current) => $"{accumulated}{current.Key}={current.Value}&")
                .FirstOrDefaultAsync();
    }
}