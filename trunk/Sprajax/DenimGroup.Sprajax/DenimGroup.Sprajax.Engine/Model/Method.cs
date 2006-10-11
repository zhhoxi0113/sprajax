using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Model
{
    public class Method
    {
        private List<Parameter> _Parameters;
        private string _Name;
        private Type _ReturnType;
        private WebService _Parent;

        public Method(WebService parent, string name, Type returnType)
        {
            this._Parent = parent;
            this._Name = name;
            this._ReturnType = returnType;
            this._Parameters = new List<Parameter>();
        }

        public WebService Parent
        {
            get { return (this._Parent); }
        }

        public Collection<Parameter> Parameters
        {
            get { return (new Collection<Parameter>(this._Parameters)); }
        }

        public string Name
        {
            get { return (this._Name); }
        }

        public Type ReturnType
        {
            get { return (this._ReturnType); }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append("(");

            bool firstTime = true;

            foreach (Parameter p in this.Parameters)
            {
                if (!firstTime)
                {
                    sb.Append(", ");
                }
                sb.Append(p.Name);
                sb.Append(":");
                sb.Append(p.Type);

                firstTime = false;
            }

            sb.Append(") : ");
            sb.Append(this.ReturnType);

            return (sb.ToString());
        }
    }
}
