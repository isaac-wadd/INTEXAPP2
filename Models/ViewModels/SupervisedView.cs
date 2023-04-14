using Microsoft.Build.Framework;

namespace INTEXAPP2.Models.ViewModels
{
    public class SupervisedView
    {
        [Required]
        public string head { get; set; }
        [Required]
        public string face { get; set; }
        [Required]
        public string sex { get; set; }
        [Required]
        public string eastwest { get; set; }
        [Required]
        public string age { get; set; }
        [Required]
        public float? depth { get; set; }
        [Required]
        public float? length { get; set; }
        [Required]
        public float? squarenorthsouth { get; set; }
        [Required]
        public float? squareeastwest { get; set; }
    }
}