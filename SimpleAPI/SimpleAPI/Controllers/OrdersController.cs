using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly ConnectionFactory _connectionFactory;

        public OrdersController()
        {
            _connectionFactory = new ConnectionFactory();
        }

        [System.Web.Http.HttpGet]
        public ActionResult ForCustomer(string id)
        {
            var connection = (SqlConnection)_connectionFactory.Create();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "CustOrdersOrders";
            command.CommandType = CommandType.StoredProcedure;

            var customerIdParameter = new SqlParameter("@CustomerID", id);
            command.Parameters.Add(customerIdParameter);

            var reader = command.ExecuteReader();

            var orders = new List<Order>();
            while (reader.Read())
            {
                try
                {
                    var order = new Order
                    {
                        OrderId = reader.GetInt32((int)OrderFields.OrderId),
                        OrderDate = reader.GetDateTime((int)OrderFields.OrderDate),
                        RequiredDate = reader.GetDateTime((int)OrderFields.RequiredDate),
                        ShippedDate = reader.GetDateTime((int)OrderFields.ShippedDate)
                    };

                    orders.Add(order);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return new JsonResult
            {
                Data = orders,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentType = "application/json"
            };
        }
    }
}