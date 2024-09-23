namespace Season.DesarrolloBVF.Domain.Entities
{
    public class CatProductos
    {
        public int Tipo { get; set; }
        public int Producto { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionIngles { get; set; }
        //public int Consecutivo { get; set; }
        //public string CodCaades { get; set; }
        //public int CtaEmb { get; set; }
        //public string SubCtaGral { get; set; }
        //public int ConsecClasif { get; set; }
        //public string Temperatura { get; set; }
        //public string NomBotanico { get; set; }

        //public string? CodTrazabilidad { get; set; }
        public bool Organico { get; set; }
        //public bool? Pediddo { get; set; }
        public string? Abreviacion { get; set; }
        //public bool Propia { get; set; }
        //public bool? Contrato { get; set; }
        //public string? Grupo { get; set; }
        public string? NombreInterno { get; set; }
    }
}
