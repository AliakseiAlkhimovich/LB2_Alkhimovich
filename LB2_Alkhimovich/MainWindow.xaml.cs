using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LB2_Alkhimovich
{
   
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
           
        }
        public void ReadData(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(Xstart.Text);
                double xEnd = double.Parse(Xstop.Text);
                double step = double.Parse(Step.Text);
                int n = int.Parse(N.Text);
               

                if (!IsValidInput(x, xEnd, step, n))
                {
                    MessageBox.Show("Ошибка: Введите корректные числовые значения.");
                    return;
                }
                Class_Lab2_1 classLab = new Class_Lab2_1(x, xEnd, step, n);

                CalculateAndDisplayResults(classLab);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Введите корректные числовые значения.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsValidInput(double x, double xEnd, double step, int n)
        {
           
            return true; 
        }

        private void CalculateAndDisplayResults(Class_Lab2_1 classLab)
        {
            int index = 1;
            ObservableCollection<string> results = new ObservableCollection<string>();
            for (double currentX = classLab.X; currentX <= classLab.XEnd; currentX += classLab.Step)
            {
                double sum = CalculateS(currentX,classLab.N);
                double y = Math.Pow(3, currentX) - 1;
                results.Add("Результат" + index + " S(" + currentX + ") = " + sum + " Y(" + currentX + ") = " + y);
                index++;
            }

            LB.Items.Clear();
            foreach (string result in results)
            {
                LB.Items.Add(result);
            }
        }

        private double CalculateS(double x, int n)
        {
            double s = 0;
            double term=1;
            for (int k = 1; k <= n; k++)
            {
                    term *= (Math.Log(3)/ k) * x;
                    s += term;
            }
            return s;
        }

        private long Factorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            return result;
        }

        public void Xstart_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            double value;

            // Замена точки на запятую
            textBox.Text = textBox.Text.Replace('.', ',');

            // Проверка на валидность числового значения
            if (!double.TryParse(textBox.Text, out value))
            {
                textBox.BorderBrush = Brushes.Red;
                ToolTipService.SetToolTip(textBox, "Введите корректное числовое значение");

                // Добавление красной звездочки
                if (textBox.Tag == null || !(textBox.Tag is TextBlock))
                {
                    TextBlock star = new TextBlock();
                    star.Text = "*";
                    star.Foreground = Brushes.Red;
                    star.FontSize = 24;
                    star.VerticalAlignment = VerticalAlignment.Top;
                    star.HorizontalAlignment = HorizontalAlignment.Right;
                    textBox.Tag = star;
                    Grid.SetColumn(star, Grid.GetColumn(textBox) + 1);
                    Grid.SetRow(star, Grid.GetRow(textBox));
                    ((Grid)textBox.Parent).Children.Add(star);
                }
            }
            else
            {
                textBox.ClearValue(Border.BorderBrushProperty);
                ToolTipService.SetToolTip(textBox, null);

                // Удаление красной звездочки
                if (textBox.Tag is TextBlock)
                {
                    ((Grid)textBox.Parent).Children.Remove((TextBlock)textBox.Tag);
                    textBox.Tag = null;
                }
            }
        }

    }
}
