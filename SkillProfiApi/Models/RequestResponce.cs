namespace SkillProfiApi.Models
{
	public class RequestResponce
	{
		public RequestResponce(byte statusCode, string message, object? data = null)
		{
			StatusCode = statusCode;
			Message = message;
			Data = data;
		}

		public byte StatusCode { get; set; }

		public string Message { get; set; } = string.Empty;

		public object? Data { get; set; }
	}
}
