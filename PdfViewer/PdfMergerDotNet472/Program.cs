using log4net;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.IO;

namespace PdfMergerDotNet472
{
    class Program
    {
        //const string mergedPdfFileName = @"C:\Users\oabjo\Dropbox\OlesArkiv\merged.pdf";

        private static readonly ILog log = LogManager.GetLogger("log4net.config");
        
        static void Main(string[] args)
        {
            log.Info($"ENTER args.Length={args.Length}");
            log.Debug($" args='{string.Join(",",args)}'");

            if (args.Length != 1)
            {
                log.Error($"One argument (directory) expected");
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
            if(!directoryInfo.Exists)
            {
                log.Error($"Directory does not exist");
                return;
            }

            var pdfFiles = directoryInfo.GetFiles("*.pdf");
            if (pdfFiles.Length < 1)
            {
                log.Error($"Directory does not contain PDF files");
                return;
            }

            foreach (var pdfFile in pdfFiles)
            {
                log.Debug($"{pdfFile.FullName}");
            }

            Merge(pdfFiles, new FileInfo($"{directoryInfo.FullName}\\merged.pdf"));

            log.Info($"EXIT");
        }

        public static void Merge(FileInfo[] pdfFiles, FileInfo mergedFile)
        {
            log.Info($"Merge pdfFiles.Length=''{pdfFiles.Length}, mergedFile='{mergedFile}'");
            log.Debug(pdfFiles);

            PdfDocument mergedPdf = new PdfDocument();

            foreach(var pdfFile in pdfFiles)
            {
                PdfDocument pdfDocument = PdfReader.Open(pdfFile.FullName, PdfDocumentOpenMode.Import);
                foreach (var page in pdfDocument.Pages)
                {
                    mergedPdf.AddPage(page);
                }
                pdfDocument.Close();
            }


            mergedPdf.Save(mergedFile.FullName);
        }
    }
}
