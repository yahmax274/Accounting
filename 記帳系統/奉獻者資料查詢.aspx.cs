using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class 奉獻者資料查詢 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 確認 Session 是否存
                if (Session["SearchResults"] != null)
                {
                    DataTable dt = (DataTable)Session["SearchResults"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    // 設定選擇的行的背景色
                    row.CssClass = "selectedRow";

                }
                else
                {
                    // 還原其他行的背景色
                    row.CssClass = string.Empty;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("起始頁面.aspx");
        }

        protected void ChoiceButton_Click(object sender, EventArgs e)
        {
            // 檢查是否有選取的行
            if (GridView1.SelectedRow != null)
            {
                // 取得選取行的索引
                int selectedIndex = GridView1.SelectedRow.RowIndex;

                // 取得選取行的資料
                string selectedId = GridView1.Rows[selectedIndex].Cells[1].Text; // 取得 Id 的值
                string selectedName = GridView1.Rows[selectedIndex].Cells[2].Text; // "Name"在第三個列（索引為2）
                string selectedPhone1 = GridView1.Rows[selectedIndex].Cells[3].Text; // "Phone1"在第四個列（索引為3）
                string selectedPhone2 = GridView1.Rows[selectedIndex].Cells[4].Text; // "Phone2"在第五個列（索引為4）
                string selectedPostalCode = GridView1.Rows[selectedIndex].Cells[5].Text; // "Postal_Code"在第六個列（索引為5）
                string selectedAddress = GridView1.Rows[selectedIndex].Cells[6].Text; // "Address"在第七個列（索引為6）
                string selectedNote = GridView1.Rows[selectedIndex].Cells[7].Text; // "note"在第八個列（索引為7）

                // 將選取的資料存儲在 Session 中
                Session["SelectedId"] = selectedId;
                Session["SelectedName"] = selectedName;
                Session["SelectedPhone1"] = selectedPhone1;
                Session["SelectedPhone2"] = selectedPhone2;
                Session["SelectedPostalCode"] = selectedPostalCode;
                Session["SelectedAddress"] = selectedAddress;
                Session["SelectedNote"] = selectedNote;

                // 返回到"奉獻者基本資料維護頁面.aspx"
                Response.Redirect("奉獻者基本資料維護頁面.aspx");


            }
        }
    }
}