using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    public static class ScreenConfigs
    {
        public static MenuLayoutConfig GetCharacterEditorScreenConfig()
        {
            return new MenuLayoutConfig()
            {
                ProcentFrame = 1,
                HeaderContainerHeight = 0.1,
                FootContainerHeight = 0.1,
                LeftPanelWidth = 0.25,
                RightPanelWidth = 0.25,
                ContentContainerWidth = 0.5
            };
        }
    }
}
