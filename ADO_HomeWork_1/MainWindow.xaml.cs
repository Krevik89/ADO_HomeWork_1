using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;

namespace ADO_HomeWork_1
{
   
    public partial class MainWindow : Window
    {        
        SqlConnection conn;
        SqlCommand command;
        SqlDataReader reader;
        private DataTable table;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.
                ConnectionStrings["MyConnection"].ConnectionString;
            command = new SqlCommand();
            command.Connection = conn;
        }

        private void While(SqlDataReader reader)
        {
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
            command.CommandText = "select * from Products";
            reader = command.ExecuteReader();
            While(reader);
        }
        private void Button_Show_Name(object sender, RoutedEventArgs e)
        {
            command.CommandText = "select Name from Products";
            reader = command.ExecuteReader();
            While(reader);
        }
        private void Button_Show_All_Color(object sender, RoutedEventArgs e)
        {
            command.CommandText = "SELECT DISTINCT Color FROM Products";
            reader = command.ExecuteReader();
            While(reader);
        }
        private void Show_Max_Kkall(object sender, RoutedEventArgs e)
        {
            command.CommandText = "SELECT MAX(Kkall) AS Максимальная_калорийность FROM Products";
            reader = command.ExecuteReader();
            While(reader);
        }
        private void Show_Min_Kkall(object sender, RoutedEventArgs e)
        {
            command.CommandText = "SELECT Min(Kkall) AS Максимальная_калорийность FROM Products";
            reader = command.ExecuteReader();
            While(reader);
        }
        private void Show_Avg_Kkall(object sender, RoutedEventArgs e)
        {
            command.CommandText = "SELECT Avg(Kkall) AS Максимальная_калорийность FROM Products";
            reader = command.ExecuteReader();
            While(reader);
        }

        private void Show_Count_vegetable(object sender, RoutedEventArgs e)
        {                     
            string strstr = "SELECT COUNT(*) AS Количество__Овощей FROM dbo.Products  where Type = 'Овощь'";
            command = new SqlCommand();
            command.CommandText = strstr;             
            command.Connection = conn;

            table = new DataTable();
            adapter = new SqlDataAdapter(command);

            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;           
        }
        private void Show_Count_fruits(object sender, RoutedEventArgs e)
        {
            string strstr = "SELECT COUNT(*) AS Количество__Фруктов FROM dbo.Products  where Type = 'Фрукт'";
            command = new SqlCommand();
            command.CommandText = strstr;
            command.Connection = conn;

            table = new DataTable();
            adapter = new SqlDataAdapter(command);

            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;
        }
        private void Show_Count_Product_Color(object sender, RoutedEventArgs e)
        {           
            if (textBox1.Text != string.Empty)
            {
                string strstr = "Select Count (*) Name from dbo.Products where Color = @typeName";
                command = new SqlCommand();
                command.CommandText = strstr;
                command.Parameters.AddWithValue("@typeName", textBox1.Text);
                command.Connection = conn;

                table = new DataTable();
                adapter = new SqlDataAdapter(command);

                adapter.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }
        private void Show_Count_Product_All_Color(object sender, RoutedEventArgs e)
        {         
            string strstr = "Select Color As Цвет, Count (*) As Количество from dbo.Products Group By Color";
            command = new SqlCommand();
            command.CommandText = strstr;
            command.Connection = conn;

            table = new DataTable();
            adapter = new SqlDataAdapter(command);

            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;
        }
        private void Show_Count_Type_Kkall_Down(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {               
                string strstr = "Select * from dbo.Products where KKall < @typeName";
                command = new SqlCommand();
                command.CommandText = strstr;
                command.Parameters.AddWithValue("@typeName", textBox1.Text);
                command.Connection = conn;

                table = new DataTable();
                adapter = new SqlDataAdapter(command);

                adapter.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }
        private void Show_Count_Type_Kkall_Up(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {               
                string strstr = "Select * from dbo.Products where KKall > @typeName";
                command = new SqlCommand();
                command.CommandText = strstr;
                command.Parameters.AddWithValue("@typeName", textBox1.Text);
                command.Connection = conn;

                table = new DataTable();
                adapter = new SqlDataAdapter(command);

                adapter.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }
        private void Show_Kkall_Range(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                string strstr = "Select * from dbo.Products where KKall BETWEEN @typeName AND @typeName1";
                command = new SqlCommand();
                command.CommandText = strstr;
                command.Parameters.AddWithValue("@typeName", textBox1.Text);
                command.Parameters.AddWithValue("@typeName1", textBox2.Text);
                command.Connection = conn;

                table = new DataTable();
                adapter = new SqlDataAdapter(command);

                adapter.Fill(table);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }
        private void Show_All_Product_Color_Red_Or_Yellow(object sender, RoutedEventArgs e)
        {           
            string strstr = "Select * from dbo.Products Where Color IN('Зеленый','Красный')";
            command = new SqlCommand();
            command.CommandText = strstr;
            command.Connection = conn;

            table = new DataTable();
            adapter = new SqlDataAdapter(command);

            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;
        }
    }
}