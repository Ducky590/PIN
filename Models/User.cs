namespace PIN_izračun.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal SalaryBruto { get; set; }
        public string City { get; set; }
        public string TaxReduction { get; set; }
        public bool Stup { get; set; }
        public decimal IznosNeto { get; set; }

        
        public decimal GradPrirez { get; set; }

        public decimal OlakšicaDjeca { get; set; }

        public decimal PrviStup { get; set; }
        public decimal DrugiStup { get; set; }

        public decimal Osnovica { get; set; }

        public decimal Porez { get; set; }

        public decimal Prirez { get; set; }

        public decimal Neto { get; set; }
    }

    public enum City
    {
        Zagreb = 1,
        VelikaGorica = 2,
        SVNedelja = 3
    }

    public enum TaxtReduction
    {
        Nijedno = 1,
        Jedno = 2,
        Dvoje = 3,
        Troje = 4
    }
}
