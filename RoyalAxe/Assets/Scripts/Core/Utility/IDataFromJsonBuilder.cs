#region

using System.Threading.Tasks;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfileBuilder<in TData> where TData : new()
    {
        void SaveTo(string folderInfoFullName, TData saveobject);
        Task BuildFrom(TData result, string folderInfoFullName);
    }
}