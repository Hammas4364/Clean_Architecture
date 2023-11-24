using SharedKernel.Constants;
namespace SharedKernel.Exceptions;

internal static partial class AppExceptions
{
    internal static class EventsExceptions
    {
        internal static AppException EventCantBeAddedInWhenMethod => new(Messages.EventsMessages.EventCantBeAddedInWhenMethod);
    }

    internal static class ControllerExceptions
    {
        internal static AppException Door1AlreadyExist => new(ValidationExceptions.GetValidationErrors(".Already.Exist", "Door1"), 400);
        internal static AppException DoorsMustBeNotBeGreaterThan2 => new(Messages.ControllerMessages.DoorsMustBeNotBeGreaterThan2);

        internal static AppException CannotAddDoorMoreThan1WhenOneDoorConfigIsEnabled => new(Messages.ControllerMessages.CannotAddDoorMoreThan1WhenOneDoorConfigIsEnabled);
    }

    internal static class DoorExceptions
    {
        internal static AppException ReadersMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled => new(Messages.DoorMessages.ReadersMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled);
        internal static AppException RexMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled => new(Messages.DoorMessages.RexMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled);

        internal static AppException ReaderMustBeEqualTo1WhenOneDoorConfigIsDisabled => new(Messages.DoorMessages.ReaderMustBeEqualTo1WhenOneDoorConfigIsDisabled);
        internal static AppException RexMustBeNotBeEqualTo1WhenOneDoorConfigIsDisabled => new(Messages.DoorMessages.RexMustBeNotBeEqualTo1WhenOneDoorConfigIsDisabled);
    }

    internal static class ReaderExceptions
    {
        internal static AppException LEDTypeMustBeDefinedWhenProcotolIsWiegand => new(Messages.ReaderMessages.LedTypeMustNotBeNull);
    }
}
