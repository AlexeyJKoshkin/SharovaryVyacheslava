#region

using System.Threading.Tasks;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfilePartBuilder<TDataWrapper>
    {
        void SaveTo(string folderInfoFullName, TDataWrapper saveobject);
        Task LoadFrom(string folderInfoFullName, TDataWrapper result);
    }
}