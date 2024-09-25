using System;
using System.Collections.Generic;
using itext7_sample;

class Program
{
    static void Main(string[] args)
    {
        string pdfPath = "path_to_your_pdf_file.pdf";

        // Step 1: Extract text from PDF
        string extractedText = PdfTextExtractorService.ExtractText(pdfPath);

        // Step 2: Parse the extracted text into data objects
        List<Invoice> invoices = PdfParser.ParseInvoices(extractedText);

        // Step 3: Output the parsed data
        foreach (var invoice in invoices)
        {
            Console.WriteLine($"Invoice No: {invoice.InvoiceNo}");
            Console.WriteLine($"From: {invoice.From}");
            Console.WriteLine($"To: {invoice.To}");
            Console.WriteLine($"Date: {invoice.Date.ToShortDateString()}");
            Console.WriteLine($"Price: {invoice.Price}");
            Console.WriteLine($"Status: {invoice.Status}");
            Console.WriteLine(new string('-', 40));
        }
    }
}