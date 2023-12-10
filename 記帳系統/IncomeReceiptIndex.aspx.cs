using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class IncomeReceiptIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 檢查 Session 是否包含值
                if (Session["StartTextBoxValue"] != null)
                {
                    StartTextBox.Text = Session["StartTextBoxValue"].ToString();
                }

                if (Session["EndTextBoxValue"] != null)
                {
                    EndTextBox.Text = Session["EndTextBoxValue"].ToString();
                }

                if (Session["OnlyOneCheckBoxValue"] != null)
                {
                    bool onlyOneChecked = (bool)Session["OnlyOneCheckBoxValue"];
                    OnlyOneCheckBox.Checked = onlyOneChecked;

                    // 根據 OnlyOneCheckBox 的狀態設定 SelectButton 的樣式
                    if (onlyOneChecked)
                    {
                        SelectButton.CssClass = ""; // 移除任何自訂的 CSS 類別
                        SelectButton.Enabled = true; // 啟用按鈕
                    }
                    else
                    {
                        SelectButton.CssClass = "disabled-button"; // 添加禁用樣式的 CSS 類別
                        SelectButton.Enabled = false; // 禁用按鈕
                    }
                }

                // 清除 Session 中的值
                Session.Remove("StartTextBoxValue");
                Session.Remove("EndTextBoxValue");
                Session.Remove("OnlyOneCheckBoxValue");
            }
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            // 將控制項的值存入 Session
            Session["StartTextBoxValue"] = StartTextBox.Text;
            Session["EndTextBoxValue"] = EndTextBox.Text;
            Session["OnlyOneCheckBoxValue"] = OnlyOneCheckBox.Checked;

            string receipt_guide = "receipt_guide";
            Session["receipt_guide"] = receipt_guide;
            Response.Redirect("MemberQuery.aspx");
        }

        protected void ViewButton_Click(object sender, EventArgs e)
        {
            if (Session["SelectedName"] != null)
            {
                // 取得選擇的資料
                string selectedUser_Id = Session["SelectedUser_Id"].ToString();
                string selectedName = Session["SelectedName"].ToString();

                //判斷是否為空值
                selectedUser_Id = (selectedUser_Id == "&nbsp;") ? string.Empty : selectedUser_Id;
                selectedName = (selectedName == "&nbsp;") ? string.Empty : selectedName;

                // 清除 Session 中的選擇資料
                Session.Remove("SelectedUser_Id");
                Session.Remove("SelectedName");

                Response.Write(selectedUser_Id);
                Response.Write(StartTextBox.Text);
                Response.Write(EndTextBox.Text);

            }
        }
    }
}