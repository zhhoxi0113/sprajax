using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DenimGroup.Sprajax.Engine;
using DenimGroup.Sprajax.Engine.Model;

namespace DenimGroup.Sprajax.Main
{
    public partial class SessionResultsForm : Form
    {
        private SprajaxSession _mySession;
        private WebServiceCollection _currentWsc;
        private WebService _currentWs;
        private Method _currentMethod;

        public SessionResultsForm(Sprajax.Engine.SprajaxSession mySession)
        {
            this._mySession = mySession;
            InitializeComponent();

            this.lblBaseUri.Text = _mySession.BaseURL;

            foreach (Uri u in _mySession.JavaScriptURLs.Keys)
            {
                this.lstJavaScript.Items.Add(u.AbsoluteUri);
            }

            foreach (Framework f in _mySession.Frameworks.Keys)
            {
                this.lstFrameworks.Items.Add(f.ToString());
            }

            foreach (Uri u in _mySession.WebServicesCollections.Keys)
            {
                this.lstWebServiceEndpoint.Items.Add(u.AbsoluteUri);
            }

            this.dgCalls.AutoGenerateColumns = true;
            this.dgCalls.DataSource = _mySession.Results;
            this.dgCalls.DataMember = "results";
        }

        private void lstWebServiceEndpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ListControl realSender = (System.Windows.Forms.ListControl)sender;
            string selectedUri = realSender.Text;
            WebServiceCollection wsc = _mySession.WebServicesCollectionsHydrated[new Uri(selectedUri)];
            this._currentWsc = wsc;
            this.lstWebServices.Items.Clear();
            this.lstMethods.Items.Clear();
            this.lstParameters.Items.Clear();
            foreach (WebService ws in wsc.WebServices)
            {
                this.lstWebServices.Items.Add(ws.Name);
            }
        }

        private void lstWebServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ListControl realSender = (System.Windows.Forms.ListControl)sender;
            string name = realSender.Text;
            WebService ws = null;
            foreach(WebService myWs in this._currentWsc.WebServices)
            {
                if (name.Equals(myWs.Name))
                {
                    this._currentWs = myWs;
                    this.lstMethods.Items.Clear();
                    this.lstParameters.Items.Clear();
                    foreach (Method m in myWs.Methods)
                    {
                        lstMethods.Items.Add(m.Name);
                    }
                }
            }
        }

        private void lstMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ListControl realSender = (System.Windows.Forms.ListControl)sender;
            string name = realSender.Text;
            Method ws = null;
            foreach (Method myMethod in this._currentWs.Methods)
            {
                if (name.Equals(myMethod.Name))
                {
                    this._currentMethod = myMethod;
                    this.lstParameters.Items.Clear();
                    foreach (Parameter p in myMethod.Parameters)
                    {
                        lstParameters.Items.Add(p.ToString());
                    }
                }
            }
        }


    }
}