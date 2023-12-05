using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace 記帳系統
{
    public partial class WebForm3 : System.Web.UI.Page
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
                    string selectedPhone1 = Session["selectedPhone1"].ToString();
                    string selectedPhone2 = Session["selectedPhone2"].ToString();
                    string selectedPostalCode = Session["selectedPostalCode"].ToString();
                    string selectedAddress = Session["selectedAddress"].ToString();
                    string selectedNote = Session["selectedNote"].ToString();

                    //判斷是否為空值
                    selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                    selectedPhone1 = (selectedPhone1 == "&nbsp;") ? string.Empty : selectedPhone1;
                    selectedPhone2 = (selectedPhone2 == "&nbsp;") ? string.Empty : selectedPhone2;
                    selectedPostalCode = (selectedPostalCode == "&nbsp;") ? string.Empty : selectedPostalCode;
                    selectedAddress = (selectedAddress == "&nbsp;") ? string.Empty : selectedAddress;
                    selectedNote = (selectedNote == "&nbsp;") ? string.Empty : selectedNote;
                    // 將資料顯示在對應的TextBox中
                    NameTextBox.Text = selectedName;
                    Phone1TextBox.Text = selectedPhone1;
                    Phone2TextBox.Text = selectedPhone2;
                    PostalCodeTextBox.Text = selectedPostalCode;
                    AddressTextBox.Text = selectedAddress;
                    NoteTextBox.Text = selectedNote;

                    // 清除 Session 中的選擇資料
                    Session.Remove("SelectedName");
                    Session.Remove("selectedPhone1");
                    Session.Remove("selectedPhone2");
                    Session.Remove("selectedPostalCode");
                    Session.Remove("selectedAddress");
                    Session.Remove("selectedNote");
                }

            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                // 還原其他行的背景色
                row.CssClass = string.Empty;
            }
        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlDataSource1.Insert();
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
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
                string insertQuery = "INSERT INTO [User_Form] ([Name], [Phone1], [Phone2], [Postal_Code], [Address], [note]) VALUES (@Name, @Phone1, @Phone2, @Postal_Code, @Address, @note)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone1", phone1);
                cmd.Parameters.AddWithValue("@Phone2", phone2);
                cmd.Parameters.AddWithValue("@Postal_Code", postalCode);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.ExecuteNonQuery();
            }

            // 清空TextBoxes
            ClearTextBoxes();

            // 重新載入資料或更新UI
            // 這裡你可以更新GridView或其他UI元件，以便顯示最新資料
            GridView1.DataBind(); // 重新绑定GridView;
            // 計算 GridView1 總頁數
            int totalPages = GridView1.PageCount;

            // 設定 GridView1 分頁最後一頁為索引
            GridView1.PageIndex = totalPages - 1;
            GridView1.DataBind();

            // 找到最後一行，設定背景色
            GridViewRow lastRow = GridView1.Rows[GridView1.Rows.Count - 1];
            lastRow.CssClass = "selectedRow"; 
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        protected void NameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void PostalCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
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
            }

            Response.Redirect("奉獻者資料查詢.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("起始頁面.aspx");
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
            // 檢查是否有選取的行
            if (GridView1.SelectedRow != null)
            {
                // 取得選取行的索引
                int selectedIndex = GridView1.SelectedRow.RowIndex;

                // 取得選取行的資料
                string selectedId = GridView1.DataKeys[selectedIndex].Value.ToString(); // 取得 Id 的值
                string selectedName = GridView1.Rows[selectedIndex].Cells[2].Text; // "Name"在第三個列（索引為2）
                string selectedPhone1 = GridView1.Rows[selectedIndex].Cells[3].Text; // "Phone1"在第四個列（索引為3）
                string selectedPhone2 = GridView1.Rows[selectedIndex].Cells[4].Text; // "Phone2"在第五個列（索引為4）
                string selectedPostalCode = GridView1.Rows[selectedIndex].Cells[5].Text; // "Postal_Code"在第六個列（索引為5）
                string selectedAddress = GridView1.Rows[selectedIndex].Cells[6].Text; // "Address"在第七個列（索引為6）
                string selectedNote = GridView1.Rows[selectedIndex].Cells[7].Text; // "note"在第八個列（索引為7）
                //判斷是否為空值
                selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;
                selectedPhone1 = (selectedPhone1 == "&nbsp;") ? string.Empty : selectedPhone1;
                selectedPhone2 = (selectedPhone2 == "&nbsp;") ? string.Empty : selectedPhone2;
                selectedPostalCode = (selectedPostalCode == "&nbsp;") ? string.Empty : selectedPostalCode;
                selectedAddress = (selectedAddress == "&nbsp;") ? string.Empty : selectedAddress;
                selectedNote = (selectedNote == "&nbsp;") ? string.Empty : selectedNote;
                // 將資料顯示在對應的TextBox中
                NameTextBox.Text = selectedName;
                Phone1TextBox.Text = selectedPhone1;
                Phone2TextBox.Text = selectedPhone2;
                PostalCodeTextBox.Text = selectedPostalCode;
                AddressTextBox.Text = selectedAddress;
                NoteTextBox.Text = selectedNote;

            }
        }

        protected void ReviseButton_Click1(object sender, EventArgs e)
        {
            // 檢查是否有選取的行
            if (GridView1.SelectedRow != null)
            {
                // 取得選取行的索引
                int selectedIndex = GridView1.SelectedRow.RowIndex;

                // 取得選取行的資料
                int selectedId = Convert.ToInt32(GridView1.DataKeys[selectedIndex].Value);

                // 輸出 selectedId 值，以確認是否正確
                Debug.WriteLine($"Selected Id: {selectedId}");

                // 取得修改後的資料
                string revisedName = NameTextBox.Text;
                string revisedPhone1 = Phone1TextBox.Text;
                string revisedPhone2 = Phone2TextBox.Text;
                string revisedPostalCode = PostalCodeTextBox.Text;
                string revisedAddress = AddressTextBox.Text;
                string revisedNote = NoteTextBox.Text;

                // 直接更新資料庫中的資料
                ReviseData(selectedId, revisedName, revisedPhone1, revisedPhone2, revisedPostalCode, revisedAddress, revisedNote);
                // 設置選中的行
                GridView1.SelectedIndex = selectedId;
                // 重新綁定 GridView 以顯示更新後的資料
                GridView1.DataBind();
                // 清空TextBoxes
                ClearTextBoxes();

                // 遍歷 GridView 中的每一行，設定選中行的背景色
                foreach (GridViewRow row in GridView1.Rows)
                {
                    // 取得資料繫結的 ID
                    int rowId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);

                    if (rowId == selectedId)
                    {
                        // 設定選中的行的背景色
                        row.CssClass = "selectedRow";
                    }
                    else
                    {
                        // 還原其他行的背景色
                        row.CssClass = string.Empty;
                    }
                }
            }
            if (Session["SelectedId"] != null)
            {
                // 取得修改後的資料
                string SelectedId = Session["SelectedId"].ToString();
                int selectedId = Convert.ToInt32(SelectedId);
                string revisedName = NameTextBox.Text;
                string revisedPhone1 = Phone1TextBox.Text;
                string revisedPhone2 = Phone2TextBox.Text;
                string revisedPostalCode = PostalCodeTextBox.Text;
                string revisedAddress = AddressTextBox.Text;
                string revisedNote = NoteTextBox.Text;

                // 直接更新資料庫中的資料
                ReviseData(selectedId, revisedName, revisedPhone1, revisedPhone2, revisedPostalCode, revisedAddress, revisedNote);
                // 設置選中的行
                GridView1.SelectedIndex = selectedId;


                // 重新綁定 GridView 以顯示更新後的資料
                GridView1.DataBind();

                // 設置當前頁索引，以跳轉到包含修改後資料的頁面
                GridView1.PageIndex = selectedId / GridView1.PageSize;

                // 重新綁定 GridView 以顯示更新後的資料及跳轉到指定頁面
                GridView1.DataBind();

                // 清空TextBoxes
                ClearTextBoxes();

                // 遍歷 GridView 中的每一行，設定選中行的背景色
                foreach (GridViewRow row in GridView1.Rows)
                {
                    // 取得資料繫結的 ID
                    int rowId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);

                    if (rowId == selectedId)
                    {
                        // 設定選中的行的背景色
                        row.CssClass = "selectedRow";
                    }
                    else
                    {
                        // 還原其他行的背景色
                        row.CssClass = string.Empty;
                    }
                }
                Session.Remove("SelectedId");
            }
        }

        private void ReviseData(int id, string name, string phone1, string phone2, string postalCode, string address, string note)
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
            [note] = @note
        WHERE
            [Id] = @Id;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // 添加參數
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone1", phone1);
                    command.Parameters.AddWithValue("@Phone2", phone2);
                    command.Parameters.AddWithValue("@Postal_Code", postalCode);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@note", note);

                    // 打開連接，執行更新語句
                    connection.Open();
                    command.ExecuteNonQuery();
                }
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

        protected void DeleteButton_Click1(object sender, EventArgs e)
        {
            {
                // 檢查是否有選取的行
                if (GridView1.SelectedRow != null)
                {
                    // 取得選取行的索引
                    int selectedIndex = GridView1.SelectedRow.RowIndex;

                    // 取得選取行的資料
                    int selectedId = Convert.ToInt32(GridView1.DataKeys[selectedIndex].Value);

                    // 輸出 selectedId 值，以確認是否正確
                    Debug.WriteLine($"Selected Id for deletion: {selectedId}");

                    // 直接刪除所選行的資料
                    DeleteData(selectedId);

                    // 重新綁定 GridView 以顯示更新後的資料
                    GridView1.DataBind();
                    ClearTextBoxes();
                }
            }
        }
        private void ClearTextBoxes()
        {
            // 清空TextBox
            NameTextBox.Text = "";
            Phone1TextBox.Text = "";
            Phone2TextBox.Text = "";
            PostalCodeTextBox.Text = "";
            AddressTextBox.Text = "";
            NoteTextBox.Text = "";
        }
    }
}