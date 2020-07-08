using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CleanSolution.Properties;

namespace CleanSolution
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            folderBrowserDialog.SelectedPath = textboxSolutionDir.Text = Settings.Default.SolutionDir;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var dialogResult = folderBrowserDialog.ShowDialog();
            switch (dialogResult)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    Settings.Default.SolutionDir = textboxSolutionDir.Text = folderBrowserDialog.SelectedPath;
                    Settings.Default.Save();
                    break;
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            Cleanup(Settings.Default.SolutionDir);
        }

        private void Cleanup(string solutionDirectory)
        {
            ClearOutput();
            var directory = new DirectoryInfo(solutionDirectory);
            var directories = new[] { "bin", "obj" };
            
            foreach (var projectDirectory in directory.GetDirectories())
            {
                AddOutput(projectDirectory.Name);

                foreach (var builddir in projectDirectory.GetDirectories().Where(d => directories.Contains(d.Name))) builddir.Delete(true);
            }

            AddOutput("Clean is done");
        }

        private void AddOutput(string text)
        {
            output.AppendText(text);
            output.AppendText(Environment.NewLine);
        }

        private void ClearOutput()
        {
            output.Text = string.Empty;
        }
    }
}