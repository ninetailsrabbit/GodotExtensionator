namespace Godot_XTension_Pack {
    public static class DateTimeExtension {

        /// <summary>
        /// Adds a specified number of weeks to a DateTime object.
        /// </summary>
        /// <param name="date">The DateTime to which weeks will be added.</param>
        /// <param name="numberOfWeeks">The number of weeks to add (positive or negative).</param>
        /// <returns>A new DateTime object representing the date after adding the specified number of weeks.</returns>
        /// <remarks>
        /// This method leverages the built-in `AddDays` method to efficiently add weeks to the date.
        /// It calculates the total number of days by multiplying the number of weeks by 7 and then adds those days to the original date.
        /// </remarks>
        public static DateTime AddWeeks(this DateTime date, int numberOfWeeks) => date.AddDays(numberOfWeeks * 7);

        /// <summary>
        /// Calculates the age in years based on a given date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth for which to calculate age.</param>
        /// <returns>The age in years (considering current year and potential leap year).</returns>
        /// <remarks>
        /// This method first calculates the difference in years between the current year (`DateTime.Today.Year`) and the birth year (`dateOfBirth.Year`).
        /// It then checks if the date of birth falls after the beginning of the current year minus the calculated age.
        /// This is necessary to account for leap years and ensure accurate age calculation.
        /// If the date of birth is indeed later, one year is subtracted from the calculated age to reflect the incomplete year.
        /// Finally, the age in years is returned.
        /// </remarks>
        public static int Age(this DateTime dateOfBirth) {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        /// Converts a DateTime object to a new DateTime in a specified time zone.
        /// </summary>
        /// <param name="date">The DateTime object to convert.</param>
        /// <param name="timeZone">The target time zone (defaults to UTC if null).</param>
        /// <returns>A new DateTime object representing the date in the specified time zone.</returns>
        /// <remarks>
        /// This method utilizes the `TimeZoneInfo.ConvertTime` function to perform the time zone conversion.
        /// If no time zone is provided (`timeZone` is null), it defaults to UTC for consistency.
        /// </remarks>
        public static DateTime To(this DateTime date, TimeZoneInfo timeZone) {
            timeZone ??= TimeZoneInfo.Utc;

            return TimeZoneInfo.ConvertTime(date, timeZone);
        }

        /// <summary>
        /// Converts a DateTime object to the number of seconds since a specific epoch (default: 1970-01-01 UTC).
        /// </summary>
        /// <param name="date">The DateTime object to convert.</param>
        /// <param name="epoch">The epoch (starting point) for the time measurement (defaults to 1970-01-01 UTC).</param>
        /// <returns>The number of seconds elapsed since the epoch represented by the DateTime object.</returns>
        /// <remarks>
        /// This method first converts the DateTime object to UTC using `ToUniversalTime`.
        /// It then calculates the difference in ticks (smallest time unit) between the converted date and the provided epoch (defaulting to 1970-01-01 UTC).
        /// Finally, it divides the difference in ticks by the number of ticks per second (`TimeSpan.TicksPerSecond`) to obtain the elapsed time in seconds.
        /// </remarks>
        public static int To(this DateTime date, DateTime epoch = default) {
            if (epoch == default)
                epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (int)((date.ToUniversalTime() - epoch).Ticks / TimeSpan.TicksPerSecond);
        }


        /// <summary>
        /// Converts an integer representing the number of seconds since an epoch (default: 1970-01-01 UTC) to a DateTime object.
        /// </summary>
        /// <param name="date">The integer representing the number of seconds since the epoch.</param>
        /// <param name="epoch">The epoch (starting point) for the time measurement (defaults to 1970-01-01 UTC).</param>
        /// <returns>A new DateTime object representing the date based on the specified epoch and number of seconds.</returns>
        /// <remarks>
        /// This method first multiplies the provided integer (`date`) by the number of ticks per second (`TimeSpan.TicksPerSecond`) to convert it to ticks.
        /// It then adds this calculated tick value to the ticks of the provided epoch (defaulting to 1970-01-01 UTC).
        /// Finally, it constructs a new DateTime object using the resulting total ticks and sets its `DateTimeKind` to UTC.
        /// </remarks>
        public static DateTime To(this int date, DateTime epoch = default) {
            if (epoch == default)
                epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return new DateTime((date * TimeSpan.TicksPerSecond) + epoch.Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Calculates the UTC offset of a DateTime object in hours.
        /// </summary>
        /// <param name="date">The DateTime object for which to calculate the UTC offset.</param>
        /// <returns>The UTC offset of the DateTime object in hours (positive for ahead of UTC, negative for behind).</returns>
        /// <remarks>
        /// This method subtracts the UTC version of the DateTime object (`date.ToUniversalTime`) from the original DateTime (`date`).
        /// The difference represents the time zone offset of the original DateTime.
        /// Finally, it converts the difference in ticks (`TotalTicks`) to hours and returns the result.
        /// </remarks>
        public static double UTCOffset(this DateTime date) => (date - date.ToUniversalTime()).TotalHours;
    }
}
