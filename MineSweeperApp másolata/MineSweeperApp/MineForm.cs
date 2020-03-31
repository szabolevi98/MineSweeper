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
        private List<Panel> mineList = new List<Panel>();
        private bool Cheat { get; set; } = false;
        private bool Start { get; set; } = false;
        private int Point { get; set; } = 0;

        public MineForm()
        {
            for (int i = 200; i <= 300; i++)
            {
                Panel panel = new Panel();
                panel.BackColor = SystemColors.Control;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Location = new Point(i*2, i*2);
                panel.Name = "panel" + i;
                panel.Size = new Size(20, 20);
                panel.Click += new EventHandler(this.panel_Click);

                this.Controls.Add(panel);
            }
            InitializeComponent();
        }

        private void MineForm_Load(object sender, EventArgs e)
        {
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
                if (mineList.Contains(sender))
                {
                    foreach (var mitem in mineList)
                    {
                        mitem.Enabled = false;
                        mitem.BackColor = Color.Red;
                    }
                    Start = false;
                    MessageBox.Show($"Bumm... Pont: {100 - mineList.Count}/{Point}", this.Text);
                }
                else
                {
                    Panel senderpanel = sender as Panel;
                    senderpanel.Enabled = false;
                    senderpanel.BackColor = Color.Green;
                    Point++;
                    labelPont.Text = Point.ToString();
                    if (Point == (100 - mineList.Count))
                    {
                        Start = false;
                        foreach (var mitem in mineList)
                        {
                            mitem.BackColor = Color.Yellow;
                        }
                        MessageBox.Show($"Nyertél! Pont: {100 - mineList.Count}/{Point}", this.Text);
                    }
                }
            }
            else
            {
                if (mineList.Count != 0)
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
            if (mineList.Count != 0)
            {
                mineList.Clear();
            }
            Random r = new Random();
            List<Panel> panels = this.Controls.OfType<Panel>().ToList();
            int mineNums = Convert.ToInt32(numericUpDown.Value);
            for (int i = 0; i < panels.Count; i++)
            {
                if (i < mineNums)
                {
                    Panel rndPanel = panels[r.Next(0, panels.Count)];
                    if (!mineList.Contains(rndPanel))
                    {
                        mineList.Add(rndPanel);
                    }
                    else
                    {
                        i--;
                        continue;
                    }
                }
                panels[i].Enabled = true;
                if (mineList.Contains(panels[i]) && Cheat)
                {
                    panels[i].BackColor = Color.Yellow;
                }
                else
                {
                    panels[i].BackColor = Color.White;
                }
            }
            Start = true;
        }
    }
}
