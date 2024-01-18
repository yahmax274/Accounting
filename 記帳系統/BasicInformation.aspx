<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInformation.aspx.cs" Inherits="記帳系統.BasicInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        /* CSS樣式，用於設定方框樣式 */
        .container {
            width: 90%; /* 設定方框寬度 */
            margin: 0 auto; /* 讓方框置中 */
            border: 1px solid #ccc; /* 設定方框邊框 */
            padding: 20px; /* 設定方框內間距 */
            display: flex; /* 使用 Flexbox 進行排列 */
        }
        .left-container, .right-container {
            flex: 1;
            padding: 10px;
            //border: 1px solid #000; /* 加入邊框 */
        }
        .main-container {
            flex: 1;
            padding: 10px;
        }
        /* CSS樣式，用於設定左側區域樣式 */
        .left-side {
            flex: 1; /* 左側區域佔據 Flexbox 中的比例 */
        }

        /* CSS樣式，用於設定右側區域樣式 */
        .right-side {
            flex: 1; /* 右側區域佔據 Flexbox 中的比例 */
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
        .column2-1 {
            flex: 7; /* 兩個欄位平均佔據 .form-row 的寬度 */
            padding: 5px; /* 可選，添加內邊距以改進外觀 */
        }
        .column2-2 {
            flex: 3; /* 兩個欄位平均佔據 .form-row 的寬度 */
            padding: 5px; /* 可選，添加內邊距以改進外觀 */
            text-align: right;
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
        .left, .right {
            flex: 1;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-row">
            <h3>基本資料設定</h3>
        </div>
        <div class="container">
            <div class="left">
                <div class="left-container">
                    <div class="form-row">
                        <asp:Label ID="Label2" runat="server" Text="負責人"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="PrincipalTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="left-container">
                    <div class="form-row">
                        <asp:Label ID="Label5" runat="server" Text="電話"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="left-container">
                    <div class="form-row">
                        <asp:Label ID="Label3" runat="server" Text="統一編號"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="AdministrationTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="left-container">
                    <div class="form-row">
                        <asp:Label ID="Label7" runat="server" Text="經辦人"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="ManagerTextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="right-container">
                    <div class="form-row">
                        <asp:Label ID="Label1" runat="server" Text="機構名稱"></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="GroupTextBox" runat="server" Width="400px"></asp:TextBox>
                    </div>
                </div>
                <div class="right-container">
                    <div class="form-row">
                        <asp:Label ID="Label4" runat="server" Text="地址" ></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="AddressTextBox" runat="server" Width="400px"></asp:TextBox>
                    </div>
                </div>

                <div class="right-container">
                    <div class="form-row">
                        <asp:Label ID="Label6" runat="server" Text="登記資料" ></asp:Label>
                    </div>
                    <div class="form-row">
                        <asp:TextBox ID="RegisterTextBox" runat="server" Width="400px"></asp:TextBox>
                    </div>
                </div>
                <div class="right-container">
                    <div class="form-row">
                        <div class="column2-1">
                            <asp:Button ID="ReviseButton" runat="server" Text="修改" OnClientClick="return confirm('確定要修改資料嗎？');"  OnClick="ReviseButton_Click" />
                        </div>
                        <div class="column2-2">
                            <asp:Button ID="ReturnButton" runat="server" Text="回上頁" OnClick="ReturnButton_Click" />
                        </div>                        
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
