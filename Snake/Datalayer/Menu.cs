using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Globals
{
    public class Menu
    {
        public List<string> Options { get; set; }
        public int Index { get; set; }
        private string fullPath;
        public string IntroTxt { get;  set; }
        
        public Menu()
        {
            Options = new List<string>() { "Play", "Scores", "Exit" };    
                ReadIntroFile();
        }

        private void ReadIntroFile()
        {
            string allText;
            fullPath = Path.GetFullPath(@"./Documents/IntroText.txt");
            if (!fullPath.EndsWith("IntroText.txt"))
            {
                IntroTxt = "Could not load file of title drawing...\n";
            }
            else
            {
                using StreamReader reader = new StreamReader(fullPath, Encoding.ASCII);
                while ((allText = reader.ReadLine()) != null)
                {
                    allText = reader.ReadToEnd();
                    IntroTxt = allText;
                }
            }
        }
    }
}
//properties of menu
//1.) needs options
//2.)options need to be selected/changed