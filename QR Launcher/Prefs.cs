﻿using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QR_Launcher
{
    class Prefs
    {
        private static Dictionary<string, string> Pairs = null;
        public static Dictionary<string, string> Replacements = null;
        private static Dictionary<string, List<string>> Tasks = null;
        public static void Load()
        {
            if (Pairs == null) Pairs = new Dictionary<string, string>();
            else Pairs.Clear();
            if (Replacements == null) Replacements = new Dictionary<string, string>();
            else Replacements.Clear();
            if (Tasks == null) Tasks = new Dictionary<string, List<string>>();
            else Tasks.Clear();
            if(File.Exists("prefs.ini"))
            using (StreamReader sr = new StreamReader(File.OpenRead("prefs.ini")))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    int pos;
                    if ((pos = line.IndexOf("=")) > 0)
                        if (line.StartsWith("s:")) Replacements.Add(line.Substring(0, pos), line.Substring(pos + 1));
                        else Pairs.Add(line.Substring(0, pos), line.Substring(pos + 1));
                }
            }
            if (File.Exists("tasks.ini"))
            using (StreamReader sr = new StreamReader(File.OpenRead("tasks.ini")))
            {
                string sectionName = "";
                List<string> sectionContent = new List<string>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (!line.StartsWith("#")) {
                        if (line.StartsWith("["))
                        {
                            if(sectionName != "") Tasks.Add(sectionName, sectionContent);
                            sectionContent = new List<string>();
                            sectionName = line.Substring(1,line.Length-2);
                        }
                        else sectionContent.Add(line);
                    }
                }
                Tasks.Add(sectionName, sectionContent);
            }
        }
        public static void Save()
        {
            StringBuilder sb = new StringBuilder();
            using (StreamWriter sw = new StreamWriter(File.OpenWrite("prefs.ini"))) {                
                foreach(KeyValuePair<string,string> row in Pairs)
                {
                    sb.AppendLine(row.Key+"="+row.Value);
                }
                sw.Write(sb.ToString());
            }
            using (StreamWriter sw = new StreamWriter(File.OpenWrite("tasks.ini"))) {
                sb.Clear();
                foreach(KeyValuePair<string,List<string>> row in Tasks)
                {
                    sb.AppendLine("[" + row.Key +"]");
                    foreach (string task in row.Value) sb.AppendLine(task);
                }
                sw.Write(sb.ToString());
            }
        }
        public static string GetPref(string s, string S)
        {
            if (Pairs.ContainsKey(s)) return Pairs[s];
            return S;
        }
        public static List<string> GetTask(string s)
        {
            if (Tasks.ContainsKey(s)) return Tasks[s];
            return new List<string>();
        }
        public static void SetCamera(FilterInfo f)
        {
            Pairs["camera"] = f.Name+"{"+f.MonikerString+"}";
        }
        public static string NewTaskSet(string s) {
            if (Tasks.ContainsKey(s)) s = "Task " + (Tasks.Count+1);
            Tasks.Add(s, new List<string>());
            return s;
        }
        public static void Rename(string s, string S)
        {
            List<string> task = GetTask(s);
            Tasks.Remove(s);
            Tasks.Add(S, task);
        }
        public static void AddTask(string s, string S) {
            List<string> entry = GetTask(s);
            entry.Add(S);
            Tasks[s] = entry;
        }
        public static void DropTask(string s, int i) {
            List<string> entry = GetTask(s);
            entry.RemoveAt(i);
            Tasks[s] = entry;
        }
        public static string[] GetTaskNames() {
            string[] res = new string[Tasks.Count];
            int i = 0;
            foreach (string s in Tasks.Keys) res[i++] = s;
            return res;
        }
    }
}
