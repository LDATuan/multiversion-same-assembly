using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


[Transaction(TransactionMode.Manual)]
public class Command : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        //MyLibrary.Class1.Show("12");

        MyLibrary.Class1.Show("12", "app 2");

        return Result.Succeeded;
    }
}