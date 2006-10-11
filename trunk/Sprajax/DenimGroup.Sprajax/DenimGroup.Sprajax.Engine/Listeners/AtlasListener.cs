using System;
using System.Collections.Generic;
using System.Text;

using Spider.Engine;
using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine.Listeners
{
    class AtlasListener : Spider.Engine.DocumentWorkerListener
    {
        private SprajaxSessionEventListener _listener;

        public AtlasListener(SprajaxSessionEventListener listener)
        {
            _listener = listener;
        }

        public void HandlePage(Uri theUri, string pageContent)
        {
            
        }

        public void HandleTagEvent(Uri baseUri, Tag tag)
        {
            //  Look for any JavaScript includes
            if (tag.Name.ToLower() == "script")
            {
                Spider.Attribute sourceFileAttribute = tag.Attributes["SRC"];
                if (sourceFileAttribute != null && sourceFileAttribute.Value != null)
                {
                    //  Found an included JavaScript file.  Check to see if this matches an AJAX framewor
                    string message = "Found a JavaScript file reference: " + sourceFileAttribute.Value;
                    Log(message);

                    _listener.JavaScriptEvent(new Uri(baseUri, sourceFileAttribute.Value));

                    Framework ajaxFramework = FrameworkDetection.Instance.ProcessJavaScriptFileEvent(sourceFileAttribute.Value, string.Empty);
                    if (ajaxFramework != null)
                    {
                        Log("Found evidence of an AJAX framework: " + ajaxFramework);
                        _listener.FoundFrameworkEvent(ajaxFramework);
                    }

                    System.Console.WriteLine(message);
                }
                else
                {
                    //  Found a JavaScript block.  Check to see if there is an XMLHTTPRequest reference in there.
                    //  TODO - Implement me!
                }
            }

            if (tag.Name.ToLower() == "add")
            {
                //  Found an Atlas web service reference
                string webServiceJs = tag.Attributes["SRC"].Value;
                Log("Found an Atlas WebService JavaScript reference: " + webServiceJs);
                //  TODO - do this in a safe manner
                string webService = webServiceJs.Substring(0, webServiceJs.Length - 3);
                Uri webServiceUri = new Uri(baseUri, webService);
                Log("Found an Atlas WebService: " + webServiceUri.AbsoluteUri);

                _listener.FoundWebServiceCollectionEvent(webServiceUri);

                string wsdl = webServiceUri.AbsoluteUri + "?wsdl";
                Log("Atlas WebService WSDL located at: " + wsdl);
            }
        }

        public void Log(string toLog)
        {
            System.Console.WriteLine(toLog);
        }
    }
}
