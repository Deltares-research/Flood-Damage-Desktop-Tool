using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FDT.Gui.ViewModels
{
    public abstract class BaseFloodMap:IFloodMap
    {
        private string _mapPath;

        public string MapPath
        {
            get => _mapPath;
            set
            {
                _mapPath = value;
                OnPropertyChanged();
            } 
        }

        public abstract bool HasReturnPeriod { get; }

        public int ReturnPeriod { get; set; }


        #region Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

    public class FloodMap : BaseFloodMap
    {
        public override bool HasReturnPeriod => false;
    }
    public class FloodMapWithReturnPeriod : BaseFloodMap
    {
        public override bool HasReturnPeriod => true;
    }
}