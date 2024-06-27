namespace API.StockManagement.EndpointDocumentations
{
    internal sealed class Documentation
    {
        public static class Auth
        {
            public const string Authenticate = "Devuelve JWT a partir de las credenciales ingresadas";
        }

        public static class Product
        {
            public const string CreateProduct = "Crea un producto";
            public const string GetProducts = "Obtiene todos los productos";
            public const string GetProductById = "Obtiene producto por su Id";
            public const string UpdateProduct = "Modifica propiedades del producto";
            public const string DeleteProduct = "Elimina un producto";
            public const string ProductOffetByBudget = "Obtiene la oferta de dos distintos tipos de productos, que sumados den, el precio más alto posible, y que el mismo no supere el presupuesto ingresado";
        }

        public static class User
        {
            public const string CreateUser = "Crea un usuario con un rol";
        }
    }
}
