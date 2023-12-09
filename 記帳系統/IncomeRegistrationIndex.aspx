<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeRegistrationIndex.aspx.cs" Inherits="記帳系統.IncomeRegistrationIndex" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        /* CSS樣式，用於設定方框樣式 */
        .container {
            width: 80%; /* 設定方框寬度 */
            margin: 0 auto; /* 讓方框置中 */
            border: 1px solid #ccc; /* 設定方框邊框 */
            padding: 20px; /* 設定方框內間距 */
            display: flex; /* 使用 Flexbox 進行排列 */
        }

        /* CSS樣式，用於設定左側區域樣式 */
        .left-side {
            flex: 5; /* 左側區域佔據 Flexbox 中的比例 */
            margin-right: 20px; /* 右側間距 */
            max-height: 300px; /* 設定最大高度為300像素 */
            overflow-y: auto; /* 超過最大高度時顯示垂直滾動條 */
            margin-bottom: 20px; /* 設定GridView底部間距 */
        }

        /* CSS樣式，用於設定GridView樣式 */
        .gridview-container {
            margin-bottom: 20px; /* 設定GridView底部間距 */
        }

        /* CSS樣式，用於設定右側區域樣式 */
        .right-side {
            flex: 4; /* 右側區域佔據 Flexbox 中的比例 */
        }
        .form-row {
            display: flex;
            align-items: center; /* 將內容在每行中垂直居中對齊 */
            margin-bottom: 10px;
        }
        .left-column {
            flex: 5; /* 兩個欄位平均佔據 .form-row 的寬度 */
            padding: 5px; /* 可選，添加內邊距以改進外觀 */
        }
        .right-column {
            flex: 5; /* 兩個欄位平均佔據 .form-row 的寬度 */
            padding: 5px; /* 可選，添加內邊距以改進外觀 */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container mt-3">
        <div class="left-side">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered gridview-container" AutoGenerateColumns="False" DataKeyNames="目次" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="目次" HeaderText="目次" InsertVisible="False" ReadOnly="True" SortExpression="目次" Visible="False" />
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="User_ID" HeaderText="代號" SortExpression="User_ID" />
                    <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
                    <asp:BoundField DataField="日期" HeaderText="日期" SortExpression="日期" />
                    <asp:BoundField DataField="憑證號碼" HeaderText="憑證號碼" SortExpression="憑證號碼" />
                    <asp:BoundField DataField="奉獻項目" HeaderText="奉獻項目" SortExpression="奉獻項目" />
                    <asp:BoundField DataField="收據" HeaderText="收據" SortExpression="收據" />
                    <asp:BoundField DataField="部門" HeaderText="部門" SortExpression="部門" />
                    <asp:BoundField DataField="傳票號碼" HeaderText="傳票號碼" SortExpression="傳票號碼" />
                    <asp:BoundField DataField="金額" HeaderText="金額" SortExpression="金額" />
                    <asp:BoundField DataField="摘要" HeaderText="摘要" SortExpression="摘要" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [New_Income_Form] WHERE [目次] = @目次" InsertCommand="INSERT INTO [New_Income_Form] ([憑證號碼], [奉獻項目], [姓名], [User_ID], [日期], [收據], [部門], [傳票號碼], [金額], [摘要]) VALUES (@憑證號碼, @奉獻項目, @姓名, @User_ID, @日期, @收據, @部門, @傳票號碼, @金額, @摘要)" SelectCommand="SELECT * FROM [New_Income_Form]" UpdateCommand="UPDATE [New_Income_Form] SET [憑證號碼] = @憑證號碼, [奉獻項目] = @奉獻項目, [姓名] = @姓名, [User_ID] = @User_ID, [日期] = @日期, [收據] = @收據, [部門] = @部門, [傳票號碼] = @傳票號碼, [金額] = @金額, [摘要] = @摘要 WHERE [目次] = @目次">
                <DeleteParameters>
                    <asp:Parameter Name="目次" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="憑證號碼" Type="String" />
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
                    <asp:Parameter Name="憑證號碼" Type="String" />
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
        </div>
        <div class="right-side">
            <div class="form-row">
                <div class="left-column">
                    <asp:Label ID="Label1" runat="server" Text="憑證號碼"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>
                <div class="right-column">
                    <asp:Label ID="Label2" runat="server" Text="奉獻項目"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>什一奉獻收入</asp:ListItem>
                        <asp:ListItem>會眾奉獻收入</asp:ListItem>
                        <asp:ListItem>感恩奉獻收入</asp:ListItem>
                        <asp:ListItem>宣教奉獻收入</asp:ListItem>
                        <asp:ListItem>建堂奉獻收入</asp:ListItem>
                        <asp:ListItem>愛宴奉獻收入</asp:ListItem>
                        <asp:ListItem>愛心奉獻收入</asp:ListItem>
                        <asp:ListItem>其他奉獻收入</asp:ListItem>
                        <asp:ListItem>同工生活奉獻</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="left-column">
                    <asp:Label ID="NameLabel" runat="server" Text="奉獻者"></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="100px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="..." />
                </div>
                <div class="right-column">
                    <asp:Label ID="DateLabel" runat="server" Text="奉獻日期"></asp:Label>
                    <asp:TextBox ID="DateTextBox" runat="server" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="left-column">
                    <asp:Label ID="IDLabel" runat="server" Text="代號"></asp:Label>
                    <asp:TextBox ID="IDTextBox" runat="server"></asp:TextBox>
                </div>
                <div class="right-column">
                    <asp:Label ID="GroupLabel" runat="server" Text="組別"></asp:Label>
                    <asp:TextBox ID="GroupTextBox" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                 <div class="left-column">
                    <asp:Label ID="Label5" runat="server" Text="收據"></asp:Label>
                     <asp:DropDownList ID="DropDownList3" runat="server" Width="150px">
                         <asp:ListItem></asp:ListItem>
                         <asp:ListItem>A</asp:ListItem>
                         <asp:ListItem>B</asp:ListItem>
                         <asp:ListItem>C</asp:ListItem>
                         <asp:ListItem>Y</asp:ListItem>
                     </asp:DropDownList>
                </div>
                <div class="right-column">
                    <asp:Label ID="DepLabel" runat="server" Text="部門"></asp:Label>
                    <asp:DropDownList ID="DepDropDownList" runat="server" Width="150px">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-row">
                <div class="left-column">
                    <asp:Label ID="Label7" runat="server" Text="專案名稱"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </div>
                <div class="right-column">
                    <asp:Label ID="Label8" runat="server" Text="奉獻金額"></asp:Label>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="left-column">
                    <asp:Label ID="Label9" runat="server" Text="摘要"></asp:Label>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </div>
                <div class="right-column">
                    <asp:Label ID="Label10" runat="server" Text="傳票號碼"></asp:Label>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
