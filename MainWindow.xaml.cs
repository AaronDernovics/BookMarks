using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string adress = "https://learn.microsoft.com/";
        readonly string fileName = "links.txt";

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void ButtonAddLink_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Linkstxt.Text) && !linksAdress.Items.Contains(Linkstxt.Text))
            {
                linksAdress.Items.Add(Linkstxt.Text);

                File.AppendAllText("links.txt", Linkstxt.Text + Environment.NewLine);

                Linkstxt.Clear();
            }
        }

        // SelectItem method can open the link by doubleclick on it
        // First make adress eaqual with the selected item in the listbox ( inside the listbox there is the textbox => casting into string)
        // Process.Start can just open the browser
        private void SelectItem(object sender, MouseButtonEventArgs e)
        {
            adress = (string)linksAdress.SelectedItem;
            Process.Start("chrome.exe", $"{adress}");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (linksAdress.SelectedItem != null)
            {

                linksAdress.Items.RemoveAt(linksAdress.Items.IndexOf(linksAdress.SelectedItem));

                StreamWriter SaveFile = new StreamWriter("links.txt");
                foreach (var item in linksAdress.Items)
                {
                    SaveFile.WriteLine(item);
                }

                SaveFile.Close();
            }

        }

        private void Load()
        {
            if (!File.Exists(fileName))
            {
              using (StreamWriter sw = File.CreateText("links.txt"));
            }

            string[] lines = File.ReadAllLines("links.txt");
            foreach (var item in lines)
            {
              linksAdress.Items.Add(item);
            }
        }
    }
}
