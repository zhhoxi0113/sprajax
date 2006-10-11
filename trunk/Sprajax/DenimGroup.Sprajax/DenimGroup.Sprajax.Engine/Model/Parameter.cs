using System;
using System.Collections.Generic;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Model
{
    public class Parameter
    {
        private string _Name;
        private Type _Type;

        public Parameter(string name, Type type)
        {
            this._Name = name;
            this._Type = type;
        }

        public string Name
        {
            get { return (_Name); }
        }

        public Type Type
        {
            get { return (_Type); }
        }

        public override string ToString()
        {
            return (Name + ":" + Type);
        }
    }
}
