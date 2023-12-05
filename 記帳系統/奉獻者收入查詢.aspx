<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="奉獻者收入查詢.aspx.cs" Inherits="記帳系統.奉獻者收入查詢" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" Text="列印" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="回首頁" />
            <asp:Button ID="ReturnButton" runat="server" OnClick="ReturnButton_Click" Text="回上頁" />
        </div>
    </form>
</body>
</html>


