using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class PrintDemand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SearchResults"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResults"];
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["PrintDate"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResults"];

                dt.Columns.Remove("憑證號碼");
                dt.Columns.Remove("收據");
                dt.Columns.Remove("部門");
                dt.Columns.Remove("傳票號碼");

                // 調整列順序
                DataTable reorderedDt = new DataTable();
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

                Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                // 設置字體
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Users\User\Desktop\資料庫\字體\標楷體.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(baseFont, 12);

                doc.Open();

                // 添加標題
                PdfPTable titleTable = new PdfPTable(1);
                PdfPCell titleCell = new PdfPCell(new Phrase("社團法人中華民國基督教佳音宣教協會(台北總會)", titleFont));
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
            if (Session["PrintSerialNumber"] != null)
            {
                Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                // 設置字體
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Users\User\Desktop\資料庫\字體\標楷體.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font contentFont = new iTextSharp.text.Font(baseFont, 12);

                doc.Open();

                // 添加標題
                Paragraph title = new Paragraph("社團法人中華民國基督教佳音宣教協會(台北總會)", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                // 添加空行
                doc.Add(new Paragraph(" "));

                // 新增一行，分成7列
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.DefaultCell.Border = Rectangle.NO_BORDER;

                // 設定每列的寬度百分比
                float[] columnWidths = { 15f, 20f, 5f, 20f, 5f, 15f, 20f };
                table.SetWidths(columnWidths);

                // 添加每一列的內容
                table.AddCell(new Phrase("收據編號:", contentFont));
                table.AddCell(new Phrase(GridView1.Rows[0].Cells[0].Text, contentFont));
                table.AddCell(new Phrase(" ", contentFont));
                table.AddCell(new Phrase("奉獻收據", contentFont));
                table.AddCell(new Phrase(" ", contentFont));
                table.AddCell(new Phrase("開證日期:", contentFont));
                table.AddCell(new Phrase(DateTime.Now.ToString("yyyy-MM-dd"), contentFont));

                // 將表格添加到文檔
                doc.Add(table);

                // 新增第二行，分成3列
                PdfPTable table2 = new PdfPTable(3);
                table2.WidthPercentage = 100;
                table2.DefaultCell.Border = Rectangle.NO_BORDER;

                // 設定每列的寬度百分比
                float[] columnWidths2 = { 30f, 40f, 30f };
                table2.SetWidths(columnWidths2);

                // 添加每一列的內容
                table2.AddCell(new Phrase("奉獻者", contentFont));
                table2.AddCell(new Phrase(GridView1.Rows[0].Cells[2].Text, contentFont));
                table2.AddCell(new Phrase("列A", contentFont));

                // 將表格添加到文檔
                doc.Add(table2);

                // 新增第三行，分成3列
                PdfPTable table3 = new PdfPTable(3);
                table3.WidthPercentage = 100;
                table3.DefaultCell.Border = Rectangle.NO_BORDER;

                // 設定每列的寬度百分比
                float[] columnWidths3 = { 30f, 40f, 30f };
                table3.SetWidths(columnWidths3);

                // 添加每一列的內容
                table3.AddCell(new Phrase("奉獻金額", contentFont));
                table3.AddCell(new Phrase(GridView1.Rows[0].Cells[8].Text, contentFont));
                table3.AddCell(new Phrase("列B", contentFont));

                // 將表格添加到文檔
                doc.Add(table3);

                // 新增第四行，分成3列
                PdfPTable table4 = new PdfPTable(3);
                table4.WidthPercentage = 100;
                table4.DefaultCell.Border = Rectangle.NO_BORDER;

                // 設定每列的寬度百分比
                float[] columnWidths4 = { 30f, 40f, 30f };
                table4.SetWidths(columnWidths4);

                // 添加每一列的內容
                table4.AddCell(new Phrase("奉獻性質", contentFont));
                table4.AddCell(new Phrase(GridView1.Rows[0].Cells[1].Text, contentFont));
                table4.AddCell(new Phrase("列C", contentFont));

                // 將表格添加到文檔
                doc.Add(table4);

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

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("BasicInformation.aspx");
        }
    }
}