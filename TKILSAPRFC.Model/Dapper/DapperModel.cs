namespace TKILSAPRFC.Model.Dapper
{
    public class DapperModel
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class DapperKey : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class DapperIgnore : Attribute
        {
        }
    }
}
