using API.StockManagement.Domain.Enums;

namespace API.StockManagement.Domain.Common
{
    public record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorTypeEnum.Failure);
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", ErrorTypeEnum.Failure);

        private Error(string code, string description, ErrorTypeEnum errorType)
        {
            Code = code;
            Description = description;
            Type = errorType;
        }

        public string Code { get; }

        public string Description { get; }

        public ErrorTypeEnum Type { get; }

        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorTypeEnum.NotFound);

        public static Error Validation(string code, string description) =>
            new(code, description, ErrorTypeEnum.Validation);

        public static Error Conflict(string code, string description) =>
            new(code, description, ErrorTypeEnum.Conflict);

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorTypeEnum.Failure);

        public static Error Unauthorized(string code, string description) =>
            new(code, description, ErrorTypeEnum.Unauthorized);

        public static Error NoContent(string code, string description) =>
            new(code, description, ErrorTypeEnum.NoContent);
    }
}
