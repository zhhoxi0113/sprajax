using System;
using System.Collections.Generic;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Listeners
{
    class GoogleWebToolkitListener : Spider.Engine.DocumentWorkerListener
    {
        private SprajaxSessionEventListener _listener;

        public GoogleWebToolkitListener(SprajaxSessionEventListener listener)
        {
            _listener = listener;
        }

        #region DocumentWorkerListener Members

        public void HandlePage(Uri page, string pageContent)
        {
            //  IMPLEMENT ME!
        }

        public void HandleTagEvent(Uri baseUri, Spider.Engine.Tag tag)
        {
            //  IMPLEMENT ME!
        }

        #endregion
    }
}
