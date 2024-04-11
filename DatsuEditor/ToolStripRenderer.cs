using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsuEditor
{
    internal class DatsuDarkRenderer : ToolStripProfessionalRenderer
    {
        public DatsuDarkRenderer(ProfessionalColorTable table) : base(table)
        {
                
        }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = ThemeDefines.DarkMode_Foreground;
            base.OnRenderItemText(e);
        }
    }
    internal class DatsuDarkColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => ThemeDefines.DarkMode_Background;
        public override Color ImageMarginGradientBegin => ThemeDefines.DarkMode_Background;
        public override Color ImageMarginGradientMiddle => ThemeDefines.DarkMode_Background;
        public override Color ImageMarginGradientEnd => ThemeDefines.DarkMode_Background;
        public override Color MenuItemSelected => ThemeDefines.DarkMode_Background;
        public override Color MenuItemSelectedGradientBegin => ThemeDefines.DarkMode_Background;
        public override Color MenuItemSelectedGradientEnd => ThemeDefines.DarkMode_Background;
        public override Color MenuItemPressedGradientBegin => ThemeDefines.DarkMode_Background;
        public override Color MenuItemPressedGradientEnd => ThemeDefines.DarkMode_Background;
    }
}
