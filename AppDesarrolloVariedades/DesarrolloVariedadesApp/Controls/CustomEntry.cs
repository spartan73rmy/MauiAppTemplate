
using Microsoft.Maui.Handlers;

namespace DesarrolloVariedadesApp.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {

#if ANDROID
            Margin = new Thickness(10, 0);
#endif
#if WINDOWS
            Background = null;
            BackgroundColor = Colors.Transparent;
#endif
        }
    }
}
