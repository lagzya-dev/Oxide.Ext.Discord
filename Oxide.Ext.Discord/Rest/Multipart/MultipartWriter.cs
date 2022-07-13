using System.IO;
using System.Text;
using Oxide.Ext.Discord.Pooling;

namespace Oxide.Ext.Discord.Rest.Multipart
{
    internal class MultipartWriter : BasePoolable
    {
        private MemoryStream _stream;
        private byte[] _buffer = new byte[1024];
        private string _boundary;
        private static readonly byte[] NewLine = Encoding.UTF8.GetBytes("\r\n");
        private static readonly byte[] Separator = Encoding.UTF8.GetBytes("--");

        public void Init(string boundary)
        {
            _boundary = boundary;
        }
        
        public void Write(byte[] bytes)
        {
            _stream.Write(bytes, 0, bytes.Length);   
        }
        
        public void Write(string value)
        {
            EnsureBufferSize(value);
            int length = Encoding.UTF8.GetBytes(value, 0, value.Length, _buffer, 0);
            _stream.Write(_buffer, 0, length);
        }
        
        public void WriteLine()
        {
            _stream.Write(NewLine, 0, NewLine.Length);  
        }
        
        public void WriteSeparator()
        {
            _stream.Write(Separator, 0, Separator.Length);  
        }
        
        public void Write(IMultipartSection section)
        {
            WriteLine();
            WriteSeparator();
            Write(_boundary);
            WriteLine();
            
            Write("Content-Disposition: form-data; name=\"");
            Write(section.SectionName);
            Write("\"");
            if (section.FileName != null)
            {
                Write("; filename=\"");
                Write(section.FileName);
                Write("\"");
            }

            if (!string.IsNullOrEmpty(section.ContentType))
            {
                WriteLine();
                Write("Content-Type: ");
                Write(section.ContentType);
            }

            WriteLine();
            WriteLine();
            section.WriteData(this);
        }

        public void WriteEnding()
        {
            WriteLine();
            WriteSeparator();
            Write(_boundary);
            WriteSeparator();
            WriteLine();
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        private void EnsureBufferSize(string value)
        {
            int maxLength = Encoding.UTF8.GetMaxByteCount(value.Length);
            if (_buffer.Length < maxLength)
            {
                _buffer = new byte[maxLength];
            }
        }

        protected override void EnterPool()
        {
            DiscordPool.FreeMemoryStream(ref _stream);
            _stream = null;
            _boundary = null;
        }

        protected override void LeavePool()
        {
            _stream = DiscordPool.GetMemoryStream();
        }

        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
    }
}