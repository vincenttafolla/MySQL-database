using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySQL
{
    class ProductRepository
    {
        private const string conStr= "Server=Localhost;Database=DataBase=BestBuy;Uid=root;Pwd=password";

        // CREATE
        public void InserProduct(Product product)
        {
            MySqlConnection conn = new MySqlConnection(conStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) " +
                $"VALUES (@productName, {product.Price}, 1, 0, 99);)";
            cmd.Parameters.AddWithValue("productName", product.Name);
            // Parameterized query

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // DELETE
        public void DeleteProduct(int productId)
        {
            MySqlConnection conn = new MySqlConnection(conStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "DELETE FROM products" +
                $"WHERE ProductID={productId};";

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // UPDATE
        public void UpdateProductName(int productId, string newName)
        {
            MySqlConnection conn = new MySqlConnection(conStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "UPDATE products " +
                "SET Name=@newName " +
                $"WHERE ProductID={productId};";
            cmd.Parameters.AddWithValue("name", newName);

            using (cmd.Connection)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(conStr);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT ProductID, Name, Price " +
                "FROM products;";

            List<Product> products = new List<Product>();
            using (cmd.Connection)
            {
                cmd.Connection.Open();

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read() == true)
                {
                    Product product1 = new Product();

                    product1.Id = dataReader.GetInt32("ProductID");
                    product1.Name = dataReader.GetString("Name");
                    product1.Price = dataReader.GetDecimal("Price");

                    products.Add(product1);
                } 
            }
            return products;
        }
    }
}
