using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Response
{
    public class BaseAPIResponse<T>
    {
        public List<ValidationError> ValidationErrors { get; set; } = new();
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public void AddValidationError(string Key, string Value)
        {
            ValidationErrors.Add(new ValidationError { Key = Key, Value = Value });
        }

        public void SetMessage(string Message)
        {
            if (ValidationErrors.Any()) throw new Exception("Set message cant be use with validations errors");

            HasError = false;
            this.Message = Message;
        }

        public void SetErrorMessage(string Message)
        {
            HasError = true;
            this.Message = Message;
        }
    }
}
