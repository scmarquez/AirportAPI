# Documentación de Arquitectura - AirportAPI

## Tabla de Contenidos
1. [Descripción General](#descripción-general)
2. [Guía de Inicio Rápido](#guía-de-inicio-rápido)
3. [Arquitectura](#arquitectura)
4. [Estructura de Directorios](#estructura-de-directorios)
5. [Componentes Principales](#componentes-principales)
6. [Patrones de Diseño](#patrones-de-diseño)
7. [Flujo de Datos](#flujo-de-datos)
8. [Stack Tecnológico](#stack-tecnológico)
9. [Endpoints de la API](#endpoints-de-la-api)

---

## Descripción General

**AirportAPI** es una API REST desarrollada en **ASP.NET Core 8** que gestiona vuelos en un sistema aeroportuario. La aplicación permite crear, consultar y eliminar registros de vuelos, persistiendo los datos en una base de datos **PostgreSQL**.

El proyecto sigue una **Clean Architecture** combinada con el patrón **CQRS**, separando claramente las responsabilidades de lectura y escritura. Toda la infraestructura (base de datos y entorno de ejecución) se gestiona mediante **Docker Compose**, y las operaciones comunes se ejecutan a través de un **Makefile**.

### Características Principales
- Gestión de vuelos (crear, consultar, eliminar)
- API REST documentada con Swagger/OpenAPI
- Configuración CORS para comunicación con frontend
- Base de datos PostgreSQL con Entity Framework Core
- Entorno de desarrollo dockerizado
- Makefile para simplificar operaciones comunes

## Guía de Inicio Rápido

### Requisitos Previos
- Docker y Docker Compose
- Make

### 1. Clonar el repositorio
```bash
git clone <repository-url>
cd AirportAPI
```

### 2. Levantar los contenedores
```bash
make up
```
Esto levanta dos contenedores:
- **postgres_db**: Base de datos PostgreSQL 16
- **airportapi**: Entorno .NET SDK 8.0 con el código fuente montado

El contenedor de la API queda en modo idle, listo para recibir comandos.

### 3. Aplicar las migraciones
```bash
make migrate
```
Ejecuta `dotnet ef database update` dentro del contenedor de la API contra la base de datos PostgreSQL.

### 4. Arrancar la API
```bash
make start
```
Arranca la aplicación en `http://localhost:5000`. Swagger disponible en `http://localhost:5000/swagger`.

### 5. Parar los contenedores
```bash
make down
```

### Resumen de comandos

| Comando        | Descripción                              |
|----------------|------------------------------------------|
| `make up`      | Levanta los contenedores                 |
| `make down`    | Para y elimina los contenedores          |
| `make migrate` | Aplica las migraciones de base de datos  |
| `make start`   | Arranca la API dentro del contenedor     |

---

## Arquitectura

La aplicación implementa **Clean Architecture** combinada con el patrón **CQRS (Command Query Responsibility Segregation)**, organizando el código en capas independientes:

```
┌─────────────────────────────────────────────────────────┐
│                    API ENDPOINTS                         │
│              (Controllers - HTTP Layer)                  │
└──────────────────┬──────────────────────────────────────┘
                   │
        ┌──────────┴──────────┐
        │                     │
┌───────▼────────┐   ┌─────────▼────────┐
│   COMMANDS     │   │     QUERIES      │
│  (Mutations)   │   │   (Read-Only)    │
└───────┬────────┘   └────────┬─────────┘
        │                     │
        └──────────┬──────────┘
                   │
        ┌──────────▼──────────┐
        │   DOMAIN LAYER      │
        │  (Business Logic)   │
        │    (Flight Model)   │
        └──────────┬──────────┘
                   │
        ┌──────────▼──────────────┐
        │  REPOSITORY INTERFACE   │
        │  (Abstraction)          │
        └──────────┬──────────────┘
                   │
        ┌──────────▼──────────────┐
        │   INFRASTRUCTURE LAYER  │
        │  (Database Access)      │
        │  (PostgreSQL + EF Core) │
        └─────────────────────────┘
```

---

## Estructura de Directorios

```
AirportAPI/
├── Controllers/                          # Capa de presentación HTTP
│   └── Flight/
│       ├── Delete/    
│       │   └── DeleteFlightController.cs
│       ├── Get/
│       │   ├── GetAllFlightsController.cs
│       │   └── GetFlightController.cs
│       └── Post/
│           ├── PostFlightController.cs
│           └── PostFlightBodyDTO.cs
│
├── Application/                          # Capa de aplicación (CQRS)
│   └── Flight/
│       ├── Command/                       # Commands (operaciones que escriben)
│       │   └── RegisterFlight/
│       │       ├── RegisterFlightCommand.cs
│       │       └── RegisterFlightCommandHandler.cs
│       ├── Query/                         # Queries (operaciones que leen)
│       │   ├── GetAllFlightsQueryHandler.cs
│       │   └── GetFlightQueryHandler.cs
│       └── Exceptions/
│           └── FlightNotFoundException.cs
│
├── Domain/                                # Capa de dominio (Lógica de negocio)
│   ├── Model/
│   │   └── Flight.cs                    # Entidad de dominio
│   └── Service/
│       └── Flight/
│           └── IFlightRepository.cs      # Interfaz del repositorio
│
├── Infrastructure/                       # Capa de infraestructura
│   └── PostgreSQL/
│       ├── Configuration/
│       │   └── AppDBContext.cs           # DbContext de EF Core
│       └── Flight/
│           └── FlightRepository.cs       # Implementación del repositorio
│
├── Migrations/                            # Historial de cambios de BD
│   ├── 20240821173902_init.cs
│   ├── 20240821173902_init.Designer.cs
│   └── AppDbContextModelSnapshot.cs
│
├── Properties/
│   └── launchSettings.json
│
├── Program.cs                             # Configuración de la aplicación
├── AirportAPI.csproj                      # Archivo de proyecto
├── AirportAPI.sln                         # Solución
├── Makefile                               # Comandos de desarrollo
├── appsettings.json                       # Configuraciones globales
├── appsettings.Development.json           # Configuraciones de desarrollo
├── docker-compose.yml                     # Orquestación de contenedores
└── AirportAPI.http                        # Ejemplos de peticiones HTTP
```

---

## Componentes Principales

### 1. **Controllers Layer** (Controllers/)
Responsables de recibir y responder HTTP requests.

#### `GetAllFlightsController`
```csharp
[ApiController]
[Route("Flight")]
public class GetAllFlightsController
{
    [HttpGet]
    public List<Flight> GetFlight()
    {
        var getAllFlightsQueryHandler = new GetAllFlightsQueryHandler(_flightRepository);
        return getAllFlightsQueryHandler.Handle();
    }
}
```
- **Endpoint**: `GET /Flight`
- **Descripción**: Recupera todos los vuelos de la base de datos
- **Response**: Lista de vuelos

#### `PostFlightController`
```csharp
[ApiController]
[Route("Flight")]
public class PostFlightController : ControllerBase
{
    [HttpPost]
    public async Task<StatusCodeResult> Post(PostFlightBodyDTO postFlightBodyDTO)
    {
        // Crea comando y lo procesa
        RegisterFlightCommand command = new(...)
        RegisterFlightCommandHandler handler = new(...)
        await handler.Handle(command);
        return Ok();
    }
}
```
- **Endpoint**: `POST /Flight`
- **Descripción**: Crea un nuevo vuelo
- **Request Body**: `PostFlightBodyDTO` con Origin, Destination, ArrivalDate, DepartureDate
- **Response**: 200 OK

#### `DeleteFlightController`
- **Endpoint**: `DELETE /Flight/{id}`
- **Descripción**: Elimina un vuelo por su ID

### DTO (Data Transfer Object)
```csharp
public class PostFlightBodyDTO
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string ArrivalDate { get; set; }
    public string DepartureDate { get; set; }
}
```
Transforma datos HTTP en objetos de dominio.

---

### 2. **Application Layer** (Application/)
Implementa la lógica de aplicación usando CQRS.

#### Commands (Mutaciones/Escritura)
**RegisterFlightCommand**
```csharp
public class RegisterFlightCommand
{
    public string Origin { get; }
    public string Destination { get; }
    public string ArrivalDate { get; }
    public string DepartureDate { get; }
}
```

**RegisterFlightCommandHandler**
```csharp
public class RegisterFlightCommandHandler
{
    public async Task Handle(RegisterFlightCommand command)
    {
        Flight flight = Flight.createFlight(
            command.Origin,
            command.Destination, 
            command.ArrivalDate, 
            command.DepartureDate
        );
        await _flightRepository.SaveFlight(flight);
    }
}
```

#### Queries (Lectura)
**GetAllFlightsQueryHandler**
```csharp
public class GetAllFlightsQueryHandler
{
    public List<Flight> Handle()
    {
        return _flightRepository.GetAllFlights();
    }
}
```

**GetFlightQueryHandler**
- Recupera un vuelo específico por ID

---

### 3. **Domain Layer** (Domain/)
Contiene la lógica de negocio core.

#### **Flight Model**
```csharp
public class Flight
{
    [Key]
    public Guid FlightID { get; set; }
    
    [Column("Origin")]
    public string Origin { get; set; }
    
    [Column("Destination")]
    public string Destination { get; set; }
    
    [Column("ArrivalDate")]
    public string ArrivalDate { get; set; }
    
    [Column("DepartureDate")]
    public string DepartureDate { get; set; }

    // Factory method para crear vuelos
    public static Flight createFlight(
        string origin, 
        string destination, 
        string arrivalDate, 
        string departureDate)
    {
        return new(new Guid(), origin, destination, arrivalDate, departureDate);
    }
}
```

**Características**:
- Identificador único: `FlightID` (GUID)
- Propiedades de navegación: origen, destino, fechas
- Factory method encapsula la creación
- Anotaciones EF Core para mapeo a BD

#### **IFlightRepository Interface**
```csharp
public interface IFlightRepository
{
    Task SaveFlight(Flight flight);
    Flight GetFlightByID(Guid id);
    Task DeleteFlight(Guid id);
    List<Flight> GetAllFlights();
}
```

Define el contrato para acceso a datos sin acoplamiento a la BD.

---

### 4. **Infrastructure Layer** (Infrastructure/)
Implementa la persistencia en base de datos.

#### **AppDbContext**
```csharp
public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{ 
    public DbSet<Flight> Flights { get; set; }  
}
```
- Contexto de Entity Framework Core
- Define la tabla `Flights` mapeada al modelo

#### **FlightRepository**
Implementa `IFlightRepository` y proporciona operaciones CRUD:
- `SaveFlight(Flight)`: Inserta vuelos nuevos
- `GetFlightByID(Guid)`: Busca por ID
- `DeleteFlight(Guid)`: Elimina por ID
- `GetAllFlights()`: Retorna todos los registros

---

### 5. **Configuración de Dependencias** (Program.cs)

```csharp
// DbContext con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

// Inyección del repositorio
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

// CORS para comunicación con frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Swagger/OpenAPI
builder.Services.AddSwaggerGen();
```

---

## Patrones de Diseño

### 1. **CQRS (Command Query Responsibility Segregation)**
Separa operaciones de lectura (Queries) y escritura (Commands):
- **Commands**: `RegisterFlightCommand` → cambios de estado
- **Queries**: `GetAllFlightsQueryHandler` → solo lectura

**Ventajas**:
- ✅ Escalabilidad independiente de lecturas y escrituras
- ✅ Claridad en la intención del código
- ✅ Facilita testing

### 2. **Repository Pattern**
- `IFlightRepository`: Abstracción de acceso a datos
- `FlightRepository`: Implementación concreta
- **Beneficio**: Independencia de la BD, fácil testing

### 3. **Dependency Injection**
```csharp
public PostFlightController(IFlightRepository _flightRepository)
```
- Inyección a través del constructor
- Loose coupling entre componentes

### 4. **Factory Method**
```csharp
public static Flight createFlight(string origin, ...) 
    => new(new Guid(), origin, ...)
```
- Encapsula lógica de creación de entidades
- Garantiza consistencia

### 5. **DTO (Data Transfer Object)**
- `PostFlightBodyDTO`: Mapea JSON HTTP a formato interno
- Separación entre API contracts y dominio

---

## Flujo de Datos

### Flujo 1: Obtener Todos los Vuelos (GET)

```
Client HTTP Request
        ↓
[GetAllFlightsController.GetFlight()]
        ↓
[GetAllFlightsQueryHandler.Handle()]
        ↓
[IFlightRepository.GetAllFlights()]
        ↓
[FlightRepository.GetAllFlights()]
        ↓
[AppDbContext.Flights] → PostgreSQL
        ↓
[List<Flight>]
        ↓
HTTP Response 200 OK + JSON
```

**Pasos**:
1. Cliente realiza `GET /Flight`
2. Controller instancia `GetAllFlightsQueryHandler`
3. Handler invoca el repositorio
4. Repositorio consulta la BD vía EF Core
5. DbContext mapea registros a objetos `Flight`
6. Retorna lista serializada como JSON

---

### Flujo 2: Crear Nuevo Vuelo (POST)

```
Client HTTP Request + JSON Body
        ↓
[PostFlightBodyDTO deserialization]
        ↓
[PostFlightController.Post(DTO)]
        ↓
[RegisterFlightCommand creation]
        ↓
[RegisterFlightCommandHandler.Handle(command)]
        ↓
[Flight.createFlight()] ← Factory Method
        ↓
[IFlightRepository.SaveFlight(flight)]
        ↓
[FlightRepository.SaveFlight(flight)]
        ↓
[AppDbContext.Flights.Add(flight)] + SaveChangesAsync()
        ↓
PostgreSQL Insert
        ↓
HTTP Response 200 OK
```

**Pasos**:
1. Cliente envía `POST /Flight` con JSON (`origin`, `destination`, `arrivalDate`, `departureDate`)
2. ASP.NET deserializa a `PostFlightBodyDTO`
3. Controller crea `RegisterFlightCommand`
4. Handler instancia `Flight` usando factory method
5. Handler invoca `SaveFlight()` en repositorio
6. Repositorio añade a DbContext y persiste en PostgreSQL
7. Retorna 200 OK

---

## Stack Tecnológico

| Componente | Versión | Propósito |
|-----------|---------|----------|
| **ASP.NET Core** | 8.0 | Framework web |
| **Entity Framework Core** | 8.0.8 | ORM para acceso a datos |
| **Npgsql** | 8.0.4 | Driver PostgreSQL para .NET |
| **PostgreSQL** | 16 | Base de datos relacional |
| **Swagger/Swashbuckle** | 6.4.0 | Documentación API interactiva |
| **Docker Compose** | - | Orquestación de contenedores |
| **.NET SDK** | 8.0 | Compilación y ejecución |

---

## Endpoints de la API

### 1. Obtener Todos los Vuelos
```http
GET /Flight
```
**Response** (200 OK):
```json
[
  {
    "flightID": "550e8400-e29b-41d4-a716-446655440000",
    "origin": "Madrid",
    "destination": "Barcelona",
    "arrivalDate": "2024-08-22T10:00:00",
    "departureDate": "2024-08-22T08:00:00"
  },
  ...
]
```

### 2. Obtener Vuelo por ID
```http
GET /Flight/{id}
```
**Response** (200 OK): Objeto Flight

### 3. Crear Nuevo Vuelo
```http
POST /Flight
Content-Type: application/json

{
  "origin": "Madrid",
  "destination": "Barcelona",
  "arrivalDate": "2024-08-22T10:00:00",
  "departureDate": "2024-08-22T08:00:00"
}
```
**Response** (200 OK)

### 4. Eliminar Vuelo
```http
DELETE /Flight/{id}
```
**Response** (200 OK / 404 Not Found)

---

## Configuración de CORS

La API acepta requests desde `http://localhost:5173`. Para cambiar el origen, editar en `Program.cs`:
```csharp
.WithOrigins("http://nuevo-dominio.com")
```

---

## Mejoras Futuras

- [ ] Autenticación y autorización (JWT)
- [ ] Paginación en `GetAllFlights`
- [ ] Validación de datos de entrada
- [ ] Unit tests e integration tests
- [ ] Logging centralizado
