using System.Reflection;

namespace SistemaGestionInventario.Enums
{
    public class WarehouseStatusEnum
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        private WarehouseStatusEnum(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static readonly WarehouseStatusEnum AC = new WarehouseStatusEnum("AC", "Activo");
        public static readonly WarehouseStatusEnum IN = new WarehouseStatusEnum("IN", "Inactivo");
        public static readonly WarehouseStatusEnum MTN = new WarehouseStatusEnum("MTN", "Mantenimiento");

        public override string ToString() => Description;
        public static WarehouseStatusEnum FromCode(string code)
        {
            var fields = typeof(WarehouseStatusEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as WarehouseStatusEnum;
                if (status != null && status.Code == code)
                    return status;
            }

            throw new ArgumentException($"No Status found with value '{code}'");
        }

        public static IList<WarehouseStatusEnum> GetAll()
        {
            var fields = typeof(WarehouseStatusEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            var statuses = new List<WarehouseStatusEnum>();

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as WarehouseStatusEnum;

                statuses.Add(status!);
            }

            return statuses;
        }
    }
}
