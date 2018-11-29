using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System;

namespace Mde.Net.Reactive.Extensions
{
    public static class DebugExtension
    {
        public static IObservable<TSource> PrintAndContinue<TSource>(this IObservable<TSource> source, string label) =>
            Observable.Create<TSource>( observer =>
                source.Subscribe( el => {
                    Console.WriteLine($"{label}: {el.ToString()}");
                    observer.OnNext(el);
                }, observer.OnError, observer.OnCompleted)
            );
    }
}