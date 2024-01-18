<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintDemand.aspx.cs" Inherits="記帳系統.PrintDemand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        /* CSS樣式，用於設定方框樣式 */
        .container {
            width: 55%;
            margin: 0 auto;
            border: 1px solid #ccc;
            padding: 20px;
            display: flex; /* 使用 flex 佈局 */
            flex-direction: column; /* 垂直方向的排列 */
            align-items: center; /* 在水平方向上置中 */
            justify-content: center; /* 在垂直方向上置中 */
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
            display: flex;
            justify-content: space-around; /* 在水平方向上均分空間，置中排列 */
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
            <h3>列印</h3>
            <!-- 上部分放置其他控制項 -->
            <div class="controls-container">
                <asp:Button ID="PrintButton" runat="server" Text="預覽列印" OnClick="PrintButton_Click" />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="基本資料設定" />
                <asp:Button ID="ReturnButton" runat="server" OnClick="ReturnButton_Click" Text="回上頁" />
                <asp:Button ID="IndexButton" runat="server" Text="回首頁" OnClick="IndexButton_Click" />
            </div>

            <!-- 下部分放置 GridView -->
            <div class="gridview-container">
                <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
