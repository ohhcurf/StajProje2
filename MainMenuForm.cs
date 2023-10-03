using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using StajProje2.Classes;
using StajProje2.Classes.StajProje2.Classes;
using static System.Windows.Forms.DataFormats;

namespace StajProje2
{
    public partial class MainMenuForm : Form
    {
        static int lineNum;
        Paths Paths = new Paths();

        static MapClass selectedMap = new MapClass();

        public MainMenuForm()
        {
            InitializeComponent();

            lineNum = 0;

            if (File.Exists(Paths.MapsPath))
            {
                Console.WriteLine("Dosya zaten mevcut.");
            }
            else
            {
                try
                {
                    File.WriteAllText(Paths.FolderPath, "default;deault.png");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }

            MapClass map = Paths.ReadData_Map(1, 0);
            Image originalImage = Image.FromFile(Path.Combine(Paths.FolderPath, map.Image));
            levelPicture.Image = ScaleImage(originalImage, levelPicture.Size);
            levelNameLabel.Text = map.Name;
            selectedMap = map;
        }




        private void nextButton_Click(object sender, EventArgs e)
        {
            var map = Paths.ReadData_Map(1, 0);
            if (map == null)
            {
                map = Paths.ReadData_Map(1, 2);
            }
            Image originalImage = Image.FromFile(Path.Combine(Paths.FolderPath, map.Image));
            levelPicture.Image = ScaleImage(originalImage, levelPicture.Size);
            levelNameLabel.Text = map.Name;
            selectedMap = map;
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            MapClass map = new MapClass();

            if (Paths.lineNum == 1)
            {
                map = Paths.ReadData_Map(1, 1);
            }
            else
            {
                Paths.lineNum -= 2;
                map = Paths.ReadData_Map(1, 0);
            }
            if (map == null)
            {
                Paths.lineNum = 0;
                map = Paths.ReadData_Map(1, 0);
            }
            Image originalImage = Image.FromFile(Path.Combine(Paths.FolderPath, map.Image));
            levelPicture.Image = ScaleImage(originalImage, levelPicture.Size);
            levelNameLabel.Text = map.Name;
            selectedMap = map;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (usernameBox.Text == "")
            {
                MessageBox.Show("Bir isim gir!");
                return;
            }

            Form1 form = new Form1(selectedMap, usernameBox.Text);
            form.Show();
            this.Hide();
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            form.Show();
            this.Hide();
        }




        // Fotoğrafın boyutlarını, panel boyutuna getirir
        private Image ScaleImage(Image image, Size size)
        {
            Bitmap scaledImage = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, size.Width, size.Height);
            }
            return scaledImage;
        }

        private void usernameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}