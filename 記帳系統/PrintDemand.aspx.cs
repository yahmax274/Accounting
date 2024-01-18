using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Bibliography;
using System.Security.Policy;
//using Aspose.Words;
//using Aspose.Words.Drawing;
//using Document = Aspose.Words.Document;

using iText.Kernel.Utils;
using Microsoft.Office.Interop.Word;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace 記帳系統
{

    public partial class PrintDemand : System.Web.UI.Page
    {
        // 定義結果資料夾的變數，請替換為實際路徑
        private static readonly string ResultFolder = @"C:\Users\User\Desktop\報表";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SearchResults"] != null)
            {
                System.Data.DataTable dt = (System.Data.DataTable)Session["SearchResults"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Write("查詢結果數量：" + dt.Rows.Count + "<br>");
            }
        }

        protected void IndexButton_Click(object sender, EventArgs e)
        {

        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Session["MoveBack"] = "MoveBack";
            Response.Redirect("IncomeReceiptIndex.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            int targetId = 1; // 要查詢的ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Basic_Information WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                // 使用參數化查詢以避免 SQL 注入攻擊
                command.Parameters.AddWithValue("@Id", targetId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (Session["PrintSerialNumber"] != null)
                    {
                        DateTime today = DateTime.Today;
                        string todayString = DateTime.Today.ToString("yyyy-MM-dd");
                        // 讀取基本資料
                        string BFGroup = reader["Group"].ToString();
                        string BFAddress = reader["Address"].ToString();
                        string BFRegister = reader["Register"].ToString();
                        string BFPrincipal = reader["Principal"].ToString();
                        string BFPhone = reader["Phone"].ToString();
                        string BFAdministration = reader["Administration"].ToString();
                        string BFManager = reader["Manager"].ToString();

                        reader.Close();

                        // 遍歷 GridView 的每一行
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            string Contribution = row.Cells[1].Text;
                            string Name = row.Cells[2].Text;
                            string Date = row.Cells[4].Text;
                            string Summons = row.Cells[7].Text;
                            string Amount = row.Cells[8].Text;
                            string Groups = row.Cells[11].Text;

                            // 呼叫 Word 範本渲染方法
                            WordTmplRendering(BFGroup, BFAddress, BFRegister, BFPrincipal, BFPhone, BFAdministration, BFManager, Contribution, Name, Date, Groups, Summons, Amount, todayString);
                        }
                        //轉PDF合併列印功能
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
                }
                if (Session["PrintDate"] != null)
                {
                    System.Data.DataTable dt = (System.Data.DataTable)Session["SearchResults"];

                    dt.Columns.Remove("憑證號碼");
                    dt.Columns.Remove("收據");
                    dt.Columns.Remove("部門");
                    dt.Columns.Remove("傳票號碼");

                    // 調整列順序
                    System.Data.DataTable reorderedDt = new System.Data.DataTable();
                    reorderedDt.Columns.Add("代號");
                    reorderedDt.Columns.Add("姓名");
                    reorderedDt.Columns.Add("日期");
                    reorderedDt.Columns.Add("奉獻項目");
                    reorderedDt.Columns.Add("金額");
                    reorderedDt.Columns.Add("摘要");

                    // 加總金額
                    decimal totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["金額"]);
                        DataRow newRow = reorderedDt.NewRow();
                        newRow["代號"] = row["代號"];
                        newRow["姓名"] = row["姓名"];
                        newRow["日期"] = row["日期"];
                        newRow["奉獻項目"] = row["奉獻項目"];
                        newRow["金額"] = row["金額"];
                        newRow["摘要"] = row["摘要"];
                        reorderedDt.Rows.Add(newRow);
                    }

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 10f);
                    MemoryStream ms = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                    // 設置字體
                    BaseFont baseFont = BaseFont.CreateFont(@"C:\Users\User\Desktop\資料庫\字體\標楷體.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 12);

                    doc.Open();
                    string BFGroup = reader["Group"].ToString();
                    // 添加標題
                    PdfPTable titleTable = new PdfPTable(1);
                    PdfPCell titleCell = new PdfPCell(new Phrase(BFGroup, titleFont));
                    titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    titleCell.Border = PdfPCell.NO_BORDER;
                    titleTable.AddCell(titleCell);

                    titleCell = new PdfPCell(new Phrase("奉獻收入徵信表", titleFont));
                    titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    titleCell.Border = PdfPCell.NO_BORDER;
                    titleTable.AddCell(titleCell);

                    // 添加空行
                    titleCell = new PdfPCell(new Phrase(" "));
                    titleCell.Border = PdfPCell.NO_BORDER;
                    titleTable.AddCell(titleCell);

                    // 添加列印日期
                    titleCell = new PdfPCell(new Phrase("列印日期: " + DateTime.Now.ToString("西元yyyy年MM月dd日"), normalFont));
                    titleCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    titleCell.Border = PdfPCell.NO_BORDER;
                    titleTable.AddCell(titleCell);

                    // 添加空行
                    titleCell = new PdfPCell(new Phrase(" "));
                    titleCell.Border = PdfPCell.NO_BORDER;
                    titleTable.AddCell(titleCell);

                    // 添加列印日期區間和總金額到新的表格
                    PdfPTable totalTable = new PdfPTable(1); // 新增一個表格，有1列
                    PdfPCell totalCell = new PdfPCell(new Phrase("列印日期區間: " + Session["StartDay"] + " ~ " + Session["EndDay"]
                        + "        總金額: " + totalAmount.ToString(), normalFont));
                    totalCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    totalCell.Border = PdfPCell.NO_BORDER;
                    totalTable.AddCell(totalCell);

                    // 新增一行空白行
                    totalCell = new PdfPCell(new Phrase(" "));
                    totalCell.Colspan = 1; // 單獨佔據一列
                    totalCell.Border = PdfPCell.NO_BORDER;
                    totalTable.AddCell(totalCell);

                    // 將標題信息和總金額表格添加到文檔
                    doc.Add(titleTable);
                    doc.Add(totalTable);

                    // 生成表格
                    PdfPTable table = new PdfPTable(reorderedDt.Columns.Count);
                    PdfPCell cell;

                    // 設置每個欄的寬度百分比
                    float[] columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f };
                    table.SetWidths(columnWidths);
                    table.WidthPercentage = 100; // 設置表格寬度為100%

                    // 添加列標題
                    foreach (DataColumn c in reorderedDt.Columns)
                    {
                        cell = new PdfPCell(new Phrase(c.ColumnName, normalFont));
                        cell.Border = PdfPCell.BOTTOM_BORDER; // 只在列標題下方加底框
                        table.AddCell(cell);
                    }

                    // 添加數據行
                    foreach (DataRow row in reorderedDt.Rows)
                    {
                        for (int i = 0; i < reorderedDt.Columns.Count; i++)
                        {
                            cell = new PdfPCell(new Phrase(row[i].ToString(), normalFont));
                            cell.Border = PdfPCell.NO_BORDER; // 其他行去掉框線
                            table.AddCell(cell);
                        }
                    }

                    // 將表格添加到文檔
                    doc.Add(table);

                    // 關閉文檔
                    doc.Close();

                    // 將生成的 PDF 保存到文件或提供下載
                    byte[] pdfData = ms.ToArray();
                    string filePath = Server.MapPath("~/App_Data/SearchResults.pdf");
                    File.WriteAllBytes(filePath, pdfData);

                    // 提供下載鏈接
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SearchResults.pdf");
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(pdfData);
                    Response.End();
                }
            }
        }

        static void WordTmplRendering(string BFGroup, string BFAddress, string BFRegister, string BFPrincipal, string BFPhone, string BFAdministration, string BFManager
            , string Contribution, string Name, string Date, string Groups, string Summons, string Amount, string todayString)
        {
            var templatePath = @"C:\Users\User\Desktop\資料庫\奉獻收據模板.docx";
            var docxBytes = Examples.WordRender.GenerateDocx(File.ReadAllBytes(templatePath),
                new Dictionary<string, string>()
                {
                    ["Group"] = BFGroup,
                    ["Address"] = BFAddress,
                    ["Register"] = BFRegister,
                    ["Principal"] = BFPrincipal,
                    ["Phone"] = BFPhone,
                    ["Administration"] = BFAdministration,
                    ["Manager"] = BFManager,
                    ["Contribution"] = Contribution,
                    ["Name"] = Name,
                    ["Date"] = Date,
                    ["Groups"] = Groups,
                    ["Summons"] = Summons,
                    ["Amount"] = Amount,
                    ["Today"] = todayString
                });

            // 使用不同的檔案名稱
            string resultFileName = $"奉獻收據-{DateTime.Now:yyyyMMddHHmmssfff}.docx";
            File.WriteAllBytes(
                Path.Combine(ResultFolder, resultFileName),
                docxBytes);
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
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("BasicInformation.aspx");
        }
    }
    namespace Examples
    {
        public static class WordRender
        {
            static void ReplaceParserTag(this OpenXmlElement elem, Dictionary<string, string> data)
            {
                var pool = new List<DocumentFormat.OpenXml.Wordprocessing.Run>();
                var matchText = string.Empty;
                var hiliteRuns = elem.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>() //找出鮮明提示
                    .Where(o => o.RunProperties?.Elements<Highlight>().Any() ?? false).ToList();

                foreach (var run in hiliteRuns)
                {
                    var t = run.InnerText;
                    if (t.StartsWith("["))
                    {
                        pool = new List<DocumentFormat.OpenXml.Wordprocessing.Run> { run };
                        matchText = t;
                    }
                    else
                    {
                        matchText += t;
                        pool.Add(run);
                    }
                    if (t.EndsWith("]"))
                    {
                        var m = Regex.Match(matchText, @"\[\$(?<n>\w+)\$\]");
                        if (m.Success && data.ContainsKey(m.Groups["n"].Value))
                        {
                            var firstRun = pool.First();
                            firstRun.RemoveAllChildren<Text>();
                            firstRun.RunProperties.RemoveAllChildren<Highlight>();
                            var newText = data[m.Groups["n"].Value];
                            var firstLine = true;
                            foreach (var line in Regex.Split(newText, @"\\n"))
                            {
                                if (firstLine) firstLine = false;
                                else firstRun.Append(new DocumentFormat.OpenXml.Drawing.Break());
                                firstRun.Append(new Text(line));
                            }
                            pool.Skip(1).ToList().ForEach(o => o.Remove());
                        }
                    }

                }
            }

            public static byte[] GenerateDocx(byte[] template, Dictionary<string, string> data)
            {
                using (var ms = new MemoryStream())
                {
                    ms.Write(template, 0, template.Length);
                    using (var docx = WordprocessingDocument.Open(ms, true))
                    {
                        docx.MainDocumentPart.HeaderParts.ToList().ForEach(hdr =>
                        {
                            hdr.Header.ReplaceParserTag(data);
                        });
                        docx.MainDocumentPart.FooterParts.ToList().ForEach(ftr =>
                        {
                            ftr.Footer.ReplaceParserTag(data);
                        });
                        docx.MainDocumentPart.Document.Body.ReplaceParserTag(data);
                        docx.Save();
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}