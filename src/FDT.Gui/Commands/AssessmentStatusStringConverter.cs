using System;
using FDT.Gui.Properties;
using FDT.Gui.ViewModels;

namespace FDT.Gui.Commands
{
    public class AssessmentStatusStringConverter: EnumToStringConverter
    {
        protected override string ConvertToString(object value)
        {
            AssessmentStatus enumVal = (AssessmentStatus)Enum.ToObject(typeof(AssessmentStatus), value);
            switch (enumVal)
            {
                case AssessmentStatus.Ready:
                    return Resources.AssessmentStatus_Ready_Run_damage_assessment;
                case AssessmentStatus.Running:
                    return Resources.AssessmentStatus_Running_Running__please_wait_;
                case AssessmentStatus.LoadingBasins:
                    return Resources.AssessmentStatus_LoadingBasins_Cannot_run__no_basins_;
                default:
                    throw new ArgumentNullException(nameof(enumVal));
            }
        }
    }
}