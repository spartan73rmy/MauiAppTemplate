using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.Messages
{
    public class AgregarFotosMessage
    {
        public ObservableCollection<DesVarImagenesApp> listaImagenes { get; set; }

        public TiposConsula Tipo { get; set; }

        public AgregarFotosMessage(ObservableCollection<DesVarImagenesApp> lista, TiposConsula tipo)
        {
            listaImagenes = lista;
            Tipo = tipo;
        }
    }
}