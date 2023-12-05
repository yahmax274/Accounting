using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class 收入登記表 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 檢查 Session 中是否存在選擇的資料
                if (Session["SelectedName"] != null)
                {
                    // 取得選擇的資料
                    string SelectedId = Session["SelectedId"].ToString();
                    string selectedName = Session["SelectedName"].ToString();

                    //判斷是否為空值
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                    // 將資料顯示在對應的TextBox中
                    TextBoxDonor.Text = selectedName;
                    IDTextBox.Text = SelectedId;
                    // 清除 Session 中的選擇資料
                    Session.Remove("SelectedId");
                    Session.Remove("SelectedName");

                }

            }
        }



        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("成員查詢.aspx");
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("起始頁面.aspx");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // 獲取畫面上輸入的數據
            string contribution = ContributionDropDownList.Text;
            string name = TextBoxDonor.Text;
            // 解析日期並格式化為yyyyMMdd
            string date = DateTime.Parse(TextBoxDate.Text).ToString("yyyyMMdd");
            string receipt = TextBoxReceipt.Text;
            string department = TextBoxDepartment.Text;
            string amount = TextBoxAmount.Text;
            string summary = TextBoxSummary.Text;
            string ID = IDTextBox.Text;
            string Voucher = TextBoxVoucher.Text;

            DateTime inputDate = DateTime.Parse(TextBoxDate.Text);

            // 獲取當天的最大流水號
            int maxSerialNumber = GetMaxSerialNumber(inputDate);

            // 新的憑證號碼
            string number = date + (maxSerialNumber + 1).ToString("D3");
            // 使用ADO.NET執行資料庫插入操作
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string insertQuery = "INSERT INTO [New_Income_Form] ([憑證號碼],[奉獻項目], [姓名],[User_ID], [日期], [收據], [部門], [傳票號碼], [金額], [摘要]) VALUES (@憑證號碼, @奉獻項目, @姓名, @User_ID, @日期, @收據, @部門, @傳票號碼, @金額, @摘要)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@憑證號碼", number);
                cmd.Parameters.AddWithValue("@奉獻項目", contribution);
                cmd.Parameters.AddWithValue("@姓名", name);
                cmd.Parameters.AddWithValue("@User_ID", ID);
                cmd.Parameters.AddWithValue("@日期", date);
                cmd.Parameters.AddWithValue("@收據", receipt);
                cmd.Parameters.AddWithValue("@部門", department);
                cmd.Parameters.AddWithValue("@傳票號碼", Voucher);
                cmd.Parameters.AddWithValue("@金額", amount);
                cmd.Parameters.AddWithValue("@摘要", summary);
                cmd.ExecuteNonQuery();
            }

            // 清空TextBox
            ContributionDropDownList.Text = "";
            TextBoxDonor.Text = "";
            TextBoxDate.Text = "";
            TextBoxReceipt.Text = "";
            TextBoxDepartment.Text = "";
            TextBoxAmount.Text = "";
            TextBoxSummary.Text = "";
            IDTextBox.Text = "";
            TextBoxVoucher.Text = "";

            // 重新載入資料或更新UI
            // 這裡你可以更新GridView或其他UI元件，以顯示最新數據
            GridView1.DataBind(); // 重新綁定GridView;

            // 獲取 GridView1 的總頁數
            int totalPages = GridView1.PageCount;

            // 設置 GridView1 的分頁索引為最後一頁的索引
            GridView1.PageIndex = totalPages - 1;
            GridView1.DataBind();
        }
        // 獲取當天的最大流水號
        private int GetMaxSerialNumber(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string selectQuery = "SELECT MAX(CAST(RIGHT([憑證號碼], 3) AS INT)) FROM [New_Income_Form] WHERE [日期] = @日期";
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                cmd.Parameters.AddWithValue("@日期", date.ToString("yyyyMMdd"));

                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0; // 如果當天還沒有憑證號碼，則返回0
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // 執行查詢操作，將查詢結果保存到 Session
            string contribution = ContributionDropDownList.Text;
            string name = TextBoxDonor.Text;
            string date = TextBoxDate.Text;
            string receipt = TextBoxReceipt.Text;
            string department = TextBoxDepartment.Text;
            string amount = TextBoxAmount.Text;
            string summary = TextBoxSummary.Text;
            string ID = IDTextBox.Text;
            string Voucher = TextBoxVoucher.Text;

            // 使用查詢條件查詢資料
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string selectQuery = "SELECT * FROM [New_Income_Form] WHERE " +
                                     "([奉獻項目] LIKE @奉獻項目) AND " +
                                     //"([姓名] LIKE @姓名) AND " +
                                     "([User_ID] LIKE @User_ID) AND " +
                                     "([日期] LIKE @日期) AND " +
                                     "([收據] LIKE @收據) AND " +
                                     "([部門] LIKE @部門) AND " +
                                     "([金額] LIKE @金額) AND " +
                                     //"([傳票號碼] LIKE @傳票號碼) AND " +
                                     "([摘要] LIKE @摘要)";

                SqlCommand cmd = new SqlCommand(selectQuery, con);
                cmd.Parameters.AddWithValue("@奉獻項目", "%" + contribution + "%");
                cmd.Parameters.AddWithValue("@姓名", "%" + name + "%");
                cmd.Parameters.AddWithValue("@User_ID", "%" + ID + "%");
                cmd.Parameters.AddWithValue("@日期", "%" + date + "%");
                cmd.Parameters.AddWithValue("@收據", "%" + receipt + "%");
                cmd.Parameters.AddWithValue("@部門", "%" + department + "%");
                cmd.Parameters.AddWithValue("@金額", "%" + amount + "%");
                cmd.Parameters.AddWithValue("@傳票號碼", "%" + Voucher + "%");
                cmd.Parameters.AddWithValue("@摘要", "%" + summary + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 將查詢結果保存到 Session
                Session["SearchResults"] = dt;
            }

            // 導航到"資料查詢"頁面
            Response.Redirect("奉獻者收入查詢.aspx");
        }
    }
}