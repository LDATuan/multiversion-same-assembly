using Autodesk.Revit.UI;
using System.IO;
using System.Reflection;

namespace TestLoadAddin2
{
    internal class App : IExternalApplication
    {
        private const string nameApp = "App2";
        public static string pathFileDll = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab(nameApp);

            Autodesk.Revit.UI.RibbonPanel panel = application.CreateRibbonPanel(nameApp, "ABOUT");
            PushButtonData pbd = new PushButtonData("Test", "Version ", pathFileDll, nameof(Command))
            {
                ToolTip = "",
                LongDescription = "",
            };
            panel.AddItem(pbd);
            var ass = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MyLibrary.dll"); 

            var a = new AssemblyManager();

            return Result.Succeeded;
        }
    }
}
