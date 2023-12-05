<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="奉獻者基本資料維護頁面.aspx.cs" Inherits="記帳系統.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 73px;
            width: 682px;
        }
        .selectButton {
        opacity: 0;
        width: 1px; /* 設定按鈕的寬度 */
        height: 1px; /* 設定按鈕的高度 */
        }
        .selectedRow {
        background-color: #FFFFCC; /* 這裡可以設定你想要的背景色 */
        }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" EnableTheming="True" AllowPaging="true" PageSize="10"  Height="16px" style="margin-right: 0px" Width="745px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="selectButton" >
                <ItemStyle CssClass="selectButton"></ItemStyle>
                </asp:CommandField>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Phone1" HeaderText="Phone1" SortExpression="Phone1" />
                <asp:BoundField DataField="Phone2" HeaderText="Phone2" SortExpression="Phone2" />
                <asp:BoundField DataField="Postal_Code" HeaderText="Postal_Code" SortExpression="Postal_Code" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="note" HeaderText="note" SortExpression="note" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [User_Form] WHERE [Id] = @Id" InsertCommand="INSERT INTO [User_Form] ([Name], [Phone1], [Phone2], [Postal_Code], [Address], [note]) VALUES (@Name, @Phone1, @Phone2, @Postal_Code, @Address, @note)" SelectCommand="SELECT * FROM [User_Form]" UpdateCommand="UPDATE [User_Form] SET [Name] = @Name, [Phone1] = @Phone1, [Phone2] = @Phone2, [Postal_Code] = @Postal_Code, [Address] = @Address, [note] = @note WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Phone1" Type="String" />
                <asp:Parameter Name="Phone2" Type="String" />
                <asp:Parameter Name="Postal_Code" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="note" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Phone1" Type="String" />
                <asp:Parameter Name="Phone2" Type="String" />
                <asp:Parameter Name="Postal_Code" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="note" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <div>
        </div>
        <p>
            <asp:Label ID="Label1" runat="server" Text="姓名:"></asp:Label>

<asp:TextBox ID="NameTextBox" runat="server" />
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="連絡電話1:"></asp:Label>
<asp:TextBox ID="Phone1TextBox" runat="server" />
            <asp:Label ID="Label3" runat="server" Text="連絡電話2:"></asp:Label>
<asp:TextBox ID="Phone2TextBox" runat="server" />
        </p>
        <p>
            <asp:Label ID="Label4" runat="server" Text="郵遞區號:"></asp:Label>
<asp:TextBox ID="PostalCodeTextBox" runat="server" OnTextChanged="PostalCodeTextBox_TextChanged" />
        </p>
        <p>
            <asp:Label ID="Label5" runat="server" Text="地址:"></asp:Label>
<asp:TextBox ID="AddressTextBox" runat="server" Width="1390px" />
        </p>
        <p>
            <asp:Label ID="Label6" runat="server" Text="備註:"></asp:Label>
<asp:TextBox ID="NoteTextBox" runat="server" Height="62px" Width="1149px" />

<asp:Button ID="InsertButton" runat="server" Text="新增" OnClick="InsertButton_Click" Height="47px" Width="230px" />
<asp:Button ID="Button1" runat="server" Text="查詢" OnClick="SearchButton_Click" />

            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="回首頁" />

            <asp:Button ID="ReviseButton" runat="server" OnClick="ReviseButton_Click1" Text="修改" />
            <asp:Button ID="DeleteButton" runat="server"  OnClientClick="return confirm('確定要刪除這筆資料嗎？');" OnClick="DeleteButton_Click1" Text="刪除" />

        </p>
    </form>
</body>
</html>
