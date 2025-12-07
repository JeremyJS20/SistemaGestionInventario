using System.Reflection;

namespace SistemaGestionInventario.Enums
{
    public class CommonStatusesEnum
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        private CommonStatusesEnum(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static readonly CommonStatusesEnum AC = new CommonStatusesEnum("AC", "Activo");
        public static readonly CommonStatusesEnum IN = new CommonStatusesEnum("IN", "Inactivo");

        public override string ToString() => Description;
        public static CommonStatusesEnum FromCode(string code)
        {
            var fields = typeof(CommonStatusesEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as CommonStatusesEnum;
                if (status != null && status.Code == code)
                    return status;
            }

            throw new ArgumentException($"No Status found with value '{code}'");
        }

        public static IList<CommonStatusesEnum> GetAll()
        {
            var fields = typeof(CommonStatusesEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            var statuses = new List<CommonStatusesEnum>();
                
            foreach (var f in fields)
            {
                var status = f.GetValue(null) as CommonStatusesEnum;

                statuses.Add(status!);
            }

            return statuses;
        }
    }
}
