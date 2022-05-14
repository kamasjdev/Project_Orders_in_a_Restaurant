using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Restaurant.UI.Async
{
    public class NaiveSynchronizationContext :
    SynchronizationContext
    {
        private readonly Action completed;
        private readonly Action<Exception> failed;

        public NaiveSynchronizationContext(
            Action completed,
            Action<Exception> failed)
        {
            this.completed = completed;
            this.failed = failed;
        }

        public override SynchronizationContext CreateCopy()
        {
            return new NaiveSynchronizationContext(
                this.completed,
                this.failed);
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (state is ExceptionDispatchInfo edi)
            {
                this.failed(edi.SourceException);
            }
            else
            {
                base.Post(d, state);
            }
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            if (state is ExceptionDispatchInfo edi)
            {
                this.failed(edi.SourceException);
            }
            else
            {
                base.Send(d, state);
            }
        }

        public override void OperationCompleted()
        {
            this.completed();
        }
    }
}
