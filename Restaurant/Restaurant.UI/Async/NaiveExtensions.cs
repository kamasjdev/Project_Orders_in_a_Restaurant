using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.UI.Async
{
    public static class NaiveExtension
    {
        public static Task NaiveRaiseAsync(
            this EventHandler @this,
            object sender,
            EventArgs eventArgs)
        {
            if (@this is null)
            {
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource<bool>();

            var delegates = @this.GetInvocationList();
            var count = delegates.Length;
            var exception = (Exception)null;

            foreach (var @delegate in @this.GetInvocationList())
            {
                var async = @delegate.Method
                    .GetCustomAttributes(typeof(AsyncStateMachineAttribute), false)
                    .Any();

                var completed = new Action(() =>
                {
                    if (Interlocked.Decrement(ref count) == 0)
                    {
                        if (exception is null)
                        {
                            tcs.SetResult(true);
                        }
                        else
                        {
                            tcs.SetException(exception);
                        }
                    }
                });
                var failed = new Action<Exception>(e =>
                {
                    Interlocked.CompareExchange(ref exception, e, null);
                });

                if (async)
                {
                    SynchronizationContext.SetSynchronizationContext(
                        new NaiveSynchronizationContext(completed, failed));
                }

                try
                {
                    @delegate.DynamicInvoke(sender, eventArgs);
                }
                catch (TargetInvocationException e) when (e.InnerException != null)
                {
                    // When exception occured inside Delegate.Invoke method all exceptions are wrapped in
                    // TargetInvocationException.

                    failed(e.InnerException);
                }
                catch (Exception e)
                {
                    failed(e);
                }

                if (!async)
                {
                    completed();
                }
            }

            return tcs.Task;
        }
    }
}
