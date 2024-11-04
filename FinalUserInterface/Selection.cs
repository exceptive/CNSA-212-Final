using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalUserInterface
{
    public partial class Selection : Form
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Incidents incidentsForm = new Incidents();

            incidentsForm.Show();
        }

        private void buttonCompanies_Click(object sender, EventArgs e)
        {
            Companies companiesForm = new Companies();

            companiesForm.Show();
        }

        private void buttonRailroads_Click(object sender, EventArgs e)
        {
            Railroads railroadsForm = new Railroads();

            railroadsForm.Show();
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            Maintenance maintenanceForm = new Maintenance();

            maintenanceForm.Show();
        }
    }
}
