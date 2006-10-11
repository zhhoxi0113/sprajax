using System;
using System.Collections.Generic;
using System.Text;

namespace DenimGroup.Sprajax.Engine.Model
{
    public class Framework
    {
        private string _name;
        private Platform _platform;

        public Framework(string name, Platform platform)
        {
            if (name == null || name.Equals(string.Empty))
            {
                throw new ArgumentException("Name must be non-null and non-blank.  Name was '" + name + "'");
            }

            this._name = name;
            this._platform = platform;
        }

        public string Name
        {
            get
            {
                return (this._name);
            }
        }

        public Platform Platform
        {
            get
            {
                return (this._platform);
            }
        }

        public override string ToString()
        {
            return (Name + ":" + Platform);
        }
    }

    public enum Platform
    {
        ASPNET,
        J2EE,
        PHP,
        PERL,
        PYTHON,
        RUBY_ON_RAILS
    }
}
