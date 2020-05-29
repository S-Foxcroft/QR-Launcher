using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QR_Launcher
{
    public partial class TaskBox : Form
    {
        public static TaskBox Instance;
        public TaskBox()
        {
            InitializeComponent();
            LoadTasks();
        }
        private void LoadTasks()
        {
            comboBox1.Items.Clear();
            foreach(string s in Prefs.GetTaskNames()) comboBox1.Items.Add(s);
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }
        private void LoadTaskData()
        {
            List<string> tasks = Prefs.GetTask(comboBox1.Text);
            TaskName.Text = comboBox1.Text;
            listBox1.Items.Clear();
            foreach (string task in tasks) listBox1.Items.Add(task);
         }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e) //New Task Entry
        {
            if (e.KeyCode == Keys.Enter)
            {
                Prefs.AddTask(comboBox1.Text, textBox1.Text.Replace("    ","\t"));
                textBox1.Text = "";
                textBox1.Visible = false;
                LoadTaskData();
            }
                
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Remove
            int pos = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(pos);
            Prefs.DropTask(TaskName.Text, pos);
            if(pos != 0)
            listBox1.SelectedIndex = pos-1;
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Add
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e){ if (!linkLabel3.Enabled) linkLabel3.Enabled = true; }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //New
            string name = Prefs.NewTaskSet("Task 1");
            LoadTasks();
            comboBox1.SelectedText = name;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTaskData();
        }

        private void TaskBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            Prefs.Save();
        }

        private void TaskName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string s = TaskName.Text;
                Prefs.Rename(comboBox1.Text, s);
                LoadTasks();
                comboBox1.Text = s;
                LoadTaskData();
            }
            
        }
    }
}
