<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="成員查詢.aspx.cs" Inherits="記帳系統.成員查詢" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Page Title</title>
    <style>
        /* 新增樣式，將按鈕固定在頁面最上方 */
        #divButton {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            background-color: #fff; /* 如果需要白色背景，避免遮擋其他內容 */
            z-index: 1000; /* 確保在其他內容之上 */
        }

        /* 下半部分的樣式，設置頂距，以避免被固定的按鈕擋住 */
        #divGridView {
            margin-top: 40px; /* 設定一個適當的頂距，避免擋住固定的按鈕 */
            overflow: auto;
        }

        #container {
            text-align: center;
            max-width: 800px; /* 設定最大寬度 */
            margin: 0 auto; /* 將容器水平居中 */
        }
        .selectButton {
            opacity: 100;
            width: 1px;
            height: 1px;
        }

        .selectedRow {
            background-color: #FFFFCC;
        }  
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <!-- 上半部分 -->
            <div id="divButton" style="height: 6%; border-bottom: 1px solid #000;">
                <!-- 上半部分的內容，這裡放置你的按鈕或其他控制項 -->
                <asp:Label ID="IDLabel" runat="server" Height="40px" Text="代號"></asp:Label>
                <asp:TextBox ID="IDTextBox" runat="server" Height="30px"></asp:TextBox>
                <asp:Label ID="NameLabel" runat="server" Height="40px" Text="姓名"></asp:Label>
                <asp:TextBox ID="NameTextBox" runat="server" Height="30px"></asp:TextBox>
                <asp:Label ID="PhoneLabel" runat="server" Height="40px" Text="電話"></asp:Label>
                <asp:TextBox ID="PhoneTextBox" runat="server" Height="30px"></asp:TextBox>
                <asp:Label ID="AddressLabel" runat="server" Height="40px" Text="地址"></asp:Label>
                <asp:TextBox ID="AddressTextBox" runat="server" Height="30px"></asp:TextBox>
                <asp:Button ID="SearchButton" runat="server" Text="尋找" Height="35px" OnClick="SearchButton_Click" />
                <asp:Button ID="SelectButton" runat="server" Height="35px" Text="選擇" OnClick="SelectButton_Click" />
            </div>

            <!-- 下半部分 -->
            <div id="divGridView" style="height: 70%;"overflow: auto;">
                <!-- 下半部分的內容，這裡放置你的GridView或其他控制項 -->
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField HeaderText="選擇" ShowSelectButton="True" ItemStyle-CssClass="selectButton" SelectText="□" >
                            <ItemStyle CssClass="selectButton"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Id" HeaderText="代號" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        <asp:BoundField DataField="Phone1" HeaderText="聯絡方式1" SortExpression="Phone1" />
                        <asp:BoundField DataField="Phone2" HeaderText="聯絡方式2" SortExpression="Phone2" />
                        <asp:BoundField DataField="Postal_Code" HeaderText="郵遞區號" SortExpression="Postal_Code" />
                        <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [Name], [Phone1], [Phone2], [Postal_Code], [Address] FROM [User_Form]"></asp:SqlDataSource>
            </div>
        </div>
    </form>
</body>
</html>
