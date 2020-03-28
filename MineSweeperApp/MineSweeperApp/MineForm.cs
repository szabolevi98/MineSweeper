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
    public partial class MineForm : Form
    {
        private List<Panel> MineList = new List<Panel>();
        private bool Cheat { get; set; } = false;
        private bool Start { get; set; } = false;
        private int Point { get; set; } = 0;

        public MineForm()
        {
            InitializeComponent();
            if (Debugger.IsAttached)
            {
                DialogResult result = MessageBox.Show("Engedélyezed a csalást?", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Cheat = true;
                }
            }
        }

        private void panel_Click(object sender, EventArgs e)
        {
            if (Start)
            {
                for (int i = 0; i < MineList.Count; i++)
                {
                    if (sender == MineList[i])
                    {
                        foreach (var mitem in MineList)
                        {
                            mitem.Enabled = false;
                            mitem.BackColor = Color.Red;
                        }
                        Start = false;
                        MessageBox.Show($"Bumm... Pont: {100 - MineList.Count}/{Point}", this.Text);
                        break;
                    }
                    else if (i == MineList.Count-1)
                    {
                        Panel senderpanel = (Panel)sender;
                        senderpanel.Enabled = false;
                        senderpanel.BackColor = Color.Green;
                        Point++;
                        labelPont.Text = Point.ToString();
                        if (Point == (100 - MineList.Count))
                        {
                            Start = false;
                            foreach (var mitem in MineList)
                            {
                                mitem.BackColor = Color.Yellow;
                            }
                            MessageBox.Show($"Nyertél! Pont: {100 - MineList.Count}/{Point}", this.Text);
                        }
                    }
                }
            }
            else
            {
                if (buttonStart.Text == "Újra")
                {
                    MessageBox.Show("Kérlek indítsd el újra a játékot!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Kérlek indítsd el a játékot először!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Point = 0;
            labelPont.Text = Point.ToString();
            buttonStart.Text = "Újra";
            if (MineList.Count != 0)
            {
                MineList.Clear();
            }
            foreach (Panel panel in this.Controls.OfType<Panel>())
            {
                panel.BackColor = Color.White;
                panel.Enabled = true;
            }
            Random r = new Random();
            for (int i = 0; i < numericUpDown.Value; i++)
            {
                Panel panel = (Panel)this.Controls["panel" + r.Next(1, 101)];
                if (!MineList.Contains(panel))
                {
                    MineList.Add(panel);
                    if (Cheat)
                    {
                        panel.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    i--;
                }
            }
            Start = true;
        }
    }
}
