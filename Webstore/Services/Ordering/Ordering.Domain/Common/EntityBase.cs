using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
// DDD - domain driven design
// treba nam da nase klase nemaju javne metode koje nisu nuzno neophodne
// ne treba nam javni set; Id-a
// ? kod LastModifiedDate jer ne mora da postoji