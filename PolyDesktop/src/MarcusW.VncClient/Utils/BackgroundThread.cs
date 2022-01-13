using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MarcusW.VncClient.Utils
{
    /// <summary>
    /// Base class for easier creation and clean cancellation of a background thread.
    /// </summary>
    public abstract class BackgroundThread : IBackgroundThread
    {
        private readonly Thread _thread;

        private bool _started;
        private readonly object _startLock = new object();

        private readonly CancellationTokenSource _stopCts = new CancellationTokenSource();
        private readonly TaskCompletionSource<object?> _completedTcs = new TaskCompletionSource<object?>();

        private volatile bool _disposed;

        /// <inheritdoc />
        public event EventHandler<BackgroundThreadFailedEventArgs>? Failed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundThread"/>.
        /// </summary>
        /// <param name="name">The thread name.</param>
        protected BackgroundThread(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _thread = new Thread(ThreadStart) {
                Name = name,
                IsBackground = true
            };
        }

        /// <summary>
        /// Starts the thread.
        /// </summary>
        /// <remarks>
        /// The thread can only be started once.
        /// </remarks>
        protected void Start()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(BackgroundThread));

            lock (_startLock)
            {
                if (_started)
                    throw new InvalidOperationException("Thread already started.");

                _thread.Start(_stopCts.Token);
                _started = true;
            }
        }

        /// <summary>
        /// Stops the thread and waits for completion.
        /// </summary>
        /// <remarks>
        /// It is safe to call this method multiple times.
        /// </remarks>
        protected Task StopAndWaitAsync()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(BackgroundThread));

            lock (_startLock)
            {
                if (!_started)
                    throw new InvalidOperationException("Thread has not been started.");
            }

            // Tell the thread to stop
            _stopCts.Cancel();

            // Wait for completion
            return _completedTcs.Task;
        }

        /// <summary>
        /// Executes the work that should happen in the background.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token that tells the method implementation when to complete.</param>
        protected abstract void ThreadWorker(CancellationToken cancellationToken);

        private void ThreadStart(object? parameter)
        {
            Debug.Assert(parameter != null, nameof(parameter) + " != null");
            var cancellationToken = (CancellationToken)parameter;

            try
            {
                // Do your work...
                ThreadWorker(cancellationToken);
            }
            catch (Exception exception) when (!(exception is OperationCanceledException || exception is ThreadAbortException))
            {
                Failed?.Invoke(this, new BackgroundThreadFailedEventArgs(exception));
            }
            finally
            {
                // Notify stop method that thread has completed
                _completedTcs.TrySetResult(null);
            }
        }

        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    // Ensure the thread is stopped
                    _stopCts.Cancel();
                    if (_thread.IsAlive)
                    {
                        // Block and wait for completion or hard-kill the thread after 1 second
                        if (!_thread.Join(TimeSpan.FromSeconds(1)))
                            _thread.Abort();
                    }
                }
                catch
                {
                    // Ignore
                }

                // Just to be sure...
                _completedTcs.TrySetResult(null);

                _stopCts.Dispose();
            }

            _disposed = true;
        }
    }
}
