using System.ComponentModel.DataAnnotations;

namespace FDT.Gui.ViewModels
{
    public enum AssessmentStatus
    {
        [Display(Name = "Run damage assessment")]
        Ready,
        [Display(Name = "Running (please wait)")]
        Running,
        [Display(Name = "Cannot run (no basins)")]
        LoadingBasins,
    }
}