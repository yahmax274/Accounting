<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="奉獻者資料查詢.aspx.cs" Inherits="記帳系統.奉獻者資料查詢" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #container {
            text-align: center;
            max-width: 800px; /* 設定最大寬度 */
            margin: 0 auto; /* 將容器水平居中 */
        }

        #GridViewContainer {
            display: inline-block;
            text-align: left;
        }

        .selectButton {
            opacity: 0;
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
            <div id="GridViewContainer">
                <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="selectButton">
                            <ItemStyle CssClass="selectButton"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="回首頁" />
            <asp:Button ID="ChoiceButton" runat="server" Text="選擇" OnClick="ChoiceButton_Click" />
        </div>
    </form>
</body>
</html>

