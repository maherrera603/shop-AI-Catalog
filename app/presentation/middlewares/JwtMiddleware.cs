using System.Security.Claims;
using Catalog.Api.app.plugins;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.presentation.attributes;


namespace Catalog.Api.app.presentation.middlewares
{

	public class JwtMiddleware{
		private readonly RequestDelegate _next;
		private readonly JwtPlugin _jwtPlugin;

		public JwtMiddleware(RequestDelegate next, JwtPlugin jwtPlugin){
			this._next = next;
			this._jwtPlugin = jwtPlugin;
		}


		public async Task InvokeAsync(HttpContext context){
			var endpoint = context.GetEndpoint();

			if(endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null){
				await _next(context);
				return;
			}
		

			var authorization = context.Request.Headers.Authorization.FirstOrDefault();

			if(string.IsNullOrWhiteSpace(authorization)) throw CustomError.Unauthorized("El encabezado authorization es obligatorio");

			if(!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) throw CustomError.Unauthorized("El token debe utilizar el esquema Bearer");

			var token = authorization["Bearer ".Length..].Trim();
			if(string.IsNullOrWhiteSpace(token)) throw CustomError.Unauthorized("El token es obligatorio");

			var principal = this._jwtPlugin.ValidateToken(token);

			string role = principal.FindFirst(ClaimTypes.Role)?.Value;
			if(role != "ADMIN") throw CustomError.Forbidden("No tiene permisos para realizaar esta operacion");

			context.User = principal;

			await this._next(context);
		}
	}

} 
