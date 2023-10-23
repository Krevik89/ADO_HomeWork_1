using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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


namespace ADO_HomeWork_1
{
   
    public partial class MainWindow : Window
    {        
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;
        
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.
                ConnectionStrings["MyConnection"].ConnectionString;           
        }
        
        private void SetConnectionIndicatorColor(Brush color)
        {
            ConnectionIndicator.Fill = color;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                conn.Open();
                SetConnectionIndicatorColor(Brushes.Green);
                MessageBox.Show("Connection successful!");
            }
            catch (Exception ex)
            {
                SetConnectionIndicatorColor(Brushes.Red);
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                SetConnectionIndicatorColor(Brushes.Red);
                MessageBox.Show("Disconnected from the database.");
            }
        }

        private void Button_Show_All(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = conn;
            conn.CreateCommand();
            command.CommandText = "select * from Products";

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string temp = string.Empty;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    temp += reader[i].ToString() + " ";
                }
                ListQuery.Items.Add(temp);
            }
            reader.Close();
        }
    }
}

