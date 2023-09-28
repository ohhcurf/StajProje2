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

namespace StajProje2
{
    public partial class ConsumableForm : Form
    {
        Paths Paths = new Paths();
        public ConsumableForm()
        {
            InitializeComponent();
            alphaTrackBar.Value = 255;
        }




        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            redLabel.Text = redTrackBar.Value.ToString();
            greenLabel.Text = greenTrackBar.Value.ToString();
            blueLabel.Text = blueTrackBar.Value.ToString();
            alphaLabel.Text = alphaTrackBar.Value.ToString();
            argbPanel.BackColor = Color.FromArgb(alphaTrackBar.Value, redTrackBar.Value, greenTrackBar.Value, blueTrackBar.Value);
        }




        private void lengthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (lengthCheckBox.Checked == true)
            {
                lengthSlider.Visible = true;
            }
            if (lengthCheckBox.Checked == false)
            {
                lengthSlider.Visible = false;
            }
        }

        private void speeddownCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (speeddownCheckBox.Checked == true)
            {
                speedupCheckBox.Checked = false;
                speeddownSlider.Visible = true;
                speedupSlider.Visible = false;
            }
            if (speeddownCheckBox.Checked == false)
            {
                speedupCheckBox.Checked = true;
                speeddownSlider.Visible = false;
                speedupSlider.Visible = true;
            }

        }

        private void speedupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (speedupCheckBox.Checked == true)
            {
                speeddownCheckBox.Checked = false;
                speeddownSlider.Visible = false;
                speedupSlider.Visible = true;
            }
            if (speedupCheckBox.Checked == false)
            {
                speeddownCheckBox.Checked = true;
                speeddownSlider.Visible = true;
                speedupSlider.Visible = false;
            }
        }




        private void createButton_Click(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                MessageBox.Show("İsim girmelisin.");
                return;
            }
            if (lifetimeBox.Text == "")
            {
                MessageBox.Show("Hayat süresi girmelisin.");
                return;
            }
            if (periodBox.Text == "")
            {
                MessageBox.Show("Periyot girmelisin.");
                return;
            }
            if (pointBox.Text == "")
            {
                MessageBox.Show("Puan girmelisin.");
                return;
            }


            ConsumableClass newConsumable = new ConsumableClass()
            {
                Name = nameBox.Text,
                Description = descRichTextBox.Text,
                Lifetime = float.Parse(lifetimeBox.Text),
                SpawnRate = float.Parse(periodBox.Text),
                Point = float.Parse(pointBox.Text),
                Color = Color.FromArgb(alphaTrackBar.Value, redTrackBar.Value, greenTrackBar.Value, blueTrackBar.Value),
                Expand = 0,
                SpeedDown = 0,
                SpeedUp = 0,
            };
            if (lengthCheckBox.Checked == true) newConsumable.Expand = lengthSlider.Value;
            if (speeddownCheckBox.Checked == true) newConsumable.SpeedDown = speeddownSlider.Value;
            if (speedupCheckBox.Checked == true) newConsumable.SpeedUp = speedupSlider.Value;

            try
            {
                string metin = newConsumable.Name + ";" + newConsumable.Lifetime + ";" + newConsumable.SpawnRate + ";" + newConsumable.Point + ";" + newConsumable.Color + ";" +
                    newConsumable.Description + ";" + newConsumable.Expand + ";" + newConsumable.SpeedDown + ";" + newConsumable.SpeedUp;
                File.AppendAllText(Paths.ConsPath, Environment.NewLine + metin);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            nameBox.Text = "";
            descRichTextBox.Text = "";
            lifetimeBox.Text = "";
            periodBox.Text = "";
            pointBox.Text = "";
            redTrackBar.Value = 0;
            greenTrackBar.Value = 0;
            blueTrackBar.Value = 0;
            alphaTrackBar.Value = 255;
            argbPanel.BackColor = Color.FromArgb(alphaTrackBar.Value, redTrackBar.Value, greenTrackBar.Value, blueTrackBar.Value);
            redLabel.Text = "0";
            greenLabel.Text = "0";
            blueLabel.Text = "0";
            alphaLabel.Text = "255";

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            AdminForm form = new AdminForm();
            form.Show();
            this.Close();
        }

        private void KeyPress_OnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
