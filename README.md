# ShopAI Catalog Service

Microservicio de **catálogo** de ShopAI, encargado de administrar categorías, productos, inventario y la información utilizada por el dashboard administrativo.

El servicio forma parte de la arquitectura de microservicios de ShopAI y es consumido a través del **API Gateway**.

## Arquitectura

El flujo general dentro de ShopAI es:

```text
                         CLIENTES
                            │
                            ▼
                    ┌───────────────┐
                    │ API Gateway   │
                    │     :3000     │
                    └───────┬───────┘
                            │
                            │ /api/v1/categories/**
                            │ /api/v1/products/**
                            │ /api/v1/dashboard/**
                            ▼
                    ┌───────────────┐
                    │ Catalog       │
                    │ Service       │
                    │     :3002     │
                    └───────┬───────┘
                            │
                            ▼
                    ┌───────────────┐
                    │   SQL Server  │
                    │shop_ai_catalog│
                    └───────────────┘
```

El Catalog Service mantiene la responsabilidad sobre la lógica de negocio relacionada con el catálogo y valida los JWT de las operaciones protegidas.

## Tecnologías

- C# / .NET 10
- ASP.NET Core
- Dapper
- Microsoft.Data.SqlClient
- SQL Server
- JWT
- OpenAPI
- Maven — no aplica
- Visual Studio / VS Code

Dependencias principales del proyecto:

```text
Dapper
Microsoft.AspNetCore.OpenApi
Microsoft.Data.SqlClient
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.IdentityModel.JsonWebTokens
Microsoft.IdentityModel.Tokens
System.IdentityModel.Tokens.Jwt
```

> La persistencia actualmente implementada utiliza **Dapper + stored procedures** mediante `Microsoft.Data.SqlClient`. Aunque el proyecto incluye referencias de Entity Framework Core, no existe actualmente un `DbContext` como mecanismo principal de acceso a datos.

## Requisitos

Para ejecutar el servicio localmente se necesita:

- .NET 10 SDK.
- SQL Server.
- Base de datos `shop_ai_catalog`.
- Credenciales válidas para SQL Server.
- Configuración válida de JWT.

Comprobar la versión instalada:

```powershell
dotnet --version
```

## Puertos

| Servicio | Puerto | Descripción |
|---|---:|---|
| API Gateway | `3000` | Punto de entrada de los clientes |
| Identity Service | `3001` | Autenticación e identidad |
| Catalog Service | `3002` | Categorías, productos e inventario |

El perfil HTTP del proyecto utiliza:

```text
http://localhost:3002
```

También existe un perfil HTTPS configurado en `launchSettings.json`:

```text
https://localhost:7002
```

## Configuración

La configuración de ASP.NET Core se encuentra en:

```text
appsettings.json
```

La configuración específica de desarrollo se maneja mediante:

```text
appsettings.Development.json
```

