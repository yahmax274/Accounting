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
    public partial class DonationReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 檢查 Session 中是否存在選擇的資料
                if (Session["SelectedName"] != null)
                {
                    // 取得選擇的資料
                    string selectedUser_Id = Session["SelectedUser_Id"].ToString();
                    string selectedName = Session["SelectedName"].ToString();

                    //判斷是否為空值
                    selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;

                    // 動態修改 SqlDataSource1 的 SelectCommand
                    SqlDataSource1.SelectCommand = "SELECT [目次], [憑證號碼], [奉獻項目], [日期], [傳票號碼], [金額], [姓名], [User_ID] FROM [New_Income_Form] WHERE [姓名] = @SelectedName AND [User_ID] = @SelectedUser_Id";
                    SqlDataSource1.SelectParameters.Clear();
                    SqlDataSource1.SelectParameters.Add("SelectedName", selectedName);
                    SqlDataSource1.SelectParameters.Add("SelectedUser_Id", selectedUser_Id);

                    // 重新繫結 GridView
                    GridView1.DataBind();

                    IDTextBox.Text = selectedUser_Id;
                    NameTextBox.Text = selectedName;

                    // 清除 Session 中的選擇資料
                    Session.Remove("SelectedUser_Id");
                    Session.Remove("SelectedName");
                }

                // 取得當下的西元年
                int currentYear = DateTime.Now.Year % 100;
                string nowyear = currentYear.ToString("D2");

                // 使用 SQL 查詢找到 [傳票號碼] 的最大值
                // 建立連接字串
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 使用 SqlCommand 執行 SQL 查詢
                    string query = "SELECT MAX(CAST(RIGHT([傳票號碼], 5) AS INT)) AS MaxNumber FROM [New_Income_Form] WHERE LEFT([傳票號碼], 2) = @Year";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 將年份傳遞給 SQL 查詢
                        command.Parameters.AddWithValue("@Year", nowyear);

                        // 執行查詢並讀取結果
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            // 讀取最大值
                            int maxNumber = reader["MaxNumber"] is DBNull ? 0 : (int)reader["MaxNumber"];

                            // 判斷是否為該年的第一筆資料
                            if (maxNumber == 0)
                            {
                                // 若是第一筆，直接設定為 nowyear 後接 "00001"
                                StartNumberTextBox.Text = $"{nowyear}00001";
                            }
                            else
                            {
                                // 若不是第一筆，則按原邏輯處理
                                StartNumberTextBox.Text = $"{nowyear}{(maxNumber + 1).ToString("D5")}";
                            }
                        }
                        reader.Close();
                    }
                }
            }
        }
        protected void SelectButton_Click(object sender, EventArgs e)
        {
            string donation_guide = "donation_guide";
            Session["donation_guide"] = donation_guide;
            Response.Redirect("MemberQuery.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRowCount = 0;

            // 迭代所有行
            foreach (GridViewRow row in GridView1.Rows)
            {
                // 取得選定行的 CommandField 控制項
                var selectButton = row.Cells[0].Controls.OfType<LinkButton>().FirstOrDefault();

                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    // 切換選擇的行的背景色
                    if (row.CssClass.Contains("selectedRow"))
                    {
                        row.CssClass = string.Empty;
                        selectButton.Text = "□";
                    }
                    else
                    {
                        row.CssClass = "selectedRow";
                        selectButton.Text = "☑";
                        selectedRowCount++;
                    }
                }
                else
                {
                    // 保留其他行的狀態
                    if (row.CssClass.Contains("selectedRow"))
                    {
                        row.CssClass = "selectedRow";
                        selectButton.Text = "☑";
                        selectedRowCount++;
                    }
                    else
                    {
                        row.CssClass = string.Empty;
                        selectButton.Text = "□";
                    }
                }
            }

            // 將選取的行數設定到 SelectCountTextBox
            SelectCountTextBox.Text = selectedRowCount.ToString();

            // 調用 JavaScript 腳本來調整滾動位置
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(StartNumberTextBox.Text, out int startNumber))
            {
                // 設定 DataStartTextBox 的值為起始值
                DataStartTextBox.Text = startNumber.ToString();

                if (int.TryParse(SelectCountTextBox.Text, out int selectedRowCount))
                {
                    int endNumber = startNumber + selectedRowCount;
                    EndNumberTextBox.Text = endNumber.ToString();
                   
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.CssClass.Contains("selectedRow"))
                        {
                            var index = Convert.ToInt32(GridView1.DataKeys[row.RowIndex]["目次"]);
                            string serialNumber = $"{startNumber++}";

                            SaveSerialNumberToDatabase(index, serialNumber);
                        }
                    }

                    // 更新數據庫後重新繫結 GridView
                    GridView1.DataBind();

                    // 重新計算 SelectCountTextBox、StartNumberTextBox 的值
                    SelectCountTextBox.Text = "0";
                    StartNumberTextBox.Text = EndNumberTextBox.Text;

                    // 設定 DataEndTextBox 的值為最新的 StartNumberTextBox.Text - DataStartTextBox.Text - 1
                    int dataEndValue = int.Parse(StartNumberTextBox.Text) - int.Parse(DataStartTextBox.Text) - 1 + int.Parse(DataStartTextBox.Text);
                    DataEndTextBox.Text = dataEndValue.ToString();

                    string generatedRange = $"產生收據編號自 {DataStartTextBox.Text} 至 {DataEndTextBox.Text}";

                    // 調整滾動位置
                    ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);

                    // 顯示提示視窗
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", $"alert('{generatedRange}');", true);
                }
            }
        }


        private void SaveSerialNumberToDatabase(int index, string serialNumber)
        {
            // 使用 ADO.NET 連接資料庫
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                connection.Open();

                // 更新資料庫中的 [傳票號碼] 欄位
                string updateQuery = "UPDATE New_Income_Form SET 傳票號碼 = @SerialNumber WHERE 目次 = @Index";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // 直接使用方法參數中的 index
                    command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                    command.Parameters.AddWithValue("@Index", index);

                    command.ExecuteNonQuery();
                }
            }
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            int selectedRowCount = 0;

            // 迭代所有行，取消選取
            foreach (GridViewRow row in GridView1.Rows)
            {
                var selectButton = row.Cells[0].Controls.OfType<LinkButton>().FirstOrDefault();
                row.CssClass = string.Empty;
                selectButton.Text = "□";
            }

            // 更新選取的行數
            SelectCountTextBox.Text = selectedRowCount.ToString();

            // 調用 JavaScript 腳本來調整滾動位置
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);
        }

        protected void SelectAllButton_Click(object sender, EventArgs e)
        {
            int selectedRowCount = 0;
            // 迭代所有行，選取
            foreach (GridViewRow row in GridView1.Rows)
            {
                var selectButton = row.Cells[0].Controls.OfType<LinkButton>().FirstOrDefault();
                row.CssClass = "selectedRow";
                selectButton.Text = "☑";
                selectedRowCount++;
            }

            // 更新選取的行數
            SelectCountTextBox.Text = selectedRowCount.ToString();

            // 調用 JavaScript 腳本來調整滾動位置
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);
        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            Session["DataStart"] = DataStartTextBox.Text;
            Session["DataEnd"] = DataEndTextBox.Text;
            Session["TypeChoice"] = "編號列印";
            Response.Redirect("IncomeReceiptIndex.aspx");
        }
    }
}