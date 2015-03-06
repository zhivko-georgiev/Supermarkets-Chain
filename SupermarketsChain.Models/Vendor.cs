namespace SupermarketsChain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vendor
    {
        private ICollection<Product> products;

        public Vendor()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }
    }
}
