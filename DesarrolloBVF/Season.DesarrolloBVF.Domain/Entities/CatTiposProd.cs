using System.ComponentModel.DataAnnotations;

namespace Season.DesarrolloBVF.Domain.Entities
{
    public class CatTiposProd
    {
        [Key]
        public int Tipo { get; set; }
        public string Descripcion { get; set; }
        public string? DescIngles { get; set; }
        //public int HPreEnfria { get; set; }
        //public int MPreEnfria { get; set; }
        //public int Consecutivo {  get; set; }
        //public bool SeparaCto { get; set; }
        //public string DescripcionAd { get; set; }
        //public string? FraccionArancelaria { get; set; }
        public string? CodigoSAT { get; set; }
        //public  string? Cod_UniMed {  get; set; }
        //public string? Cod_UnimedProd {  get; set; }
        //public string? CodigoSATProd {  get; set; }
    }
}
