using System;
using System.Collections.Generic;
using System.Text;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    public interface SprajaxSessionEventListener
    {
        void JavaScriptEvent(Uri javaScriptUri);

        void FoundFrameworkEvent(Framework framework);

        void FoundWebServiceCollectionEvent(Uri webServiceUri);
    }
}
