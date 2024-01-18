using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class BasicInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 在第一次載入頁面時執行
                LoadDataFromDatabase();
            }
        }

        private void LoadDataFromDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            int targetId = 1; // 要查詢的ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Basic_Information WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                // 使用參數化查詢以避免 SQL 注入攻擊
                command.Parameters.AddWithValue("@Id", targetId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // 讀取資料並設定到對應的TextBox
                    GroupTextBox.Text = reader["Group"].ToString();
                    AddressTextBox.Text = reader["Address"].ToString();
                    RegisterTextBox.Text = reader["Register"].ToString();
                    PrincipalTextBox.Text = reader["Principal"].ToString();
                    PhoneTextBox.Text = reader["Phone"].ToString();
                    AdministrationTextBox.Text = reader["Administration"].ToString();
                    ManagerTextBox.Text = reader["Manager"].ToString();
                }

                reader.Close();
            }
        }
        protected void ReviseButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            int targetId = 1; // 要修改的ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Basic_Information SET [Group] = @Group, Address = @Address, Register = @Register, Principal = @Principal, Phone = @Phone, Administration = @Administration, Manager = @Manager WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                // 取得TextBox中的值
                string groupValue = GroupTextBox.Text;
                string addressValue = AddressTextBox.Text;
                string registerValue = RegisterTextBox.Text;
                string principalValue = PrincipalTextBox.Text;
                string phoneValue = PhoneTextBox.Text;
                string administrationValue = AdministrationTextBox.Text;
                string ManagerValue = ManagerTextBox.Text;

                // 使用參數化查詢以避免 SQL 注入攻擊
                command.Parameters.AddWithValue("@Group", groupValue);
                command.Parameters.AddWithValue("@Address", addressValue);
                command.Parameters.AddWithValue("@Register", registerValue);
                command.Parameters.AddWithValue("@Principal", principalValue);
                command.Parameters.AddWithValue("@Phone", phoneValue);
                command.Parameters.AddWithValue("@Administration", administrationValue);
                command.Parameters.AddWithValue("@Manager", ManagerValue);
                command.Parameters.AddWithValue("@Id", targetId);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // 成功更新資料庫
                    //Response.Write("資料已更新");
                    Response.Redirect("PrintDemand.aspx");
                }
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintDemand.aspx");
        }
    }
}