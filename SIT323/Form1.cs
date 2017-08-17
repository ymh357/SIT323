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
                String crozzleHTML = @"<!DOCTYPE html>
                                <html>
                                <head>
                                <style>
                                table, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                td {
                                    width:24px;
                                    text-align: center;
                                }
                                </style>
                                </head>
                                <body>
                                <table>";

                for (int row = 0; row < crozzleGrid.GetLength(0); row++)
                {
                    String tr = "<tr>";

                    for (int column = 0; column < crozzleGrid.GetLength(1); column++)
                        tr += @"<td>" + crozzleGrid[row, column] + @"</td>";
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

                scoreTextBox.Text = validater.Score.ToString();
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
