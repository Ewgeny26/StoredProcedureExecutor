using StoredProcedureExecutor.Infrastructure;
using StoredProcedureExecutor.Services.Contracts;
using System.Data;

namespace StoredProcedureExecutor.Dtos
{
    public class ParamDto : NotifyPropertyChangedBase, IBaseParamInfo
    {
        private string _name = string.Empty;
        private string _alias = string.Empty;
        private SqlDbType _type;
        private string? _value;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Alias
        {
            get => _alias;
            set => SetProperty(ref _alias, value);
        }
        public SqlDbType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }
        public string? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
        public int? Id { get; set; }

    }
}
