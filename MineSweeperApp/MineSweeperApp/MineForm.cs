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
        private List<Panel> MineList { get; set; } = new List<Panel>();
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
            foreach (Panel panel in this.Controls.OfType<Panel>())
            {
                panel.BackColor = Color.White;
                panel.Enabled = true;
            }
            placeMines(numericUpDown.Value);
            Start = true;
        }

        private void placeMines(decimal number)
        {
            Random r = new Random();
            if (MineList.Count != 0)
            {
                MineList.Clear();
            }
            for (int i = 0; i < number; i++)
            {
                string randomName = "panel" + r.Next(1, 101);
                foreach (Panel panel in this.Controls.OfType<Panel>())
                {
                    if (panel.Name == randomName)
                    {
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
                        break;
                    }
                }
            }
        }
    }
}
