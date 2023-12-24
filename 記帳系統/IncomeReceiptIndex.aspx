<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IncomeReceiptIndex.aspx.cs" Inherits="記帳系統.IncomeReceiptIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            margin: 0;
        }

        #container {
            width: 400px; /* 調整方框寬度 */
            height: 200px; /* 調整方框高度 */
            border: 1px solid #ccc;
            padding: 20px;
            display: flex;
        }

        #leftSpace {
            flex: 6;
            padding-right: 10px; /* 左右空間之間的間距 */
        }

        #rightSpace {
            flex: 1;
            padding-left: 10px; /* 左右空間之間的間距 */
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

        .right-column2-2 {
            flex: 5; /* 兩個欄位平均佔據 .form-row 的寬度 */
            padding: 5px; /* 可選，添加內邊距以改進外觀 */
            text-align: right;
        }

        .disabled-button {
            background-color: #ccc;
            cursor: not-allowed;
        }
    </style>

    <script>
        function handleCheckboxChange() {
            var selectButton = document.getElementById('<%= SelectButton.ClientID %>');
            var checkBox = document.getElementById('<%= OnlyOneCheckBox.ClientID %>');

            if (checkBox.checked) {
                // Checkbox is checked, enable the button
                selectButton.classList.remove('disabled-button');
                selectButton.disabled = false;
            } else {
                // Checkbox is not checked, disable the button
                selectButton.classList.add('disabled-button');
                selectButton.disabled = true;
            }
        }
        function isButtonEnabled() {
            var selectButton = document.getElementById('<%= SelectButton.ClientID %>');
            return !selectButton.disabled;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <div id="container">
            <div class="leftSpace">
                <!-- 左側內容 -->
                <div class="form-row">
                    <asp:Label ID="Label2" runat="server" Text="列印選項"></asp:Label>
                </div>
                <div class="form-row">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem>編號列印</asp:ListItem>
                        <asp:ListItem>日期列印</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-row">
                    <asp:Label ID="Label1" runat="server" Text="起迄日期"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="left-column">
                        <asp:TextBox ID="StartTextBox" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="right-column">
                        <asp:TextBox ID="EndTextBox" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <asp:CheckBox ID="OnlyOneCheckBox" runat="server" Text="選擇單一奉獻者列印" OnChange="handleCheckboxChange()" />
                </div>
                <div class="form-row">
                    <asp:Label ID="Label3" runat="server" Text="起迄收據編號"></asp:Label>
                </div>
                <div class="form-row">
                    <div class="left-column">
                        <asp:TextBox ID="StartDataTextBox" runat="server" ></asp:TextBox>
                    </div>
                    <div class="right-column">
                        <asp:TextBox ID="EndDataTextBox" runat="server" ></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="rightSpace">
                <!-- 右側內容 -->
                <div class="form-row">
                    <div class="right-column2-2">
                        <asp:Button ID="ViewButton" runat="server" Text="預覽" OnClick="ViewButton_Click" />
                    </div>
                </div>
                <div class="form-row">
                    <div class="right-column2-2">
                        <asp:Button ID="IndexButton" runat="server" Text="離開" />
                    </div>
                </div>
                <div class="form-row">
                    <div class="right-column2-2">
                        <asp:Button ID="SelectButton" runat="server" Text="奉獻者選擇" CssClass="disabled-button" OnClientClick="return isButtonEnabled();" OnClick="SelectButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>