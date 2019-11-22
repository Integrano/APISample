using CoreAPISample.API.Models;
using CoreAPISample.Core.Models;
using System.Net.Http;

namespace CoreAPISample.API.Helpers
{
    public interface IHttpClientProvider
    {
        /// <summary>
        /// Post an login details to authentication api.
        /// </summary>
        /// <param name="model">Object of Authenticate model to be posted.</param>
        /// <returns>Http Response</returns>
        HttpResponseMessage PostAsJsonAsync(AuthenticateModel model);

        /// <summary>
        /// Post delivery details to api.
        /// </summary>
        /// <param name="model">Object of Manufacturer ViewModel to be posted.</param>
        /// <returns>Http Response</returns>
        HttpResponseMessage PostDeliveryAsJsonAsync(ManufacturerViewModel model);

        /// <summary>
        /// Post delivery details to api.
        /// </summary>
        /// <param name="model">Object of Delivery ViewModel to be posted.</param>
        /// <returns>Http Response</returns>
        HttpResponseMessage PostDeliveryDetailsAsJsonAsync(DeliveryViewModel model);
    }
}
