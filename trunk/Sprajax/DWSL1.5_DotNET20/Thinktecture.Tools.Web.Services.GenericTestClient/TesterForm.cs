#region Copyright (c) 2003-2004, thinktecture (http://www.thinktecture.com). All rights reserved.
/*
// Copyright (c) 2003-2004, thinktecture (http://www.thinktecture.com).
// All rights reserved.

Copyright
---------
The software and all other content of the distributed package, if not otherwise stated,
is copyright 2003-2004 thinktecture (http://www.thinktecture.com/).
All rights reserved.

Terms of Use
------------
Permission is hereby granted to use this software, for both commercial and non-commercial purposes,
without limitations and free of charge. Permission is hereby granted to copy and distribute
the software for non-commercial purposes. A commercial distribution is NOT allowed without prior
written permission of the author(s).
Further, redistribution and use in source and binary forms, with or without modification, are permitted
only provided that the following conditions are met:

(1) Redistributions of source code must retain the above copyright notice, this list of conditions and
the following warranty disclaimer. 
(2) Redistributions in binary form must reproduce the above copyright notice, this list of conditions
and the following warranty disclaimer in the documentation and/or other materials provided with the distribution. 
(3) Neither the name of thinktecture nor the names of its author(s) may be used to endorse or promote
products derived from this software without specific prior written permission.

Warranty
--------
This software is supplied "AS IS". The author(s) disclaim all warranties, expressed or implied, including,
without limitation, the warranties of merchantability and of fitness for any purpose. The author(s) assume
no liability for direct, indirect, incidental, special, exemplary, or consequential damages, which may
result from the use of this software, even if advised of the possibility of such damage.

Submissions
-----------
The author(s) encourage the submission of comments and suggestions concerning this software.
All suggestions will be given serious technical consideration. By submitting material to the author(s),
you are granting the right to make any use of the material deemed appropriate, i.e. any communication
or material that you transmit to the author(s) by electronic mail or otherwise, including any data,
questions, comments, suggestions or the like, is, and will be treated as, non-confidential and
nonproprietary information. The author(s) may use such communication or material for any purpose whatsoever
including, but not limited to, reproduction, disclosure, transmission, publication, broadcast and further
posting. Further, the author(s) are free to use any ideas, concepts, know-how or techniques contained in any
communication or material you send for any purpose whatsoever, including, but not limited to, developing,
manufacturing and marketing products.
*/
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using AxSHDocVw;
using Thinktecture.Tools.Web.Services.DynamicProxy;
using Thinktecture.Tools.Web.Services.Extensions;

