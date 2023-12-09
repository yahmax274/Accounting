<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberSheet.aspx.cs" Inherits="記帳系統.MemberSheet" %>

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
            opacity: 100;
            width: 1px;
            height: 1px;
        }

        .selectedRow {
            background-color: #FFFFCC;
        }  
    </style>
    <script>
        function sendMessageToParent(selectedUser_Id, selectedName, selectedGroup, selectedPhone1, selectedPhone2, selectedPostal_Code, selectedAddress, selectedNote, idValue) {
            // 將資料包裝成物件
            var messageData = {
                User_Id: selectedUser_Id,
                Name: selectedName,
                Group: selectedGroup,
                Phone1: selectedPhone1,
                Phone2: selectedPhone2,
                Postal_Code: selectedPostal_Code,
                Address: selectedAddress,
                Note: selectedNote,
                idValue: idValue
            };

            // 使用 window.parent.postMessage 將資料傳遞給主頁面
            window.parent.postMessage(messageData, '*');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="selectButton" HeaderText="選取" SelectText="□" >
                    <ItemStyle CssClass="selectButton"></ItemStyle>
                    </asp:CommandField>
                    <asp:BoundField DataField="User_Id" HeaderText="代號" SortExpression="User_Id" />
                    <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" >
                    <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Group" HeaderText="組別" SortExpression="Group" >
                    <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Phone1" HeaderText="連絡電話1" SortExpression="Phone1" />
                    <asp:BoundField DataField="Phone2" HeaderText="連絡電話2" SortExpression="Phone2" />
                    <asp:BoundField DataField="Postal_Code" HeaderText="郵遞區號" SortExpression="Postal_Code" >
                    <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Address" HeaderText="地址" SortExpression="Address" >
                    <ItemStyle Width="500px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="note" HeaderText="備註" SortExpression="note" />
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
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
                    <asp:Parameter Name="Group" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
