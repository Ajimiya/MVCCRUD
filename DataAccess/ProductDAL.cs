using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NEW_CRUD_MVC.Models;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;

namespace NEW_CRUD_MVC.DataAccess
{
    public class ProductDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["addConnectionString"].ToString();
        private SqlConnection conn;

        //Get ALL PRODUCTS
        public List<Product> GetAllProducts()
        { 
            List<Product> productList = new List<Product>();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd=conn.CreateCommand();
                cmd.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtProducts = new DataTable();


                conn.Open();
                sqlDA.Fill(dtProducts);
                conn.Close();


                foreach(DataRow dr  in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32( dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal( dr["Price"]),
                        Qty = (int)Convert.ToDecimal(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }   
            }

            return productList;
        }


        //INSERT PRODUCTS

        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(conString))

            {
                SqlCommand cmd = new SqlCommand("sp_InsertProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.ProductId);
                cmd.Parameters.AddWithValue("@Qty", product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", product.Remarks);


                conn.Open();
                id=cmd.ExecuteNonQuery();
                conn.Close();

            }
            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Product By Id


        public List<Product> GetProductById(int ProductId)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "sp_expGetProductbyId";
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@ProductId",ProductId );
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtProducts = new DataTable();


                conn.Open();
                sqlDA.Fill(dtProducts);
                conn.Close();


                foreach (DataRow dr in dtProducts.Rows)
                {
                    productList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = (int)Convert.ToDecimal(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }

            return productList;
        }


        //Update Product
          public bool UpdateProduct(Product product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(conString))

            {
                SqlCommand cmd = new SqlCommand("sp_UpdateProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.ProductId);
                cmd.Parameters.AddWithValue("@Qty", product.Qty);
                cmd.Parameters.AddWithValue("@Remarks", product.Remarks);


                conn.Open();
                id=cmd.ExecuteNonQuery();
                conn.Close();

            }
            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Delete Product

        public string DeleteProduct(int ProductId)
        {
            string result = "";
            using (SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("DeleteValue", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(" @ProductId ", ProductId);
                command.Parameters.Add("@Outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@Outputmessage"].Value.ToString();
                connection.Close();

            }

            return result;
        }


    }
}