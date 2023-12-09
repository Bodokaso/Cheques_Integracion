namespace Cheques_Integracion.Models
{
    public class AsientoCheques
    {
        public string descripcion { get; set; }
        public int idAuxiliar { get; set; }
        public List<CuentaContable> cuentas { get; set; }
    }
}
