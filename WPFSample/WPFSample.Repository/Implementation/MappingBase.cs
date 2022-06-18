
namespace WPFSample.Repository.Implementation
{
    public abstract class MappingBase
    {
        protected virtual string TABLE_NAME => string.Empty;
        protected const string ID_COLUMN = "Id";
    }
}
