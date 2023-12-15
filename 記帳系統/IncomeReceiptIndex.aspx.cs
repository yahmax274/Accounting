using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class IncomeReceiptIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 檢查 Session 是否包含值
                if (Session["MoveBack"] != null && Session["MoveBack"].ToString() == "MoveBack")
                {
                    // 清空頁面上的所有 Session
                    Session.Clear();
                }

                // 如果 Session["MoveBack"] 不是 "MoveBack"，執行正常的頁面初始化

                if (Session["StartTextBoxValue"] != null)
                {
                    StartTextBox.Text = Session["StartTextBoxValue"].ToString();
                }

                if (Session["EndTextBoxValue"] != null)
                {
                    EndTextBox.Text = Session["EndTextBoxValue"].ToString();
                }

                if (Session["OnlyOneCheckBoxValue"] != null)
                {
                    bool onlyOneChecked = (bool)Session["OnlyOneCheckBoxValue"];
                    OnlyOneCheckBox.Checked = onlyOneChecked;

                    // 根據 OnlyOneCheckBox 的狀態設定 SelectButton 的樣式
                    if (onlyOneChecked)
                    {
                        SelectButton.CssClass = ""; // 移除任何自訂的 CSS 類別
                        SelectButton.Enabled = true; // 啟用按鈕
                    }
                    else
                    {
                        SelectButton.CssClass = "disabled-button"; // 添加禁用樣式的 CSS 類別
                        SelectButton.Enabled = false; // 禁用按鈕
                    }


                    // 清除 Session 中的值
                    Session.Remove("StartTextBoxValue");
                    Session.Remove("EndTextBoxValue");
                    Session.Remove("OnlyOneCheckBoxValue");
                }
            }
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            // 將控制項的值存入 Session
            Session["StartTextBoxValue"] = StartTextBox.Text;
            Session["EndTextBoxValue"] = EndTextBox.Text;
            Session["OnlyOneCheckBoxValue"] = OnlyOneCheckBox.Checked;

            string receipt_guide = "receipt_guide";
            Session["receipt_guide"] = receipt_guide;
            Response.Redirect("MemberQuery.aspx");
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            // 取得搜尋的姓名資訊
            string selectedUser_Id = Session["SelectedUser_Id"]?.ToString() ?? string.Empty;
            string selectedName = Session["SelectedName"]?.ToString() ?? string.Empty;

            // 判斷是否為空值
            selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
            selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;

            // 清除 Session 中的選擇資料
            Session.Remove("SelectedUser_Id");
            Session.Remove("SelectedName");

            // 取得搜尋的時間區間資訊
            string StartDay = StartTextBox.Text;
            string EndDay = EndTextBox.Text;
            Session["StartDay"] = StartDay;
            Session["EndDay"] = EndDay;

            // 使用查詢條件查詢資料
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string selectQuery = "SELECT [憑證號碼], [奉獻項目], [姓名], [User_ID] AS 代號, [日期], [收據], [部門], [傳票號碼], [金額], [摘要], [專案名稱], [組別] FROM [New_Income_Form] WHERE 1 = 1";

                // 加入搜尋條件，要求 [姓名] 和 [User_ID] 同時符合
                if (!string.IsNullOrEmpty(selectedName) && !string.IsNullOrEmpty(selectedUser_Id))
                {
                    selectQuery += " AND [姓名] = @SelectedName AND [User_ID] = @SelectedUser_Id";
                }

                // 加入日期區間條件
                if (!string.IsNullOrEmpty(StartDay) && !string.IsNullOrEmpty(EndDay))
                {
                    // 使用 CONVERT 將日期字串轉換為日期型態
                    selectQuery += " AND CONVERT(DATE, [日期], 23) BETWEEN CONVERT(DATE, @StartDay, 23) AND CONVERT(DATE, @EndDay, 23)";
                }

                SqlCommand cmd = new SqlCommand(selectQuery, con);

                // 加入搜尋條件的參數
                if (!string.IsNullOrEmpty(selectedName) && !string.IsNullOrEmpty(selectedUser_Id))
                {
                    cmd.Parameters.AddWithValue("@SelectedName", selectedName);
                    cmd.Parameters.AddWithValue("@SelectedUser_Id", selectedUser_Id);
                }

                // 加入日期區間的參數
                if (!string.IsNullOrEmpty(StartDay) && !string.IsNullOrEmpty(EndDay))
                {
                    // 使用 CONVERT 將日期字串轉換為日期型態
                    cmd.Parameters.AddWithValue("@StartDay", StartDay);
                    cmd.Parameters.AddWithValue("@EndDay", EndDay);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 將查詢結果保存到 Session
                Session["SearchResults"] = dt;
            }

            Response.Redirect("PrintDemand.aspx");
        }
    }
}