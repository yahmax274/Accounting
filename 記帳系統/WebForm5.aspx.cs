using iText.Kernel.Pdf;
//using iText.Layout;
using iText.Layout.Element;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using iText.Kernel.Font;
using iText.Layout.Properties;
using iTextSharp.text;
using System.Diagnostics;
using System.Linq;

using System.Collections.Generic;
using iText.Kernel.Utils;
using Microsoft.Office.Interop.Word;
using iTextSharp.text.pdf;

namespace 記帳系統
{

    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static void ConvertWordToPdf(string inputPath, MemoryStream outputStream)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(inputPath, ReadOnly: true);

            // 建立一個新的暫時檔案來保存 PDF 內容
            string tempPdfPath = Path.GetTempFileName();

            try
            {
                // 將 Word 文件轉換為 PDF 並儲存到暫時檔案中
                wordDoc.ExportAsFixedFormat(tempPdfPath, WdExportFormat.wdExportFormatPDF);

                // 讀取 PDF 內容並寫入 MemoryStream
                using (FileStream fs = new FileStream(tempPdfPath, FileMode.Open, FileAccess.Read))
                {
                    fs.CopyTo(outputStream);
                }
            }
            finally
            {
                // 關閉 Word 文件並刪除暫時檔案
                wordDoc.Close(false);
                wordApp.Quit();

                File.Delete(tempPdfPath);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 指定資料夾路徑
            string folderPath = @"C:\Users\User\Desktop\報表";

            // 取得資料夾中所有 Word 文件
            string[] wordFiles = Directory.GetFiles(folderPath, "*.docx");

            // 轉換 Word 文件成 PDF
            List<MemoryStream> pdfStreams = new List<MemoryStream>();

            foreach (string wordFile in wordFiles)
            {
                MemoryStream pdfStream = new MemoryStream();
                ConvertWordToPdf(wordFile, pdfStream);
                pdfStreams.Add(pdfStream);
            }

            // 刪除合併前的 Word 檔案
            foreach (string wordFile in wordFiles)
            {
                File.Delete(wordFile);
            }

            // 合併 PDF 文件
            MemoryStream mergedPdfStream = new MemoryStream();
            MergePDFStreams(pdfStreams, mergedPdfStream);

            // 提供下載連結
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=MergedReport.pdf");
            Response.BinaryWrite(mergedPdfStream.ToArray());
            Response.End();

            // 刪除檔名為 "奉獻收據-" 開頭的檔案
            string[] filesToDelete = Directory.GetFiles(folderPath, "奉獻收據-*");
            foreach (string fileToDelete in filesToDelete)
            {
                File.Delete(fileToDelete);
            }
        }


        static void MergePDFStreams(List<MemoryStream> pdfStreams, MemoryStream outputStream)
        {
            using (var mergedPdf = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(outputStream)))
            {
                PdfMerger pdfMerger = new PdfMerger(mergedPdf);

                foreach (var pdfStream in pdfStreams)
                {
                    // 使用 iText.Kernel.Pdf.PdfReader 的不同建構子
                    using (var sourcePdf = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfReader(new MemoryStream(pdfStream.ToArray()))))
                    {
                        pdfMerger.Merge(sourcePdf, 1, sourcePdf.GetNumberOfPages());
                    }
                }
            }
        }

        /// <summary>
        /// 合併PDF
        /// </summary>
        /// <param name="fileList">被合併的文件集合</param>
        /// <param name="outMergeFile">合併文件路徑</param>
        /// <param name="iFlag">0:A4直印, 1:A4橫印</param>
        public void MergePDFFiles(string[] fileList, string outMergeFile, int iFlag)
        {
            iTextSharp.text.pdf.PdfReader reader;
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            if (iFlag != 0)
            {
                document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
            }

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));

            document.Open();

            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;

            for (int i = 0; i < fileList.Length; i++)
            {
                if (fileList[i] != null && fileList[i] != string.Empty)
                {
                    reader = new iTextSharp.text.pdf.PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;

                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        newPage = writer.GetImportedPage(reader, j);
                        cb.AddTemplate(newPage, 0, 0);
                    }
                }
            }

            document.Close();
        }

        /// <summary>
        /// 合併PDF 自動判斷方向
        /// </summary>
        /// <param name="fileList">被合併的文件集合</param>
        /// <param name="outMergeFile">合併文件路徑</param>
        public void MergePDFFiles(string[] fileList, string outMergeFile)
        {
            int f = 0;

            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(fileList[f]);
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1));

            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));

            document.Open();

            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            int rotation = 0;

            for (int i = 0; i < fileList.Length; i++)
            {
                if (fileList[i] != null)
                {
                    reader = new iTextSharp.text.pdf.PdfReader(fileList[i]);
                    int iPageNum = reader.NumberOfPages;

                    for (int j = 1; j <= iPageNum; j++)
                    {
                        document.NewPage();
                        newPage = writer.GetImportedPage(reader, j);
                        rotation = reader.GetPageRotation(j);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(newPage, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(newPage, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                }
            }

            document.Close();
        }
    }
}