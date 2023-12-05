using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("奉獻者基本資料維護頁面.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("奉獻者資料查詢.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("收入登記表.aspx");
        }
    }
}