using SkillProfi;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class SocialNetworksRequests : RequestController
    {
        public SocialNetworksRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts/SocialNetworks") { }

        public List<SocialNetwork> GetList() =>
            Request.Get<List<SocialNetwork>>(Url);

        public async Task<List<SocialNetwork>> GetListAsync() =>
            await Request.GetAsync<List<SocialNetwork>>(Url);



        public string Add(SocialNetwork socialNetwork, Stream stream, string accessToken)
        {
            using (stream)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                var obj = new ObjectWithImage<SocialNetwork>()
                {
                    Object = socialNetwork,
                    Picture = buffer,
                };

                return Request.Add(obj, Url, accessToken);
            }
           
        }


        public async Task<string> AddAsync(SocialNetwork socialNetwork, Stream stream, string accessToken)
        {
            using (stream)
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                var obj = new ObjectWithImage<SocialNetwork>()
                {
                    Object = socialNetwork,
                    Picture = buffer,
                };
                return await Request.AddAsync(obj, Url, accessToken);
            }
        }




        public string Edit(string id, SocialNetwork socialNetwork, Stream stream, string accessToken)
        {
            using (stream)
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                var obj = new ObjectWithImage<SocialNetwork>()
                {
                    Object = socialNetwork,
                    Picture = buffer,
                };
                return Request.Edit(obj, Url, id, accessToken);
            }
        }


        public async Task<string> EditAsync(string id, SocialNetwork socialNetwork, Stream stream, string accessToken)
        {

            using (stream)
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                var obj = new ObjectWithImage<SocialNetwork>()
                {
                    Object = socialNetwork,
                    Picture = buffer,
                };
                return await Request.EditAsync(obj, Url, id, accessToken);
            }
        }



        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);
    }
}
