# Docker - PostgreSQL Setup

## Requisitos
- Docker instalado y funcionando
- Docker Compose instalado

## Instrucciones de uso

### 1. Iniciar el contenedor de PostgreSQL

```bash
docker-compose up -d
```

Este comando:
- Crea e inicia un contenedor de PostgreSQL
- Mapea el puerto 5432 del contenedor al puerto 5432 de tu máquina local
- Crea un volumen para persistencia de datos
- Crea la base de datos `test` automáticamente

### 2. Verificar que PostgreSQL está corriendo

```bash
docker-compose ps
```

O verificar los logs:

```bash
docker-compose logs postgres
```

### 3. Conectar desde tu aplicación

La cadena de conexión ya está configurada en `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost; Database=test; Username=postgres; Password=admin"
}
```

### 4. Acceder a PostgreSQL desde la línea de comandos (opcional)

```bash
docker-compose exec postgres psql -U postgres -d test
```

### 5. Detener el contenedor

```bash
docker-compose down
```

### 6. Detener y eliminar todo (incluyendo datos)

```bash
docker-compose down -v
```

## Notas
- Los datos persisten en el volumen `postgres_data` incluso si paras el contenedor
- El contenedor incluye un health check para verificar que PostgreSQL está listo
- La configuración coincide con tu `appsettings.Development.json`
