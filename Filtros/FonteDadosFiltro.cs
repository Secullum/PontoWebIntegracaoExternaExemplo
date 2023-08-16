namespace PontoWebIntegracaoExterna.Filtros
{
    public class FonteDadosFiltro
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string HoraInicio
        {
            get => _horaInicio;
            set
            {
                // 08-00 -> 08:00
                if (!string.IsNullOrEmpty(value))
                {
                    _horaInicio = value.Replace("-", ":");
                }
            }
        }
        public string HoraFim
        {
            get => _horaFim;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _horaFim = value.Replace("-", ":");
                }
            }
        }
        public string FuncionarioPis { get; set; }
        public string EquipamentoId { get; set; }
        public string Origem { get; set; }

        private string _horaFim;
        private string _horaInicio;
    }
}
