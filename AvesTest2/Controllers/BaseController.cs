using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AvesTest2.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationOptions AppOptions { get; set; }

        private SqlConnection _connection;

        public BaseController(
             IOptions<ApplicationOptions> appOptions)
        {
            AppOptions = appOptions.Value;          
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Return database connection to the connection pool
            if (_connection != null)
            {
                _connection.Dispose();
            }
            base.OnActionExecuted(context);
        }

        protected SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    SqlConnection connection = new SqlConnection(AppOptions.ConnectionString);
                    connection.Open();
                    _connection = connection;
                }
                return _connection;
            }
        }
    }
}