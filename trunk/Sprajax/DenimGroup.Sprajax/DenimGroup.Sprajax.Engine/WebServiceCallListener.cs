using System;
using System.Collections.Generic;
using System.Text;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    public interface WebServiceCallListener
    {
        void HandleCall(Method method, object[] parameters, string requestText, string responseText, Exception exception);
    }
}
