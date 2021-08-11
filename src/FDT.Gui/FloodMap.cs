using System.ComponentModel;
using System.Runtime.CompilerServices;
using FDT.Gui.Annotations;

namespace FDT.Gui
{
    public class FloodMap:IFloodMap
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

        public virtual bool HasReturnPeriod => false;
        public int ReturnPeriod { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}