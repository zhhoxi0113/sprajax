using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Engine
{
    public class Tag
    {
        private AttributeList _attributes;
        private string _name;

        public Tag(string name, AttributeList attributes)
        {
            if (name == null)
            {
                throw new ArgumentException("name cannot be null");
            }
            if (attributes == null)
            {
                throw new ArgumentException("attributes cannot be null");
            }

            this._name = name;
            this._attributes = attributes;
        }

        public string Name
        {
            get
            {
                return (_name);
            }
        }

        public AttributeList Attributes
        {
            get
            {
                return (_attributes);
            }
        }

    }
}
