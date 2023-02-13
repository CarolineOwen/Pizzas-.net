using System.ComponentModel.DataAnnotations;

namespace Pizzas.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public float Prix { get; set; }

        //pour renommer à l'affichage de la propriété sur la page web
        [Display(Name ="Végétarienne")]
        public bool Vegetarienne { get; set; }

        public string Ingredients { get; set; }
    }
}
