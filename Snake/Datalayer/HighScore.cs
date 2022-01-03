using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Globals
{
    public class HighScore
    {
        private readonly IDictionary<string, int> playerAndScores;
        
        public HighScore()
        {
            playerAndScores = new Dictionary<string, int>();
            
        }

        public void NewPlayerCheck(string player, int score)
        {
            player.ToLower();
            if (!playerAndScores.ContainsKey(player))
            {
                playerAndScores.Add(player, score);
            }
            else
            {
                foreach (KeyValuePair<string, int> playerExists in playerAndScores)
                {
                    if (playerExists.Key == player)
                    {
                        if (score > playerExists.Value)
                        {
                            playerAndScores[playerExists.Key] = score;
                        }
                    }
                }
            }
        }

        public void ValuesToDoc()
        {
                foreach (KeyValuePair<string, int> playerWithScores in playerAndScores)
                {

                //if (!File.Exists(System.IO.Path.Combine(SpecialDirectories.Desktop, "SnakeScores.txt")))
                //{
                //    throw new FileNotFoundException("Scores could not be saved (file not found) try again.");
                //}
                System.IO.File.AppendAllText(System.IO.Path.Combine(SpecialDirectories.Desktop, "SnakeScores.txt"),
                           string.Format("__________\nPlayer: {0}Score:{1}\n" + playerAndScores.Count, playerWithScores.Key, playerWithScores.Value));
            }
        }
    }
}

                    //if (!System.IO.Path.Combine(SpecialDirectories.Desktop, "SnakeScores.txt").Contains(playerWithScores.Key))
                    //{
                    //}