namespace DesarrolloVariedadesApp.Security
{
    public static class PermisosDesarrolloVariedad
    {
        private static int CodigoModulo => 38;

        public static string GeneticaUsuario => $"{CodigoModulo}-FRMDESVARIEDADGENETICAUSER";

        public static string Procesamiento => $"{CodigoModulo}-FRMDESVARIEDADPROCESAMIENTO";
        public static string Catalogos => $"{CodigoModulo}-FRMDESVARIEDADCATALOGOS";
        public static string Gestion => $"{CodigoModulo}-FRMDESVARIEDADGESTION";
        public static string Configuracion => $"{CodigoModulo}-FRMDESVARIEDADCONFIG";
        public static string Reportes => $"{CodigoModulo}-FRMDESVARIEDADREPORTES";

        public static string Origen => $"{CodigoModulo}-FRMDESVARIEDADORIGEN";
        public static string OrigenNuevo => $"{CodigoModulo}-FRMDESVARIEDADORIGENNUEVO";
        public static string OrigenUpdate => $"{CodigoModulo}-FRMDESVARIEDADORIGENUPDATE";
        public static string OrigenDelete => $"{CodigoModulo}-FRMDESVARIEDADORIGENDELETE";

        public static string Nodo => $"{CodigoModulo}-FRMDESVARIEDADNODO";
        public static string NodoNuevo => $"{CodigoModulo}-FRMDESVARIEDADNODONUEVO";
        public static string NodoUpdate => $"{CodigoModulo}-FRMDESVARIEDADNODOUPDATE";

        public static string Invitro => $"{CodigoModulo}-FRMDESVARIEDADINVITRO";
        public static string InvitroNuevo => $"{CodigoModulo}-FRMDESVARIEDADINVITRONUEVO";
        public static string InvitroUpdate => $"{CodigoModulo}-FRMDESVARIEDADINVITROUPDTAE";

        public static string CatalogoNuevo => $"{CodigoModulo}-FRMDESVARIEDADCATNUEVO";
        public static string CatalogoUpdate => $"{CodigoModulo}-FRMDESVARIEDADCATUPDATE";

        public static string Imagenes => $"{CodigoModulo}-FRMDESVARIEDADIMAGENES";
        public static string ImagenesEditar => $"{CodigoModulo}-FRMDESVARIEDADIMAGENESEDIT";
    }
}
