using PdfSharp.Pdf;

namespace AirBooker.Reports.Services.Contracts
{
    public interface IReportService
    {
        PdfDocument CreatePDFDocument(Dictionary<int, string> pageData);
    }
}
