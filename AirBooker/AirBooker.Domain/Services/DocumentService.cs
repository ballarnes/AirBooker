using AirBooker.Domain.Services.Contracts;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AirBooker.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        public DocumentService() { }

        public PdfDocument CreatePDFDocument(string htmlData)
        {
            var document = PdfGenerator.GeneratePdf(htmlData, PageSize.A4);
            return document;
        }
    }
}
