namespace DenimGroup.Sprajax.Main
{
    partial class SprajaxForm
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
            this.btnFootprint = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkStoreAll = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDbConnString = new System.Windows.Forms.TextBox();
            this.btnFuzz = new System.Windows.Forms.Button();
            this.btnShowResults = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFootprint
            // 
            this.btnFootprint.Location = new System.Drawing.Point(25, 138);
            this.btnFootprint.Name = "btnFootprint";
            this.btnFootprint.Size = new System.Drawing.Size(117, 23);
            this.btnFootprint.TabIndex = 0;
            this.btnFootprint.Text = "Footprint Application";
            this.btnFootprint.UseVisualStyleBackColor = true;
            this.btnFootprint.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(180, 21);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(346, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://localhost:2681/DenimGroup.Sprajax.Atlas.DemoSite/Default.aspx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application URL:";
            // 
            // chkStoreAll
            // 
            this.chkStoreAll.AutoSize = true;
            this.chkStoreAll.Location = new System.Drawing.Point(180, 58);
            this.chkStoreAll.Name = "chkStoreAll";
            this.chkStoreAll.Size = new System.Drawing.Size(170, 17);
            this.chkStoreAll.TabIndex = 4;
            this.chkStoreAll.Text = "Store All Results (or just Errors)";
            this.chkStoreAll.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Database Connection String:";
            // 
            // txtDbConnString
            // 
            this.txtDbConnString.Location = new System.Drawing.Point(180, 92);
            this.txtDbConnString.Name = "txtDbConnString";
            this.txtDbConnString.PasswordChar = '*';
            this.txtDbConnString.Size = new System.Drawing.Size(346, 20);
            this.txtDbConnString.TabIndex = 6;
            this.txtDbConnString.Text = "Data Source=(local);Initial Catalog=sprajax;User Id=sprajax;Password=sprajax;";
            // 
            // btnFuzz
            // 
            this.btnFuzz.Enabled = false;
            this.btnFuzz.Location = new System.Drawing.Point(219, 138);
            this.btnFuzz.Name = "btnFuzz";
            this.btnFuzz.Size = new System.Drawing.Size(117, 23);
            this.btnFuzz.TabIndex = 7;
            this.btnFuzz.Text = "Fuzz Application";
            this.btnFuzz.UseVisualStyleBackColor = true;
            this.btnFuzz.Click += new System.EventHandler(this.btnFuzz_Click);
            // 
            // btnShowResults
            // 
            this.btnShowResults.Enabled = false;
            this.btnShowResults.Location = new System.Drawing.Point(409, 138);
            this.btnShowResults.Name = "btnShowResults";
            this.btnShowResults.Size = new System.Drawing.Size(117, 23);
            this.btnShowResults.TabIndex = 8;
            this.btnShowResults.Text = "Show Results";
            this.btnShowResults.UseVisualStyleBackColor = true;
            this.btnShowResults.Click += new System.EventHandler(this.btnShowResults_Click);
            // 
            // SprajaxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 182);
            this.Controls.Add(this.btnShowResults);
            this.Controls.Add(this.btnFuzz);
            this.Controls.Add(this.txtDbConnString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkStoreAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnFootprint);
            this.Name = "SprajaxForm";
            this.Text = "Sprajax (by Denim Group www.denimgroup.com/sprajax)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFootprint;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkStoreAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDbConnString;
        private System.Windows.Forms.Button btnFuzz;
        private System.Windows.Forms.Button btnShowResults;
    }
}

