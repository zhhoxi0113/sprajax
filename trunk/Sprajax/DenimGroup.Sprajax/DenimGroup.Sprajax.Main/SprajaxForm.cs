using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using DenimGroup.Sprajax.Engine;

namespace DenimGroup.Sprajax.Main
{
    public partial class SprajaxForm : Form
    {
        private SprajaxSession _mySession;

        public SprajaxForm()
        {
            InitializeComponent();
        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            _mySession = new SprajaxSession(this.txtUrl.Text, this.txtDbConnString.Text, this.chkStoreAll.Checked);
            this.btnFootprint.Enabled = false;
            _mySession.RunFootprint();
            this.btnFuzz.Enabled = true;
        }        

        private void btnFuzz_Click(object sender, EventArgs e)
        {
            this.btnFuzz.Enabled = false;
            _mySession.RunFuzz();
            this.btnShowResults.Enabled = true;
        }

        private void btnShowResults_Click(object sender, EventArgs e)
        {
            SessionResultsForm resultsForm = new SessionResultsForm(_mySession);
            resultsForm.Show(this);
        }

        
    }
}