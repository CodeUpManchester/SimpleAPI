using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly DataLayer _dataLayer;

        public OrdersController()
        {
            _dataLayer = new DataLayer();
        }

        [System.Web.Http.HttpGet]
        public ActionResult ForCustomer(string id)
        {
            var storedProcedureName = "CustOrdersOrders";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CustomerID", id)
            };

            var reader  = _dataLayer.RunStoredProcedure(storedProcedureName, parameters);

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