<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserFavorites.aspx.cs" Inherits="UserFavorites" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Look Up Favorite Books for a User</title>
</head>
<body>
    <form id="form1" runat="server">
        <atlas:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <atlas:ServiceReference Path="~/Services/UserService.asmx" />
            </Services>
        </atlas:ScriptManager>
        
        <div>User Favorites Lookup</div>
        <div>
            Username: <input id="username" /> <br />
            <input type="button" value="Look Up Favorite Books for User" onclick="DoRetrieveFavorites();" /> <br />
            
            <textarea id="favoriteBooks" cols="60" rows="10">
            </textarea> <br />
            
            <a href="Default.aspx">Home</a>
            
            <script>
            
                function DoRetrieveFavorites()
                {
                    var username = document.getElementById("username").value;
                    
                    // alert("About to call RetrieveFavoriteBooksForUser with username: " + username);
                    UserService.RetrieveFavoriteBooksForUser(username, OnRetrieveFavoriteBooksForUserComplete);
                }
                
                function OnRetrieveFavoriteBooksForUserComplete(result)
                {
                    // alert("Result: " + result);
                    
                    var favoriteBooks = document.getElementById("favoriteBooks");
                    favoriteBooks.value = "";
                    
                    for(i = 0; i < result.length; i++)
                    {
                        favoriteBooks.value += result[i] + "\n";
                    }
                }
                
            </script>
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
