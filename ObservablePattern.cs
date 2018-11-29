using System;

namespace Mde.Net.Reactive
{
    interface _IObservable<T> {
        IDisposable Subscribe(IObserver<T> observer);
    }

    interface _IObserver<T> {
        void OnNext(T value);

        void OnError(Exception ex);

        void OnComplete();
    }
}