using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CarMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Nuskaitoma informacija iš failų
            string[] markeLines = File.ReadAllLines("marke.txt");
            string[] automobiliaiLines = File.ReadAllLines("automobiliai.txt");

            // Formuojamas Marke klasės objektų sąrašas
            List<Marke> markes = new List<Marke>();
            foreach (string str in markeLines)
            {
                string[] tmp = str.Split(';');
                Marke marke = new Marke(Convert.ToInt32(tmp[0]), tmp[1]);
                markes.Add(marke);
            }

            // Formuojami Automobilis klasės objektai ir priskiriami attinkamam markės objektui
            foreach (string str in automobiliaiLines)
            {
                string[] tmp = str.Split(';');
                Automobilis auto = new Automobilis(Convert.ToInt32(tmp[1]), tmp[2], Convert.ToInt32(tmp[3]), Convert.ToInt32(tmp[4]), Convert.ToDouble(tmp[5]), tmp[6]);

                int categoryId = Convert.ToInt32(tmp[0]);
                foreach (Marke marke in markes)
                    if (marke.GetID() == categoryId)
                        marke.AddAuto(auto);
            }

            // Kiekvienas Marke tipo objektas atvaizduojamas kaip mygtukas
            int i = 0;
            foreach(Marke marke in markes)
            {
                Button markeButton = new Button();
                markeButton.Text = marke.GetPavadinimas();
                markeButton.Location = new Point(0, 50 * i);
                markeButton.Font = new Font(FontFamily.GenericSansSerif, 14);
                markeButton.Size = new Size(150, 45);
                markeButton.Click += MarkeButton_Click;
                markeButton.Tag = marke; // Mygtukui priskiriam objektą, kurį mygtukas reprezentuoja
                markesPanel.Controls.Add(markeButton);
                i++;               
            }

        }

        private void MarkeButton_Click(object sender, EventArgs e)
        {
            autosPanel.Controls.Clear();
            label1.Text = "";

            // sender - komponentas iškvietęs šį metodą, šiuo atveju, tam tikras mygtukas
            Button b = (Button)sender;
            Marke marke = (Marke)b.Tag; // Nuskaitom, kuris markės objektas buvo priskirtas

            // Kiekvienas markės automobilis yra atvaizduojamas kaip mygtukas
            int i = 0;
            foreach (Automobilis auto in marke.GetAllAutos())
            {
                Button autoButton = new Button();
                autoButton.Text = auto.GetModelis();
                autoButton.Location = new Point(155 * i, 0);
                autoButton.Font = new Font(FontFamily.GenericSansSerif, 14);
                autoButton.Size = new Size(150, 45);
                autoButton.Click += AutoButton_Click;
                autoButton.Tag = auto;
                autosPanel.Controls.Add(autoButton);
                i++;                
            }
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Automobilis auto = (Automobilis)b.Tag;
            label1.Text = auto.GetInfo();
        }
    }
}
