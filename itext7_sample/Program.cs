using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using itext7_sample;

class Program
{
    static void Main(string[] args)
    {
        string pdfPath = "D:\\dev\\net\\itext7_sample\\Technical_spec_for_SD6_and_SD6A.pdf";
        List<TableRow> tableData = new List<TableRow>();

        // Open the PDF document
        using (PdfReader pdfReader = new PdfReader(pdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
        {
            int lastPageNumber = pdfDoc.GetNumberOfPages();
            var strategy = new SimpleTextExtractionStrategy();
            string lastPageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(lastPageNumber), strategy);

            // Parse the table from the last page's text
            tableData = ParseTable(lastPageText);
        }

        // Output the parsed table data
        foreach (var row in tableData)
        {
            Console.WriteLine($"{row.Date} {row.Time} {row.Code1} {row.Value1} ...");
        }
    }

    // Method to parse the table from the extracted text
    static List<TableRow> ParseTable(string text)
    {
        List<TableRow> tableRows = new List<TableRow>();

        // Split the text by lines (each line represents a row in the table)
        var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            // Split each line by spaces (each column is separated by spaces)
            var columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Ensure the line has enough columns before creating a TableRow object
            if (columns.Length >= 18)
            {
                TableRow row = new TableRow
                {
                    Date = columns[0],
                    Time = columns[1],
                    Code1 = columns[2],
                    Value1 = columns[3],
                    Code2 = columns[4],
                    Value2 = columns[5],
                    Code3 = columns[6],
                    Value3 = columns[7],
                    Value4 = columns[8],
                    Value5 = columns[9],
                    LargeNumber = columns[10],
                    Identifier1 = columns[11],
                    Identifier2 = columns[12],
                    Date1 = columns[13],
                    Date2 = columns[14],
                    Attachment = columns[15],
                    Value6 = columns[16],
                    Value7 = columns[17],
                };

                tableRows.Add(row);
            }
        }

        return tableRows;
    }
}