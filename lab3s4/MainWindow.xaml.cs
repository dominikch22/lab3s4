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

namespace lab3s4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<double> NumberList;

        private Func<double, double> PowerTo = delegate { return 0; };

        private Func<double, double, double> XToPowerN = (x, n) => {
            if (n == 0)
                return 1;

            double result = 1;
            for (int i = 0; i < Math.Abs(n); i++) {
                result *= x; 
            }

            if (n < 0)
                result = 1 / result;

            return result;
        };
        private Func<string, int, string> CreateString = (ch, length) => {
            string result = "";
            for (int i = 0; i < length; i++) {
                result += ch;
            }
            return result;
        };

        public delegate int CompareTo<T>(T e1, T e2);
        public static void BubbleSort<T>(List<T> list, CompareTo<T> comperator) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (comperator(list[j], list[j + 1]) > 0)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        private CompareTo<double> CompareToAscending = (a, b) => {
            if (a < b)
                return -1;
            else if (a > b)
                return 1;
            else 
                return 0;
        };
        private CompareTo<double> CompareToDescending = (b, a) => {
            if (a < b)
                return -1;
            else if (a > b)
                return 1;
            else
                return 0;
        };
        private CompareTo<double> comperator = delegate { return 0; };


        public MainWindow()
        {
            InitializeComponent();
            NumberList = new List<double>();        
        }

        public void CreatePerson(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = nameInput.Text;
                string surname = surnameInput.Text;
                DateTime birthDate = DateTime.Parse(birthDateInput.Text);
                Person person = new Person(name, surname, birthDate);

                personInfo.Text = person.ToString() +" | i: "+ person["Name"].ToString();
            }
            catch { }
        }

        public void XToN(object sender, RoutedEventArgs e) {
            try
            {
                double x = double.Parse(xInput.Text);
                double n = double.Parse(nInput.Text);

                resultText.Text = XToPowerN(x, n).ToString();
            }
            catch { 
            
            }
            
        }

        public void CreateText(object sender, RoutedEventArgs e) {
            try
            {
                string ch = characterInput.Text;
                int length = int.Parse(textLengthInput.Text);

                string text = CreateString(ch, length);
                textResult.Text = text;
            }
            catch { }
           


        }

        public void AddNumber(object sender, RoutedEventArgs e) {
            try
            {
                double number = double.Parse(numberInput.Text);
                NumberList.Add(number);

                LoadNumbersToListBox();
            }
            catch { }
           
        }

        public void ChangeToAscendingSort(object sender, RoutedEventArgs e) {
            CheckBox checkBox = (CheckBox)sender;
            if ((bool)checkBox.IsChecked) {
                comperator = CompareToAscending;
            }
            else
                comperator = delegate { return 0; };

            DisableOtherCheckBoxes(checkBox);
        }

        public void ChangeToDescendingSort(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if ((bool)checkBox.IsChecked)
            {
                comperator = CompareToDescending;
            }
            else
                comperator = delegate { return 0; };

            DisableOtherCheckBoxes(checkBox);
        }

        public void DisableOtherCheckBoxes(CheckBox sender) {
            if (!sender.Equals(ascendingCheckBox))
                ascendingCheckBox.IsChecked = false;
            if (!sender.Equals(descendingCheckBox))
                descendingCheckBox.IsChecked = false;
        }

        public void LoadNumbersToListBox() {
            numbersListBox.Items.Clear();
            foreach (double n in NumberList)
            {
                numbersListBox.Items.Add(n);
            }
        }

        public void Sort(object sender, RoutedEventArgs e) {
            BubbleSort<double>(NumberList, comperator);

            LoadNumbersToListBox();
        }

        public Func<double, double> GetPowerToFunction(double n) {
            return (x) => Math.Pow(x, n);
        }

        public void CreatePowerToFunction(object sender, RoutedEventArgs e) {
            //double x = double.Parse(xpInput.Text);
            double n = double.Parse(npInput.Text);

            PowerTo = GetPowerToFunction(n);
        }

        public void UsePowerTo(object sender, RoutedEventArgs e) {
            try
            {
                double x = double.Parse(xpInput.Text);

                powerResult.Text = PowerTo(x).ToString();

            }
            catch { }

        }




    }
}
