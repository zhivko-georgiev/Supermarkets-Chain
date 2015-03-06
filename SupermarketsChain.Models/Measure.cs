namespace SupermarketsChain.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Measure
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
