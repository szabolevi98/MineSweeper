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
    public partial class InputForm : Form
    {
        public int Quantity { get; set; }
        public bool Cheat { get; set; }
        public InputForm(bool cheat)
        {
            InitializeComponent();
            this.Cheat = cheat;
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            if (Debugger.IsAttached || Cheat)
            {
                checkBoxCheat.Visible = true;
            }
            for (int i = 10; i <= 32; i += 2)
            {
                comboBox.Items.Add($"{i}x{i} Pálya");
            }
            comboBox.SelectedIndex = 0;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Cheat = checkBoxCheat.Checked;
            Quantity = int.Parse(comboBox.SelectedItem.ToString().Substring(0, 2));
        }
    }
}
