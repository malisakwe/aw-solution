using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class BaseEntity
    {
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
