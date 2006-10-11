using System;
using System.Collections.Generic;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Model
{
    public class WebService
    {
        private string _Name;
        private List<Method> _Methods;
        private WebServiceCollection _Parent;

        public WebService(WebServiceCollection parent, string name)
        {
            this._Parent = parent;
            this._Name = name;
            this._Methods = new List<Method>();
        }

        public WebServiceCollection Parent
        {
            get { return (this._Parent); }
        }

        public string Name
        {
            get { return (this._Name); }
        }

        public List<Method> Methods
        {
            get { return (this._Methods); }
        }
    }
}
