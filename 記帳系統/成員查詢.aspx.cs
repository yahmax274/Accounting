using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class 成員查詢 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 取得選定的索引
            int index = GridView1.SelectedIndex;

            // 迭代所有行
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == index)
                {
                    // 設定選擇的行的背景色
                    row.CssClass = "selectedRow";

                    // 取得選定行的 CommandField 控制項
                    var selectButton = row.Cells[0].Controls.OfType<LinkButton>().FirstOrDefault();

                    // 修改 SelectText 的值
                    if (selectButton != null)
                    {
                        selectButton.Text = "☑"; // 將選擇的文本改為☑
                    }
                }
                else
                {
                    // 還原其他行的背景色
                    row.CssClass = string.Empty;
                    // 取得選定行的 CommandField 控制項
                    var selectButton = row.Cells[0].Controls.OfType<LinkButton>().FirstOrDefault();
                    selectButton.Text = "□";
                }
            }

        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            // 檢查是否有選取的行
            if (GridView1.SelectedRow != null)
            {
                // 取得選取行的索引
                int selectedIndex = GridView1.SelectedRow.RowIndex;

                // 取得選取行的資料
                string selectedId = GridView1.Rows[selectedIndex].Cells[1].Text; // 取得 Id 的值
                string selectedName = GridView1.Rows[selectedIndex].Cells[2].Text; // "Name"在第三個列（索引為2）

                // 將選取的資料存儲在 Session 中
                Session["SelectedId"] = selectedId;
                Session["SelectedName"] = selectedName;

                // 返回到"收入登記表頁面.aspx"
                Response.Redirect("收入登記表.aspx");

            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string id = IDTextBox.Text;
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string address = AddressTextBox.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Id], [Name], [Phone1], [Phone2], [Postal_Code], [Address] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(id))
            {
                sqlQuery += $" AND [Id] = '{id}'";
            }

            if (!string.IsNullOrEmpty(name))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [Name] LIKE N'%{name}%'";
            }

            if (!string.IsNullOrEmpty(phone))
            {
                sqlQuery += $" AND ([Phone1] LIKE '%{phone}%' OR [Phone2] LIKE '%{phone}%')";
            }

            if (!string.IsNullOrEmpty(address))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [Address] LIKE N'%{address}%'";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
        }
    }
}