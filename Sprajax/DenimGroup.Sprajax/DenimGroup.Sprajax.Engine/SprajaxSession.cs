using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

using Thinktecture.Tools.Web.Services.DynamicProxy;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    public class SprajaxSession : SprajaxSessionEventListener, WebServiceCallListener
    {
        private string _baseUrl;
        private Spider.Engine.Spider _spider;
        private Dictionary<Uri, Guid>         _javaScriptUris = new Dictionary<Uri, Guid>();
        private Dictionary<Framework, Guid>   _frameworks = new Dictionary<Framework, Guid>();
        private Dictionary<Uri, Guid>         _webServicesCollections = new Dictionary<Uri, Guid>();
        private Dictionary<Uri, WebServiceCollection> _WebServicesCollectionsHydrated = new Dictionary<Uri, WebServiceCollection>();


        private Guid _sessionId;
        private string _connString;
        private bool _storeAll;

        private int _successfulCalls = 0;
        private int _failedCalls = 0;

        public SprajaxSession(string baseUrl, string connString, bool storeAll)
        {
            _baseUrl = baseUrl;
            _connString = connString;
            _storeAll = storeAll;
            DynamicWebServiceProxy.ClearAllCached();
        }

        public string BaseURL
        {
            get { return (this._baseUrl); }
        }

        public Dictionary<Uri, Guid> JavaScriptURLs
        {
            get { return (this._javaScriptUris); }
        }

        public Dictionary<Framework, Guid> Frameworks
        {
            get { return (this._frameworks); }
        }

        public Dictionary<Uri, Guid> WebServicesCollections
        {
            get { return (this._webServicesCollections); }
        }

        public Dictionary<Uri, WebServiceCollection> WebServicesCollectionsHydrated
        {
            get { return (this._WebServicesCollectionsHydrated); }
        }

        public System.Data.DataSet Results
        {
            get
            {
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "up_RetrieveWebServiceCalls";
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("SessionID", _sessionId));
                
                DataSet retVal = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(retVal, "results");
                conn.Dispose();

                return (retVal);
            }
        }

        public void RunFootprint()
        {
            // ThreadStart starter = new ThreadStart(this.SpiderThread);
            // Thread spider = new Thread(starter);
            // spider.Start();

            _spider = new Spider.Engine.Spider();
            _spider.ReportTo = new Spider.Engine.ConsoleSpiderStatusListener();
            //  TODO - Make this a parameter
            _spider.OutputPath = "C:\\Sprajax\\";
            //  TODO - Make this ia parameter, and when it can be more than 1 we need to do some serious QA
            //  because certain things (AtlastListener page and tag handling, for example) currently depend on
            //  this being single-threaded.
            int threads = 2;

            _spider.Listeners.Add(new DenimGroup.Sprajax.Engine.Listeners.AtlasListener(this));
            _spider.Listeners.Add(new DenimGroup.Sprajax.Engine.Listeners.GoogleWebToolkitListener(this));

            try
            {
                _sessionId = Guid.NewGuid();
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "up_CreateSession";
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("ID", _sessionId));
                cmd.Parameters.Add(new SqlParameter("BaseUri", _baseUrl));
                cmd.ExecuteNonQuery();
                conn.Dispose();

                //  Spider the site and grab all the information we can find.
                //  TODO - There seems to be some sort of threading or timing problem
                //  waiting for the spider to finish.  Might need to debug some
                //  spider code - DPC
                Log("About to start spidering");
                _spider.Start(new Uri(_baseUrl), threads);
                _spider.SpiderDone.WaitDone();
                Log("Finished spidering");

                //  Print out what we found
                Log("JavaScript files found");
                foreach (Uri jsUri in _javaScriptUris.Keys)
                {
                    Log(jsUri.AbsoluteUri);
                }

                Log("Frameworks detected");
                foreach(Framework framework in _frameworks.Keys)
                {
                    Log(framework.ToString());
                }

                Log("WebServices found");
                foreach(Uri wsUri in _webServicesCollections.Keys)
                {
                    Log(wsUri.AbsoluteUri);
                }

                
            }
            catch (UriFormatException ex)
            {
                Log(ex.Message);
                return;
            }
        }

        public void RunFuzz()
        {
            //  Fuzz the web services

            //  First set up our parameter dictionary
            //  TOFIX - Load these from files and flesh out the types that are supported.  This is really weak right now.
            //  TOFIX - Also figure out how to feed "null" values through the DynWSLib without getting exceptions
            Dictionary<Type, object[]> parameterLibrary = new Dictionary<Type, object[]>();
            parameterLibrary.Add(Type.GetType("System.String"), new object[] { string.Empty, "'JUNK", "\"JUNK", "1234567890", "`~!@#$%^&*()_-+={[}]|\\:;<,>.?/",
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"+
                "AAAAAAAAAAAAAAAAAAAAAAAAA" //  1025 A's
            });
            parameterLibrary.Add(Type.GetType("System.Int32"), new object[] { int.MinValue, -1025, -1024, -1023, -129, -128, -127, -101, -100, -99, -10, -5, -1, 0, 1, 5, 10, 100, 127, 128, 129, 1023, 1024, 1025, int.MaxValue });
            parameterLibrary.Add(Type.GetType("System.Single"), new object[] { float.MinValue, float.MaxValue, float.NaN, float.NegativeInfinity, float.PositiveInfinity, float.Epsilon, 0.0, -1.0, 1.0 });
            parameterLibrary.Add(Type.GetType("System.Double"), new object[] { double.MinValue, double.MaxValue, double.NaN, double.NegativeInfinity, double.PositiveInfinity, double.Epsilon, 0.0, -1.0, 1.0 });

            Log("About to fuzz the web services");
            foreach (Uri wsUri in _webServicesCollections.Keys)
            {
                Log("Attempting to fuzz web service at: " + wsUri.AbsoluteUri);
                //  TODO - Non-Atlas, non-.NET web services will need different logic here
                string sWsdlUri = wsUri.AbsoluteUri + "?wsdl";
                Log("Looking for WSDL at: " + sWsdlUri);
                WebServiceEnumerator wsEnumerator = new WebServiceEnumerator(sWsdlUri);
                WebServiceCollection wsCollection = wsEnumerator.Enumerate();
                this._WebServicesCollectionsHydrated[wsUri] = wsCollection;

                // List<Method> methods = wsEnumerator.Services.AllMethods;
                foreach (WebService w in wsCollection.WebServices)
                {
                    foreach (Method m in w.Methods)
                    {
                        Log(m.ToString());
                        MethodTracker tracker = new MethodTracker(m, parameterLibrary, this);
                        Log("Call count for the method will be: " + tracker.CallCount);
                        try
                        {
                            tracker.RunCalls();
                        }
                        catch (Exception ex)
                        {
                            Log("Unhandled exception: " + ex.Message + ", Stack Trace: " + ex.StackTrace);
                        }
                    }
                }
            }

            Log("Successful calls: " + _successfulCalls);
            Log("Failed calls: " + _failedCalls);
        }

        private void Log(string str)
        {
            System.Console.WriteLine("(SprajaxSession)Log: {0}", str);
        }


        #region SprajaxSessionEventListener Members

        public void JavaScriptEvent(Uri javaScriptUri)
        {
            Log("Found JavaScript URI: " + javaScriptUri.AbsoluteUri);

            Guid jsId = Guid.NewGuid();
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "up_CreateJavaScriptUri";
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("ID", jsId));
            cmd.Parameters.Add(new SqlParameter("SessionID", _sessionId));
            cmd.Parameters.Add(new SqlParameter("Uri", javaScriptUri.AbsoluteUri));
            cmd.ExecuteNonQuery();
            conn.Dispose();

            this._javaScriptUris[javaScriptUri] = jsId;


        }

        public void FoundFrameworkEvent(DenimGroup.Sprajax.Engine.Model.Framework framework)
        {
            Log("Found Framework: " + framework.ToString());

            Guid frameworkId = Guid.NewGuid();
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "up_CreateFramework";
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("ID", frameworkId));
            cmd.Parameters.Add(new SqlParameter("SessionID", _sessionId));
            cmd.Parameters.Add(new SqlParameter("Name", framework.Name));
            cmd.Parameters.Add(new SqlParameter("Platform", framework.Platform.ToString()));
            cmd.ExecuteNonQuery();
            conn.Dispose();

            this._frameworks[framework] = frameworkId;
        }

        public void FoundWebServiceCollectionEvent(Uri webServiceCollectionUri)
        {
            Log("Found WebService: " + webServiceCollectionUri.AbsoluteUri);

            Guid wsId = Guid.NewGuid();
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "up_CreateWebServiceCollection";
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("ID", wsId));
            cmd.Parameters.Add(new SqlParameter("SessionID", _sessionId));
            cmd.Parameters.Add(new SqlParameter("Uri", webServiceCollectionUri.AbsoluteUri));
            cmd.ExecuteNonQuery();
            conn.Dispose();

            this._webServicesCollections[webServiceCollectionUri] = wsId;
        }

        #endregion

        #region WebServiceCallListener Members

        public void HandleCall(Method method, object[] parameters, string requestText, string responseText, Exception exception)
        {
            if (exception != null)
            {
                // Log("Method: " + method + ", parameters: " + parameters + ", requestText: " + requestText + ", responseText: " + responseText + ", exception: " + exception);
                Log("Call to " + method.Name + " failed!  Parameters were: " + parameters + " and responseText was: " + responseText);
                _failedCalls++;
            }
            else
            {
                Log("Call to " + method.Name + " ran fine");
                _successfulCalls++;
            }

            if (exception != null || _storeAll == true)
            {
                Guid id = Guid.NewGuid();
                SqlConnection conn = GetConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "up_CreateWebServicesCall";
                cmd.Connection = conn;
                cmd.Parameters.Add(new SqlParameter("ID", id));
                cmd.Parameters.Add(new SqlParameter("SessionID", _sessionId));
                cmd.Parameters.Add(new SqlParameter("MethodName", method.Name));
                cmd.Parameters.Add(new SqlParameter("Parameters", FormatParams(parameters)));
                cmd.Parameters.Add(new SqlParameter("RequestText", requestText));
                cmd.Parameters.Add(new SqlParameter("ResponseText", responseText));
                if (exception != null)
                {
                    cmd.Parameters.Add(new SqlParameter("ExceptionMessage", exception.InnerException.Message));
                    cmd.Parameters.Add(new SqlParameter("ExceptionStackTrace", exception.InnerException.StackTrace));
                }
                cmd.ExecuteNonQuery();
                conn.Dispose();
            }
        }

        private string FormatParams(object[] myParams)
        {
            bool first = true;

            StringBuilder sb = new StringBuilder();
            foreach (object o in myParams)
            {
                sb.Append(o.ToString());
                sb.Append(", ");
                first = false;
            }
            if (!first)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return (sb.ToString());
        }

        #endregion

        private System.Data.SqlClient.SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = this._connString;
            conn.Open();
            return (conn);
        }
    }
}
