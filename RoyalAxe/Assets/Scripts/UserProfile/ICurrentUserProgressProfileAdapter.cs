using System.Collections.Generic;
using System.Linq;

namespace Core.UserProfile
{
    public interface IUserProfileProgressRoot
    {
        void AddPartProgress(IUserProgressProfile userProgressProfile);
    }

    public interface ICurrentUserProgressProfileFacade
    {
        string ProfileName { get; }
        T Get<T>();
    }
    
    internal interface IUserLevelsProgress : IUserProgressProfile
    {
        LastLevel LastLevel { get; }
    }

    public interface IUserProgressProfile
    {
      //  void Save();
    }



    public class CurrentUserProgressProfileFacade : ICurrentUserProgressProfileFacade,IUserProfileProgressRoot
    {
        public string ProfileName { get;  }

        private HashSet<IUserProgressProfile> _progressProfiles = new HashSet<IUserProgressProfile>();

        public CurrentUserProgressProfileFacade(string profileName)
        {
            ProfileName = profileName;
        }

        public T Get<T>()
        {
            var result = _progressProfiles.FirstOrDefault(o => o is T);
            return result == default ? default : (T) result;
        }

        public void AddPartProgress(IUserProgressProfile userProgressProfile)
        {
            _progressProfiles.Add(userProgressProfile);
        }
    }
}
