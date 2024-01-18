using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Org.BouncyCastle.Crypto.Tls;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Data;

namespace 記帳系統
{
    public partial class IncomeRegistrationIndex : System.Web.UI.Page
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
                    string selectedGroup = Session["SelectedGroup"].ToString();

                    //判斷是否為空值
                    selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                    selectedGroup = (selectedGroup == "&nbsp;") ? string.Empty : selectedGroup;

                    // 將資料顯示在對應的TextBox中
                    IDTextBox.Text = selectedUser_Id;
                    NameTextBox.Text = selectedName;
                    GroupTextBox.Text = selectedGroup;

                    // 清除 Session 中的選擇資料
                    Session.Remove("SelectedUser_Id");
                    Session.Remove("SelectedName");
                    Session.Remove("SelectedGroup");
                  
                }
                if (!string.IsNullOrEmpty(DateTextBox.Text))
                {
                    string inputDateText = DateTextBox.Text; // 從使用者輸入的文本框中取得日期

                    // 解析使用者輸入的日期
                    DateTime parsedDate = DateTime.ParseExact(inputDateText, "yyyy-MM-dd", null);

                    // 使用 MaxCertificateNumber 方法獲取憑證號碼
                    string newCertificateNumber = MaxCertificateNumber(parsedDate);

                    CertificateTextBox.Text = newCertificateNumber;
                }

            }
        }


        protected void MemberButton_Click(object sender, EventArgs e)
        {
            string income_guide = "income_guide";
            Session["income_guide"] = income_guide;
            Response.Redirect("MemberQuery.aspx");
        }


        protected void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            if (index >= 0)
            {

            }
            else
            {
                string inputDateText = DateTextBox.Text; // 從使用者輸入的文本框中取得日期

                // 解析使用者輸入的日期
                DateTime parsedDate = DateTime.ParseExact(inputDateText, "yyyy-MM-dd", null);

                // 使用 MaxCertificateNumber 方法獲取憑證號碼
                string number = MaxCertificateNumber(parsedDate);

                CertificateTextBox.Text = number;
            }
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
            if (index >= 0)
            {
                // 取得選定行的DataKeys集合
                DataKey dataKeys = GridView1.DataKeys[index];

                // 確認DataKeys不為空並且包含Id
                if (dataKeys != null && dataKeys.Values["目次"] != null)
                {
                    // 讀取Id的值
                    string idValue = dataKeys.Values["目次"].ToString();

                    // 取得選取行的索引
                    int selectedIndex = GridView1.SelectedRow.RowIndex;

                    // 取得選取行的資料
                    string selectedCertificate = GridView1.Rows[selectedIndex].Cells[1].Text; // 取得 Certificate 的值
                    string selectedContribution = GridView1.Rows[selectedIndex].Cells[2].Text; //取得 Contribution 的值
                    string selectedName = GridView1.Rows[selectedIndex].Cells[3].Text; // 取得 Name 的值
                    string selectedUser_Id = GridView1.Rows[selectedIndex].Cells[4].Text; // 取得 User_Id 的值
                    string selectedDep = GridView1.Rows[selectedIndex].Cells[5].Text; // 取得 Dep 的值
                    string selectedDate = GridView1.Rows[selectedIndex].Cells[6].Text; // 取得 Date 的值
                    string selectedNote = GridView1.Rows[selectedIndex].Cells[7].Text; // 取得 Note 的值
                    string selectedProject = GridView1.Rows[selectedIndex].Cells[12].Text; //取得 Project 的值...
                    string selectedAmount = GridView1.Rows[selectedIndex].Cells[9].Text; // 取得 Amount 的值
                    string selectedVoucher = GridView1.Rows[selectedIndex].Cells[10].Text; // 取得 Voucher 的值
                    string selectedReceipt = GridView1.Rows[selectedIndex].Cells[11].Text; // 取得 Receipt 的值


                    //判斷是否為空值
                    selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                    selectedCertificate = (selectedCertificate == "&nbsp;") ? string.Empty : selectedCertificate;
                    selectedContribution = (selectedContribution == "&nbsp;") ? string.Empty : selectedContribution;
                    selectedDep = (selectedDep == "&nbsp;") ? string.Empty : selectedDep;
                    selectedDate = (selectedDate == "&nbsp;") ? string.Empty : selectedDate;
                    selectedNote = (selectedNote == "&nbsp;") ? string.Empty : selectedNote;
                    idValue = (idValue == "&nbsp;") ? string.Empty : idValue;
                    selectedProject = (selectedProject == "&nbsp;") ? string.Empty : selectedProject;
                    selectedAmount = (selectedAmount == "&nbsp;") ? string.Empty : selectedAmount;
                    selectedVoucher = (selectedVoucher == "&nbsp;") ? string.Empty : selectedVoucher;
                    selectedReceipt = (selectedReceipt == "&nbsp;") ? string.Empty : selectedReceipt;

                    DateTextBox.Text = selectedDate;
                    CertificateTextBox.Text= selectedCertificate;
                    ContributionDropDownList.Text = selectedContribution;
                    NameTextBox.Text = selectedName;
                    IDTextBox.Text = selectedUser_Id;
                    ReceiptDropDownList.Text = selectedReceipt;
                    DepDropDownList.Text = selectedDep;
                    ProjectTextBox.Text = selectedProject;
                    AmountTextBox.Text = selectedAmount;
                    NoteTextBox.Text = selectedNote;
                    VoucherTextBox.Text = selectedVoucher;
                    KeyIDTextBox.Text = idValue;

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

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // 獲取畫面上輸入的數據
            string Certificate = CertificateTextBox.Text; // 取得 Certificate 的值
            string Contribution = ContributionDropDownList.Text; //取得 Contribution 的值
            string Name = NameTextBox.Text; // 取得 Name 的值
            string User_Id = IDTextBox.Text; // 取得 User_Id 的值
            string Dep = DepDropDownList.Text; // 取得 Dep 的值
            string Date = DateTextBox.Text; // 取得 Date 的值
            string Note = NoteTextBox.Text; // 取得 Note 的值
            string Project = ProjectTextBox.Text; //取得 Project 的值
            string Amount = AmountTextBox.Text; // 取得 Amount 的值
            string Voucher = VoucherTextBox.Text; // 取得 Voucher 的值
            string Receipt = ReceiptDropDownList.Text; // 取得 Receipt 的值
            string Group = GroupTextBox.Text; // 取得 Group 的值

            if (KeyIDTextBox != null)
            {
                string inputDateText = DateTextBox.Text; // 從使用者輸入的文本框中取得日期

                // 解析使用者輸入的日期
                DateTime parsedDate = DateTime.ParseExact(inputDateText, "yyyy-MM-dd", null);

                // 使用 MaxCertificateNumber 方法獲取憑證號碼
                string number = MaxCertificateNumber(parsedDate);

                Certificate = number;
            }
            // 使用ADO.NET執行資料庫插入操作
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string insertQuery = "INSERT INTO [New_Income_Form] ([憑證號碼],[奉獻項目], [姓名],[User_ID], [日期], [收據], [部門], [傳票號碼], [金額], [摘要],[專案名稱],[組別]) " +
                    "VALUES (@憑證號碼, @奉獻項目, @姓名, @User_ID, @日期, @收據, @部門, @傳票號碼, @金額, @摘要, @專案名稱, @組別)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@憑證號碼", Certificate);
                cmd.Parameters.AddWithValue("@奉獻項目", Contribution);
                cmd.Parameters.AddWithValue("@姓名", Name);
                cmd.Parameters.AddWithValue("@User_ID", User_Id);
                cmd.Parameters.AddWithValue("@日期", Date);
                cmd.Parameters.AddWithValue("@收據", Receipt);
                cmd.Parameters.AddWithValue("@部門", Dep);
                cmd.Parameters.AddWithValue("@傳票號碼", Voucher);
                cmd.Parameters.AddWithValue("@金額", Amount);
                cmd.Parameters.AddWithValue("@摘要", Note);
                cmd.Parameters.AddWithValue("@專案名稱", Project);
                cmd.Parameters.AddWithValue("@組別", Group);
                cmd.ExecuteNonQuery();
            }
            // 清空TextBox
            ClearTextBoxes();

            // 重新載入資料或更新UI
            // 這裡你可以更新GridView或其他UI元件，以顯示最新數據
            GridView1.DataBind(); // 重新綁定GridView;

            ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToBottom", "ScrollToBottom();", true);
        }
        private void ClearTextBoxes()
        {
            // 清空TextBox
            CertificateTextBox.Text = "";
            ContributionDropDownList.Text = "";
            NameTextBox.Text = "";
            DateTextBox.Text = "";
            IDTextBox.Text = "";
            GroupTextBox.Text = "";
            ReceiptDropDownList.Text = "";
            DepDropDownList.Text = "";
            ProjectTextBox.Text = "";
            AmountTextBox.Text = "";
            NoteTextBox.Text = "";
            VoucherTextBox.Text = "";
            KeyIDTextBox.Text="";
        }

        protected void IndexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        private string MaxCertificateNumber(DateTime parsedDate)
        {
            // 將日期轉換為指定的格式：yyyyMMdd
            string formattedDate = parsedDate.ToString("yyyyMMdd");

            // 建立連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // 建立 SQL 查詢語句
            string sqlQuery = "SELECT MAX([憑證號碼]) AS MaxCertificateNumber FROM [New_Income_Form] WHERE [憑證號碼] LIKE @FormattedDate";

            // 建立 SQLDataAdapter 和 DataTable 來執行查詢
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                {
                    // 設定查詢條件
                    adapter.SelectCommand.Parameters.AddWithValue("@FormattedDate", $"{formattedDate}%");

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // 檢查查詢結果是否包含數據
                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["MaxCertificateNumber"] != DBNull.Value)
                    {
                        // 有查到數據，生成新的憑證號碼
                        int maxNumber = int.Parse(dataTable.Rows[0]["MaxCertificateNumber"].ToString().Substring(8)); // 取得最大數字部分
                        return $"{formattedDate}{(maxNumber + 1):D3}";
                    }
                    else
                    {
                        // 沒有查到數據，回傳新的憑證號碼（001）
                        return $"{formattedDate}001";
                    }
                }
            }
        }

        protected void ReviseButton_Click(object sender, EventArgs e)
        {
            string keyid = KeyIDTextBox.Text;
            if (!String.IsNullOrEmpty(keyid))
            {

                string reviseCertificate = CertificateTextBox.Text; // 取得 Certificate 的值
                string reviseContribution = ContributionDropDownList.Text; //取得 Contribution 的值
                string reviseName = NameTextBox.Text; // 取得 Name 的值
                string reviseUser_Id = IDTextBox.Text; // 取得 User_Id 的值
                string reviseDep = DepDropDownList.Text; // 取得 Dep 的值
                string reviseDate = DateTextBox.Text; // 取得 Date 的值
                string reviseNote = NoteTextBox.Text; // 取得 Note 的值
                string reviseProject = ProjectTextBox.Text; //取得 Project 的值
                string reviseAmount = AmountTextBox.Text; // 取得 Amount 的值
                string reviseVoucher = VoucherTextBox.Text; // 取得 Voucher 的值
                string reviseReceipt = ReceiptDropDownList.Text; // 取得 Receipt 的值
                string reviseGroup = GroupTextBox.Text; // 取得 Group 的值

                int selectedId = Convert.ToInt32(keyid);

                // 直接更新資料庫中的資料
                ReviseData(selectedId, reviseCertificate, reviseContribution, reviseName, reviseUser_Id, reviseDep, reviseDate, reviseNote
                    , reviseProject, reviseAmount, reviseVoucher, reviseReceipt, reviseGroup);

                // 清空TextBox
                ClearTextBoxes();

                // 重新載入資料或更新UI
                // 這裡你可以更新GridView或其他UI元件，以顯示最新數據
                GridView1.DataBind(); // 重新綁定GridView;

                // 調用 JavaScript 腳本來調整滾動位置
                ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "NewAdjustScrollPosition();", true);
            }
        }
        private void ReviseData(int Id, string Certificate, string Contribution, string Name, string User_Id, string Dep, string Date, string Note
            , string Project, string Amount, string Voucher, string Receipt, string Group)
        {
            // 請替換這裡的連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL 更新語句
            string updateQuery = @"
        UPDATE [New_Income_Form]
        SET
            [憑證號碼] = @憑證號碼,
            [奉獻項目] = @奉獻項目,
            [姓名] = @姓名,
            [User_ID] = @User_ID,
            [日期] = @日期,
            [收據] = @收據,
            [部門] = @部門,
            [傳票號碼] = @傳票號碼,
            [金額] = @金額,
            [摘要] = @摘要,
            [專案名稱] = @專案名稱,
            [組別] = @組別
        WHERE
            [目次] = @目次;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    // 添加參數
                    cmd.Parameters.AddWithValue("@目次", Id);
                    cmd.Parameters.AddWithValue("@憑證號碼", Certificate);
                    cmd.Parameters.AddWithValue("@奉獻項目", Contribution);
                    cmd.Parameters.AddWithValue("@姓名", Name);
                    cmd.Parameters.AddWithValue("@User_Id", User_Id);
                    cmd.Parameters.AddWithValue("@日期", Date);
                    cmd.Parameters.AddWithValue("@收據", Receipt);
                    cmd.Parameters.AddWithValue("@部門", Dep);
                    cmd.Parameters.AddWithValue("@傳票號碼", Voucher);
                    cmd.Parameters.AddWithValue("@金額", Amount);
                    cmd.Parameters.AddWithValue("@摘要", Note);
                    cmd.Parameters.AddWithValue("@專案名稱", Project);
                    cmd.Parameters.AddWithValue("@組別", Group);
                    // 打開連接，執行更新語句
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string keyid = KeyIDTextBox.Text;
            if (!String.IsNullOrEmpty(keyid))
            {

                int selectedId = Convert.ToInt32(keyid);

                // 直接更新資料庫中的資料
                DeleteData(selectedId);

                // 清空TextBox
                ClearTextBoxes();

                // 重新載入資料或更新UI
                // 這裡你可以更新GridView或其他UI元件，以顯示最新數據
                GridView1.DataBind(); // 重新綁定GridView;

                ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToBottom", "ScrollToBottom();", true);
            }
        }
        private void DeleteData(int id)
        {
            // 請替換這裡的連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL 刪除語句
            string deleteQuery = "DELETE FROM [New_Income_Form] WHERE [目次] = @目次";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    // 添加參數
                    command.Parameters.AddWithValue("@目次", id);

                    // 打開連接，執行刪除語句
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}