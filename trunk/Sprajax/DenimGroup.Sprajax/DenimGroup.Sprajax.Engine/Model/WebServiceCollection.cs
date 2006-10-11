using System;
using System.Collections.Generic;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Model
{
    public class WebServiceCollection
    {
        private List<WebService> _WebServices;
        private string _WSDL;

        public WebServiceCollection(string wsdl)
        {
            this._WSDL = wsdl;
            _WebServices = new List<WebService>();
        }

        public List<WebService> WebServices
        {
            get { return (_WebServices); }
        }

        public string WSDL
        {
            get { return (_WSDL); }
        }

        public string EndPoint
        {
            get
            {
                //  TOFIX - This is a vicious hack that only works for ASP.NET .asmx web services
                return(_WSDL.Substring(0, _WSDL.Length - 5));
            }
        }

        public List<Method> AllMethods
        {
            get
            {
                //  TODO - Add some caching

                List<Method> retVal = new List<Method>();

                foreach (WebService w in this.WebServices)
                {
                    foreach (Method m in w.Methods)
                    {
                        retVal.Add(m);
                    }
                }

                return (retVal);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("WebServicesCollection: WSDL=");
            sb.Append(WSDL);
            sb.Append("\n");
            foreach (WebService w in WebServices)
            {
                sb.Append("\tWebService: Name=");
                sb.Append(w.Name);

                foreach (Method m in w.Methods)
                {
                    sb.Append("\t\tMethod: Name=");
                    sb.Append(m.Name);
                    sb.Append("(");

                    foreach (Parameter p in m.Parameters)
                    {
                        sb.Append(p.Name);
                        sb.Append(":");
                        sb.Append(p.Type);
                        sb.Append(", ");
                    }

                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(")\n");
                }
            }

            return (sb.ToString());
        }
    }
}
