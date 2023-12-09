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
                if (string.IsNullOrEmpty(DateTextBox.Text))
                {
                    // 如果 DateTextBox 沒有值，執行以下邏輯
                    string date = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                    DateTime inputDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

                    // 取得當天的最大流水號
                    int maxSerialNumber = GetMaxSerialNumber(inputDate);

                    // 新的憑證號碼
                    string number = date + (maxSerialNumber + 1).ToString("D3");

                    CertificateTextBox.Text = number;
                }

            }
        }


        protected void MemberButton_Click(object sender, EventArgs e)
        {
            string income_guide = "income_guide";
            Session["income_guide"] = income_guide;
            Response.Redirect("MemberQuery.aspx");
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

        protected void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            if (index >= 0)
            {

            }
            else
            {
                // 如果 DateTextBox 有值
                string date = DateTime.Parse(DateTextBox.Text).ToString("yyyyMMdd");
                DateTime inputDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

                // 取得當天的最大流水號
                int maxSerialNumber = GetMaxSerialNumber(inputDate);

                // 新的憑證號碼
                string number = date + (maxSerialNumber + 1).ToString("D3");

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

                    Response.Write(selectedDate);

                    CertificateTextBox.Text= selectedCertificate;
                    ContributionDropDownList.Text = selectedContribution;
                    NameTextBox.Text = selectedName;
                    //DateTextBox.Text = DateTime.Parse(selectedDate).ToString("yyyy-MM-dd");
                    IDTextBox.Text = selectedUser_Id;
                    ReceiptDropDownList.Text = selectedReceipt;
                    DepDropDownList.Text = selectedDep;
                    ProjectTextBox.Text = selectedProject;
                    AmountTextBox.Text = selectedAmount;
                    NoteTextBox.Text = selectedNote;
                    VoucherTextBox.Text = selectedVoucher;


                }
            }

            // 調用 JavaScript 腳本來調整滾動位置
            ScriptManager.RegisterStartupScript(this, GetType(), "scrollScript", "AdjustScrollPosition();", true);
        }
    }
}