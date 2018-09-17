using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Dym = Revit.Elements;

namespace hsbCustomizableRoofs
{
    public class hsbCustomizableRoofByFootPrint
    {

        private static double ConvertToDegrees(float dDegrees)
        {
            double dRadians = dDegrees * Math.PI / 180.0;

            return dRadians;
        }

        public static bool SetRoofByFootPrintSlope(Dym.Roof Roof, bool DefinesSlope, double SlopeAngle)
        {
            if (Roof == null)
                return false;

            FootPrintRoof footPrintRoof = Roof.InternalElement as FootPrintRoof;

            if (footPrintRoof == null) return false;

            ModelCurveArrArray modelCurveArrArray = footPrintRoof.GetProfiles();

            if (modelCurveArrArray == null || modelCurveArrArray.Size == 0) return false;

            ModelCurveArray modelCurveArray = modelCurveArrArray.get_Item(0);

            if (modelCurveArray == null || modelCurveArray.Size == 0) return false;

            foreach (ModelCurve modelCurve in modelCurveArray)
            {
                double dSlopeAngleRadians = ConvertToDegrees(SlopeAngle);

                footPrintRoof.set_DefinesSlope(modelCurve, DefinesSlope);

                footPrintRoof.set_SlopeAngle(modelCurve, dSlopeAngleRadians);
            }

            return true;

        }
    }
}
