<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DonationReceipt.aspx.cs" Inherits="記帳系統.DonationReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            margin: 0;
        }

        #container {
            width: 600px; /* 調整大方框寬度 */
            border: 1px solid #ccc;
            padding: 20px;
            box-sizing: border-box;
        }
        .gridview-container {
            margin-bottom: 20px; /* 設定GridView底部間距 */
            height: 300px; /* 設定GridView容器高度 */
            overflow-y: auto; /* 如果內容超過高度，啟用垂直滾動條 */
            width: 100%; /* 讓 gridview-container 的寬度與 container 一致 */
        }
        #top, #middle, #bottom {
            margin-bottom: 10px; /* 調整各區塊間距 */
        }
        .selectButton {
            opacity: 100;
            width: 100%;
            height: 100%;
            padding: 0;
            margin: 0;
            font-size: inherit;
        }

        .selectedRow {
            background-color: #FFFFCC;
        } 
    </style>
        <script type="text/javascript">
            function AdjustScrollPosition() {
                // 獲取選中行的位置
                var selectedRow = document.querySelector(".selectedRow");

                if (selectedRow) {
                    // 將滾動位置調整為選中行的位置
                    selectedRow.scrollIntoView({ behavior: 'auto', block: 'center' });
                }
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <h2>奉獻收據</h2>
            <div id="top">
                <!-- 上區塊的內容放在這裡 -->
                <asp:Label ID="Label1" runat="server" Text="奉獻者"></asp:Label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="代號"></asp:Label>
                <asp:TextBox ID="IDTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="SelectButton" runat="server" OnClick="SelectButton_Click" Text="選擇" />
                <asp:TextBox ID="KeyIDTextBox" runat="server" BorderStyle="None" style="display: none;"  ></asp:TextBox>
                <asp:TextBox ID="StartNumberTextBox" runat="server" BorderStyle="None" style="display: none;" ></asp:TextBox>
                <asp:TextBox ID="SelectCountTextBox" runat="server" BorderStyle="None" style="display: none;" ></asp:TextBox>
                <asp:TextBox ID="EndNumberTextBox" runat="server" BorderStyle="None" style="display: none;" ></asp:TextBox>
                <asp:TextBox ID="DataStartTextBox" runat="server" BorderStyle="None" style="display: none;" ></asp:TextBox>
                <asp:TextBox ID="DataEndTextBox" runat="server" BorderStyle="None" style="display: none;" ></asp:TextBox>
            </div>
            <div id="middle">
                <div class="gridview-container">
                    <!-- 中區塊的內容放在這裡 -->
                    <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" DataKeyNames="目次" DataSourceID="SqlDataSource1">

                        <Columns>
                            <asp:CommandField HeaderText="選取" SelectText="□" ShowSelectButton="True" />
                            <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
                            <asp:BoundField DataField="User_ID" HeaderText="代號" SortExpression="User_ID" />
                            <asp:BoundField DataField="憑證號碼" HeaderText="憑證號碼" SortExpression="憑證號碼" />
                            <asp:BoundField DataField="奉獻項目" HeaderText="奉獻項目" SortExpression="奉獻項目" />
                            <asp:BoundField DataField="日期" HeaderText="日期" SortExpression="日期" />
                            <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額" />
                            <asp:BoundField DataField="傳票號碼" HeaderText="傳票號碼" SortExpression="傳票號碼" />
                            <asp:BoundField DataField="目次" HeaderText="目次" SortExpression="目次" InsertVisible="False" ReadOnly="True" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [目次], [憑證號碼], [奉獻項目], [日期], [傳票號碼], [金額], [姓名], [User_ID] FROM [New_Income_Form]"></asp:SqlDataSource>
                </div>
            </div>
            <div id="bottom">
                <!-- 下區塊的內容放在這裡 -->
                <asp:Button ID="AddButton" runat="server" Text="產生收據編號" OnClick="AddButton_Click" OnClientClick="return confirm('確定產生收據編號');"/>
                <asp:Button ID="ClearButton" runat="server" Text="清除所有選項" OnClick="ClearButton_Click" />
                <asp:Button ID="SelectAllButton" runat="server" Text="選取所有選項" OnClick="SelectAllButton_Click" />
                <asp:Button ID="PrintButton" runat="server" Text="列印奉獻收據" OnClick="PrintButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
