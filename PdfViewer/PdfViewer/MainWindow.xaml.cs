using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PdfViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string pdfFile1 = @"C:\Users\oabjo\Downloads\Hovedprospekt Middelthunet.pdf";
        private const string pdfFile2 = @"C:\Users\oabjo\Downloads\Brøndums.pdf";
        private const string pdfFile = @"C:\Users\oabjo\Dropbox\OlesArkiv\000 Innboks\Epson_10092020163806.pdf";
        public MainWindow()
        {
            InitializeComponent();

            //test();

            var pdfReader = PdfReader.Open(pdfFile);
            PdfSharp.Pdf.PdfDocument doc = new PdfSharp.Pdf.PdfDocument(pdfFile);
            //PdfSharp.Pdf.Advanced.PdfImage img = new PdfSharp.Pdf.Advanced.PdfImage(doc, img);
            //PdfView.Source = new Uri(pdfFile);
        }

        public void test()
        {

            PdfDocument document = PdfReader.Open(pdfFile);

            // Iterate pages
            foreach (PdfPage page in document.Pages)
            {
                // Get resources dictionary
                PdfDictionary resources = page.Elements.GetDictionary("/Resources");
                if (resources != null)
                {
                    // Get external objects dictionary
                    PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");






                    if (xObjects != null)
                    {
                        //ICollection<pdfitem> items = xObjects.Elements;
                        // Iterate references to external objects
                        foreach (PdfItem item in xObjects.Elements.Values)
                        {
                            PdfReference reference = item as PdfReference;
                            if (reference != null)
                            {
                                PdfDictionary xObject = reference.Value as PdfDictionary;
                                // Is external object an image?
                                if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                                {
                                    var count = 0;
                                    // Fortunately JPEG has native support in PDF and exporting an image is just writing the stream to a file.
                                    byte[] stream = xObjects.Stream.Value;
                                    FileStream fs = new FileStream(String.Format("Image{0}.jpeg", count++), FileMode.Create, FileAccess.Write);
                                    BinaryWriter bw = new BinaryWriter(fs);
                                    bw.Write(stream);
                                    bw.Close();

                                    //ExportImage(xObject, ref imageCount);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
