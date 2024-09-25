using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

class Program
{
    static void Main(string[] args)
    {
        string pdfPath = "D:\\dev\\net\\itext7_sample\\Technical_spec_for_SD6_and_SD6A.pdf";

        // Open the PDF document
        using (PdfReader pdfReader = new PdfReader(pdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
        {
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                var strategy = new SimpleTextExtractionStrategy();
                string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);

                Console.WriteLine($"Page {page}:");
                Console.WriteLine(pageText);
                Console.WriteLine();
            }
        }
    }
}