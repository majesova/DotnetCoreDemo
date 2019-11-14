using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PDV.API.Infrastructure.ManagedResponses
{
     public enum ManagedErrorCode
    {
        Exception,
        Validation,
        Deny,
    }
    public class ManagedErrorResponse
    {
        public ManagedErrorResponse(ManagedErrorCode code, string friendlyMessage)
        {
            this.errorCode = code;
            this.friendlyMessage = friendlyMessage;
        }
        public ManagedErrorResponse(ManagedErrorCode code, string friendlyMessage, List<string> errors)
        {
            this.errorCode = code;
            this.friendlyMessage = friendlyMessage;
            this.errors = errors;

        }
        public ManagedErrorResponse(ManagedErrorCode code, string friendlyMessage, Exception exception)
        {
            this.errorCode = code;
            this.friendlyMessage = friendlyMessage;
            this.exception = exception;
            if (exception.InnerException != null)
                this.innerException = exception.InnerException;
        }
        public ManagedErrorResponse(ManagedErrorCode code, string friendlyMessage, ModelStateDictionary modelState)
        {
            this.errorCode = code;
            this.friendlyMessage = friendlyMessage;
            errors = new List<string>();
            foreach (var value in modelState.Values) 
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
        }
        private string friendlyMessage;
        public string FriendlyMessage
        {
            get { return friendlyMessage; }
            private set { friendlyMessage = value; }
        }

        private List<string> errors;
        public List<string> Errors
        {
            get { return errors; }
            set { errors = value; }
        }

        private Exception exception;
        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }

        private Exception innerException;
        public Exception InnerException
        {
            get { return innerException; }
            set { innerException = value; }
        }

        private ManagedErrorCode errorCode;
        public ManagedErrorCode ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }


    }
    
}