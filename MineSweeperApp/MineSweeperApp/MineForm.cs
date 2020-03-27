using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeperApp
{
    public partial class MineForm : Form
    {

        private List<Panel> mineList = new List<Panel>();
        private bool Start { get; set; }
        private int Pont { get; set; }

        public MineForm()
        {
            InitializeComponent();
        }

        private void panel_Click(object sender, EventArgs e)
        {
            if (Start)
            {
                Panel senderpanel = (Panel)sender;
                senderpanel.Enabled = false;
                senderpanel.BackColor = Color.Green;
                Pont++;

                foreach (var item in mineList)
                {
                    if (sender == item)
                    {
                        foreach (var mitem in mineList)
                        {
                            mitem.Enabled = false;
                            mitem.BackColor = Color.Red;
                        }
                        Pont--;
                        Start = false;
                        MessageBox.Show($"Bumm... Pont: {Pont}", this.Text);
                        break;
                    }
                }
                
                labelPont.Text = Pont.ToString();
                if (Pont == (100 - mineList.Count))
                {
                    Start = false;
                    MessageBox.Show("Nyertél!", this.Text);
                }
            }
            else
            {
                MessageBox.Show("Kérlek indítsd el a játékot először!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int darab = (int)numericUpDown.Value;
            List<string> mineName = new List<string>();
            for (int i = 0; i < darab; i++)
            {
                int tmp_rnd = r.Next(1, 101);
                string tmp_name = "panel" + tmp_rnd;
                if (!mineName.Contains(tmp_name))
                {
                    mineName.Add(tmp_name);
                }
                else
                {
                    i--;
                }
            }
            mineList.Clear();
            foreach (Panel panel in this.Controls.OfType<Panel>())
            {
                panel.BackColor = Color.White;
                panel.Enabled = true;
                foreach (var mn in mineName)
                {
                    if (panel.Name == mn)
                    {
                        mineList.Add(panel);
                    }
                }
            }
            Pont = 0;
            labelPont.Text = Pont.ToString();
            buttonStart.Text = "Újra";
            Start = true;
        }
    }
}
