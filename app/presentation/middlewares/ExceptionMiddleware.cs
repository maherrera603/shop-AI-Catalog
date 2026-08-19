using System.Text.Json;

using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.common;


namespace Catalog.Api.app.presentation.middlewares
{

	public class ExceptionMiddleware {

		private readonly RequestDelegate _next;
		
		public ExceptionMiddleware(RequestDelegate next){
			this._next = next;
		}
		
		public async Task InvokeAsync(HttpContext context){
			try
			{
			    await this._next(context);
			}
			catch (CustomError error)
			{
				context.Response.StatusCode = error.code;
				context.Response.ContentType = "application/json";

				var response = ApiResponse<object>.Error( 
					error.code,
					error.status,
					error.Message
				);

				await context.Response.WriteAsync(JsonSerializer.Serialize(response));
			}catch(Exception ex){
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				context.Response.ContentType = "application/json";

				var response = ApiResponse<object>.Error ( 
					StatusCodes.Status500InternalServerError,
					"Internal-Server-Error",
					"Ha occurido un error interno: " + ex.Message
				);

				await context.Response.WriteAsync(JsonSerializer.Serialize(response));
			}
		}
	}
}
