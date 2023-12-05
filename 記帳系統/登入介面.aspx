<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="登入介面.aspx.cs" Inherits="記帳系統.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
</body>
</html>
<head>
    <style>
        .centered-text {
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="centered-text">
        <h1>使用者登入介面</h1>
        <h1>
            <asp:Label ID="Account" runat="server" Text="帳號:"></asp:Label>
            <asp:TextBox ID="Account_TextBox" runat="server"></asp:TextBox>
        </h1>
        <h1>
            <asp:Label ID="Password" runat="server" Text="密碼:"></asp:Label>
            <asp:TextBox ID="Password_TextBox" runat="server" TextMode="Password"></asp:TextBox>
        </h1>
        <h1>
            <asp:Button ID="Enter_Button" runat="server" Text="登入" OnClick="Enter_Button_Click" />
        </h1>
    </div>
</body>
    </form>

