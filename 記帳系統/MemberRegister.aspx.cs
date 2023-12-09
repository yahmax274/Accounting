using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace 記帳系統
{
    public partial class MemberRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void IDTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            string id = IDTextBox.Text;
            string group = GroupDropDownList.Text;
            string name = NameTextBox.Text;
            string phone1 = Phone1TextBox.Text;
            string phone2 = Phone2TextBox.Text;
            string postalCode = PostalCodeTextBox.Text;
            string address = AddressTextBox.Text;
            string note = NoteTextBox.Text;

            // 使用ADO.NET執行資料庫插入操作
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string insertQuery = "INSERT INTO [User_Form] ([Name], [Phone1], [Phone2], [Postal_Code], [Address], [note],[User_Id],[Group]) VALUES (@Name, @Phone1, @Phone2, @Postal_Code, @Address, @note, @User_Id, @Group)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone1", phone1);
                cmd.Parameters.AddWithValue("@Phone2", phone2);
                cmd.Parameters.AddWithValue("@Postal_Code", postalCode);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@User_Id", id);
                cmd.Parameters.AddWithValue("@Group", group);
                cmd.ExecuteNonQuery();
            }

            // 清空TextBoxes
            ClearTextBoxes();
            ScriptManager.RegisterStartupScript(this, GetType(), "ScrollAndApplyStyle", "scrollToBottom(); applyStyleToLastRow();", true);

        }
        private void ClearTextBoxes()
        {
            // 清空TextBox
            IDTextBox.Text = "";
            GroupDropDownList.Text = "";
            NameTextBox.Text = "";
            Phone1TextBox.Text = "";
            Phone2TextBox.Text = "";
            PostalCodeTextBox.Text = "";
            AddressTextBox.Text = "";
            NoteTextBox.Text = "";
            KeyIDTextBox.Text = "";
        }

        protected void ReviseButton_Click(object sender, EventArgs e)
        {
            string keyid = KeyIDTextBox.Text;
            if (!String.IsNullOrEmpty(keyid))
            {
                string revisedid = IDTextBox.Text;
                string revisedgroup = GroupDropDownList.Text;
                string revisedname = NameTextBox.Text;
                string revisedphone1 = Phone1TextBox.Text;
                string revisedphone2 = Phone2TextBox.Text;
                string revisedpostalCode = PostalCodeTextBox.Text;
                string revisedaddress = AddressTextBox.Text;
                string revisednote = NoteTextBox.Text;
                int selectedId = Convert.ToInt32(keyid);

                // 直接更新資料庫中的資料
                ReviseData(selectedId, revisedname, revisedphone1, revisedphone2, revisedpostalCode, revisedaddress, revisednote, revisedid, revisedgroup);
                ClearTextBoxes();
            }
        }
        private void ReviseData(int id, string name, string phone1, string phone2, string postalCode, string address, string note, string user_id, string group)
        {
            // 請替換這裡的連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL 更新語句
            string updateQuery = @"
        UPDATE [User_Form]
        SET
            [Name] = @Name,
            [Phone1] = @Phone1,
            [Phone2] = @Phone2,
            [Postal_Code] = @Postal_Code,
            [Address] = @Address,
            [note] = @note,
            [User_Id] = @User_Id,
            [Group] = @Group
        WHERE
            [Id] = @Id;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    // 添加參數
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Phone1", phone1);
                    cmd.Parameters.AddWithValue("@Phone2", phone2);
                    cmd.Parameters.AddWithValue("@Postal_Code", postalCode);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@note", note);
                    cmd.Parameters.AddWithValue("@User_Id", user_id);
                    cmd.Parameters.AddWithValue("@Group", group);
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
                ClearTextBoxes();
            }
        }
        private void DeleteData(int id)
        {
            // 請替換這裡的連接字串
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL 刪除語句
            string deleteQuery = "DELETE FROM [User_Form] WHERE [Id] = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    // 添加參數
                    command.Parameters.AddWithValue("@Id", id);

                    // 打開連接，執行刪除語句
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // 獲取查詢條件
            string name = NameTextBox.Text;
            string phone1 = Phone1TextBox.Text;
            string phone2 = Phone2TextBox.Text;
            string postalCode = PostalCodeTextBox.Text;
            string address = AddressTextBox.Text;
            string note = NoteTextBox.Text;

            // 構建查詢字符串
            string queryString = $"?Name={Server.UrlEncode(name)}&Phone1={Server.UrlEncode(phone1)}&Phone2={Server.UrlEncode(phone2)}&PostalCode={Server.UrlEncode(postalCode)}&Address={Server.UrlEncode(address)}&Note={Server.UrlEncode(note)}";

            // 使用 JavaScript 開啟小視窗，將查詢條件傳遞到新的頁面
            string script = $"openPopup('MemberRegisterSearch.aspx{queryString}');";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenPopup", script, true);
        }
    }
}