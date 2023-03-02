using AirBooker.Domain.Services.Contracts;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AirBooker.Domain.Services
{
    public class DocumentService : IDocumentService
    {
        public DocumentService() { }

        public byte[] CreatePDFDocument(string htmlData, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var document = PdfGenerator.GeneratePdf(htmlData, PageSize.A4);
                document.Info.Title = fileName;
                document.Save(stream, false);
                return stream.ToArray();
            }
        }

        public byte[] CreateCombinedPDFDocument(string[] htmlData, string fileName)
        {
            var document = new PdfDocument();

            foreach (var htmlCode in htmlData)
            {
                var importedDocument = ImportPDFDocument(PdfGenerator.GeneratePdf(htmlCode, PageSize.A4));
                foreach (var page in importedDocument.Pages)
                {
                    document.Pages.Add(page);
                }
            }

            using (MemoryStream stream = new MemoryStream())
            {
                document.Info.Title = fileName;
                document.Save(stream, false);
                return stream.ToArray();
            }
        }

        private PdfDocument ImportPDFDocument(PdfDocument document)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                stream.Position = 0;
                var result = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
                return result;
            }
        }
    }
}
