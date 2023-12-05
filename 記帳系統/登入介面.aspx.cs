using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Enter_Button_Click(object sender, EventArgs e)
        {
            if(Account_TextBox.Text=="admin"&Password_TextBox.Text=="admin")
            {
                Response.Redirect("起始頁面.aspx");
            }
            else
            {
                string errorMessage = "帳號或密碼錯誤";
                string styledErrorMessage = $"<p style='font-size: 24px; text-align: center;'>{errorMessage}</p>";
                Response.Write(styledErrorMessage);
            }
        }


    }
}