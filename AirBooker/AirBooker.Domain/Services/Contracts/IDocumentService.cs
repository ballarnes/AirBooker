using PdfSharp.Pdf;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IDocumentService
    {
        PdfDocument CreatePDFDocument(string htmlData);
    }
}
