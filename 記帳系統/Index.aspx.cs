﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberRegisterIndex.aspx");
        }
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncomeRegistrationIndex.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncomeReceiptIndex.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("DonationReceipt.aspx");
        }
    }
}