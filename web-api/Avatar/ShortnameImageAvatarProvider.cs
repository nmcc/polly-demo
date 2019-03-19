using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PollyDemo.Server.Avatar
{
    public class ShortnameImageAvatarProvider : IAvatarProvider
    {
        private const float emSize = 150;
        private readonly (int X, int Y) imageSize = (400, 400);
        private readonly Font font = new Font(FontFamily.GenericSansSerif, emSize, FontStyle.Regular);
        private readonly Color BrushColor = ColorTranslator.FromHtml("#5e2b97");
        
        public Stream GetAvatarForUsername(string username)
        {
            using (var bitmap = new Bitmap(imageSize.X, imageSize.Y))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (Brush b = new SolidBrush(BrushColor))
            {
                g.Clear(Color.White);
                g.FillEllipse(b, 0, 0, imageSize.X - 1, imageSize.Y - 1);

                var shortUsername = GetShortUsername(username);
                g.DrawString(shortUsername, font, new SolidBrush(Color.White), x: 30, y: 100);

                var memStream = new MemoryStream();
                bitmap.Save(memStream, ImageFormat.Png);

                // Reset to start of stream 
                memStream.Position = 0;

                return memStream;
            }
        }

        private string GetShortUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "??";

            var words = username.Split(" ");

            if (words.Length == 1)
            {
                return username.Substring(0, 2).ToUpper();
            }

            return string.Concat(words[0].Substring(0, 1), words[words.Length - 1].Substring(0, 1))
                .ToUpper();
        }
    }
}
