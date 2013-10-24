using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Multi_Drive_Copy_Utility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            // click the refresh button when form is loaded
            refreshList.PerformClick();

            //  Change the UI
            listBox1.Enabled = false;
            refreshList.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            // Open a openFileDialog and select a file
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private int fileCopy(String source, String destination)
        {
            try
            {
                FileSystem.CopyFile(source, destination, UIOption.AllDialogs, UICancelOption.ThrowException);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Copying Canceled!\n"+ex.Message);
                return 1;
            }
        }

        private void copyFile_Click(object sender, EventArgs e)
        {
            // Get the file name
            String sourceFile = textBox1.Text;
            int index = sourceFile.LastIndexOf("\\");
            String fileName = sourceFile.Substring(index + 1);
            int state = 0;

            if (sourceFile.Length != 0)
            {
                if (radioButton1.Checked)              // For Manual selection
                {
                    if (listBox1.SelectedIndices != null)
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
                            
                            // Call the filecopy function 
                            state = fileCopy(sourceFile,destFile);
                            if (state == 1) break;
                        }
                        if (state == 0) MessageBox.Show("Copying Complete!");
                    }
                    else
                    {
                        MessageBox.Show("Please select atleast one Drive");
                    }
                }
                else if (radioButton2.Checked)       // For automatic selection
                {
                    if (radioButton3.Checked)
                    {
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach (DriveInfo Drive in drives)
                        {
                            // Call the filecopy function
                            if (Drive.DriveType == DriveType.Removable)
                                state = fileCopy(sourceFile, Drive.Name + "\\" + fileName);
                            if (state == 1) break;
                        }
                        if (state == 0) MessageBox.Show("Copying Complete!");
                    }
                    else if (radioButton4.Checked)
                    {
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach (DriveInfo Drive in drives)
                        {
                            // Call the filecopy function
                            if (Drive.DriveType == DriveType.Fixed)
                                state = fileCopy(sourceFile, Drive.Name + "\\" + fileName);
                            if (state == 1) break;
                        }
                        if (state == 0) MessageBox.Show("Copying Complete!");
                    }
                    else if (radioButton5.Checked)
                    {
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach (DriveInfo Drive in drives)
                        {
                            // Call the filecopy function
                            if (Drive.DriveType == DriveType.CDRom)
                                state = fileCopy(sourceFile, Drive.Name + "\\" + fileName);
                            if (state == 1) break;
                        }
                        if (state == 0) MessageBox.Show("Copying Complete!");
                    }
                    else
                    {
                        MessageBox.Show("Please select a type of Drive you wish to copy to");
                    }
                }
                else
                {
                    MessageBox.Show("Please select an Option");
                }
            }
            else
            {
                MessageBox.Show("Please select a file to copy");
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Visible = true;
        }
       
    }
}
