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
        private readonly List<Panel> mineList = new List<Panel>();
        public Random r = new Random();
        public Point p;
        public bool start;
        public bool cheat;
        public int score;
        public int x = 10;
        public int y = 34;
        public int dist = 24;

        public MineForm()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i < 10)
                    p = new Point(x + (i * dist), 34);
                else if (i < 20)
                    p = new Point(x + (i - 10) * dist, y + dist * 1); 
                else if (i < 30)
                    p = new Point(x + (i - 20) * dist, y + dist * 2); 
                else if (i < 40)
                    p = new Point(x + (i - 30) * dist, y + dist * 3);
                else if (i < 50)
                    p = new Point(x + (i - 40) * dist, y + dist * 4);
                else if (i < 60)
                    p = new Point(x + (i - 50) * dist, y + dist * 5);
                else if (i < 70)
                    p = new Point(x + (i - 60) * dist, y + dist * 6);
                else if (i < 80)
                    p = new Point(x + (i - 70) * dist, y + dist * 7);
                else if (i < 90)
                    p = new Point(x + (i - 80) * dist, y + dist * 8);
                else
                    p = new Point(x + (i - 90) * dist, y + dist * 9);
                Panel panel = new Panel
                {
                    BackColor = SystemColors.Control,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = p,
                    Name = "panel" + i,
                    Size = new Size(20, 20)
                };
                panel.Click += new EventHandler(Panel_Click);
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
                    cheat = true;
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            if (start)
            {
                if (mineList.Contains(sender))
                {
                    foreach (var mitem in mineList)
                    {
                        mitem.Enabled = false;
                        mitem.BackColor = Color.Red;
                    }
                    start = false;
                    MessageBox.Show($"Bumm... Pont: {100 - mineList.Count}/{score}", this.Text);
                }
                else
                {
                    Panel senderpanel = sender as Panel;
                    senderpanel.Enabled = false;
                    senderpanel.BackColor = Color.Green;
                    score++;
                    labelPont.Text = score.ToString();
                    if (score == (100 - mineList.Count))
                    {
                        start = false;
                        foreach (var mitem in mineList)
                        {
                            mitem.BackColor = Color.Yellow;
                        }
                        MessageBox.Show($"Nyertél! Pont: {100 - mineList.Count}/{score}", this.Text);
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

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            score = 0;
            labelPont.Text = score.ToString();
            buttonStart.Text = "Újra";
            foreach (Panel panel in Controls.OfType<Panel>())
            {
                panel.Enabled = true;
                panel.BackColor = Color.White;
            }
            int mineCount = Convert.ToInt32(numericUpDown.Value);
            PlaceMines(mineCount);
            start = true;
        }
        private void PlaceMines(int value)
        {
            if (mineList.Count != 0)
            {
                mineList.Clear();
            }
            List<Panel> panels = this.Controls.OfType<Panel>().ToList();
            for (int i = 0; i < value; i++)
            {
                Panel rndPanel = panels[r.Next(panels.Count)];
                if (!mineList.Contains(rndPanel))
                {
                    mineList.Add(rndPanel);
                    if (cheat)
                    {
                        rndPanel.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    i--;
                    continue;
                }
            }
        }
    }
}
