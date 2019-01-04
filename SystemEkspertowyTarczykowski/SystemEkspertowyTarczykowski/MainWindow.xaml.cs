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
using SystemEkspertowyTarczykowski.EventArgs;
using SystemEkspertowyTarczykowski.ViewModels;

namespace SystemEkspertowyTarczykowski
{
    public partial class MainWindow : Window
    {
        private ProgramSession programSession;
        public MainWindow()
        {
            InitializeComponent();
            programSession = new ProgramSession();
            programSession.OnMessageRaised += OnGameMessageRaised;
            DataContext = programSession; 
        }

        private void OnGameMessageRaised(object sender, LogMessageEventArgs e)
        {
            LogMessage.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            LogMessage.ScrollToEnd();
        }
    }
}
