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
        private readonly List<Panel> mineList = new List<Panel>();
        public bool Start { get; set; }
        public int Score { get; set; }
        public bool Cheat { get; set; }

        public MineForm(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0)
            {
                if (args.Length == 1 && args[0] == "Cheat")
                {
                    Cheat = true;
                }
                else
                {
                    MessageBox.Show("Argumentum hiba!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void MineForm_Load(object sender, EventArgs e)
        {
            using (InputForm input = new InputForm(Cheat))
            {
                if (input.ShowDialog() == DialogResult.OK) //InputForm-ban a gomb DialogResult-ja OK-ra van téve
                {
                    Cheat = input.Cheat;
                    CreateMineFields(input.Quantity);
                    this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); //Form átméretezése a panelok miatt, AutoSize True-ra állítva
                    this.CenterToScreen(); //Az új méret miatt a CenterScreen már nincs pontosan középen, újra középre kell igazítani
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            if (Start)
            {
                int maxCount = Controls.OfType<Panel>().Count();
                if (mineList.Contains(sender))
                {
                    Start = false;
                    foreach (Panel panel in mineList)
                    {
                        panel.BackColor = Color.Red;
                        panel.Enabled = false;
                    }
                    MessageBox.Show($"Bumm... Pont: {maxCount - mineList.Count}/{Score}", this.Text);
                }
                else
                {
                    Panel senderpanel = sender as Panel;
                    senderpanel.Enabled = false;
                    senderpanel.BackColor = Color.Green;
                    Score++;
                    textBoxPont.Text = Score.ToString("000");
                    if (Score == (100 - mineList.Count))
                    {
                        Start = false;
                        mineList.ForEach(x => x.BackColor = Color.Yellow);
                        MessageBox.Show($"Nyertél! Pont: {maxCount - mineList.Count}/{Score}", this.Text);
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
            Score = 0;
            textBoxPont.Text = Score.ToString("000");
            buttonStart.Text = "Újra";
            foreach (Panel panel in Controls.OfType<Panel>())
            {
                panel.Enabled = true;
                panel.BackColor = Color.White;
            }
            int mineCount = Convert.ToInt32(numericUpDown.Value);
            PlaceMines(mineCount);
            Start = true;
        }

        private void CreateMineFields(int quantity)
        {
            numericUpDown.Minimum = quantity;
            numericUpDown.Maximum = quantity * quantity / 2;
            int size = 20;
            int padding = 3;
            Point startPoint = new Point(3, 30);
            for (int y = 0; y < quantity; y++)
            {
                for (int x = 0; x < quantity; x++)
                {
                    Point tmpPoint = new Point(x * (size + padding), y * (size + padding));
                    Panel panel = new Panel
                    {
                        Size = new Size(size, size),
                        Location = tmpPoint + new Size(startPoint),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = SystemColors.Control,
                        Parent = this
                    };
                    panel.Click += new EventHandler(Panel_Click);
                }
            }
        }

        private void PlaceMines(int value)
        {
            Random r = new Random();
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
                    if (Cheat)
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
