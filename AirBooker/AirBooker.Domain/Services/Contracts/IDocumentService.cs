using PdfSharp.Pdf;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IDocumentService
    {
        byte[] CreatePDFDocument(string htmlData, string fileName);
        byte[] CreateCombinedPDFDocument(string[] htmlData, string fileName);
    }
}
