using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Configs
{
    public class TextFileOperation : ITextFileOperation
    {
        public Task<string> LoadText(string path)
        {
            if (!File.Exists(path))
            {
                HLogger.LogError($"Not found file {path}");
                return Task.FromResult<string>(null);
            }

            string result = null;
            Thread thread = new Thread(() => { result = File.ReadAllText(path); });
            thread.Start();

            while (result == null) Task.Yield();
            return Task.FromResult(result);
        }

        public void Save(string path, string json)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            File.WriteAllText(path, json);
        }
    }
}