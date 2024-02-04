using CwkApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Models
{
    public class OperationResault<T>
    {
        public T PayLoad { get; set; }
        public bool IsError  { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
        public void AddError(ErrorCode code, string message)
        {
            HandleError(code, message);
        }
        public void AddUnknownError(string message)
        {
            HandleError(ErrorCode.UnknownError, message);
        }
        public void ResetIsErrorFlag()
        {
            IsError = false;
        }

        #region Private methods

        private void HandleError(ErrorCode code, string message)
        {
            Errors.Add(new Error { code = code, Massage = message });
            IsError = true;
        }

        #endregion
    }
}
