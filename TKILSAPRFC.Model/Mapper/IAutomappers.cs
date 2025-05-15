namespace TKILSAPRFC.Model.Mapper
{
    public interface IAutomappers
    {
        public D MappModel<D, S>(S mappModel);

        public List<D> MappListModel<D, S>(List<S> mappModel);
        public IEnumerable<D> MappListModel<D, S>(IEnumerable<S> mappModel);
    }
}