namespace Thinktecture.Tools.Web.Services.TestClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TesterForm : Form
	{
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Panel panel1;

		private ArrayList alTb = new ArrayList();
		private ArrayList alLbl = new ArrayList();
		private ArrayList alTypes = new ArrayList();
		private ArrayList alTypeNames = new ArrayList();
		private TextBox tbWSDL;
		private TextBox tbResult;
		private GroupBox groupBox1;
		private GroupBox groupBox2;

		private DynamicWebServiceProxy wsp = null;
		private ComboBox cbMethods;
		private Label label5;

		private ArrayList methods = null;
		private ParameterInfo[] paramInfo= null;
		private Button bnInvoke;
		private Button bnCreateProxy;
		private Button bnClearCache;
		private Label label6;
		private TextBox tbEndpoint;
		private ComboBox cbTypes;
		private Button bnInvokeAsync;
		private AxWebBrowser webBrowserRequest;
		private AxWebBrowser webBrowserResponse;
		private Label label7;
		private TextBox tbElapsedTime;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public TesterForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			wsp = new DynamicWebServiceProxy();
			methods = new ArrayList();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TesterForm));
			this.bnInvoke = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tbWSDL = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbResult = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.webBrowserResponse = new AxSHDocVw.AxWebBrowser();
			this.webBrowserRequest = new AxSHDocVw.AxWebBrowser();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbElapsedTime = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.bnInvokeAsync = new System.Windows.Forms.Button();
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.tbEndpoint = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cbMethods = new System.Windows.Forms.ComboBox();
			this.bnCreateProxy = new System.Windows.Forms.Button();
			this.bnClearCache = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webBrowserResponse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.webBrowserRequest)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// bnInvoke
			// 
			this.bnInvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnInvoke.Enabled = false;
			this.bnInvoke.Location = new System.Drawing.Point(224, 464);
			this.bnInvoke.Name = "bnInvoke";
			this.bnInvoke.Size = new System.Drawing.Size(80, 24);
			this.bnInvoke.TabIndex = 0;
			this.bnInvoke.Text = "Invoke Call";
			this.bnInvoke.Click += new System.EventHandler(this.bnInvoke_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "WSDL:";
			// 
			// tbWSDL
			// 
			this.tbWSDL.Location = new System.Drawing.Point(64, 40);
			this.tbWSDL.Name = "tbWSDL";
			this.tbWSDL.Size = new System.Drawing.Size(336, 20);
			this.tbWSDL.TabIndex = 2;
			this.tbWSDL.Text = "http://api.google.com/GoogleSearch.wsdl";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Service Name:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Operation Name:";
			// 
			// tbResult
			// 
			this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tbResult.Location = new System.Drawing.Point(8, 354);
			this.tbResult.Multiline = true;
			this.tbResult.Name = "tbResult";
			this.tbResult.Size = new System.Drawing.Size(384, 64);
			this.tbResult.TabIndex = 7;
			this.tbResult.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Parameters:";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Location = new System.Drawing.Point(8, 192);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 141);
			this.panel1.TabIndex = 10;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.webBrowserResponse);
			this.groupBox1.Controls.Add(this.webBrowserRequest);
			this.groupBox1.Location = new System.Drawing.Point(416, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(516, 498);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SOAP messages";
			// 
			// webBrowserResponse
			// 
			this.webBrowserResponse.ContainingControl = this;
			this.webBrowserResponse.Enabled = true;
			this.webBrowserResponse.Location = new System.Drawing.Point(8, 236);
			this.webBrowserResponse.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowserResponse.OcxState")));
			this.webBrowserResponse.Size = new System.Drawing.Size(500, 249);
			this.webBrowserResponse.TabIndex = 21;
			// 
			// webBrowserRequest
			// 
			this.webBrowserRequest.ContainingControl = this;
			this.webBrowserRequest.Enabled = true;
			this.webBrowserRequest.Location = new System.Drawing.Point(8, 24);
			this.webBrowserRequest.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowserRequest.OcxState")));
			this.webBrowserRequest.Size = new System.Drawing.Size(500, 205);
			this.webBrowserRequest.TabIndex = 20;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tbResult);
			this.groupBox2.Controls.Add(this.tbElapsedTime);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.bnInvokeAsync);
			this.groupBox2.Controls.Add(this.cbTypes);
			this.groupBox2.Controls.Add(this.tbEndpoint);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.cbMethods);
			this.groupBox2.Controls.Add(this.bnCreateProxy);
			this.groupBox2.Controls.Add(this.bnClearCache);
			this.groupBox2.Controls.Add(this.bnInvoke);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(400, 496);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// tbElapsedTime
			// 
			this.tbElapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbElapsedTime.Location = new System.Drawing.Point(80, 430);
			this.tbElapsedTime.Name = "tbElapsedTime";
			this.tbElapsedTime.ReadOnly = true;
			this.tbElapsedTime.Size = new System.Drawing.Size(93, 20);
			this.tbElapsedTime.TabIndex = 21;
			this.tbElapsedTime.Text = "";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(7, 430);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 20);
			this.label7.TabIndex = 20;
			this.label7.Text = "Call duration:";
			// 
			// bnInvokeAsync
			// 
			this.bnInvokeAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnInvokeAsync.Enabled = false;
			this.bnInvokeAsync.Location = new System.Drawing.Point(312, 464);
			this.bnInvokeAsync.Name = "bnInvokeAsync";
			this.bnInvokeAsync.Size = new System.Drawing.Size(80, 24);
			this.bnInvokeAsync.TabIndex = 19;
			this.bnInvokeAsync.Text = "Invoke Async";
			this.bnInvokeAsync.Click += new System.EventHandler(this.bnInvokeAsync_Click);
			// 
			// cbTypes
			// 
			this.cbTypes.Location = new System.Drawing.Point(104, 64);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(288, 21);
			this.cbTypes.TabIndex = 18;
			this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged_1);
			// 
			// tbEndpoint
			// 
			this.tbEndpoint.Location = new System.Drawing.Point(104, 128);
			this.tbEndpoint.Name = "tbEndpoint";
			this.tbEndpoint.Size = new System.Drawing.Size(208, 20);
			this.tbEndpoint.TabIndex = 17;
			this.tbEndpoint.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 130);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 16);
			this.label6.TabIndex = 16;
			this.label6.Text = "Endpoint:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 339);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 15);
			this.label5.TabIndex = 15;
			this.label5.Text = "Result:";
			// 
			// cbMethods
			// 
			this.cbMethods.Location = new System.Drawing.Point(104, 96);
			this.cbMethods.Name = "cbMethods";
			this.cbMethods.Size = new System.Drawing.Size(288, 21);
			this.cbMethods.TabIndex = 14;
			this.cbMethods.SelectedIndexChanged += new System.EventHandler(this.cbMethods_SelectedIndexChanged);
			// 
			// bnCreateProxy
			// 
			this.bnCreateProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bnCreateProxy.Location = new System.Drawing.Point(8, 464);
			this.bnCreateProxy.Name = "bnCreateProxy";
			this.bnCreateProxy.Size = new System.Drawing.Size(88, 23);
			this.bnCreateProxy.TabIndex = 13;
			this.bnCreateProxy.Text = "Create Proxy";
			this.bnCreateProxy.Click += new System.EventHandler(this.bnCreateProxy_Click);
			// 
			// bnClearCache
			// 
			this.bnClearCache.Location = new System.Drawing.Point(320, 128);
			this.bnClearCache.Name = "bnClearCache";
			this.bnClearCache.TabIndex = 12;
			this.bnClearCache.Text = "Clear Cache";
			this.bnClearCache.Click += new System.EventHandler(this.bnClearCache_Click);
			// 
			// TesterForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(931, 510);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbWSDL);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "TesterForm";
			this.Text = "Dynamically invoke Web Services";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.webBrowserResponse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.webBrowserRequest)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TesterForm());
		}

		private void bnInvoke_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			
			try
			{
				Init();

				HiResTimer hrt = new HiResTimer();

				hrt.Start();
				object result = wsp.InvokeCall();
				hrt.Stop();

				tbElapsedTime.Text = hrt.ElapsedMicroseconds.ToString();
				tbResult.Text = result.ToString();

				if(wsp.EnableMessageAccess)
				{
					string tmpReqFile = SaveRequestToTempFile(wsp.SoapRequest);
					string tmpRespFile = SaveResponseToTempFile(wsp.SoapResponse);

					object obj = null;
					webBrowserRequest.Navigate ("file://" + tmpReqFile, ref obj, ref obj, ref obj, ref obj);
					webBrowserResponse.Navigate ("file://" + tmpRespFile, ref obj, ref obj, ref obj, ref obj);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private string SaveRequestToTempFile(string request)
		{
			string path = Path.GetTempPath();
			string filePath = path + "soap_request.xml";

			using (StreamWriter sw = new StreamWriter(filePath)) 
			{
				sw.Write(request);
			}

			return filePath;
		}

		private string SaveResponseToTempFile(string request)
		{
			string path = Path.GetTempPath();
			string filePath = path + "soap_response.xml";

			using (StreamWriter sw = new StreamWriter(filePath)) 
			{
				sw.Write(request);
			}

			return filePath;
		}
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void Init()
		{	
			wsp.TypeName = cbTypes.SelectedItem.ToString();
			wsp.MethodName = cbMethods.SelectedItem.ToString();
			wsp.Url = new Uri(tbEndpoint.Text);

			for (int i=0; i < this.panel1.Controls.Count/2;i++)
			{
				object paramValue = this.panel1.Controls[i*2].Text;
				if (!paramInfo[i].IsOut)
					paramValue = Convert.ChangeType(paramValue, paramInfo[i].ParameterType);
				
				// out params are not supported here! An exception will be raised.

				wsp.AddParameter(paramValue);
			}

			bnInvoke.Enabled = true;
		}

		private void bnClearCache_Click(object sender, EventArgs e)
		{
			DynamicWebServiceProxy.ClearCache(tbWSDL.Text);
		}

		private void bnCreateProxy_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			
			wsp.EnableMessageAccess = true;
			wsp.Wsdl = tbWSDL.Text;
			
			Type[] types = wsp.ProxyAssembly.GetTypes();
			Type theType = null;

			alTypes.Clear();

			foreach (Type t in types)
			{
				if(t.BaseType == typeof(SoapHttpClientProtocolExtended))
				{
					alTypes.Add(t);
					alTypeNames.Add(t.Name);
					if (theType == null) theType = t;
				}
			}

			cbTypes.DataSource = alTypeNames;
			PopulateMethods(theType);

			tbEndpoint.Text = wsp.Url.AbsoluteUri;

			bnClearCache.Enabled = false;
			bnInvoke.Enabled = true;
			bnInvokeAsync.Enabled = true;

			this.Cursor = Cursors.Default;
		}

		private void PopulateMethods(Type theType)
		{
			MethodInfo[] mi = theType.GetMethods(BindingFlags.Public | 
												BindingFlags.Instance |
												BindingFlags.DeclaredOnly);
			ArrayList methodNames = new ArrayList();

			methods.Clear();
			foreach (MethodInfo m in mi)
			{
				if(!(m.Name.StartsWith("Begin") || m.Name.StartsWith("End")))
				{
					methods.Add(m);
					methodNames.Add(m.Name);
				}
			}

			cbMethods.DataSource = methodNames;
			cbMethods_SelectedIndexChanged(null, null);
		}

		private void cbMethods_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.panel1.Controls.Clear();
			alTb.Clear();
			alLbl.Clear();

			MethodInfo[] mia = (MethodInfo[])methods.ToArray(typeof(MethodInfo));
			ParameterInfo[] pi = mia[cbMethods.SelectedIndex].GetParameters();
			paramInfo = pi;

			foreach (ParameterInfo p in pi)
			{
				alTb.Add(new TextBox());
				int offset = alTb.Count-1;
				((TextBox)alTb[alTb.Count-1]).Text = "[" + p.Name + "]";
				((TextBox)alTb[alTb.Count-1]).Bounds = new Rectangle(new Point(0, offset*27), new Size(200, 50));

				alLbl.Add(new Label());
				((Label)alLbl[alLbl.Count-1]).Text = p.ParameterType.ToString();	
				((Label)alLbl[alLbl.Count-1]).Location = new Point(210, offset*27);
				((Label)alLbl[alLbl.Count-1]).Size = new Size(160, 28);

				this.panel1.Controls.Add((TextBox)alTb[alTb.Count-1]);
				this.panel1.Controls.Add((Label)alLbl[alLbl.Count-1]);
			}

		}

		private void cbTypes_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			Type theType = (Type)alTypes[((ComboBox)sender).SelectedIndex];
			PopulateMethods(theType);
		}

		private void bnInvokeAsync_Click(object sender, EventArgs e)
		{
			Init();

			wsp.BeginInvokeCall(new AsyncCallback(this.InvokeServiceCallBack), wsp);
		}

		private void InvokeServiceCallBack(IAsyncResult state)
		{
			if(state.IsCompleted)
			{
				object result = wsp.EndInvokeCall(state);

				// TODO: jump back into the GUI thread ... the following code is not correct!!!
				tbResult.Text = result.ToString();
				
				string tmpReqFile = SaveRequestToTempFile(wsp.SoapRequest);
				string tmpRespFile = SaveResponseToTempFile(wsp.SoapResponse);

				object obj = null;
				webBrowserRequest.Navigate("file://" + tmpReqFile, ref obj, ref obj, ref obj, ref obj);
				webBrowserResponse.Navigate("file://" + tmpRespFile, ref obj, ref obj, ref obj, ref obj);
			}
		}
	}
}



