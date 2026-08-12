
using System.Data;

namespace Catalog.Api.app.infrastructure.database;

public interface IDBConnectionFactory{
	IDbConnection CreateConnection();
}
    

