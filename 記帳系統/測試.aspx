<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="測試.aspx.cs" Inherits="記帳系統.測試" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnOpenNewPage" runat="server" Text="Open New Page" OnClientClick="return openNewPage();" />
            <script type="text/javascript">
    function openNewPage() {
        // 打开新页面，并传递当前页面的 URL 作为参数
        var originalPageUrl = window.location.href;
        var newPageUrl = 'NewPage.aspx?originalPageUrl=' + encodeURIComponent(originalPageUrl);
        window.open(newPageUrl, '_blank');

        // 阻止原始页面的 PostBack
        return false;
    }

    // 在新页面加载时，检查是否有参数传递回来，并将其显示在原始页面上
    window.onload = function () {
        var originalPageUrl = getParameterByName('originalPageUrl');
        if (originalPageUrl) {
            var inputValue = prompt('Enter text:');
            if (inputValue !== null) {
                // 将输入的文本传递回原始页面
                window.opener.updateOriginalPage(inputValue);
                window.close();
            }
        }
    };

    // 获取 URL 参数的函数
    function getParameterByName(name) {
        var url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }
            </script>

        </div>
    </form>
</body>
</html>
