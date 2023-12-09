using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 記帳系統
{
    public partial class MemberSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            // 確認有選定行
            if (index >= 0)
            {
                // 取得選定行的DataKeys集合
                DataKey dataKeys = GridView1.DataKeys[index];

                // 確認DataKeys不為空並且包含Id
                if (dataKeys != null && dataKeys.Values["Id"] != null)
                {
                    // 讀取Id的值
                    string idValue = dataKeys.Values["Id"].ToString();

                    // 取得選取行的索引
                    int selectedIndex = GridView1.SelectedRow.RowIndex;

                    // 取得選取行的資料
                    string selectedUser_Id = GridView1.Rows[selectedIndex].Cells[1].Text; // 取得 User_Id 的值
                    string selectedName = GridView1.Rows[selectedIndex].Cells[2].Text; // 取得 Name 的值
                    string selectedGroup = GridView1.Rows[selectedIndex].Cells[3].Text; // 取得 Group 的值
                    string selectedPhone1 = GridView1.Rows[selectedIndex].Cells[4].Text; // 取得 Phone1 的值
                    string selectedPhone2 = GridView1.Rows[selectedIndex].Cells[5].Text; //取得 Phone2 的值
                    string selectedPostal_Code = GridView1.Rows[selectedIndex].Cells[6].Text; // 取得 Postal_Code 的值
                    string selectedAddress = GridView1.Rows[selectedIndex].Cells[7].Text; // 取得 Address 的值
                    string selectedNote = GridView1.Rows[selectedIndex].Cells[8].Text; // 取得 note 的值

                    // 使用 ScriptManager 註冊 JavaScript 以將資料傳遞給主頁面
                    ScriptManager.RegisterStartupScript(this, GetType(), "SendMessageToParent", "sendMessageToParent('" + selectedUser_Id + "','" + selectedName + "','" + selectedGroup + "','" + selectedPhone1 + "','" + selectedPhone2 + "','" + selectedPostal_Code + "','" + selectedAddress + "','" + selectedNote + "','" + idValue + "');", true);

                }

            }
            /*
            // 取得選取行的索引
            int selectedIndex = GridView1.SelectedRow.RowIndex;

            // 取得選取行的資料
            string selectedUser_Id = GridView1.Rows[selectedIndex].Cells[1].Text; // 取得 User_Id 的值
            string selectedName = GridView1.Rows[selectedIndex].Cells[2].Text; // 取得 Name 的值
            string selectedGroup = GridView1.Rows[selectedIndex].Cells[3].Text; // 取得 Group 的值
            string selectedPhone1 = GridView1.Rows[selectedIndex].Cells[4].Text; // 取得 Phone1 的值
            string selectedPhone2 = GridView1.Rows[selectedIndex].Cells[5].Text; //取得 Phone2 的值
            string selectedPostal_Code = GridView1.Rows[selectedIndex].Cells[6].Text; // 取得 Postal_Code 的值
            string selectedAddress = GridView1.Rows[selectedIndex].Cells[7].Text; // 取得 Address 的值
            string selectedNote = GridView1.Rows[selectedIndex].Cells[8].Text; // 取得 note 的值*/

            /*
            // 將選取的資料存儲在 Session 中
            Session["SelectedUser_Id"] = selectedUser_Id;
            Session["SelectedName"] = selectedName;
            Session["SelectedGroup"] = selectedGroup;
            Session["SelectedPhone1"] = selectedPhone1;
            Session["SelectedPhone2"] = selectedPhone2;
            Session["SselectedPostal_Code"] = selectedPostal_Code;
            Session["SelectedAddress"] = selectedAddress;
            Session["SelectedNote"] = selectedNote;*/
            
        }
    }

}