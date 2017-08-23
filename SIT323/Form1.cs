using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void validateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Validater validater = new Validater();
                validater.ValidateCrozzleText(fileDialog.FileName);

                char[,] crozzleGrid = validater.CrozzleGrid;

                String style=validater.ConfigDic["STYLE"] != null ? validater.ConfigDic["STYLE"].Split('\"')[1] : @"<style> table, td { border: 1px solid black; border-collapse: collapse; } td { width:24px; height:18px; text-align: center; } </style>";

                String crozzleHTML = @"<!DOCTYPE html>
                                <html>
                                <head>"+
                                style
                                +
                                
                              @"</head>
                                <body>
                                <table>";

                for (int row = 0; row < crozzleGrid.GetLength(0); row++)
                {
                    String tr = "<tr>";

                    for (int column = 0; column < crozzleGrid.GetLength(1); column++)
                    {
                        String tdEmpty = validater.ConfigDic["BGCOLOUR_EMPTY_TD"] != null ? @"<td style='background-color:" + validater.ConfigDic["BGCOLOUR_EMPTY_TD"] + "'>" + crozzleGrid[row, column] + @"</td>" : @"<td>" + crozzleGrid[row, column] + @"</td>";
                        String tdNonEmpty = validater.ConfigDic["BGCOLOUR_NON_EMPTY_TD"] != null ? @"<td style='background-color:" + validater.ConfigDic["BGCOLOUR_NON_EMPTY_TD"] + "'>" + crozzleGrid[row, column] + @"</td>" : @"<td>" + crozzleGrid[row, column] + @"</td>";
                        // Empty.
                        if (crozzleGrid[row, column]=='\0')
                        {
                            tr += tdEmpty;
                        }
                        else
                        {
                            tr += tdNonEmpty;
                        }                        
                    }
                        
                    tr += "</tr>";

                    crozzleHTML += tr;
                }

                crozzleHTML += @"</table>
                             </body>
                             </html>";

                crozzleWebBrowser.DocumentText = crozzleHTML;

                String errorHTML = @"<!DOCTYPE html><html><head></head><body>";
                try
                {
                    String[] errorLines = File.ReadAllLines(validater.LogPath);
                    foreach (String line in errorLines)
                    {
                        errorHTML += "<p>" + line + "</p>";
                    }
                }
                catch (Exception)
                {
                    errorHTML += "<p>" + "LogFile path is not correct. So program cannot show the errors. Check the file you opened." + "</p>";
                }
                errorHTML += "</body></html>";

                errorWebBrowser.DocumentText = errorHTML;

                bool allValid = true;
                foreach(bool b in validater.ValidInfo)
                {
                    if (!b)
                    {
                        allValid = false;
                    }
                }
                scoreTextBox.Text = validater.Score.ToString();

                if (!allValid)
                {
                    if(validater.ConfigDic["INVALID_CROZZLE_SCORE"] != null)
                    {
                        scoreTextBox.Text += "   " + validater.ConfigDic["INVALID_CROZZLE_SCORE"];
                    }

                    
                }
                                   
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void scoreTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
