using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElderlyCareApp.Utils
{
    public abstract class ApplicationConfigBase
    {
        public abstract void Save(Stream stream);

        public abstract Task SaveAsync(Stream stream, CancellationToken token = default);
        public abstract void Read(Stream stream);
        public abstract Task ReadAsync(Stream stream, CancellationToken token = default);
        public abstract void New();
    }
}
