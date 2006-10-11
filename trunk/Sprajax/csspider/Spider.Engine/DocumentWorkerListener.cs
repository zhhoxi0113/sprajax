using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Engine
{
    public interface DocumentWorkerListener
    {
        void HandleTagEvent(Uri baseUri, Tag tag);

        void HandlePage(Uri page, string pageContent);
    }
}
