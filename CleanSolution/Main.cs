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
            output.Text = string.Empty;
            var directory = new DirectoryInfo(solutionDirectory);
            var builddirs = new[] { "bin", "obj" };


            foreach (var projectDirectory in directory.GetDirectories())
            {
                output.AppendText(projectDirectory.Name);
                output.AppendText(Environment.NewLine);
                Console.WriteLine(projectDirectory.Name);

                foreach (var builddir in projectDirectory.GetDirectories().Where(d => builddirs.Contains(d.Name))) builddir.Delete(true);
            }
        }
    }
}