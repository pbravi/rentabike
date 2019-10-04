using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.model
{
    public class RentalType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComposite { get; set; }
    }
}
