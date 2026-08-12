using System.Text.Json.Serialization;


namespace Catalog.Api.app.domain.common {

	public class ApiResponse<T> {
		public int code { get; set;}
		public string status { get; set; }
		public string message { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public T? data { get; set; }

		public ApiResponse(){}

		public ApiResponse( int code, string status, string message, T? data){
			this.code = code;
			this.status = status;
			this.message = message;
			this.data = data;
		}


		public static ApiResponse<T> Success(T data, string message){
			return new ApiResponse<T>(200, "OK", message, data);
		}

		public static ApiResponse<T> Created(T data, string message){
			return new ApiResponse<T>(201, "CREATED", message, data);
		}

		public static ApiResponse<T> Error(int code, string status, string message){
			return new ApiResponse<T>(code, status, message, default);
		}
	}
}
