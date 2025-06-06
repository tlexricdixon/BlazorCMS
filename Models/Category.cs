using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsModels
{
    public class Category : SyncEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public List<Post> Post { get; set; } = new();
    }
}
