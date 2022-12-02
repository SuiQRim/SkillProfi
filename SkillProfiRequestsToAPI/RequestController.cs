using SkillProfi;
using System;
using System.IO;

namespace SkillProfiRequestsToAPI
{
    public class RequestController
    {
        public RequestController(Func<string> getBaseUrl, string originalUrl)
        {
            _originalUrl = originalUrl;
            GetBaseUrl = getBaseUrl;
        }
        protected string Url { get => GetBaseUrl() + _originalUrl; }

        private readonly string _originalUrl;

        private readonly Func<string> GetBaseUrl;

        protected static ObjectWithPicture<T> BuildObjectWithImage<T>(T targetObject, Stream? imageStream = null)
        {
            if(imageStream == null)
                return new ObjectWithPicture<T>()
				{
					Object = targetObject
				};

			using (imageStream)
            {
				byte[] buffer = new byte[imageStream.Length];
				imageStream.Read(buffer, 0, buffer.Length);
				var obj = new ObjectWithPicture<T>()
				{
					Object = targetObject,
					Picture = buffer
				};
                return obj;
			}
		}

    }
}
