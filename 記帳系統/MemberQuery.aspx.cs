using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class MemberQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void IDButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string User_Id = IDTextBox.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group] ,[ID] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(User_Id))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [User_Id] LIKE N'%{User_Id}%'";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
        }

        protected void NameButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string Name = NameTextBox.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group] ,[ID] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(Name))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [Name] LIKE N'%{Name}%'";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
        }

        protected void PhoneButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string phone = PhoneTextBox.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group] ,[ID] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(phone))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND ([Phone1] LIKE '%{phone}%' OR [Phone2] LIKE '%{phone}%')";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
        }

        protected void AddressButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string Address = AddressTextBox.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group] ,[ID] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(Address))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [Address] LIKE N'%{Address}%'";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
        }

        protected void GroupButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋條件
            string Group = GroupDropDownList.Text;

            // 構建 SQL 查詢字串
            string sqlQuery = "SELECT [Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group] ,[ID] FROM [User_Form] WHERE 1 = 1";

            if (!string.IsNullOrEmpty(Group))
            {
                // 修改為使用 N 前綴處理中文字符
                sqlQuery += $" AND [Group] LIKE N'%{Group}%'";
            }

            // 將構建好的查詢字串設定給 SqlDataSource1
            SqlDataSource1.SelectCommand = sqlQuery;

            // 重新繫結 GridView
            GridView1.DataBind();
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

            // 調用 JavaScript 腳本來調整滾動位置
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);
        }

        protected void TopButton_Click(object sender, EventArgs e)
        {
            // 注冊 JavaScript 腳本
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToTop", "ScrollToTop();", true);
        }

        protected void BottomButton_Click(object sender, EventArgs e)
        {
            // 注冊 JavaScript 腳本
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToBottom", "ScrollToBottom();", true);
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            // 檢查是否有選取的行
            if (GridView1.SelectedRow != null)
            {
                // 取得選定的索引
                int index = GridView1.SelectedIndex;
                DataKey dataKeys = GridView1.DataKeys[index];

                // 讀取Id的值
                string idValue = dataKeys.Values["Id"].ToString();

                // 取得選取行的索引
                int selectedIndex = GridView1.SelectedRow.RowIndex;

                // 取得選取行的資料
                string selectedUser_Id = GridView1.Rows[selectedIndex].Cells[2].Text; // 取得 User_Id 的值
                string selectedName = GridView1.Rows[selectedIndex].Cells[4].Text; // 取得 Name 的值
                string selectedGroup = GridView1.Rows[selectedIndex].Cells[3].Text; // 取得 Group 的值
                string selectedPhone1 = GridView1.Rows[selectedIndex].Cells[5].Text; // 取得 Phone1 的值
                string selectedPhone2 = GridView1.Rows[selectedIndex].Cells[6].Text; //取得 Phone2 的值
                string selectedPostal_Code = GridView1.Rows[selectedIndex].Cells[7].Text; // 取得 Postal_Code 的值
                string selectedAddress = GridView1.Rows[selectedIndex].Cells[8].Text; // 取得 Address 的值
                string selectedNote = GridView1.Rows[selectedIndex].Cells[9].Text; // 取得 note 的值


                // 將選取的資料存儲在 Session 中
                Session["SelectedUser_Id"] = selectedUser_Id;
                Session["SelectedName"] = selectedName;
                Session["SelectedGroup"] = selectedGroup;
                Session["SelectedPhone1"] = selectedPhone1;
                Session["SelectedPhone2"] = selectedPhone2;
                Session["SelectedPostalCode"] = selectedPostal_Code;
                Session["SelectedAddress"] = selectedAddress;
                Session["SelectedNote"] = selectedNote;
                Session["SelectedidValue"] = idValue;

                // 返回到"MemberRegisterIndex.aspx"
                Response.Redirect("MemberRegisterIndex.aspx");

            }
        }
    }
}