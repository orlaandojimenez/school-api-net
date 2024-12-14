# School API - C# .NET Core

Este es un proyecto de API para la gestión de calificaciones escolares, desarrollado en C# con .NET Core. La API permite realizar operaciones CRUD sobre estudiantes, calificaciones y más.

## Requisitos

- [.NET 6.0 SDK o superior](https://dotnet.microsoft.com/download)
- [MySQL](https://www.mysql.com/) o [XAMPP](https://www.apachefriends.org/es/index.html) para simular un servidor MySQL

## Instrucciones para ejecutar el proyecto

1. **Clonar el repositorio**:

   ```bash
   git clone https://github.com/tu_usuario/tu_repositorio.git

   ```

2. **Configurar el archivo appsettings.json**:
   Al clonar el proyecto, asegúrate de revisar el archivo appsettings.json, especialmente las siguientes configuraciones:
   • ConnectionString:
   En la sección "ConnectionStrings", asegúrate de configurar los parámetros de la conexión a tu base de datos de MySQL. Reemplaza los valores DB_HOST, DB_USER, DB_PASSWORD, DB_NAME, y DB_PORT según corresponda.

"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=school_management;User=root;Password=myPassword123!;Port=3306"
}

• JWT Key:
En la sección "Jwt", asegúrate de configurar la clave secreta para la autenticación JWT. Este es un valor que se utiliza para firmar y verificar los tokens JWT. Puedes generar tu propia clave secreta si es necesario.

"Jwt": {
"Key": "KJwN0+xtfQjOM85qe14CxT557bshwRqhYbYOpTkE9Pw=",
"Issuer": "school-api-net",
"Audience": "YourApiClients"
}

3. **Instalar las dependencias**:
   Si es la primera vez que ejecutas el proyecto, necesitarás restaurar los paquetes NuGet para el proyecto. Ejecuta el siguiente comando:
   dotnet restore

4. **Ejecutar la API**:
   Una vez que hayas configurado la base de datos y el archivo appsettings.json, puedes ejecutar el proyecto con el siguiente comando:
