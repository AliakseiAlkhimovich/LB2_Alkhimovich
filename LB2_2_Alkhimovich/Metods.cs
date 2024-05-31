using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace LB2_2_Alkhimovich
{
    public static class Metods
    {
        public static void LoadDataToComboBoxes(string filePath, ComboBox comboBoxPositions, ComboBox comboBoxCities, ComboBox comboBoxStreets)
        {
            // Хранилища для уникальных значений
            var positions = new HashSet<string>();
            var cities = new HashSet<string>();
            var streets = new HashSet<string>();

            try
            {
                // Считывание всех строк из файла
                var lines = File.ReadAllLines(filePath);

                // Предполагаем, что первая строка содержит должности, вторая - города, третья - улицы
                for (int i = 0; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var part in parts)
                    {
                        if (i == 0) // Первая строка содержит должности
                        {
                            positions.Add(part);
                        }
                        else if (i == 1) // Вторая строка содержит города
                        {
                            cities.Add(part);
                        }
                        else // содержит улицы
                        {
                            streets.Add(part);
                        }
                    }
                }

                // Сортировка и установка источников данных для ComboBox
                comboBoxPositions.ItemsSource = positions.OrderBy(n => n).ToList();
                comboBoxCities.ItemsSource = cities.OrderBy(n => n).ToList();
                comboBoxStreets.ItemsSource = streets.OrderBy(n => n).ToList();
            }
            catch (FileNotFoundException)
            {
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine("");
                    writer.WriteLine("");
                    writer.WriteLine("");
                }
                MessageBox.Show("Файл не найден. Создан новый файл.");
            }
        }
        public static void SaveDataToFile(string filePath, TextBox FIO, TextBox Zp, ComboBox comboBoxPosition, ComboBox comboBoxCity, ComboBox comboBoxStreet, TextBox Home)
        {
            // Проверка на заполненность всех полей
            if (string.IsNullOrWhiteSpace(FIO.Text) ||
                string.IsNullOrWhiteSpace(Zp.Text) ||
                comboBoxPosition.SelectedItem == null ||
                comboBoxCity.SelectedItem == null ||
                comboBoxStreet.SelectedItem == null ||
                string.IsNullOrWhiteSpace(Home.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка на числовое значение в поле Zp
            if (!decimal.TryParse(Zp.Text, out decimal salary))
            {
                MessageBox.Show("Значение в поле 'Zp' должно быть числовым.");
                return;
            }

            // Формирование строки для записи с разделителем "---"
            string dataToWrite = $"{FIO.Text}---{salary} руб---{comboBoxPosition.SelectedItem}---{comboBoxCity.SelectedItem}---{comboBoxStreet.SelectedItem}---{Home.Text}";

            // Запись данных в файл
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(dataToWrite);
                }
                MessageBox.Show("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                File.Create(filePath).Close();
                MessageBox.Show("Файл не найден. Создан новый пустой файл.");
            }
        }
        public static void LoadDataToListBox(string filePath, ListBox listBox)
        {
            try
            {
                // Очистка ListBox перед загрузкой новых данных
                listBox.Items.Clear();

                // Чтение всех строк из файла
                var lines = File.ReadAllLines(filePath);

                // Добавление каждой строки в ListBox
                foreach (var line in lines)
                {
                    listBox.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                File.Create(filePath).Close();
                MessageBox.Show("Файл не найден. Создан новый пустой файл.");
            }
        }
    }
}
