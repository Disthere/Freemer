using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freemer.Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string OrderType { get; set; } // Отдельная таблица с типами заказов
        public string OtherInfo { get; set; }
    }
}
