using SharedKernel.Constants;
namespace SharedKernel.Exceptions;

public static partial class AppExceptions
{
    public static class EventsExceptions
    {
        public static AppException EventCantBeAddedInWhenMethod => new(Messages.EventsMessages.EventCantBeAddedInWhenMethod);
    }

    public static class ControllerExceptions
    {
        public static AppException Door1AlreadyExist => new(ValidationExceptions.GetValidationErrors(".Already.Exist", "Door1"), 400);
        public static AppException DoorsMustBeNotBeGreaterThan2 => new(Messages.ControllerMessages.DoorsMustBeNotBeGreaterThan2);
        public static AppException CannotAddDoorMoreThan1WhenOneDoorConfigIsEnabled => new(Messages.ControllerMessages.CannotAddDoorMoreThan1WhenOneDoorConfigIsEnabled);
    }

    public static class DoorExceptions
    {
        public static AppException ReadersMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled => new(Messages.DoorMessages.ReadersMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled);
        public static AppException RexMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled => new(Messages.DoorMessages.RexMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled);
        public static AppException ReaderMustBeEqualTo1WhenOneDoorConfigIsDisabled => new(Messages.DoorMessages.ReaderMustBeEqualTo1WhenOneDoorConfigIsDisabled);
        public static AppException RexMustBeNotBeEqualTo1WhenOneDoorConfigIsDisabled => new(Messages.DoorMessages.RexMustBeNotBeEqualTo1WhenOneDoorConfigIsDisabled);
    }

    public static class ReaderExceptions
    {
        public static AppException LEDTypeMustBeDefinedWhenProcotolIsWiegand => new(Messages.ReaderMessages.LedTypeMustNotBeNull);
    }
}
