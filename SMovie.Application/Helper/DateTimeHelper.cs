namespace SMovie.Application.Helper
{
	public class DateTimeHelper
	{
		public static DateTime GetDateTimeNow()
		{
			// Lấy múi giờ hiện tại của server
			DateTime serverTime = DateTime.UtcNow;

			// Tìm thông tin múi giờ của Việt Nam
			TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			// Chuyển múi giờ của server sang múi giờ của Việt Nam
			DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(serverTime, vietnamTimeZone);

			return vietnamTime;
		}

		public static string GetDateNow()
		{
			// Lấy múi giờ hiện tại của server
			DateTime serverTime = DateTime.UtcNow;

			// Tìm thông tin múi giờ của Việt Nam
			TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

			// Chuyển múi giờ của server sang múi giờ của Việt Nam
			DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(serverTime, vietnamTimeZone);

			return vietnamTime.ToString("dd/MM/yyyy");
		}
	}
}
