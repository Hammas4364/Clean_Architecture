﻿namespace SharedKernel.Constants;
public static class Messages
{
    public static class EventsMessages
    {
        public static string EventCantBeAddedInWhenMethod = "Event Can't be added in When Method";
    }

    public static class ControllerMessages
    {
        public const string CannotAddDoorMoreThan1WhenOneDoorConfigIsEnabled = "Can't Add Door More Than One When OneDoorConfig is Enabled";
        public const string DoorsMustBeNotBeGreaterThan2 = "The Length of Door Collection Must not be Greater than 2.";
    }

    public static class DoorMessages
    {
        public const string ReadersMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled = "The Length of Reader Collection Must not be Greater than 2 or Less than 1 when One Door Config is Enabled.";
        public const string RexMustBeNotBeGreaterThan2OrLessThan1WhenDoorConfigIsEnabled = "The Length of Rex Collection Must not be Greater than 2 or Less than 1 when One Door Config is Enabled.";

        public const string ReaderMustBeEqualTo1WhenOneDoorConfigIsDisabled = "The Length of Reader Collection Must be 1 when OneDoorConfig is Disabled.";
        public const string RexMustBeNotBeEqualTo1WhenOneDoorConfigIsDisabled = "The Length of Rex Collection Must be 1 when OneDoorConfig is Disabled.";
    }

    public static class ReaderMessages
    {
        public const string LedTypeMustNotBeNull = "LED Type Must be defined when Protocol is Wiegand";
    }

    public static string NullOrWhitespaceValidationException(string propertyName) => $"{propertyName} Can't be Empty, Null or Whitespace";
    public static string MaxLengthValidationException(string propertyName, int maxLength) => $"The Length of {propertyName} can't be greater than {maxLength}.";

    public static class CommonMessages
    {
        public const string DoesNotExist = ".Does.Not.Exist";
        public const string AlreadyExist = ".Already.Exist";
        public const string NameAlreadyExist = ".Name.AlreadyExist";
        public const string NotFound = ".NotFound";
    }
}
