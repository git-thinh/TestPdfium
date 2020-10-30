using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace test_lib
{
    class Program
    {

        static void img_01()
        {
            string pdfFile = @"C:\Pdf\file\2.pdf";

            //string pdfFile = @"C:\Pdf\file\Grid_Sort.ai.pdf";
            PdfReader pdfReader = new PdfReader(pdfFile);
            for (int pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
            {
                PdfReader pdf = new PdfReader(pdfFile);
                PdfDictionary pg = pdf.GetPageN(pageNumber);
                PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
                foreach (PdfName name in xobj.Keys)
                {
                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                        string width = tg.Get(PdfName.WIDTH).ToString();
                        string height = tg.Get(PdfName.HEIGHT).ToString();
                        //ImageRenderInfo imgRI = ImageRenderInfo.CreateForXObject(new Matrix(float.Parse(width), float.Parse(height)), (PRIndirectReference)obj, tg);
                        //RenderImage(imgRI);
                    }
                }
            }
        }

        static void img_02()
        {
            //string file = @"C:\Pdf\file\Grid_Sort.ai.pdf";
            //string file = @"C:\Pdf\file\1.jpg.pdf";
            string file = @"C:\Pdf\file\2.pdf";

            FileStream fs = File.OpenRead(file);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);

            List<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();

            iTextSharp.text.pdf.RandomAccessFileOrArray RAFObj = null;
            iTextSharp.text.pdf.PdfReader PDFReaderObj = null;
            iTextSharp.text.pdf.PdfObject PDFObj = null;
            iTextSharp.text.pdf.PdfStream PDFStremObj = null;

            try
            {
                RAFObj = new iTextSharp.text.pdf.RandomAccessFileOrArray(data);
                PDFReaderObj = new iTextSharp.text.pdf.PdfReader(RAFObj, null);

                for (int i = 0; i <= PDFReaderObj.XrefSize - 1; i++)
                {
                    PDFObj = PDFReaderObj.GetPdfObject(i);

                    if ((PDFObj != null) && PDFObj.IsStream())
                    {
                        PDFStremObj = (iTextSharp.text.pdf.PdfStream)PDFObj;
                        iTextSharp.text.pdf.PdfObject subtype = PDFStremObj.Get(iTextSharp.text.pdf.PdfName.SUBTYPE);

                        if ((subtype != null) && subtype.ToString() == iTextSharp.text.pdf.PdfName.IMAGE.ToString())
                        {
                            byte[] bytes = iTextSharp.text.pdf.PdfReader.GetStreamBytesRaw((iTextSharp.text.pdf.PRStream)PDFStremObj);

                            if ((bytes != null))
                            {
                                try
                                { 
                                    File.WriteAllBytes(file + i + ".png", bytes);

                                    //System.IO.MemoryStream MS = new System.IO.MemoryStream(bytes);

                                    //MS.Position = 0;
                                    //System.Drawing.Image ImgPDF = System.Drawing.Image.FromStream(MS);
                                    ////ImgList.Add(ImgPDF);
                                    //ImgPDF.Save(file + ".output.png", System.Drawing.Imaging.ImageFormat.Png);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                }
                PDFReaderObj.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        static void img_03()
        { 
            string sourcePdf = @"C:\Pdf\file\2.pdf";
            //string sourcePdf = @"C:\Pdf\file\Grid_Sort.ai.pdf"; 

            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);

            try
            {
                for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
                {
                    PdfDictionary pg = pdf.GetPageN(pageNumber);
                    PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));

                    PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
                    if (xobj != null)
                    {
                        int k = 0;
                        foreach (PdfName name in xobj.Keys)
                        {
                            PdfObject obj = xobj.Get(name);
                            if (obj.IsIndirect())
                            {
                                PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                                PdfName type = (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));
                                if (PdfName.IMAGE.Equals(type))
                                {
                                    int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                                    PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                                    PdfStream pdfStrem = (PdfStream)pdfObj;
                                    byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                                    k++;
                                    File.WriteAllBytes(sourcePdf + k + ".png", bytes);
                                    //if ((bytes != null))
                                    //{
                                    //    using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                                    //    {
                                    //        memStream.Position = 0;
                                    //        System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                                    //        img.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);

                                    //        //// must save the file while stream is open.
                                    //        //if (!Directory.Exists(outputPath))
                                    //        //    Directory.CreateDirectory(outputPath);

                                    //        //string path = Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
                                    //        //System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                                    //        //parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                    //        //System.Drawing.Imaging.ImageCodecInfo jpegEncoder = Utilities.GetImageEncoder("JPEG");
                                    //        //img.Save(path, jpegEncoder, parms);
                                    //        break;
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                }
            }

            catch
            {
                throw;
            }
            finally
            {
                pdf.Close();
                raf.Close();
            }
        }


        static void img_05()
        {
            //string sourcePdf = @"C:\Pdf\file\2.pdf"; 
            //string sourcePdf = @"C:\Pdf\file\Grid_Sort.ai.pdf";
            //string sourcePdf = @"C:\Pdf\file\ai3.pdf";
            string sourcePdf = @"C:\Pdf\file\out.pdf";

            // existing pdf path
            PdfReader reader = new PdfReader(sourcePdf);
            PRStream pst;
            PdfImageObject pio;
            PdfObject po;
            // number of objects in pdf document
            int n = reader.XrefSize;
            //FileStream fs = null;
            // set image file location
            //String path = "E:/";
            List<string> list = new List<string>() { };
            for (int i = 0; i < n; i++)
            {
                // get the object at the index i in the objects collection
                po = reader.GetPdfObject(i);
                // object not found so continue
                if (po == null || !po.IsStream())
                    continue;
                //cast object to stream
                pst = (PRStream)po;
                //get the object type
                PdfObject type = pst.Get(PdfName.SUBTYPE);
                if (type != null)list.Add(type.ToString());
                //check if the object is the image type object
                if (type != null && type.ToString().Equals(PdfName.XML.ToString()))
                {
                     
                    byte[] b;
                    try
                    {
                        b = PdfReader.GetStreamBytes(pst);
                    }
                    catch 
                    {
                        b = PdfReader.GetStreamBytesRaw(pst);
                    }

                    string xml = Encoding.UTF8.GetString(b);
                    list.Add(xml);

                    //get the image
                    //pio = new PdfImageObject(pst);
                    //// fs = new FileStream(path + "image" + i + ".jpg", FileMode.Create);
                    ////read bytes of image in to an array
                    //byte[] imgdata = pio.GetImageAsBytes();
                    //try
                    //{
                    //    Stream stream = new MemoryStream(imgdata);
                    //    FileStream fs = stream as FileStream;
                    //    if (fs != null) Console.WriteLine(fs.Name);
                    //}
                    //catch
                    //{
                    //}
                    //pio.GetDrawingImage().Save(sourcePdf + i + ".output.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }


        }

        private static PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res =
                (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));


            PdfDictionary xobj =
              (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {

                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                        PdfName type =
                          (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            return obj;
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        }

                    }
                }
            }

            return null;

        }

        static void pdf_03()
        {
            //string file = @"C:\Pdf\file\1.jpg.pdf";
            //string imagePath = @"C:\Pdf\file\1.jpg";

            string file = @"C:\Pdf\file\1.ai.pdf";
            string imagePath = @"C:\Pdf\file\1.ai";

            FileStream fs = new FileStream(file, FileMode.Create);
            Document pdfdoc = new Document();
            PdfWriter.GetInstance(pdfdoc, fs);
            pdfdoc.Open();
            Paragraph para = new Paragraph("Hello My Name Is Gajendra");
            para.Alignment = Element.ALIGN_CENTER;
            pdfdoc.Add(para);

            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
            image.Alignment = Element.ALIGN_CENTER;
            image.ScaleToFit(180f, 250f);

            pdfdoc.Add(image);
            pdfdoc.Close();
        }

        static void pdf_04()
        {
            //string file = @"C:\Pdf\file\1.jpg.pdf";
            //string imagePath = @"C:\Pdf\file\1.jpg";

            string file = @"C:\Pdf\file\2.pdf";
            string imagePath = @"C:\Pdf\file\2.png";

            Document document = new Document();
            using (Stream outputPdfStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Stream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                PdfWriter.GetInstance(document, outputPdfStream);

                Image image = Image.GetInstance(imageStream);
                image.Alignment = Element.ALIGN_CENTER;
                image.ScaleToFit(180f, 250f);

                document.Open();
                document.Add(image);
                document.Close();
            }
        }


        static void Main(string[] args)
        {
            //img_01();
            //img_02();
            //img_03();
            img_05();

            //pdf_03();
            //pdf_04();

            Console.ReadLine();
        }
    }
}
