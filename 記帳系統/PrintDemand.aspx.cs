using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class PrintDemand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SearchResults"] != null)
            {
                DataTable dt = (DataTable)Session["SearchResults"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Write("查詢結果數量：" + dt.Rows.Count + "<br>");
            }
        }

        protected void IndexButton_Click(object sender, EventArgs e)
        {

        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Session["MoveBack"] = "MoveBack";
            Response.Redirect("IncomeReceiptIndex.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}