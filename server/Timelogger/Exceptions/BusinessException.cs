using System;
using System.Collections.Generic;

namespace Timelogger.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly List<BusinessError> _businessErrors;

        public BusinessException(List<BusinessError> businessErrors)
        {
            _businessErrors = businessErrors;
        }

        public BusinessException(BusinessError businessError)
        {
            _businessErrors = new List<BusinessError>();
            _businessErrors.Add(businessError);
        }

        public IReadOnlyCollection<BusinessError> BusinessErrors => _businessErrors?.AsReadOnly();
    }
}
