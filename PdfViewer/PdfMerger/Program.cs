using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PdfMerger
{
    class Program
    {
        const string mergedPdf = @"C:\Users\oabjo\Dropbox\OlesArkiv\merged.pdf";

        const string filename1 = @"C:\Users\oabjo\Dropbox\OlesArkiv\1.pdf";
        const string filename2 = @"C:\Users\oabjo\Dropbox\OlesArkiv\2.pdf";

        static void Main(string[] args)
        {
            Test();
            PdfDocument pdfDocument0 = new PdfDocument();

            PdfDocument pdfDocument1 = PdfReader.Open(filename1, PdfDocumentOpenMode.Import);
            foreach(var page in pdfDocument1.Pages)
            {
                pdfDocument0.InsertPage(0, page);
            }
            pdfDocument1.Close();

            PdfDocument pdfDocument2 = PdfReader.Open(filename2, PdfDocumentOpenMode.Import);
            foreach (var page in pdfDocument2.Pages)
            {
                pdfDocument0.InsertPage(1, page);
            }
            pdfDocument2.Close();

            pdfDocument0.Save(mergedPdf);

            if (args.Length != 1) return;

            DirectoryInfo dir = new DirectoryInfo(args[0]);

            foreach(var file in dir.GetFiles())
            {
                if (file.Extension != ".pdf") continue;
                //PdfDocument p = new PdfDocument(file.FullName);
                //p.Close();
                try
                {
                    // Open the file

                    PdfDocument inputDocument = PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import);
                    //var fs = File.OpenRead(@"C:\\temp\\test.pdf");
                    //XPdfForm form = XPdfForm.FromStream(fs);
                    //XPdfForm form2 = XPdfForm.FromFile(file.FullName);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            Console.ReadLine();
        }

        static void Test()
        {
            // Open the input files
            PdfDocument inputDocument1 = PdfReader.Open(filename1, PdfDocumentOpenMode.Import);
            PdfDocument inputDocument2 = PdfReader.Open(filename2, PdfDocumentOpenMode.Import);

            // Create the output document
            PdfDocument outputDocument = new PdfDocument();

            // Show consecutive pages facing. Requires Acrobat 5 or higher.
            outputDocument.PageLayout = PdfPageLayout.TwoColumnLeft;

            XFont font = new XFont("Verdana", 10, XFontStyle.Bold);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            format.LineAlignment = XLineAlignment.Far;
            XGraphics gfx;
            XRect box;
            int count = Math.Max(inputDocument1.PageCount, inputDocument2.PageCount);
            for (int idx = 0; idx < count; idx++)
            {
                // Get page from 1st document
                PdfPage page1 = inputDocument1.PageCount > idx ?
                  inputDocument1.Pages[idx] : new PdfPage();

                // Get page from 2nd document
                PdfPage page2 = inputDocument2.PageCount > idx ?
                  inputDocument2.Pages[idx] : new PdfPage();

                // Add both pages to the output document
                page1 = outputDocument.AddPage(page1);
                page2 = outputDocument.AddPage(page2);

                // Write document file name and page number on each page
                gfx = XGraphics.FromPdfPage(page1);
                box = page1.MediaBox.ToXRect();
                box.Inflate(0, -10);
                gfx.DrawString(String.Format("{0} • {1}", filename1, idx + 1),
                  font, XBrushes.Red, box, format);

                gfx = XGraphics.FromPdfPage(page2);
                box = page2.MediaBox.ToXRect();
                box.Inflate(0, -10);
                gfx.DrawString(String.Format("{0} • {1}", filename2, idx + 1),
                  font, XBrushes.Red, box, format);
            }

            // Save the document...
            //const string filename = "CompareDocument1_tempfile.pdf";
            outputDocument.Save(mergedPdf);
        }
    }
}
