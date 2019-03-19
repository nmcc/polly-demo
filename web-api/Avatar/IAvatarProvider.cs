using System.IO;

namespace PollyDemo.Server.Avatar
{
    public interface IAvatarProvider
    {
        Stream GetAvatarForUsername(string username);
    }
}
