using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
namespace GACViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker m_bgw = new BackgroundWorker();
        private string m_fileContent = string.Empty;
        private ObservableCollection<AssemblyObject> m_Assemblies = new ObservableCollection<AssemblyObject>();
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "GAC Viewer Loading...";
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            grid_GAC.Margin = new Thickness(0, 0, 0, 0);
            
            if(m_Assemblies == null) m_Assemblies = new ObservableCollection<AssemblyObject>();
            m_Assemblies.Clear();

            string[] arrayoo = m_fileContent.Split(new char[] { '\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
     
            foreach (var line in arrayoo)
            {
                if (line.Contains("Version") &&
                    line.Contains("Culture") &&
                    line.Contains("PublicKeyToken"))
                {
                    AssemblyObject ao = new AssemblyObject();
                    ao.AssemblyName = line.Substring(0, line.IndexOf(','));
                    ao.Version = Regex.Match(line, "(?<=Version=).+?(?=,)", RegexOptions.IgnoreCase).Value;
                    ao.Culture = Regex.Match(line, "(?<=Culture=).+?(?=,)", RegexOptions.IgnoreCase).Value;
                    ao.PublicKeyToken = Regex.Match(line, "(?<=PublicKeyToken=).+?(?=,)", RegexOptions.IgnoreCase).Value;
                    ao.ProcessorArchitecture = Regex.Match(line, "(?<=processorArchitecture=).+", RegexOptions.IgnoreCase).Value;

                    m_Assemblies.Add(ao);
                }
            }
            this.Title = "GAC Viewer (" + m_Assemblies.Count + ")";
            grid_GAC.DataContext = m_Assemblies;
        
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string executionFile = System.Environment.CurrentDirectory + @"\gacutil.exe ";
            m_fileContent = Util.Execute(executionFile, " /l");
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_bgw.DoWork += bgw_DoWork;
            m_bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            m_bgw.RunWorkerAsync();
        }
    }
}
