using System.Web.Http;

namespace KSH.Okta.ApiClient.OpenId.Controllers
{
    public class OktaUserController : ApiController
    {
        [Authorize]
        public IHttpActionResult Protected()
        {
            return Ok();
        }
    }
}
