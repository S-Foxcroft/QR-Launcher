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
        }
        private void LoadTasks()
        {
            comboBox1.Items.Clear();
            foreach(string s in Prefs.GetTaskNames()) comboBox1.Items.Add(s);
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
                Prefs.AddTask(TaskName.Text, textBox1.Text);
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
            listBox1.SelectedIndex = pos;
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
            e.Cancel = true;
            Hide();
        }

        private void TaskName_Leave(object sender, EventArgs e)
        {
            //update name
            string s = TaskName.Text;
            Prefs.Rename(comboBox1.Text, s);
            LoadTaskData();
            comboBox1.SelectedText = s;
        }
    }
}
