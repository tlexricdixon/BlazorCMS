using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsModels
{
    public class Tag : SyncEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public List<PostTag> PostTags { get; set; } = new();
    }
}
