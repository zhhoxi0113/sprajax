using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DBUtil
/// </summary>
public class DBUtil
{
	public DBUtil()
	{

	}

    public static SqlConnection GetConnection()
    {
        string connString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        SqlConnection retVal = new SqlConnection(connString);
        retVal.Open();
        return (retVal);
    }

    public static void CloseConnection(SqlConnection con)
    {
        try
        {
            if (con != null)
            {
                con.Close();
            }
        }
        catch
        {

        }
    }
}
