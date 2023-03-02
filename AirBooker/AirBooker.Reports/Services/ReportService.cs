using PdfSharp.Pdf;
using PdfSharp;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using AirBooker.Reports.Services.Contracts;

namespace AirBooker.Reports.Services
{
    public class ReportService : IReportService
    {
        public PdfDocument CreatePDFDocument(Dictionary<int, string> pageData)
        {
            var outputDocument = new PdfDocument();

            foreach (var pageHTML in pageData)
            {
                var generatedPdf = PdfGenerator.GeneratePdf(pageHTML.Value, PageSize.A4);
                foreach (var page in generatedPdf.Pages)
                {
                    outputDocument.AddPage(page);
                }
            }

            return outputDocument;
        }
    }
}
