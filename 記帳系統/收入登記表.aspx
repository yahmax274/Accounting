<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="收入登記表.aspx.cs" Inherits="記帳系統.收入登記表" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        #form1 {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .labelTextBoxGroup {
            display: flex;
            margin-bottom: 10px;
        }

        .labelTextBoxGroup label {
            width: 100px;
            text-align: right;
            margin-right: 10px;
        }

        .labelTextBoxGroup input[type="text"] {
            width: 150px;
        }

        .labelTextBoxGroup:nth-child(2),
        .labelTextBoxGroup:nth-child(4),
        .labelTextBoxGroup:nth-child(6) {
            margin-top: 10px;
        }

        .buttons {
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }

        .buttons button {
            margin: 0 10px;
        }

        #GridView1 {
            margin-top: 20px;
            max-height: 300px; /* 設置GridView1的最大高度，超過這個高度將顯示垂直卷軸 */
            overflow-y: auto; /* 顯示垂直卷軸，僅在內容超出高度時顯示 */
            overflow-x: hidden; /* 隱藏水平卷軸，避免水平卷軸出現 */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="目次" DataSourceID="SqlDataSource2" AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="目次" HeaderText="目次" InsertVisible="False" ReadOnly="True" SortExpression="目次" />
                    <asp:BoundField DataField="憑證號碼" HeaderText="憑證號碼" SortExpression="憑證號碼" />
                    <asp:BoundField DataField="奉獻項目" HeaderText="奉獻項目" SortExpression="奉獻項目" />
                    <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
                    <asp:BoundField DataField="User_ID" HeaderText="代號" SortExpression="User_ID" />
                    <asp:BoundField DataField="日期" HeaderText="日期" SortExpression="日期" />
                    <asp:BoundField DataField="收據" HeaderText="收據" SortExpression="收據" />
                    <asp:BoundField DataField="部門" HeaderText="部門" SortExpression="部門" />
                    <asp:BoundField DataField="傳票號碼" HeaderText="傳票號碼" SortExpression="傳票號碼" />
                    <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額" />
                    <asp:BoundField DataField="摘要" HeaderText="摘要" SortExpression="摘要" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [New_Income_Form] WHERE [目次] = @目次" InsertCommand="INSERT INTO [New_Income_Form] ([憑證號碼], [奉獻項目], [姓名], [User_ID], [日期], [收據], [部門], [傳票號碼], [金額], [摘要]) VALUES (@憑證號碼, @奉獻項目, @姓名, @User_ID, @日期, @收據, @部門, @傳票號碼, @金額, @摘要)" SelectCommand="SELECT * FROM [New_Income_Form]" UpdateCommand="UPDATE [New_Income_Form] SET [憑證號碼] = @憑證號碼, [奉獻項目] = @奉獻項目, [姓名] = @姓名, [User_ID] = @User_ID, [日期] = @日期, [收據] = @收據, [部門] = @部門, [傳票號碼] = @傳票號碼, [金額] = @金額, [摘要] = @摘要 WHERE [目次] = @目次">
                <DeleteParameters>
                    <asp:Parameter Name="目次" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="憑證號碼" Type="Int32" />
                    <asp:Parameter Name="奉獻項目" Type="String" />
                    <asp:Parameter Name="姓名" Type="String" />
                    <asp:Parameter Name="User_ID" Type="Int32" />
                    <asp:Parameter Name="日期" Type="String" />
                    <asp:Parameter Name="收據" Type="String" />
                    <asp:Parameter Name="部門" Type="String" />
                    <asp:Parameter Name="傳票號碼" Type="String" />
                    <asp:Parameter Name="金額" Type="String" />
                    <asp:Parameter Name="摘要" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="憑證號碼" Type="Int32" />
                    <asp:Parameter Name="奉獻項目" Type="String" />
                    <asp:Parameter Name="姓名" Type="String" />
                    <asp:Parameter Name="User_ID" Type="Int32" />
                    <asp:Parameter Name="日期" Type="String" />
                    <asp:Parameter Name="收據" Type="String" />
                    <asp:Parameter Name="部門" Type="String" />
                    <asp:Parameter Name="傳票號碼" Type="String" />
                    <asp:Parameter Name="金額" Type="String" />
                    <asp:Parameter Name="摘要" Type="String" />
                    <asp:Parameter Name="目次" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
          <div class="labelTextBoxGroup">
            <asp:Label ID="LabelDonor" runat="server" Text="奉獻者"></asp:Label>
            <asp:TextBox ID="TextBoxDonor" runat="server" ReadOnly="True" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
            <asp:Button ID="UserSelectButton" runat="server" EnableTheming="False" Height="30px" OnClick="Button4_Click" Text="..." Width="25px" />
            <asp:Label ID="IDLabel" runat="server" Text="編號"></asp:Label>
            <asp:TextBox ID="IDTextBox" runat="server" Width="95px" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="LabelDepartment" runat="server" Text="部門"></asp:Label>
            <asp:TextBox ID="TextBoxDepartment" runat="server"></asp:TextBox>
        </div>

        <div class="labelTextBoxGroup">
            <asp:Label ID="LabelContribution" runat="server" Text="奉獻項目"></asp:Label>
            <asp:DropDownList ID="ContributionDropDownList" runat="server">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>什一奉獻收入</asp:ListItem>
                <asp:ListItem>會眾奉獻收入</asp:ListItem>
                <asp:ListItem>感恩奉獻收入</asp:ListItem>
                <asp:ListItem>宣教奉獻收入</asp:ListItem>
                <asp:ListItem>建堂奉獻收入</asp:ListItem>
                <asp:ListItem>愛宴奉獻收入</asp:ListItem>
                <asp:ListItem Value="愛心奉獻收入"></asp:ListItem>
                <asp:ListItem>其他奉獻收入</asp:ListItem>
                <asp:ListItem>同工生活奉獻</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="LabelDate" runat="server" Text="日期"></asp:Label>
            <asp:TextBox ID="TextBoxDate" runat="server" TextMode="Date"></asp:TextBox>
        </div>

        <div class="labelTextBoxGroup">
            <asp:Label ID="LabelReceipt" runat="server" Text="收據"></asp:Label>
            <asp:TextBox ID="TextBoxReceipt" runat="server"></asp:TextBox>
            <asp:Label ID="LabelAmount" runat="server" Text="金額"></asp:Label>
            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
        </div>

        <div class="labelTextBoxGroup">
            <asp:Label ID="LabelVoucher" runat="server" Text="傳票號碼"></asp:Label>
            <asp:TextBox ID="TextBoxVoucher" runat="server"></asp:TextBox>
            <asp:Label ID="LabelSummary" runat="server" Text="摘要"></asp:Label>
            <asp:TextBox ID="TextBoxSummary" runat="server"></asp:TextBox>
        </div>

        <div class="buttons">
            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" Text="新增" />
            <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="查詢" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Style="height: 41px" Text="回首頁" />
        </div>

        </div>
    </form>
</body>
</html>
