using NannyModels.Models;

namespace NannyData.Interfaces
{
    public interface IAddressDAO
    {
        public ICollection<Address> GetAddressesByUserID(int id);
    }
}
