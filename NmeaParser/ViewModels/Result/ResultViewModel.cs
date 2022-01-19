using Autocomp.Nmea.Common;
using NmeaParser.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NmeaParser.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        public TableCreator TableCreater = new TableCreator();

        private ResultView View;
        private List<NmeaGLL> NmeaGLLList = new List<NmeaGLL>();
        private List<List<string>> NmeaGLLString = new List<List<string>>();
        private List<NmeaMWV> NmeaMWVList = new List<NmeaMWV>();
        private List<List<string>> NmeaMWVString = new List<List<string>>();
        private List<List<string>> ErrorList = new List<List<string>>();

        public ResultViewModel(ResultView view)
            : this()
        {
            View = view;
        }

        public ResultViewModel()
        {
            ParseText();
        }

        private void ParseText()
        {
            string text = ((MainWindow)Application.Current.MainWindow).Text;
            if (text != null)
            {
                string[] textArray = text.Split(new string[] { NmeaFormat.DefaultTerminator }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < textArray.Length; i++)
                {
                    try
                    {
                        NmeaMessage nmeaMessage = NmeaMessage.FromString(textArray[i]);

                        if (NmeaGLL.CheckHeader(nmeaMessage.Header))
                        {
                            var nmeaGLL = new NmeaGLL(textArray[i]);
                            if (nmeaGLL.Error)
                            {
                                ErrorList.Add(new List<string>() { nmeaGLL.OriginalMessage, nmeaGLL.ErrorMessage });
                            }
                            else
                            {
                                NmeaGLLList.Add(nmeaGLL);
                                NmeaGLLString.Add(nmeaGLL.GetAllFields());
                            }
                        }
                        else if (NmeaMWV.CheckHeader(nmeaMessage.Header))
                        {
                            var nmeaMWV = new NmeaMWV(textArray[i]);
                            if (nmeaMWV.Error)
                            {
                                ErrorList.Add(new List<string>() { nmeaMWV.OriginalMessage, nmeaMWV.ErrorMessage });
                            }
                            else
                            {
                                NmeaMWVList.Add(nmeaMWV);
                                NmeaMWVString.Add(nmeaMWV.GetAllFields());
                            }
                        }
                        else
                        {
                            ErrorList.Add(new List<string>() { textArray[i], "Not recognize header" });
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorList.Add(new List<string>() { textArray[i], ex.Message });
                    }
                }
            }
        }

        public void SelectionChanged()
        {
            if (((MainWindow)Application.Current.MainWindow).Header != null)
            {
                if (View != null)
                {
                    var header = ((MainWindow)Application.Current.MainWindow).Header;
                    if (NmeaGLL.CheckHeader(header))
                    {
                        TableCreater.CreateTable(View.table, NmeaGLL.Headers, NmeaGLLString);
                    }
                    else if (NmeaMWV.CheckHeader(header))
                    {
                        TableCreater.CreateTable(View.table, NmeaMWV.Headers, NmeaMWVString);
                    }
                    else
                    {
                        TableCreater.CreateTable(View.table, new string[] { "Original", "Error Message" }, ErrorList);
                    }
                }
            }
        }
    }
}