using Microsoft.Data.SqlClient;
using System.Data;


namespace Catalog.Api.app.infrastructure.database
{

	public class SqlConnectionFactory : IDBConnectionFactory {
		private readonly IConfiguration _configuration;

		public SqlConnectionFactory(IConfiguration configuration){
			_configuration = configuration;
		}


		public IDbConnection CreateConnection(){
			var connectionString = 
				_configuration.GetConnectionString("DefaultConnection")
				?? throw new Exception("La cadena de conexion no esta configurada");

			return new SqlConnection(connectionString);
		}


	}
    
}
