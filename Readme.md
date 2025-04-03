# GreenPhyto API

## Overview
GreenPhyto API provides endpoints for managing plant sensor data, retrieving configurations, and integrating with third-party services.

Design with clean architecture, Vertical Slice Architecture in mind.
It's achived by grouping all the classes that are related to a feature in a single folder. 
This way, all the classes that are related to a feature are in one place, and you can easily find them.
With the help of mediatR and GenericResponse of T.

Ideally, the service project would be a microservice that runs over a message bus. This way, the webapi is not coupled to the service and can be easily replaced with another component.
And opentelemetry is for tracing.

## Features
- Fetch plant sensor data
- Update plant configurations
- Third-party API integration
- Uses PostgreSQL for data storage

## Prerequisites
- .NET 8 or later
- PostgreSQL installed

## Database Setup
To restore the PostgreSQL database from the provided dump file:

1. Open a terminal or command prompt.
2. Use the following command to restore the dump:
   ```sh
   .\createdb -U postgres GreenphytoDB
   .\pg_restore -U postgres -d GreenphytoDB ...\isaac.tar
   ```
   Replace `path/to/...\isaac.tar` with the actual path to your dump file.
   File is at ...\Greenphyto\isaac.tar

3. Enter the password `1234` when prompted.
4. Verify the restoration:
   ```sh
   .\psql -U postgres -d GreenphytoDB -c "\dt"
   ```

## API Usage

### Get Plant Sensor Data
**Endpoint:** `GET /api/plants/plant-sensor-data`

**Response Example:**
```json
[
  {
    "tray_id": 0,
    "plant_type": "string",
    "target_temperature": 0,
    "target_humidity": 0,
    "target_light": 0,
    "sensor_temperature": 0,
    "sensor_humidity": 0,
    "sensor_light": 0
  }
]
```

## Running the Application
1. Restore dependencies:
   ```sh
   dotnet restore
   ```
2. Build the project:
   ```sh
   dotnet build
   ```
3. Run the application:
   ```sh
   dotnet run
   ```

## HTTP Requests Example

### Get Plant Sensor Data via HTTP file

```http
@Greenphyto_HostAddress = http://localhost:5107

GET {{Greenphyto_HostAddress}}/plant-sensor-data/
Accept: application/json
```
You may use the built-in http file and click on the send request button to get the plant sensor data.
You may also use the Swagger UI to test the API endpoints.


