using System;
using System.IO;
using DocGen.Data;

namespace DocGen.Processor
{
    public abstract class BaseProcessor
    {
        public event Action<int, string> ProgressUpdatedEvent;
        protected void ProgressUpdated(double percentage, string message = null)
        {
            var progressUpdated = ProgressUpdatedEvent;
            if (progressUpdated != null)
            {
                progressUpdated(Convert.ToInt32(Math.Round(percentage)), message);
            }
        }

        public string SourceFilePath { get; set; }

        public string DestinationFilePath { get; protected set; }
        protected string UpdateDestinationFilePath()
        {
            var dirPath = Path.GetDirectoryName(SourceFilePath);
            var fileName = Path.GetFileNameWithoutExtension(SourceFilePath);
            var extension = Path.GetExtension(SourceFilePath);

            string fullFileName = string.Empty;
            do {
                if (fileCount < 1)
                {
                    fullFileName = $"{fileName}.звіт{extension}";
                }
                else if (fileCount == 1)
                {
                    fullFileName = $"{fileName}.звіт1{extension}";
                }
                else
                {
                    fullFileName = fullFileName.Replace($"звіт{fileCount-1}", $"звіт{fileCount}");
                }
                DestinationFilePath = Path.Combine(dirPath, fullFileName);
                fileCount += 1;
            } while (File.Exists(DestinationFilePath));
            return DestinationFilePath;
        }
        private byte fileCount = 0;

        public Datastore Datastore { get; set; }

        public abstract void OpenDocumnet(string filePath);
    }
}
