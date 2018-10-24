using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using System.Linq;

public class DialogueParser : MonoBehaviour {

    List<DialogueLine> lines;

    struct DialogueLine
    {
        string name;
        string content;
        int pose;

        public DialogueLine (string n, string c, int p)
        {
            name = n;
            content = c;
            pose = p;
        }
    }

	// Use this for initialization
	void Start () {
        string file = "Dialogue";
        string sceneNum =  EditorApplication.currentScene;
        sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
        file += sceneNum;
        file += ".txt";

        LoadDialogue(file);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void LoadDialogue(string filename)
    {
        string file = "Assests/Resources/" + filename;
        string line;
        StreamReader r = new StreamReader(file);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if(line != null)
                {
                    string[] line_values = SplitCsvline(line);
                    DialogueLine line_entry = new DialogueLine(line_values[0], line_values[1], int.Parse(line_values[2]));
                    lines.Add(line_entry);
                }
            }
            while (line != null);
            r.Close();
        }
    }
    /*string [] SplitCsvline(string line)
    {
        string pattern = "@"
        #region Match one value if in valid CSV string
            ;

        string[] values = (from Match m in Regex.Matches(line.pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) select m.Groups[1].Value).ToArray();
        return values;
    }*/
    string[] SplitCsvline(string line)
    {
        string pattern = "@";

        string[] values = (from Match m in Regex.Matches(line, pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) select m.Groups[1].Value).ToArray();

        return values;
    }
    
}
