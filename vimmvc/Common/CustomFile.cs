namespace vimmvc.Common
{
    public class CustomFile
    {
        public CustomFile()
        {
            this.Content = (Stream) new MemoryStream();
        }

        public string Name { get; set; }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }

        public string Extension
        {
            get
            {
                return Path.GetExtension(this.Name);
            }
        }
    }
}
