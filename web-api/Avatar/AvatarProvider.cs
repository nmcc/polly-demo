using System;
using System.IO;

namespace PollyDemo.Server.Avatar
{
    public class AvatarProvider : IAvatarProvider
    {
        private readonly AssetAvatarProvider assetAvatarProvider;
        private readonly ShortnameImageAvatarProvider shortnameImageAvatarProvider;

        public AvatarProvider(AssetAvatarProvider assetAvatarProvider, ShortnameImageAvatarProvider shortnameImageAvatarProvider)
        {
            this.assetAvatarProvider = assetAvatarProvider ?? throw new ArgumentNullException(nameof(assetAvatarProvider));
            this.shortnameImageAvatarProvider = shortnameImageAvatarProvider ?? throw new ArgumentNullException(nameof(shortnameImageAvatarProvider));
        }

        public Stream GetAvatarForUsername(string username)
        {
            var avatar = assetAvatarProvider.GetAvatarForUsername(username);

            if (avatar.Length > 0)
                return avatar;

            return shortnameImageAvatarProvider.GetAvatarForUsername(username);
        }
    }
}
