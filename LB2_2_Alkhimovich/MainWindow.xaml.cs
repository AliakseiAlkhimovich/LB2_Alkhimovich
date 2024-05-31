using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LB2_2_Alkhimovich
{
    
    public partial class MainWindow : Window
    {
        public string buttonName;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Metods.LoadDataToComboBoxes("ComboData.txt", comboBoxPosition, comboBoxCity, comboBoxStreet);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(sender, e);
            InputBox inputBox = new InputBox();
            inputBox.Show();
        }

        public void DeleteDataMethod(object sender, RoutedEventArgs e)
        {
            Button_Click(sender, e);
            try
            {
                    InputBox.RemoveDataFromComboBoxesAndSaveToFile("ComboData.txt", comboBoxPosition, comboBoxCity,comboBoxStreet,buttonName); 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            // Приведение sender к типу Button
            System.Windows.Controls.Button clickedButton = sender as System.Windows.Controls.Button;
            // Проверка, что sender действительно является кнопкой
            if (clickedButton != null)
            {
                // Получение имени кнопки
                 buttonName = clickedButton.Name;
            }
        }
        public void SaveToFile(object sender, RoutedEventArgs e)
        {
            try
            {
                Metods.SaveDataToFile("Info.txt", FIO, Zp, comboBoxPosition, comboBoxCity, comboBoxStreet, Home);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        public void LoadDataLB(object sender, RoutedEventArgs e)
        {
            try
            {
                Metods.LoadDataToListBox("Info.txt", LB);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }



    }

   
}
