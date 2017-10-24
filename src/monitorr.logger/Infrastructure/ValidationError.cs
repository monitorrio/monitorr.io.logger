namespace monitorr.logger.Infrastructure
{
    public class ValidationError
    {
        public string Message { get; set; }

        public string FieldName { get; set; }

        public ValidationError() { }

        public ValidationError(string message, string fieldName = null)
        {
            Message = message;
            FieldName = fieldName;
        }
    }
}
