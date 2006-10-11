<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookInformation.aspx.cs" Inherits="BookInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Look Up Information for a Book By its ID</title>
</head>
<body>
    <form id="form1" runat="server">
    
       <atlas:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <atlas:ServiceReference Path="~/Services/BookService.asmx" />
        </Services>
       </atlas:ScriptManager>
       
        <div>
            Book Lookup<br />
            
            <input type="button" value="Fill In Book IDs" onclick="DoRetrieveBookIDs();" /><br />
            
            <select id="bookIds">
            </select> <br />
            
            <input type="button" value="Get the Book Information" onclick="DoRetrieveBookInfo();" /><br />
            
            <textarea id="bookTitle">
            </textarea> <br />
            
            <a href="Default.aspx">Home</a>
            
            <script type="text/javascript">
            
                function DoRetrieveBookIDs()
                {
                    // alert("About to call RetrieveAllBookIds");
                    BookService.RetrieveAllBookIds(OnDoRetrieveBookIDsComplete)
                }
                
                function OnDoRetrieveBookIDsComplete(result)
                {
                    // alert("Result: " + result);
                    
                    var bookIds = document.getElementById("bookIds");
                    for(i = 0; i < result.length; i++)
                    {
                        bookIds.options[i] = new Option(result[i], result[i]);
                    }
                }
                
                function DoRetrieveBookInfo()
                {
                    var bookIds = document.getElementById("bookIds");
                    if(bookIds.options.length > 0)
                    {
                        var bookId = bookIds.options[bookIds.selectedIndex].value;
                        
                        // alert("About to call RetrieveBookTitleForId with bookId: " + bookId);
                        BookService.RetrieveBookTitleForId(bookId, OnDoRetrieveBookInfoComplete);
                    }
                    else
                    {
                        var bookTitle = document.getElementById("bookTitle");
                        bookTitle.value = "[No book ID selected]";
                    }
                }
                
                function OnDoRetrieveBookInfoComplete(result)
                {
                    // alert("Result: " + result);
                    
                    var bookTitle = document.getElementById("bookTitle");
                    bookTitle.value = result;
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