Configuración base:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=shop_ai_catalog;User Id=USERNAME;Password=PASSWORD;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Secret": "",
    "Issuer": "",
    "Audience": ""
  }
}
```

### Configuración de base de datos

El servicio utiliza la cadena:

```text
ConnectionStrings:DefaultConnection
```

Ejemplo:

```text
Server=localhost;Database=shop_ai_catalog;User Id=USERNAME;Password=PASSWORD;TrustServerCertificate=True;
```

La conexión es creada mediante:

```text
SqlConnectionFactory
```

a través de la abstracción:

```text
IDBConnectionFactory
```

### Configuración JWT

El servicio necesita:

```json
{
  "Jwt": {
    "Secret": "",
    "Issuer": "",
    "Audience": ""
  }
}
```

Estos valores son utilizados por `JwtPlugin` para validar los tokens emitidos por Identity Service.

**El secreto JWT no debe almacenarse en el repositorio.**

## Base de datos

Base de datos utilizada:

```text
shop_ai_catalog
```

El catálogo actualmente trabaja principalmente mediante **stored procedures**.

La capa de infraestructura utiliza Dapper para ejecutar los procedimientos almacenados y mapear sus resultados a las entidades y DTOs.

### Categorías

La entidad `Category` representa las categorías del catálogo.

Campos principales:

```text
Id
Name
Slug
Description
ImageUrl
IsActive
CreatedAt
UpdatedAt
```

### Productos

La entidad `Product` representa los productos.

Campos principales:

```text
Id
CategoryId
Name
Slug
ShortDescription
Description
Price
Sku
IsActive
CreatedAt
UpdatedAt
```

### Inventario

El inventario se representa mediante:

```text
ProductInventory
```

con:

```text
ProductId
Stock
ReservedStock
```

Las operaciones de productos reciben y actualizan la información de inventario junto con el producto.

## Stored Procedures

La capa `Datasource` utiliza stored procedures para las operaciones de persistencia.

### Categorías

Entre los procedimientos utilizados actualmente:

```text
sp_create_category
sp_delete_categoty_by_id
sp_categories_all
sp_categories_by_active
sp_category_by_slug
sp_category_by_slug_and_active
sp_category_by_id
sp_update_category
```

### Productos

```text
sp_create_product
sp_product_delete_by_id
sp_products
sp_products_by_active
sp_product_by_id
sp_product_by_sku
sp_product_by_slug
sp_product_by_slug_active
sp_product_update_by_id
```

### Dashboard

```text
sp_catalog_product_summary
```

Los stored procedures forman parte de la capa de persistencia de Catalog y deben existir en la base de datos `shop_ai_catalog` antes de utilizar las operaciones correspondientes.

## API

Todas las respuestas utilizan una estructura común mediante:

```text
ApiResponse<T>
```

La respuesta general sigue el formato:

```json
{
  "code": 200,
  "status": "Ok",
  "message": "Operación realizada correctamente",
  "data": {}
}
```

Los errores son procesados mediante:

```text
ExceptionMiddleware
```

## Autenticación y autorización

Las operaciones protegidas requieren un JWT enviado mediante:

```http
Authorization: Bearer <access-token>
```

El middleware:

```text
JwtMiddleware
```

realiza las siguientes validaciones:

1. Comprueba la existencia del header `Authorization`.
2. Comprueba que utilice el esquema `Bearer`.
3. Extrae el token.
4. Valida firma, issuer y expiración.
5. Comprueba el tipo del token.
6. Obtiene el rol del usuario.
7. Requiere el rol `ADMIN`.
8. Coloca el `ClaimsPrincipal` en `HttpContext.User`.

Los tokens utilizados para operaciones del catálogo deben ser de tipo:

```text
ACCESS
```

Las operaciones públicas utilizan:

```csharp
[AllowAnonymous]
```

mediante el atributo personalizado del proyecto.

## Validación JWT

La validación se centraliza en:

```text
app/plugins/JwtPlugin.cs
```

Se utilizan:

```text
Jwt:Secret
Jwt:Issuer
Jwt:Audience
```

La validación actual contempla:

```text
ValidateIssuerSigningKey = true
ValidateIssuer = true
ValidateLifetime = true
ClockSkew = TimeSpan.Zero
```

La implementación actual no valida el `Audience` durante la validación (`ValidateAudience = false`), aunque el valor `Jwt:Audience` se mantiene dentro de la configuración.

## Categorías

Base:

```text
/api/v1/categories
```

### Obtener categorías activas

```http
GET /api/v1/categories
```

No requiere autenticación.

### Obtener todas las categorías

```http
GET /api/v1/categories/all
```

Requiere:

```http
Authorization: Bearer <access-token>
```

### Obtener categoría por ID

```http
GET /api/v1/categories/{id}
```

Ejemplo:

```text
GET /api/v1/categories/550e8400-e29b-41d4-a716-446655440000
```

Requiere autenticación.

### Obtener categoría por slug

```http
GET /api/v1/categories/slug/{slug}
```

No requiere autenticación.

Ejemplo:

```text
GET /api/v1/categories/slug/electronica
```

### Crear categoría

```http
POST /api/v1/categories
```

Requiere rol:

```text
ADMIN
```

Body:

```json
{
  "name": "Electrónica",
  "slug": "electronica",
  "description": "Productos electrónicos"
}
```

### Actualizar categoría

```http
PUT /api/v1/categories/{id}
```

Requiere rol:

```text
ADMIN
```

Body:

```json
{
  "name": "Electrónica",
  "slug": "electronica",
  "description": "Productos electrónicos",
  "isActive": true
}
```

### Eliminar categoría

```http
DELETE /api/v1/categories/{id}
```

Requiere rol:

```text
ADMIN
```

## Productos

Base:

```text
/api/v1/products
```

### Obtener productos activos

```http
GET /api/v1/products
```

No requiere autenticación.

### Obtener todos los productos

```http
GET /api/v1/products/all
```

Requiere rol:

```text
ADMIN
```

### Obtener producto por ID

```http
GET /api/v1/products/{id}
```

Requiere autenticación.

### Obtener producto por slug

```http
GET /api/v1/products/slug/{slug}
```

No requiere autenticación.

### Crear producto

```http
POST /api/v1/products
```

Requiere rol:

```text
ADMIN
```

Body de referencia:

```json
{
  "categoryId": "550e8400-e29b-41d4-a716-446655440000",
  "name": "Producto de ejemplo",
  "slug": "producto-de-ejemplo",
  "shortDescription": "Descripción corta",
  "description": "Descripción completa del producto",
  "price": 25000,
  "sku": "PROD-001",
  "stock": 100
}
```

### Actualizar producto

```http
PUT /api/v1/products/{id}
```

Requiere rol:

```text
ADMIN
```

Body:

```json
{
  "categoryId": "550e8400-e29b-41d4-a716-446655440000",
  "name": "Producto actualizado",
  "slug": "producto-actualizado",
  "shortDescription": "Descripción corta",
  "description": "Descripción completa",
  "price": 30000,
  "sku": "PROD-001",
  "isActive": true,
  "stock": 80
}
```

### Eliminar producto

```http
DELETE /api/v1/products/{id}
```

Requiere rol:

```text
ADMIN
```

## Dashboard

Base:

```text
/api/v1/dashboard
```

### Resumen

```http
GET /api/v1/dashboard/summary
```

Requiere autenticación.

Respuesta de referencia:

```json
{
  "code": 200,
  "status": "Ok",
  "message": "summary",
  "data": {
    "categoriesTotal": 10,
    "categoriesCreatedThisMonth": 2,
    "productsTotal": 50,
    "productsCreatedThisMonth": 8
  }
}
```

El resumen actualmente proporciona:

```text
CategoriesTotal
CategoriesCreatedThisMonth
ProductsTotal
ProductsCreatedThisMonth
```

## Validaciones

Las solicitudes de categorías y productos realizan validaciones antes de ejecutar los casos de uso.

Las validaciones se centralizan principalmente en:

```text
app/application/validations/Validator.cs
```

### Categorías

Se valida:

- Nombre obligatorio.
- Formato del nombre.
- Slug obligatorio.
- Formato del slug.
- Descripción obligatoria.
- Caracteres permitidos en la descripción.

### Productos

Se valida:

- Categoría obligatoria.
- Nombre obligatorio.
- Formato del nombre.
- Slug obligatorio.
- Formato del slug.
- Descripción corta.
- Descripción.
- Precio mayor a `5000`.
- SKU obligatorio.
- Formato del SKU.
- Stock no negativo.

Además, los casos de uso verifican reglas de negocio como:

- Slugs únicos.
- SKU únicos.
- Existencia de la categoría asociada.
- Existencia del producto antes de actualizar o eliminar.

## Manejo de errores

El proyecto utiliza:

```text
CustomError
```

para representar errores de aplicación.

`ExceptionMiddleware` transforma estas excepciones en respuestas HTTP estructuradas.

Estados utilizados por la lógica actual incluyen:

```text
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
500 Internal Server Error
```

Ejemplo:

```json
{
  "code": 403,
  "status": "Forbidden",
  "message": "No tiene permisos para realizar esta operacion",
  "data": null
}
```

## Estructura del proyecto

La aplicación está organizada por responsabilidades:

```text
Catalog.Api/
│
├── app/
│   │
│   ├── application/
│   │   ├── usecases/
│   │   │   ├── category/
│   │   │   ├── product/
│   │   │   └── dashboard/
│   │   │
│   │   └── validations/
│   │
│   ├── domain/
│   │   ├── common/
│   │   ├── datasources/
│   │   ├── dtos/
│   │   │   ├── requests/
│   │   │   └── responses/
│   │   ├── entities/
│   │   ├── errors/
│   │   └── repositories/
│   │
│   ├── infrastructure/
│   │   ├── database/
│   │   ├── datasources/
│   │   └── repositories/
│   │
│   ├── plugins/
│   │   └── JwtPlugin.cs
│   │
│   └── presentation/
│       ├── attributes/
│       ├── controllers/
│       └── middlewares/
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Catalog.Api.csproj
└── Catalog.Api.slnx
```

## Capas

### Application

Contiene los casos de uso de la aplicación.

```text
usecases/
├── category/
├── product/
└── dashboard/
```

Cada caso de uso representa una operación concreta del negocio.

### Domain

Contiene los elementos centrales del dominio:

```text
entities
DTOs
repositories
datasources
errors
common
```

Las interfaces de repositorio y datasource permiten desacoplar la lógica de aplicación de la implementación concreta de persistencia.

### Infrastructure

Implementa el acceso a datos.

```text
infrastructure/
├── database/
├── datasources/
└── repositories/
```

La conexión SQL Server se abstrae mediante:

```text
IDBConnectionFactory
```

y se implementa mediante:

```text
SqlConnectionFactory
```

Los datasources utilizan Dapper para ejecutar stored procedures.

### Presentation

Contiene:

```text
controllers/
middlewares/
attributes/
```

Los controllers exponen la API HTTP y los middlewares procesan errores y autenticación.

## Inyección de dependencias

La configuración de servicios se encuentra en:

```text
Program.cs
```

Se registran:

- `IDBConnectionFactory`
- Repositories
- Datasources
- Category use cases
- Product use cases
- Dashboard use cases
- `JwtPlugin`

Los servicios de aplicación se registran con ciclo de vida:

```text
Scoped
```

## Ejecución

Desde la carpeta del proyecto:

```powershell
dotnet restore
dotnet run
```

Para ejecutar explícitamente en el perfil HTTP:

```powershell
dotnet run --launch-profile http
```

El servicio queda disponible en:

```text
http://localhost:3002
```

## OpenAPI

Durante el entorno `Development`, la aplicación registra OpenAPI:

```csharp
app.MapOpenApi();
```

Esto permite disponer del documento OpenAPI generado por ASP.NET Core.

## Build

Restaurar dependencias:

```powershell
dotnet restore
```

Compilar:

```powershell
dotnet build
```

Ejecutar:

```powershell
dotnet run
```

Publicar:

```powershell
dotnet publish -c Release
```

## Integración con API Gateway

Catalog Service es consumido por el API Gateway mediante:

```text
http://localhost:3002
```

Ejemplo:

```text
Cliente
   │
   │ GET http://localhost:3000/api/v1/categories
   ▼
