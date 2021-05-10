using System.Collections.Generic;

namespace Application.Domain.Entities
{
    public class Location : Entity
    {
        public string LocationName { get; set; }
        public string Acronym { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
