#region

using System.Threading.Tasks;

#endregion

namespace Core.UserProfile
{

    
    public interface IUserSaveProfileCRUDCommand<TData>
    {
        TData Create(string profileName);
        TData Read(string profileName);
        void Update(TData profile);
        void Delete(string profileName);
    }
    
    
}