using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using WpfBasics.Enums;
using WpfBasics.Models;


namespace WpfBasics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickResetButton(object sender, RoutedEventArgs e)
        {
            var ckList = FindVisualChildren<CheckBox>(OurMainWindow);
            foreach (CheckBox checkBox in ckList)
            {
                checkBox.IsChecked = false;
            }

            var tbList = FindVisualChildren<TextBox>(OurMainWindow);
            foreach (TextBox textBox in tbList)
            {
                textBox.Text = "";
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void ClickAddButton(object sender, RoutedEventArgs e)
        {
            var auto = CreateCar();
            var filename = AppDomain.CurrentDomain.BaseDirectory + @"..\..\CarFiles\" + auto.Name + ".txt";
            Helpers.Serializer.SerializeObject<Car>(auto, filename);
            MessageBox.Show("chyba poszło");
        }

        private Car CreateCar()
        {
            var CreatedCar = new Car();
            CreatedCar.Name = DescriptionText.Text;
            CreatedCar.Revision = Revision.Text;
            CreatedCar.Status = Status.Text;
            if (IdNumber.Text != "")
            {
                CreatedCar.IdentificationNumber = Int32.Parse(IdNumber.Text.Trim());
            }

            if (Material.Text != "")
            {
                CreatedCar.Material = (MaterialEnum)System.Enum.Parse(typeof(MaterialEnum),
                    Material.Text.Replace(" ", "").Replace(":", ""));
            }

            if (MassText.Text != "")
            {
                CreatedCar.Weight = Double.Parse(MassText.Text.Trim());
            }

            if (LengthText.Text != "")
            {
                CreatedCar.Length = Double.Parse(LengthText.Text.Trim());
            }

            var productionOptionList = new List<ProductionOptionEnum>();
            foreach (var option in Helpers.Serializer.FindVisualChildren<CheckBox>(OurMainWindow)
                .Where(_ => _.IsChecked == true))
            {
                productionOptionList.Add((ProductionOptionEnum)System.Enum.Parse(typeof(ProductionOptionEnum),
                    ((string)(option.Content)).Replace(" ", "").Replace(":", "")));
            }

            CreatedCar.ProductionOptions = productionOptionList;

            CreatedCar.Finish = (FinishEnum)System.Enum.Parse(typeof(FinishEnum),
                FinishDropdown.Text.Replace(" ", "").Replace(":", ""));
            CreatedCar.PurchaseInformation = (PurchaseInformationEnum)System.Enum.Parse(
                typeof(PurchaseInformationEnum), PurchaseInformation.Text.Replace(" ", "").Replace(":", ""));
            CreatedCar.SupplierName = SupplierNameText.Text;
            if (SupplierCode.Text != "")
            {
                CreatedCar.SupplierCode = Int32.Parse(SupplierCode.Text.Trim());
            }

            CreatedCar.Note = NoteText.Text;
            return CreatedCar;
        }

        private void FindCarFile(object sender, RoutedEventArgs e)
        {
            var path = Environment.CurrentDirectory + @"..\..\CarFiles\" + DescriptionText.Text + ".txt";
            if (File.Exists(path))
            {
                var car = Helpers.Serializer.DeSerializeObject<Models.Car>(path);
                var checkBoxes = Helpers.Serializer.FindVisualChildren<CheckBox>(OurMainWindow).ToList();
                foreach (var option in car.ProductionOptions)
                {
                    var item = option.ToString();
                    var checkBox = checkBoxes.Single(x => x.Name.Replace("Checkbox", "") == item);
                    checkBox.IsChecked = true;
                }
            }
            else
            {
                MessageBox.Show("nie ma");
            }
        }



    }

}