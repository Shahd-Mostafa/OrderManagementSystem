using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<validationError> Errors { get; set; }
    }

    public class validationError
    {
        public string Field { get; set; }
        public IEnumerable<String> Errors { get; set; }
    }
}
