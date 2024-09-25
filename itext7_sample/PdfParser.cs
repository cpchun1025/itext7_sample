using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using itext7_sample;

public class PdfParser
{
    public static List<Invoice> ParseInvoices(string text)
    {
        var invoices = new List<Invoice>();

        // Split the text into sections based on a separator or heuristic.
        // In this example, we assume each invoice starts with "Invoice No" or "Inovide No"
        var invoiceSections = Regex.Split(text, @"(?i)(?:Invoice|Inovide)\s+No", RegexOptions.Multiline);

        foreach (var section in invoiceSections)
        {
            if (string.IsNullOrWhiteSpace(section))
                continue;

            var invoice = new Invoice();

            // Extract Invoice No
            var invoiceNoMatch = Regex.Match(section, @"(?:Invoice|Inovide)\s*No[\s,:]*([\w\d]+)", RegexOptions.IgnoreCase);
            if (invoiceNoMatch.Success)
            {
                invoice.InvoiceNo = invoiceNoMatch.Groups[1].Value.Trim();
            }

            // Extract From
            var fromMatch = Regex.Match(section, @"From\s+([A-Z]{2,})", RegexOptions.IgnoreCase);
            if (fromMatch.Success)
            {
                invoice.From = fromMatch.Groups[1].Value.Trim();
            }

            // Extract To
            var toMatch = Regex.Match(section, @"To\s+([A-Z]{2,})", RegexOptions.IgnoreCase);
            if (toMatch.Success)
            {
                invoice.To = toMatch.Groups[1].Value.Trim();
            }

            // Extract Date
            var dateMatch = Regex.Match(section, @"Date\s+(\d{1,2}/\d{1,2}/\d{4})", RegexOptions.IgnoreCase);
            if (dateMatch.Success && DateTime.TryParse(dateMatch.Groups[1].Value, out DateTime date))
            {
                invoice.Date = date;
            }

            // Extract Price
            var priceMatch = Regex.Match(section, @"Price\s+(\d+\.?\d*)", RegexOptions.IgnoreCase);
            if (priceMatch.Success && decimal.TryParse(priceMatch.Groups[1].Value, out decimal price))
            {
                invoice.Price = price;
            }

            // Extract Status
            var statusMatch = Regex.Match(section, @"Status\s+(\w+)", RegexOptions.IgnoreCase);
            if (statusMatch.Success)
            {
                invoice.Status = statusMatch.Groups[1].Value.Trim();
            }

            invoices.Add(invoice);
        }

        return invoices;
    }
}