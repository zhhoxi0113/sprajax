using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Thinktecture.Tools.Web.Services.DynamicProxy;

using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Engine
{
    class MethodTracker
    {
        private int _ParameterCount;
        private int[] _parameterValueCounts;
        private int[] _parameterCurrents;

        private Method _theMethod;
        private Collection<Parameter> _parametersToFuzz;
        private Dictionary<Type, object[]> _parameterLibrary;
        private WebServiceCallListener _listener;

        public MethodTracker(Method theMethod, Dictionary<Type, object[]> parameterLibrary, WebServiceCallListener listener)
        {
            this._listener = listener;
            this._theMethod = theMethod;
            this._parametersToFuzz = theMethod.Parameters;
            this._ParameterCount = this._parametersToFuzz.Count;

            this._parameterLibrary = parameterLibrary;

            _parameterCurrents = new int[ParameterCount];
            for (int i = 0; i < ParameterCount; i++)
            {
                _parameterCurrents[i] = 0;
            }

            _parameterValueCounts = new int[ParameterCount];

            int valueCounter = 0;
            foreach (Parameter p in _parametersToFuzz)
            {
                object[] parameterValues = parameterLibrary[p.Type];
                if (parameterValues == null)
                {
                    throw new ArgumentException("No parameter values available for type: "
                                                    + p.Type + " required by parameter: " + p.Name);
                }

                _parameterValueCounts[valueCounter] = parameterValues.Length;

                valueCounter++;
            }
        }

        public int CallCount
        {
            get
            {
                int retVal = 1;
                for (int i = 0; i < ParameterCount; i++)
                {
                    retVal *= _parameterValueCounts[i];
                }
                return (retVal);
            }
        }

        public void RunCalls()
        {
            bool running = true;
            while (running)
            {
                object[] args = AssembleArgs();
                try
                {
                    CallMethod(args);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("(RunCalls - Unhandled exception: " + ex.Message + ", Stack Trace: " + ex.StackTrace);
                }
                running = IncrementIndicesRecursive(0);
            }
        }

        private object[] AssembleArgs()
        {
            object[] retVal = new object[ParameterCount];
            for (int i = 0; i < ParameterCount; i++)
            {
                Type currentType = this._parametersToFuzz[i].Type;
                object[] argLibrary = this._parameterLibrary[currentType];
                retVal[i] = argLibrary[this._parameterCurrents[i]];
            }

            return (retVal);
        }

        private bool IncrementIndicesRecursive(int dimension)
        {
            if (dimension == ParameterCount)
            {
                return (false);
            }

            if (CanIncrement(dimension))
            {
                Increment(dimension);
                return (true);
            }
            else
            {
                ResetValue(dimension);
                return (IncrementIndicesRecursive(dimension + 1));
            }
        }

        private void ResetValue(int dimension)
        {
            this._parameterCurrents[dimension] = 0;
        }

        private bool CanIncrement(int dimension)
        {
            bool retVal = false;

            if (this._parameterCurrents[dimension] < this._parameterValueCounts[dimension] - 1)
            {
                retVal = true;
            }

            return (retVal);
        }

        private void Increment(int dimension)
        {
            this._parameterCurrents[dimension]++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public void CallMethod(object[] values)
        {
            //  Initialize the call

            DynamicWebServiceProxy wsp = new DynamicWebServiceProxy();
            wsp.EnableMessageAccess = true;
            wsp.Wsdl = _theMethod.Parent.Parent.WSDL;
            wsp.TypeName = _theMethod.Parent.Name;
            wsp.MethodName = _theMethod.Name;
            wsp.Url = new Uri(_theMethod.Parent.Parent.EndPoint);
            foreach (object o in values)
            {
                wsp.AddParameter(o);
            }

            //  Make the call
            try
            {
                object result = wsp.InvokeCall();
                _listener.HandleCall(_theMethod, values, wsp.SoapRequest, wsp.SoapResponse, null);
            }
            catch(Exception ex)
            {
                // System.Console.WriteLine("Exception while invoking call: {0}", ex.Message);
                _listener.HandleCall(_theMethod, values, wsp.SoapRequest, wsp.SoapResponse, ex.InnerException);
            }
            
        }

        public int ParameterCount
        {
            get { return (this._ParameterCount); }
        }
    }
}
