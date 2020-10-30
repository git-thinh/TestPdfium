using iText.Kernel.Pdf;
using iText.Svg.Converter;
using System.IO;

namespace itext
{
    class Program
    {


        static void svg_01()
        {
            //string fileIV = @"C:\Pdf\file\intel.svg";
            //string fileIV = @"C:\Pdf\file\1.svg";
            //string fileIV = @"C:\Pdf\file\3.svg";
            string fileIV = @"C:\Pdf\file\ai_eps_1.svg";

            var writer = new PdfWriter(fileIV + ".pdf", new WriterProperties().SetCompressionLevel(0));
            var doc = new PdfDocument(writer);
            doc.AddNewPage();

            var streamFile = new MemoryStream(File.ReadAllBytes(fileIV));
            SvgConverter.DrawOnDocument(streamFile, doc, 1, 1, 1);

            //var image = SvgConverter.ConvertToImage(streamFile, doc);
            //image.SetFixedPosition(1, 1);
            //image.ScaleToFit(900, 900);

            doc.Close();
        }

        static void Main(string[] args)
        {
            svg_01();

        }
    }
}
