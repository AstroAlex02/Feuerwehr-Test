using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ffw.Pages.Verwaltung
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute
    {
        public AdminAuthorizeAttribute() : base(typeof(AdminAuthorizeFilter)) { }
    }

    public class AdminAuthorizeFilter : IAsyncPageFilter
    {
        private readonly IConfiguration _config;

        public AdminAuthorizeFilter(IConfiguration config)
        {
            _config = config;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var http = context.HttpContext;
            if (!http.Request.Cookies.TryGetValue("AdminAuth", out var id) || string.IsNullOrEmpty(id))
            {
                context.Result = new RedirectResult("/verwaltung");
                return;
            }

            var connStr = _config.GetConnectionString("feuerwehr");
            if (string.IsNullOrEmpty(connStr))
            {
                http.Response.Cookies.Delete("AdminAuth");
                context.Result = new RedirectResult("/verwaltung");
                return;
            }

            try
            {
                await using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
                const string sql = @"SELECT TOP (1) [deleted] FROM [ffw].[dbo].[Benutzer] WHERE [id] = @id";
                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                var deletedObj = await cmd.ExecuteScalarAsync();

                var isDeleted = false;
                if (deletedObj != null && int.TryParse(deletedObj.ToString(), out var delVal))
                {
                    isDeleted = delVal == 1;
                }

                if (isDeleted)
                {
                    http.Response.Cookies.Delete("AdminAuth");
                    context.Result = new RedirectResult("/verwaltung");
                    return;
                }

                // allowed
                await next();
            }
            catch
            {
                http.Response.Cookies.Delete("AdminAuth");
                context.Result = new RedirectResult("/verwaltung");
                return;
            }
        }
    }
}
