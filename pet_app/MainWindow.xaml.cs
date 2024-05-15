using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pet_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var data = File.ReadAllLines(@"..\..\..\src\animals.txt");

            ObservableCollection<Pet> UnsortedPets = new ObservableCollection<Pet>();

            foreach (var item in data)
            {
                var d = item.Split(";");

                string name = d[0];
                int age = int.Parse(d[1]);
                string color = d[2];
                string source = d[3];

                UnsortedPets.Add(new Pet(name, age, color, source));
            }

            ObservableCollection<Pet> Pets = new ObservableCollection<Pet>(UnsortedPets.OrderBy(p => p.Name));


            petek.ItemsSource = Pets;
            petek.SelectedItem = Pets[0];
            petek.SelectionChanged += (sender, e) => { if (petek.SelectedItem != null) UpdateAttributeText(); };
        }
        private void UpdateAttributeText()
        {
            if (petek.SelectedItem != null)
            {
                Pet selectedItem = (Pet)petek.SelectedItem;
                attributeTextBlock.Text = $"Megnevezés: {selectedItem.Name}\n" +
                                          $"Életkor: {selectedItem.Age} \n" +
                                          $"Szín: {selectedItem.Color}";


                string imagePath = @$"..\Images\{selectedItem.Image}";


                if (File.Exists(imagePath)) kep.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)); else kep.Source = null;
            }
        }
        private void btn_torol_Click(object sender, RoutedEventArgs e)
        {

            if (petek.SelectedItem != null)
            {
                kedvencek.Items.Remove(kedvencek.SelectedItem);
            }
            
        }

        private void btn_kedvencek_Click(object sender, RoutedEventArgs e)
        {
            string kedvenc = "";
            if (petek.SelectedItem != null)
            {
                var selectedListBoxItem = petek.SelectedItem;

                if (selectedListBoxItem is ListBoxItem listBoxItem)
                    kedvenc = listBoxItem.Content.ToString();
                else
                    kedvenc = selectedListBoxItem.ToString();

                kedvencek.Items.Add(kedvenc);
            }
        }
    }
}