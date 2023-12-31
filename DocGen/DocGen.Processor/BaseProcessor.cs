﻿using System;
using System.IO;
using DocGen.Data;

namespace DocGen.Processor
{
    public abstract class BaseProcessor : ProgressCapable
    {
        public string SourceFilePath { get; set; }

        public string DestinationFilePath { get; protected set; }
        protected string UpdateDestinationFilePath()
        {
            var dirPath = Path.GetDirectoryName(SourceFilePath);
            var fileName = Path.GetFileNameWithoutExtension(SourceFilePath);
            var extension = Path.GetExtension(SourceFilePath);

            do {
                var suffix = ((_fileCount == 0) ? string.Empty : _fileCount.ToString());
                var newFileName = $"{fileName}.звіт{suffix}{extension}";
                DestinationFilePath = Path.Combine(dirPath, newFileName);
                _fileCount += 1;
            } while (File.Exists(DestinationFilePath));
            return DestinationFilePath;
        }
        private byte _fileCount = 0;

        public Datastore Datastore { get; set; }

        public abstract void OpenDocumnet(string filePath);

        public abstract void Process(bool reloadDatastore = false);
    }

    public abstract class ProgressCapable
    {
        public event Action<int, string> ProgressUpdatedEvent;

        protected void UpdateProgress(double percentage, string message)
        {
            ProgressPercentage = percentage;

            var progressUpdated = ProgressUpdatedEvent;
            if (progressUpdated != null)
            {
                var value = Convert.ToInt32(Math.Round(ProgressPercentage));
                progressUpdated(value, message);
            }
        }
        protected void IncrementProgressBy(double value)
        {
            UpdateProgress(ProgressPercentage + value, null);
        }
        public double ProgressPercentage { get; private set; } = 0F;
    }
}
