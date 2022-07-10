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
        /// <returns>The new address id</returns>
        public int AddNewAddress(AddressInput address, int userID);

        /// <summary>
        /// Updates and existing address
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="userID">The user id</param>
        /// <param name="addressID">The address id</param>
        /// <returns>If the update was successful</returns>
        public bool UpdateAddress(AddressInput address, int userID, int addressID);
    }
}
