<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberRegister.aspx.cs" Inherits="記帳系統.MemberRegister"  EnableViewState="true"%>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ifram</title>
    <style>
        body {
            margin: 0;
            padding: 0;
        }

        #container {
            display: flex;
            height: 100vh;
        }

        #left-section {
            flex: 2; /* 占5份 */
            /*background-color: #f0f0f0;  左邊區塊的背景色 */
            padding: 10px;
        }

        #right-section {
            flex: 5; /* 占2份 */
            /*background-color: #d0d0d0;  右邊區塊的背景色 */
            padding: 10px;
        }

        iframe {
            border: none;
        }

        .button-container {
            display: flex;
            flex-direction: column;
            gap: 10px;
        }
        .form-row {
        display: flex;
        align-items: flex-start;
        margin-bottom: 10px;
        }

        .form-column {
            flex: 1;
            margin-right: 10px;
        }

        .form-column:last-child {
            margin-right: 0;
        }
    </style>
<script>
    // 使用 window.addEventListener 監聽 message 事件
    window.addEventListener('message', function (event) {
        var receivedData = event.data;

        console.log('Received data from iframe:', receivedData);

        // 將接收到的資料設定到相應的控制項中，並將&nbsp;替換為空字串
        document.getElementById('<%= IDTextBox.ClientID %>').value = replaceNbsp(receivedData.User_Id);
        document.getElementById('<%= NameTextBox.ClientID %>').value = replaceNbsp(receivedData.Name);
        document.getElementById('<%= GroupDropDownList.ClientID %>').value = replaceNbsp(receivedData.Group);
        document.getElementById('<%= Phone1TextBox.ClientID %>').value = replaceNbsp(receivedData.Phone1);
        document.getElementById('<%= Phone2TextBox.ClientID %>').value = replaceNbsp(receivedData.Phone2);
        document.getElementById('<%= PostalCodeTextBox.ClientID %>').value = replaceNbsp(receivedData.Postal_Code);
        document.getElementById('<%= AddressTextBox.ClientID %>').value = replaceNbsp(receivedData.Address);
        document.getElementById('<%= NoteTextBox.ClientID %>').value = replaceNbsp(receivedData.Note);
        document.getElementById('<%= KeyIDTextBox.ClientID %>').value = replaceNbsp(receivedData.idValue);
    });

    function replaceNbsp(value) {
        // 如果值為&nbsp;，則替換為空字串；否則保持原值
        return value === '&nbsp;' ? '' : value;
    }
    function scrollToBottom() {
        var iframe = document.getElementById('yourIframe');
        console.log("Iframe Height:", iframe.contentDocument.documentElement.scrollHeight);
        console.log("Iframe Content Height:", iframe.contentDocument.body.scrollHeight);
        iframe.contentWindow.scrollTo(0, iframe.contentDocument.body.scrollHeight);
        window.onload = function () {
            // 執行你的程式碼，包括 scroll
            scrollToBottom();
        };
    }
    function applyStyleToLastRow() {
        var iframe = document.getElementById('yourIframe');
        var gridView = iframe.contentDocument.getElementById('GridView1'); 

        // 獲取最後一行
        var rows = gridView.rows;
        var lastRow = rows[rows.length - 1];

        // 添加樣式
        lastRow.classList.add('selectedRow');
    }

</script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <div id="left-section">
                <h3>奉獻者資料維護</h3>
                <iframe id="yourIframe" src="MemberSheet.aspx" height="350" width="700"></iframe>
            </div>
            <div id="right-section">
                <div class="button-container">
                    <h3></h3>
                    <div class="form-row">

                        <asp:Label ID="IDLabel" runat="server" Text="代號:" ></asp:Label>
                        <asp:TextBox ID="IDTextBox" runat="server" OnTextChanged="IDTextBox_TextChanged" ></asp:TextBox>
                        <asp:Label ID="PostalCodeLabel" runat="server" Text="郵遞區號:" ></asp:Label>
                        <asp:TextBox ID="PostalCodeTextBox" runat="server" ></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="NameLabel" runat="server" Text="姓名:"></asp:Label>
                        <asp:TextBox ID="NameTextBox" runat="server" ></asp:TextBox>

                        <asp:Label ID="GroupLabel" runat="server" Text="組別:" ></asp:Label>
                        <asp:DropDownList ID="GroupDropDownList" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>A_教會組</asp:ListItem>
                            <asp:ListItem>B_XX組</asp:ListItem>
                            <asp:ListItem>C_XX組</asp:ListItem>
                            <asp:ListItem>D_XX組</asp:ListItem>
                            <asp:ListItem>E_XX組</asp:ListItem>
                            <asp:ListItem>T_測試</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="Phone1Label" runat="server" Text="連絡電話1:" ></asp:Label>
                        <asp:TextBox ID="Phone1TextBox" runat="server" MaxLength="10" TextMode="Phone"></asp:TextBox>

                        <asp:Label ID="Phone2Label" runat="server" Text="連絡電話2:"></asp:Label>
                        <asp:TextBox ID="Phone2TextBox" runat="server" MaxLength="10" TextMode="Phone"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="AddressLabel" runat="server" Text="地址:" ></asp:Label>
                        <asp:TextBox ID="AddressTextBox" runat="server" ></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <asp:Label ID="NoteLabel" runat="server" Text="備註:" Height="30px" Width="72px"></asp:Label>
                        <asp:TextBox ID="NoteTextBox" runat="server" Height="90px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <asp:Button ID="IndexButton" runat="server" Text="回首頁" />
                        <asp:TextBox ID="KeyIDTextBox" runat="server" BorderStyle="None" style="display: none;"  ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="UpdateButton" runat="server" Text="新增" OnClick="UpdateButton_Click" /><asp:Button ID="SearchButton" runat="server" Text="查詢" OnClientClick="return openPopup();" OnClick="SearchButton_Click" /><asp:Button ID="DeleteButton" runat="server" Text="刪除" OnClientClick="return confirm('確定要刪除這筆資料嗎？');" OnClick="DeleteButton_Click" /><asp:Button ID="ReviseButton" runat="server" Text="修改" OnClick="ReviseButton_Click" />
                        </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>