﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.Azure.WebJobs.Host.Indexers;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Loggers;

namespace Microsoft.Azure.WebJobs.Host.Executors
{
    internal sealed class JobHostContext : IDisposable
    {
        private readonly IFunctionIndexLookup _functionLookup;
        private readonly IFunctionExecutor _executor;
        private readonly IListener _listener;
        private readonly TraceWriter _trace;
        private readonly IAsyncCollector<FunctionInstanceLogEntry> _fastLogger; // optional

        private bool _disposed;

        public JobHostContext(IFunctionIndexLookup functionLookup,
            IFunctionExecutor executor,
            IListener listener,
            TraceWriter trace,
            IAsyncCollector<FunctionInstanceLogEntry> fastLogger = null)
        {
            _functionLookup = functionLookup;
            _executor = executor;
            _listener = listener;
            _trace = trace;
            _fastLogger = fastLogger;
        }

        public TraceWriter Trace
        {
            get
            {
                ThrowIfDisposed();
                return _trace;
            }
        }

        public IFunctionIndexLookup FunctionLookup
        {
            get
            {
                ThrowIfDisposed();
                return _functionLookup;
            }
        }

        public IFunctionExecutor Executor
        {
            get
            {
                ThrowIfDisposed();
                return _executor;
            }
        }

        public IListener Listener
        {
            get
            {
                ThrowIfDisposed();
                return _listener;
            }
        }

        public IAsyncCollector<FunctionInstanceLogEntry> FastLogger
        {
            get
            {
                ThrowIfDisposed();
                return _fastLogger;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _listener.Dispose();

                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(null);
            }
        }
    }
}
