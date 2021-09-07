using System.ComponentModel.DataAnnotations;

namespace FDT.Gui.ViewModels
{
    public enum AssessmentStatus
    {
        [Display(Name = "Run Damage Assessment")]
        Ready,
        [Display(Name = "Running (please wait)")]
        Running,
        [Display(Name = "Cannot run (no basins)")]
        LoadingBasins,
    }
}