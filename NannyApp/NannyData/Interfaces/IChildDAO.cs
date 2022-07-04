using NannyModels.Models;

namespace NannyData.Interfaces
{
    public interface IChildDAO
    {
        public ICollection<Child> GetChildByUserID(int userID);
    }
}
