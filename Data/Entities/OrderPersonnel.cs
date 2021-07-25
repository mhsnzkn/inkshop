using Core.Data;
using Data.Constants;

namespace Data.Entities
{
    public class OrderPersonnel : Entity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Personnel Personnel { get; set; }
        public int PersonnelId { get; set; }
        public OrderPersonnelJob Job { get; set; }

    }
}
