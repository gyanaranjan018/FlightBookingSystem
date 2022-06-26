namespace Utility.Enums
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public enum Meals : short
    {
        None = 0,
        Veg = 1,
        NonVeg = 2,
        Both = 3,
    }

    public enum BookingStatus : short
    {
        Success = 0,
        Cancel = 1
    }

    public enum ScheduledDays : short
    {
        Daily = 0,
        WeekDays = 1,
        WeekEnds = 2,
        SpecificDays = 4,
    }

    public enum AirlineStatus : short
    {
        Active = 0,
        InActive = 1
    }

    public enum UserRole
    {
        User = 0,
        Admin = 1,
    }

    public enum FlightType
    {
        Oneway,
        RoundTrip
    }
}
