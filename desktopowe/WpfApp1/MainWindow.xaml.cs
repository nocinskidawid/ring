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
using Microsoft.Win32;

namespace WpfApp1
{
    
    public partial class MainWindow : Window
    {
        string fileName = "";
        bool canClear = false;

        public static RoutedCommand SaveAsCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            SaveAsCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("właściciel: aha ok\nNr indeksu:1234123");
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                fileName = openFileDialog.FileName;
               
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEditor.Text))
            {
                txtEditor.Text = "";
                fileName = "";
            }
            else{
                if(MessageBox.Show("Zmiany nie zostały zapisane. Czy chcesz zapsać je teraz?", "Zapisz zmiany", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes){
                    canClear = true;
                    Save_Click(sender, e);
                }
                else
                {
                    txtEditor.Text = "";
                    fileName = "";
                }
            }
          
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fileName)){
                Save_As_Click(sender, e);
            }
            else
            {
                File.WriteAllText(fileName, txtEditor.Text);
                if (canClear)
                {
                    txtEditor.Text = "";
                    fileName = "";
                }
            }
        }

        private void Save_As_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
                fileName = saveFileDialog.FileName;
                if (canClear)
                {
                    txtEditor.Text = "";
                    fileName = "";
                }
            }
        }

        private void Edit_Color_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var v = item.Tag.ToString();
            if (v.Equals("Zo"))
            {
                txtEditor.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else if (v.Equals("Zi")){
                txtEditor.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (v.Equals("C"))
            {
                txtEditor.Foreground = new SolidColorBrush(Colors.Red);
            }                  
        }
        private void Edit_Font_Size_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var v = item.Tag.ToString();
            txtEditor.FontSize = Double.Parse(v);
        }


    }
}