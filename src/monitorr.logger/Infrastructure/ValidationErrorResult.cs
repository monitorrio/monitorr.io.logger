using System.Collections.Generic;
using System.Text;

namespace monitorr.logger.Infrastructure
{
    public class ValidationErrorResult
    {
        private readonly IList<ValidationError> _errors;
        public bool IsEmpty => _errors.Count == 0;

        public ValidationErrorResult()
        {
            _errors = new List<ValidationError>();
        }


        public void Add(string message, string fieldName = "")
        {
            var error = new ValidationError()
            {
                Message = message,
                FieldName = fieldName,
            };
            _errors.Add(error);
        }
       
        public void AddFormat(string message, string fieldName, params object[] arguments)
        {
            Add(string.Format(message, arguments), fieldName);
        }

        public override string ToString()
        {
            if (_errors.Count < 1)
                return "";

            StringBuilder sb = new StringBuilder(128);

            foreach (ValidationError error in _errors)
            {
                sb.AppendLine(error.Message);
            }

            return sb.ToString();
        }
    }
}
