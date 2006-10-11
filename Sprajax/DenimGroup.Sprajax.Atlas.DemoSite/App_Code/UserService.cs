using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for UserService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class UserService : System.Web.Services.WebService {

    public UserService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] RetrieveFavoriteBooksForUser(string username)
    {
        string[] retVal;

        //  Oh no!  A SQL injection vulnerability!
        string sql = "SELECT ID FROM [User] WHERE Username = '" + username + "'";
        SqlConnection con = DBUtil.GetConnection();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            int userId = (int)reader["ID"];
            DBUtil.CloseConnection(con);
            con = DBUtil.GetConnection();
            sql = "SELECT Title FROM Book b WHERE b.ID IN ( SELECT BookID FROM UserBook ub WHERE ub.UserID = " + userId + " )";
            cmd = new SqlCommand(sql, con);
            reader = cmd.ExecuteReader();
            ArrayList results = new ArrayList();
            while (reader.Read())
            {
                results.Add(reader["Title"]);
            }

            retVal = new string[results.Count];
            for (int i = 0; i < results.Count; i++)
            {
                retVal[i] = (string)results[i];
            }
        }
        else
        {
            retVal = new string[] { "Invalid username: '" + username + "'" };
        }

        return (retVal);
    }

    [WebMethod]
    public string UnusedMethodWithAnError(string input1, int input2, float input3)
    {
        //  Oh no!  A software bug that doesn't handle all inputs
        if (input2 > 100)
        {
            throw new Exception("Can't handle inputs over 100");
        }

        return ("A string: " + input1 + "An int: " + input2 + "And a float: " + input3);
    }
    
}

