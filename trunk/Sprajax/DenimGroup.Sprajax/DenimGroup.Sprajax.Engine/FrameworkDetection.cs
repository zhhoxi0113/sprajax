using System;
using System.Collections.Generic;
using System.Text;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    public class FrameworkDetection
    {
        private static FrameworkDetection _instance;

        private FrameworkDetection()
        {

        }

        public static FrameworkDetection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FrameworkDetection();
                }
                return (_instance);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public Framework ProcessJavaScriptFileEvent(string uri, string fileContent)
        {
            Framework retVal = null;

            if (uri == null)
            {
                return (null);
            }

            if (uri.Contains("Atlas.js") || uri.Contains("WebResource.axd"))
            {
                return (new Framework("Atlas", Platform.ASPNET));
            }

            if (uri.Contains("gwt.js"))
            {
                retVal = new Framework("Google Web Toolkit", Platform.J2EE);
            }

            return (retVal);
        }
    }
}
