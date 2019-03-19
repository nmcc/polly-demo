using System.IO;

namespace PollyDemo.Server.Avatar
{
    public class AssetAvatarProvider : IAvatarProvider
    {
        public Stream GetAvatarForUsername(string username)
        {
            var filename = Path.Combine("assets", BuildFilename(username) + ".jpg");
            if (File.Exists(filename))
            {
                return File.OpenRead(filename);
            }

            return Stream.Null;
        }

        private static string BuildFilename(string filename) => filename.Replace(" ", string.Empty);
    }
}
