<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sprajax Atlas Demo Site</title>
</head>
<body>
    <form id="form1" runat="server">
        <atlas:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            Welcome to the Sprajax DemoSite for Microsoft Atlas!
        </div>
        <div>
            What would you like to do?
            <ul>
                <li><a href="UserFavorites.aspx">Look up favorite books for a user</a></li>
                <li><a href="BookInformation.aspx">Look up information for a book by its ID</a></li>
            </ul>
        </div>
    </form>

    <script type="text/xml-script">
        <page xmlns:script="http://schemas.microsoft.com/xml-script/2005">
            <references>
            </references>
            <components>
            </components>
        </page>
    </script>
</body>
</html>
