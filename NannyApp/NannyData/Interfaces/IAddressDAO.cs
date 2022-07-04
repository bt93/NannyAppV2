using NannyModels.Models;

namespace NannyData.Interfaces
{
    public interface IAddressDAO
    {
        /// <summary>
        /// Gets a collection of Addresses by the users ID
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>Collection of Addresses</returns>
        public ICollection<Address> GetAddressesByUserID(int id);
    }
}
