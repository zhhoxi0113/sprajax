using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for BookService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class BookService : System.Web.Services.WebService {

    public BookService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string RetrieveBookTitleForId(int bookId)
    {
        //  Oh no - if the id isn't in the database we throw an exception
        string sql = "SELECT Title FROM Book WHERE ID = " + bookId;
        SqlConnection con = DBUtil.GetConnection();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        string retVal = (string)reader["Title"];
        return (retVal);
    }

    [WebMethod]
    public int[] RetrieveAllBookIds()
    {
        int[] retVal;
        string sql = "SELECT ID FROM Book";
        SqlConnection con = DBUtil.GetConnection();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        ArrayList results = new ArrayList();
        while (reader.Read())
        {
            results.Add((int)reader["ID"]);
        }

        retVal = new int[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            retVal[i] = (int)results[i];
        }

        DBUtil.CloseConnection(con);

        return (retVal);
    }
    
}

