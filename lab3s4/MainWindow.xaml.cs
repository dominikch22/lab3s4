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
        private Action<object, RoutedEventArgs> XToN = delegate { };
        private List<double> NumberList;

        private Action<object, RoutedEventArgs> CreateText = delegate { };
        private Func<double, double> PowerTo = delegate { return 0; };

        private Func<double, double, double> XToPowerN = (x, n) => Math.Pow(x,n);
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


        public MainWindow()
        {
            InitializeComponent();
            NumberList = new List<double>();

            CreateText = (sender, e) =>
            {
                string ch = characterInput.Text;
                int length = int.Parse(textLengthInput.Text);

                string text = CreateString(ch, length);
                textResult.Text = text;


            };

            XToN = (sender, e) => {
                double x = double.Parse(xInput.Text);
                double n = double.Parse(xInput.Text);

                resultText.Text = XToPowerN(x,n).ToString();

            };
        }

        public void XToN(object sender, RoutedEventArgs e) {
            double x = double.Parse(xInput.Text);
            double n = double.Parse(xInput.Text);

            resultText.Text = XToPowerN(x, n).ToString();
        }

        public void AddNumber(object sender, RoutedEventArgs e) {
            double number = double.Parse(numberInput.Text);
            NumberList.Add(number);

            LoadNumbersToListBox();
        }

        public void LoadNumbersToListBox() {
            numbersListBox.Items.Clear();
            foreach (double n in NumberList)
            {
                numbersListBox.Items.Add(n);
            }
        }

        public void Sort(object sender, RoutedEventArgs e) {
            BubbleSort<double>(NumberList, CompareToAscending);

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
            double x = double.Parse(xpInput.Text);

            if(PowerTo != null)
                powerResult.Text = PowerTo(x).ToString();
        }




    }
}