API Gateway
   │
   │ http://localhost:3002/api/v1/categories
   ▼
Catalog Service
   │
   ▼
SQL Server
```

De esta forma, los clientes no necesitan comunicarse directamente con el microservicio.

## Seguridad y responsabilidades

La arquitectura actual mantiene la validación JWT dentro de los microservicios.

Para Catalog:

```text
API Gateway
     │
     ▼
Catalog Service
     │
     ├── JwtMiddleware
     │
     ├── Validación JWT
     │
     ├── Validación de tipo ACCESS
     │
     └── Validación de rol ADMIN
```

Las operaciones públicas se marcan explícitamente con:

```csharp
[AllowAnonymous]
```

## Configuración sensible y Git

No se deben versionar:

- Contraseñas de SQL Server.
- Secretos JWT.
- Credenciales.
- Tokens.
- Cadenas de conexión con credenciales reales.
- Archivos generados por Visual Studio.
- `bin/`.
- `obj/`.

La configuración de ejemplo debe mantenerse sin secretos reales.

Para desarrollo local se recomienda utilizar la configuración específica del entorno o variables de entorno.

## Estado actual

Catalog Service actualmente proporciona:

- CRUD de categorías.
- Consulta de categorías activas.
- Consulta de categorías por slug.
- CRUD de productos.
- Consulta de productos activos.
- Consulta de productos por slug.
- Gestión de stock.
- Resumen para dashboard administrativo.
- Validación de JWT.
- Autorización por rol `ADMIN`.
- Persistencia mediante SQL Server.
- Dapper.
- Stored procedures.
- Respuestas estructuradas.
- Manejo global de errores.
- Validaciones de entrada y reglas de negocio.

## Limitaciones actuales

Algunas capacidades están preparadas en las interfaces pero todavía no tienen implementación completa.

Actualmente:

```text
IProductDatasource.FindByCategory()
IProductDatasource.FindByCategoryActive()
```

están pendientes de implementación.

Estas operaciones no deben considerarse endpoints disponibles hasta que se implemente la lógica correspondiente.

## Arquitectura ShopAI

Catalog Service forma parte del núcleo de catálogo de ShopAI:

```text
                         CLIENTES
                            │
                            ▼
                    ┌───────────────┐
                    │ API Gateway   │
                    │     :3000     │
                    └───────┬───────┘
                            │
            ┌───────────────┴────────────────┐
            │                                │
            ▼                                ▼
     ┌───────────────┐                ┌───────────────┐
     │   Identity    │                │    Catalog    │
     │     :3001     │                │     :3002     │
     └───────────────┘                └───────┬───────┘
                                             │
                                             ▼
                                      ┌───────────────┐
                                      │   SQL Server  │
                                      │shop_ai_catalog│
                                      └───────────────┘
```

El Gateway centraliza el acceso de los clientes, Identity administra la identidad y Catalog mantiene la información del catálogo.

---

**ShopAI** — Plataforma de comercio electrónico basada en arquitectura de microservicios.
