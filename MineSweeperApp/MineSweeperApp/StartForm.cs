using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeperApp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            if (Debugger.IsAttached)
            {
                checkBoxCheat.Visible = true;
            }
            for (int i = 10; i <= 32; i += 2)
            {
                comboBox.Items.Add($"{i}x{i} pálya");
            }
            comboBox.SelectedIndex = 0;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            bool cheat = checkBoxCheat.Checked;
            int quantity = int.Parse(comboBox.SelectedItem.ToString().Substring(0, 2));
            this.Hide();
            MineForm mineform = new MineForm(quantity, cheat);
            mineform.ShowDialog();
            this.Close();
        }
    }
}
