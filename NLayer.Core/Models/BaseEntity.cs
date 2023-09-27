namespace NLayer.Core.Models
{
    public abstract class BaseEntity
    {
        // Bu entityden bir nesne örneği alınmaması için abstract class yapıyoruz. Çünkü bu entity bir base yapıdır.
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
