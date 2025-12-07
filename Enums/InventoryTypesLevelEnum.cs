using System.Reflection;

namespace SistemaGestionInventario.Enums
{
    public class InventoryTypesLevelEnum
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        private InventoryTypesLevelEnum(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static readonly InventoryTypesLevelEnum PARENT = new InventoryTypesLevelEnum("PARENT", "Categoría");
        public static readonly InventoryTypesLevelEnum CHILD = new InventoryTypesLevelEnum("CHILD", "Subcategoría");

        public override string ToString() => Description;
        public static InventoryTypesLevelEnum FromCode(string code)
        {
            var fields = typeof(InventoryTypesLevelEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as InventoryTypesLevelEnum;
                if (status != null && status.Code == code)
                    return status;
            }

            throw new ArgumentException($"No Status found with value '{code}'");
        }

        public static IList<InventoryTypesLevelEnum> GetAll()
        {
            var fields = typeof(InventoryTypesLevelEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            var statuses = new List<InventoryTypesLevelEnum>();
                
            foreach (var f in fields)
            {
                var status = f.GetValue(null) as InventoryTypesLevelEnum;

                statuses.Add(status!);
            }

            return statuses;
        }
    }
}
