﻿using System;
using System.IO;

namespace logger
{
   public abstract class FileBase: IDisposable
    {
        public string NameFile;

        protected readonly object lockObj = new object();

        internal StreamWriter _sw;

        private bool _disposed;



        protected FileBase(string nameFile)
        {
            NameFile = nameFile;
        }

        public abstract bool WrtFile(string logString, string logLevel, string module, DateTime date);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sw.Dispose();
                }
                _disposed = true;
                NameFile = null;
            }
        }

        ~FileBase()
        {
            Dispose(false);
        }
    }
}
