using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using KSH.Okta.ApiClient.TokenAuth.Models;
using Newtonsoft.Json;

namespace KSH.Okta.ApiClient.TokenAuth.Services
{
    // todo: handle http 429 for org-wide rate limit exceeded - may need to retry last update   https://developer.okta.com/docs/api/getting_started/rate-limits
    // todo: need to handle failed updates and gets without failing out application and track for later retry
    public class OktaClient : HttpClient
    {
        public OktaClient()
        {
            BaseAddress = OktaSettings.BaseUri;
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(OktaSettings.MediaTypeJson));

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OktaSettings.AuthenticationScheme, OktaSettings.Token);
        }

        /// <summary>
        /// Get an okta user based  
        /// </summary>
        /// <see>https://developer.okta.com/docs/api/resources/users#get-user</see>
        /// <loginId name="loginId">User.Id, User.Profile.Login, or shortname of User.Profile.Login</loginId>
        /// <returns>Task with User</returns>
        public async Task<User> GetUserAsync(string loginId = "")
        {
            var user = new User();
            var response = await GetAsync(OktaPaths.Users + "/" + loginId);
            if (response.IsSuccessStatusCode) user = await response.Content.ReadAsAsync<User>();
            return user;
        }

        /// <summary>
        /// Get an okta user  
        /// </summary>
        /// <see>https://developer.okta.com/docs/api/resources/users#list-users</see>
        /// <returns>Task with List of Users</returns>
        public async Task<List<User>> GetUsersAsync()
        {
            var users = new List<User>();
            var response = await GetAsync(OktaPaths.Users);
            if (response.IsSuccessStatusCode) users = await response.Content.ReadAsAsync<List<User>>();
            return users;
        }

        /// <summary>
        /// Update partial user profile (SHOULD NOT USE 'PUT')
        /// </summary>
        ///<see>https://developer.okta.com/docs/api/resources/users#update-profile</see>
        /// <loginId name="fullLoginEmail">Must use full email to make sure we match uniquely.</loginId>
        /// <returns></returns>
        public async Task<User> UpdateUserProfileAsync(User user)
        {
            var response = await PostAsync(OktaPaths.Users + "/" + user.Id, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, OktaSettings.MediaTypeJson));
            response.EnsureSuccessStatusCode();

            var updatedUser = await response.Content.ReadAsAsync<User>();

            return updatedUser;
        }
    }
}