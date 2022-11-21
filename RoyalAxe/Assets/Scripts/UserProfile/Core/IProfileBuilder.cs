#region

using System.Threading.Tasks;

#endregion

namespace Core.UserProfile
{
    public interface IUserProfilePartBuilder<TDataWrapper>
    {
        void SaveProgress(IProfileProgressStorageContext context,TDataWrapper saveobject);
        void LoadTo(IProfileProgressStorageContext context, TDataWrapper result);
    }


}