using System;
using System.Collections.Generic;
using System.Text;

namespace ENIDABackendAPI.Model
{
    public class Image
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxYOffset { get; set; }

        public override bool Equals(object obj)
        {
            var image = obj as Image;
            return image != null &&
                   Id == image.Id &&
                   Path == image.Path &&
                   Name == image.Name &&
                   Description == image.Description &&
                   MaxYOffset == image.MaxYOffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Path, Name, Description, MaxYOffset);
        }
    }
}
