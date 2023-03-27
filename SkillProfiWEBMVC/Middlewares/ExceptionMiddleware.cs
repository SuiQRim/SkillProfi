using Microsoft.AspNetCore.Authentication;
using NuGet.Protocol;
using SkillProfiRequestsToAPI.Exceptions;

namespace SkillProfiWEBMVC.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next) => _next = next;

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (SkillProfiUnauthorizedException)
			{

				await context.SignOutAsync();
				await context.ChallengeAsync(new AuthenticationProperties
				{
					RedirectUri = context.Request.Path,
				});
			}
		}
	}
}
