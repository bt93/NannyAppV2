using NannyModels.Models.AddressModels;

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

        /// <summary>
        /// Adds a new address
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="userID">The users id</param>
        /// <returns>The new address idl</returns>
        public int AddNewAddress(AddressInput address, int userID);
    }
}
