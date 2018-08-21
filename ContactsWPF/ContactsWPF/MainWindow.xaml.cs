using SQLite;
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
using ContactsWPF.Classes;

namespace ContactsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                contacts = connection.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();
            }

            contactsListView.ItemsSource = contacts;
        }

        // search functionality of contacts
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            contactsListView.ItemsSource = filteredList;
        }
    }
}
