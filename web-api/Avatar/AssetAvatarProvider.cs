using System.IO;

namespace PollyDemo.Server.Avatar
{
    public class AssetAvatarProvider : IAvatarProvider
    {
        public Stream GetAvatarForUsername(string username)
        {
            var jpgFile = Path.Combine("assets", BuildFilename(username) + ".jpg");
            if (File.Exists(jpgFile))
            {
                return File.OpenRead(jpgFile);
            }

            var pngFile = Path.Combine("assets", BuildFilename(username) + ".png");
            if (File.Exists(pngFile))
            {
                return File.OpenRead(pngFile);
            }
            
            return Stream.Null;
        }

        private static string BuildFilename(string filename) => filename.Replace(" ", string.Empty);
    }
}
