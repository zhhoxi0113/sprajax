namespace DenimGroup.Sprajax.Main
{
    partial class SessionResultsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstWebServices = new System.Windows.Forms.ListBox();
            this.lstMethods = new System.Windows.Forms.ListBox();
            this.lstParameters = new System.Windows.Forms.ListBox();
            this.dgCalls = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstJavaScript = new System.Windows.Forms.ListBox();
            this.lstFrameworks = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBaseUri = new System.Windows.Forms.Label();
            this.lstWebServiceEndpoint = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCalls)).BeginInit();
            this.SuspendLayout();
            // 
            // lstWebServices
            // 
            this.lstWebServices.FormattingEnabled = true;
            this.lstWebServices.Location = new System.Drawing.Point(603, 141);
            this.lstWebServices.Name = "lstWebServices";
            this.lstWebServices.Size = new System.Drawing.Size(557, 95);
            this.lstWebServices.TabIndex = 1;
            this.lstWebServices.SelectedIndexChanged += new System.EventHandler(this.lstWebServices_SelectedIndexChanged);
            // 
            // lstMethods
            // 
            this.lstMethods.FormattingEnabled = true;
            this.lstMethods.Location = new System.Drawing.Point(603, 258);
            this.lstMethods.Name = "lstMethods";
            this.lstMethods.Size = new System.Drawing.Size(557, 95);
            this.lstMethods.TabIndex = 2;
            this.lstMethods.SelectedIndexChanged += new System.EventHandler(this.lstMethods_SelectedIndexChanged);
            // 
            // lstParameters
            // 
            this.lstParameters.FormattingEnabled = true;
            this.lstParameters.Location = new System.Drawing.Point(603, 375);
            this.lstParameters.Name = "lstParameters";
            this.lstParameters.Size = new System.Drawing.Size(557, 95);
            this.lstParameters.TabIndex = 3;
            // 
            // dgCalls
            // 
            this.dgCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCalls.Location = new System.Drawing.Point(12, 489);
            this.dgCalls.Name = "dgCalls";
            this.dgCalls.Size = new System.Drawing.Size(1148, 353);
            this.dgCalls.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Web Services";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(600, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Methods";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(600, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Parameters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 473);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Call Results";
            // 
            // lstJavaScript
            // 
            this.lstJavaScript.FormattingEnabled = true;
            this.lstJavaScript.Location = new System.Drawing.Point(12, 141);
            this.lstJavaScript.Name = "lstJavaScript";
            this.lstJavaScript.Size = new System.Drawing.Size(557, 95);
            this.lstJavaScript.TabIndex = 9;
            // 
            // lstFrameworks
            // 
            this.lstFrameworks.FormattingEnabled = true;
            this.lstFrameworks.Location = new System.Drawing.Point(12, 258);
            this.lstFrameworks.Name = "lstFrameworks";
            this.lstFrameworks.Size = new System.Drawing.Size(557, 95);
            this.lstFrameworks.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Java Script Files";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "AJAX Frameworks";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Base URI:";
            // 
            // lblBaseUri
            // 
            this.lblBaseUri.AutoSize = true;
            this.lblBaseUri.Location = new System.Drawing.Point(61, 12);
            this.lblBaseUri.Name = "lblBaseUri";
            this.lblBaseUri.Size = new System.Drawing.Size(0, 13);
            this.lblBaseUri.TabIndex = 14;
            // 
            // lstWebServiceEndpoint
            // 
            this.lstWebServiceEndpoint.FormattingEnabled = true;
            this.lstWebServiceEndpoint.Location = new System.Drawing.Point(12, 375);
            this.lstWebServiceEndpoint.Name = "lstWebServiceEndpoint";
            this.lstWebServiceEndpoint.Size = new System.Drawing.Size(557, 95);
            this.lstWebServiceEndpoint.TabIndex = 15;
            this.lstWebServiceEndpoint.SelectedIndexChanged += new System.EventHandler(this.lstWebServiceEndpoint_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 359);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Web Service Endpoints";
            // 
            // SessionResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 854);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstWebServiceEndpoint);
            this.Controls.Add(this.lblBaseUri);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lstFrameworks);
            this.Controls.Add(this.lstJavaScript);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgCalls);
            this.Controls.Add(this.lstParameters);
            this.Controls.Add(this.lstMethods);
            this.Controls.Add(this.lstWebServices);
            this.Name = "SessionResultsForm";
            this.Text = "Sprajax Session Results";
            ((System.ComponentModel.ISupportInitialize)(this.dgCalls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstWebServices;
        private System.Windows.Forms.ListBox lstMethods;
        private System.Windows.Forms.ListBox lstParameters;
        private System.Windows.Forms.DataGridView dgCalls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstJavaScript;
        private System.Windows.Forms.ListBox lstFrameworks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBaseUri;
        private System.Windows.Forms.ListBox lstWebServiceEndpoint;
        private System.Windows.Forms.Label label8;
    }
}