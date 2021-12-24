using DomainBase;

namespace Identity.Domain
{
    public class AddressEntity:IValueObject
    {
        public AddressEntity()
        {
            
        }
        
        /// <summary>
        /// 省份
        /// </summary>
        private string Province { get; }

        /// <summary>
        /// 城市
        /// </summary>
        private string City { get; }

        /// <summary>
        /// 区县
        /// </summary>
        private string County { get; }

        /// <summary>
        /// 街道
        /// </summary>
        private string Street { get; }
        
        
        public AddressEntity(string province, string city,
            string county, string street)
        {
            this.Province = province;
            this.City = city;
            this.County = county;
            this.Street = street;
        }

    }
}