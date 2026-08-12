namespace Catalog.Api.app.domain.error
{
    
	public class CustomError : Exception {
		public int code { get;}
		public string status { get; }

		private CustomError(int code, string status, string message): base(message){
			this.code = code;
			this.status = status;
		}

		public static CustomError BadRequest( string message) => new CustomError(400, "Bad-Request", message);

		public static CustomError Unauthorized( string message) => new CustomError(401, "Unauthorized", message);

		public static CustomError Forbidden( string message) => new CustomError(403, "Forbidden", message);

		public static CustomError NotFound( string message) => new CustomError(404, "Not-Found", message);

		public static CustomError Conflict( string message) => new CustomError(409, "Conflict", message);

		public static CustomError InternalServer( string message) => new CustomError(500, "Internal-Server", message);

	}
}
