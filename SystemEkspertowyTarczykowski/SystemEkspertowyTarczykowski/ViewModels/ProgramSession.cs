using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Xml;
using SystemEkspertowyTarczykowski.EventArgs;
using SystemEkspertowyTarczykowski.Model;

namespace SystemEkspertowyTarczykowski.ViewModels
{
    class ProgramSession
    {
        public event EventHandler<LogMessageEventArgs> OnMessageRaised;
        public Basket CurrentBasket { get; set; }
        public Mushroom CurrentMushroomToCompare { get; set; }
        public MainWindowButtonHandler MainWindowButtonHandler { get; set; }
        public ProgramSession()
        {
            MainWindowButtonHandler = new MainWindowButtonHandler();
            MainWindowButtonHandler.LoadDataFromXmlDocumentAction = LoadDataFromXmlDocument;
            MainWindowButtonHandler.GetClassOfTheMostSuitableMushroomAndDisplayMessageAction = GetClassOfTheMostSuitableMushroomAndDisplayMessage;
            CurrentMushroomToCompare = new Mushroom();
            CurrentBasket = new Basket();
            CurrentBasket.ResultantClass = "Koszyk Dziala";
            CurrentBasket.ImageOK = "/SystemEkspertowyTarczykowski;component/Images/OK.png";
        }

        public void LoadDataFromXmlDocument()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(openFileDialog.FileName);
                    CurrentBasket.ListOfMushroom.Clear();
                    foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
                    {
                        Mushroom newMushroom = new Mushroom();
                        newMushroom.DiameterOfTheHat = double.TryParse(node["srednica_kapelusza"].InnerText, out double Srednica_kapelusza) ? Srednica_kapelusza : 0;
                        newMushroom.Lamella = double.TryParse(node["blaszki"].InnerText, out double Blaszki) ? Blaszki : 0;
                        newMushroom.Toxins = double.TryParse(node["toksyny"].InnerText, out double Toksyny) ? Toksyny : 0;
                        newMushroom.Edibility = node["cl"].InnerText;
                        CurrentBasket.ListOfMushroom.Add(newMushroom);
                    }
                    Logger.WriteLog($"[{DateTime.Now}] Load data from xml success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Niepoprawny plik xml");
                    Logger.WriteLog($"[{DateTime.Now}] {ex.Message}");
                }
            }
        }

        public void GetClassOfTheMostSuitableMushroomAndDisplayMessage()
        {
            string message = string.Empty;
            message = GetClassOfTheMostSuitableMushroom();
            CurrentBasket.ResultantClass = message;
            RaiseMessage(message);
        }

        public string GetClassOfTheMostSuitableMushroom()
        {
            string classOfTheMostSuitableMushroom = string.Empty;
            short countOfInEdibleMushroom = 0;
            short countOfEdibleMushroom = 0;
            double lastSuitable = 0;
            Logger.WriteLog($"[{DateTime.Now}] Calculation suitable for each mushroom");
            int i = 0;
            foreach (Mushroom mushroom in CurrentBasket.ListOfMushroom)
            {
                mushroom.Suitable = 0;
                mushroom.Suitable += Math.Pow(mushroom.DiameterOfTheHat - CurrentMushroomToCompare.DiameterOfTheHat, 2);
                mushroom.Suitable += Math.Pow(mushroom.Lamella - CurrentMushroomToCompare.Lamella, 2);
                mushroom.Suitable += Math.Pow(mushroom.Toxins - CurrentMushroomToCompare.Toxins, 2);
                Logger.WriteLog($"[{DateTime.Now}] Mushroom {i}: Diameter Of The Hat: {mushroom.DiameterOfTheHat} Lamella: {mushroom.Lamella} " +
                                $"Toxins: {mushroom.Toxins} Suitable: {mushroom.Suitable} Class: {mushroom.Edibility}");
                i++;
            }
            Logger.WriteLog($"[{DateTime.Now}] Mushroom to compare: Diameter Of The Hat: {CurrentMushroomToCompare.DiameterOfTheHat} Lamella: {CurrentMushroomToCompare.Lamella} " +
                                $"Toxins: {CurrentMushroomToCompare.Toxins} Suitable: {CurrentMushroomToCompare.Suitable}");
            foreach (Mushroom mushroom in CurrentBasket.ListOfMushroom.OrderBy(g => g.Suitable))
            {
                if ((countOfInEdibleMushroom + countOfEdibleMushroom) < CurrentBasket.CountOfElementToCompare)
                {
                    MakeDistinctionBetweenMushroom(mushroom, ref countOfEdibleMushroom, ref countOfInEdibleMushroom);
                    lastSuitable = mushroom.Suitable;
                }
                else
                {
                    if ((countOfEdibleMushroom + countOfInEdibleMushroom) < CurrentBasket.ListOfMushroom.Count)
                    {
                        if (mushroom.Suitable == lastSuitable)
                        {
                            MakeDistinctionBetweenMushroom(mushroom, ref countOfEdibleMushroom, ref countOfInEdibleMushroom);
                        }
                        else
                        {
                            if (countOfEdibleMushroom > countOfInEdibleMushroom)
                            {
                                return "Jadalny";
                            }
                            else if (countOfEdibleMushroom < countOfInEdibleMushroom)
                            {
                                return "Niejadalny";
                            }
                            else
                            {
                                MakeDistinctionBetweenMushroom(mushroom, ref countOfEdibleMushroom, ref countOfInEdibleMushroom);
                            }
                        }
                    }
                }
            }
            return "Nie znaleziono";
        }

        private void MakeDistinctionBetweenMushroom(Mushroom mushroom, ref short countOfEdibleMushroom, ref short countOfInEdibleMushroom)
        {
            if (mushroom.Edibility == "Jadalny")
            {
                countOfEdibleMushroom++;
            }
            else
            {
                countOfInEdibleMushroom++;
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new LogMessageEventArgs(message));
        }
    }
}
