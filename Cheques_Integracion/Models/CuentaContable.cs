namespace Cheques_Integracion.Models
{
    public class CuentaContable
    {
        public int idCuentaContable { get; set; }
        public string descripcion { get; set; }
        public string tipoMovimiento { get; set; }
        public double monto { get; set; }
    }
}
