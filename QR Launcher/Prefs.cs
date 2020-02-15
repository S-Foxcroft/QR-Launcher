using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QR_Launcher
{
    class Prefs
    {
        private static Dictionary<String, String> Pairs = null;
        private static Dictionary<String, List<String>> Tasks = null;
        public static void Load()
        {
            if (Pairs == null) Pairs = new Dictionary<string, string>();
            else Pairs.Clear();
            if (Tasks == null) Tasks = new Dictionary<string, List<string>>();
            else Tasks.Clear();
            using (StreamReader sr = new StreamReader(File.OpenRead("prefs.ini")))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    int pos;
                    if ((pos = line.IndexOf("=")) > 0)
                    {
                        Pairs.Add(line.Substring(0, pos), line.Substring(pos + 1));
                    }
                }
            }
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
                            if(sectionName != "")
                            {
                                Tasks.Add(sectionName, sectionContent);
                            }
                        }
                        else sectionContent.Add(line);
                    }
                }
            }
        }
        public static void Save()
        {
            using (StreamWriter sw = new StreamWriter(File.OpenWrite("prefs.ini"))) {
                StringBuilder sb = new StringBuilder();
                foreach(KeyValuePair<string,string> row in Pairs)
                {
                    sb.AppendLine(row.Key+"="+row.Value);
                }
                sw.Write(sb.ToString());
            }
            using (StreamWriter sw = new StreamWriter(File.OpenWrite("tasks.ini"))) { }
        }
        public static string GetPref(string s, string S)
        {
            if (Pairs.ContainsKey(s)) return Pairs[s];
            return S;
        }
        public static List<string> GetTask(string s)
        {
            if (Pairs.ContainsKey(s)) return Tasks[s];
            return new List<string>();
        }
        public static string NewTaskSet(string s) {
            if (Tasks.ContainsKey(s)) s = "Task " + (Tasks.Count+1);
            Tasks.Add(s, new List<string>());
            return s;
        }
        public static void Rename(string s, string S)
        {
            
        }
        public static void AddTask(string s, string S) { Tasks[s].Add(S); }
        public static void DropTask(string s, int i) { Tasks[s].RemoveAt(i); }
        public static string[] GetTaskNames() {
            string[] res = new string[Tasks.Count];
            int i = 0;
            foreach (string s in Tasks.Keys) res[i++] = s;
            return res;
        }
    }
}
