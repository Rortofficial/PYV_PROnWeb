using iText.Kernel.Pdf;

namespace Client.Lib.OtherFunction
{
    public class PDFMerge
    {
        public static byte[] MergePdfBytes(params byte[][] pdfBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                using (var pdf = new PdfDocument(writer))
                {
                    var merger = new iText.Kernel.Utils.PdfMerger(pdf);
                    foreach (var pdfByte in pdfBytes)
                    {
                        var sourcePdf = new PdfDocument(new PdfReader(new MemoryStream(pdfByte)));
                        merger.Merge(sourcePdf, 1, sourcePdf.GetNumberOfPages());
                        sourcePdf.Close();
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}
