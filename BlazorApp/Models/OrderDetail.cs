using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP;
public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public Guid OrderDetailIdGuid {  get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }


    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
