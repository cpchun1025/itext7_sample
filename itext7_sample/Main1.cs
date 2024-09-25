using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

class Program
{
    static void Main(string[] args)
    {
        string pdfPath = "path_to_your_pdf_file.pdf";
        Dictionary<string, string> extractedData = new Dictionary<string, string>();

        // Extract text from the PDF
        using (PdfReader pdfReader = new PdfReader(pdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
        {
            var text = ExtractTextFromPDF(pdfDoc);

            // Process the extracted text to get key-value pairs
            extractedData = ParseTextToKeyValuePairs(text);
        }

        // Output the extracted key-value data
        foreach (var kvp in extractedData)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    // Method to extract text from the entire PDF
    static string ExtractTextFromPDF(PdfDocument pdfDoc)
    {
        StringBuilder text = new StringBuilder();
        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            var strategy = new SimpleTextExtractionStrategy();
            string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);
            text.AppendLine(pageText);
        }
        return text.ToString();
    }

    // Method to parse text and extract key-value pairs
    static Dictionary<string, string> ParseTextToKeyValuePairs(string text)
    {
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        // Define regex patterns or simple heuristics for known fields
        var patterns = new Dictionary<string, string>
        {
            { "Invoice No", @"Invoice No,?\s*(\d+)" },
            { "From", @"From\s+(\w+)" },
            { "To", @"To\s+(\w+)" },
            { "Date", @"Date\s+([\d/]+)" },
            { "Price", @"Price\s+(\d+)" },
            { "Status", @"Status\s*(\w+)" }
        };

        // Match each pattern and add the key-value pair to the dictionary
        foreach (var pattern in patterns)
        {
            var match = Regex.Match(text, pattern.Value, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                keyValuePairs[pattern.Key] = match.Groups[1].Value;
            }
        }

        return keyValuePairs;
    }

    void General_KeyPair()
    {
        // Match general key-value patterns
        MatchCollection matches = Regex.Matches(extractedText, @"(\w+)\s*[:\n]\s*(.*)");
        foreach (Match match in matches)
        {
            keyValuePairs[match.Groups[1].Value.Trim()] = match.Groups[2].Value.Trim();
        }
    }
}