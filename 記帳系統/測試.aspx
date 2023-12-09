<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="測試.aspx.cs" Inherits="記帳系統.測試" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Draggable Popup Example</title>
    <script type="text/javascript">
        var isDragging = false;
        var offsetX, offsetY;

        function openDraggablePopup() {
            var popup = document.getElementById('draggablePopup');
            popup.style.display = 'block';

            // 設置mousedown事件以啟動拖動
            popup.addEventListener('mousedown', startDrag);
        }

        function closeDraggablePopup(selectedValue) {
            alert('選擇的值： ' + selectedValue);
            document.getElementById('draggablePopup').style.display = 'none';
        }

        function startDrag(e) {
            isDragging = true;

            // 記錄鼠標點擊位置和彈出視窗的偏移量
            offsetX = e.clientX - parseFloat(getComputedStyle(document.getElementById('draggablePopup')).left);
            offsetY = e.clientY - parseFloat(getComputedStyle(document.getElementById('draggablePopup')).top);

            // 添加mousemove和mouseup事件
            document.addEventListener('mousemove', drag);
            document.addEventListener('mouseup', stopDrag);
        }

        function drag(e) {
            if (isDragging) {
                // 移動視窗
                var popup = document.getElementById('draggablePopup');
                popup.style.left = e.clientX - offsetX + 'px';
                popup.style.top = e.clientY - offsetY + 'px';
            }
        }

        function stopDrag() {
            // 停止拖動
            isDragging = false;

            // 移除mousemove和mouseup事件
            document.removeEventListener('mousemove', drag);
            document.removeEventListener('mouseup', stopDrag);
        }
    </script>
    <style>
        #draggablePopup {
            display: none;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            border: 1px solid #ccc;
            padding: 20px;
            background-color: #fff;
            z-index: 1000;
            cursor: move; /* 設置光標樣式為移動 */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- 按鈕觸發打開可拖動的小視窗 -->
            <asp:Button ID="OpenPopupButton" runat="server" Text="打開可拖動的小視窗" OnClientClick="openDraggablePopup(); return false;" OnClick="OpenPopupButton_Click" />
            
            <!-- 可拖動的小視窗 -->
            <div id="draggablePopup">
                <h3>選擇一個值：</h3>
                <select id="popupSelect">
                    <option value="Option1">選項1</option>
                    <option value="Option2">選項2</option>
                    <option value="Option3">選項3</option>
                </select>
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                <br />
                <!-- 在按鈕中呼叫closeDraggablePopup函數，並傳遞選擇的值 -->
                <asp:Button ID="ClosePopupButton" runat="server" Text="確定" OnClientClick="closeDraggablePopup(document.getElementById('popupSelect').value); return false;" />
            </div>
        </div>
    </form>
</body>
</html>