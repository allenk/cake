﻿using System.Diagnostics;

namespace Cake.Core.IO
{
    internal sealed class ProcessRunner : IProcessRunner
    {
        public IProcess Start(ProcessStartInfo info)
        {
            var process = Process.Start(info);
            return process == null ? null : new ProcessWrapper(process);
        }
    }
}