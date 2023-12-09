<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberQuery.aspx.cs" Inherits="記帳系統.MemberQuery" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        /* CSS樣式，用於設定方框樣式 */
        .container {
            width: 55%; /* 設定方框寬度 */
            margin: 0 auto; /* 讓方框置中 */
            border: 1px solid #ccc; /* 設定方框邊框 */
            padding: 20px; /* 設定方框內間距 */
        }

        /* CSS樣式，用於設定GridView樣式 */
        .gridview-container {
            margin-bottom: 20px; /* 設定GridView底部間距 */
            height: 300px; /* 設定GridView容器高度 */
            overflow-y: auto; /* 如果內容超過高度，啟用垂直滾動條 */
            width: 100%; /* 讓 gridview-container 的寬度與 container 一致 */
        }
        /* CSS樣式，用於設定GridView1樣式 */
        #<%= GridView1.ClientID %> {
            width: 100%; /* 設定GridView1寬度為100% */
        }
        /* CSS樣式，用於設定其他控制項樣式 */
        .controls-container {
            margin-top: 20px; /* 設定其他控制項頂部間距 */
            width: 100%;
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
        function ScrollToTop() {
            // 獲取 gridview-container 元素
            var gridViewContainer = document.querySelector(".gridview-container");

            if (gridViewContainer) {
                // 將滾動位置設置為最上方
                gridViewContainer.scrollTop = 0;
            }
        }
        function ScrollToBottom() {
            // 獲取 gridview-container 元素
            var gridViewContainer = document.querySelector(".gridview-container");

            if (gridViewContainer) {
                // 將滾動位置設置為最底部
                gridViewContainer.scrollTop = gridViewContainer.scrollHeight;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3>奉獻者資料查詢</h3>
            <!-- 上部分放置 GridView -->
            <div class="gridview-container">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" HeaderText="選擇" SelectText="□" />
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
                        <asp:BoundField DataField="User_Id" HeaderText="代號" SortExpression="User_Id" />
                        <asp:BoundField DataField="Group" HeaderText="組別" SortExpression="Group" />
                        <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        <asp:BoundField DataField="Phone1" HeaderText="連絡電話1" SortExpression="Phone1" />
                        <asp:BoundField DataField="Phone2" HeaderText="連絡電話2" SortExpression="Phone2" />
                        <asp:BoundField DataField="Postal_Code" HeaderText="郵遞區號" SortExpression="Postal_Code" />
                        <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address" />
                        <asp:BoundField DataField="note" HeaderText="備註" SortExpression="note" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [User_Form] WHERE [Id] = @Id" InsertCommand="INSERT INTO [User_Form] ([Name], [Phone1], [Phone2], [Postal_Code], [Address], [note], [User_Id], [Group]) VALUES (@Name, @Phone1, @Phone2, @Postal_Code, @Address, @note, @User_Id, @Group)" SelectCommand="SELECT * FROM [User_Form]" UpdateCommand="UPDATE [User_Form] SET [Name] = @Name, [Phone1] = @Phone1, [Phone2] = @Phone2, [Postal_Code] = @Postal_Code, [Address] = @Address, [note] = @note, [User_Id] = @User_Id, [Group] = @Group WHERE [Id] = @Id">
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
                        <asp:Parameter Name="User_Id" Type="String" />
                        <asp:Parameter Name="Group" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Phone1" Type="String" />
                        <asp:Parameter Name="Phone2" Type="String" />
                        <asp:Parameter Name="Postal_Code" Type="String" />
                        <asp:Parameter Name="Address" Type="String" />
                        <asp:Parameter Name="note" Type="String" />
                        <asp:Parameter Name="User_Id" Type="String" />
                        <asp:Parameter Name="Group" Type="String" />
                        <asp:Parameter Name="Id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>

            <!-- 下部分放置其他控制項 -->
            <div class="controls-container">
                <asp:Label ID="IDLabel" runat="server" Text="代號"></asp:Label>
                <asp:TextBox ID="IDTextBox" runat="server"></asp:TextBox>
                <!-- 其他控制項的設定... -->

                <asp:Label ID="NameLabel" runat="server" Text="姓名"></asp:Label>
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                <!-- 其他控制項的設定... -->

                <asp:Label ID="GroupLabel" runat="server" Text="組別"></asp:Label>
                <asp:DropDownList ID="GroupDropDownList" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>A_教會組</asp:ListItem>
                    <asp:ListItem>B_XX組</asp:ListItem>
                    <asp:ListItem>C_XX組</asp:ListItem>
                    <asp:ListItem>D_XX組</asp:ListItem>
                    <asp:ListItem>E_XX組</asp:ListItem>
                    <asp:ListItem>T_測試</asp:ListItem>
                </asp:DropDownList>
                <br />

                <asp:Label ID="PhoneLabel" runat="server" Text="電話"></asp:Label>
                <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
                <!-- 其他控制項的設定... -->

                <asp:Label ID="AddressLabel" runat="server" Text="地址"></asp:Label>
                <asp:TextBox ID="AddressTextBox" runat="server"></asp:TextBox>
                <!-- 其他控制項的設定... -->

                <br />

                <asp:Button ID="IDButton" runat="server" Text="代號查詢" OnClick="IDButton_Click" />
                <asp:Button ID="NameButton" runat="server" Text="姓名查詢" OnClick="NameButton_Click" />
                <!-- 其他按鈕的設定... -->
                <asp:Button ID="PhoneButton" runat="server" Text="電話查詢" OnClick="PhoneButton_Click" />
                <asp:Button ID="AddressButton" runat="server" Text="地址查詢" OnClick="AddressButton_Click" />
                <asp:Button ID="GroupButton" runat="server" Text="組別查詢" OnClick="GroupButton_Click" />
                <asp:Button ID="TopButton" runat="server" Text="第一筆" OnClick="TopButton_Click" />
                <asp:Button ID="BottomButton" runat="server" Text="最末筆" OnClick="BottomButton_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="SelectButton" runat="server" Text="選擇" OnClick="SelectButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>