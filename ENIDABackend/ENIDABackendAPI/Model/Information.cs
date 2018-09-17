using System;
using System.Collections.Generic;

namespace ENIDABackendAPI.Model
{
    public class Information
    {
        public string Id { get; set; }
        public Image Image { get; set; }
        public int YOffset { get; set; }
        public string Content { get; set; }
        public InformationType Type { get; set; }

        public override bool Equals(object obj)
        {
            var information = obj as Information;
            return information != null &&
                   EqualityComparer<Image>.Default.Equals(Image, information.Image) &&
                   YOffset == information.YOffset &&
                   Content == information.Content &&
                   Type == information.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Image, YOffset, Content, Type);
        }
    }

    public enum InformationType
    {
        URL,
        Photo,
        Text
    }
}
