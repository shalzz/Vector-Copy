using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Multi_Drive_Copy_Utiliy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // click the refresh button on form initialization
            copyFile.PerformClick();
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            // Open a openFileDialog and select a file
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void copyFile_Click(object sender, EventArgs e)
        {
            // Get the file name
            String sourceFile = textBox1.Text;
            int index = sourceFile.LastIndexOf("\\");
            String fileName = sourceFile.Substring(index + 1);

            // For Manual selection
            if (radioButton1.Checked)
            {
                // Copy file into each Selected Drive
                foreach (Object selecteditem in listBox1.SelectedItems)
                {
                    // Extract the Drive letter from ListBox Item
                    String item = selecteditem as String;
                    int itemIndex = item.LastIndexOf("\\");
                    String driveLetter = item.Substring(0, itemIndex);
                    // Form the complete address
                    String destFile = driveLetter + "\\" + fileName;
                    File.Copy(sourceFile, destFile, true);
                }
                MessageBox.Show("Copy Complete!");
            }

            // For automatic selection
            if (radioButton2.Checked)
            {
                if (radioButton3.Checked)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo Drive in drives)
                    {
                        try
                        {
                            if (Drive.DriveType == DriveType.Removable)
                                File.Copy(sourceFile, Drive.Name + "\\" + fileName, true);
                        }
                        catch(Exception ex)
                        {
                                MessageBox.Show(ex.Message);
                        }
                    }
                    MessageBox.Show("Copy Complete!");
                }
                else if (radioButton4.Checked)
                {
                     DriveInfo[] drives = DriveInfo.GetDrives();
                     foreach (DriveInfo Drive in drives)
                    {
                        try
                        {
                            if (Drive.DriveType == DriveType.Fixed)
                                File.Copy(sourceFile, Drive.Name + "\\" + fileName, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    MessageBox.Show("Copy Complete!");
                }
                else if (radioButton5.Checked)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo Drive in drives)
                    {
                        try
                        {
                            if (Drive.DriveType == DriveType.CDRom)
                                File.Copy(sourceFile, Drive.Name + "\\" + fileName, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    MessageBox.Show("Copy Complete!");
                }
                else
                {
                    MessageBox.Show("Please select an Option");
                }
            }
        }

        private void refreshList_Click(object sender, EventArgs e)
        {
            // Get a list of drives and display them
            listBox1.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo Drive in drives)
            {
                listBox1.Items.Add(Drive.Name + "\tType: " + Drive.DriveType);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Change the UI
            listBox1.Enabled = true;
            refreshList.Enabled = true;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Change the UI
            listBox1.Enabled = false;
            refreshList.Enabled = false;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
        }
       
    }
}
