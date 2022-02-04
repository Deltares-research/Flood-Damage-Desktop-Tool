using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FIAT.Gui.ViewModels
{
    public abstract class BaseFloodMap: IFloodMap
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
        public Func<string> GetDefaultHazardDirectory { get; set; }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                if (columnName != nameof(ReturnPeriod)) return null;
                if (!HasReturnPeriod) return null;
                return HasValidReturnPeriod() ? null : Resources.BaseFloodMap_this_Return_period_should_be_greater_than_0;
            }
        }

        protected abstract bool HasValidReturnPeriod();

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
        protected override bool HasValidReturnPeriod()
        {
            throw new NotImplementedException();
        }
    }
    public class FloodMapWithReturnPeriod : BaseFloodMap
    {
        public override bool HasReturnPeriod => true;
        protected override bool HasValidReturnPeriod()
        {
            return ReturnPeriod > 0;
        }
    }
}