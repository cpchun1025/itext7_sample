using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

public class PdfPigExtractor
{
    public static string ExtractText(string pdfPath)
    {
        StringBuilder text = new StringBuilder();

        using (PdfDocument document = PdfDocument.Open(pdfPath))
        {
            foreach (Page page in document.GetPages())
            {
                foreach (var word in page.GetWords())
                {
                    text.Append(word.Text + " ");
                }
                text.AppendLine();
            }
        }

        return text.ToString();
    }
}