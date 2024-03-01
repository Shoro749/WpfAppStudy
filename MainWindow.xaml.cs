using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using WpfAppStudy.Models;

namespace WpfAppStudy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                
                try
                {
                    sqlConnection.Open();


                    //SqlCommand sqlCommand = new SqlCommand(
                    //    @"create table [product2](
                    //        id int primary key identity,
                    //        [name] varchar(30) not null,
                    //        [price] int not null,
                    //    )"
                    //    , sqlConnection);

                    //sqlCommand.ExecuteNonQuery();

                    //SqlCommand insertCommand = new SqlCommand("", sqlConnection);

                    //List<Tuple<string, int>> data = new List<Tuple<string, int>>()
                    //{
                    //    new Tuple<string, int>("Cola", 17),
                    //    new Tuple<string, int>("Pepsi", 15),
                    //    new Tuple<string, int>("Fanta", 12),
                    //    new Tuple<string, int>("Sprite", 10),
                    //};

                    //foreach (var item in data)
                    //{
                    //    insertCommand.CommandText = $"insert product2 values ('{item.Item1}', '{item.Item2}')";
                    //    insertCommand.ExecuteNonQuery();
                    //}



                    SqlCommand sqlCommand = new SqlCommand("Select * from product2", sqlConnection);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        List<Product> products = new List<Product>();
                        while (reader.Read())
                        {
                            products.Add(new Product()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetInt32(2),
                            });
                        }

                        grid.ItemsSource = products;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text != "" && tb_Price.Text != "")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    try
                    {
                        sqlConnection.Open();
                        SqlCommand sqlCommand = new SqlCommand("Select * from product2", sqlConnection);

                        SqlCommand insertCommand = new SqlCommand("", sqlConnection);

                            insertCommand.CommandText = $"insert product2 values ('{tb_Name.Text}', '{Convert.ToInt32(tb_Price.Text)}')";
                            insertCommand.ExecuteNonQuery();
 
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            List<Product> products = new List<Product>();
                            while (reader.Read())
                            {
                                products.Add(new Product()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Price = reader.GetInt32(2),
                                });
                            }

                            grid.ItemsSource = products;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter values!");
            }
        }
    }
}