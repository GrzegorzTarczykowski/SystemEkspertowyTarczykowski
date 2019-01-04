using System;
using System.Windows.Input;

namespace SystemEkspertowyTarczykowski.ViewModels
{
    class MainWindowButtonHandler
    {
        private ICommand buttonLoadXmlFromFile;
        private ICommand buttonGetClassOfTheMostSuitableMushroom;
        public Action LoadDataFromXmlDocumentAction { get; set; }
        public Action GetClassOfTheMostSuitableMushroomAndDisplayMessageAction { get; set; }
        public ICommand ButtonLoadXmlFromFile
        {
            get
            {
                return buttonLoadXmlFromFile 
                    ?? (buttonLoadXmlFromFile = new CommandHandler(LoadDataFromXmlDocumentAction, true));
            }
        }
        public ICommand ButtonGetClassOfTheMostSuitableMushroom
        {
            get
            {
                return buttonGetClassOfTheMostSuitableMushroom 
                    ?? (buttonGetClassOfTheMostSuitableMushroom = new CommandHandler(GetClassOfTheMostSuitableMushroomAndDisplayMessageAction, true));
            }
        }
    }
}
