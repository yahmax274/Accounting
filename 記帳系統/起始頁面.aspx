﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="起始頁面.aspx.cs" Inherits="記帳系統.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<head>
    <style>
        .centered-text {
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="centered-text ">
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="奉獻者基本資料維護" Height="200px" OnClick="Button1_Click" Width="300px" />
            <br />
            <asp:Button ID="Button3" runat="server" Text="收入登記表" Height="200px" Width="300px" OnClick="Button3_Click" />
        </div>
    </form>
    </div>
</body>
</html>