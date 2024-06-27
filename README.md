* Proyecto desarrollado en .NET 8
* Para utilizar el proyecto se debe cambiar el ConnectionString que hay en el appsettings por el de la base de datos que van a utilizar
* Aclaración: Las migraciones que hice tienen inserción de datos: Algunos productos de prueba, un usuario administrador (username admin, password 1234), y los roles de usuarios.
El único endpoint que no requiere autenticarse es el endpoint de autenticación... el resto requieren de estar autenticado.
* Roles y permisos:
Admin: Permiso en todos los endpoints,
StockManager: Solo 403 en dar de alta usuarios, para el resto tiene permisos,
Customer: Solo puede acceder al endpoint de obtención filtrada de productos, de todas formas, este endpoint lo único que requeriría es estar autenticado.
* Middleware y logs, para toda excepción que ocurra durante la request, se loguea con NLog en el path configurado en el archivo nlog.config, se loguea con un correlationId, el cual se incluye también en la respuesta.
* Se utilizó Result Pattern para manejo de errores, las excepciones se manejan en el catch del middleware
