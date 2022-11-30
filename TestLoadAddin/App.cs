using Autodesk.Revit.UI;

namespace TestLoadAddin
{
    internal class App : IExternalApplication
    {
        private const string nameApp = "App1";
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

            var a = new AssemblyManager();

            return Result.Succeeded;
        }
    }
}
