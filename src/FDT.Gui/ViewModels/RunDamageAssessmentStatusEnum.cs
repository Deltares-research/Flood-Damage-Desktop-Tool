using System.ComponentModel.DataAnnotations;

namespace FDT.Gui.ViewModels
{
    public enum RunDamageAssessmentStatusEnum
    {
        [Display(Name = "Run Damage Assessment")]
        Ready,
        [Display(Name = "Running")]
        Running,
        [Display(Name = "No Basins")]
        FailedLoadingBasins,
    }
}