using Newtonsoft.Json.Linq;
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
            if (!IsPostBack)
            {
                // 檢查 Session 中是否存在選擇的資料
                if (Session["SelectedName"] != null)
                {
                    // 取得選擇的資料
                    string selectedUser_Id = Session["SelectedUser_Id"].ToString();
                    string selectedName = Session["SelectedName"].ToString();
                    string selectedGroup = Session["SelectedGroup"].ToString();
                    string selectedPhone1 = Session["SelectedPhone1"].ToString();
                    string selectedPhone2 = Session["SelectedPhone2"].ToString();
                    string selectedPostalCode = Session["SelectedPostalCode"].ToString();
                    string selectedAddress = Session["SelectedAddress"].ToString();
                    string selectedNote = Session["SelectedNote"].ToString();
                    string idValue = Session["SelectedidValue"].ToString();

                    //判斷是否為空值
                    selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                    selectedGroup = (selectedGroup == "&nbsp;") ? string.Empty : selectedGroup;
                    selectedPhone1 = (selectedPhone1 == "&nbsp;") ? string.Empty : selectedPhone1;
                    selectedPhone2 = (selectedPhone2 == "&nbsp;") ? string.Empty : selectedPhone2;
                    selectedPostalCode = (selectedPostalCode == "&nbsp;") ? string.Empty : selectedPostalCode;
                    selectedAddress = (selectedAddress == "&nbsp;") ? string.Empty : selectedAddress;
                    selectedNote = (selectedNote == "&nbsp;") ? string.Empty : selectedNote;
                    idValue = (idValue == "&nbsp;") ? string.Empty : idValue;

                    // 將資料顯示在對應的TextBox中
                    IDTextBox.Text = selectedUser_Id;
                    NameTextBox.Text = selectedName;
                    GroupDropDownList.Text = selectedGroup;
                    Phone1TextBox.Text = selectedPhone1;
                    Phone2TextBox.Text = selectedPhone2;
                    PostalCodeTextBox.Text = selectedPostalCode;
                    AddressTextBox.Text = selectedAddress;
                    NoteTextBox.Text = selectedNote;
                    KeyIDTextBox.Text = idValue;

                    // 清除 Session 中的選擇資料
                    Session.Remove("SelectedUser_Id");
                    Session.Remove("SelectedName");
                    Session.Remove("SelectedGroup");
                    Session.Remove("selectedPhone1");
                    Session.Remove("selectedPhone2");
                    Session.Remove("selectedPostalCode");
                    Session.Remove("selectedAddress");
                    Session.Remove("selectedNote");
                    Session.Remove("SelectedidValue");
                }
            }
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
            /*
            // 查詢並將結果存在 Session 
            string name = NameTextBox.Text;
            string phone1 = Phone1TextBox.Text;
            string phone2 = Phone2TextBox.Text;
            string postalCode = PostalCodeTextBox.Text;
            string address = AddressTextBox.Text;
            string note = NoteTextBox.Text;

            // 使用查詢條件查詢資料
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con.Open();
                string selectQuery = "SELECT * FROM [User_Form] WHERE [Name] LIKE @Name AND [Phone1] LIKE @Phone1 AND [Phone2] LIKE @Phone2 AND [Postal_Code] LIKE @PostalCode AND [Address] LIKE @Address AND [note] LIKE @Note";
                SqlCommand cmd = new SqlCommand(selectQuery, con);
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                cmd.Parameters.AddWithValue("@Phone1", "%" + phone1 + "%");
                cmd.Parameters.AddWithValue("@Phone2", "%" + phone2 + "%");
                cmd.Parameters.AddWithValue("@PostalCode", "%" + postalCode + "%");
                cmd.Parameters.AddWithValue("@Address", "%" + address + "%");
                cmd.Parameters.AddWithValue("@Note", "%" + note + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 結果保存到 Session
                Session["SearchResults"] = dt;
            }*/

            Response.Redirect("MemberQuery.aspx");
        }

        protected void IndexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}