using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

public class PdfTextExtractorService
{
    public static string ExtractText(string pdfPath)
    {
        StringBuilder text = new StringBuilder();

        using (PdfReader reader = new PdfReader(pdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(reader))
        {
            for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
            {
                var pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page));
                text.AppendLine(pageText);
            }
        }

        return text.ToString();
    }
}