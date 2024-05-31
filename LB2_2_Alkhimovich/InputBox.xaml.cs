using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace LB2_2_Alkhimovich
{
    
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
        }
        public static void LoadDataToComboBoxesAndSaveToFile(string filePath, ComboBox comboBoxPositions, ComboBox comboBoxCities, ComboBox comboBoxStreets, TextBox textBox, string buttonName)
        {
            //MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            var positions = new ObservableCollection<string>();
            var cities = new ObservableCollection<string>();
            var streets = new ObservableCollection<string>();

            try
            {
                var lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var part in parts)
                    {
                        if (i == 0)
                        {
                            positions.Add(part);
                        }
                        else if (i == 1)
                        {
                            cities.Add(part);
                        }
                        else
                        {
                            streets.Add(part);
                        }
                    }
                }
                string newValue = textBox.Text.Trim();

                if (!string.IsNullOrEmpty(newValue))
                {
                    string[] liness = File.ReadAllLines(filePath);

                    switch (buttonName)
                    {
                        case "Долж":
                            positions.Add(newValue);
                            liness[0] = liness[0] + "---" + newValue;
                            break;
                        case "Гор":
                            cities.Add(newValue);
                            liness[1] = liness[1] + "---" + newValue;
                            break;
                        case "Ул":
                            streets.Add(newValue);
                            liness[2] = liness[2] + "---" + newValue;
                            break;
                        default:
                            MessageBox.Show("Неверное имя кнопки.");
                            return;
                    }

                    File.WriteAllLines(filePath, liness);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите значение в текстовое поле.");
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден. Пожалуйста, убедитесь, что файл ComboData.txt существует.");
            }
            comboBoxPositions.ItemsSource = positions.OrderBy(n => n).ToList();
            comboBoxCities.ItemsSource = cities.OrderBy(n => n).ToList();
            comboBoxStreets.ItemsSource = streets.OrderBy(n => n).ToList();
   
        }
        public  void RunLoadDataMethod(object sender, RoutedEventArgs e)
        {
            string newValue = NewValue.Text.Trim();
            try
            {
                // Предполагаем, что mainWindow - это экземпляр MainWindow
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

                if (mainWindow != null)
                {
                    // Вызываем метод загрузки данных в ComboBox и сохранения в файл
                    LoadDataToComboBoxesAndSaveToFile("ComboData.txt", mainWindow.comboBoxPosition, mainWindow.comboBoxCity, mainWindow.comboBoxStreet, NewValue, mainWindow.buttonName);
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        MessageBox.Show("Данные успешно обновлены и сохранены.");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось получить доступ к главному окну.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        public static void RemoveDataFromComboBoxesAndSaveToFile(string filePath, ComboBox comboBoxPositions, ComboBox comboBoxCities, ComboBox comboBoxStreets, string buttonName)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
           
            try
            {
                // Считываем все строки из файла
                string[] lines = File.ReadAllLines(filePath);

                // Удаляем выбранное значение из соответствующего ComboBox и строки в файле
                switch (buttonName)
                {
                    case "Del1":
                        if (comboBoxPositions.SelectedItem != null)
                        {
                            string toRemove = comboBoxPositions.SelectedItem.ToString();
                            lines[0] = RemoveValueFromLine(lines[0], toRemove);
                            UpdateItemsSource(comboBoxPositions, toRemove);
                            MessageBox.Show("Должность успешно удалена!");
                        }
                        break;
                    case "Del2":
                        if (comboBoxCities.SelectedItem != null)
                        {
                            string toRemove = comboBoxCities.SelectedItem.ToString();
                            lines[1] = RemoveValueFromLine(lines[1], toRemove);
                            UpdateItemsSource(comboBoxCities, toRemove);
                            MessageBox.Show("Город успешно удален!");
                        }
                        break;
                    case "Del3":
                        if (comboBoxStreets.SelectedItem != null)
                        {
                            string toRemove = comboBoxStreets.SelectedItem.ToString();
                            lines[2] = RemoveValueFromLine(lines[2], toRemove);
                            UpdateItemsSource(comboBoxStreets, toRemove);
                            MessageBox.Show("Улица успешно удалена!");
                        }
                        break;
                    default:
                        MessageBox.Show("Неверное имя кнопки.");
                        return;
                }
                // Перезаписываем файл с обновленными данными
                File.WriteAllLines(filePath, lines);
                Metods.LoadDataToComboBoxes("ComboData.txt", mainWindow.comboBoxPosition, mainWindow.comboBoxCity, mainWindow.comboBoxStreet);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        // Вспомогательный метод для обновления ItemsSource после удаления элемента
        private static void UpdateItemsSource(ComboBox comboBox, string toRemove)
        {
            var itemsSource = comboBox.ItemsSource as ObservableCollection<string>;
            if (itemsSource != null)
            {
                itemsSource.Remove(toRemove);
            }
        }
        // Вспомогательный метод для удаления значения из строки
        private static string RemoveValueFromLine(string line, string value)
        {
            var parts = line.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            parts.Remove(value);
            return string.Join("---", parts);
        }

       

    }

}
