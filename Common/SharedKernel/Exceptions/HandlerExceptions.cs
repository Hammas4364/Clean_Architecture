namespace SharedKernel.Exceptions;
using SharedKernel.Constants;
using static SharedKernel.Constants.Messages;

public static class HandlerExceptions
{
    public static class CommonHandlerExceptions
    {
        public static AppException NameAlreadyExist => new(ValidationExceptions.GetValidationErrors(Messages.CommonMessages.AlreadyExist, CommonFields.Name));
        public static AppException IdDoesNotExist => new(ValidationExceptions.GetValidationErrors(Messages.CommonMessages.DoesNotExist, CommonFields.Id));
        public static AppException ItemNotFound => new(ValidationExceptions.GetValidationErrors(Messages.CommonMessages.DoesNotExist, CommonFields.Item));
    }
    public static class ControllerHandlerExceptions
    {
        public static AppException ControllerNameAlreadyExist => new(ValidationExceptions.GetValidationErrors("." + CommonMessages.AlreadyExist, ControllerFields.ControllerName));
        public static AppException ControllerDoesNotExists => new(ValidationExceptions.GetValidationErrors("." + CommonMessages.DoesNotExist, ControllerFields.Controller));
    }
}
