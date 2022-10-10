using System;
using System.Threading.Tasks;

namespace Core.Configs
{
    public interface IJsonFileLoader
    {
        Task<string> LoadText(string path);
    }
}