using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace 記帳系統
{
    public partial class 奉獻者收入查詢 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SearchResults"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResults"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
                // 输出查询结果的行数，以确保有数据
                //Response.Write("查询结果行数：" + dt.Rows.Count + "<br>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("起始頁面.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["SearchResults"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResults"];

                // 移除 "目次" 欄
                dt.Columns.Remove("目次");
                dt.Columns.Remove("憑證號碼");
                dt.Columns.Remove("收據");
                dt.Columns.Remove("部門");
                dt.Columns.Remove("傳票號碼");

                // 將 "User_ID" 改為 "代號"
                dt.Columns["User_ID"].ColumnName = "代號";

                // 调整列的顺序
                DataTable reorderedDt = new DataTable();
                reorderedDt.Columns.Add("代號");
                reorderedDt.Columns.Add("姓名");
                reorderedDt.Columns.Add("日期");
                reorderedDt.Columns.Add("奉獻項目");
                reorderedDt.Columns.Add("金額");
                reorderedDt.Columns.Add("摘要");

                foreach (DataRow row in dt.Rows)
                {
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

                // 添加標題信息
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

                // 將標題信息添加到文檔
                doc.Add(titleTable);

                // 創建一個表格
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

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("收入登記表.aspx");
        }
    }
}